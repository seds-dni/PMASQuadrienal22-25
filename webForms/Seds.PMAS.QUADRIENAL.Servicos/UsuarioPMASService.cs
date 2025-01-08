using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seds.PMAS.QUADRIENAL.Servicos.Contratos;
using System.ServiceModel;
using System.Transactions;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio;
using System.Security.Permissions;
using System.Threading;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.Seguranca.Token;
using System.Data;
using System.Data.Objects;
using Microsoft.IdentityModel.Claims;

namespace Seds.PMAS.QUADRIENAL.Servicos
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
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        public UsuarioPMASInfo GetUsuarioLogado()
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;
            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];

            var id = Convert.ToInt32(identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/id").FirstOrDefault().Value);
            if (id == 0)
            {
                return null;
            }

            ContextManager.OpenConnection();

            var usuario = new UsuarioPMAS().GetById(id);
            if (usuario == null)
            {
                ContextManager.CloseConnection();
                return null;
            }

            var cadastroUnico = new CadastroUsuario.Usuario();
            DataSet ds = cadastroUnico.RetornaDadosID(id.ToString());

            usuario.TrocarSenha = Convert.ToBoolean(ds.Tables[0].Rows[0]["USU_FL_SENHA_ALTERADA"]);

            var perfis = new List<string>();
            using (var proxy = new ProxyPerfil())
            {
                //TODO:DM: Quando servidor 13 cair - Paleativo Servico de perfis
                perfis = proxy.Service.GetPerfisByUsuario(usuario.IdUsuario).ToList();

                //perfis = new string[] { "PMAS QUADRIENAL@Orgão Gestor" }.ToList();
                //perfis = new string[] { "@DRADS Administrador" }.ToList();
                //perfis = new string[] { "@SEDS" }.ToList();
                //perfis = new string[] { "@CAS" }.ToList();
                //perfis = new string[] { "@Administrador" }.ToList();
                //perfis = new string[] { "@Convidados" }.ToList();
                //perfis = new string[] { "@CMAS" }.ToList();
            }
            var perfil = perfis.FirstOrDefault(c => c.Contains("@") && c.Contains("PMAS QUADRIENAL"));

            if (perfil == null)
                return null;

            usuario.Perfil = perfil.Split('@')[1];
            switch (usuario.Perfil)
            {
                case "Orgão Gestor": usuario.EnumPerfil = EPerfil.OrgaoGestor; break;
                case "DRADS Administrador": usuario.EnumPerfil = EPerfil.DRADSAdministrador; break;
                case "SEDS": usuario.EnumPerfil = EPerfil.SEDS; break;
                case "CAS": usuario.EnumPerfil = EPerfil.CAS; break;
                case "Administrador": usuario.EnumPerfil = EPerfil.Administrador; break;
                case "Convidados": usuario.EnumPerfil = EPerfil.Convidados; break;
                case "DRADS": usuario.EnumPerfil = EPerfil.DRADS; break;
                case "CMAS": usuario.EnumPerfil = EPerfil.CMAS; break;
            }
            if (usuario != null)
            {
                usuario.Recursos = new Recurso().GetRecursosByPerfil(Convert.ToInt32(usuario.EnumPerfil)).ToList();
                ContextManager.CloseConnection();
            }

            return usuario;
        }

        /// <summary>
        /// Seleciona Usuário do PMAS
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns>Dados do Usuário</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        public UsuarioPMASInfo GetUsuarioById(int idUsuario)
        {
            ContextManager.OpenConnection();
            var u = new UsuarioPMAS().GetById(idUsuario);

            if (u == null)
            {
                ContextManager.CloseConnection();
                return null;
            }

            var cadastroUnico = new CadastroUsuario.Usuario();
            DataSet ds = cadastroUnico.RetornaDadosID(idUsuario.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                u.Nome = ds.Tables[0].Rows[0]["USU_NOME"] == null ? string.Empty : ds.Tables[0].Rows[0]["USU_NOME"].ToString();
                u.Login = ds.Tables[0].Rows[0]["USU_LOGIN"] == null ? string.Empty : ds.Tables[0].Rows[0]["USU_LOGIN"].ToString();
                u.RG = ds.Tables[0].Rows[0]["USU_RG"] == null ? string.Empty : ds.Tables[0].Rows[0]["USU_RG"].ToString();
                u.OrgaoEmissor = ds.Tables[0].Rows[0]["USU_ORGEMISSOR"] == null ? string.Empty : ds.Tables[0].Rows[0]["USU_ORGEMISSOR"].ToString();
                u.UFCidade = u.UFRG = ds.Tables[0].Rows[0]["USU_UF"] == null ? string.Empty : ds.Tables[0].Rows[0]["USU_UF"].ToString();
                u.Email = ds.Tables[0].Rows[0]["USU_EMAIL"] == null ? string.Empty : ds.Tables[0].Rows[0]["USU_EMAIL"].ToString();
                u.Telefone = ds.Tables[0].Rows[0]["USU_TELEFONE"] == null ? string.Empty : ds.Tables[0].Rows[0]["USU_TELEFONE"].ToString();
                u.Cidade = ds.Tables[0].Rows[0]["USU_CIDADE"] == null ? string.Empty : ds.Tables[0].Rows[0]["USU_CIDADE"].ToString();

                u.CEP = ds.Tables[0].Rows[0]["USU_CEP"] == null ? string.Empty : ds.Tables[0].Rows[0]["USU_CEP"].ToString();
                u.Endereco = ds.Tables[0].Rows[0]["USU_ENDERECO"] == null ? string.Empty : ds.Tables[0].Rows[0]["USU_ENDERECO"].ToString();
                u.Complemento = ds.Tables[0].Rows[0]["USU_COMPLEMENTO"] == null ? string.Empty : ds.Tables[0].Rows[0]["USU_COMPLEMENTO"].ToString();
                u.Numero = ds.Tables[0].Rows[0]["USU_NROENDERECO"] == null ? string.Empty : ds.Tables[0].Rows[0]["USU_NROENDERECO"].ToString();
                u.Bairro = ds.Tables[0].Rows[0]["USU_BAIRRO"] == null ? string.Empty : ds.Tables[0].Rows[0]["USU_BAIRRO"].ToString();
                u.TrocarSenha = Convert.ToBoolean(ds.Tables[0].Rows[0]["USU_FL_SENHA_ALTERADA"] == null ? false : ds.Tables[0].Rows[0]["USU_FL_SENHA_ALTERADA"]);

               var perfis = new List<string>();
                using (var proxy = new ProxyPerfil())
                {
                    perfis = proxy.Service.GetPerfisByUsuario(u.IdUsuario).ToList();
                }
                var perfil = perfis.FirstOrDefault(c => c.Contains("@") && c.Contains("PMAS QUADRIENAL"));

                if (perfil != null)
                {
                    //  return null;

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
                        case "Gabinete": u.EnumPerfil = EPerfil.Gabinete; break;
                    }
                    if (u != null)
                    {
                        u.Recursos = new Recurso().GetRecursosByPerfil(Convert.ToInt32(u.EnumPerfil)).ToList();
                        ContextManager.CloseConnection();
                    }
                }
            }

                return u;

        }


        /// <summary>
        /// Seleciona Usuário do PMAS
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns>Dados do Usuário</returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        public UsuarioPMASInfo GetUsuarioByCPF(string cpf)
        {
            ContextManager.OpenConnection();
            var u = new UsuarioPMAS().GetUsuarioByCPF(cpf);

            if (u == null)
            {
                ContextManager.CloseConnection();
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

            /*var perfis = new List<string>();
            using (var proxy = new ProxyPerfil())
            {
                perfis = proxy.Service.GetPerfisByUsuario(u.IdUsuario).ToList();
            }
            var perfil = perfis.FirstOrDefault(c => c.Contains("@") && c.Contains("PMAS QUADRIENAL"));
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
                u.Recursos = new Recurso().GetRecursosByPerfil(Convert.ToInt32(u.EnumPerfil)).ToList();
                ContextManager.CloseConnection();
            }*/

            return u;
        }

        /// <summary>
        /// Seleciona Consulta de Usuário do PMAS
        /// </summary>        
        /// <returns></returns>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL")]
        public List<ConsultaUsuariosInfo> GetConsultaUsuariosCadastrados(string nome, string rg, int? idDrads, int? idPerfil, int? idMunicipio, string instituicao)
        {
            ContextManager.OpenConnection();
            var lst = new UsuarioPMAS().GetConsultaUsuariosCadastrados(nome, rg, idDrads, idPerfil, idMunicipio, instituicao).ToList();
            //new UsuarioPMAS().GetConsultaUsuariosCadastrados(nome,rg,idDrads,idPerfil,idMunicipio,instituicao).ToList();
            ContextManager.CloseConnection();
            return lst;
        }

        /// <summary>
        /// Adicionar usuário ao aplicativo PMAS.Caso o usuário não estiver no cadastro único, o mesmo é cadastrado
        /// </summary>
        /// <param name="u">Dados do Usuário</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Administrador")]
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@DRADS Administrador")]
        public Int32 AddUsuario(UsuarioPMASInfo u)
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;

            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var name = identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/login").FirstOrDefault().Value;

            ContextManager.OpenConnection();
            try
            {
                new UsuarioPMAS().Add(u, name);
                ContextManager.CloseConnection();
                return u.IdUsuario;
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                if (ex.InnerException != null)
                    throw new Exception(ex.Message + "\n" + ex.InnerException.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Atualizar dados do usuário do PMAS
        /// </summary>
        /// <param name="u">Dados do Usuário</param>
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Administrador")]
        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@DRADS Administrador")]
        public void UpdateUsuario(UsuarioPMASInfo u)
        {
            //SedsPrincipal principal = (SedsPrincipal)Thread.CurrentPrincipal;
            //SedsIdentity identity = (SedsIdentity)principal.Identity;

            IClaimsPrincipal principal = (IClaimsPrincipal)Thread.CurrentPrincipal;
            IClaimsIdentity identity = (IClaimsIdentity)principal.Identities[0];
            var name = identity.Claims.Where(c => c.ClaimType == "http://seds.sp.gov.br/identity/claims/login").FirstOrDefault().Value;

            ContextManager.OpenConnection();
            try
            {

                new UsuarioPMAS().Update(u, name);
                ContextManager.CloseConnection();
            }
            catch (Exception ex)
            {
                ContextManager.CloseConnection();
                if (ex.InnerException != null)
                    throw new Exception(ex.Message + "\n" + ex.InnerException.Message);
                throw ex;
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Administrador")]
        public DataTable GetConsulta(string idusuario)
        {
            try
            {
                return new Db().GetTable(idusuario);
            }
            catch (Exception ex)
            {
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "PMAS QUADRIENAL@Administrador")]
        public void RunCadastro(string idusuario)
        {
            try
            {
                new Db().Execute(idusuario);
            }
            catch (Exception ex)
            {
                throw new Exception(Extensions.GetExceptionMessage(ex));
            }
        }



    }
}
