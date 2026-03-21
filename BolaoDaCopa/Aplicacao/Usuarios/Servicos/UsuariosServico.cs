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
                    Credential = GoogleCredential.FromFile(@"C:\Users\MARCO\Desktop\World-Cup-Betting-Game\Bolao (backend)\BolaoDaCopa\bolao-33185-firebase-adminsdk-fbsvc-fb8ec571f6.json")
                });
            }
        }

        public AutenticacaoResponse Inserir(UsuarioRequest request)
        {
            try
            {
                Usuario usuario = new(request.Nome, request.Email, request.FirebaseUid);

                _unitOfWork.BeginTransaction();

                VerificarUsuarioExistente(new VerificarUsuarioExistenteRequest { Email = request.Email, Nome = request.Nome });

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
                        Nome = usuario.Nome,
                        Email = usuario.Email,
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

        public UsuarioResponse ObterUsuarioLogado(int idUsuario)
        {
            var query = _usuariosRepositorio.Query();
            
            Usuario? usuario = query.Where(x => x.Id == idUsuario).FirstOrDefault();

            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            return new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
        }

        public void VerificarUsuarioExistente(VerificarUsuarioExistenteRequest request)
        {
            var query = _usuariosRepositorio.Query();

            bool usuarioExistente = query.Any(x => x.Email == request.Email || x.Nome == request.Nome);

            if (usuarioExistente)
            {
                throw new Exception("Já existe um usuário cadastrado com este nome ou email.");
            }
        }

        public void AlterarNome(int idUsuario, string novoNome)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var query = _usuariosRepositorio.Query();
                Usuario? usuario = query.Where(x => x.Id == idUsuario).FirstOrDefault();

                if (usuario == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }

                // Verificar se já existe outro usuário com o novo nome
                bool nomeExistente = query.Any(x => x.Nome == novoNome && x.Id != idUsuario);
                if (nomeExistente)
                {
                    throw new Exception("Já existe um usuário cadastrado com este nome.");
                }

                usuario.SetNome(novoNome);
                _usuariosRepositorio.Editar(usuario);

                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
