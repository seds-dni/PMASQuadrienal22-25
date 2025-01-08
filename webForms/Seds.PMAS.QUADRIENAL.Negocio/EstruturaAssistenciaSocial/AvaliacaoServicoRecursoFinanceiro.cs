using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class AvaliacaoServicoRecursoFinanceiro
    {
        private static IRepository<AvaliacaoServicoRecursoFinanceiroInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<AvaliacaoServicoRecursoFinanceiroInfo>>();
            }
        }

        public IQueryable<AvaliacaoServicoRecursoFinanceiroInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public AvaliacaoServicoRecursoFinanceiroInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }
    }
}
