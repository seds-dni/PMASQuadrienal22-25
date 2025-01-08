using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
   public class CaracterizacaoUsuario
    {
       private static IRepository<CaracterizacaoUsuariosInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<CaracterizacaoUsuariosInfo>>();
            }
        }

       public IQueryable<CaracterizacaoUsuariosInfo> GetAll()
        {
            return _repository.GetQuery();

        }

       public IQueryable<CaracterizacaoUsuariosInfo> GetByIdProgramaProjeto(int idProgramaProjeto)
       {
           return _repository.GetQuery().Where(m => m.IdProgramaProjeto == idProgramaProjeto);

       }
       public CaracterizacaoUsuariosInfo GetById(int id)
        {
            return _repository.GetQuery().SingleOrDefault(m => m.Id == id);
        }

      
    }
}
