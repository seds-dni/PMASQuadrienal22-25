using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class InterfacePublicaEducacao
    {
        private static IRepository<InterfacePublicaEducacaoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<InterfacePublicaEducacaoInfo>>();
            }
        }

        public InterfacePublicaEducacaoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }

        public InterfacePublicaEducacaoInfo GetByPrefeitura(int idPrefeitura)
        {
            return _repository.Single(m => m.IdPrefeitura == idPrefeitura);
        }

        public void Add(InterfacePublicaEducacaoInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarEducacao(obj);
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Update(InterfacePublicaEducacaoInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarEducacao(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }     
    }
}
