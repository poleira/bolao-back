﻿using BolaoDaCopa.Dto.Boloes.Responses;
using BolaoDaCopa.Dto.Usuarios;

namespace BolaoDaCopa.Dto.BoloesUsuarios.Responses
{
    public class BolaoUsuarioResponse
    {
        public int Id { get; set; }
        public BolaoResponse? Bolao { get; set; }
        public UsuarioResponse? Usuario { get; set; }

    }
}
