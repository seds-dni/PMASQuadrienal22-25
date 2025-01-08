using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Entidades.Estruturas;
using Genericos;
using Seds.PMAS.QUADRIENAL.UI.Processos;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class ConsultaPMAS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);
                carregarCombos();                
            }
        }

        void load()
        {
            var lst = new List<ConsultaFluxoInfo>();
            var municipios = new List<Int32>();
            if (ddlMunicipio.SelectedIndex != 0)
                municipios.Add(Convert.ToInt32(ddlMunicipio.SelectedValue));
            else if(ddlDrads.SelectedIndex != 0)
                municipios.AddRange(ProxyDivisaoAdministrativa.MunicipiosEstaduais.Where(m => m.IdDrads == Convert.ToInt32(ddlDrads.SelectedValue)).Select(m => m.Id).ToList());

            using (var proxy = new ProxyPrefeitura())
            {
                lst = proxy.Service.GetConsultaFluxo(municipios.ToList()).ToList();
            }

            lst.ForEach(c => c.Municipio = ProxyDivisaoAdministrativa.MunicipiosEstaduais.First(m => m.Id == c.IdMunicipio).Nome);

            lstPMAS.DataSource = lst.OrderBy(c => c.Municipio);
            lstPMAS.DataBind();
            
        }

        void carregarCombos()
        {
            if (SessaoPmas.UsuarioLogado.Perfil == "Convidados")
            {
                ddlDrads.DataSource = SessaoPmas.UsuarioLogado.IdDrads != null ? ProxyDivisaoAdministrativa.Drads.Where(d => d.Id == SessaoPmas.UsuarioLogado.IdDrads) : ProxyDivisaoAdministrativa.Drads;
            }
            else
            {
                ddlDrads.DataSource = ProxyDivisaoAdministrativa.Drads;
            }
            ddlDrads.DataTextField = "Nome";
            ddlDrads.DataValueField = "Id";
            ddlDrads.DataBind();
            ddlDrads.Items.Insert(0, new ListItem("Todos", "0", true));
            ddlDrads.SelectedIndex = 0;

            if (SessaoPmas.UsuarioLogado.Perfil == "Convidados")  
            {
                if (SessaoPmas.UsuarioLogado.IdPrefeitura != null)
                {
                    using (var proxy = new ProxyPrefeitura())
                    {
                        var p = proxy.Service.GetPrefeituraById(SessaoPmas.UsuarioLogado.IdPrefeitura.Value);
                        ddlMunicipio.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais.Where(m => m.Id == p.IdMunicipio).ToList();
                    }                    
                }
                else
                {
                    ddlMunicipio.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais;
                }
            }
            else
            {
                ddlMunicipio.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais;
            }            
            ddlMunicipio.DataTextField = "Nome";
            ddlMunicipio.DataValueField = "Id";
            ddlMunicipio.DataBind();
            ddlMunicipio.Items.Insert(0, new ListItem("Todos", "0", true));
            ddlMunicipio.SelectedIndex = 0;

            if (SessaoPmas.UsuarioLogado.Perfil == "Convidados")  
            {
                if (SessaoPmas.UsuarioLogado.IdPrefeitura != null)
                {
                    ddlMunicipio.Enabled = false;
                    ddlDrads.Enabled = false;
                    ddlMunicipio.SelectedIndex = 1;
                    ddlDrads.SelectedValue = ProxyDivisaoAdministrativa.MunicipiosEstaduais.Where(m => m.Id == Convert.ToInt32(ddlMunicipio.SelectedValue)).Single().IdDrads.ToString();
                }

                if (SessaoPmas.UsuarioLogado.IdDrads != null)
                {
                    ddlDrads.Enabled = false;
                    ddlDrads.SelectedIndex = 1;
                    ddlMunicipio.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais.Where(m => m.IdDrads == Convert.ToInt32(ddlDrads.SelectedValue)).ToList();
                    ddlMunicipio.DataBind();
                    ddlMunicipio.Items.Insert(0, new ListItem("Todos", "0", true));
                    ddlMunicipio.SelectedIndex = 0;
                }
            }
        }

        void carregarComboMunicipioByDrads()
        {
            if (ddlDrads.SelectedIndex == 0)
                ddlMunicipio.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais;
            else
                ddlMunicipio.DataSource = ProxyDivisaoAdministrativa.MunicipiosEstaduais.Where(m => m.IdDrads == Convert.ToInt32(ddlDrads.SelectedValue)).ToList();
                ddlMunicipio.DataBind();
                ddlMunicipio.Items.Insert(0, new ListItem("Todos", "0", true));
                ddlMunicipio.SelectedIndex = 0;            
        }

        protected void ddlDrads_SelectedIndexChanged(object sender, EventArgs e)
        {
            carregarComboMunicipioByDrads();
        }

        protected void lstPMAS_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var item = (ConsultaFluxoInfo)e.Item.DataItem;
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();                              
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            load();
        }

        protected void lstPMAS_ItemCommand(object sender, ListViewCommandEventArgs e)
        {            
            var key = lstPMAS.DataKeys[e.Item.DataItemIndex];                

            switch (e.CommandName)
            {
                case "Visualizar_Municipio":                    
                    using(var proxy = new ProxyPrefeitura()){
                        SessaoPmas.UsuarioLogado.Prefeitura = proxy.Service.GetPrefeituraById(Convert.ToInt32(key["IdPrefeitura"]));
                        SessaoPmas.UsuarioLogado.Prefeitura.Municipio = ProxyDivisaoAdministrativa.MunicipiosEstaduais.FirstOrDefault(m => m.Id == Convert.ToInt32(key["IdMunicipio"]));
                        SessaoPmas.UsuarioLogado.Prefeitura.Municipio.Drads = ProxyDivisaoAdministrativa.Drads.FirstOrDefault(d => d.Id == SessaoPmas.UsuarioLogado.Prefeitura.Municipio.IdDrads);
                    }
                    Response.Redirect("~/Default.aspx");
                    break;                

                default:
                    break;
            }
        }
    }
}