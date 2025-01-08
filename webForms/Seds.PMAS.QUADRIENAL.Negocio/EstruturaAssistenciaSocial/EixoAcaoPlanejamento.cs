using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Transactions;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class EixoAcaoPlanejamento
    {
        private static IRepository<EixoAcaoPlanejamentoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<EixoAcaoPlanejamentoInfo>>();
            }
        }

        public IQueryable<EixoAcaoPlanejamentoInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public EixoAcaoPlanejamentoInfo GetById(int id)
        {
            var p = _repository.Single(m => m.Id == id);
            return p;
        }          
    }
}
