using System.Web;
using BolaoDaCopa.Aplicacao.ModosJogos.Servicos.Interfaces;
using BolaoDaCopa.Dto.ModosJogos.Responses;
using BolaoDaCopa.Dto.Regras.Responses;
using BolaoDaCopa.Infra.Repositorios.ModosJogosRegras.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Regras.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces;
using BolaoDaCopa.Services;

namespace BolaoDaCopa.Aplicacao.ModosJogos.Servicos
{
    public class ModosJogosServicos : IModosJogosServicos
    {
        private readonly IModosJogosRegrasRepositorios _modosJogosRegrasRepositorios;
        private readonly IRegrasRepositorio _regrasRepositorio;
        private readonly IBoloesRepositorio _boloesRepositorio;

        public ModosJogosServicos(IModosJogosRegrasRepositorios modosJogosRegrasRepositorios, IRegrasRepositorio regrasRepositorio, IBoloesRepositorio boloesRepositorio)
        {
            _modosJogosRegrasRepositorios = modosJogosRegrasRepositorios;
            _regrasRepositorio = regrasRepositorio;
            _boloesRepositorio = boloesRepositorio;
        }

        public IEnumerable<RegraResponse> ListarRegrasModoJogo(int idModoJogo)
        {
            var query = _modosJogosRegrasRepositorios.QueryModosJogosRegras()
                .Where(mr => mr.ModoJogo.Id == idModoJogo)
                .Select(mr => mr.Regra)
                .OrderBy(r => r.Id);

            return query.Select(r => new RegraResponse
            {
                Id = r.Id,
                Descricao = r.Descricao,
                Explicacao = r.Explicacao
            })
            .ToList();
        }

        public ModoJogoResponse? ObterModoPorHashBolao(string hashBolao)
        {
            if (string.IsNullOrEmpty(hashBolao)) return null;

            // Decode and normalize hash (same as BoloesServico)
            string hash = HttpUtility.UrlDecode(hashBolao).Replace(" ", "+");

            int idBolao;
            try
            {
                idBolao = int.Parse(CryptoHelper.Decrypt(hash));
            }
            catch
            {
                return null;
            }

            var bolao = _boloesRepositorio.Recuperar(idBolao);

            var modo = bolao?.ModoJogo;

            if (modo == null) return null;

            return new ModoJogoResponse
            {
                Id = modo.Id,
                NomeModoJogo = modo.NomeModoJogo
            };
        }
    }
}
