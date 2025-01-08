using Seds.PMAS.QUADRIENAL.Entidades.EstruturaAssistenciaSocial;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio.EstruturaAssistenciaSocial
{
    public class ConsorcioCREAS
    {
        private static IRepository<ConsorcioCREASInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsorcioCREASInfo>>();
            }
        }

        public IQueryable<ConsorcioCREASInfo> GetIdServico(int idServicoRecursoFinanceiroCREAS)
        {
            return _repository.GetQuery().Where(s => s.IdServicosRecursosFinanceirosCREAS == idServicoRecursoFinanceiroCREAS);
        }

        public void SalvarConsorcioCREAS(ConsorcioCREASInfo consorcio)
        {
            bool commit = true;

            if (!_repository.GetAll().Any(c => c.IdServicosRecursosFinanceirosCREAS == consorcio.IdServicosRecursosFinanceirosCREAS))
            {
                _repository.Add(consorcio);
            }
            else
            {
                consorcio.Id = _repository.GetAll().Where(c => c.IdServicosRecursosFinanceirosCREAS == consorcio.IdServicosRecursosFinanceirosCREAS).First().Id;
                _repository.Update(consorcio);
            }


            if (commit)
                ContextManager.Commit();
        }
    }
}