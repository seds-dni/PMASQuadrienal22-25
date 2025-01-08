using Seds.PMAS.QUADRIENAL.Entidades.EstruturaAssistenciaSocial;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio.EstruturaAssistenciaSocial
{
    public class ConsorcioPublico
    {
        private static IRepository<ConsorcioPublicoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsorcioPublicoInfo>>();
            }
        }

        public IQueryable<ConsorcioPublicoInfo> GetIdServico(int idServicoRecursoFinanceiroPublico)
        {
            return _repository.GetQuery().Where(s => s.IdServicosRecursosFinanceirosPublico == idServicoRecursoFinanceiroPublico);
        }

        public void SalvarConsorcioPublico(ConsorcioPublicoInfo consorcio) 
        {

            if (!_repository.GetAll().Any(c => c.IdServicosRecursosFinanceirosPublico == consorcio.IdServicosRecursosFinanceirosPublico))
            {
                _repository.Add(consorcio);
            }
            else
            {
                consorcio.Id = _repository.GetAll().Where(c => c.IdServicosRecursosFinanceirosPublico == consorcio.IdServicosRecursosFinanceirosPublico).First().Id;
                _repository.Update(consorcio);
            }

        }
    }
}
