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
    public class IdentificacaoTerritorio
    {
        private static IRepository<IdentificacaoTerritorioInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<IdentificacaoTerritorioInfo>>();
            }
        }

        public IQueryable<IdentificacaoTerritorioInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public IdentificacaoTerritorioInfo GetById(int id)
        {
            var centro = _repository.Single(m => m.Id == id);
            return centro;
        }

        public IQueryable<IdentificacaoTerritorioInfo> GetByTransferenciaRenda(int idProgramaProjeto)
        {
            return _repository.GetObjectSet().Where(m => m.IdProgramaProjeto == idProgramaProjeto);
        }

        public IQueryable<IdentificacaoTerritorioInfo> GetByProgramaProjeto(int idProgramaProjeto)  
        {
            return _repository.GetObjectSet().Where(m => m.IdProgramaProjeto == idProgramaProjeto);
        }

        public void Update(IdentificacaoTerritorioInfo obj, Boolean commit)
        {
            new ValidadorIdentificacaoTerritorio().Validar(obj);
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Add(IdentificacaoTerritorioInfo obj, Boolean commit)
        {
            new ValidadorIdentificacaoTerritorio().Validar(obj);
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(IdentificacaoTerritorioInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }

        public List<String> GetLabel(List<String> propriedades)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "AderiuFamiliaPaulista":
                        labels.Add("aderiu ao Programa Família Paulista"); break;
                    case "PossuiInterlocutorMunicipal": break;
                    case "DataAdesaoPrograma":
                        labels.Add("Data de assinatura do termo de Adesão do Programa Família Paulista");
                        break;
                    case "ValorFEAS":
                        labels.Add("Valor FEAS 1° parcela"); break;
                    case "MesRepasse":
                        labels.Add("Mês de Repasse da 1° Parcela");
                        break;
                    case "AnoRepasse":
                        labels.Add("Ano de Repasse da 1° Parcela");
                        break;
                    case "ValorFMAS":
                        labels.Add("valor FMAS"); break;
                    case "ValorOrcamentoMunicipal":
                        labels.Add("valor orçamento municipal"); break;
                    case "ValorFundoMunicipal":
                        labels.Add("valor outros fundos municipais"); break;
                    case "ValorOrcamentoEstadual":
                        labels.Add("valor orçamento estadual"); break;
                    case "ValorFundoEstadual":
                        labels.Add("valor outros fundos estaduais"); break;
                    case "ValorFNAS":
                        labels.Add("valor FNAS"); break;
                    case "ValorOrcamentoFederal":
                        labels.Add("valor orçamento federal"); break;
                    case "ValorFundoFederal":
                        labels.Add("valor outros fundos nacionais"); break;
                    case "ValorIGDPBF":
                        labels.Add("valor IGD-PBF"); break;
                    case "ValorIGDSUAS":
                        labels.Add("valor IGD-SUAS"); break;
                }
            }
            return labels.Distinct().ToList();
        }
    }
}
