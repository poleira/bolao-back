﻿using BolaoTeste.Aplicacao.Cadastros.Servicos;
using BolaoTeste.Aplicacao.Cadastros.Servicos.Interfaces;
using BolaoTeste.Aplicacao.Rank.Servicos;
using BolaoTeste.Aplicacao.Rank.Servicos.Interfaces;
using BolaoTeste.Data.Dto.Cadastros;
using BolaoTeste.Dto.Rank;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BolaoTeste.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsImplementationPolicy")]
    public class RankController : Controller
    {
        private readonly IRankServico rankServico;
        public RankController(IRankServico rankServico)
        {
            this.rankServico = rankServico;
        }

        [HttpGet]
        public ActionResult<IList<RankResponse>> ListarRank()
        {
            var retorno = rankServico.ListarRank();
            return Ok(retorno);
        }
    }
}
