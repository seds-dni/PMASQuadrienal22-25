using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class SituacaoVulnerabilidade
    {
        private static IRepository<SituacaoVulnerabilidadeInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<SituacaoVulnerabilidadeInfo>>();
            }
        }

        public IQueryable<SituacaoVulnerabilidadeInfo> GetAll()
        {
            return _repository.GetQuery();

        }        

        public SituacaoVulnerabilidadeInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }
    }
}
