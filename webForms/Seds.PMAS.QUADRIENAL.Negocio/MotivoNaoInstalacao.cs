using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class MotivoNaoInstalacao
    {
        private static IRepository<MotivoNaoInstalacaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<MotivoNaoInstalacaoInfo>>();
            }
        }

        public IQueryable<MotivoNaoInstalacaoInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public IQueryable<MotivoNaoInstalacaoInfo> GetCRAS()
        {
            return _repository.GetQuery().Where(m => m.Tipo == "CRAS");
        }

        public IQueryable<MotivoNaoInstalacaoInfo> GetCREAS()
        {
            return _repository.GetQuery().Where(m => m.Tipo == "CREAS");
        }

        public IQueryable<MotivoNaoInstalacaoInfo> GetPOP()
        {
            return _repository.GetQuery().Where(m => m.Tipo == "POP");
        }

        public MotivoNaoInstalacaoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }        
    }
}
