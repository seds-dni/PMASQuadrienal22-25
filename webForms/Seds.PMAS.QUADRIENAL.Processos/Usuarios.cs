using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Seds.PMAS.QUADRIENAL.UI.Processos;

namespace Seds.PMAS.QUADRIENAL.Processos
{
    public class Usuarios
    {
        public enum EAutenticaUsuario : int
        {
            UsuarioAutenticado,
            TrocaSenha,
            UsuarioNaoAutencidado
        }

        public UsuarioPMASInfo GetUsuarioFromCadastroUnico(Int32 idUsuario)
        {
            DataSet ds = new CadastroUsuario.Usuario().RetornaDadosID(idUsuario.ToString());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count < 1)
                return null;
            var usuarioPmas = new UsuarioPMASInfo();
            usuarioPmas.IdUsuario = idUsuario;
            usuarioPmas.Nome = ds.Tables[0].Rows[0]["USU_NOME"].ToString();
            usuarioPmas.Login = ds.Tables[0].Rows[0]["USU_LOGIN"].ToString();
            usuarioPmas.RG = ds.Tables[0].Rows[0]["USU_RG"].ToString();
            usuarioPmas.OrgaoEmissor = ds.Tables[0].Rows[0]["USU_ORGEMISSOR"].ToString();
            usuarioPmas.UFCidade = usuarioPmas.UFRG = ds.Tables[0].Rows[0]["USU_UF"].ToString();
            usuarioPmas.Email = ds.Tables[0].Rows[0]["USU_EMAIL"].ToString();
            usuarioPmas.Telefone = ds.Tables[0].Rows[0]["USU_TELEFONE"].ToString();
            usuarioPmas.Cidade = ds.Tables[0].Rows[0]["USU_CIDADE"].ToString();

            usuarioPmas.CEP = ds.Tables[0].Rows[0]["USU_CEP"].ToString();
            usuarioPmas.Endereco = ds.Tables[0].Rows[0]["USU_ENDERECO"].ToString();
            usuarioPmas.Complemento = ds.Tables[0].Rows[0]["USU_COMPLEMENTO"].ToString();
            usuarioPmas.Numero = ds.Tables[0].Rows[0]["USU_NROENDERECO"].ToString();
            usuarioPmas.Bairro = ds.Tables[0].Rows[0]["USU_BAIRRO"].ToString();
            return usuarioPmas;
        }

        public UsuarioPMASInfo GetUsuarioById(Int32 idUsuario, ProxyUsuarioPMAS proxy)
        {
            return proxy.Service.GetUsuarioById(Convert.ToInt32(idUsuario));
        }

        public Int32 SaveUsuario(UsuarioPMASInfo usuario, string action, ProxyUsuarioPMAS proxy)
        {
            if (action == "ADD")
            {
                var existeUsuario = proxy.Service.GetUsuarioByCPF(usuario.CPF);
                if (existeUsuario == null)
                {
                    usuario.IdUsuario = proxy.Service.AddUsuario(usuario);
                }
                else 
                {
                    throw new Exception("Já existe um usuário cadastrado com o CPF informado");
                }
            }
            else
            {
                proxy.Service.UpdateUsuario(usuario);
            }
            return usuario.IdUsuario;
        }

        public List<ConsultaUsuariosInfo> GetConsultaUsuariosCadastrados(string nome, string rg, int? idDrads, int? idPerfil, int?idMunicipio, string instituicao, ProxyUsuarioPMAS proxy)
        {
            return proxy.Service.GetConsultaUsuariosCadastrados(nome,rg, idDrads, idPerfil, idMunicipio, instituicao).ToList();
        }

        public DataSet PesquisarCadastroUnicoUsuarios(string nome, string rg)
        {
            CadastroUsuario.Usuario usuario = new CadastroUsuario.Usuario();
            return usuario.SelecionaUsuarios(nome, rg);
        }

        public void AlterarSenha(string idUsuario, string novaSenha, string confirmacaoSenha)
        {
            CadastroUsuario.Usuario usuario = new CadastroUsuario.Usuario();
            DataSet ds = usuario.RetornaDadosID(idUsuario);
            string login = ds.Tables[0].Rows[0]["USU_LOGIN"].ToString();

            var lstMsg = new List<string>();

            if (novaSenha != confirmacaoSenha)
            {
                lstMsg.Add("Nova senha diferente da senha atual.");
            }
            else if (novaSenha.Length > 10
                    || novaSenha.Length < 4)
            {
                lstMsg.Add("Digite números e/ou letras entre 4 e 10 caracteres!");
            }
            if (lstMsg.Count > 0)
                throw new Exception(Util.Concat(lstMsg, System.Environment.NewLine));

            usuario.AlterarSenhaDoUsuario(login, novaSenha, confirmacaoSenha, false);
        }

        public void AlterarSenha(string idUsuario, string senhaAtual, string novaSenha, string confirmacaoSenha)
        {
            CadastroUsuario.Usuario usuario = new CadastroUsuario.Usuario();
            DataSet ds = usuario.RetornaDadosID(idUsuario);
            string login = ds.Tables[0].Rows[0]["USU_LOGIN"].ToString();

            EAutenticaUsuario autentica = this.ValidarSenha(login, senhaAtual);
            var lstMsg = new List<string>();
            if (autentica == EAutenticaUsuario.UsuarioNaoAutencidado)
            {
                lstMsg.Add("Senha atual inválida");
            }
            if (novaSenha != confirmacaoSenha)
            {
                lstMsg.Add("Nova senha diferente da senha atual.");
            }
            else if (novaSenha.Length > 10 || novaSenha.Length < 4)
            {
                lstMsg.Add("Digite números e/ou letras entre 4 e 10 caracteres!");
            }
            if (lstMsg.Count > 0)
                throw new Exception(Util.Concat(lstMsg, System.Environment.NewLine));
            
            usuario.AlterarSenhaDoUsuario(login, novaSenha, confirmacaoSenha, false);
            
        }

        public EAutenticaUsuario ValidarSenha(string login, string senha)
        {
            EAutenticaUsuario retorno = EAutenticaUsuario.UsuarioNaoAutencidado;
            
            if (string.IsNullOrEmpty(login.Replace(" ", "")) || string.IsNullOrEmpty(senha.Replace(" ", "")))            
                throw new Exception("Informe o Login e a Senha.");
                      
            CadastroUsuario.Usuario cadastroUnico = new CadastroUsuario.Usuario();

            DataSet dsUsuario = cadastroUnico.RetornaDados(login);
            if (dsUsuario.Tables[0].Rows.Count == 0)            
                throw new Exception("Usuário não cadastrado.");
            
            int? idCadastroUnico = Convert.ToInt32(dsUsuario.Tables[0].Rows[0]["USU_ID"].ToString());
            DataSet ds = cadastroUnico.Autentica(login, senha);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (!Convert.IsDBNull(ds.Tables[0].Rows[0]["OK"]))
                {
                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["OK"]))
                    {
                        if (Convert.ToBoolean(dsUsuario.Tables[0].Rows[0]["USU_FL_SENHA_ALTERADA"].ToString()))
                        {
                            retorno = EAutenticaUsuario.TrocaSenha;
                        }
                        else
                        {
                            retorno = EAutenticaUsuario.UsuarioAutenticado;
                        }
                    }
                    else
                    {
                        throw new Exception("Senha inválida.");
                    }
                }                                    
            }            
            return retorno;
        }
    }
}
