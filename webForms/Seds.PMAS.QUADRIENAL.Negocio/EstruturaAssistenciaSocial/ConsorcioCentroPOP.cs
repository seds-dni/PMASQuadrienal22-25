using Seds.PMAS.QUADRIENAL.Entidades.EstruturaAssistenciaSocial;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio.EstruturaAssistenciaSocial
{
    public class ConsorcioCentroPOP
    {
        private static IRepository<ConsorcioCentroPOPInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsorcioCentroPOPInfo>>();
            }
        }

        public IQueryable<ConsorcioCentroPOPInfo> GetIdServico(int idServicoRecursoFinanceiroCentroPOP)
        {
            return _repository.GetQuery().Where(s => s.IdServicosRecursosFinanceirosCentroPOP == idServicoRecursoFinanceiroCentroPOP);
        }

        public void SalvarConsorcioCentroPOP(ConsorcioCentroPOPInfo consorcio)
        {
            bool commit = true;

            if (!_repository.GetAll().Any(c => c.IdServicosRecursosFinanceirosCentroPOP == consorcio.IdServicosRecursosFinanceirosCentroPOP))
            {
                _repository.Add(consorcio);
            }
            else
            {
                consorcio.Id = _repository.GetAll().Where(c => c.IdServicosRecursosFinanceirosCentroPOP == consorcio.IdServicosRecursosFinanceirosCentroPOP).First().Id;
                _repository.Update(consorcio);
            }

            if (commit)
                ContextManager.Commit();

        }
    }
}
