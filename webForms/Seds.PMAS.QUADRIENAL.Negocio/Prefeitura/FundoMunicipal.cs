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
    public class FundoMunicipal
    {

        public List<int> Exercicios = new List<int> { 2022, 2023, 2024, 2025 };
        private static IRepository<FundoMunicipalInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<FundoMunicipalInfo>>();
            }
        }

        public IQueryable<FundoMunicipalInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public FundoMunicipalInfo GetById(int id)
        {
            return GetByPrefeitura(_repository.GetQuery().Where(t => t.Id == id).Select(t => t.IdPrefeitura).First());
        }

        public FundoMunicipalInfo GetByPrefeitura(int idPrefeitura)
        {
            var fmas = _repository.GetObjectSet().Include("FundosMunicipaisValoresInfo").Single(m => m.IdPrefeitura == idPrefeitura);
            if (fmas == null)
            {
                return fmas;
            }
            var CNPJ = new Prefeitura().GetAll().Where(t => t.Id == idPrefeitura).Select(t => t.CNPJ).FirstOrDefault();

            fmas.Filial = fmas.CNPJ.Substring(0, 8) == CNPJ.Substring(0, 8);

            return fmas;
        }

        #region FMAS

        public void Add(FundoMunicipalInfo fundoMunicipal, Boolean commit)
        {
            if (GetByPrefeitura(fundoMunicipal.IdPrefeitura) != null)
            {
                throw new Exception("Já existe cadastrado um FMAS para a Prefeitura!");
            }
            _repository.Add(fundoMunicipal);
            if (commit)
            {
                ContextManager.Commit();
            }
        }

        public void Update(FundoMunicipalInfo obj, Boolean commit)
        {
            new ValidadorFonteRecursoFMAS().ValidarFMAS(obj);


            _repository.Update(obj);
            var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var log = new LogInfo();
            var propriedades = new List<string>();
            if (obj.Bloco == 1)
            {
                propriedades = GetLabelForInfo(propriedadesEntity, obj);
                log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, 94, Log.CreateDescricaoDefaultUpdate(propriedades));
            }
            else
            {
                propriedades = GetLabelForInfoIV(propriedadesEntity, obj);
                log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, 9, Log.CreateDescricaoDefaultUpdate(propriedades));
            }

            if (propriedades.Count > 0)
            {
                if (log != null)
                    new Log().Add(log, false);
            }

            if (commit)
                ContextManager.Commit();
        } 
        #endregion

        #region FMAS [Fontes]
        public void AddFontesRecursosFMAS(FundoMunicipalInfo fundoMunicipal, List<PrevisaoOrcamentariaInfo> previsoesOrcamentarias, int exercicio, Boolean commit)
        {
            if (exercicio == Exercicios[0])
            {
                new ValidadorFonteRecursoFMAS().ValidarFonteRecursosFMAS(fundoMunicipal, previsoesOrcamentarias, exercicio);
            }

            if (exercicio == Exercicios[1])
            {
                new ValidadorFonteRecursoFMAS().ValidarFonteRecursosFMAS(fundoMunicipal, previsoesOrcamentarias, exercicio);
            }

            if (GetByPrefeitura(fundoMunicipal.IdPrefeitura) != null)
            {
                throw new Exception("Já existe cadastrado um FMAS para a Prefeitura!");
            }
            _repository.Add(fundoMunicipal);
            if (commit)
            {
                ContextManager.Commit();
            }
        }

        public void UpdateFontesRecursosFMAS(FundoMunicipalInfo fundoMunicipal, List<PrevisaoOrcamentariaInfo> previsoesOrcamentarias, int exercicio, Boolean commit)
        {
            new ValidadorFonteRecursoFMAS().ValidarFonteRecursosFMAS(fundoMunicipal, previsoesOrcamentarias, exercicio);
            //new ValidadorFundoMunicipal().ValidarFMAS(obj);


            _repository.Update(fundoMunicipal);
            var propriedadesEntity = _repository.GetModifiedProperties(fundoMunicipal);
            var log = new LogInfo();
            var propriedades = new List<string>();
            if (fundoMunicipal.Bloco == 1)
            {
                propriedades = GetLabelForInfo(propriedadesEntity, fundoMunicipal);
                log = Log.CreateLog(fundoMunicipal.IdPrefeitura, EAcao.Update, 94, Log.CreateDescricaoDefaultUpdate(propriedades));
            }
            else
            {
                propriedades = GetLabelForInfoIV(propriedadesEntity, fundoMunicipal);
                log = Log.CreateLog(fundoMunicipal.IdPrefeitura, EAcao.Update, 9, Log.CreateDescricaoDefaultUpdate(propriedades));
            }

            if (propriedades.Count > 0)
            {
                if (log != null)
                    new Log().Add(log, false);
            }

            if (commit)
                ContextManager.Commit();
        } 
        #endregion

        #region Utils
        public List<String> GetLabelForInfo(List<String> propriedades, FundoMunicipalInfo obj)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "CNPJ": labels.Add("CNPJ"); break;
                    case "NomeGestor": labels.Add("nome do gestor"); break;
                    case "LeiAlterada": labels.Add("alteração na Lei de criação"); break;
                    case "DataLeiAlterada": labels.Add("Data de alteração da lei de criação"); break;
                    case "Lei": labels.Add("número da lei"); break;
                    case "DataCriacao": labels.Add("data de publicação da lei"); break;
                    case "Regulamenta": labels.Add("o FMAS já está legalmente regulamentado"); break;
                    case "NumeroDecreto": if (obj.Regulamenta) labels.Add("número do decreto de regulamentação"); break;
                    case "DataDecreto": if (obj.Regulamenta) labels.Add("data de publicação do decreto de regulamentação"); break;
                    case "Orcamentaria": labels.Add("o FMAS constitui-se como Unidade Orçamentária"); break;
                    case "AlteracaoLei": labels.Add("alteração na Lei de criação"); break;
                }
            }
            return labels.Distinct().ToList();
        }
        public List<string> GetLabelForInfoIV(List<string> propriedades, FundoMunicipalInfo obj)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "ValorFMAS": labels.Add("recursos municipais"); break;
                    case "ValorCusteio": labels.Add("recursos municipais destinado apenas para custeio dos serviços"); break;
                    case "ValorFEAS": labels.Add("recursos transferidos do FEAS"); break;
                    case "ValorFNAS": labels.Add("recursos transferidos do FNAS"); break;
                }
            }
            return labels.Distinct().ToList();
        } 
        #endregion
    }
}
