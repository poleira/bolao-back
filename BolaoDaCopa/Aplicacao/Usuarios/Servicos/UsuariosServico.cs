using AutoMapper;
using BolaoDaCopa.Aplicacao.Comum.Repositorios;
using BolaoDaCopa.Aplicacao.Usuarios.Servicos.Interfaces;
using BolaoDaCopa.Bibliotecas.Transacoes.Interfaces;
using BolaoDaCopa.Dto.Autenticacao.Responses;
using BolaoDaCopa.Dto.Usuarios;
using BolaoDaCopa.Dto.Usuarios.Requests;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using BolaoDaCopa.Models;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;

namespace BolaoDaCopa.Aplicacao.Usuarios.Servicos
{
    public class UsuariosServico : IUsuariosServico
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUsuariosRepositorio _usuariosRepositorio;

        public UsuariosServico(IMapper _mapper, IUnitOfWork _unitOfWork, IJwtTokenGenerator _jwtTokenGenerator, IUsuariosRepositorio _usuariosRepositorio)
        {
            this._mapper = _mapper;
            this._unitOfWork = _unitOfWork;
            this._jwtTokenGenerator = _jwtTokenGenerator;
            this._usuariosRepositorio = _usuariosRepositorio;

            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(@"C:\Github\Backend\bolao-back\BolaoDaCopa\generic-application-firebase-adminsdk.json")
                });
            }
        }

        public AutenticacaoResponse Inserir(UsuarioRequest request)
        {
            try
            {
                Usuario usuario = new(request.Nome, request.Email, request.FirebaseUid);

                _unitOfWork.BeginTransaction();

                _usuariosRepositorio.Inserir(usuario);

                var token = _jwtTokenGenerator.GerarToken(usuario);

                _unitOfWork.Commit();

                var response = new AutenticacaoResponse { Token = token };

                return response;
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<AutenticacaoResponse> Autenticar(LoginRequest request)
        {
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(request.Token);

                var uid = decodedToken.Uid;

                Usuario? usuario = RecuperarUsuarioPorFirebaseUid(uid);

                string? token = _jwtTokenGenerator.GerarToken(usuario);

                AutenticacaoResponse response = new AutenticacaoResponse
                {
                    Usuario = new UsuarioResponse
                    {
                        Id = usuario.Id,
                        Nome = usuario.Nome,
                        Email = usuario.Email,
                        FirebaseUid = usuario.FirebaseUid,
                    },
                    Token = token
                };

                return response;
            }
            catch
            {
                throw;
            }
        }

        public Usuario? RecuperarUsuarioPorFirebaseUid(string uid)
        {
            var query = _usuariosRepositorio.Query();

            return query.Where(x => x.FirebaseUid == uid).FirstOrDefault();
        }
    }
}
