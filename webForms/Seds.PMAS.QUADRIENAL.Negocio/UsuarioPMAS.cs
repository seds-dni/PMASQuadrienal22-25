using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Persistencia.Repositorio;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using System.Transactions;
using Seds.PMAS.QUADRIENAL.Persistencia;


namespace Seds.PMAS.QUADRIENAL.Negocio
{
    public class UsuarioPMAS
    {
        private static IRepository<UsuarioPMASInfo> _repository
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<UsuarioPMASInfo>>();
            }
        }

        private static IRepository<ConsultaUsuariosInfo> _repositoryView
        {
            get
            {
                return ObjectFactory.GetInstance<IRepository<ConsultaUsuariosInfo>>();
            }
        }

        public UsuarioPMASInfo GetById(int id)
        {
            return _repository.GetObjectSet().Include("Prefeitura")
                .Include("Prefeitura.Situacao")
                .Include("Prefeitura.PrefeiturasExerciciosBloqueio")
                .SingleOrDefault(m => m.IdUsuario == id);
        }

        public UsuarioPMASInfo GetUsuarioByCPF(string cpf)
        {
            return _repository.GetObjectSet().Include("Prefeitura").Include("Prefeitura.Situacao").SingleOrDefault(m => m.CPF == cpf);
        }


        public void ValidarUsuario(UsuarioPMASInfo u)
        {
            var lst = new List<String>();
            if (!Util.ValidaCPF(u.CPF))
            {
                lst.Add("C.P.F. inválido.");
            }
            if (!Util.ValidaString(u.Nome))
            {
                lst.Add("É obrigatório informar o nome.");
            }
            if (!Util.ValidaString(u.RG))
            {
                lst.Add("É obrigatório informar o RG.");
            }
            if (!Util.ValidaString(u.OrgaoEmissor))
            {
                lst.Add("É obrigatório informar o órgão emissor.");
            }
            if (!Util.ValidaString(u.Email))
            {
                lst.Add("É obrigatório informar o email.");
            }
            if (!Util.ValidaString(u.CEP))
            {
                lst.Add("É obrigatório informar o CEP.");
            }
            if (!Util.ValidaString(u.Endereco))
            {
                lst.Add("É obrigatório informar o endereço.");
            }
            if (!Util.ValidaString(u.Numero))
            {
                lst.Add("É obrigatório informar o número do endereço.");
            }
            if (!Util.ValidaString(u.Bairro))
            {
                lst.Add("É obrigatório informar o bairro.");
            }
            if (!Util.ValidaString(u.Cidade))
            {
                lst.Add("É obrigatório informar a cidade.");
            }
            if (!Util.ValidaString(u.UFCidade))
            {
                lst.Add("É obrigatório informar o UF.");
            }
            if (!Util.ValidaString(u.Telefone))
            {
                lst.Add("É obrigatório informar o telefone.");
            }

            if (!Util.ValidaString(u.Login))
            {
                lst.Add("É obrigatório informar o login.");
            }

            if (String.IsNullOrEmpty(u.Instituicao))
            {
                lst.Add("É obrigatório informar a Instituição.");
            }

            if (String.IsNullOrEmpty(u.Cargo))
            {
                lst.Add("É obrigatório informar o Cargo.");
            }
         
            if (u.EnumPerfil == null)
            {
                lst.Add("É obrigatório informar o Perfil.");
            }            
            else
            {                
                switch (u.EnumPerfil)
                {
                    case EPerfil.OrgaoGestor:
                    case EPerfil.CMAS:
                        if (!u.IdPrefeitura.HasValue || u.IdPrefeitura.Value == 0)
                        {
                            lst.Add("É obrigatório informar a Prefeitura.");
                        }                        
                        break;
                    case EPerfil.DRADSAdministrador:
                    case EPerfil.DRADS:
                        if (!u.IdDrads.HasValue || u.IdDrads.Value == 0)
                        {
                            lst.Add("É obrigatório informar a Drads.");
                        }                        
                        break;
                    case EPerfil.SEDS:                        
                    case EPerfil.CAS:
                    case EPerfil.Administrador:                        
                    case EPerfil.Convidados:
                    case EPerfil.Gabinete:
                        break;
                    default:
                        break;
                }

            }

            if(lst.Count > 0) {
                throw new Exception(Extensions.Concat(lst,System.Environment.NewLine));
            }
        }

        public void Add(UsuarioPMASInfo u, String loginUsuarioLogado)
        {
            try
            {
                ValidarUsuario(u);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            CadastroUsuario.Usuario usuario = new CadastroUsuario.Usuario();
            try
            {
                string senha = string.Empty;
                string tipo = string.Empty;
                if (u.IdUsuario == 0)
                {
                    senha = "pmasweb";
                    tipo = "I";
                }
                else
                    tipo = "A";

                int iResultado = usuario.AtualizaDados(tipo, loginUsuarioLogado, u.Nome, u.RG, u.OrgaoEmissor, u.UFRG, u.Email, u.CEP, u.Endereco, u.Numero, u.Complemento, u.Bairro, u.Cidade, u.UFCidade, u.Telefone, u.Celular, u.Login, senha);

                if (iResultado == -2627)
                    throw new Exception("Usuário existente na base de dados !");

                if (u.IdUsuario == 0)
                    u.IdUsuario = iResultado;

                switch (u.EnumPerfil)
                {
                    case EPerfil.OrgaoGestor:
                    case EPerfil.SEDS:
                    case EPerfil.CAS:
                    case EPerfil.Gabinete:
                    case EPerfil.Administrador:
                    case EPerfil.CMAS:
                         u.IdDrads = null;
                         break;
                    case EPerfil.DRADSAdministrador:
                    case EPerfil.DRADS:
                         u.IdPrefeitura = null;
                         break;
                    default:
                         break;
                }

                u.IdStatus = 1;
                TransactionOptions tsOptions = new TransactionOptions();
                tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
                {
                    _repository.Add(u);


                    (ContextManager.GetContext() as PMASContext).InserirPerfilUsuario(u.IdUsuario, (int)u.EnumPerfil);

                    ContextManager.Commit();
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    throw new Exception("Login do usuário já existente na base de dados, por favor altere-o e clique em Salvar novamente!");
                throw ex;
            }            
            
        }

        public void Update(UsuarioPMASInfo u, String loginUsuarioLogado)
        {

            try
            {
                ValidarUsuario(u);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            CadastroUsuario.Usuario usuario = new CadastroUsuario.Usuario();
            try
            {
                string senha = string.Empty;
                string tipo = "A";
               


                int iResultado = usuario.AtualizaDados(tipo, loginUsuarioLogado, u.Nome, u.RG, u.OrgaoEmissor, u.UFRG, u.Email, u.CEP, u.Endereco, u.Numero, u.Complemento, u.Bairro, u.Cidade, u.UFCidade, u.Telefone, u.Celular, u.Login, senha);

                if (iResultado == -2627)
                    throw new Exception("Usuário existente na base de dados !");                

                switch (u.EnumPerfil)
                {
                    case EPerfil.OrgaoGestor:
                    case EPerfil.SEDS:
                    case EPerfil.CAS:
                    case EPerfil.Administrador:
                    //case EPerfil.Convidados:
                    case EPerfil.CMAS:
                        u.IdDrads = null;
                        break;
                    case EPerfil.DRADSAdministrador:
                    case EPerfil.DRADS:
                        u.IdPrefeitura = null;
                        break;
                    default:
                        break;
                }

                u.IdStatus = 1;

                TransactionOptions tsOptions = new TransactionOptions();
                tsOptions.IsolationLevel = IsolationLevel.ReadCommitted;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, tsOptions))
                {

                    int idPerfilAntigo = u.idPerfilAntigo;
                    _repository.Update(u);
                    ContextManager.Commit();

                    if (idPerfilAntigo == 0)
                    {
                        (ContextManager.GetContext() as PMASContext).InserirPerfilUsuario(u.IdUsuario, (int)u.EnumPerfil);
                    }
                    else
                    {

                        (ContextManager.GetContext() as PMASContext).AtualizarPerfilUsuario(u.IdUsuario, (int)u.EnumPerfil, idPerfilAntigo);
                    }
                                     
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    throw new Exception("Login do usuário já existente na base de dados, por favor altere-o e clique em Salvar novamente!");
                throw ex;
            }

        }

        public IQueryable<ConsultaUsuariosInfo> GetConsultaUsuariosCadastrados(string nome, string rg, int? idDrads, int? idPerfil, int? idMunicipio, string instituicao)
        {
            IQueryable<ConsultaUsuariosInfo> retorno = _repositoryView.GetQuery().OrderBy(m=> m.Nome);
            if(!String.IsNullOrEmpty(nome))
                retorno = (from a in retorno
                       where a.Nome.Contains(nome)
                       orderby a.Nome
                       select a);
            if (rg.Length.ToString() != "0")
                retorno = (from a in retorno                           
                           where a.RG.StartsWith(rg)
                           orderby a.Nome
                           select a);

            if (idDrads.HasValue)
                retorno = (from a in retorno
                           where a.IdDrads == idDrads
                           orderby a.Nome
                           select a);

            if (idPerfil.HasValue)
                retorno = (from a in retorno
                           where a.IdPerfil == idPerfil.Value
                           orderby a.Nome
                           select a);

            if(idMunicipio.HasValue)
                retorno = (from a in retorno
                           where a.IdMunicipio == idMunicipio
                           orderby a.Nome
                           select a);

            if(!String.IsNullOrEmpty(instituicao))
                retorno = (from a in retorno
                           where a.Instituicao.Contains(instituicao)
                           orderby a.Nome
                           select a);

            return retorno;
        }       
    }
}
