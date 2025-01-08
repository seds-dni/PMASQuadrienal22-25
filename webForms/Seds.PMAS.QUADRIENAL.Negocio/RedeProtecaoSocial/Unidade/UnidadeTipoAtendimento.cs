using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using StructureMap;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class UnidadeTipoAtendimento
    {
        private static IRepository<UnidadeTipoAtendimentoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<UnidadeTipoAtendimentoInfo>>();
            }
        }


        public IQueryable<UnidadeTipoAtendimentoInfo> GetAll()
        {
            return _repository.GetQuery();
        }

    }
}
