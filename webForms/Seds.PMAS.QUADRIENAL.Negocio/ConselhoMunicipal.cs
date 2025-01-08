using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class ConselhoMunicipal
    {
        private static IRepository<ConselhoMunicipalInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConselhoMunicipalInfo>>();
            }
        }

        public IQueryable<ConselhoMunicipalInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ConselhoMunicipalInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }

        public ConselhoMunicipalInfo GetByPrefeitura(int idPrefeitura)
        {
            return _repository.Single(m => m.IdPrefeitura == idPrefeitura);
        }

        public void Add(ConselhoMunicipalInfo obj, Boolean commit, Boolean ignorarValidacao = false)
        {
            if (!ignorarValidacao)
            {
                new ValidadorConselhoMunicipal().Validar(obj);
            }
            if (GetByPrefeitura(obj.IdPrefeitura) != null)
                throw new Exception("Já existe cadastrado um CMAS para a Prefeitura!");
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Update(ConselhoMunicipalInfo obj, Boolean commit, Boolean ignorarValidacao = false)
        {
            if (!ignorarValidacao)
            {
                new ValidadorConselhoMunicipal().Validar(obj);
            }
            _repository.Update(obj);

            var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var propriedades = GetLabelForInfo(propriedadesEntity, obj);

            if (propriedades.Count > 0)
            {
                String descricao = "Conselho Municipal: " + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, 72, descricao);
                if (log != null)
                    new Log().Add(log, false);
            }
            if (commit)
                ContextManager.Commit();
        }

        public List<String> GetLabelForInfo(List<String> propriedades,ConselhoMunicipalInfo obj)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "NumeroLei": labels.Add("número da lei"); break;
                    case "DataLei": labels.Add("data de publicação da lei"); break;
                    case "AlteracaoNaLei": labels.Add("houve alteração na lei"); break;
                    case "NumeroLeiAlterada": if(obj.AlteracaoNaLei.HasValue && obj.AlteracaoNaLei.Value) labels.Add("número da lei alterada"); break;
                    case "DataLeiAlterada": if (obj.AlteracaoNaLei.HasValue && obj.AlteracaoNaLei.Value) labels.Add("data de publicação da lei alterada"); break;
                    case "IdUsuarioPresidente": labels.Add("nome do presidente"); break;
                    case "NumeroDecreto": labels.Add("número do decreto de nomeação do presidente"); break;
                    case "DataDecreto": labels.Add("data do decreto de nomeação do presidente"); break;
                    case "DataMandatoInicio": labels.Add("data de ínicio do mandato do presidente"); break;
                    case "DataMandatoTerminio": labels.Add("data de término do mandato do presidente"); break;
                    case "NumeroRepresentanteGovernamentais": labels.Add("número de conselheiros titulares que são representantes governamentais"); break;
                    case "NumeroRepresentanteSociedadeCivil": labels.Add("número de conselheiros titulares que são representantes da sociedade civil"); break;
                    case "PossuiSecretariaExecutivaEstruturada": labels.Add("a secretaria executiva do CMAS já foi estruturada"); break;
                    case "TotalFuncionariosTecnicoSecretariaExecutiva": if (obj.PossuiSecretariaExecutivaEstruturada.HasValue && obj.PossuiSecretariaExecutivaEstruturada.Value) labels.Add("quantidade de trabalhadores fazem parte do corpo técnico da Secretaria Executiva do CMAS"); break;
                    case "TotalFuncionariosAdministrativoSecretariaExecutiva": if (obj.PossuiSecretariaExecutivaEstruturada.HasValue && obj.PossuiSecretariaExecutivaEstruturada.Value) labels.Add("quantidade de trabalhadores fazem parte do corpo administrativo da Secretaria Executiva do CMAS"); break;
                    case "CEP": labels.Add("CEP"); break;
                    case "Logradouro": labels.Add("logradouro"); break;
                    case "Numero": labels.Add("número"); break;
                    case "Complemento": labels.Add("complemento"); break;
                    case "Cidade": labels.Add("cidade"); break;
                    case "Bairro": labels.Add("bairro"); break;
                    case "Telefone": labels.Add("telefone"); break;
                    case "Celular": labels.Add("Celular"); break;                    
                    case "Email": labels.Add("e-mail"); break;                    
                }
            }
            return labels.Distinct().ToList();
        }
    }
}
