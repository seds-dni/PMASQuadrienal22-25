using Seds.PMAS.QUADRIENAL.Entidades.RedeProtecaoSocial;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio.RedeProtecaoSocial
{
    public class DemandasParlamentaresServicosCRAS
    {

        private static IRepository<DemandasParlamentaresServicosCRASInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<DemandasParlamentaresServicosCRASInfo>>();
            }
        }

        public IQueryable<DemandasParlamentaresServicosCRASInfo> GetIdServico(int idServicoRecursoFinanceiroCRAS)
        {
            return _repository.GetQuery().Where(s => s.ServicoRecursoFinanceiroCRAS == idServicoRecursoFinanceiroCRAS);
        }

        public void SalvarDemandasServicosCRAS(DemandasParlamentaresServicosCRASInfo demandas)
        {
            bool commit = true;

            if (!_repository.GetAll().Any(c => c.ServicoRecursoFinanceiroCRAS == demandas.ServicoRecursoFinanceiroCRAS))
            {
                _repository.Add(demandas);
            }
            else
            {
                demandas.Id = _repository.GetAll().Where(c => c.ServicoRecursoFinanceiroCRAS == demandas.ServicoRecursoFinanceiroCRAS).First().Id;
                _repository.Update(demandas);
            }

            if (commit)
                ContextManager.Commit();
        }
    }
}