using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Entidades;
using StructureMap;


namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class MotivoDesativacaoServico
    {
        private static IRepository<MotivoDesativacaoServicoInfo> _repository 
        {
            get {
                return ObjectFactory.GetInstance<IRepository<MotivoDesativacaoServicoInfo>>();
            }
        }

        public IQueryable<MotivoDesativacaoServicoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public MotivoDesativacaoServicoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }   
    }
}
