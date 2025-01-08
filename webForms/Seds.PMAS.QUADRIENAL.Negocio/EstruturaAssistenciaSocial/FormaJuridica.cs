using Seds.PMAS.QUADRIENAL.Entidades.EstruturaAssistenciaSocial;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio.EstruturaAssistenciaSocial
{
    public class FormaJuridica
    {
        private static IRepository<FormaJuridicaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<FormaJuridicaInfo>>();
            }
        }

        public IQueryable<FormaJuridicaInfo> GetAll()
        {
            return _repository.GetQuery();
        }
    }
}
