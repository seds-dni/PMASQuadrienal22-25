using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class AcoesDesenvolvidaProgramas
    {
        private static IRepository<AcoesDesenvolvidaProgramasInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<AcoesDesenvolvidaProgramasInfo>>();
            }
        }

        public IQueryable<AcoesDesenvolvidaProgramasInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public IQueryable<AcoesDesenvolvidaProgramasInfo> GetByIdProgramaProjeto(int idProgramaProjeto)
        {

            //       return _repository.GetObjectSet().Include("TipoParceria").Include("Parceria").Where(m => m.IdProgramaProjeto == idProgramaProjeto);
            return _repository.GetObjectSet().Include("AcoesDesenvolvidasPrograma").Where(m => m.IdProgramaProjeto == idProgramaProjeto);
        }

        public AcoesDesenvolvidaProgramasInfo GetById(int id)
        {
            return _repository.GetQuery().SingleOrDefault(m => m.Id == id);
        }
    }
}
