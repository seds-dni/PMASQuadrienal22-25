using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class InstrumentoMonitoramento
    {
        private static IRepository<InstrumentoMonitoramentoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<InstrumentoMonitoramentoInfo>>();
            }
        }

        public IQueryable<InstrumentoMonitoramentoInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public IQueryable<InstrumentoMonitoramentoInfo> GetByProcedimento(Int32 idProcedimento)
        {
            return _repository.GetQuery().Where(t => t.IdProcedimentoMonitoramento == idProcedimento);
        }

        public InstrumentoMonitoramentoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
