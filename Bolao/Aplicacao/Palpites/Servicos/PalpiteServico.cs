using AutoMapper;
using BolaoTeste.Aplicacao.Palpites.Services;
using BolaoTeste.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoTeste.Data.Interfaces;
using BolaoTeste.Data.Repositorios.Interfaces;
using BolaoTeste.Dto;
using BolaoTeste.Dto.JogosBr;
using BolaoTeste.Dto.ListarPalpite;
using BolaoTeste.Dto.Palpites;
using BolaoTeste.Models;
using Microsoft.AspNetCore.Mvc;
using ISession = NHibernate.ISession;

namespace BolaoTeste.Aplicacao.Palpites.Servicos
{
    public class PalpiteServico : IPalpiteServico
    {
        private readonly ISession session;
        private readonly IMapper mapper;
        private readonly ICadastroRepositorio cadastroRepositorio;
        private readonly IPalpiteRepositorio palpiteRepositorio;

        public PalpiteServico(ISession session, IMapper mapper, ICadastroRepositorio cadastroRepositorio, IPalpiteRepositorio palpiteRepositorio)
        {
            this.session = session;
            this.mapper = mapper;
            this.cadastroRepositorio = cadastroRepositorio;
            this.palpiteRepositorio = palpiteRepositorio;
        }

        public OkResponse EditaCampeao(CampeaoEditarRequest request)
        {
            var campeao = mapper.Map<Campeao>(request);
            var queryUsuario = cadastroRepositorio.Query().Where(r => r.Usuario == request.Usuario).ToList();
            var transacao = session.BeginTransaction();
            try
            {
                foreach (var item in queryUsuario)
                {
                    campeao.Id = item.Campeao.Id;
                    item.Campeao = campeao;
                    
                    cadastroRepositorio.Editar(item);
                    if (transacao.IsActive)
                        transacao.Commit();
                    return new OkResponse { Ok = "Ok" };
                }

                return null;

            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }

        public OkResponse EditaFinais(FinaisEditarRequest request)
        {
            var finais = mapper.Map<Finais>(request);
            var queryUsuario = cadastroRepositorio.Query().Where(r => r.Usuario == request.Usuario).ToList();
            var transacao = session.BeginTransaction();
            try
            {
                foreach (var item in queryUsuario)
                {
                    finais.Id = item.Finais.Id;
                    item.Finais = finais;
                    
                    cadastroRepositorio.Editar(item);
                    if (transacao.IsActive)
                        transacao.Commit();
                    return new OkResponse { Ok = "Ok" };
                }

                return null;

            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }

        public OkResponse EditaGa(GaEditarRequest request)
        {
            string gaString = "ga";
            string idgaString = "idga";
            string primeiro = "";
            string segundo = "";

            if (request.Qatar == 1)
                primeiro = nameof(request.Qatar);
            if (request.Qatar == 2)
                segundo = nameof(request.Qatar);
            if (request.Equador == 1)
                primeiro = nameof(request.Equador);
            if (request.Equador == 2)
                segundo = nameof(request.Equador);
            if (request.Senegal == 1)
                primeiro = nameof(request.Senegal);
            if (request.Senegal == 2)
                segundo = nameof(request.Senegal);
            if (request.Holanda == 1)
                primeiro = nameof(request.Holanda);
            if (request.Holanda == 2)
                segundo = nameof(request.Holanda);




            var transacao = session.BeginTransaction();
            try
            {

            palpiteRepositorio.EditarGa(request,idgaString,gaString, primeiro, segundo);


                if (transacao.IsActive)
                    transacao.Commit();
                return new OkResponse { Ok = "Ok" };
            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }

        public OkResponse EditaGb(GbEditarRequest request)
        {
            string gbString = "gb";
            string idgbString = "idgb";
            string primeiro = "";
            string segundo = "";

            if (request.PaisDeGales == 1)
                primeiro = nameof(request.PaisDeGales);
            if (request.PaisDeGales == 2)
                segundo = nameof(request.PaisDeGales);
            if (request.Inglaterra == 1)
                primeiro = nameof(request.Inglaterra);
            if (request.Inglaterra == 2)
                segundo = nameof(request.Inglaterra);
            if (request.USA == 1)
                primeiro = nameof(request.USA);
            if (request.USA == 2)
                segundo = nameof(request.USA);
            if (request.Iram == 1)
                primeiro = nameof(request.Iram);
            if (request.Iram == 2)
                segundo = nameof(request.Iram);

            var transacao = session.BeginTransaction();
            try
            {

                
            palpiteRepositorio.EditarGb(request, idgbString, gbString,primeiro, segundo);

                if (transacao.IsActive)
                    transacao.Commit();
                return new OkResponse { Ok = "Ok" };
            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }


        }
        public OkResponse EditaGc(GcEditarRequest request)
        {
            string gcString = "gc";
            string idgcString = "idgc";
            string primeiro = "";
            string segundo = "";

            if (request.Argentina == 1)
                primeiro = nameof(request.Argentina);
            if (request.Argentina == 2)
                segundo = nameof(request.Argentina);
            if (request.ArabiaSaudita == 1)
                primeiro = nameof(request.ArabiaSaudita);
            if (request.ArabiaSaudita == 2)
                segundo = nameof(request.ArabiaSaudita);
            if (request.Mexico == 1)
                primeiro = nameof(request.Mexico);
            if (request.Mexico == 2)
                segundo = nameof(request.Mexico);
            if (request.Polonia == 1)
                primeiro = nameof(request.Polonia);
            if (request.Polonia == 2)
                segundo = nameof(request.Polonia);



            var transacao = session.BeginTransaction();
            try
            {

            palpiteRepositorio.EditarGc(request, idgcString, gcString, primeiro, segundo);


                if (transacao.IsActive)
                    transacao.Commit();
                return new OkResponse { Ok = "Ok" };
            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }
        public OkResponse EditaGd(GdEditarRequest request)
        {
            string gdString = "gd";
            string idgdString = "idgd";
            string primeiro = "";
            string segundo = "";

            if (request.Franca == 1)
                primeiro = nameof(request.Franca);
            if (request.Franca == 2)
                segundo = nameof(request.Franca);
            if (request.Australia == 1)
                primeiro = nameof(request.Australia);
            if (request.Australia == 2)
                segundo = nameof(request.Australia);
            if (request.Dinamarca == 1)
                primeiro = nameof(request.Dinamarca);
            if (request.Dinamarca == 2)
                segundo = nameof(request.Dinamarca);
            if (request.Tunisia == 1)
                primeiro = nameof(request.Tunisia);
            if (request.Tunisia == 2)
                segundo = nameof(request.Tunisia);



            var transacao = session.BeginTransaction();
            try
            {

            palpiteRepositorio.EditarGd(request, idgdString, gdString, primeiro, segundo);

                if (transacao.IsActive)
                    transacao.Commit();
                return new OkResponse { Ok = "Ok" };
            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }
    
        public OkResponse EditaGe(GeEditarRequest request)
        {
            string geString = "ge";
            string idgeString = "idge";
            string primeiro = "";
            string segundo = "";

            if (request.Espanha == 1)
                primeiro = nameof(request.Espanha);
            if (request.Espanha == 2)
                segundo = nameof(request.Espanha);
            if (request.CostaRica == 1)
                primeiro = nameof(request.CostaRica);
            if (request.CostaRica == 2)
                segundo = nameof(request.CostaRica);
            if (request.Alemanha == 1)
                primeiro = nameof(request.Alemanha);
            if (request.Alemanha == 2)
                segundo = nameof(request.Alemanha);
            if (request.Japao == 1)
                primeiro = nameof(request.Japao);
            if (request.Japao == 2)
                segundo = nameof(request.Japao);

            var transacao = session.BeginTransaction();
            try
            {

                palpiteRepositorio.EditarGe(request, idgeString, geString, primeiro, segundo);
                if (transacao.IsActive)
                    transacao.Commit();
                return new OkResponse { Ok = "Ok" };
            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }

        }
        public OkResponse EditaGf(GfEditarRequest request)
        {
            string gfString = "gf";
            string idgfString = "idgf";
            string primeiro = "";
            string segundo = "";

            if (request.Belgica == 1)
                primeiro = nameof(request.Belgica);
            if (request.Belgica == 2)
                segundo = nameof(request.Belgica);
            if (request.Canada == 1)
                primeiro = nameof(request.Canada);
            if (request.Canada == 2)
                segundo = nameof(request.Canada);
            if (request.Croacia == 1)
                primeiro = nameof(request.Croacia);
            if (request.Croacia == 2)
                segundo = nameof(request.Croacia);
            if (request.Marrocos == 1)
                primeiro = nameof(request.Marrocos);
            if (request.Marrocos == 2)
                segundo = nameof(request.Marrocos);


            var transacao = session.BeginTransaction();
            try
            {

                palpiteRepositorio.EditarGf(request, idgfString, gfString, primeiro, segundo);
                if (transacao.IsActive)
                    transacao.Commit();
                return new OkResponse { Ok = "Ok" };
            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }
        public OkResponse EditaGg(GgEditarRequest request)
        {
            string ggString = "gg";
            string idggString = "idgg";
            string primeiro = "";
            string segundo = "";

            if (request.Brasil == 1)
                primeiro = nameof(request.Brasil);
            if (request.Brasil == 2)
                segundo = nameof(request.Brasil);
            if (request.Servia == 1)
                primeiro = nameof(request.Servia);
            if (request.Servia == 2)
                segundo = nameof(request.Servia);
            if (request.Suica == 1)
                primeiro = nameof(request.Suica);
            if (request.Suica == 2)
                segundo = nameof(request.Suica);
            if (request.Camaroes == 1)
                primeiro = nameof(request.Camaroes);
            if (request.Camaroes == 2)
                segundo = nameof(request.Camaroes);


            var transacao = session.BeginTransaction();
            try
            {

                palpiteRepositorio.EditarGg(request, idggString, ggString, primeiro, segundo);
                if (transacao.IsActive)
                    transacao.Commit();
                return new OkResponse { Ok = "Ok" };
            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }
        public OkResponse EditaGh(GhEditarRequest request)
        {
            string ghString = "gh";
            string idghString = "idgh";
            string primeiro = "";
            string segundo = "";

            if (request.Portugal == 1)
                primeiro = nameof(request.Portugal);
            if (request.Portugal == 2)
                segundo = nameof(request.Portugal);
            if (request.Gana == 1)
                primeiro = nameof(request.Gana);
            if (request.Gana == 2)
                segundo = nameof(request.Gana);
            if (request.Uruguai == 1)
                primeiro = nameof(request.Uruguai);
            if (request.Uruguai == 2)
                segundo = nameof(request.Uruguai);
            if (request.CoreiaDoSul == 1)
                primeiro = nameof(request.CoreiaDoSul);
            if (request.CoreiaDoSul == 2)
                segundo = nameof(request.CoreiaDoSul);



            var transacao = session.BeginTransaction();
            try
            {

            palpiteRepositorio.EditarGh(request, idghString, ghString,primeiro,segundo);
                
                if (transacao.IsActive)
                    transacao.Commit();
                return new OkResponse { Ok = "Ok" };
            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }

        public OkResponse EditaJogosDoBr(JogosDoBrEditarRequest request)
        {
            var jogosDoBr = mapper.Map<Jogos_BR>(request);
            var queryUsuario = cadastroRepositorio.Query().Where(r => r.Usuario == request.Usuario).ToList();
            var transacao = session.BeginTransaction();
            try
            {
                foreach (var item in queryUsuario)
                {
                    jogosDoBr.Id = item.Jogos_BR.Id;
                    item.Jogos_BR = jogosDoBr;
                    
                    cadastroRepositorio.Editar(item);
                    if (transacao.IsActive)
                        transacao.Commit();
                    return new OkResponse { Ok = "Ok" };
                }

                return null;

            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }

        public OkResponse EditaQuartas (QuartasEditarRequest request)
        {
            var quartas = mapper.Map<Quartas>(request);
            var queryUsuario = cadastroRepositorio.Query().Where(r => r.Usuario == request.Usuario).ToList();
            var transacao = session.BeginTransaction();
            try
            {
                foreach (var item in queryUsuario)
                {
                    quartas.Id = item.Quartas.Id;
                    item.Quartas = quartas;
                    
                    cadastroRepositorio.Editar(item);
                    if (transacao.IsActive)
                        transacao.Commit();
                    return new OkResponse { Ok = "Ok" };
                }

                return null;

            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }

        public OkResponse EditaSemis(SemisEditarRequest request)
        {
            var semis = mapper.Map<Semis>(request);
            var queryUsuario = cadastroRepositorio.Query().Where(r => r.Usuario == request.Usuario).ToList();
            var transacao = session.BeginTransaction();
            try
            {
                foreach (var item in queryUsuario)
                {
                    semis.Id = item.Semis.Id;
                    item.Semis = semis;
                    
                    cadastroRepositorio.Editar(item);
                    if (transacao.IsActive)
                        transacao.Commit();
                    return new OkResponse { Ok = "Ok" };
                }

                return null;

            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }

        public ListarPalpiteResponse ListarPalpites(ListarPalpiteRequest request)
        {
            var queryUsuario = cadastroRepositorio.Query().Where(r => r.Usuario == request.Usuario).ToList();
            foreach (var item in queryUsuario)
            {
                var retorno = mapper.Map<ListarPalpiteResponse>(item);
                return retorno;
            }
            throw new Exception("Usuario nao encontrado");
        }

        public ListarOitavasResponse ListarOitavas(ListarOitavasRequest request)
        {
            var queryUsuario = cadastroRepositorio.Query().Where(r => r.Usuario == request.Usuario).FirstOrDefault();
            var usuario = mapper.Map<ListarOitavasResponse>(queryUsuario);
            usuario = TransformarEmSigla.TransformaEmSigla(usuario);
            return usuario;
        }

        public OkResponse EditaJogosBrGrupos (FaseDeGruposJogosBrRequest request)
        {
            var transacao = session.BeginTransaction();
            try
            {

                palpiteRepositorio.EditarJogosBrGrupos(request);

                if (transacao.IsActive)
                    transacao.Commit();
                return new OkResponse { Ok = "Ok" };
            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }
        public OkResponse EditaJogosBrOitavas(MataMataJogosBrRequest request)
        {
            var transacao = session.BeginTransaction();
                string oitavas = "oitavas";
            try
            {
                palpiteRepositorio.EditarJogosBrOitavas(request, oitavas);

                if (transacao.IsActive)
                    transacao.Commit();
                return new OkResponse { Ok = "Ok" };
            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }
        public OkResponse EditaJogosBrQuartas(MataMataJogosBrRequest request)
        {
            var transacao = session.BeginTransaction();
                string quartas = "quartas";
            try
            {
                palpiteRepositorio.EditarJogosBrQuartas(request, quartas);

                if (transacao.IsActive)
                    transacao.Commit();
                return new OkResponse { Ok = "Ok" };
            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }
        public OkResponse EditaJogosBrSemis(MataMataJogosBrRequest request)
        {
            var transacao = session.BeginTransaction();
                string semis = "semis";
            try
            {
                palpiteRepositorio.EditarJogosBrSemis(request, semis);

                if (transacao.IsActive)
                    transacao.Commit();
                return new OkResponse { Ok = "Ok" };
            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }
        public OkResponse EditaJogosBrFinais(MataMataJogosBrRequest request)
        {
            var transacao = session.BeginTransaction();
                string finais = "final";
            try
            {
                palpiteRepositorio.EditarJogosBrFinais(request, finais);

                if (transacao.IsActive)
                    transacao.Commit();
                return new OkResponse { Ok = "Ok" };
            }
            catch
            {
                if (transacao.IsActive)
                    transacao.Rollback();
                return null;
            }
        }
    }







    }

