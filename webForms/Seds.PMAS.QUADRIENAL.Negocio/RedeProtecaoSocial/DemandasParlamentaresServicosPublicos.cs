using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.RedeProtecaoSocial;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio.RedeProtecaoSocial
{
    public class DemandasParlamentaresServicosPublicos
    {

        private static IRepository<DemandasParlamentaresServicosPublicosInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<DemandasParlamentaresServicosPublicosInfo>>();
            }
        }

        public IQueryable<DemandasParlamentaresServicosPublicosInfo> GetIdServico(int idServico)
        {
            return _repository.GetQuery().Where(s => s.IdServicoRecursoFinanceiroFundosPublico == idServico);
        }

        public void SalvarDemandasServicosPublicos(DemandasParlamentaresServicosPublicosInfo demandas)
        {
            bool commit = true;

            if (!_repository.GetAll().Any(c => c.IdServicoRecursoFinanceiroFundosPublico == demandas.IdServicoRecursoFinanceiroFundosPublico))
            {
                _repository.Add(demandas);
            }
            else
            {
                demandas.Id = _repository.GetAll().Where(c => c.IdServicoRecursoFinanceiroFundosPublico == demandas.IdServicoRecursoFinanceiroFundosPublico).First().Id;
                _repository.Update(demandas);
            }

            if (commit)
                ContextManager.Commit();
        }
    }
}
