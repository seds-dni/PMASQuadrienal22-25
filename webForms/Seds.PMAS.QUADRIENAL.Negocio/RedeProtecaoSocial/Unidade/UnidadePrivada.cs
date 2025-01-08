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
    public class UnidadePrivada
    {
        private static IRepository<UnidadePrivadaInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<UnidadePrivadaInfo>>();
            }
        }
        private static IRepository<ConsultaUnidadePrivadaInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaUnidadePrivadaInfo>>();
            }
        }

        private static IRepository<ConsultaUnidadePrivadaDesativadaInfo> _repositoryConsultaDesativadas
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaUnidadePrivadaDesativadaInfo>>();
            }
        }


        public IQueryable<UnidadePrivadaInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public UnidadePrivadaInfo GetById(int id)
        {
            return _repository.GetObjectSet().Include("FormasAtuacoes").Include("CaracterizacaoAtividades").Include("UnidadesTipoAtendimentos").Include("PublicoAlvos").Single(m => m.Id == id);
        }

        public IQueryable<UnidadePrivadaInfo> GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetObjectSet().Include("FormasAtuacoes").Include("CaracterizacaoAtividades").Include("UnidadesTipoAtendimentos").Include("PublicoAlvos").Where(m => m.IdPrefeitura == idPrefeitura);
            //return _repository.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura);
        }


        public IQueryable<ConsultaUnidadePrivadaInfo> GetConsultaByPrefeitura(int idPrefeitura)
        {
            return _repositoryConsulta.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura);
        }

        public IQueryable<ConsultaUnidadePrivadaDesativadaInfo> GetConsultaDesativadasByPrefeitura(int idPrefeitura)
        {
            return _repositoryConsultaDesativadas.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura);
        }

        public void Update(UnidadePrivadaInfo unidade, Boolean commit)
        {
            if (!unidade.Desativado)
            {
                new ValidadorUnidadePrivada().Validar(unidade);
            }
            if (_repository.GetQuery().Any(t => t.CNPJ == unidade.CNPJ && t.IdPrefeitura == unidade.IdPrefeitura && t.Id != unidade.Id))
                throw new Exception("Já existe uma Unidade Privada cadastrada com esse CNPJ.");

            _repository.Update(unidade);

            //QUADRO 39
            var propriedadesEntity = _repository.GetModifiedProperties(unidade);
            var propriedades = GetLabelForInfo(propriedadesEntity);

            var original = GetById(unidade.Id);
            var hasChangeCaracterizacaoAtividades = _repository.UpdateNN<CaracterizacaoAtividadesInfo>(original, unidade.CaracterizacaoAtividades, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.CaracterizacaoAtividades);  

            //var idsPublicosAlvos = obj.PublicoAlvos.Select(s => s.Id).ToList();
            //obj.PublicoAlvos = new PublicoAlvo().GetAll().Where(s => idsPublicosAlvos.Contains(s.Id)).ToList();

            var hasChangeUnidadeTipoAtendimento = _repository.UpdateNN<UnidadeTipoAtendimentoInfo>(original, unidade.UnidadesTipoAtendimentos, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.UnidadesTipoAtendimentos);//Welington p.

            var hasChangeFormaAtendimentos = _repository.UpdateNN<FormaAtuacaoInfo>(original, unidade.FormasAtuacoes, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.FormasAtuacoes);

            var hasChangePublicoAlvo = _repository.UpdateNN<PublicoAlvoInfo>(original, unidade.PublicoAlvos, (a, lst) => lst.Any(t => t.Id == a.Id), p => p.PublicoAlvos);

            //var lstDeleted = new List<PublicoAlvoInfo>();
            //var ppp = new PublicoAlvo();
            //var lstPublicosOriginal = GetById(obj.Id);
            //obj.PublicoAlvos = obj.PublicoAlvos ?? new List<PublicoAlvoInfo>();
            //var hasChangeParcerias = false;

            //foreach (var p in lstPublicosOriginal.PublicoAlvos)
            //    if (!obj.PublicoAlvos.Any(t => t.Id == p.Id))
            //    {
            //        hasChangeParcerias = true;
            //        lstDeleted.Add(p);
            //    }

            //foreach (var p in lstDeleted)
            //    ppp.Delete(p, false);

            //foreach (var p in obj.PublicoAlvos)
            //{
            //    p.FormaAtuacao = null;
            //    p. = null;
            //    p.IdProgramaProjeto = projeto.Id;
            //    if (p.Id == 0)
            //    {
            //        ppp.Add(p, false);
            //        hasChangeParcerias = true;
            //    }
            //    else
            //        ppp.Update(p, false);
            //}


            if (hasChangeFormaAtendimentos)
                propriedades.Add("formas de atendimento da unidade");

            if (hasChangeCaracterizacaoAtividades)
                propriedades.Add("caracterização de atividades");

            if (hasChangeUnidadeTipoAtendimento)
                propriedades.Add("tipo de atendimento da unidade");

            if (propriedades.Count > 0)
            {
                String descricao = string.Empty;
                var acao = EAcao.Update;
                if(propriedades.Contains("Desativado")){
                  descricao =  "Desativada a organização da rede indireta: " + unidade.Id + " - " + unidade.RazaoSocial;
                  acao = EAcao.Deactivate;
                }
                else
                   descricao =  "Unidade Privada: " + unidade.Id + " - " + unidade.RazaoSocial + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                //   var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, 39, descricao, obj.Id);
                var log = Log.CreateLog(unidade.IdPrefeitura, acao, 37, descricao, unidade.Id);
                if (log != null)
                    new Log().Add(log, false);

            }

            if (commit)
                ContextManager.Commit();
        }

        public void Add(UnidadePrivadaInfo obj, Boolean commit)
        {
            new ValidadorUnidadePrivada().Validar(obj);
            if (_repository.GetQuery().Any(t => t.CNPJ == obj.CNPJ && t.IdPrefeitura == obj.IdPrefeitura))
                throw new Exception("Já existe uma Unidade Privada cadastrada com esse CNPJ.");

            var idsFormasAtuacoes = obj.FormasAtuacoes.Select(s => s.Id).ToList();
            obj.FormasAtuacoes = new FormaAtuacao().GetAll().Where(s => idsFormasAtuacoes.Contains(s.Id)).ToList();


            var idsAtividades = obj.CaracterizacaoAtividades.Select(s => s.Id).ToList();
            obj.CaracterizacaoAtividades = new CaracterizacaoAtividades().GetAll().Where(s => idsAtividades.Contains(s.Id)).ToList();  


            //var idsTipoAtendimento = obj.UnidadesTipoAtendimentos.Select(s => s.Id).ToList();
            //obj.UnidadesTipoAtendimentos = new UnidadeTipoAtendimento().GetAll().Where(s => idsTipoAtendimento.Contains(s.Id)).ToList(); //Welington P.

            var idsPublicosAlvos = obj.PublicoAlvos.Select(s => s.Id).ToList();
            obj.PublicoAlvos = new PublicoAlvo().GetAll().Where(s => idsPublicosAlvos.Contains(s.Id)).ToList();

            _repository.Add(obj);

            var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Add, 38, "Incluída a Unidade Privada " + obj.RazaoSocial + ".");
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Delete(UnidadePrivadaInfo unidade, Boolean commit)
        {
            var l = new LocalExecucaoPrivado();
            var u = new PrefeituraBeneficioEventual();
            //u.UnidadesExecutoras = new PrefeituraBeneficioEventual().GetAll().Where(t => t.UnidadesExecutoras.Contains(unidade.Id)).ToList();
            if (l.GetByUnidade(unidade.Id).Count() > 0)
            {
                throw new Exception("Essa unidade possui locais de execução! Exclua primeiro os locais de execução para excluir a unidade.");
            }
            else if (u.GetByUnidadePrivada(unidade).Count() > 0)
            {
                throw new Exception("Essa unidade privada está vinculada à algum programa e/ou benefício!");
            }
            String descricao = "Excluída a Unidade Privada " + unidade.Id + " - " + unidade.RazaoSocial + ".";
            _repository.Delete(unidade);

            var log = Log.CreateLog(unidade.IdPrefeitura, EAcao.Remove, 38, descricao);
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
                    case "IdSituacaoInscricao": labels.Add("Situação da Inscrição"); break;
                    case "DataPublicacao": labels.Add("Data de Publicação da Inscrição"); break;
                    case "InscricaoCMAS": labels.Add("Número de Inscrição no CMAS"); break;
                    case "IdSituacaoAtualInscricao": labels.Add("Situação Atual da Inscrição"); break;
                    case "Desativado": labels.Add("Desativado"); break;
                }
            }
            return labels;
        }

    }
}
