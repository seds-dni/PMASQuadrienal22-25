using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Linq;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class OutraPolitica
    {
        private static IRepository<InterfacePublicaOutroServicoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<InterfacePublicaOutroServicoInfo>>();
            }
        }

        public IQueryable<InterfacePublicaOutroServicoInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public InterfacePublicaOutroServicoInfo GetById(int id)
        {
            var restaurante = _repository.Single(m => m.Id == id);
            return restaurante;
        }

        public IQueryable<InterfacePublicaOutroServicoInfo> GetByInterfacePolitica(int idInterfacePublicaPolitica)
        {
            return _repository.GetObjectSet().Where(r => r.IdInterfacePublicaOutraPolitica == idInterfacePublicaPolitica);
        }

        public void Add(InterfacePublicaOutroServicoInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarOutroServico(obj);
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Update(InterfacePublicaOutroServicoInfo obj, Boolean commit)
        {
            new ValidadorInterfacePublica().ValidarOutroServico(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(InterfacePublicaOutroServicoInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
