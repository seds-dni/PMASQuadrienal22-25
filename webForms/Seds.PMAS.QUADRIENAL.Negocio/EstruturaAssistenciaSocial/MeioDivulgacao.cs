using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class MeioDivulgacao
    {
        private static IRepository<MeioDivulgacaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<MeioDivulgacaoInfo>>();
            }
        }

        public IQueryable<MeioDivulgacaoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public MeioDivulgacaoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
