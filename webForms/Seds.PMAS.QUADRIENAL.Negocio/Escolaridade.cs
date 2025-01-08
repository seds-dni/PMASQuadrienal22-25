using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class Escolaridade
    {
        private static IRepository<EscolaridadeInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<EscolaridadeInfo>>();
            }
        }

        public IQueryable<EscolaridadeInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public EscolaridadeInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
