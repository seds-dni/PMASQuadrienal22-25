using Seds.PMAS.QUADRIENAL.Entidades.RedeProtecaoSocial;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio.RedeProtecaoSocial
{
    public class DemandasParlamentaresServicosCREAS
    {

        private static IRepository<DemandasParlamentaresServicosCREASInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<DemandasParlamentaresServicosCREASInfo>>();
            }
        }

        public IQueryable<DemandasParlamentaresServicosCREASInfo> GetIdServico(int idServicoRecursoFinanceiroCREAS)
        {
            return _repository.GetQuery().Where(s => s.ServicoRecursoFinanceiroCREAS == idServicoRecursoFinanceiroCREAS);
        }

        public void SalvarDemandasServicosCREAS(DemandasParlamentaresServicosCREASInfo demandas)
        {
            bool commit = true;

            if (!_repository.GetAll().Any(c => c.ServicoRecursoFinanceiroCREAS == demandas.ServicoRecursoFinanceiroCREAS))
            {
                _repository.Add(demandas);
            }
            else
            {
                demandas.Id = _repository.GetAll().Where(c => c.ServicoRecursoFinanceiroCREAS == demandas.ServicoRecursoFinanceiroCREAS).First().Id;
                _repository.Update(demandas);
            }

            if (commit)
                ContextManager.Commit();
        }
    }
}