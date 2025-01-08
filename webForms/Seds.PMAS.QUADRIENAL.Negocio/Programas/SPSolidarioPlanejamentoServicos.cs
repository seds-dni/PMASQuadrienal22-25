using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Transactions;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class SPSolidarioPlanejamentoServicos
    {
        private static IRepository<SPSolidarioPlanejamentoServicosInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<SPSolidarioPlanejamentoServicosInfo>>();
            }
        }

        public IQueryable<SPSolidarioPlanejamentoServicosInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public SPSolidarioPlanejamentoServicosInfo GetById(int id)
        {
            var centro = _repository.Single(m => m.Id == id);
            return centro;
        }

        public IQueryable<SPSolidarioPlanejamentoServicosInfo> GetByProgramaProjeto(int idProgramaProjeto)  
        {
            return _repository.GetObjectSet().Include("ProgramaProjeto").Where(m => m.IdProgramaProjeto == idProgramaProjeto);
        }

        public void Update(SPSolidarioPlanejamentoServicosInfo obj, Boolean commit)
        {
            new ValidadorPlanejamentoServicos().Validar(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Add(SPSolidarioPlanejamentoServicosInfo obj, Boolean commit)
        {
            new ValidadorPlanejamentoServicos().Validar(obj);
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(SPSolidarioPlanejamentoServicosInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
