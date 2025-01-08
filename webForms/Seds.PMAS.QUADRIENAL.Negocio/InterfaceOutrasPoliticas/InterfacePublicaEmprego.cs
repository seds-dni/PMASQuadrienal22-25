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
    public class InterfacePublicaEmprego
    {
        private static IRepository<InterfacePublicaEmpregoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<InterfacePublicaEmpregoInfo>>();
            }
        }

        public InterfacePublicaEmpregoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }

        public InterfacePublicaEmpregoInfo GetByPrefeitura(int idPrefeitura)
        {
            return _repository.Single(m => m.IdPrefeitura == idPrefeitura);
        }

        public void Add(InterfacePublicaEmpregoInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarEmprego(obj);
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Update(InterfacePublicaEmpregoInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarEmprego(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
