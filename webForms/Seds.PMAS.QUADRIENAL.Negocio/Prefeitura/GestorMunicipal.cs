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
    public class GestorMunicipal
    {
        private static IRepository<GestorMunicipalInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<GestorMunicipalInfo>>();
            }
        }

        public IQueryable<GestorMunicipalInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public GestorMunicipalInfo GetById(int id)
        {
            return _repository.Single(m => m.Id == id);
        }

        public GestorMunicipalInfo GetByPrefeitura(int idPrefeitura)
        {
            return _repository.Single(m => m.IdPrefeitura == idPrefeitura && m.IdStatus == 1);
        }

        public GestorMunicipalInfo GetGestorPrestacaoContas(int idPrefeitura, int idUsuario) 
        {
            return _repository.Single(m => m.IdUsuarioGestor == idUsuario && m.IdPrefeitura == idPrefeitura && m.IdStatus == 1);
        }

        public IQueryable<GestorMunicipalInfo> GetAnterioresByPrefeitura(int idPrefeitura)
        {
            return _repository.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && m.IdStatus != 1);
        }

        public void Update(GestorMunicipalInfo obj, Boolean commit)
        {
            new ValidadorGestorMunicipal().Validar(obj);
            _repository.Update(obj);

            var propriedadesEntity = _repository.GetModifiedProperties(obj);
            var propriedades = GetLabelForInfo(propriedadesEntity,obj);
            if (propriedades.Count > 0)
            {
                var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Update, 7, Log.CreateDescricaoDefaultUpdate(propriedades));
                if (log != null)
                    new Log().Add(log, false);
            }

            if (commit)
                ContextManager.Commit();
        }

        public void Add(GestorMunicipalInfo obj, Boolean commit)
        {
            new ValidadorGestorMunicipal().Validar(obj);
            if (GetByPrefeitura(obj.IdPrefeitura) != null)
                throw new Exception("Já existe cadastro do gestor municipal atual!");

            _repository.Add(obj);

            var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Add, 7, "Incluído o gestor municipal atual " + obj.Nome + ".");
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Delete(GestorMunicipalInfo obj, Boolean commit)
        {
            String descricao = "Excluído o gestor municipal " + obj.Nome + ".";

            _repository.Delete(obj);

            var log = Log.CreateLog(obj.IdPrefeitura, EAcao.Remove, 8, descricao);
            if (log != null)
                new Log().Add(log, false);

            if (commit)
                ContextManager.Commit();
        }

        public void Substituir(Int32 idPrefeitura, DateTime dataTerminoGestao)
        {
           var gestorMunicipalAtual = GetByPrefeitura(idPrefeitura);
            gestorMunicipalAtual.IdStatus = 3;
            gestorMunicipalAtual.DataTerminoGestao = dataTerminoGestao;
            _repository.Update(gestorMunicipalAtual);
            var log = Log.CreateLog(idPrefeitura, EAcao.Update, 7, "Substituído o gestor municipal atual " + gestorMunicipalAtual.Nome + ".");
            if (log != null)
                new Log().Add(log, false);

            ContextManager.Commit(); 
        }

        public List<String> GetLabelForInfo(List<String> propriedades, GestorMunicipalInfo obj)
        {
            var labels = new List<String>();
            foreach (var p in propriedades)
            {
                switch (p)
                {
                    case "IdUsuarioGestor": labels.Add("nome"); break;
                    case "IdCargo":
                    case "OutroCargo":
                        labels.Add("cargo"); break;
                    case "IdEscolaridade": labels.Add("escolaridade"); break;
                    case "IdFormacao":
                    case "OutraFormacao":                    
                       if (obj.IdEscolaridade == 4 ) labels.Add("formação acadêmica"); break;
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
