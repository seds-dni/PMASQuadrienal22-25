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
    public class UnidadePublica
    {
        private static IRepository<UnidadePublicaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<UnidadePublicaInfo>>();
            }
        }
        private static IRepository<ConsultaUnidadePublicaInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaUnidadePublicaInfo>>();
            }
        }

        private static IRepository<ConsultaLocalPublicoGeral> _repositoryConsultaPublicoGeral
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaLocalPublicoGeral>>();
            }
        }

        public IQueryable<UnidadePublicaInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public UnidadePublicaInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }



        public IQueryable<ConsultaLocalPublicoGeral> GetLocaisPublicosByUnidade(int idUnidade)
        {
            return _repositoryConsultaPublicoGeral.GetQuery().Where(m => m.IdUnidade == idUnidade);
        }

        public IQueryable<UnidadePublicaInfo> GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura);
        }

        public IQueryable<ConsultaUnidadePublicaInfo> GetConsultaByPrefeitura(int idPrefeitura)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura);
        }

        public IQueryable<ConsultaUnidadePublicaInfo> GetConsultaByPrefeituraExercicio(int idPrefeitura, int Exercicio)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && m.Exercicio == Exercicio);
        }
      
        public void Update(UnidadePublicaInfo unidadePublicaNova, Boolean commit)
        {
            if (!unidadePublicaNova.Desativado)
            {
                new ValidadorUnidadePublica().Validar(unidadePublicaNova);
            }

            if (_repository.GetQuery().Any(unidade => unidade.CNPJ == unidadePublicaNova.CNPJ 
                                        && unidade.IdPrefeitura == unidadePublicaNova.IdPrefeitura 
                                        && unidade.Id != unidadePublicaNova.Id)
                                            )
            {
                throw new Exception("Já existe uma Unidade Pública cadastrada com esse CNPJ.");
            }
            _repository.Update(unidadePublicaNova);

            //QUADRO 18
            var propriedadesEntity = _repository.GetModifiedProperties(unidadePublicaNova);
            var propriedades = GetLabelForInfo(propriedadesEntity);
            if (propriedades.Count > 0)
            {
                String descricao = string.Empty;
                if (propriedades.Contains("desativado"))
                {
                    descricao += "Desativada a Unidade Pública: "
                                + unidadePublicaNova.Id
                                + " - "
                                + unidadePublicaNova.RazaoSocial;
                }
                else
                {
                    descricao += "Unidade Pública: " 
                              + unidadePublicaNova.Id 
                              + " - " + unidadePublicaNova.RazaoSocial 
                              + System.Environment.NewLine 
                              + Log.CreateDescricaoDefaultUpdate(propriedades);
                }

                var log = Log.CreateLog(unidadePublicaNova.IdPrefeitura, EAcao.Update, 18, descricao, unidadePublicaNova.Id);
                if (log != null)
                {
                    new Log().Add(log, false);
                }
            }

            if (commit)
            {
                ContextManager.Commit();
            }
        }

        public void Add(UnidadePublicaInfo obj, Boolean commit)
        {
            new ValidadorUnidadePublica().Validar(obj);
            if (_repository.GetQuery().Any(t => t.CNPJ == obj.CNPJ && t.IdPrefeitura == obj.IdPrefeitura))
                throw new Exception("Já existe uma Unidade Pública cadastrada com esse CNPJ.");
            _repository.Add(obj);

            var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Add, 17, "Incluída a Unidade Pública " + obj.RazaoSocial + ".");
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Delete(UnidadePublicaInfo unidade, Boolean commit)
        {
            var l = new LocalExecucaoPublico();
            if (l.GetByUnidade(unidade.Id).Where(c => c.Desativado != true).Count() > 0)
                throw new Exception("Essa unidade possui locais de execução! Exclua primeiro os locais de execução para excluir a unidade.");

            String descricao = "Excluída a Unidade Pública " + unidade.Id + " - " + unidade.RazaoSocial + ".";
            _repository.Delete(unidade);

            var log = Log.CreateLog(unidade.IdPrefeitura, EAcao.Remove, 17, descricao);
            if (log != null)
                new Log().Add(log, false);
            if (commit)
                ContextManager.Commit();
        }

        public List<String> GetLabelForInfo(List<String> propriedades)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "CNPJ": labels.Add("CNPJ"); break;
                    case "RazaoSocial": labels.Add("razão social"); break;
                    case "Desativado": labels.Add("desativado"); break;
                }
            }
            return labels;
        }

    }
}
