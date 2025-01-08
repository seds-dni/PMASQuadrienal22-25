using Seds.PMAS.QUADRIENAL.Entidades.RedeProtecaoSocial;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio.RedeProtecaoSocial
{
    public class DemandasParlamentaresServicosPrivado
    {

        private static IRepository<DemandasParlamentaresServicosPrivadosInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<DemandasParlamentaresServicosPrivadosInfo>>();
            }
        }

        public IQueryable<DemandasParlamentaresServicosPrivadosInfo> GetIdServico(int idServicoRecursoFinanceiroPrivado)
        {
            return _repository.GetQuery().Where(s => s.ServicoRecursoFinanceiroPrivado == idServicoRecursoFinanceiroPrivado);
        }

        public void SalvarDemandasServicosPrivados(DemandasParlamentaresServicosPrivadosInfo demandas)
        {
            bool commit = true;

            if (!_repository.GetAll().Any(c => c.ServicoRecursoFinanceiroPrivado == demandas.ServicoRecursoFinanceiroPrivado))
            {
                _repository.Add(demandas);
            }
            else
            {
                demandas.Id = _repository.GetAll().Where(c => c.ServicoRecursoFinanceiroPrivado == demandas.ServicoRecursoFinanceiroPrivado).First().Id;
                _repository.Update(demandas);
            }

            if (commit)
                ContextManager.Commit();
        }
    }
}