using Seds.PMAS.Dominio.Entities;
using Seds.PMAS.Dominio.Interfaces.Repositories;
using Seds.PMAS.Dominio.Interfaces.Services;
using Seds.PMAS.Resource.Resources;
using Seds.Seguranca.Negocio;
using System;
using System.Data;

namespace Seds.PMAS.Aplicacao
{
    /// <summary>
    /// Microsoft.owin.Host.SystemWeb
    /// Microsoft.Owin.Security.OAuth
    /// Microsoft.Owin.Cors
    /// </summary>
    public class UsuarioAppService : IUsuarioService
    //AppServiceBase<UsuarioEntity>, IUsuarioAppService
    {
        private IUsuarioRepository _repository;
        public UsuarioAppService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public UsuarioAutenticado Autenticar(string login, string senha)
        {
            var user = new Usuario().Autenticar(login, senha);
            if (user == null)
            {
                throw new Exception(Errors.InvalidCredentials);
            }
            return user;
        }

        public void Create(UsuarioEntity usuario)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
           _repository.Dispose();
        }

        public UsuarioEntity GetByIdUsuario(int idUsuario)
        {
            return _repository.GetByIdUsuario(idUsuario); ;
        }

        public UsuarioEntity GetUsuarioByCPF(string cpf)
        {
            throw new NotImplementedException();
        }

        public UsuarioEntity RetornaDadosIDUsuario(int idUsuario)
        {

            var usuario = _repository.GetByIdUsuario(idUsuario);
            var cadUnico = new CadastroUsuario.Usuario();
            DataSet ds = cadUnico.RetornaDadosID(usuario.IdUsuario.ToString());
            usuario.Nome = ds.Tables[0].Rows[0]["USU_NOME"].ToString();
            usuario.Login = ds.Tables[0].Rows[0]["USU_LOGIN"].ToString();
            usuario.RG = ds.Tables[0].Rows[0]["USU_RG"].ToString();
            usuario.OrgaoEmissor = ds.Tables[0].Rows[0]["USU_ORGEMISSOR"].ToString();
            usuario.UFCidade = usuario.UFRG = ds.Tables[0].Rows[0]["USU_UF"].ToString();
            usuario.Email = ds.Tables[0].Rows[0]["USU_EMAIL"].ToString();
            usuario.Telefone = ds.Tables[0].Rows[0]["USU_TELEFONE"].ToString();
            usuario.Cidade = ds.Tables[0].Rows[0]["USU_CIDADE"].ToString();
            usuario.CEP = ds.Tables[0].Rows[0]["USU_CEP"].ToString();
            usuario.Endereco = ds.Tables[0].Rows[0]["USU_ENDERECO"].ToString();
            usuario.Complemento = ds.Tables[0].Rows[0]["USU_COMPLEMENTO"].ToString();
            usuario.Numero = ds.Tables[0].Rows[0]["USU_NROENDERECO"].ToString();
            usuario.Bairro = ds.Tables[0].Rows[0]["USU_BAIRRO"].ToString();
            usuario.TrocarSenha = Convert.ToBoolean(ds.Tables[0].Rows[0]["USU_FL_SENHA_ALTERADA"]);
            usuario.Perfil = (EPerfil)usuario.IdPerfil;
            // usuario.Funcionalidades = new RecursoRepository().GetByIdPerfil(usuario.IdPerfil).ToList();
            return usuario;
        }

        public void Update(UsuarioEntity usuario)
        {
            throw new NotImplementedException();
        }
    }
}
