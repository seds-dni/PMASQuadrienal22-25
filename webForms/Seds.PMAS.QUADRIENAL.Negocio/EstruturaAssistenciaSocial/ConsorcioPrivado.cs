using Seds.PMAS.QUADRIENAL.Entidades.EstruturaAssistenciaSocial;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio.EstruturaAssistenciaSocial
{
    public class ConsorcioPrivado
    {
        private static IRepository<ConsorcioPrivadoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsorcioPrivadoInfo>>();
            }
        }

        public IQueryable<ConsorcioPrivadoInfo> GetIdServico(int idServicoRecursoFinanceiroPrivado)
        {
            return _repository.GetQuery().Where(s => s.IdServicosRecursosFinanceirosPrivado == idServicoRecursoFinanceiroPrivado);
        }

        public void SalvarConsorcioPrivado(ConsorcioPrivadoInfo consorcio)
        {

            if (!_repository.GetAll().Any(c => c.IdServicosRecursosFinanceirosPrivado == consorcio.IdServicosRecursosFinanceirosPrivado))
            {
                _repository.Add(consorcio);
            }
            else
            {
                consorcio.Id = _repository.GetAll().Where(c => c.IdServicosRecursosFinanceirosPrivado == consorcio.IdServicosRecursosFinanceirosPrivado).First().Id;
                _repository.Update(consorcio);
            }

        }
    }
}
