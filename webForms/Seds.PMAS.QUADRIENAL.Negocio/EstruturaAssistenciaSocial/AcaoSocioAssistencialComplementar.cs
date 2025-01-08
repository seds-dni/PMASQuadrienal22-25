using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class AcaoSocioAssistencialComplementar
    {
        private static IRepository<AcaoSocioAssistencialComplementarInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<AcaoSocioAssistencialComplementarInfo>>();
            }
        }

        public IQueryable<AcaoSocioAssistencialComplementarInfo> GetAll()
        {
            return _repository.GetQuery();
        }       

        public AcaoSocioAssistencialComplementarInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
