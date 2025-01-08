using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class PlanoMunicipalHistorico
    {
        private static IRepository<PlanoMunicipalHistoricoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PlanoMunicipalHistoricoInfo>>();
            }
        }

        private static IRepository<ConsultaPlanoMunicipalHistoricoInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaPlanoMunicipalHistoricoInfo>>();
            }
        }

        public IQueryable<PlanoMunicipalHistoricoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public PlanoMunicipalHistoricoInfo GetById(int id)
        {
            return _repository.GetObjectSet().Include("PlanosMunicipaisHistoricoConsolidados").Single(m => m.Id == id);
        }

        public PlanoMunicipalHistoricoInfo GetByIdPrefeitura(int idPrefeitura)
        {
            return _repository.GetObjectSet().Include("PlanosMunicipaisHistoricoConsolidados").Where(m => m.IdPrefeitura == idPrefeitura).OrderByDescending(t => t.Data).First();
        }

        public PlanoMunicipalHistoricoInfo GetByIdPrefeituraSituacao(int idPrefeitura, int idSituacao)
        {
            return _repository.GetObjectSet().Include("PlanosMunicipaisHistoricoConsolidados").Where(m => m.IdPrefeitura == idPrefeitura && m.IdSituacao == idSituacao).OrderByDescending(t => t.Data).First();
        }

        public IQueryable<PlanoMunicipalHistoricoInfo> GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetObjectSet().Include("PlanosMunicipaisHistoricoConsolidados").Where(m => m.IdPrefeitura == idPrefeitura).OrderByDescending(t => t.Data);
        }

        public ConsultaPlanoMunicipalHistoricoInfo GetConsultaById(int id)
        {
            return _repositoryConsulta.Single(t=> t.Id == id);
        }

        public IQueryable<ConsultaPlanoMunicipalHistoricoInfo> GetConsultaByPrefeitura(int idPrefeitura)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura).OrderByDescending(t => t.Data);
        }

        public void Add(PlanoMunicipalHistoricoInfo obj, Boolean commit)
        {           
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }


        public void Update(PlanoMunicipalHistoricoInfo obj, Boolean commit)
        {
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }  
    }
}
