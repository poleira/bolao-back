﻿namespace BolaoDaCopa.Models
{
    public class PalpiteArtilheiro
    {
        public virtual int Id { get; set; }
        public virtual Jogador Jogador { get; set; }
        public virtual BolaoUsuario BolaoUsuario { get; set; }

        protected PalpiteArtilheiro() { }

        public PalpiteArtilheiro(Jogador jogador, BolaoUsuario bolaoUsuario)
        {
            Jogador = jogador;
            BolaoUsuario = bolaoUsuario;
        }
    }
}
