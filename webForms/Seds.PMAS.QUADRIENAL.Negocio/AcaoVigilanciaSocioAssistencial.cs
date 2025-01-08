using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class AcaoVigilanciaSocioAssistencial
    {
        private static IRepository<AcaoVigilanciaSocioAssistencialInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<AcaoVigilanciaSocioAssistencialInfo>>();
            }
        }

        public IQueryable<AcaoVigilanciaSocioAssistencialInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public IQueryable<AcaoVigilanciaSocioAssistencialInfo> GetByEixo(Int32 idAcaoVigilanciaSocioAssistencial)
        {
            return _repository.GetQuery().Where(t=> t.IdAcaoVigilanciaSocioAssistencial.HasValue && t.IdAcaoVigilanciaSocioAssistencial == idAcaoVigilanciaSocioAssistencial);
        }

        public AcaoVigilanciaSocioAssistencialInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
