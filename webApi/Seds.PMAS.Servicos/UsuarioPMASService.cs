using System;
using System.Collections.Generic;
using System.Linq;
using Seds.PMAS.Servicos.Contratos;
using System.ServiceModel;

using System.Security.Permissions;
using System.Threading;
using System.Data;

using Microsoft.IdentityModel.Claims;
using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Infra.Data.Context;
using Seds.PMAS.Infra.Data.Repositories;

namespace Seds.PMAS.Servicos
{
    /// <summary>
    /// Serviço Responsável por fornecer informações sobre os Usuários do PMAS oriundos do Cadastro Único e Serviço de Segurança
    /// </summary>
    [ServiceBehavior(Namespace = "http://seds.sp.gov.br/usuariopmas",
    ConcurrencyMode = ConcurrencyMode.Multiple,
    InstanceContextMode = InstanceContextMode.PerSession)]
    public class UsuarioPMASService : IUsuarioPMASService
    {
        /// <summary>
        /// Seleciona Usuário Logado
        /// </summary>        
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS")]
        public UsuarioEntity GetUsuarioLogado()
        {
            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);

            if (id == 0)
                return null;
            DBPMASContext context = new DBPMASContext();

            context.OpenConnection();
            var u = new UsuarioRepository().GetById(id);

            if (u == null)
            {
                context.CloseConnection();
                return null;
            }

            var cadastroUnico = new CadastroUsuario.Usuario();
            DataSet ds = cadastroUnico.RetornaDadosID(id.ToString());

            u.TrocarSenha = Convert.ToBoolean(ds.Tables[0].Rows[0]["USU_FL_SENHA_ALTERADA"]);

            var perfis = new List<string>();
            using (var proxy = new ProxyPerfil())
            {
                perfis = proxy.Service.GetPerfisByUsuario(u.IdUsuario).ToList();
            }
            var perfil = perfis.FirstOrDefault(c => c.Contains("@") && c.Contains("PMAS"));
            if (perfil == null)
                return null;

            u.Perfil = perfil.Split('@')[1];
            switch (u.Perfil)
            {
                case "Orgão Gestor": u.EnumPerfil = EPerfil.OrgaoGestor; break;
                case "DRADS Administrador": u.EnumPerfil = EPerfil.DRADSAdministrador; break;
                case "SEDS": u.EnumPerfil = EPerfil.SEDS; break;
                case "CAS": u.EnumPerfil = EPerfil.CAS; break;
                case "Administrador": u.EnumPerfil = EPerfil.Administrador; break;
                case "Convidados": u.EnumPerfil = EPerfil.Convidados; break;
                case "DRADS": u.EnumPerfil = EPerfil.DRADS; break;
                case "CMAS": u.EnumPerfil = EPerfil.CMAS; break;
            }
            if (u != null)
            {
                u.Funcionalidades = new RecursoRepository().GetByIdPerfil(Convert.ToInt32(u.EnumPerfil)).ToList();
                context.CloseConnection();
            }

            return u;
        }

        /// <summary>
        /// Seleciona Usuário do PMAS
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns>Dados do Usuário</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017")]
        public UsuarioEntity GetUsuarioById(int idUsuario)
        {
            DBPMASContext context = new DBPMASContext();
            context.OpenConnection();
            var u = new UsuarioRepository().GetById(idUsuario);

            if (u == null)
            {
                context.CloseConnection();
                return null;
            }

            var cadastroUnico = new CadastroUsuario.Usuario();
            DataSet ds = cadastroUnico.RetornaDadosID(idUsuario.ToString());

            u.Nome = ds.Tables[0].Rows[0]["USU_NOME"].ToString();
            u.Login = ds.Tables[0].Rows[0]["USU_LOGIN"].ToString();
            u.RG = ds.Tables[0].Rows[0]["USU_RG"].ToString();
            u.OrgaoEmissor = ds.Tables[0].Rows[0]["USU_ORGEMISSOR"].ToString();
            u.UFCidade = u.UFRG = ds.Tables[0].Rows[0]["USU_UF"].ToString();
            u.Email = ds.Tables[0].Rows[0]["USU_EMAIL"].ToString();
            u.Telefone = ds.Tables[0].Rows[0]["USU_TELEFONE"].ToString();
            u.Cidade = ds.Tables[0].Rows[0]["USU_CIDADE"].ToString();

            u.CEP = ds.Tables[0].Rows[0]["USU_CEP"].ToString();
            u.Endereco = ds.Tables[0].Rows[0]["USU_ENDERECO"].ToString();
            u.Complemento = ds.Tables[0].Rows[0]["USU_COMPLEMENTO"].ToString();
            u.Numero = ds.Tables[0].Rows[0]["USU_NROENDERECO"].ToString();
            u.Bairro = ds.Tables[0].Rows[0]["USU_BAIRRO"].ToString();
            u.TrocarSenha = Convert.ToBoolean(ds.Tables[0].Rows[0]["USU_FL_SENHA_ALTERADA"]);

            var perfis = new List<string>();
            using (var proxy = new ProxyPerfil())
            {
                perfis = proxy.Service.GetPerfisByUsuario(u.IdUsuario).ToList();
            }
            var perfil = perfis.FirstOrDefault(c => c.Contains("@") && c.Contains("PMAS 2017"));
            if (perfil == null)
                return null;

            u.Perfil = perfil.Split('@')[1];
            switch (u.Perfil)
            {
                case "Orgão Gestor": u.EnumPerfil = EPerfil.OrgaoGestor; break;
                case "DRADS Administrador": u.EnumPerfil = EPerfil.DRADSAdministrador; break;
                case "SEDS": u.EnumPerfil = EPerfil.SEDS; break;
                case "CAS": u.EnumPerfil = EPerfil.CAS; break;
                case "Administrador": u.EnumPerfil = EPerfil.Administrador; break;
                case "Convidados": u.EnumPerfil = EPerfil.Convidados; break;
                case "DRADS": u.EnumPerfil = EPerfil.DRADS; break;
                case "CMAS": u.EnumPerfil = EPerfil.CMAS; break;
            }
            if (u != null)
            {
                u.Funcionalidades = new RecursoRepository().GetByIdPerfil(Convert.ToInt32(u.EnumPerfil)).ToList();
                context.CloseConnection();
            }

            return u;
        }


        /// <summary>
        /// Seleciona Usuário do PMAS
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns>Dados do Usuário</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017")]
        public UsuarioEntity GetUsuarioByCPF(string cpf)
        {
            DBPMASContext context = new DBPMASContext();
            context.OpenConnection();
            var u = new UsuarioRepository().GetUsuarioByCPF(cpf);

            if (u == null)
            {
                return null;
            }

            var cadastroUnico = new CadastroUsuario.Usuario();
            DataSet ds = cadastroUnico.RetornaDadosID(u.IdUsuario.ToString());

            u.Nome = ds.Tables[0].Rows[0]["USU_NOME"].ToString();
            u.Login = ds.Tables[0].Rows[0]["USU_LOGIN"].ToString();
            u.RG = ds.Tables[0].Rows[0]["USU_RG"].ToString();
            u.OrgaoEmissor = ds.Tables[0].Rows[0]["USU_ORGEMISSOR"].ToString();
            u.UFCidade = u.UFRG = ds.Tables[0].Rows[0]["USU_UF"].ToString();
            u.Email = ds.Tables[0].Rows[0]["USU_EMAIL"].ToString();
            u.Telefone = ds.Tables[0].Rows[0]["USU_TELEFONE"].ToString();
            u.Cidade = ds.Tables[0].Rows[0]["USU_CIDADE"].ToString();

            u.CEP = ds.Tables[0].Rows[0]["USU_CEP"].ToString();
            u.Endereco = ds.Tables[0].Rows[0]["USU_ENDERECO"].ToString();
            u.Complemento = ds.Tables[0].Rows[0]["USU_COMPLEMENTO"].ToString();
            u.Numero = ds.Tables[0].Rows[0]["USU_NROENDERECO"].ToString();
            u.Bairro = ds.Tables[0].Rows[0]["USU_BAIRRO"].ToString();
            u.TrocarSenha = Convert.ToBoolean(ds.Tables[0].Rows[0]["USU_FL_SENHA_ALTERADA"]);

            var perfis = new List<string>();
            using (var proxy = new ProxyPerfil())
            {
                perfis = proxy.Service.GetPerfisByUsuario(u.IdUsuario).ToList();
            }
            var perfil = perfis.FirstOrDefault(c => c.Contains("@") && c.Contains("PMAS 2017"));
            if (perfil == null)
                return null;

            u.Perfil = perfil.Split('@')[1];
            switch (u.Perfil)
            {
                case "Orgão Gestor": u.EnumPerfil = EPerfil.OrgaoGestor; break;
                case "DRADS Administrador": u.EnumPerfil = EPerfil.DRADSAdministrador; break;
                case "SEDS": u.EnumPerfil = EPerfil.SEDS; break;
                case "CAS": u.EnumPerfil = EPerfil.CAS; break;
                case "Administrador": u.EnumPerfil = EPerfil.Administrador; break;
                case "Convidados": u.EnumPerfil = EPerfil.Convidados; break;
                case "DRADS": u.EnumPerfil = EPerfil.DRADS; break;
                case "CMAS": u.EnumPerfil = EPerfil.CMAS; break;
            }
            if (u != null)
            {
                u.Funcionalidades = new RecursoRepository().GetByIdPerfil(Convert.ToInt32(u.EnumPerfil)).ToList();
                context.CloseConnection();
            }

            return u;
        }

        /// <summary>
        /// Seleciona Consulta de Usuário do PMAS
        /// </summary>        
        /// <returns></returns>
        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017")]
        //public List<ConsultaUsuariosInfo> GetConsultaUsuariosCadastrados(string nome, string rg, int? idDrads, int? idPerfil, int? idMunicipio, string instituicao)
        //{

        //    ContextManager.OpenConnection();
        //    var lst = new UsuarioPMAS().GetConsultaUsuariosCadastrados(nome, rg, idDrads, idPerfil, idMunicipio, instituicao).ToList();
        //    //new UsuarioPMAS().GetConsultaUsuariosCadastrados(nome,rg,idDrads,idPerfil,idMunicipio,instituicao).ToList();
        //    ContextManager.CloseConnection();
        //    return lst;
        //}

        /// <summary>
        /// Adicionar usuário ao aplicativo PMAS.Caso o usuário não estiver no cadastro único, o mesmo é cadastrado
        /// </summary>
        /// <param name="u">Dados do Usuário</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017@Administrador")]
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017@DRADS Administrador")]
        public Int32 AddUsuario(UsuarioEntity u)
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;

            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var name = identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/login").FirstOrDefault().Value;

            //ContextManager.OpenConnection();
            try
            {
                new UsuarioRepository().Add(u);
                //ContextManager.CloseConnection();
                return u.IdUsuario;
            }
            catch (Exception ex)
            {
                //ContextManager.CloseConnection();
                if (ex.InnerException != null)
                    throw new Exception(ex.Message + "\n" + ex.InnerException.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Atualizar dados do usuário do PMAS
        /// </summary>
        /// <param name="u">Dados do Usuário</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017@Administrador")]
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017@DRADS Administrador")]
        public void UpdateUsuario(UsuarioEntity u)
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;

            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var name = identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/login").FirstOrDefault().Value;

            try
            {

                new UsuarioRepository().Update(u);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw new Exception(ex.Message + "\n" + ex.InnerException.Message);
                throw ex;
            }
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017@Administrador")]
        //public DataTable GetConsulta(string idusuario)
        //{
        //    try
        //    {   

        //        //return new Db().GetTable(idusuario);
        //    }
        //    catch (Exception ex)
        //    {                
        //        throw new Exception(Extensions.GetExceptionMessage(ex));
        //    }
        //}

        //[PrincipalPermission(SecurityAction.Demand, Role = "PMAS 2017@Administrador")]
        //public void RunCadastro(string idusuario)
        //{
        //    try
        //    {
        //        //new Db().Execute(idusuario);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(Extensions.GetExceptionMessage(ex));
        //    }
        //}

        public List<UsuarioEntity> GetConsultaUsuariosCadastrados(string nome, string rg, int? idDrads, int? idPerfil, int? idMunicipio, string instituicao)
        {
            throw new NotImplementedException();
        }

        public DataTable GetConsulta(string idusuario)
        {
            throw new NotImplementedException();
        }


        public void RunCadastro(string idusuario)
        {
            throw new NotImplementedException();
        }
    }
}
