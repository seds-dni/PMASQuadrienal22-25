using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class Formacao
    {
        private static IRepository<FormacaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<FormacaoInfo>>();
            }
        }

        public IQueryable<FormacaoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public FormacaoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
