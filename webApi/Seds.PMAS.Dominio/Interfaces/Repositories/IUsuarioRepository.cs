using Seds.PMAS.Dominio.Entities;
using System;

namespace Seds.PMAS.Dominio.Interfaces.Repositories
{
    public interface IUsuarioRepository : IDisposable
        
        //IEntityBaseRepository<UsuarioEntity>
    {
        UsuarioEntity GetByIdUsuario(int idUsuario);
        UsuarioEntity GetUsuarioByCPF(string cpf);
        //UsuarioEntity Autenticar(string email, string password);
        UsuarioEntity GetByEmail(string email);
        //void Register(string name, string email, string password, string confirmPassword);
        void Create(UsuarioEntity usuario);
        void Update(UsuarioEntity usuario);
    }
}
