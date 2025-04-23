using AutoMapper;
using BolaoDaCopa.Aplicacao.Comum.Repositorios;
using BolaoDaCopa.Aplicacao.Usuarios.Servicos.Interfaces;
using BolaoDaCopa.Bibliotecas.Transacoes.Interfaces;
using BolaoDaCopa.Dto.Autenticacao.Responses;
using BolaoDaCopa.Dto.Usuarios.Requests;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using BolaoDaCopa.Models;

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
    }
}
