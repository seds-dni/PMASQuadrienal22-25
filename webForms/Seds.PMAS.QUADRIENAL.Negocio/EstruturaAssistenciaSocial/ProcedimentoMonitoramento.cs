using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class ProcedimentoMonitoramento
    {
        private static IRepository<ProcedimentoMonitoramentoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ProcedimentoMonitoramentoInfo>>();
            }
        }

        public IQueryable<ProcedimentoMonitoramentoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ProcedimentoMonitoramentoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
