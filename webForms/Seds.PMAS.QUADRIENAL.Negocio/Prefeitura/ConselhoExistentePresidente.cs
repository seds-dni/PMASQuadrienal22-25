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
    public class ConselhoExistentePresidente
    {
        private static IRepository<ConselhoMunicipalExistentePresidenteInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConselhoMunicipalExistentePresidenteInfo>>();
            }
        }

        public IQueryable<ConselhoMunicipalExistentePresidenteInfo> GetAll()
        {
            return _repository.GetQuery();
        }

        public ConselhoMunicipalExistentePresidenteInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }



        public ConselhoMunicipalExistentePresidenteInfo GetPresidenteConselhoByIdConselho(int idConselho)
        {
            return _repository.Single(m => m.IdConselho == idConselho && m.IdStatus == 1);
        }

        public IQueryable<ConselhoMunicipalExistentePresidenteInfo> GetPresidenteConselhoByIdConselhoCollection(int idConselho)
        {
            return _repository.GetQuery().Where(m => m.IdConselho == idConselho && m.IdStatus == 1);
        }

        public IQueryable<ConselhoMunicipalExistentePresidenteInfo> GetPresidentesByIdConselhoExistente(int idConselho) 
        {
            return _repository.GetQuery().Where(m => m.IdConselho == idConselho);
        }

        public IQueryable<ConselhoMunicipalExistentePresidenteInfo> GetPresidentesConselhoExistenteByIdConselhoPrefeitura(int idConselho, int idPrefeitura)
        {
            return _repository.GetQuery().Where(m => m.IdConselho == idConselho && m.IdPrefeitura == idPrefeitura && m.IdStatus != 1);
        }

        public void Add(ConselhoMunicipalExistentePresidenteInfo c, Boolean commit)
        {
            new ValidadorConselhoExistente().ValidarPresidente(c);
            c.IdStatus = 1;

            _repository.Add(c);

            var conselho = new ConselhoExistente().GetById(c.IdConselho);

            var log = Log.CreateLog(c.IdPrefeitura, EAcao.Add, 10, "Incluído o presidente - " + (!String.IsNullOrEmpty(c.Nome) ? " (" + c.Nome + ")" : "") + "do conselho" + (conselho.Nome) + ".");
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }


        public void Delete(ConselhoMunicipalExistentePresidenteInfo c, Boolean commit)
        {
            var presidenteconselho = new ConselhoExistentePresidente().GetById(c.Id);
            var log = Log.CreateLog(c.IdPrefeitura, EAcao.Add, 10, "Excluído o presidente do conselho - " + presidenteconselho.Nome + (!String.IsNullOrEmpty(c.Nome) ? " (" + c.Nome + ")" : "") + ".");

            _repository.Delete(c);

            if (log != null)
                new Log().Add(log, false);
            if (commit)
                ContextManager.Commit();
        }

        public void Update(ConselhoMunicipalExistentePresidenteInfo c, Boolean commit)
        {
            new ValidadorConselhoExistente().ValidarPresidente(c);
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

        public void Substituir(ConselhoMunicipalExistentePresidenteInfo c, Boolean commit)
        {
            new ValidadorConselhoExistente().ValidarPresidente(c);
            c.IdStatus = 2;
            _repository.Update(c);
            //var propriedadesEntity = _repository.GetModifiedProperties(c);
            //var propriedades = GetLabelForInfo(propriedadesEntity);
            //if (propriedades.Count > 0)
            //{
            var tipoConselho = new ConselhoExistente().GetById(c.IdConselho);
            var descricao = "Substituído o presidente atual" + c.Nome + " do Conselho - " + tipoConselho.Nome + "."; // + System.Environment.NewLine + Log.CreateDescricaoDefaultUpdate(propriedades);
            var log = Log.CreateLog(c.IdPrefeitura, EAcao.Update, 10, descricao, c.Id);
            if (log != null)
                new Log().Add(log, false);
            //}

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
                    case "NomePresidente": labels.Add("nome do presidente"); break;
                    case "Telefone": labels.Add("telefone"); break;
                    case "Celular": labels.Add("Celular"); break;
                    case "Email": labels.Add("e-mail"); break;
                    case "RG": labels.Add("RG"); break;
                    case "CPF": labels.Add("CPF"); break;
                }
            }
            return labels.Distinct().ToList();
        }

    }
}
