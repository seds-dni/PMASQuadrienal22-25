using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Entidades;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class ConselhoExistente
    {
        private static IRepository<ConselhoExistenteInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConselhoExistenteInfo>>();
            }
        }

        private static IRepository<IdentificacaoConselhoExistenteInfo> _repositoryView
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<IdentificacaoConselhoExistenteInfo>>();
            }
        }

        public IQueryable<ConselhoExistenteInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public ConselhoExistenteInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }

        public IQueryable<IdentificacaoConselhoExistenteInfo> GetIdentificacaoByPrefeitura(int idPrefeitura)
        {
            return _repositoryView.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura);
        }

        public IQueryable<ConselhoExistenteInfo> GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura);

        }

        public void Update(ConselhoExistenteInfo c, Boolean commit)
        {
            new ValidadorConselhoExistente().Validar(c);
            _repository.Update(c);
            var propriedadesEntity = _repository.GetModifiedProperties(c);
            var propriedades = GetLabelForInfo(propriedadesEntity);
            if (propriedades.Count > 0)
            {
                var tipoConselho = new Conselhos().GetById(c.IdConselho);
                var descricao = "Conselho - " + tipoConselho.Nome + (!String.IsNullOrEmpty(c.Nome) ? " (" + c.Nome + ")" : "") + "." + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
                var log = Log.CreateLog(c.IdPrefeitura, EAcao.Update, 10, descricao, c.Id);
                if (log != null)
                    new Log().Add(log, false);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void Add(ConselhoExistenteInfo c, Boolean commit)
        {            
            new ValidadorConselhoExistente().Validar(c);
            //PODERÁ CADASTRAR MAIS DE UM CONSELHO TUTELAR TAMBÉM
            if (c.IdConselho != 9 && c.IdConselho != 3 && GetAll().Where(ce => ce.IdConselho == c.IdConselho && c.IdPrefeitura == ce.IdPrefeitura).Count() > 0)
                throw new Exception("Já foi adicionado esse Conselho.");
            else if ((c.IdConselho == 9 || c.IdConselho == 3) && GetAll().Where(ce => ce.IdConselho == c.IdConselho && c.IdPrefeitura == ce.IdPrefeitura && c.Nome == ce.Nome).Count() > 0)
                throw new Exception("Já foi adicionado esse Conselho.");

            _repository.Add(c);

            var tipoConselho = new Conselhos().GetById(c.IdConselho);
            var log = Log.CreateLog(c.IdPrefeitura, EAcao.Add, 10, "Incluído o Conselho - " + tipoConselho.Nome + (!String.IsNullOrEmpty(c.Nome) ? " (" + c.Nome + ")" : "") + ".");
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Delete(ConselhoExistenteInfo c, Boolean commit)
        {
           
            var tipoConselho = new Conselhos().GetById(c.IdConselho);

            var log = Log.CreateLog(c.IdPrefeitura, EAcao.Add, 10, "Excluído o Conselho - " + tipoConselho.Nome + (!String.IsNullOrEmpty(c.Nome) ? " (" + c.Nome + ")" : "") + ".");

            _repository.Delete(c);

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
                    case "Nome": labels.Add("nome"); break;
                    case "NomePresidente": labels.Add("nome do presidente"); break;
                    case "Lei": labels.Add("número da lei de criação"); break;
                    case "DataCriacao": labels.Add("data de publicação da lei de criação"); break;                    
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
