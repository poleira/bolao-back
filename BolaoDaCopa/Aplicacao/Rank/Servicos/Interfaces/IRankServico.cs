using BolaoTeste.Dto.Rank;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BolaoDaCopa.Aplicacao.Rank.Servicos.Interfaces
{
    public interface IRankServico
    {
        Task<IList<RankResponse>> ListarRankAsync(string hashBolao);
    }
}

