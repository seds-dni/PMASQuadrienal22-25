using Seds.PMAS.Dominio.Entities;
using Seds.Seguranca.Negocio;
using System;
using System.Data;

namespace Seds.PMAS.Dominio.Interfaces.Services
{
    public interface IUsuarioService : IDisposable
    {
        UsuarioAutenticado Autenticar(string login, string senha);
        UsuarioEntity RetornaDadosIDUsuario(int idUsuario);
        UsuarioEntity GetByIdUsuario(int idUsuario);
        UsuarioEntity GetUsuarioByCPF(string cpf);

        void Update(UsuarioEntity usuario);

        void Create(UsuarioEntity usuario);
    }
}
