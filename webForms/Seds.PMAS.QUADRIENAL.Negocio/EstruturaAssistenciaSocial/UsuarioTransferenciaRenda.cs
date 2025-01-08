using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class UsuarioTransferenciaRenda
    {
        private static IRepository<UsuarioTransferenciaRendaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<UsuarioTransferenciaRendaInfo>>();
            }
        }

        public IQueryable<UsuarioTransferenciaRendaInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public UsuarioTransferenciaRendaInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
