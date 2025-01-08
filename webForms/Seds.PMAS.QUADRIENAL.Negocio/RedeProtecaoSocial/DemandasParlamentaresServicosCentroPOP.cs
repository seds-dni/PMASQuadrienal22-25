using Seds.PMAS.QUADRIENAL.Entidades.RedeProtecaoSocial;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio.RedeProtecaoSocial
{
    public class DemandasParlamentaresServicosCentroPOP
    {

        private static IRepository<DemandasParlamentaresServicosCentroPOPInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<DemandasParlamentaresServicosCentroPOPInfo>>();
            }
        }

        public IQueryable<DemandasParlamentaresServicosCentroPOPInfo> GetIdServico(int idServicoRecursoFinanceiroCentroPOP)
        {
            return _repository.GetQuery().Where(s => s.ServicoRecursoFinanceiroCentroPOP == idServicoRecursoFinanceiroCentroPOP);
        }

        public void SalvarDemandasServicosCentroPOP(DemandasParlamentaresServicosCentroPOPInfo demandas)
        {
            bool commit = true;

            if (!_repository.GetAll().Any(c => c.ServicoRecursoFinanceiroCentroPOP == demandas.ServicoRecursoFinanceiroCentroPOP))
            {
                _repository.Add(demandas);
            }
            else
            {
                demandas.Id = _repository.GetAll().Where(c => c.ServicoRecursoFinanceiroCentroPOP == demandas.ServicoRecursoFinanceiroCentroPOP).First().Id;
                _repository.Update(demandas);
            }

            if (commit)
                ContextManager.Commit();
        }
    }
}