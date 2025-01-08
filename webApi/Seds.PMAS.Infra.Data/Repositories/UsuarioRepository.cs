using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Dominio.Interfaces.Repositories;
using System.Linq;
using System;
using Seds.PMAS.Infra.Data.Context;

namespace Seds.PMAS.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private DBPMASContext _context;

        public UsuarioRepository(DBPMASContext context)
        {
            this._context = context;
        }

        public void Create(UsuarioEntity usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public UsuarioEntity GetByEmail(string email)
        {
            return _context.Usuarios.Where(u => u.Email == email).FirstOrDefault();
        }

        public UsuarioEntity GetByIdUsuario(int idUsuario)
        {
            return _context.Usuarios.Where(u => u.IdUsuario == idUsuario).FirstOrDefault();
        }
        public UsuarioEntity GetUsuarioByCPF(string cpf)
        {
            return _context.Usuarios.Where(u => u.CPF == cpf).FirstOrDefault();
        }

        public void Update(UsuarioEntity usuario)
        {
            _context.Entry<UsuarioEntity>(usuario).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
