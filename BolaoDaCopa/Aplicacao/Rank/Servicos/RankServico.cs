using BolaoDaCopa.Aplicacao.Rank.Servicos.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces;
using BolaoDaCopa.Models;
using BolaoDaCopa.Services;
using BolaoTeste.Dto.Rank;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISession = NHibernate.ISession;

namespace BolaoDaCopa.Aplicacao.Rank.Servicos
{
    public class RankServico : IRankServico
    {
        private static readonly Dictionary<string, int> DescricaoParaFaseMap = new Dictionary<string, int>
        {
            ["acertou pais nas 1/16 avos de final"] = 1,
            ["acertou pais nas oitavas"] = 2,
            ["acertou pais quartas"] = 3,
            ["acertou pais semis"] = 4,
            ["acertou pais final"] = 5,
            ["acertou campeao"] = 7,
            ["acertou terceiro colocado geral da copa"] = 8
        };

        private readonly ISession session;
        private readonly IBoloesRepositorio boloesRepositorio;
        private readonly IBoloesUsuariosRepositorio boloesUsuariosRepositorio;

        public RankServico(
            ISession session,
            IBoloesRepositorio boloesRepositorio,
            IBoloesUsuariosRepositorio boloesUsuariosRepositorio)
        {
            this.session = session;
            this.boloesRepositorio = boloesRepositorio;
            this.boloesUsuariosRepositorio = boloesUsuariosRepositorio;
        }

        public async Task<IList<RankResponse>> ListarRankAsync(string hashBolao)
        {
            if (string.IsNullOrWhiteSpace(hashBolao))
            {
                throw new ArgumentException("HashBolao inválido.", nameof(hashBolao));
            }

            int idBolao = int.Parse(CryptoHelper.Decrypt(hashBolao));

            var regrasPorFase = await ObterRegrasPontuacaoPorFaseAsync(idBolao);
            var participantes = await boloesUsuariosRepositorio.Query()
                .Where(x => x.Bolao.Id == idBolao)
                .Select(x => new { x.Id, UsuarioNome = x.Usuario.Nome })
                .ToListAsync();

            var acumuladores = participantes.ToDictionary(
                x => x.Id,
                x => new RankAccumulator(x.UsuarioNome));

            if (!acumuladores.Any())
            {
                return new List<RankResponse>();
            }

            if (!regrasPorFase.Any())
            {
                return ConverterParaResponse(acumuladores);
            }

            var faseIds = regrasPorFase.Keys.ToList();
            var gabaritoPorFase = await ObterGabaritoPorFaseAsync(faseIds);

            if (!gabaritoPorFase.Any())
            {
                return ConverterParaResponse(acumuladores);
            }

            var palpites = await session.Query<PalpiteFaseSelecao>()
                .Where(p => p.BolaoUsuario.Bolao.Id == idBolao)
                .Where(p => faseIds.Contains(p.Fase.Id))
                .Select(p => new
                {
                    BolaoUsuarioId = p.BolaoUsuario.Id,
                    FaseId = p.Fase.Id,
                    SelecaoId = p.Selecao.Id
                })
                .ToListAsync();

            foreach (var grupoUsuario in palpites.GroupBy(x => x.BolaoUsuarioId))
            {
                if (!acumuladores.TryGetValue(grupoUsuario.Key, out var accumulator))
                {
                    continue;
                }

                foreach (var grupoFase in grupoUsuario.GroupBy(x => x.FaseId))
                {
                    if (!regrasPorFase.TryGetValue(grupoFase.Key, out var pontosPorAcerto))
                    {
                        continue;
                    }

                    if (!gabaritoPorFase.TryGetValue(grupoFase.Key, out var gabaritoSelecoes))
                    {
                        continue;
                    }

                    var acertos = grupoFase
                        .Select(x => x.SelecaoId)
                        .Distinct()
                        .Count(id => gabaritoSelecoes.Contains(id));

                    accumulator.Pontuacao += acertos * pontosPorAcerto;
                }
            }

            return ConverterParaResponse(acumuladores);
        }

        private async Task<Dictionary<int, int>> ObterRegrasPontuacaoPorFaseAsync(int idBolao)
        {
            var regras = await boloesRepositorio.QueryBolaoRegra()
                .Where(x => x.Bolao.Id == idBolao)
                .Select(x => new { x.Pontuacao, x.Regra.Descricao })
                .ToListAsync();

            var resultado = new Dictionary<int, int>();
            foreach (var regra in regras)
            {
                var faseId = MapearDescricaoParaFase(regra.Descricao);
                if (!faseId.HasValue)
                {
                    continue;
                }

                resultado[faseId.Value] = regra.Pontuacao;
            }

            return resultado;
        }

        private async Task<Dictionary<int, HashSet<int>>> ObterGabaritoPorFaseAsync(IEnumerable<int> faseIds)
        {
            var resultado = new Dictionary<int, HashSet<int>>();

            foreach (var faseId in faseIds.Distinct())
            {
                var fase = await session.GetAsync<Fase>(faseId);
                if (fase?.Selecoes == null)
                {
                    continue;
                }

                var selecoes = fase.Selecoes
                    .Where(fs => fs?.Selecao != null)
                    .Select(fs => fs.Selecao.Id)
                    .ToHashSet();

                if (selecoes.Any())
                {
                    resultado[faseId] = selecoes;
                }
            }

            return resultado;
        }

        private static IList<RankResponse> ConverterParaResponse(Dictionary<int, RankAccumulator> acumuladores)
        {
            return acumuladores.Values
                .OrderByDescending(x => x.Pontuacao)
                .ThenBy(x => x.Usuario, StringComparer.OrdinalIgnoreCase)
                .Select(x => new RankResponse(x.Usuario, x.Pontuacao))
                .ToList();
        }

        private static int? MapearDescricaoParaFase(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                return null;
            }

            var normalizado = NormalizarDescricao(descricao);
            if (DescricaoParaFaseMap.TryGetValue(normalizado, out var faseId))
            {
                return faseId;
            }

            if (normalizado.Contains("1/16"))
            {
                return 1;
            }

            if (normalizado.Contains("oitav"))
            {
                return 2;
            }

            if (normalizado.Contains("quart"))
            {
                return 3;
            }

            if (normalizado.Contains("semi"))
            {
                return 4;
            }

            if (normalizado.Contains("campea"))
            {
                return 7;
            }

            if (normalizado.Contains("terceir"))
            {
                return 8;
            }

            if (normalizado.Contains("final"))
            {
                return 5;
            }

            return null;
        }

        private static string NormalizarDescricao(string descricao)
        {
            var texto = descricao.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder(texto.Length);

            foreach (var caractere in texto)
            {
                var categoria = CharUnicodeInfo.GetUnicodeCategory(caractere);
                if (categoria != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(char.ToLowerInvariant(caractere));
                }
            }

            return builder.ToString().Normalize(NormalizationForm.FormC).Trim();
        }

        private class RankAccumulator
        {
            public RankAccumulator(string usuario)
            {
                Usuario = usuario;
            }

            public string Usuario { get; }
            public int Pontuacao { get; set; }
        }
    }
}
