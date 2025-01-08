using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class AprimoramentoAcao
    {
        private static IRepository<AprimoramentoAcaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<AprimoramentoAcaoInfo>>();
            }
        }

        public IQueryable<AprimoramentoAcaoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public AprimoramentoAcaoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
