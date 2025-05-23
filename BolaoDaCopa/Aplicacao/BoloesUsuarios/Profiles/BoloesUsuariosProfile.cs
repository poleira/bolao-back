﻿using AutoMapper;
using BolaoDaCopa.Dto.BoloesUsuarios.Responses;
using BolaoDaCopa.Models;

namespace BolaoDaCopa.Aplicacao.BoloesUsuarios.Profiles
{
    public class BoloesUsuariosProfile : Profile
    {
        public BoloesUsuariosProfile()
        {
            CreateMap<BolaoUsuario, BolaoUsuarioResponse>();
        }
    }
}
