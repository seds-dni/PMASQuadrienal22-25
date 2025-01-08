using Seds.PMAS.QUADRIENAL.Entidades.EstruturaAssistenciaSocial;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio.EstruturaAssistenciaSocial
{
    public class ConsorcioCRAS
    {
        private static IRepository<ConsorcioCRASInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsorcioCRASInfo>>();
            }
        }

        public IQueryable<ConsorcioCRASInfo> GetIdServico(int idServicoRecursoFinanceiroCRAS)
        {
            return _repository.GetQuery().Where(s => s.IdServicosRecursosFinanceirosCRAS == idServicoRecursoFinanceiroCRAS);
        }

        public void SalvarConsorcioCRAS(ConsorcioCRASInfo consorcio)
        {
            bool commit = true;

            if (!_repository.GetAll().Any(c => c.IdServicosRecursosFinanceirosCRAS == consorcio.IdServicosRecursosFinanceirosCRAS))
            {
                _repository.Add(consorcio);
            }
            else
            {
                consorcio.Id = _repository.GetAll().Where(c => c.IdServicosRecursosFinanceirosCRAS == consorcio.IdServicosRecursosFinanceirosCRAS).First().Id;
                _repository.Update(consorcio);
            }

            if (commit)
                ContextManager.Commit();

        }
    }
}