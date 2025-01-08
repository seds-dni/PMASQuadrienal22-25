using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class UsuarioProgramaProjeto
    {
        private static IRepository<UsuarioProgramaProjetoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<UsuarioProgramaProjetoInfo>>();
            }
        }

        public IQueryable<UsuarioProgramaProjetoInfo> GetUsuariosVivaLeite()
        {
            return _repository.GetQuery().Where(a => a.ProgramaProjeto == "vivaleite");
        }

        public UsuarioProgramaProjetoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }
    }
}
