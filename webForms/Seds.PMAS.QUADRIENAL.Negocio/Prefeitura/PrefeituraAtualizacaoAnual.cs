using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class PrefeituraAtualizacaoAnual
    {
        private static IRepository<PrefeituraAtualizacaoAnualInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PrefeituraAtualizacaoAnualInfo>>();
            }
        }

        public IQueryable<PrefeituraAtualizacaoAnualInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public PrefeituraAtualizacaoAnualInfo GetById(int id)
        {
            return _repository.GetObjectSet().Include("Situacao").Single(m => m.Id == id);
        }


        public void Update(PrefeituraAtualizacaoAnualInfo prefeituraAtualizacaoNova, Boolean commit, bool validar = true)
        {
            if (validar)
            {
                new ValidadorPrefeituraAtualizacaoAnual().Validar(prefeituraAtualizacaoNova);
            }
            PrefeituraAtualizacaoAnualInfo prefeituraAtualizacaoAnualExistente = this.GetAll()
                                                .Where(x => x.IdPrefeitura == prefeituraAtualizacaoNova.IdPrefeitura 
                                                         && x.Exercicio == prefeituraAtualizacaoNova.Exercicio).FirstOrDefault();
            if (prefeituraAtualizacaoAnualExistente != null)
            {
                prefeituraAtualizacaoAnualExistente.AtualizacaoAnual = prefeituraAtualizacaoNova.AtualizacaoAnual;
                _repository.Update(prefeituraAtualizacaoNova);

                var propriedadesEntity = _repository.GetModifiedProperties(prefeituraAtualizacaoNova);
                var propriedades = GetLabelForInfo(propriedadesEntity);
                if (propriedades.Count > 0)
                {
                    String descricao = String.Empty;

                    var log = new LogInfo();
                    var quadro = propriedadesEntity;

                    descricao = Log.CreateDescricaoDefaultUpdate(propriedades);
                    log = Log.CreateLog(prefeituraAtualizacaoNova.Id, EAcao.Update, 1, descricao);

                    if (log != null)
                        new Log().Add(log, false);
                }
            }
            else
            {
                _repository.Add(prefeituraAtualizacaoNova);

                var propriedadesEntity = _repository.GetModifiedProperties(prefeituraAtualizacaoNova);
                var propriedades = GetLabelForInfo(propriedadesEntity);
                if (propriedades.Count > 0)
                {
                    String descricao = String.Empty;

                    var log = new LogInfo();
                    var quadro = propriedadesEntity;

                    descricao = Log.CreateDescricaoDefaultUpdate(propriedades);
                    log = Log.CreateLog(prefeituraAtualizacaoNova.Id, EAcao.Add, 1, descricao);

                    if (log != null)
                        new Log().Add(log, false);
                }
            }

            if (commit)
            {
                ContextManager.Commit();
            }
        }

        public List<String> GetLabelForInfo(List<String> propriedades)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "IdNivelGestao": labels.Add("gestão"); break;
                    case "DataPublicacao": labels.Add("data da última publicação de nível de gestão no DOE"); break;
                    case "CEP": labels.Add("CEP"); break;
                    case "Logradouro": labels.Add("logradouro"); break;
                    case "Numero": labels.Add("número"); break;
                    case "Complemento": labels.Add("complemento"); break;
                    case "Cidade": labels.Add("cidade"); break;
                    case "Bairro": labels.Add("bairro"); break;
                    case "Telefone": labels.Add("telefone"); break;
                    case "Celular": labels.Add("Celular"); break;
                    case "WebSite": labels.Add("web site"); break;
                    case "Email": labels.Add("e-mail institucional"); break;
                    case "Caracterizacao": labels.Add("caracterização"); break;
                    case "CaracterizacaoAnaliseInterpretacao": labels.Add("caracterizacao Analise Interpretacao"); break;
                    case "CaracterizacaoPopulacao": labels.Add("caracterizacao populacao"); break;
                    case "CaracterizacaoRedeSocioassistencial": labels.Add("caracterizacao Rede Socioassistencial"); break;
                    case "JustificativaAcaoPlanejamento": labels.Add("justificativa de ações planejadas"); break;
                }
            }
            return labels.Distinct().ToList();
        }


    }
}
