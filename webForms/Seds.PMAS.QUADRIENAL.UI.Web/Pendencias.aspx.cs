using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio;
using Seds.PMAS.QUADRIENAL.Pendencia;

namespace Seds.PMAS.QUADRIENAL.UI.Web
{
    public partial class Pendencias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (SessaoPmas.UsuarioLogado.Prefeitura == null)
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }
                load();
            }
        }

        void load()
        {
            if (SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == Convert.ToInt32(ESituacao.EmAnalisedoCMAS) && SessaoPmas.UsuarioLogado.EnumPerfil.Value == EPerfil.CMAS)
            {
                bloco7.Visible = true;
            }
            else if (SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao > Convert.ToInt32(ESituacao.EmAnalisedoCMAS))
            {
                bloco7.Visible = true;
            }

            lblMunicipio.Text = SessaoPmas.UsuarioLogado.Prefeitura.Municipio.Nome;
            ValidacaoPMASInfo pendencia;

            pendencia = new VerificadorPendenciaPMAS().ValidarPlanoMunicipalByPrefeitura(
                       SessaoPmas.UsuarioLogado.Prefeitura.Id
                       , SessaoPmas.UsuarioLogado.EnumPerfil.Value
                       , (Object)RetornaTotalCofinanciamentoRepasseMunicipal());
            

            if (pendencia == null)
                return;

            if (!pendencia.InformacoesPrefeitura)
                imgMunicipio.Attributes.Add("class", "mif-cross fg-red");
            else
                imgMunicipio.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.InformacoesPrefeito)
                imgPrefeito.Attributes.Add("class", "mif-cross fg-red");
            else
                imgPrefeito.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.InformacoesOrgaoGestor)
                imgOrgaoGestor.Attributes.Add("class", "mif-cross fg-red");
            else
                imgOrgaoGestor.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.InformacoesGestorMunicipal)
                imgGestorMunicipal.Attributes.Add("class", "mif-cross fg-red");
            else
                imgGestorMunicipal.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.InformacoesFundoMunicipal)
                imgFundoMunicipal.Attributes.Add("class", "mif-cross fg-red");
            else
                imgFundoMunicipal.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.InformacoesConselhosMunicipais)
                imgConselhosMunicipais.Attributes.Add("class", "mif-cross fg-red");
            else
                imgConselhosMunicipais.Attributes.Add("class", "mif-checkmark fg-green");


            if (!pendencia.TerritorioDemografia)
                imgTerritorioDemografia.Attributes.Add("class", "mif-cross fg-red");
            else
                imgTerritorioDemografia.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.PopulacaoVulnerabilidade)
                imgPopulacaoVulnerabilidade.Attributes.Add("class", "mif-cross fg-red");
            else
                imgPopulacaoVulnerabilidade.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.EvolucaoRedeAtendimento)
                imgEvolucaoRedeAtendimento.Attributes.Add("class", "mif-cross fg-red");
            else
                imgEvolucaoRedeAtendimento.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.SituacoesVulnerabilidade)
                imgSituacaoVulnerabilidade.Attributes.Add("class", "mif-cross fg-red");
            else
                imgSituacaoVulnerabilidade.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.AnaliseInterpretacao)
                imgAnaliseInterpretacao.Attributes.Add("class", "mif-cross fg-red");
            else
                imgAnaliseInterpretacao.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.RedeProtecaoSocialPublica)
                imgExecutoraPublica.Attributes.Add("class", "mif-cross fg-red");
            else
                imgExecutoraPublica.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.RedeProtecaoSocialPrivada)
                imgExecutoraPrivada.Attributes.Add("class", "mif-cross fg-red");
            else
                imgExecutoraPrivada.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.CRAS)
                imgCRAS.Attributes.Add("class", "mif-cross fg-red");
            else
                imgCRAS.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.CREAS)
                imgCREAS.Attributes.Add("class", "mif-cross fg-red");
            else
                imgCREAS.Attributes.Add("class", "mif-checkmark fg-green");


            if (!pendencia.CentroPOP)
                imgCentroPOP.Attributes.Add("class", "mif-cross fg-red");
            else
                imgCentroPOP.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.ProgramasProjetos)
                imgProgramas.Attributes.Add("class", "mif-cross fg-red");
            else
                imgProgramas.Attributes.Add("class", "mif-checkmark fg-green");
            //if (!pendencia.TransferenciaRenda)
            //    imgTransferenciaRenda.ImageUrl = "~/Styles/Icones/messagebox_critical.png";
            if (!pendencia.BeneficiosContinuados)
                imgBeneficiosContinuados.Attributes.Add("class", "mif-cross fg-red");
            else
                imgBeneficiosContinuados.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.BeneficiosEventuais)
                imgBeneficios.Attributes.Add("class", "mif-cross fg-red");
            else
                imgBeneficios.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.Educacao)
                imgEducacao.Attributes.Add("class", "mif-cross fg-red");
            else
                imgEducacao.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.Saude)
                imgSaude.Attributes.Add("class", "mif-cross fg-red");
            else
                imgSaude.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.SegurancaAlimentar)
                imgAlimentacao.Attributes.Add("class", "mif-cross fg-red");
            else
                imgAlimentacao.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.Emprego)
                imgEmprego.Attributes.Add("class", "mif-cross fg-red");
            else
                imgEmprego.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.OutrasPoliticas)
                imgOutrasPoliticas.Attributes.Add("class", "mif-cross fg-red");
            else
                imgOutrasPoliticas.Attributes.Add("class", "mif-checkmark fg-green");


            if (!pendencia.FontesFinanciamento)
                imgFontesFinanciamento.Attributes.Add("class", "mif-cross fg-red");
            else
                imgFontesFinanciamento.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.CronogramaDesembolsoProtecaoBasica)
                imgCronogramaBasica.Attributes.Add("class", "mif-cross fg-red");
            else
                imgCronogramaBasica.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.CronogramaDesembolsoProtecaoMedia)
                imgCronogramaMedia.Attributes.Add("class", "mif-cross fg-red");
            else
                imgCronogramaMedia.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.CronogramaDesembolsoProtecaoAlta)
                imgCronogramaAlta.Attributes.Add("class", "mif-cross fg-red");
            else
                imgCronogramaAlta.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.CronogramaDesembolsoProgramaProjeto)
                imgCronogramaProgramas.Attributes.Add("class", "mif-cross fg-red");
            else
                imgCronogramaProgramas.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.CronogramaDesembolsoBeneficioEventual)
                imgCronogramaBeneficios.Attributes.Add("class", "mif-cross fg-red");
            else
                imgCronogramaBeneficios.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.AcoesPlanejadas)
                imgAcoes.Attributes.Add("class", "mif-cross fg-red");
            else
                imgAcoes.Attributes.Add("class", "mif-checkmark fg-green");


            if (!pendencia.VigilanciaSocioAssistencial)
                imgVigilancia.Attributes.Add("class", "mif-cross fg-red");
            else
                imgVigilancia.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.Monitoramento)
                imgMonitoramento.Attributes.Add("class", "mif-cross fg-red");
            else
                imgMonitoramento.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.Avaliacao)
                imgAvaliacao.Attributes.Add("class", "mif-cross fg-red");
            else
                imgAvaliacao.Attributes.Add("class", "mif-checkmark fg-green");

            if (!pendencia.AspectosGerais)
                imgAspectosGerais.Attributes.Add("class", "mif-cross fg-red");
            else
                imgAspectosGerais.Attributes.Add("class", "mif-checkmark fg-green");

            if (SessaoPmas.UsuarioLogado.EnumPerfil.Value == EPerfil.CMAS)
            {
                bloco7.Visible = true;
                if (!pendencia.ConselhoMunicipal)
                    imgCMAS.Attributes.Add("class", "mif-cross fg-red");
                else
                    imgCMAS.Attributes.Add("class", "mif-checkmark fg-green");

                if (!pendencia.ParecerConselhoMunicipal)
                    imgParecerCMAS.Attributes.Add("class", "mif-cross fg-red");
                else
                    imgParecerCMAS.Attributes.Add("class", "mif-checkmark fg-green");
            }

            tbInconsistencias.Visible = pendencia.Pendencias.Count > 0;
            var msg = String.Empty;

            pendencia.Pendencias.ForEach(p => msg += p + "<br/><hr style='background:#000' />");

            lblInconsistencias.Text = msg;

            tbAlertas.Visible = pendencia.Alertas.Count > 0;
            var alerta = String.Empty;
            pendencia.Alertas.ForEach(p => alerta += p + "<br/>");
            lblAlertas.Text = alerta;
        }

        private Decimal RetornaTotalCofinanciamentoRepasseMunicipal()
        {
            int exercicio = 2022;

            var prefeituras = (new Prefeituras(new ProxyPrefeitura()));
            Decimal servicosSocioAssMunicipal = prefeituras.GetPrevisaoOrcamentaria(SessaoPmas.UsuarioLogado.Prefeitura.Id, exercicio).Sum(p => p.RedePublicaMunicipal + p.RedePrivadaMunicipal);
            return servicosSocioAssMunicipal;
        }
    }
}