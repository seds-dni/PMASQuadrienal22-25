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
   public class GestorFundoMunicipal
    {
       private static IRepository<GestorFundoMunicipalInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<GestorFundoMunicipalInfo>>();
            }
        }

       public IQueryable<GestorFundoMunicipalInfo> GetAll()
        {
            return _repository.GetQuery();

        }

       public GestorFundoMunicipalInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }

       public GestorFundoMunicipalInfo GetByPrefeitura(int idPrefeitura)
        {
            return _repository.Single(m => m.IdPrefeitura == idPrefeitura && m.IdStatus == 1);
        }

       public IQueryable<GestorFundoMunicipalInfo> GetAnterioresByPrefeitura(int idPrefeitura)
        {
            return _repository.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && m.IdStatus != 1);

        }

       public void Update(GestorFundoMunicipalInfo obj, Boolean commit)
        {
            new ValidadorGestorFundoMunicipal().Validar(obj);
            _repository.Update(obj);

            var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var propriedades = GetLabelForInfo(propriedadesEntity, obj);
            if (propriedades.Count > 0)
            {
                var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, 97, Log.CreateDescricaoDefaultUpdate(propriedades));
                if (log != null)
                    new Log().Add(log, false);
            }

            if (commit)
                ContextManager.Commit();
        }

       public void Add(GestorFundoMunicipalInfo obj, Boolean commit)
        {
            new ValidadorGestorFundoMunicipal().Validar(obj);
            if (GetByPrefeitura(obj.IdPrefeitura) != null)
                throw new Exception("Já existe cadastro do gestor do fundo municipal atual!");

            _repository.Add(obj);

            var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Add, 97, "Incluído o gestor fundo municipal atual " + obj.Nome + ".");
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

       public void Delete(GestorFundoMunicipalInfo obj, Boolean commit)
        {
            String descricao = "Excluído o gestor do fundo municipal " + obj.Nome + ".";

            _repository.Delete(obj);

            var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Remove, 97, descricao);
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Substituir(Int32 idPrefeitura, DateTime dataTerminoGestao)
        {
            var gestorMunicipalAtual = GetByPrefeitura(idPrefeitura);
            gestorMunicipalAtual.IdStatus = 3;
            gestorMunicipalAtual.TerminoGestao = dataTerminoGestao;
            _repository.Update(gestorMunicipalAtual);
            var log = Log.CreateLog(idPrefeitura, EAcao.Update, 97, "Substituído o gestor do fundo municipal atual " + gestorMunicipalAtual.Nome + ".");
            if (log != null)
                new Log().Add(log, false);

            ContextManager.Commit();
        }

        public List<String> GetLabelForInfo(List<String> propriedades, GestorFundoMunicipalInfo obj)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "DataNomeacao": labels.Add("data de nomeação"); break;
                    case "Telefone": labels.Add("telefone"); break;
                    case "Celular": labels.Add("Celular"); break;
                    case "Email": labels.Add("e-mail institucional"); break;

                }
            }
            return labels.Distinct().ToList();
        }
    }
}
