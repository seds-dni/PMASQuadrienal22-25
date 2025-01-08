using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Persistencia;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class DradsPlanoMunicipalRecursos
    {
        private static IRepository<DradsPlanoMunicipalRecursosInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<DradsPlanoMunicipalRecursosInfo>>();
            }
        }

        private static IRepository<DradsPlanoMunicipalRecursosInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<DradsPlanoMunicipalRecursosInfo>>();
            }
        }

        public IQueryable<DradsPlanoMunicipalRecursosInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public DradsPlanoMunicipalRecursosInfo GetById(int id)
        {
            return _repository.GetObjectSet()
                .Include("PlanosMunicipaisHistoricoConsolidados")
                .Single(m => m.Id == id);
        }

        public DradsPlanoMunicipalRecursosInfo GetByIdPrefeitura(int idPrefeitura)
        {
            return _repository.GetObjectSet()
                .Where(m => m.IdPrefeitura == idPrefeitura)
                .First();
        }

        public DradsPlanoMunicipalRecursosInfo GetByIdPrefeituraSituacao(int idPrefeitura, int idSituacao)
        {
            return _repository.GetObjectSet()
                .Where(m => m.IdPrefeitura == idPrefeitura).First();
        }

        public DradsPlanoMunicipalRecursosInfo GetResumoCofinanciamentoDradsBy(int idPrefeitura, int exercicio)
        {
            var dradsCofinanciamentosPMAS = (ContextManager.GetContext() as PMASContext).GetResumoCofinanciamentoDradsBy(idPrefeitura, exercicio);
            return dradsCofinanciamentosPMAS;
        }

        public DradsPlanoMunicipalRecursosReprogramadoInfo GetResumoCofinanciamentoReprogramadoDradsBy(int idPrefeitura, int exercicio)
        {
            var dradsCofinanciamentosReprogramadoPMAS = (ContextManager.GetContext() as PMASContext).GetResumoCofinanciamentoReprogramadoDradsBy(idPrefeitura, exercicio);
            return dradsCofinanciamentosReprogramadoPMAS;
        }

        public DradsPlanoMunicipalDemandasParlamentaresInfo GetResumoCofinanciamentoDemandasDradsBy(int idPrefeitura, int exercicio)
        {
            var dradsCofinanciamentosDemandasPMAS = (ContextManager.GetContext() as PMASContext).GetResumoCofinanciamentoDemandasDradsBy(idPrefeitura, exercicio);
            return dradsCofinanciamentosDemandasPMAS;
        }

        public DradsPlanoMunicipalReprogramacaoDemandasParlamentaresInfo GetResumoCofinanciamentoReprogramacaoDemandasDradsBy(int idPrefeitura, int exercicio)
        {
            var dradsCofinanciamentosDemandasPMAS = (ContextManager.GetContext() as PMASContext).GetResumoCofinanciamentoReprogramadoDemandasDradsBy(idPrefeitura, exercicio);
            return dradsCofinanciamentosDemandasPMAS;
        }

        public DradsPlanoMunicipalBeneficioProgramaRecursosInfo GetResumoCofinanciamentoBeneficioProgramaDradsBy(int idPrefeitura, int exercicio)
        {
            var dradsCofinanciamentosBeneficioProgramaPMAS = (ContextManager.GetContext() as PMASContext).GetResumoCofinanciamentoBeneficioProgramaDradsBy(idPrefeitura, exercicio);
            return dradsCofinanciamentosBeneficioProgramaPMAS;
        }

        

        public void Add(DradsPlanoMunicipalRecursosInfo obj, Boolean commit)
        {           
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }


        public void Update(DradsPlanoMunicipalRecursosInfo obj, Boolean commit)
        {
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }  
    }
}
