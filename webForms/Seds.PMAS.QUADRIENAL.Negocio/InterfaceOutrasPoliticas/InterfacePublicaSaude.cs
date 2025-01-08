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
    public class InterfacePublicaSaude
    {
        private static IRepository<InterfacePublicaSaudeInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<InterfacePublicaSaudeInfo>>();
            }
        }

        public InterfacePublicaSaudeInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }

        public InterfacePublicaSaudeInfo GetByPrefeitura(int idPrefeitura)
        {
            return _repository.Single(m => m.IdPrefeitura == idPrefeitura);
        }

        public void Add(InterfacePublicaSaudeInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarSaude(obj);
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Update(InterfacePublicaSaudeInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarSaude(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
