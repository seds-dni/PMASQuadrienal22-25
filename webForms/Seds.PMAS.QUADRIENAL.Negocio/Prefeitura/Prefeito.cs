using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class Prefeito
    {
        private static IRepository<PrefeitoInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<PrefeitoInfo>>();
            }
        }

        public IQueryable<PrefeitoInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public PrefeitoInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }

        public PrefeitoInfo GetByPrefeitura(int idPrefeitura)
        {
            return _repository.Single(m => m.IdPrefeitura == idPrefeitura && m.IdStatus == 1);
        }

        public IQueryable<PrefeitoInfo> GetAnterioresByPrefeitura(int idPrefeitura)
        {
            return _repository.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && m.IdStatus != 1);

        }

        public void Update(PrefeitoInfo obj, Boolean commit)
        {
            new ValidadorPrefeito().Validar(obj);
            _repository.Update(obj);
            var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var propriedades = GetLabelForInfo(propriedadesEntity);
            if (propriedades.Count > 0)
            {
                var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, 2, Log.CreateDescricaoDefaultUpdate(propriedades));
                if (log != null)
                    new Log().Add(log, false);
            }
            if (commit)
                ContextManager.Commit();
        }

        public void Add(PrefeitoInfo obj, Boolean commit)
        {
            new ValidadorPrefeito().Validar(obj);
            if (GetByPrefeitura(obj.IdPrefeitura) != null)
                throw new Exception("Já existe cadastro do prefeito atual!");
            _repository.Add(obj);
            var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Add, 2, "Incluído o prefeito atual " + obj.Nome + ".");
            if (log != null)
                new Log().Add(log, false);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(PrefeitoInfo obj, Boolean commit)
        {
            String descricao = "Excluído o prefeito " + obj.Nome +".";
            _repository.Delete(obj);

            var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Remove, 3, descricao);
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Substituir(Int32 idPrefeitura, string dataTerminoNova = "")
        {
            var prefeitoAtual = GetByPrefeitura(idPrefeitura);

            if (!string.IsNullOrEmpty(dataTerminoNova))
            {
                prefeitoAtual.MandatoTerminio = Convert.ToDateTime(dataTerminoNova);
            }

            if (prefeitoAtual == null)
            {
                return;
            }
            prefeitoAtual.IdStatus = 3;
            _repository.Update(prefeitoAtual);
            var log = Log.CreateLog(idPrefeitura, EAcao.Update, 2, "Substituído o prefeito atual " + prefeitoAtual.Nome + ".");
            if (log != null)
                new Log().Add(log, false);
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
                    case "RG": 
                    case "RGDigito":
                        labels.Add("rg"); break;
                    case "RGDataEmissao": labels.Add("data de emissão do rg"); break;
                    case "IdUFRG": labels.Add("uf do rg"); break;
                    case "SiglaEmissor": labels.Add("sigla do órgão emissor do rg"); break;
                    case "CPF": labels.Add("cpf"); break;
                    case "MandatoInicio": labels.Add("data de início do mandato"); break;
                    case "MandatoTerminio": labels.Add("data de término do mandato"); break;                    
                    case "Email": labels.Add("e-mail institucional"); break;

                }
            }
            return labels.Distinct().ToList();
        }
    }
}
