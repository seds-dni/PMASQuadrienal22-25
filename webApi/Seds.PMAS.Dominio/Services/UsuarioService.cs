using System;
using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Dominio.Interfaces.Repositories;
using Seds.PMAS.Dominio.Interfaces.Services;
using Seds.Seguranca.Negocio;
using Seds.PMAS.Resource.Resources;

namespace Seds.PMAS.Dominio.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepositorio;

        public UsuarioService(IUsuarioRepository usuarioRepositorio)
            //: base(usuarioRepositorio)
        {
            this._usuarioRepositorio = usuarioRepositorio;
        }

        public UsuarioEntity GetUsuarioByCPF(string cpf)
        {
            return _usuarioRepositorio.GetUsuarioByCPF(cpf);
        }


        public UsuarioEntity GetByIdUsuario(int idUsuario)
        {
            return _usuarioRepositorio.GetByIdUsuario(idUsuario);
        }

        public UsuarioEntity RetornaDadosIDUsuario(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public void Update(UsuarioEntity usuario)
        {
            throw new NotImplementedException();
        }

        public void Create(UsuarioEntity usuario)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public UsuarioAutenticado Autenticar(string login, string senha)
        {
            var user = new Usuario().Autenticar(login, senha);
            if (user == null)
            {
                throw new Exception(Errors.InvalidCredentials);
            }
            return user;
        }
    }
}
