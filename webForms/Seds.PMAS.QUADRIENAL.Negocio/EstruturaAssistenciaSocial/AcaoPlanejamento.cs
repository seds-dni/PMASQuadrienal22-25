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
    public class AcaoPlanejamento
    {
        private static IRepository<AcaoPlanejamentoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<AcaoPlanejamentoInfo>>();
            }
        }        

        public IQueryable<AcaoPlanejamentoInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public AcaoPlanejamentoInfo GetById(int id)
        {
            var p = _repository.Single(m => m.Id == id);
            return p;
        }

        public IQueryable<AcaoPlanejamentoInfo> GetByEixo(int idEixoAcaoPlanejamento)
        {
            return _repository.GetQuery().Where(m => m.IdEixoAcaoPlanejamento == idEixoAcaoPlanejamento);
        }           
    }
}
