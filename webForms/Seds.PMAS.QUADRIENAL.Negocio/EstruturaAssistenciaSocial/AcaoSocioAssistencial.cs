using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class AcaoSocioAssistencial
    {
        private static IRepository<AcaoSocioAssistencialInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<AcaoSocioAssistencialInfo>>();
            }
        }

        public IQueryable<AcaoSocioAssistencialInfo> GetCRAS()
        {
            return _repository.GetQuery().Where(a=> a.Tipo == "CRAS");
        }

        public IQueryable<AcaoSocioAssistencialInfo> GetCREAS()
        {
            return _repository.GetQuery().Where(a => a.Tipo == "CREAS");
        }

        public IQueryable<AcaoSocioAssistencialInfo> GetCentroPOP()
        {
            return _repository.GetQuery().Where(a => a.Tipo == "POP");
        }

        public IQueryable<AcaoSocioAssistencialInfo> GetUnidadesSocioAssistenciais() 
        {
            return _repository.GetQuery().Where(a => a.Tipo == "USA");
        }

        public AcaoSocioAssistencialInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
