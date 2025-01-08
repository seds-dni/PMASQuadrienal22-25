using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades;
using System.Data.Objects;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Transactions;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.Seguranca.Token;
using System.Threading;
using Microsoft.IdentityModel.Claims;

namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class Log
    {
        private static IRepository<LogInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<LogInfo>>();
            }
        }

        private static IRepository<ConsultaLogInfo> _repositoryConsulta
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaLogInfo>>();
            }
        }

        public IQueryable<LogInfo> GetAll()
        {
            return _repository.GetQuery();

        }

        public LogInfo GetById(int id)
        {
            var p = _repository.Single(m => m.Id == id);
            return p;
        }

        public IQueryable<LogInfo> GetByPrefeitura(int idPrefeitura)
        {
            return _repository.GetObjectSet().Where(m => m.IdPrefeitura == idPrefeitura);
        }

        public IQueryable<LogInfo> GetByQuadro(int idPrefeitura, Int32 idQuadro)
        {
            var revisao = new Prefeitura().GetRevisaoByPrefeitura(idPrefeitura);
            var quadros = new Quadro().GetByQuadroPai(idQuadro).Select(t => t.Id).ToList();
            if (idQuadro == 16 || idQuadro == 36 || idQuadro == 20)
            {
                var childrenQuadros = new List<Int32>();
                foreach (var n in quadros)
                    childrenQuadros.AddRange(new Quadro().GetByQuadroPai(n).Select(t => t.Id).ToList());
                quadros.AddRange(childrenQuadros);
            }

            quadros.Add(idQuadro);
            return _repository.GetObjectSet().Where(m => m.IdPrefeitura == idPrefeitura && m.Revisao == revisao && quadros.Contains(m.IdQuadro));
        }

        public IQueryable<LogInfo> GetByQuadroEItem(int idPrefeitura, Int32 idQuadro, Int32 idForeignkey)
        {
            var revisao = new Prefeitura().GetRevisaoByPrefeitura(idPrefeitura);
            var quadros = new Quadro().GetByQuadroPai(idQuadro).Select(t => t.Id).ToList();
            if (idQuadro == 17 || idQuadro == 37)
            {
                return _repository.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && m.Revisao == revisao && ((m.IdForeignKey == idForeignkey && m.IdQuadro == idQuadro) || (m.IdForeignKeyPai == idForeignkey && quadros.Contains(m.IdQuadro))));
            }

            quadros.Add(idQuadro);
            return _repository.GetObjectSet().Where(m => m.IdPrefeitura == idPrefeitura && m.Revisao == revisao && m.IdForeignKey == idForeignkey && quadros.Contains(m.IdQuadro));
        }

        public IQueryable<ConsultaLogInfo> GetConsultaByQuadro(int idPrefeitura, Int32 idQuadro)
        {
            var revisao = new Prefeitura().GetRevisaoByPrefeitura(idPrefeitura);
            var quadros = new Quadro().GetByQuadroPai(idQuadro).Select(t => t.Id).ToList();
            if (idQuadro == 16 || idQuadro == 36)
            {
                var childrenQuadros = new List<Int32>();
                foreach (var n in quadros)
                    childrenQuadros.AddRange(new Quadro().GetByQuadroPai(n).Select(t => t.Id).ToList());
                quadros.AddRange(childrenQuadros);
            }
            quadros.Add(idQuadro);
            return _repositoryConsulta.GetObjectSet().Where(m => m.IdPrefeitura == idPrefeitura && m.Revisao == revisao && quadros.Contains(m.IdQuadro));
        }

        public IQueryable<ConsultaLogInfo> GetConsultaByPrefeitura(int idPrefeitura)
        {
            return _repositoryConsulta.GetObjectSet().Where(m => m.IdPrefeitura == idPrefeitura);
        }
        public IQueryable<ConsultaLogInfo> GetConsultaByPrefeituraUltimaRevisao(int idPrefeitura)
        {
            var revisao = new Prefeitura().GetRevisaoByPrefeitura(idPrefeitura);
            return _repositoryConsulta.GetObjectSet().Where(m => m.IdPrefeitura == idPrefeitura && m.Revisao == revisao);
        }

        public IQueryable<ConsultaLogInfo> GetConsultaByQuadroEItem(int idPrefeitura, Int32 idQuadro, Int32 idForeignkey)
        {
            var revisao = new Prefeitura().GetRevisaoByPrefeitura(idPrefeitura);
            var quadros = new Quadro().GetByQuadroPai(idQuadro).Select(t => t.Id).ToList();
            if (idQuadro == 17 || idQuadro == 37 || idQuadro == 20)
            {
                return _repositoryConsulta.GetQuery().Where(m => m.IdPrefeitura == idPrefeitura && m.Revisao == revisao && ((m.IdForeignKey == idForeignkey && m.IdQuadro == idQuadro) || (m.IdForeignKeyPai == idForeignkey && quadros.Contains(m.IdQuadro))));
            }

            quadros.Add(idQuadro);
            return _repositoryConsulta.GetObjectSet().Where(m => m.IdPrefeitura == idPrefeitura && m.Revisao == revisao && m.IdForeignKey == idForeignkey && quadros.Contains(m.IdQuadro));
        }

        public static LogInfo CreateLog(Int32 idPrefeitura, EAcao acao, Int32 idQuadro, String descricao)
        {
            return CreateLog(idPrefeitura, acao, idQuadro, descricao, null, null);
        }

        public static LogInfo CreateLog(Int32 idPrefeitura, EAcao acao, Int32 idQuadro, String descricao, Int32? idForeignKey)
        {
            return CreateLog(idPrefeitura, acao, idQuadro, descricao, idForeignKey, null);
        }

        public static LogInfo CreateLog(Int32 idPrefeitura, EAcao acao, Int32 idQuadro, String descricao, Int32? idForeignKey, Int32? idForeignKeyPai)
        {
            var log = new LogInfo();
            log.Revisao = new Prefeitura().GetRevisaoByPrefeitura(idPrefeitura);
            if (log.Revisao == 0)
                return null;
            log.IdPrefeitura = idPrefeitura;
            log.DataHorario = DateTime.Now;
            log.IdQuadro = idQuadro;
            log.EnumAcao = acao;
            log.IdForeignKey = idForeignKey;
            log.IdForeignKeyPai = idForeignKeyPai;
            log.Descricao = descricao;

            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;
            //if (identity == null || identity.Id == 0)
            //    return null;

            ///DBM: PARA TESTE VIA UnitTest - Autenticação Windows - Precisa mockar
            if (Thread.CurrentPrincipal as System.Security.Principal.WindowsPrincipal != null)
            {
                log.IdUsuario = 9999; //id fake munit teste
            }
            else
            {

                IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
                IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
                var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

                log.IdUsuario = id;
            }

            return log;
        }

        public static String CreateDescricaoDefaultUpdate(List<String> propriedades)
        {
            return "Campo(s) alterado(s): " + Extensions.Concat(propriedades) + ".";
        }

        public static String CreateMotivosDefault(List<String> propriedades)
        {
            return "motivo(s) indicado(s): " + Extensions.Concat(propriedades) + ". ";
        }

        public static String CreateDescricaoDefaultData(List<String> propriedades)
        {
            return "data(s) prevista(s): " + Extensions.Concat(propriedades) + ". ";
        }

        public static String DeleteDescricaoDefaultData(List<String> propriedades)
        {
            return "data(s) excluída(s): " + Extensions.Concat(propriedades) + ". ";
        }

        public void Update(LogInfo obj, Boolean commit)
        {
            _repository.Update(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Add(LogInfo obj, Boolean commit)
        {
            _repository.Add(obj);
            if (commit)
                ContextManager.Commit();
        }

        public void Delete(LogInfo obj, Boolean commit)
        {
            _repository.Delete(obj);
            if (commit)
                ContextManager.Commit();
        }
    }
}
