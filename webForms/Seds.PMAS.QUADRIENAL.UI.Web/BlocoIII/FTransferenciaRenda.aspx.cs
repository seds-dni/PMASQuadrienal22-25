using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.PMAS.QUADRIENAL.UI.Processos;
using Seds.PMAS.QUADRIENAL.Entidades;
using Seds.PMAS.QUADRIENAL.Negocio.Validadores;
using Seds.PMAS.QUADRIENAL.CA;
using Seds.PMAS.QUADRIENAL.Entidades.Programas;


namespace Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII
{
    public partial class FTransferenciaRenda : System.Web.UI.Page
    {
        private static List<int> Exercicios = new List<int>() { 2022, 2023, 2024, 2025 };
        
        protected List<TransferenciaRendaParceriaInfo> Parcerias
        {
            get { return Session["PARCERIAS"] as List<TransferenciaRendaParceriaInfo>; }
            set { Session["PARCERIAS"] = value; }
        }

        protected List<TransferenciaRendaTecnicoReferenciaInfo> Tecnico
        {
            get { return Session["TECNICO"] as List<TransferenciaRendaTecnicoReferenciaInfo>; }
            set { Session["TECNICO"] = value; }
        }

        protected List<PETIAcaoInfo> AcoesPETI
        {
            get { return Session["ACOESPETI"] as List<PETIAcaoInfo>; }
            set { Session["ACOESPETI"] = value; }
        }


        #region Variaveis Composicao
         int composicao1013Anos2018 =0;
         int composicao1415Anos2018 =0;
         int composicao1617Anos2018 =0;

         int composicao1013Anos2022 = 0;
         int composicao1415Anos2022 = 0;
         int composicao1617Anos2022 = 0;

                                   
         int composicao1013Anos2019 =0;
         int composicao1415Anos2019 =0;
         int composicao1617Anos2019 =0;


         int composicao1013Anos2023 = 0;
         int composicao1415Anos2023 = 0;
         int composicao1617Anos2023 = 0;
                 

         int composicao1013Anos2020 =0;
         int composicao1415Anos2020 =0;
         int composicao1617Anos2020 =0;

         int composicao1013Anos2024 = 0;
         int composicao1415Anos2024 = 0;
         int composicao1617Anos2024 = 0;
        #endregion


         protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessaoPmas.VerificarSessao(this);

                if (SessaoPmas.UsuarioLogado.Prefeitura == null || String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    Response.Redirect("~/Default.aspx");
                    return;
                }

                carregarEstruturas();


                using (var proxy = new ProxyProgramas())
                {
                    load(proxy);
                }

                ordenaQuadro();

                #region Adiciona Eventos
                txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio1.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio2.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio3.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio4.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");

                txtPetiNumeroBeneficiarios.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtPetiMensalRepasse.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtProgramasMunicipaisNumeroBeneficiarios.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtProgramasMunicipaisValorRepasse.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtFMAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtOrcamentoMunicipal.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtOutrosFundosMunicipais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtFEAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtOrcamentoEstadual.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtOutrosFundosEstaduais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtFNAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtOrcamentoFederal.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtOutrosFundosFederais.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtIGDPBF.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtIGDSUAS.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

                txtBolsaFamiliaEstimativaFamiliasExercicio0.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtBolsaFamiliaFamiliasBeneficiariasExercicio0.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtBolsaFamiliaCadastradasExercicio0.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");

                txtBolsaFamiliaRepasseMensalExercicio0.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

                txtBolsaFamiliaEstimativaFamiliasExercicio1.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtBolsaFamiliaFamiliasBeneficiariasExercicio1.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtBolsaFamiliaCadastradasExercicio1.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtBolsaFamiliaRepasseMensalExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");


                txtBolsaFamiliaEstimativaFamiliasExercicio2.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtBolsaFamiliaFamiliasBeneficiariasExercicio2.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtBolsaFamiliaCadastradasExercicio2.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtBolsaFamiliaRepasseMensalExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

                txtBolsaFamiliaEstimativaFamiliasExercicio3.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtBolsaFamiliaFamiliasBeneficiariasExercicio3.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtBolsaFamiliaCadastradasExercicio3.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtBolsaFamiliaRepasseMensalExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtValorAEPETI.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtValorAEPETI2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");

                txtValorRepasseEstadual2021.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtValorRepasseEstadual2022.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtValorRepasseEstadual2023.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtValorRepasseEstadual2024.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtValorRepasseEstadual2025.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                
                txtFEASReprogramadoExercicio1.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtFEASReprogramadoExercicio2.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtFEASReprogramadoExercicio3.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                txtFEASReprogramadoExercicio4.Attributes.Add("oninput", "return( currencyFormat( this, '.', ',', event ) );");
                
                txtNumeroAtendidos2021.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtNumeroAtendidos2022.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtNumeroAtendidos2023.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtNumeroAtendidos2024.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtNumeroAtendidos2025.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");

                txtMetaPactuada2021.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtMetaPactuada2022.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtMetaPactuada2023.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtMetaPactuada2024.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");
                txtMetaPactuada2025.Attributes.Add("onkeypress", "if (event.keyCode < 48 || event.keyCode > 58) {event.keyCode = 0;}");

                txtIdade1013AnosExercicio0.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                txtIdade1415AnosExercicio0.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                txtIdade1617AnosExercicio0.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                txtMetaMunicipalExercicio0.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");


                txtAuxilioAluguelNumeroAtendidosExercicio3.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                txtAuxilioAluguelAtivasExercicio3.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                txtAuxilioAluguelRecebidasExercicio3.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");

                txtAuxilioAluguelNumeroAtendidosExercicio4.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                txtAuxilioAluguelAtivasExercicio4.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                txtAuxilioAluguelRecebidasExercicio4.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");

                txtIdade1013AnosExercicio1.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                txtIdade1415AnosExercicio1.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                txtIdade1617AnosExercicio1.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                txtMetaMunicipalExercicio1.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");

                txtIdade1013AnosExercicio2.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                txtIdade1415AnosExercicio2.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                txtIdade1617AnosExercicio2.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                txtMetaMunicipalExercicio2.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");

                txtIdade1013AnosExercicio2.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                txtIdade1415AnosExercicio2.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                txtIdade1617AnosExercicio2.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                txtMetaMunicipalExercicio2.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");

                txtmediaMensalExercicio1.Attributes.Add("onkeypress", "javascript:SoNumero(this, event);");
                #endregion
               
                #region Bloqueia , Desbloqueia e ordena Controles
                this.AplicarRegraBloqueioDesbloqueio();
                Permissao.VerificarPermissaoControles(this.LoadControlesVerificacaoBloqueioDemaisControles(), Session);
                Permissao.VerificarPermissaoControles(Telefone.Controles, Session);
                Permissao.VerificarPermissaoControles(Celular.Controles, Session);
                Permissao.VerificarPermissaoControles(txtPetiDataAdesao.Controles, Session);
                Permissao.VerificarPermissaoControles(txtDataAdesao.Controles, Session);
                DesbloqueioEspecial();
                #endregion
            }
            
             CarregarExercicios();
        }





        void ordenaQuadro()
        {
            switch ((ETipoTransferenciaRenda)Convert.ToInt32(hdfTipoTransferenciaRenda.Value))
            {
                case ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso:
                case ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia:
                    divAcaoRenda.Visible = false;
                    divBolsaFamilia.Visible = false;
                    divPETI.Visible = false;
                    divProgramasMunicipais.Visible = false;
                    divBeneficiariosBPCIdosoPessoaDeficiencia.Visible = true;
                    break;
                case ETipoTransferenciaRenda.BolsaFamilia:
                    divAcaoRenda.Visible = false;
                    divBolsaFamilia.Visible = true;
                    divPETI.Visible = false;
                    divProgramasMunicipais.Visible = false;
                    divBeneficiariosBPCIdosoPessoaDeficiencia.Visible = false;
                    txtObjetivo.Visible = false;
                    break;
                case ETipoTransferenciaRenda.PETIProgramaErradicacaoTrabalhoInfantil:
                    divAcaoRenda.Visible = false;
                    divBolsaFamilia.Visible = false;
                    divPETI.Visible = true;
                    divProgramasMunicipais.Visible = false;
                    divBeneficiariosBPCIdosoPessoaDeficiencia.Visible = false;
                    break;
                case ETipoTransferenciaRenda.AcaoJovem:
                case ETipoTransferenciaRenda.RendaCidada:
                    divAcaoRenda.Visible = true;                    
                    divBolsaFamilia.Visible = false;
                    divPETI.Visible = false;
                    divProgramasMunicipais.Visible = false;
                    divBeneficiariosBPCIdosoPessoaDeficiencia.Visible = false;
                    trFEASRecursosReprogramados.Visible = false;
                    txtObjetivo.Visible = false;
                    break;
                case ETipoTransferenciaRenda.ProsperaFamilia:
                    divProsperaFamilia.Visible = true;
                    divAcaoRenda.Visible = false;
                    divBolsaFamilia.Visible = false;
                    divPETI.Visible = false;
                    divProgramasMunicipais.Visible = false;
                    divBeneficiariosBPCIdosoPessoaDeficiencia.Visible = false;
                    txtObjetivo.Visible = false;
                    break;
                case ETipoTransferenciaRenda.AuxilioAluguel:
                    divAuxilioAluguel.Visible = true;
                    divCriterioElegibilidade.Visible = true;
                    divAcaoRenda.Visible = false;
                    divBolsaFamilia.Visible = false;
                    divPETI.Visible = false;
                    divProgramasMunicipais.Visible = false;
                    divBeneficiariosBPCIdosoPessoaDeficiencia.Visible = false;
                    txtObjetivo.Visible = false;
                    break;
                case ETipoTransferenciaRenda.FCadUnico:
                    divProsperaFamilia.Visible = true;
                    //trFEASRecursosReprogramados.Visible = true;
                    divAcaoRenda.Visible = false;
                    divBolsaFamilia.Visible = false;
                    divPETI.Visible = false;
                    divProgramasMunicipais.Visible = false;
                    divBeneficiariosBPCIdosoPessoaDeficiencia.Visible = false;
                    txtObjetivo.Visible = false;
                    break;
                case ETipoTransferenciaRenda.FVigilancia:
                    divProsperaFamilia.Visible = true;
                    trFEASRecursosReprogramados.Visible = true;
                    divBeneficiarios.Visible = false;
                    divAcaoRenda.Visible = false;
                    divBolsaFamilia.Visible = false;
                    divPETI.Visible = false;
                    divProgramasMunicipais.Visible = false;
                    divBeneficiariosBPCIdosoPessoaDeficiencia.Visible = false;
                    txtObjetivo.Visible = false;

                    thDemandaEstimada.Visible = false;
                    thNumeroAtendidos.Visible = false;
                    tdMetaPactuada2021.Visible = false;
                    tdNumeroAtendidos2021.Visible = false;
                    
                    tdExercicio2021.Visible = false;
                    tdValorRepasseEstadual2021.Visible = false;

                    tdMetaPactuada2022.Visible = false;
                    tdNumeroAtendidos2022.Visible = false;
                    
                    tdMetaPactuada2023.Visible = false;
                    tdNumeroAtendidos2023.Visible = false;
                    
                    tdMetaPactuada2024.Visible = false;
                    tdNumeroAtendidos2024.Visible = false;
                    
                    tdMetaPactuada2025.Visible = false;
                    tdNumeroAtendidos2025.Visible = false;


                    break;
                case ETipoTransferenciaRenda.ProJovemAdolescente:

                    break;
                case ETipoTransferenciaRenda.Outros:
                    divAcaoRenda.Visible = false;
                    divBolsaFamilia.Visible = false;
                    divPETI.Visible = false;
                    divProgramasMunicipais.Visible = true;
                    divBeneficiariosBPCIdosoPessoaDeficiencia.Visible = false;
                    break;
                default:
                    break;
            }
        }

        private void DesbloqueioEspecial() 
        {
            if (SessaoPmas.UsuarioLogado.Prefeitura.IdSituacao == 1)
            {
                if ((ETipoTransferenciaRenda)Convert.ToInt32(hdfTipoTransferenciaRenda.Value) == ETipoTransferenciaRenda.ProsperaFamilia)
                {
                    AplicarRegraBloqueioDesbloqueioEspecial();
                }                
            }

        }
        void load(ProxyProgramas proxy)
        {
            Parcerias = new List<TransferenciaRendaParceriaInfo>();
            Tecnico = new List<TransferenciaRendaTecnicoReferenciaInfo>();

            if (String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                preencheTitulo(ETipoTransferenciaRenda.Outros);
                return;
            }
            var obj = proxy.Service.GetTransferenciaRendaById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));
            if (obj == null)
                return;

            hdfAno.Value = String.IsNullOrEmpty(hdfAno.Value) ? Exercicios[0].ToString() : hdfAno.Value;
            this.btnExercicio1.CssClass = String.IsNullOrEmpty(hdfAno.Value) ? "btn-seds btn-info-seds" : "btn-seds btn-primary-seds";
            int exercicio = Convert.ToInt32(hdfAno.Value);

            CarregarExercicios();
            selecionaReprogramacao(Exercicios[0]);

            if (obj.IdTipoTransferenciaRenda == 1 || obj.IdTipoTransferenciaRenda == 2)
                btnVoltar.PostBackUrl = "~/BlocoIII/CBeneficiosContinuados.aspx";

            if (obj.IdTipoTransferenciaRenda == (Int32)ETipoTransferenciaRenda.BolsaFamilia)
            {
                Session["idTipoTransferencia"] = 3;
                trArticulacoesPromovidas.Visible = trMetasAtendimento.Visible = true;
                if (obj.TransferenciaRendaPrevisaoAnual != null)
                {

                    if (obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2021.HasValue)
                    txtBolsaFamiliaEstimativaFamiliasExercicio0.Text = obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2021.ToString();
                    txtBolsaFamiliaCadastradasExercicio0.Text = obj.TransferenciaRendaPrevisaoAnual.FamiliasCadastradas2021.HasValue ? obj.TransferenciaRendaPrevisaoAnual.FamiliasCadastradas2021.Value.ToString() : String.Empty;
                    txtBolsaFamiliaFamiliasBeneficiariasExercicio0.Text = obj.TransferenciaRendaPrevisaoAnual.NumeroFamiliasBeneficiarias2021.HasValue ? obj.TransferenciaRendaPrevisaoAnual.NumeroFamiliasBeneficiarias2021.Value.ToString() : String.Empty;
                    txtBolsaFamiliaRepasseMensalExercicio0.Text = obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2021.HasValue ? obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2021.Value.ToString("N2") : String.Empty;
                    lblBolsaFamiliaPrevisaoAnualExercicio0.Text = obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2021.HasValue ? (obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2021.Value * 12).ToString("N2") : String.Empty;


                    txtBolsaFamiliaEstimativaFamiliasExercicio1.Text = obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2022.HasValue ? obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2022.ToString() : String.Empty;
                    txtBolsaFamiliaCadastradasExercicio1.Text = obj.TransferenciaRendaPrevisaoAnual.FamiliasCadastradas2022.HasValue ? obj.TransferenciaRendaPrevisaoAnual.FamiliasCadastradas2022.ToString() : String.Empty;
                    txtBolsaFamiliaFamiliasBeneficiariasExercicio1.Text = obj.TransferenciaRendaPrevisaoAnual.NumeroFamiliasBeneficiarias2022.HasValue ? obj.TransferenciaRendaPrevisaoAnual.NumeroFamiliasBeneficiarias2022.ToString() : String.Empty;
                    txtBolsaFamiliaRepasseMensalExercicio1.Text = obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2022.HasValue ? obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2022.Value.ToString("N2") : String.Empty;
                    lblBolsaFamiliaPrevisaoAnualExercicio1.Text = obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2022.HasValue ? (obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2022.Value * 12).ToString("N2") : String.Empty;

                    txtBolsaFamiliaEstimativaFamiliasExercicio2.Text = obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2023.HasValue ? obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2023.ToString() : String.Empty;
                    txtBolsaFamiliaCadastradasExercicio2.Text = obj.TransferenciaRendaPrevisaoAnual.FamiliasCadastradas2023.HasValue ? obj.TransferenciaRendaPrevisaoAnual.FamiliasCadastradas2023.ToString() : String.Empty;
                    txtBolsaFamiliaFamiliasBeneficiariasExercicio2.Text = obj.TransferenciaRendaPrevisaoAnual.NumeroFamiliasBeneficiarias2023.ToString();
                    txtBolsaFamiliaRepasseMensalExercicio2.Text = obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2023.HasValue ? obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2023.Value.ToString("N2") : String.Empty;
                    lblBolsaFamiliaPrevisaoAnualExercicio2.Text = obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2023.HasValue ? (obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2023.Value * 12).ToString("N2") : String.Empty;

                    txtBolsaFamiliaEstimativaFamiliasExercicio3.Text = obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2024.ToString() : String.Empty;
                    txtBolsaFamiliaCadastradasExercicio3.Text = obj.TransferenciaRendaPrevisaoAnual.FamiliasCadastradas2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.FamiliasCadastradas2024.ToString() : String.Empty;
                    txtBolsaFamiliaFamiliasBeneficiariasExercicio3.Text = obj.TransferenciaRendaPrevisaoAnual.NumeroFamiliasBeneficiarias2024.ToString();
                    txtBolsaFamiliaRepasseMensalExercicio3.Text = obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2024.Value.ToString("N2") : String.Empty;
                    lblBolsaFamiliaPrevisaoAnualExercicio3.Text = obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2024.HasValue ? (obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2024.Value * 12).ToString("N2") : String.Empty;
                }
            }


            if (obj.IdTipoTransferenciaRenda == (Int32)ETipoTransferenciaRenda.PETIProgramaErradicacaoTrabalhoInfantil)
            {
                Session["idTipoTransferencia"] = 4;
                carregarEstruturasPETI(obj);
            }

            //Renda Cidadã e Ação Jovem
            if (obj.IdTipoTransferenciaRenda == 5 || obj.IdTipoTransferenciaRenda == 6)
            {
                Session["idTipoTransferencia"] = 5;
                rblAdesaoPrograma.SelectedValue = obj.ExecutaPrograma.HasValue && obj.ExecutaPrograma.Value ? "1" : "0";
                rblAdesaoPrograma_SelectedIndexChanged(null, null);
                txtDataAdesao.Text = obj.DataAdesaoPrograma.HasValue ? obj.DataAdesaoPrograma.Value.ToShortDateString() : String.Empty;
                if (obj.TransferenciaRendaPrevisaoAnual != null)
                {
                    txtMetaPactuadaExercicio0.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2021.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2021.Value.ToString() : String.Empty;
                    txtMetaPactuadaExercicio1.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2022.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2022.Value.ToString() : String.Empty;
                    txtMetaPactuadaExercicio2.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2023.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2023.Value.ToString() : String.Empty;
                    txtMetaPactuadaExercicio3.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2024.Value.ToString() : String.Empty;
                    txtMetaPactuadaExercicio4.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2025.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2025.Value.ToString() : String.Empty;

                    txtmediaMensalExercicio0.Text = obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2021.HasValue ? obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2021.Value.ToString() : String.Empty;
                    txtmediaMensalExercicio1.Text = obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2022.HasValue ? obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2022.Value.ToString() : String.Empty;
                    txtmediaMensalExercicio2.Text = obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2023.HasValue ? obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2023.Value.ToString() : String.Empty;
                    txtmediaMensalExercicio3.Text = obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2024.Value.ToString() : String.Empty;
                    txtmediaMensalExercicio4.Text = obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2025.HasValue ? obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2025.Value.ToString() : String.Empty;

                    txtPrevisaoAnualExercicio0.Text = obj.TransferenciaRendaPrevisaoAnual.CalculoAcaoRendaPrevisaoAnualSistemaAnterior.HasValue ? "R$ " + obj.TransferenciaRendaPrevisaoAnual.CalculoAcaoRendaPrevisaoAnualSistemaAnterior.Value.ToString("N2") : "R$ 0,00";
                    txtPrevisaoAnualExercicio1.Text = obj.TransferenciaRendaPrevisaoAnual.CalculoAcaoRendaPrevisaoAnualExercicio1.HasValue ? "R$ " + obj.TransferenciaRendaPrevisaoAnual.CalculoAcaoRendaPrevisaoAnualExercicio1.Value.ToString("N2") : "R$ 0,00";
                    txtPrevisaoAnualExercicio2.Text = obj.TransferenciaRendaPrevisaoAnual.CalculoAcaoRendaPrevisaoAnualExercicio2.HasValue ? "R$ " + obj.TransferenciaRendaPrevisaoAnual.CalculoAcaoRendaPrevisaoAnualExercicio2.Value.ToString("N2") : "R$ 0,00";
                    txtPrevisaoAnualExercicio3.Text = obj.TransferenciaRendaPrevisaoAnual.CalculoAcaoRendaPrevisaoAnualExercicio3.HasValue ? "R$ " + obj.TransferenciaRendaPrevisaoAnual.CalculoAcaoRendaPrevisaoAnualExercicio3.Value.ToString("N2") : "R$ 0,00";
                    txtPrevisaoAnualExercicio4.Text = obj.TransferenciaRendaPrevisaoAnual.CalculoAcaoRendaPrevisaoAnualExercicio4.HasValue ? "R$ " + obj.TransferenciaRendaPrevisaoAnual.CalculoAcaoRendaPrevisaoAnualExercicio4.Value.ToString("N2") : "R$ 0,00";
                }
            }

            if (obj.IdTipoTransferenciaRenda == 10 )
            {
                Session["idTipoTransferencia"] = 10; 
                rblAdesaoPrograma.SelectedValue = obj.ExecutaPrograma.HasValue && obj.ExecutaPrograma.Value ? "1" : "0";

                if (obj.ExecutaPrograma.HasValue && obj.ExecutaPrograma.Value == true)
                {
                    trFEASRecursosReprogramados.Visible = true;    
                }

                rblAdesaoPrograma_SelectedIndexChanged(null, null);
                txtDataAdesao.Text = obj.DataAdesaoPrograma.HasValue ? obj.DataAdesaoPrograma.Value.ToShortDateString() : String.Empty;
                

                if (obj.TransferenciaRendaPrevisaoAnual != null)
                {
                    txtMetaPactuada2021.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2021.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2021.Value.ToString() : String.Empty;
                    txtMetaPactuada2022.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2022.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2022.Value.ToString() : String.Empty;
                    txtMetaPactuada2023.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2023.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2023.Value.ToString() : String.Empty;
                    txtMetaPactuada2024.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2024.Value.ToString() : String.Empty;
                    txtMetaPactuada2025.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2025.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2025.Value.ToString() : String.Empty;

                    txtNumeroAtendidos2021.Text = obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2021.HasValue ? obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2021.Value.ToString() : String.Empty;
                    txtNumeroAtendidos2022.Text = obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2022.HasValue ? obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2022.Value.ToString() : String.Empty;
                    txtNumeroAtendidos2023.Text = obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2023.HasValue ? obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2023.Value.ToString() : String.Empty;
                    txtNumeroAtendidos2024.Text = obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2024.Value.ToString() : String.Empty;
                    txtNumeroAtendidos2025.Text = obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2025.HasValue ? obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2025.Value.ToString() : String.Empty;

                    txtValorRepasseEstadual2021.Text = obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2021.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2021.Value.ToString("N2") : "0,00";
                    txtValorRepasseEstadual2022.Text = obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2022.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2022.Value.ToString("N2") : "0,00";
                    txtValorRepasseEstadual2023.Text = obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2023.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2023.Value.ToString("N2") : "0,00";
                    txtValorRepasseEstadual2024.Text = obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2024.Value.ToString("N2") : "0,00";
                    txtValorRepasseEstadual2025.Text = obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2025.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2025.Value.ToString("N2") : "0,00";

                    txtFEASReprogramadoExercicio1.Text = obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2022.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2022.Value.ToString("N2") : "0,00";
                    txtFEASReprogramadoExercicio2.Text = obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2023.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2023.Value.ToString("N2") : "0,00";
                    txtFEASReprogramadoExercicio3.Text = obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2024.Value.ToString("N2") : "0,00";
                    txtFEASReprogramadoExercicio4.Text = obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2025.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2025.Value.ToString("N2") : "0,00";

                }
            }


            if (obj.IdTipoTransferenciaRenda == 13)
            {
                rblAdesaoPrograma.SelectedValue = obj.ExecutaPrograma.HasValue && obj.ExecutaPrograma.Value ? "1" : "0";

                Session["idTipoTransferencia"] = 13;
                Session["idTransferencia"] = obj.Id;
                trMetasAtendimento.Visible = trTecnicoReferencia.Visible = true;
                
                rblAdesaoPrograma_SelectedIndexChanged(null, null);
                txtDataAdesao.Text = obj.DataAdesaoPrograma.HasValue ? obj.DataAdesaoPrograma.Value.ToShortDateString() : String.Empty;

                
                if (obj.TecnicoReferencia != null)
                {

                    if (obj.NaoPossuiTecnicoReferencia.HasValue)
                    {
                        chkTecnicoReferencia.Checked = obj.NaoPossuiTecnicoReferencia.Value == true ? true : false;
                    }
                                       

                    if (obj.NaoPossuiTecnicoReferencia == null)
                    {
                        Tecnico = obj.TecnicoReferencia;
                        carregarTecnicoReferencia();
                    }

                    if (obj.NaoPossuiTecnicoReferencia == false)
                    {
                        Tecnico = obj.TecnicoReferencia;
                        carregarTecnicoReferencia();
                    }
                }

                if (obj.TransferenciaRendaPrevisaoAnual != null)
                {
                    txtAuxilioAluguelNumeroAtendidosExercicio3.Text = obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelNumeroAtendidasExercicio2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelNumeroAtendidasExercicio2024.Value.ToString() : String.Empty;
                    txtAuxilioAluguelAtivasExercicio3.Text = obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelAtivasExercicio2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelAtivasExercicio2024.Value.ToString() : String.Empty;
                    txtAuxilioAluguelRecebidasExercicio3.Text = obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelRecebidasExercicio2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelRecebidasExercicio2024.Value.ToString() : String.Empty;

                    txtAuxilioAluguelNumeroAtendidosExercicio4.Text = obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelNumeroAtendidasExercicio2025.HasValue ? obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelNumeroAtendidasExercicio2025.Value.ToString() : String.Empty;
                    txtAuxilioAluguelAtivasExercicio4.Text = obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelAtivasExercicio2025.HasValue ? obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelAtivasExercicio2025.Value.ToString() : String.Empty;
                    txtAuxilioAluguelRecebidasExercicio4.Text = obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelRecebidasExercicio2025.HasValue ? obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelRecebidasExercicio2025.Value.ToString() : String.Empty;
                }

            }


            if (obj.IdTipoTransferenciaRenda == 11)
            {
                trInterlocutorMunicipal.Visible = trArticulacoesPromovidas.Visible = trMetasAtendimento.Visible = true;
                trPP2021.Visible = false;
                trFEASRecursosReprogramados.Visible = true;

                Session["idTipoTransferencia"] = 11;

                if (obj.TransferenciaRendaPrevisaoAnual != null)
                {
                    txtMetaPactuada2021.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2021.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2021.Value.ToString() : String.Empty;
                    txtMetaPactuada2022.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2022.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2022.Value.ToString() : String.Empty;
                    txtMetaPactuada2023.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2023.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2023.Value.ToString() : String.Empty;
                    txtMetaPactuada2024.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2024.Value.ToString() : String.Empty;
                    txtMetaPactuada2025.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2025.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2025.Value.ToString() : String.Empty;

                    txtNumeroAtendidos2021.Text = obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2021.HasValue ? obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2021.Value.ToString() : String.Empty;
                    txtNumeroAtendidos2022.Text = obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2022.HasValue ? obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2022.Value.ToString() : String.Empty;
                    txtNumeroAtendidos2023.Text = obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2023.HasValue ? obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2023.Value.ToString() : String.Empty;
                    txtNumeroAtendidos2024.Text = obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2024.Value.ToString() : String.Empty;
                    txtNumeroAtendidos2025.Text = obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2025.HasValue ? obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2025.Value.ToString() : String.Empty;

                    txtValorRepasseEstadual2021.Text = obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2021.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2021.Value.ToString("N2") : "0,00";
                    txtValorRepasseEstadual2022.Text = obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2022.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2022.Value.ToString("N2") : "0,00";
                    txtValorRepasseEstadual2023.Text = obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2023.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2023.Value.ToString("N2") : "0,00";
                    txtValorRepasseEstadual2024.Text = obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2024.Value.ToString("N2") : "0,00";
                    txtValorRepasseEstadual2025.Text = obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2025.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2025.Value.ToString("N2") : "0,00";

                    txtFEASReprogramadoExercicio1.Text = obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2022.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2022.Value.ToString("N2") : "0,00";
                    txtFEASReprogramadoExercicio2.Text = obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2023.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2023.Value.ToString("N2") : "0,00";
                    txtFEASReprogramadoExercicio3.Text = obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2024.Value.ToString("N2") : "0,00";
                    txtFEASReprogramadoExercicio4.Text = obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2025.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2025.Value.ToString("N2") : "0,00";

                }
            }

            if (obj.IdTipoTransferenciaRenda == 12)
            {
                trInterlocutorMunicipal.Visible = trMetasAtendimento.Visible = true;

                Session["idTipoTransferencia"] = 12;

                if (obj.TransferenciaRendaPrevisaoAnual != null)
                {
                    txtValorRepasseEstadual2021.Text = obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2021.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2021.Value.ToString("N2") : "0,00";
                    txtValorRepasseEstadual2022.Text = obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2022.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2022.Value.ToString("N2") : "0,00";
                    txtValorRepasseEstadual2023.Text = obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2023.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2023.Value.ToString("N2") : "0,00";
                    txtValorRepasseEstadual2024.Text = obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2024.Value.ToString("N2") : "0,00";
                    txtValorRepasseEstadual2025.Text = obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2025.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2025.Value.ToString("N2") : "0,00";

                    txtFEASReprogramadoExercicio1.Text = obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2022.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2022.Value.ToString("N2") : "0,00";
                    txtFEASReprogramadoExercicio2.Text = obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2023.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2023.Value.ToString("N2") : "0,00";
                    txtFEASReprogramadoExercicio3.Text = obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2024.Value.ToString("N2") : "0,00";
                    txtFEASReprogramadoExercicio4.Text = obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2025.HasValue ? obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2025.Value.ToString("N2") : "0,00";

                }
            }

            if (obj.IdTipoTransferenciaRenda == Convert.ToInt32(ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso) || obj.IdTipoTransferenciaRenda == Convert.ToInt32(ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia))
            {
                Session["idTipoTransferencia"] = 1;

                if (obj.TransferenciaRendaPrevisaoAnual != null)
                {
                    lblBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio1.Text = obj.TransferenciaRendaPrevisaoAnual.CalculoBPCPrevisaoAnualExercicio1.HasValue ? "R$ " + obj.TransferenciaRendaPrevisaoAnual.CalculoBPCPrevisaoAnualExercicio1.Value.ToString("N2") : "R$ 0,00";
                    lblBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio2.Text = obj.TransferenciaRendaPrevisaoAnual.CalculoBPCPrevisaoAnualExercicio2.HasValue ? "R$ " + obj.TransferenciaRendaPrevisaoAnual.CalculoBPCPrevisaoAnualExercicio2.Value.ToString("N2") : "R$ 0,00";
                    lblBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio3.Text = obj.TransferenciaRendaPrevisaoAnual.CalculoBPCPrevisaoAnualExercicio3.HasValue ? "R$ " + obj.TransferenciaRendaPrevisaoAnual.CalculoBPCPrevisaoAnualExercicio3.Value.ToString("N2") : "R$ 0,00";
                    lblBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio4.Text = obj.TransferenciaRendaPrevisaoAnual.CalculoBPCPrevisaoAnualExercicio4.HasValue ? "R$ " + obj.TransferenciaRendaPrevisaoAnual.CalculoBPCPrevisaoAnualExercicio4.Value.ToString("N2") : "R$ 0,00";

                    txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio1.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2022.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2022.ToString() : "";
                    txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio2.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2023.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2023.ToString() : "";
                    txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio3.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2024.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2024.ToString() : "";
                    txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio4.Text = obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2025.HasValue ? obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2025.ToString() : "";
                }
            }

            lblNome.Text = obj.Nome;
            lblObjetivo.Text = obj.Objetivo;
            hdfTipoTransferenciaRenda.Value = obj.IdTipoTransferenciaRenda.ToString();
            ddlBeneficiarios.SelectedValue = obj.IdUsuarioTransferenciaRenda.ToString();

            if (obj.IdTipoTransferenciaRenda == 5 || obj.IdTipoTransferenciaRenda == 6 || obj.IdTipoTransferenciaRenda == 10 || obj.IdTipoTransferenciaRenda == 11 || obj.IdTipoTransferenciaRenda == 12 || obj.IdTipoTransferenciaRenda == 13)
            {
                lblBeneficiarios.Text = obj.UsuarioTransferenciaRenda.Nome.ToString();

                txtNomeTecnico.Text = !String.IsNullOrEmpty(obj.NomeTecnico) ? obj.NomeTecnico : "";

                Telefone.Text = !String.IsNullOrEmpty(obj.Telefone) ? obj.Telefone : "";

                Celular.Text = !String.IsNullOrEmpty(obj.Celular) ? obj.Celular : "";

                txtEmailInstitucional.Text = !String.IsNullOrEmpty(obj.Email) ? obj.Email : "";

                chkNaoHaTecnico.Checked = obj.NaoHaTecnico == null ? false : obj.NaoHaTecnico;

                if (obj.NaoHaTecnico != null)
                {
                   chkNaoHaTecnico_CheckedChanged(null, null);
                }
            }


            if (obj.BolsaFamiliaEstimativaFamilias.HasValue)
                txtBolsaFamiliaEstimativaFamiliasExercicio0.Text = obj.BolsaFamiliaEstimativaFamilias.ToString();

            if (obj.MunicipaisNumeroBeneficiarios.HasValue)
                txtProgramasMunicipaisNumeroBeneficiarios.Text = obj.MunicipaisNumeroBeneficiarios.ToString();

            if (obj.MunicipaisRepasse != null)
                txtProgramasMunicipaisValorRepasse.Text = obj.MunicipaisRepasse.Value.ToString("N2");

            if (obj.MunicipaisRepasseAnual.HasValue)
                lblProgramasMunicipaisAnualRepasse.Text = obj.MunicipaisRepasseAnual.Value.ToString("N2");

            rblParcerias.SelectedValue = Convert.ToSByte(obj.PossuiParceriaFormal).ToString();
            tbParcerias.Visible = obj.PossuiParceriaFormal;
            if (obj.PossuiParceriaFormal)
            {
                Parcerias = obj.Parcerias;
                carregarParcerias();
            }

            txtFMAS.Text = obj.ValorFMAS.HasValue ? obj.ValorFMAS.Value.ToString("N2") : "";
            txtOrcamentoMunicipal.Text = obj.ValorOrcamentoMunicipal.HasValue ? obj.ValorOrcamentoMunicipal.Value.ToString("N2") : "";
            txtOutrosFundosMunicipais.Text = obj.ValorFundoMunicipal.HasValue ? obj.ValorFundoMunicipal.Value.ToString("N2") : "";
            txtFEAS.Text = obj.ValorFEAS.HasValue ? obj.ValorFEAS.Value.ToString("N2") : "";
            txtOrcamentoEstadual.Text = obj.ValorOrcamentoEstadual.HasValue ? obj.ValorOrcamentoEstadual.Value.ToString("N2") : "";
            txtOutrosFundosEstaduais.Text = obj.ValorFundoEstadual.HasValue ? obj.ValorFundoEstadual.Value.ToString("N2") : "";
            txtFNAS.Text = obj.ValorFNAS.HasValue ? obj.ValorFNAS.Value.ToString("N2") : "";
            txtOrcamentoFederal.Text = obj.ValorOrcamentoFederal.HasValue ? obj.ValorOrcamentoFederal.Value.ToString("N2") : "";
            txtOutrosFundosFederais.Text = obj.ValorFundoFederal.HasValue ? obj.ValorFundoFederal.Value.ToString("N2") : "";
            txtIGDPBF.Text = obj.ValorIGDPBF.HasValue ? obj.ValorIGDPBF.Value.ToString("N2") : "";
            txtIGDSUAS.Text = obj.ValorIGDSUAS.HasValue ? obj.ValorIGDSUAS.Value.ToString("N2") : "";

            preencheTitulo((ETipoTransferenciaRenda)obj.IdTipoTransferenciaRenda);

            int idQuadro = 0;
            switch ((ETipoTransferenciaRenda)obj.IdTipoTransferenciaRenda)
            {
                case ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso:
                    idQuadro = 58;
                    break;
                case ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia:
                    idQuadro = 59;
                    break;
                case ETipoTransferenciaRenda.BolsaFamilia:
                    idQuadro = 44;
                    break;
                case ETipoTransferenciaRenda.PETIProgramaErradicacaoTrabalhoInfantil:
                    idQuadro = 45;
                    break;
                case ETipoTransferenciaRenda.AcaoJovem:
                    idQuadro = 47;
                    break;
                case ETipoTransferenciaRenda.RendaCidada:
                    idQuadro = 48;
                    break;
                case ETipoTransferenciaRenda.Outros:
                    idQuadro = 51;
                    break;
            }

            verificarAlteracoes(obj.Id, idQuadro);

        }


        void selecionaReprogramacao(int exercicio)
        {
            if (exercicio == Exercicios[0])
            {
                trRecursosFinanceirosEstadualReprogramadoExercicio1.Visible = true;
                trRecursosFinanceirosEstadualReprogramadoExercicio2.Visible = false;
                trRecursosFinanceirosEstadualReprogramadoExercicio3.Visible = false;
                trRecursosFinanceirosEstadualReprogramadoExercicio4.Visible = false;
                hdfAno.Value = btnExercicio1.Text;
                SelecionarCorAba();
            }

            if (exercicio == Exercicios[1])
            {
                trRecursosFinanceirosEstadualReprogramadoExercicio1.Visible = false;
                trRecursosFinanceirosEstadualReprogramadoExercicio2.Visible = true;
                trRecursosFinanceirosEstadualReprogramadoExercicio3.Visible = false;
                trRecursosFinanceirosEstadualReprogramadoExercicio4.Visible = false;
                hdfAno.Value = btnExercicio2.Text;
                SelecionarCorAba();
            }

            if (exercicio == Exercicios[2])
            {
                trRecursosFinanceirosEstadualReprogramadoExercicio1.Visible = false;
                trRecursosFinanceirosEstadualReprogramadoExercicio2.Visible = false;
                trRecursosFinanceirosEstadualReprogramadoExercicio3.Visible = true;
                trRecursosFinanceirosEstadualReprogramadoExercicio4.Visible = false;
                hdfAno.Value = btnExercicio3.Text;
                SelecionarCorAba();
            }

            if (exercicio == Exercicios[3])
            {
                trRecursosFinanceirosEstadualReprogramadoExercicio1.Visible = false;
                trRecursosFinanceirosEstadualReprogramadoExercicio2.Visible = false;
                trRecursosFinanceirosEstadualReprogramadoExercicio3.Visible = false;
                trRecursosFinanceirosEstadualReprogramadoExercicio4.Visible = true;
                hdfAno.Value = btnExercicio4.Text;
                SelecionarCorAba();
            }
        }

        private void CarregarExercicios()
        {
            this.btnExercicio1.Text = Exercicios[0].ToString();
            this.btnExercicio2.Text = Exercicios[1].ToString();
            this.btnExercicio3.Text = Exercicios[2].ToString();
            this.btnExercicio4.Text = Exercicios[3].ToString();
            this.SelecionarCorAba();
        }

        private void SelecionarCorAba()
        {
            if (Exercicios[0] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";

            }

            if (Exercicios[1] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (Exercicios[2] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-info-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-primary-seds";
            }

            if (Exercicios[3] == Convert.ToInt32(this.hdfAno.Value))
            {
                this.btnExercicio1.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio2.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio3.CssClass = "btn-seds btn-primary-seds";
                this.btnExercicio4.CssClass = "btn-seds btn-info-seds";
            }
        }


        void verificarAlteracoes(Int32 idTransferenciaRenda, Int32 idQuadro)
        {
            if (Util.VerificarAlteracoes())
            {
                using (var proxy = new ProxyPlanoMunicipal())
                {
                    linkAlteracoesQuadro.Visible = proxy.Service.ExisteAlteracoesNoPlanoMunicipalByQuadroCadastro(SessaoPmas.UsuarioLogado.Prefeitura.Id, idQuadro, idTransferenciaRenda);
                    linkAlteracoesQuadro.HRef = "../HistoricoAlteracoes.aspx?idQuadro=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idQuadro.ToString())) + "&idForeignKey=" + Server.UrlEncode(Genericos.clsCrypto.Encrypt(idTransferenciaRenda.ToString()));
                }
            }
        }

       
        void carregarTecnicoReferencia() 
        {
            trListTecnicoReferencia.Visible = true;
            lstTecnicoReferencia.DataSource = Tecnico;
            lstTecnicoReferencia.DataBind();
        }
        

        void carregarParcerias()
        {
            lstParcerias.DataSource = Parcerias;
            lstParcerias.DataBind();
        }

        void carregarEstruturas()
        {
            using (var proxy = new ProxyEstruturaAssistenciaSocial())
            {
                ddlParceria.DataValueField = "Id";
                ddlParceria.DataTextField = "Nome";
                ddlParceria.DataSource = proxy.Service.GetParcerias();
                ddlParceria.DataBind();
                Util.InserirItemEscolha(ddlParceria);

                ddlTipoParceria.DataValueField = "Id";
                ddlTipoParceria.DataTextField = "Nome";
                ddlTipoParceria.DataSource = proxy.Service.GetTiposParceria();
                ddlTipoParceria.DataBind();
                Util.InserirItemEscolha(ddlTipoParceria);

                ddlBeneficiarios.DataValueField = "Id";
                ddlBeneficiarios.DataTextField = "Nome";
                ddlBeneficiarios.DataSource = proxy.Service.GetUsuarioTransferenciaRenda();
                ddlBeneficiarios.DataBind();
                Util.InserirItemEscolha(ddlBeneficiarios);
            }
        }

        private void preencheTitulo(ETipoTransferenciaRenda _tipoTransferenciaRenda)
        {
            try
            {
                int numeracao = 0;
                switch (_tipoTransferenciaRenda)
                {
                    case ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso:
                        lblTitulo.Text = "Benefício de Prestação Continuada - BPC Idosos";
                        lblNumeracao.Text = "3.30";
                        //numeracao = 47;
                        //numeracao = 21;
                        ddlBeneficiarios.Enabled = false;
                        lblTituloMetaPactuada.Text = "Meta pactuada";
                        txtNome.Visible = false;
                        txtObjetivo.Visible = false;
                        //lblProtocoloGestao.Visible = true;
                        lblNomePrograma.Visible = false;
                        trArticulacoesPromovidas.Visible = trMetasAtendimento.Visible = true;
                        lblQuadro.Text = "Previsão anual de número de beneficiários e valor de repasse";
                        txtValorAEPETI2.Visible = false;
                        trAEPETI2.Visible = false;
                        txtValorAEPETI.Visible = false;
                        trAEPETI.Visible = false;
                        break;
                    case ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia:
                        lblTitulo.Text = "Benefício de Prestação Continuada - BPC Pessoas com Deficiência";
                        lblTituloMetaPactuada.Text = "Meta pactuada";
                        ddlBeneficiarios.Enabled = false;
                        txtNome.Enabled = false;
                        txtObjetivo.Enabled = false;
                        lblNumeracao.Text = "3.31";
                        // numeracao = 48;
                        txtNome.Visible = false;
                        txtObjetivo.Visible = false;
                        // numeracao = 22;
                        //lblProtocoloGestao.Visible = true;
                        //trBPCEscola.Visible = true;
                        trArticulacoesPromovidas.Visible = trMetasAtendimento.Visible = true;
                        lblQuadro.Text = "Previsão anual de número de beneficiários e valor de repasse";
                        lblNomePrograma.Visible = false;
                        txtValorAEPETI2.Visible = false;
                        trAEPETI2.Visible = false;
                        txtValorAEPETI.Visible = false;
                        trAEPETI.Visible = false;
                        break;
                    case ETipoTransferenciaRenda.BolsaFamilia:
                        lblTitulo.Text = "Bolsa Família"; ;
                        ddlBeneficiarios.Enabled = false;
                        txtNome.Enabled = false;
                        txtObjetivo.Enabled = false;
                        lblNumeracao.Text = "3.18";
                        numeracao = 28;
                        txtValorAEPETI2.Visible = false;
                        trAEPETI2.Visible = false;
                        txtValorAEPETI.Visible = false;
                        trAEPETI.Visible = false;
                        //numeracao = 3;
                        //lblProtocoloGestao.Visible = true;
                        break;
                    case ETipoTransferenciaRenda.PETIProgramaErradicacaoTrabalhoInfantil:
                        lblTitulo.Text = "PETI - Programa de Erradicação Trabalho Infantil";
                        lblQuadro.Text = "Previsão do número de beneficiários"; //PMAS 2016
                        numeracao = 29;
                        lblNumeracao.Text = "3.19";
                        //numeracao = 4;
                        ddlBeneficiarios.Enabled = false;
                        txtNome.Visible = false;
                        txtObjetivo.Visible = false;
                        trRecursosFinanceiros.Visible = trAdesaoPrograma.Visible = false;
                        trAderiuCofinanciamentoPeti.Visible = true;
                        //  lblProtocoloGestao.Visible = true;
                        break;
                    case ETipoTransferenciaRenda.AcaoJovem:
                        lblTitulo.Text = "Ação Jovem";
                        lblNumeracao.Text = "3.21";
                        numeracao = 36;
                        lblTituloMetaPactuada.Text = "Demanda estimada (jovens)";
                        //numeracao = 10;
                        ddlBeneficiarios.Enabled = false;
                        txtNome.Enabled = false;
                        txtObjetivo.Enabled = false;
                        trAdesaoPrograma.Visible = true;
                        lblPerguntaAdesao.Text = "O município aderiu ao Programa Ação Jovem?";
                        lblDescAdesao.Text = "Data de assinatura do Termo de Adesão ao Programa Ação Jovem: ";
                        txtValorAEPETI2.Visible = false;
                        trAEPETI2.Visible = false;
                        txtValorAEPETI.Visible = false;
                        trAEPETI.Visible = false;
                        //  lblProtocoloGestaoAcaoRenda.Visible = true;
                        break;
                    case ETipoTransferenciaRenda.RendaCidada:
                        lblTitulo.Text = "Renda Cidadã";
                        lblNumeracao.Text = "3.22";
                        numeracao = 37;
                        lblTituloMetaPactuada.Text = "Demanda estimada (famílias)";
                        ddlBeneficiarios.Enabled = false;
                        txtNome.Enabled = false;
                        txtObjetivo.Enabled = false;
                        trAdesaoPrograma.Visible = true;
                        lblPerguntaAdesao.Text = "O município aderiu ao Programa Renda Cidadã?";
                        lblDescAdesao.Text = "Data de assinatura do Termo de Adesão ao Programa Renda Cidadã: ";
                        txtValorAEPETI2.Visible = false;
                        trAEPETI2.Visible = false;
                        txtValorAEPETI.Visible = false;
                        trAEPETI.Visible = false;
                        break;
                    case ETipoTransferenciaRenda.ProsperaFamilia:
                        lblTitulo.Text = "Prospera Família";
                        lblNumeracao.Text = "3.24";
                        numeracao = 38;
                        lblTituloMetaPactuada.Text = "Demanda estimada";
                        ddlBeneficiarios.Enabled = false;
                        txtNome.Enabled = false;
                        txtObjetivo.Enabled = false;
                        trAdesaoPrograma.Visible = true;
                        lblPerguntaAdesao.Text = "O município aderiu ao Programa Prospera Família?";
                        lblDescAdesao.Text = "Data de assinatura do Termo de Adesão ao Programa Prospera Família: ";
                        divProsperaFamilia.Visible = true;
                        txtValorAEPETI2.Visible = false;
                        trAEPETI2.Visible = false;
                        txtValorAEPETI.Visible = false;
                        trAEPETI.Visible = false;
                        break;
                    case ETipoTransferenciaRenda.AuxilioAluguel:
                        lblTitulo.Text = "Auxílio Aluguel";
                        lblNumeracao.Text = "3.27";
                        numeracao = 38;
                        lblTituloMetaPactuada.Text = "Demanda estimada";
                        ddlBeneficiarios.Enabled = false;
                        txtNome.Enabled = false;
                        txtObjetivo.Enabled = false;
                        trAdesaoPrograma.Visible = true;
                        lblPerguntaAdesao.Text = "O município aderiu ao Programa Auxílio Aluguel ?";
                        lblDescAdesao.Text = "Data de assinatura do Termo de Adesão ao Programa Auxílio Aluguel: ";
                        divAuxilioAluguel.Visible = true;
                        txtValorAEPETI2.Visible = false;
                        trAEPETI2.Visible = false;
                        txtValorAEPETI.Visible = false;
                        trAEPETI.Visible = false;
                        break;
                    case ETipoTransferenciaRenda.FCadUnico:
                        lblTitulo.Text = "Fortalecimento CadÚnico";
                        lblNumeracao.Text = "3.25";
                        numeracao = 39;
                        lblTituloMetaPactuada.Text = "Demanda estimada";
                     
                        ddlBeneficiarios.Enabled = false;
                        txtNome.Enabled = false;
                        txtObjetivo.Enabled = false;
                        trAdesaoPrograma.Visible = false;
                        divProsperaFamilia.Visible = true;
                        txtValorAEPETI2.Visible = false;
                        trAEPETI2.Visible = false;
                        txtValorAEPETI.Visible = false;
                        trAEPETI.Visible = false;
                        break;
                    case ETipoTransferenciaRenda.FVigilancia:
                        lblTitulo.Text = "Fortalecimento Da Vigilância Socioassistencial";
                        lblNumeracao.Text = "3.26";
                        numeracao = 39;
                        divProsperaFamilia.Visible = true;
                        txtValorAEPETI2.Visible = false;
                        trAEPETI2.Visible = false;
                        txtValorAEPETI.Visible = false;
                        trAEPETI.Visible = false;
                        break;
                    case ETipoTransferenciaRenda.Outros:
                        trNomePrograma.Visible = true;
                        lblNumeracao.Text = "3.23";
                        numeracao = 40;
                        ddlBeneficiarios.Enabled = true;
                        txtNome.Enabled = true;
                        txtObjetivo.Enabled = true;
                        txtValorAEPETI2.Visible = false;
                        trAEPETI2.Visible = false;
                        txtValorAEPETI.Visible = false;
                        trAEPETI.Visible = false;
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void verificaTecnicoReferencia() 
        {
            var erros = new List<string>();


            if (!chkTecnicoReferencia.Checked)
            {
                if (!String.IsNullOrEmpty(txtNomeTecnicoReferencia.Text) || !String.IsNullOrEmpty(txtEmailTecnicoReferencia.Text) || !String.IsNullOrEmpty(txtUnidadeLotacao.Text))
                    erros.Add("Favor adicionar tecnico de referencia ou selecionar a opção não há técnico de referencia.");
            }

            if (erros.Count > 0)
                throw new Exception(Extensions.Concat(erros, System.Environment.NewLine));            
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int exercicio = String.IsNullOrEmpty(hdfAno.Value) ? 2022 : Convert.ToInt32(hdfAno.Value);
            SessaoPmas.VerificarSessao(this);

            var transferenciaRenda = new TransferenciaRendaInfo();

            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                transferenciaRenda.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"]));
            }

            transferenciaRenda.IdPrefeitura = SessaoPmas.UsuarioLogado.Prefeitura.Id;
            transferenciaRenda.IdUsuarioTransferenciaRenda = Convert.ToInt32(ddlBeneficiarios.SelectedValue);
            transferenciaRenda.IdTipoTransferenciaRenda = Convert.ToInt32(hdfTipoTransferenciaRenda.Value);
            transferenciaRenda.Nome = lblNome.Text;
            transferenciaRenda.Objetivo = lblObjetivo.Text;
            transferenciaRenda.PossuiParceriaFormal = rblParcerias.SelectedValue == "1";

            if (transferenciaRenda.IdTipoTransferenciaRenda == 5 || transferenciaRenda.IdTipoTransferenciaRenda == 6 || transferenciaRenda.IdTipoTransferenciaRenda == 10 || transferenciaRenda.IdTipoTransferenciaRenda == 11 || transferenciaRenda.IdTipoTransferenciaRenda == 12)
            {
                if (!String.IsNullOrEmpty(txtNomeTecnico.Text))
                {
                    transferenciaRenda.NomeTecnico = txtNomeTecnico.Text;
                }
                else
                {
                    transferenciaRenda.NaoHaTecnico = true;
                }

                if (!String.IsNullOrEmpty(Telefone.Text.Trim()))
                {
                    transferenciaRenda.Telefone = Telefone.Text.Trim();
                }

                if (!String.IsNullOrEmpty(Celular.Text.Trim()))
                {
                    transferenciaRenda.Celular = Celular.Text.Trim();
                }

                if (!String.IsNullOrEmpty(txtEmailInstitucional.Text))
                {
                    transferenciaRenda.Email = txtEmailInstitucional.Text;
                }

                if (!String.IsNullOrEmpty(txtBolsaFamiliaEstimativaFamiliasExercicio0.Text))
                {
                    transferenciaRenda.BolsaFamiliaEstimativaFamilias = Convert.ToInt32(txtBolsaFamiliaEstimativaFamiliasExercicio0.Text);
                }

                if (chkNaoHaTecnico.Checked)
                {
                    transferenciaRenda.NaoHaTecnico = true;
                }
                else
                {
                    transferenciaRenda.NaoHaTecnico = false;
                }
            }
            else
            {
                transferenciaRenda.NomeTecnico = String.Empty;
                transferenciaRenda.Telefone = String.Empty;
                transferenciaRenda.Celular = String.Empty;
                transferenciaRenda.Email = String.Empty;
                transferenciaRenda.NaoHaTecnico = false;
            }



            if (transferenciaRenda.IdTipoTransferenciaRenda == (Int32)ETipoTransferenciaRenda.RendaCidada || transferenciaRenda.IdTipoTransferenciaRenda == (Int32)ETipoTransferenciaRenda.AcaoJovem || transferenciaRenda.IdTipoTransferenciaRenda == (Int32)ETipoTransferenciaRenda.ProsperaFamilia || transferenciaRenda.IdTipoTransferenciaRenda == (Int32)ETipoTransferenciaRenda.AuxilioAluguel)
            {
                transferenciaRenda.ExecutaPrograma = rblAdesaoPrograma.SelectedValue == "1";
                
                if (!String.IsNullOrEmpty(txtDataAdesao.Text))
                {
                    transferenciaRenda.DataAdesaoPrograma = Convert.ToDateTime(txtDataAdesao.Text);
                }
            }

            if (transferenciaRenda.IdTipoTransferenciaRenda == (Int32)ETipoTransferenciaRenda.FCadUnico)
            {
                transferenciaRenda.ExecutaPrograma = ExecutaProgramaFortalecimentoCad();

                if (ExecutaProgramaFortalecimentoCad())
                {
                    if (!transferenciaRenda.DataAdesaoPrograma.HasValue)
                    {
                        transferenciaRenda.DataAdesaoPrograma = DateTime.Now;    
                    }
                }
                else
                {
                    transferenciaRenda.DataAdesaoPrograma = null;
                }

            }

            if (transferenciaRenda.IdTipoTransferenciaRenda == (Int32)ETipoTransferenciaRenda.FVigilancia)
            {
                transferenciaRenda.ExecutaPrograma = ExecutaProgramaFortalecimentoVigilancia();

                if (ExecutaProgramaFortalecimentoVigilancia())
                {
                    if (!transferenciaRenda.DataAdesaoPrograma.HasValue)
                    {
                        transferenciaRenda.DataAdesaoPrograma = DateTime.Now;
                    }
                }
                else
                {
                    transferenciaRenda.DataAdesaoPrograma = null;
                }
            }

            PreencherPrevisaoAnual(transferenciaRenda);

            if (!String.IsNullOrEmpty(txtProgramasMunicipaisNumeroBeneficiarios.Text))
            {
                transferenciaRenda.MunicipaisNumeroBeneficiarios = Convert.ToInt32(txtProgramasMunicipaisNumeroBeneficiarios.Text);
            }
            if (!String.IsNullOrEmpty(txtProgramasMunicipaisValorRepasse.Text))
            {
                transferenciaRenda.MunicipaisRepasse = Convert.ToDecimal(txtProgramasMunicipaisValorRepasse.Text);
            }

            if (transferenciaRenda.PossuiParceriaFormal)
            {
                transferenciaRenda.Parcerias = Parcerias;
            }

            if (transferenciaRenda.IdTipoTransferenciaRenda == 13)
            {

                if (chkTecnicoReferencia.Checked)
                {
                    transferenciaRenda.NaoPossuiTecnicoReferencia = true;
                }
                else
                {
                    transferenciaRenda.NaoPossuiTecnicoReferencia = false;
                }
                
                

                transferenciaRenda.TecnicoReferencia = Tecnico;

            }
            else
            {
                txtAuxilioAluguelNumeroAtendidosExercicio3.Text = String.Empty;
                txtAuxilioAluguelAtivasExercicio3.Text = String.Empty;
                txtAuxilioAluguelRecebidasExercicio3.Text = String.Empty;

                txtAuxilioAluguelNumeroAtendidosExercicio4.Text = String.Empty;
                txtAuxilioAluguelAtivasExercicio4.Text = String.Empty;
                txtAuxilioAluguelRecebidasExercicio4.Text = String.Empty;


            }


            if (transferenciaRenda.IdTipoTransferenciaRenda == (Int32)ETipoTransferenciaRenda.PETIProgramaErradicacaoTrabalhoInfantil)
            {
                try
                {
                    PreencherPETI(transferenciaRenda);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message), true);
                    lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                    tbInconsistencias.Visible = true;
                    return;
                }
            }

            transferenciaRenda.ValorFMAS = !String.IsNullOrWhiteSpace(txtFMAS.Text) ? Convert.ToDecimal(txtFMAS.Text) : 0M;
            transferenciaRenda.ValorOrcamentoMunicipal = !String.IsNullOrWhiteSpace(txtOrcamentoMunicipal.Text) ? Convert.ToDecimal(txtOrcamentoMunicipal.Text) : 0M;
            transferenciaRenda.ValorFundoMunicipal = !String.IsNullOrWhiteSpace(txtOutrosFundosMunicipais.Text) ? Convert.ToDecimal(txtOutrosFundosMunicipais.Text) : 0M;
            transferenciaRenda.ValorFEAS = !String.IsNullOrWhiteSpace(txtFEAS.Text) ? Convert.ToDecimal(txtFEAS.Text) : 0M;
            transferenciaRenda.ValorOrcamentoEstadual = !String.IsNullOrWhiteSpace(txtOrcamentoEstadual.Text) ? Convert.ToDecimal(txtOrcamentoEstadual.Text) : 0M;
            transferenciaRenda.ValorFundoEstadual = !String.IsNullOrWhiteSpace(txtOutrosFundosEstaduais.Text) ? Convert.ToDecimal(txtOutrosFundosEstaduais.Text) : 0M;
            transferenciaRenda.ValorFNAS = !String.IsNullOrWhiteSpace(txtFNAS.Text) ? Convert.ToDecimal(txtFNAS.Text) : 0M;
            transferenciaRenda.ValorOrcamentoFederal = !String.IsNullOrWhiteSpace(txtOrcamentoFederal.Text) ? Convert.ToDecimal(txtOrcamentoFederal.Text) : 0M;
            transferenciaRenda.ValorFundoFederal = !String.IsNullOrWhiteSpace(txtOutrosFundosFederais.Text) ? Convert.ToDecimal(txtOutrosFundosFederais.Text) : 0M;
            transferenciaRenda.ValorIGDPBF = !String.IsNullOrWhiteSpace(txtIGDPBF.Text) ? Convert.ToDecimal(txtIGDPBF.Text) : 0M;
            transferenciaRenda.ValorIGDSUAS = !String.IsNullOrWhiteSpace(txtIGDSUAS.Text) ? Convert.ToDecimal(txtIGDSUAS.Text) : 0M;

            if (!String.IsNullOrEmpty(txtValorAEPETI.Text))
            {
                transferenciaRenda.ValorAEPETI = Convert.ToDecimal(txtValorAEPETI.Text);    
            }

            if (!String.IsNullOrEmpty(txtValorAEPETI2.Text))
            {
                transferenciaRenda.ValorAEPETI2 = Convert.ToDecimal(txtValorAEPETI2.Text);    
            }

           
            String action = "TI";
            try
            {
            
                if(transferenciaRenda.IdTipoTransferenciaRenda == 13)
                    verificaTecnicoReferencia();
                new ValidadorTransferenciaRenda().Validar(transferenciaRenda);

                using (var proxy = new ProxyProgramas())
                {
                    if (transferenciaRenda.Id == 0)
                    {
                        proxy.Service.AddTransferenciaRenda(transferenciaRenda);
                    }
                    else
                    {
                        action = "TU";
                        proxy.Service.UpdateTransferenciaRenda(transferenciaRenda);

                        if (transferenciaRenda.PetiIndicadores != null)
                        {
                            proxy.Service.UpdatePETIIndicadores(transferenciaRenda.PetiIndicadores);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            if (((ETipoTransferenciaRenda)transferenciaRenda.IdTipoTransferenciaRenda) == ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso || ((ETipoTransferenciaRenda)transferenciaRenda.IdTipoTransferenciaRenda) == ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia)
            {
                Response.Redirect("~/BlocoIII/CBeneficiosContinuados.aspx?msg=" + action);
                return;
            }

            Response.Redirect("~/BlocoIII/CProgramasProjetos.aspx?msg=" + action);
        }




        private void PreencherPETI(TransferenciaRendaInfo obj)
        {
            obj.PETIAderiuCofinanciamentoFederal = rblAderiuCofinanciamentoPeti.SelectedValue == "1" ? true : false;

            try
            {
                if (obj.PETIAderiuCofinanciamentoFederal.Value)
                {

                    ValidadorPETIIndicadores Valiar = new ValidadorPETIIndicadores();

                    obj.PETIDataAdesao = obj.PETIAderiuCofinanciamentoFederal.Value ?
                        (!String.IsNullOrWhiteSpace(txtPetiDataAdesao.Text) ? Convert.ToDateTime(txtPetiDataAdesao.Text) : new Nullable<DateTime>()) : new Nullable<DateTime>();

                    obj.PETIAcoesTrabalhoInfantil = rblAcoesPeti.SelectedValue == "1" ? true : false;

                    if (obj.PETIAcoesTrabalhoInfantil.Value)
                        obj.AcoesPETI = AcoesPETI;

                    obj.PetiIndicadores = new PETIIndicadoresInfo();
                    obj.PetiIndicadores.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(hdfIdPETI.Value));
                    obj.PetiIndicadores.IdMunicipio = Convert.ToInt32(Genericos.clsCrypto.Decrypt(hdfMunicipio.Value));
                    if (!String.IsNullOrEmpty(txtIdade1013AnosExercicio0.Text))
                        obj.PetiIndicadores.Idade1013Ano2021 = Convert.ToInt32(txtIdade1013AnosExercicio0.Text);
                    if (!String.IsNullOrEmpty(txtIdade1415AnosExercicio0.Text))
                        obj.PetiIndicadores.Idade1415Ano2021 = Convert.ToInt32(txtIdade1415AnosExercicio0.Text);
                    if (!String.IsNullOrEmpty(txtIdade1617AnosExercicio0.Text))
                        obj.PetiIndicadores.Idade1617Ano2021 = Convert.ToInt32(txtIdade1617AnosExercicio0.Text);

                    if (!String.IsNullOrEmpty(txtIdade1013AnosExercicio1.Text))
                        obj.PetiIndicadores.Idade1013Ano2022 = Convert.ToInt32(txtIdade1013AnosExercicio1.Text);
                    if (!String.IsNullOrEmpty(txtIdade1415AnosExercicio1.Text))
                        obj.PetiIndicadores.Idade1415Ano2022 = Convert.ToInt32(txtIdade1415AnosExercicio1.Text);
                    if (!String.IsNullOrEmpty(txtIdade1617AnosExercicio1.Text))
                        obj.PetiIndicadores.Idade1617Ano2022 = Convert.ToInt32(txtIdade1617AnosExercicio1.Text);


                    if (!String.IsNullOrEmpty(txtIdade1013AnosExercicio2.Text))
                        obj.PetiIndicadores.Idade1013Ano2023 = Convert.ToInt32(txtIdade1013AnosExercicio2.Text);
                    if (!String.IsNullOrEmpty(txtIdade1415AnosExercicio2.Text))
                        obj.PetiIndicadores.Idade1415Ano2023 = Convert.ToInt32(txtIdade1415AnosExercicio2.Text);
                    if (!String.IsNullOrEmpty(txtIdade1617AnosExercicio2.Text))
                        obj.PetiIndicadores.Idade1617Ano2023 = Convert.ToInt32(txtIdade1617AnosExercicio2.Text);


                    if (!String.IsNullOrEmpty(txtIdade1013AnosExercicio3.Text))
                        obj.PetiIndicadores.Idade1013Ano2024 = Convert.ToInt32(txtIdade1013AnosExercicio3.Text);
                    if (!String.IsNullOrEmpty(txtIdade1415AnosExercicio3.Text))
                        obj.PetiIndicadores.Idade1415Ano2024 = Convert.ToInt32(txtIdade1415AnosExercicio3.Text);
                    if (!String.IsNullOrEmpty(txtIdade1617AnosExercicio3.Text))
                        obj.PetiIndicadores.Idade1617Ano2024 = Convert.ToInt32(txtIdade1617AnosExercicio3.Text);
                                           


                    if (!String.IsNullOrEmpty(txtMetaMunicipalExercicio0.Text))
                        obj.PetiIndicadores.MetaMunicipal2021 = Convert.ToInt32(txtMetaMunicipalExercicio0.Text);

                    if (!String.IsNullOrEmpty(txtMetaMunicipalExercicio1.Text))
                        obj.PetiIndicadores.MetaMunicipal2022 = Convert.ToInt32(txtMetaMunicipalExercicio1.Text);

                    if (!String.IsNullOrEmpty(txtMetaMunicipalExercicio2.Text))
                        obj.PetiIndicadores.MetaMunicipal2023 = Convert.ToInt32(txtMetaMunicipalExercicio2.Text);

                    if (!String.IsNullOrEmpty(txtMetaMunicipalExercicio3.Text))
                        obj.PetiIndicadores.MetaMunicipal2024 = Convert.ToInt32(txtMetaMunicipalExercicio3.Text);

                    obj.NaoPossuiTecnicoAcao = chkNaoPossuiGestorPETI.Checked;

                    obj.GestorAcao = new TransferenciaRendaGestorAcaoInfo();
                    obj.GestorAcao.Id = Convert.ToInt32(Genericos.clsCrypto.Decrypt(hdfIdGestorAcao.Value));
                    obj.GestorAcao.IdTransferenciaRenda = obj.Id;

                    if (!String.IsNullOrEmpty(txtNomeGestorPETI.Text))
                        obj.GestorAcao.Nome = txtNomeGestorPETI.Text;
                    if (!String.IsNullOrEmpty(txtTelefone.Text))
                        obj.GestorAcao.Telefone = txtTelefone.Text;
                    if (!String.IsNullOrEmpty(txtCelular.Text))
                        obj.GestorAcao.Celular = txtCelular.Text;
                    if (!String.IsNullOrEmpty(txtEmailGestorPETI.Text))
                        obj.GestorAcao.Email = txtEmailGestorPETI.Text;


                    Valiar.Validar(obj.PetiIndicadores);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PreencherPrevisaoAnual(TransferenciaRendaInfo obj)
        {
            ProxyProgramas proxy = new ProxyProgramas();

            obj.TransferenciaRendaPrevisaoAnual = new TransferenciaRendaPrevisaoAnualInfo();
            obj.TransferenciaRendaPrevisaoAnual.IdTransferenciaRenda = obj.Id;
            obj.TransferenciaRendaPrevisaoAnual.IdPrefeitura = obj.IdPrefeitura;
            
            
            var obj3 = proxy.Service.GetTransferenciaRendaById(Convert.ToInt32(Genericos.clsCrypto.Decrypt(Request.QueryString["id"])));

            if (obj.IdTipoTransferenciaRenda == Convert.ToInt32(ETipoTransferenciaRenda.BolsaFamilia))
            {

                if (!String.IsNullOrEmpty(txtBolsaFamiliaEstimativaFamiliasExercicio0.Text))
                    obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2021 = Convert.ToInt32(txtBolsaFamiliaEstimativaFamiliasExercicio0.Text);
                if (!String.IsNullOrEmpty(txtBolsaFamiliaCadastradasExercicio0.Text))
                    obj.TransferenciaRendaPrevisaoAnual.FamiliasCadastradas2021 = Convert.ToInt32(txtBolsaFamiliaCadastradasExercicio0.Text);
                if (!String.IsNullOrEmpty(txtBolsaFamiliaFamiliasBeneficiariasExercicio0.Text))
                    obj.TransferenciaRendaPrevisaoAnual.NumeroFamiliasBeneficiarias2021 = Convert.ToInt32(txtBolsaFamiliaFamiliasBeneficiariasExercicio0.Text);
                if (!String.IsNullOrEmpty(txtBolsaFamiliaRepasseMensalExercicio0.Text))
                    obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2021 = Convert.ToDecimal(txtBolsaFamiliaRepasseMensalExercicio0.Text);

                if (!String.IsNullOrEmpty(txtBolsaFamiliaEstimativaFamiliasExercicio1.Text))
                {
                    obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2022 = Convert.ToInt32(txtBolsaFamiliaEstimativaFamiliasExercicio1.Text);
                }
                if (!String.IsNullOrEmpty(txtBolsaFamiliaCadastradasExercicio1.Text))
                {
                    obj.TransferenciaRendaPrevisaoAnual.FamiliasCadastradas2022 = Convert.ToInt32(txtBolsaFamiliaCadastradasExercicio1.Text);
                }
                if (!String.IsNullOrEmpty(txtBolsaFamiliaFamiliasBeneficiariasExercicio1.Text))
                {
                    obj.TransferenciaRendaPrevisaoAnual.NumeroFamiliasBeneficiarias2022 = Convert.ToInt32(txtBolsaFamiliaFamiliasBeneficiariasExercicio1.Text);
                }
                if (!String.IsNullOrEmpty(txtBolsaFamiliaRepasseMensalExercicio1.Text))
                    obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2022 = Convert.ToDecimal(txtBolsaFamiliaRepasseMensalExercicio1.Text);

                if (!String.IsNullOrEmpty(txtBolsaFamiliaEstimativaFamiliasExercicio2.Text))
                    obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2023 = Convert.ToInt32(txtBolsaFamiliaEstimativaFamiliasExercicio2.Text);
                if (!String.IsNullOrEmpty(txtBolsaFamiliaCadastradasExercicio2.Text))
                    obj.TransferenciaRendaPrevisaoAnual.FamiliasCadastradas2023 = Convert.ToInt32(txtBolsaFamiliaCadastradasExercicio2.Text);
                if (!String.IsNullOrEmpty(txtBolsaFamiliaFamiliasBeneficiariasExercicio2.Text))
                    obj.TransferenciaRendaPrevisaoAnual.NumeroFamiliasBeneficiarias2023 = Convert.ToInt32(txtBolsaFamiliaFamiliasBeneficiariasExercicio2.Text);
                if (!String.IsNullOrEmpty(txtBolsaFamiliaRepasseMensalExercicio2.Text))
                    obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2023 = Convert.ToDecimal(txtBolsaFamiliaRepasseMensalExercicio2.Text);

                if (!String.IsNullOrEmpty(txtBolsaFamiliaEstimativaFamiliasExercicio3.Text))
                    obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2024 = Convert.ToInt32(txtBolsaFamiliaEstimativaFamiliasExercicio3.Text);
                if (!String.IsNullOrEmpty(txtBolsaFamiliaCadastradasExercicio3.Text))
                    obj.TransferenciaRendaPrevisaoAnual.FamiliasCadastradas2024 = Convert.ToInt32(txtBolsaFamiliaCadastradasExercicio3.Text);
                if (!String.IsNullOrEmpty(txtBolsaFamiliaFamiliasBeneficiariasExercicio3.Text))
                    obj.TransferenciaRendaPrevisaoAnual.NumeroFamiliasBeneficiarias2024 = Convert.ToInt32(txtBolsaFamiliaFamiliasBeneficiariasExercicio3.Text);
                if (!String.IsNullOrEmpty(txtBolsaFamiliaRepasseMensalExercicio3.Text))
                    obj.TransferenciaRendaPrevisaoAnual.RepasseMensal2024 = Convert.ToDecimal(txtBolsaFamiliaRepasseMensalExercicio3.Text);
            }
            if (obj.IdTipoTransferenciaRenda == Convert.ToInt32(ETipoTransferenciaRenda.RendaCidada) || obj.IdTipoTransferenciaRenda == Convert.ToInt32(ETipoTransferenciaRenda.AcaoJovem))
            {
                if (!String.IsNullOrEmpty(txtmediaMensalExercicio0.Text))
                    obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2021 = Convert.ToInt32(txtmediaMensalExercicio0.Text);

                if (!String.IsNullOrEmpty(txtmediaMensalExercicio1.Text))
                    obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2022 = Convert.ToInt32(txtmediaMensalExercicio1.Text);

                if (!String.IsNullOrEmpty(txtmediaMensalExercicio2.Text))
                    obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2023 = Convert.ToInt32(txtmediaMensalExercicio2.Text);

                if (!String.IsNullOrEmpty(txtmediaMensalExercicio3.Text))
                    obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2024 = Convert.ToInt32(txtmediaMensalExercicio3.Text);

                if (!String.IsNullOrEmpty(txtmediaMensalExercicio4.Text))
                    obj.TransferenciaRendaPrevisaoAnual.EstimativaFamilias2025 = Convert.ToInt32(txtmediaMensalExercicio4.Text);

                if (!String.IsNullOrEmpty(txtMetaPactuadaExercicio0.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2021 = Convert.ToInt32(txtMetaPactuadaExercicio0.Text);

                if (!String.IsNullOrEmpty(txtMetaPactuadaExercicio1.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2022 = Convert.ToInt32(txtMetaPactuadaExercicio1.Text);

                if (!String.IsNullOrEmpty(txtMetaPactuadaExercicio2.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2023 = Convert.ToInt32(txtMetaPactuadaExercicio2.Text);

                if (!String.IsNullOrEmpty(txtMetaPactuadaExercicio3.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2024 = Convert.ToInt32(txtMetaPactuadaExercicio3.Text);

                if (!String.IsNullOrEmpty(txtMetaPactuadaExercicio4.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2025 = Convert.ToInt32(txtMetaPactuadaExercicio4.Text);
            }

            if (obj.IdTipoTransferenciaRenda == Convert.ToInt32(ETipoTransferenciaRenda.ProsperaFamilia) || obj.IdTipoTransferenciaRenda == Convert.ToInt32(ETipoTransferenciaRenda.FCadUnico) || obj.IdTipoTransferenciaRenda == Convert.ToInt32(ETipoTransferenciaRenda.FVigilancia))
            {

                if (!String.IsNullOrEmpty(txtMetaPactuada2021.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2021 = Convert.ToInt32(txtMetaPactuada2021.Text);

                if (!String.IsNullOrEmpty(txtMetaPactuada2022.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2022 = Convert.ToInt32(txtMetaPactuada2022.Text);

                if (!String.IsNullOrEmpty(txtMetaPactuada2023.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2023 = Convert.ToInt32(txtMetaPactuada2023.Text);

                if (!String.IsNullOrEmpty(txtMetaPactuada2024.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2024 = Convert.ToInt32(txtMetaPactuada2024.Text);

                if (!String.IsNullOrEmpty(txtMetaPactuada2025.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2025 = Convert.ToInt32(txtMetaPactuada2025.Text);

                if (!String.IsNullOrEmpty(txtNumeroAtendidos2021.Text))
                    obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2021 = Convert.ToInt32(txtNumeroAtendidos2021.Text);
                
                if (!String.IsNullOrEmpty(txtNumeroAtendidos2022.Text))
                    obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2022 = Convert.ToInt32(txtNumeroAtendidos2022.Text);
                
                if (!String.IsNullOrEmpty(txtNumeroAtendidos2023.Text))
                    obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2023 = Convert.ToInt32(txtNumeroAtendidos2023.Text);
                
                if (!String.IsNullOrEmpty(txtNumeroAtendidos2024.Text))
                    obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2024 = Convert.ToInt32(txtNumeroAtendidos2024.Text);
                
                if (!String.IsNullOrEmpty(txtNumeroAtendidos2025.Text))
                    obj.TransferenciaRendaPrevisaoAnual.NumeroAtendidos2025 = Convert.ToInt32(txtNumeroAtendidos2025.Text);

                if (!String.IsNullOrEmpty(txtValorRepasseEstadual2021.Text))
                    obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2021 = Convert.ToDecimal(txtValorRepasseEstadual2021.Text);
                
                if (!String.IsNullOrEmpty(txtValorRepasseEstadual2022.Text))
                    obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2022 = Convert.ToDecimal(txtValorRepasseEstadual2022.Text);
                
                if (!String.IsNullOrEmpty(txtValorRepasseEstadual2023.Text))
                    obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2023 = Convert.ToDecimal(txtValorRepasseEstadual2023.Text);
                
                if (!String.IsNullOrEmpty(txtValorRepasseEstadual2024.Text))
                    obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2024 = Convert.ToDecimal(txtValorRepasseEstadual2024.Text);
                
                if (!String.IsNullOrEmpty(txtValorRepasseEstadual2025.Text))
                    obj.TransferenciaRendaPrevisaoAnual.ValorRepasseEstadual2025 = Convert.ToDecimal(txtValorRepasseEstadual2025.Text);

                if (!String.IsNullOrEmpty(txtFEASReprogramadoExercicio1.Text))
                    obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2022 = Convert.ToDecimal(txtFEASReprogramadoExercicio1.Text);

                if (!String.IsNullOrEmpty(txtFEASReprogramadoExercicio2.Text))
                    obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2023 = Convert.ToDecimal(txtFEASReprogramadoExercicio2.Text);

                if (!String.IsNullOrEmpty(txtFEASReprogramadoExercicio3.Text))
                    obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2024 = Convert.ToDecimal(txtFEASReprogramadoExercicio3.Text);
               
                if (!String.IsNullOrEmpty(txtFEASReprogramadoExercicio4.Text))
                    obj.TransferenciaRendaPrevisaoAnual.ValorReprogramacaoRepasseEstadual2025 = Convert.ToDecimal(txtFEASReprogramadoExercicio4.Text);

            }

            if (obj.IdTipoTransferenciaRenda == Convert.ToInt32(ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCIdoso) || obj.IdTipoTransferenciaRenda == Convert.ToInt32(ETipoTransferenciaRenda.BeneficioPrestacaoContinuaBPCPessoaDeficiencia))
            {
                if (!String.IsNullOrEmpty(txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio1.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2022 = Convert.ToInt32(txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio1.Text);

                if (!String.IsNullOrEmpty(txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio2.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2023 = Convert.ToInt32(txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio2.Text);

                if (!String.IsNullOrEmpty(txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio3.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2024 = Convert.ToInt32(txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio3.Text);

                if (!String.IsNullOrEmpty(txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio4.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2025 = Convert.ToInt32(txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio4.Text);

                // Verifica se a "Previsão anual do valor do repasse" já esta preenchida, caso sim, mantem o que esta.

                if (!String.IsNullOrEmpty(lblBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio1.Text) && String.IsNullOrEmpty(txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio1.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2022 =  obj3.TransferenciaRendaPrevisaoAnual != null ? obj3.TransferenciaRendaPrevisaoAnual.MetaPactuada2022 : 0;

                if (!String.IsNullOrEmpty(lblBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio2.Text) && String.IsNullOrEmpty(txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio2.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2023 = obj3.TransferenciaRendaPrevisaoAnual != null ? obj3.TransferenciaRendaPrevisaoAnual.MetaPactuada2023 : 0;
                
                if (!String.IsNullOrEmpty(lblBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio3.Text) && String.IsNullOrEmpty(txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio3.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2024 = obj3.TransferenciaRendaPrevisaoAnual != null ? obj3.TransferenciaRendaPrevisaoAnual.MetaPactuada2024 : 0;
                
                if (!String.IsNullOrEmpty(lblBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio4.Text) && String.IsNullOrEmpty(txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio4.Text))
                    obj.TransferenciaRendaPrevisaoAnual.MetaPactuada2025 = obj3.TransferenciaRendaPrevisaoAnual != null ? obj3.TransferenciaRendaPrevisaoAnual.MetaPactuada2025 : 0;
            }

            if (obj.IdTipoTransferenciaRenda == Convert.ToInt32(ETipoTransferenciaRenda.AuxilioAluguel))
            {


                if (!String.IsNullOrEmpty(txtAuxilioAluguelNumeroAtendidosExercicio3.Text))
                {
                    obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelNumeroAtendidasExercicio2024 = Convert.ToInt32(txtAuxilioAluguelNumeroAtendidosExercicio3.Text);    
                }
                if (!String.IsNullOrEmpty(txtAuxilioAluguelAtivasExercicio3.Text))
                {
                    obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelAtivasExercicio2024 = Convert.ToInt32(txtAuxilioAluguelAtivasExercicio3.Text);    
                }

                if (!String.IsNullOrEmpty(txtAuxilioAluguelRecebidasExercicio3.Text))
                {
                    obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelRecebidasExercicio2024 = Convert.ToInt32(txtAuxilioAluguelRecebidasExercicio3.Text);    
                }

                if (!String.IsNullOrEmpty(txtAuxilioAluguelNumeroAtendidosExercicio4.Text))
                    obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelNumeroAtendidasExercicio2025 = Convert.ToInt32(txtAuxilioAluguelNumeroAtendidosExercicio4.Text);    

                if (!String.IsNullOrEmpty(txtAuxilioAluguelAtivasExercicio4.Text))
                    obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelAtivasExercicio2025 = Convert.ToInt32(txtAuxilioAluguelAtivasExercicio4.Text);    
                
                if (!String.IsNullOrEmpty(txtAuxilioAluguelRecebidasExercicio4.Text))
                    obj.TransferenciaRendaPrevisaoAnual.AuxilioAluguelRecebidasExercicio2025 = Convert.ToInt32(txtAuxilioAluguelRecebidasExercicio4.Text);    


                
            }


        }

        protected void lstParcerias_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluirParceria")) };
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        protected void lstParcerias_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            try
            {
                switch (e.CommandName)
                {
                    case "Excluir":
                        if (Parcerias == null || Parcerias.Count == 0)
                            break;
                        Parcerias.RemoveAt(e.Item.DataItemIndex);
                        carregarParcerias();
                        var script = Util.GetJavaScriptDialogOK("Parceria excluída com sucesso!");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                var script = Util.GetJavaScriptDialogOK(ex.Message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
            }
        }

        protected void btnAdicionarParceria_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var parceria = new TransferenciaRendaParceriaInfo();
            parceria.IdTipoParceria = Convert.ToInt32(ddlTipoParceria.SelectedValue);
            parceria.NomeOrgao = txtNomeOrgao.Text;
            parceria.IdParceria = Convert.ToInt32(ddlParceria.SelectedValue);
            parceria.Parceria = new ParceriaInfo() { Nome = ddlParceria.SelectedItem.Text };
            parceria.TipoParceria = new TipoParceriaInfo() { Nome = ddlTipoParceria.SelectedItem.Text };

            try
            {
                new ValidadorTransferenciaRendaParceria().Validar(parceria);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            Parcerias = Parcerias ?? new List<TransferenciaRendaParceriaInfo>();
            Parcerias.Add(parceria);

            carregarParcerias();

            txtNomeOrgao.Text = String.Empty;
            ddlTipoParceria.SelectedIndex = ddlParceria.SelectedIndex = 0;
        }

        protected void rblParcerias_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbParcerias.Visible = rblParcerias.SelectedValue == "1";
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BlocoIII/CProgramasProjetos.aspx");
        }

        void carregarEstruturasPETI(TransferenciaRendaInfo obj)
        {
            using (var proxy = new ProxyProgramas())
            {
                ddlEixoAtuacaoPeti.DataValueField = "Id";
                ddlEixoAtuacaoPeti.DataTextField = "Nome";
                ddlEixoAtuacaoPeti.DataSource = proxy.Service.GetPETIEixosAtuacao();
                ddlEixoAtuacaoPeti.DataBind();
                Util.InserirItemEscolha(ddlEixoAtuacaoPeti);

                Util.InserirItemEscolha(ddlTipoAcaoPeti);

                var indicadores = proxy.Service.GetPETIIndicadoresByMunicipio(SessaoPmas.UsuarioLogado.Prefeitura.IdMunicipio);
                hdfIdPETI.Value = Genericos.clsCrypto.Encrypt(indicadores.Id.ToString());
                hdfMunicipio.Value = Genericos.clsCrypto.Encrypt(indicadores.IdMunicipio.ToString());
                if (indicadores != null)
                {

                    txtIdade1013AnosExercicio0.Text = indicadores.Idade1013Ano2021.HasValue ? indicadores.Idade1013Ano2021.ToString() : "0";
                    txtIdade1415AnosExercicio0.Text = indicadores.Idade1415Ano2021.HasValue ? indicadores.Idade1415Ano2021.ToString() : "0";
                    txtIdade1617AnosExercicio0.Text = indicadores.Idade1617Ano2021.HasValue ? indicadores.Idade1617Ano2021.ToString() : "0";
                    int total2021 = Convert.ToInt32(txtIdade1013AnosExercicio0.Text) + Convert.ToInt32(txtIdade1415AnosExercicio0.Text) + Convert.ToInt32(txtIdade1617AnosExercicio0.Text);

                    lblTotalExercicio0.Text = total2021.ToString();

                    txtIdade1013AnosExercicio1.Text = indicadores.Idade1013Ano2022.HasValue ? indicadores.Idade1013Ano2022.ToString() : String.Empty;
                    txtIdade1415AnosExercicio1.Text = indicadores.Idade1415Ano2022.HasValue ? indicadores.Idade1415Ano2022.ToString() : String.Empty;
                    txtIdade1617AnosExercicio1.Text = indicadores.Idade1617Ano2022.HasValue ? indicadores.Idade1617Ano2022.ToString() : String.Empty;

                    #region Valores Somados Automaticamente

                    if (!String.IsNullOrEmpty(txtIdade1013AnosExercicio1.Text))
                    {
                        composicao1013Anos2022 = Convert.ToInt32(txtIdade1013AnosExercicio1.Text);
                    }
                    if (!String.IsNullOrEmpty(txtIdade1415AnosExercicio1.Text))
                    {
                        composicao1415Anos2022 = Convert.ToInt32(txtIdade1415AnosExercicio1.Text);
                    }
                    if (!String.IsNullOrEmpty(txtIdade1617AnosExercicio1.Text))
                    {
                        composicao1617Anos2022 = Convert.ToInt32(txtIdade1617AnosExercicio1.Text);
                    }

                    int total2022 = composicao1013Anos2022 + composicao1415Anos2022 + composicao1617Anos2022;
                    lblTotalExercicio1.Text = total2022.ToString();

                    txtIdade1013AnosExercicio2.Text = indicadores.Idade1013Ano2023.HasValue ? indicadores.Idade1013Ano2023.ToString() : String.Empty;
                    txtIdade1415AnosExercicio2.Text = indicadores.Idade1415Ano2023.HasValue ? indicadores.Idade1415Ano2023.ToString() : String.Empty;
                    txtIdade1617AnosExercicio2.Text = indicadores.Idade1617Ano2023.HasValue ? indicadores.Idade1617Ano2023.ToString() : String.Empty;


                    if (!String.IsNullOrEmpty(txtIdade1013AnosExercicio2.Text))
                    {
                        composicao1013Anos2023 = Convert.ToInt32(txtIdade1013AnosExercicio2.Text);
                    }
                    if (!String.IsNullOrEmpty(txtIdade1415AnosExercicio2.Text))
                    {
                        composicao1415Anos2023 = Convert.ToInt32(txtIdade1415AnosExercicio2.Text);
                    }
                    if (!String.IsNullOrEmpty(txtIdade1617AnosExercicio2.Text))
                    {
                        composicao1617Anos2023 = Convert.ToInt32(txtIdade1617AnosExercicio2.Text);
                    }

                    int total2023 = composicao1013Anos2023 + composicao1415Anos2023 + composicao1617Anos2023;
                    lblTotalExercicio2.Text = total2023.ToString();

                    txtIdade1013AnosExercicio3.Text = indicadores.Idade1013Ano2024.HasValue ? indicadores.Idade1013Ano2024.ToString() : String.Empty;
                    txtIdade1415AnosExercicio3.Text = indicadores.Idade1415Ano2024.HasValue ? indicadores.Idade1415Ano2024.ToString() : String.Empty;
                    txtIdade1617AnosExercicio3.Text = indicadores.Idade1617Ano2024.HasValue ? indicadores.Idade1617Ano2024.ToString() : String.Empty;


                    if (!String.IsNullOrEmpty(txtIdade1013AnosExercicio3.Text))
                    {
                        composicao1013Anos2024 = Convert.ToInt32(txtIdade1013AnosExercicio3.Text);
                    }
                    if (!String.IsNullOrEmpty(txtIdade1415AnosExercicio3.Text))
                    {
                        composicao1415Anos2024 = Convert.ToInt32(txtIdade1415AnosExercicio3.Text);
                    }
                    if (!String.IsNullOrEmpty(txtIdade1617AnosExercicio3.Text))
                    {
                        composicao1617Anos2024 = Convert.ToInt32(txtIdade1617AnosExercicio3.Text);
                    }

                    int total2024 = composicao1013Anos2024 + composicao1415Anos2024 + composicao1617Anos2024;
                    lblTotalExercicio3.Text = total2024.ToString();


                    #endregion

                    txtMetaMunicipalExercicio0.Text = indicadores.MetaMunicipal2021.HasValue ? indicadores.MetaMunicipal2021.ToString() : String.Empty;
                    txtMetaMunicipalExercicio1.Text = indicadores.MetaMunicipal2022.HasValue ? indicadores.MetaMunicipal2022.ToString() : String.Empty;
                    txtMetaMunicipalExercicio2.Text = indicadores.MetaMunicipal2023.HasValue ? indicadores.MetaMunicipal2023.ToString() : String.Empty;
                    txtMetaMunicipalExercicio3.Text = indicadores.MetaMunicipal2024.HasValue ? indicadores.MetaMunicipal2024.ToString() : String.Empty;
                }
                chkNaoPossuiGestorPETI.Checked = obj.NaoPossuiTecnicoAcao;
                chkNaoPossuiGestorPETI_CheckedChanged(null, null);

                var gestorAcao = proxy.Service.GetTransferenciaRendaGestorAcaoByTransferenciaRenda(obj.Id);

                if (gestorAcao != null)
                {
                    hdfIdGestorAcao.Value = Genericos.clsCrypto.Encrypt(gestorAcao.Id.ToString());
                    txtNomeGestorPETI.Text = gestorAcao.Nome;
                    txtTelefone.Text = gestorAcao.Telefone;
                    txtCelular.Text = gestorAcao.Celular;
                    txtEmailGestorPETI.Text = gestorAcao.Email;
                }
                else
                {
                    hdfIdGestorAcao.Value = Genericos.clsCrypto.Encrypt("0");
                }

                txtValorAEPETI.Text = obj.ValorAEPETI.HasValue ? obj.ValorAEPETI.Value.ToString("N2") : String.Empty;
                txtValorAEPETI2.Text = obj.ValorAEPETI2.HasValue ? obj.ValorAEPETI2.Value.ToString("N2") : String.Empty;
            }
            
            rblAderiuCofinanciamentoPeti.SelectedValue = obj.PETIAderiuCofinanciamentoFederal.HasValue && obj.PETIAderiuCofinanciamentoFederal.Value ? "1" : "0";
            rblAderiuCofinanciamentoPeti_SelectedIndexChanged(null, null);
            divAderiuCofinanciamentoPeti.Visible = obj.PETIAderiuCofinanciamentoFederal.HasValue && obj.PETIAderiuCofinanciamentoFederal.Value;
            txtPetiDataAdesao.Text = obj.PETIDataAdesao.HasValue ? obj.PETIDataAdesao.Value.ToShortDateString() : String.Empty;

            rblAcoesPeti.SelectedValue = obj.PETIAcoesTrabalhoInfantil.HasValue && obj.PETIAcoesTrabalhoInfantil.Value ? "1" : "0";

            if (obj.PETIAcoesTrabalhoInfantil.HasValue && obj.PETIAcoesTrabalhoInfantil.Value)
            {
                divAcoesPeti.Visible = true;
                AcoesPETI = obj.AcoesPETI;
                carregarAcoesPETI();
            }
        }

        void carregarAcoesPETI()
        {
            int sequencia = 0;
            AcoesPETI.ForEach(a =>
            {
                a.ListViewIndex = sequencia++;
            });

            lstAcoesPETI.DataSource = AcoesPETI.OrderBy(a => a.IdPETIEixoAtuacao).GroupBy(e => e.PETIEixoAtuacao.Nome).Select(g => new { Key = g.Key, Items = g.OrderBy(n => n.IdPETITipoAcao) }).OrderBy(g => g.Key).ToList();
            lstAcoesPETI.DataBind();
        }

      
        protected void rblAderiuCofinanciamentoPeti_SelectedIndexChanged(object sender, EventArgs e) //PMAS 2016
        {
            trAEPETI2.Visible = txtValorAEPETI.Visible = trAEPETI.Visible = trAEPETI2.Visible = trAEPETI.Visible = trGestorPETI.Visible = divPETI.Visible = divAderiuCofinanciamentoPeti.Visible = rblAderiuCofinanciamentoPeti.SelectedValue == "1";
            
            if (rblAderiuCofinanciamentoPeti.SelectedValue == "0")
            {
                txtPetiDataAdesao.Text = txtValorAEPETI.Text = String.Empty;
                rblAcoesPeti.SelectedValue = "0";
                rblAcoesPeti_SelectedIndexChanged(null, null);
            }

        }

        protected void rblAcoesPeti_SelectedIndexChanged(object sender, EventArgs e) //PMAS 2016
        {
            divAcoesPeti.Visible = rblAcoesPeti.SelectedValue == "1";
        }

        protected void ddlEixoAtuacaoPeti_SelectedIndexChanged(object sender, EventArgs e) //PMAS 2016
        {
            using (var proxy = new ProxyProgramas())
            {
                ddlTipoAcaoPeti.DataValueField = "Id";
                ddlTipoAcaoPeti.DataTextField = "Nome";
                ddlTipoAcaoPeti.DataSource = proxy.Service.GetPETITiposAcaoByEixoAtuacao(Convert.ToInt32(ddlEixoAtuacaoPeti.SelectedValue));
                ddlTipoAcaoPeti.DataBind();
                Util.InserirItemEscolha(ddlTipoAcaoPeti);
            }
        }

        protected void btnAdicionarAcaoPeti_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var acao = new PETIAcaoInfo();
            acao.IdPETIEixoAtuacao = Convert.ToInt32(ddlEixoAtuacaoPeti.SelectedValue);
            acao.IdPETITipoAcao = Convert.ToInt32(ddlTipoAcaoPeti.SelectedValue);
            acao.PeriodoRealizacao = ddlPeriodoRealizacao.SelectedValue;
            //  acao.IdPETISituacaoAcao = rblSituacaoAcaoPeti.SelectedValue == "" ? 0 : Convert.ToInt32(rblSituacaoAcaoPeti.SelectedValue);
            acao.PETIEixoAtuacao = new PETIEixoAtuacaoInfo() { Nome = ddlEixoAtuacaoPeti.SelectedItem.Text };
            acao.PETITipoAcao = new PETITipoAcaoInfo() { Nome = ddlTipoAcaoPeti.SelectedItem.Text };
            //  acao.PETISituacaoAcao = rblSituacaoAcaoPeti.SelectedValue == "" ? null : new PETISituacaoAcaoInfo() { Nome = rblSituacaoAcaoPeti.SelectedItem.Text };
            acao.ListViewIndex = AcoesPETI == null ? 0 : AcoesPETI.Count;

            try
            {
                new ValidadorPETIAcao().Validar(acao);
                tbInconsistencias.Visible = false;
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            AcoesPETI = AcoesPETI ?? new List<PETIAcaoInfo>();
            AcoesPETI.Add(acao);

            carregarAcoesPETI();

            ddlEixoAtuacaoPeti.SelectedValue = "0";
            ddlEixoAtuacaoPeti_SelectedIndexChanged(null, null);
            ddlPeriodoRealizacao.SelectedValue = "0";
            //rblSituacaoAcaoPeti.ClearSelection();
        }

        protected void lstItemsAcoesPETI_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            try
            {
                switch (e.CommandName)
                {
                    case "Excluir":
                        if (AcoesPETI == null || AcoesPETI.Count == 0)
                            break;
                        AcoesPETI.RemoveAt(Convert.ToInt32(((Label)e.Item.FindControl("lblIndex")).Text));
                        carregarAcoesPETI();
                        var script = Util.GetJavaScriptDialogOK("Ação excluída com sucesso!");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                var script = Util.GetJavaScriptDialogOK(ex.Message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
            }
        }

        protected void lstItemsAcoesPETI_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                PETIAcaoInfo temp = (PETIAcaoInfo)(((ListViewDataItem)e.Item).DataItem);
                if (temp.PeriodoRealizacao == "2026")
                    ((Label)e.Item.FindControl("lblPeriodoRealizacao")).Text = "Contínuo até 2025";
                else
                    ((Label)e.Item.FindControl("lblPeriodoRealizacao")).Text = temp.PeriodoRealizacao;

                ((Label)e.Item.FindControl("lblIndex")).Text = temp.ListViewIndex.ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluir")) };
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        protected void rblAdesaoPrograma_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idTipoTransferencia = Convert.ToInt32(Session["idTipoTransferencia"]);

            if (rblAdesaoPrograma.SelectedValue == "1")
            {
                
                if (idTipoTransferencia == 13)
                {
                    trTecnicoReferencia.Visible = trMetasAtendimento.Visible = trDataAdesao.Visible = true;
                }
                else
                {
                    trFEASRecursosReprogramados.Visible = trInterlocutorMunicipal.Visible = trArticulacoesPromovidas.Visible = trMetasAtendimento.Visible = trDataAdesao.Visible = true;
                }

            }
            else
            {
                trFEASRecursosReprogramados.Visible = trTecnicoReferencia.Visible = trInterlocutorMunicipal.Visible = trArticulacoesPromovidas.Visible = trMetasAtendimento.Visible = trDataAdesao.Visible = false;

                txtDataAdesao.Text = String.Empty;
                txtMetaPactuadaExercicio3.Text = String.Empty;
                txtMetaPactuadaExercicio4.Text = String.Empty;
                txtmediaMensalExercicio3.Text = String.Empty;
                txtmediaMensalExercicio4.Text = String.Empty;
            }
        }

        protected void chkNaoPossuiGestorPETI_CheckedChanged(object sender, EventArgs e)
        {

            if (chkNaoPossuiGestorPETI.Checked)
            {
                txtNomeGestorPETI.Text = txtTelefone.Text = txtCelular.Text = txtEmailGestorPETI.Text = String.Empty;
                txtNomeGestorPETI.Enabled = txtTelefone.Enabled = txtCelular.Enabled = txtEmailGestorPETI.Enabled = false;
            }
            else
            {
                txtNomeGestorPETI.Enabled = txtTelefone.Enabled = txtCelular.Enabled = txtEmailGestorPETI.Enabled = true;
            }
        }

      

        #region bloqueio e desbloqueio

        private void AplicarRegraBloqueioDesbloqueio()
        {
            WebControl[] controles0 = LoadControlesVerificacaoBloqueioExercicio0();
            WebControl[] controles1 = LoadControlesVerificacaoBloqueioExercicio1();
            WebControl[] controles2 = LoadControlesVerificacaoBloqueioExercicio2();
            WebControl[] controles3 = LoadControlesVerificacaoBloqueioExercicio3();
            WebControl[] controles4 = LoadControlesVerificacaoBloqueioExercicio4();

            WebControl[] controlesReprogramacao1 = LoadControlesVerificacaoBloqueioReprogramacaoExercicio1();
            WebControl[] controlesReprogramacao2 = LoadControlesVerificacaoBloqueioReprogramacaoExercicio2();
            WebControl[] controlesReprogramacao3 = LoadControlesVerificacaoBloqueioReprogramacaoExercicio3();
            WebControl[] controlesReprogramacao4 = LoadControlesVerificacaoBloqueioReprogramacaoExercicio4();

            WebControl[] controlesDatePicker = LoadDatePickerControlesVerificacaoBloqueio();

            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoTransfRendaBlocoIII(controles0,2021);
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoTransfRendaBlocoIII(controles1, Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoTransfRendaBlocoIII(controles2, Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoTransfRendaBlocoIII(controles3, Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoTransfRendaBlocoIII(controles4, Exercicios[3]);

            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(controlesReprogramacao1, Exercicios[0]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(controlesReprogramacao2, Exercicios[1]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(controlesReprogramacao3, Exercicios[2]);
            Permissao.BlocoIII.VerificaPermissaoRedeDiretaBlocoIIIReprogramacao(controlesReprogramacao4, Exercicios[3]);


            var permisao =  Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoBotaoSalvarBlocoIII(btnSalvar);

            if (!permisao)
            {
                Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoReprogramacaoBotaoSalvarBlocoIII(btnSalvar);    
            }
            
            
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoTransfRendaDatePicker(controlesDatePicker);

        }

        private void AplicarRegraBloqueioDesbloqueioEspecial()
        {
            WebControl[] controles0 = LoadControlesVerificacaoBloqueioExercicio0();
            WebControl[] controles1 = LoadControlesVerificacaoBloqueioExercicio1();
            WebControl[] controles2 = LoadControlesVerificacaoBloqueioExercicio2();
            WebControl[] controles3 = LoadControlesVerificacaoBloqueioExercicio3();
            WebControl[] controles4 = LoadControlesVerificacaoBloqueioExercicio4();




            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoTransfRendaBlocoIII(controles0, 2021); //22
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoTransfRendaBlocoIII(controles1, Exercicios[0]); //22 + 1
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoTransfRendaBlocoIII(controles2, Exercicios[1]); //23 + 1
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoTransfRendaBlocoIII(controles3, Exercicios[2]); //24 + 1
            Permissao.BlocoIII.VerificaPermissaoExercicioProgramaProjetoTransfRendaBlocoIII(controles4, Exercicios[3]); //25 + 1
                      
        }


        private WebControl[] LoadDatePickerControlesVerificacaoBloqueio()
        {
            WebControl[] controles = txtDataAdesao.Controles;
            return txtDataAdesao.Controles;
        }

        private WebControl[] LoadControlesVerificacaoBloqueioExercicio0()
        {
            WebControl[] controles = { 
                                       txtValorRepasseEstadual2021
                                      ,txtMetaPactuada2021
                                      ,txtNumeroAtendidos2021
                                     };
            return controles;
        }

        

        private WebControl[] LoadControlesVerificacaoBloqueioExercicio1()
        {
            WebControl[] controles = { 
                                              txtBolsaFamiliaCadastradasExercicio0,
                                              txtBolsaFamiliaFamiliasBeneficiariasExercicio0, 
                                              txtBolsaFamiliaRepasseMensalExercicio0
                                             ,txtMetaPactuadaExercicio1
                                             ,txtMetaPactuada2022
                                             ,txtValorRepasseEstadual2022
                                             ,txtmediaMensalExercicio0
                                             ,txtMetaMunicipalExercicio0
                                             ,txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio1
                                             , txtIdade1013AnosExercicio0
                                             , txtIdade1415AnosExercicio0
                                             , txtIdade1617AnosExercicio0
                                             , txtMetaMunicipalExercicio0
                                             };

            return controles;
        }

        private WebControl[] LoadControlesVerificacaoBloqueioExercicio2()
        {
            WebControl[] controles = { 
                                              txtBolsaFamiliaCadastradasExercicio1,
                                              txtBolsaFamiliaFamiliasBeneficiariasExercicio1,
                                              txtBolsaFamiliaRepasseMensalExercicio1
                                             ,txtMetaPactuadaExercicio2
                                             ,txtMetaPactuada2022
                                             ,txtValorRepasseEstadual2023
                                             ,txtNumeroAtendidos2022
                                             ,txtmediaMensalExercicio1
                                             ,txtMetaMunicipalExercicio1
                                             ,txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio2
                                             , txtIdade1013AnosExercicio1
                                             , txtIdade1415AnosExercicio1
                                             , txtIdade1617AnosExercicio1
                                             , txtMetaMunicipalExercicio1
                                             };
            return controles;
        }

        private WebControl[] LoadControlesVerificacaoBloqueioExercicio3()
        {
            WebControl[] controles = { 
                                              txtBolsaFamiliaCadastradasExercicio2,
                                              txtBolsaFamiliaFamiliasBeneficiariasExercicio2,
                                              txtBolsaFamiliaRepasseMensalExercicio2
                                             , txtmediaMensalExercicio2
                                             , txtMetaMunicipalExercicio2
                                             , txtMetaPactuadaExercicio3
                                             , txtMetaPactuada2024
                                             , txtNumeroAtendidos2023
                                             , txtValorAEPETI
                                             , txtValorRepasseEstadual2024
                                             , txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio3
                                             , txtAuxilioAluguelNumeroAtendidosExercicio3                                             
                                             , txtAuxilioAluguelAtivasExercicio3
                                             , txtAuxilioAluguelRecebidasExercicio3
                                             , txtIdade1013AnosExercicio2
                                             , txtIdade1415AnosExercicio2
                                             , txtIdade1617AnosExercicio2
                                             , txtMetaMunicipalExercicio2
                                             };
            return controles;
        }




        private WebControl[] LoadControlesVerificacaoBloqueioExercicio4()
        {

            WebControl[] controles = { 
                                              txtBolsaFamiliaCadastradasExercicio3,
                                              txtBolsaFamiliaFamiliasBeneficiariasExercicio3,
                                              txtBolsaFamiliaRepasseMensalExercicio3
                                             , txtMetaPactuadaExercicio4
                                             , txtMetaPactuada2025
                                             , txtValorAEPETI2
                                             , txtNumeroAtendidos2024
                                             , txtValorRepasseEstadual2025
                                             , txtmediaMensalExercicio3
                                             , txtMetaMunicipalExercicio3
                                             , txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio4
                                             , txtAuxilioAluguelNumeroAtendidosExercicio4                                             
                                             , txtAuxilioAluguelAtivasExercicio4
                                             , txtAuxilioAluguelRecebidasExercicio4
                                             , txtIdade1013AnosExercicio3
                                             , txtIdade1415AnosExercicio3
                                             , txtIdade1617AnosExercicio3
                                             , txtMetaMunicipalExercicio3
                                             };
            return controles;
        }



        private WebControl[] LoadControlesVerificacaoBloqueioDemaisControles()
        {

            WebControl[] controles = { 
                                             
                                             btnAdicionarParceria
                                             ,ddlBeneficiarios
                                             ,ddlParceria
                                             ,ddlTipoParceria 
                                             ,rblAdesaoPrograma                                     
                                             ,rblParcerias
                                             ,txtNome 
                                             ,txtObjetivo 
                                             ,txtPetiNumeroBeneficiarios
                                             ,txtPetiMensalRepasse
                                             ,txtProgramasMunicipaisNumeroBeneficiarios
                                             ,txtProgramasMunicipaisValorRepasse
                                             ,txtNomeOrgao
                                             ,txtBolsaFamiliaCadastradasExercicio0
                                             ,txtBolsaFamiliaFamiliasBeneficiariasExercicio0
                                             ,txtBolsaFamiliaRepasseMensalExercicio0
                                             ,rblAderiuCofinanciamentoPeti
                                             ,txtNomeGestorPETI
                                             ,chkNaoPossuiGestorPETI
                                             ,txtEmailGestorPETI
                                             ,txtIdade1013AnosExercicio0
                                             ,txtIdade1415AnosExercicio0
                                             ,txtIdade1617AnosExercicio0
                                             ,ddlEixoAtuacaoPeti
                                             ,ddlTipoAcaoPeti
                                             ,ddlPeriodoRealizacao
                                             ,btnAdicionarAcaoPeti
                                             ,txtMetaPactuadaExercicio0
                                             ,txtmediaMensalExercicio0
                                             ,rblAcoesPeti
                                             ,txtNomeTecnico
                                             ,txtEmailInstitucional
                                             ,chkNaoHaTecnico
                                             ,chkTecnicoReferencia
                                             ,txtNomeTecnicoReferencia
                                             ,txtEmailTecnicoReferencia
                                             ,txtUnidadeLotacao
                                             };
            return controles;
        }


        private WebControl[] LoadControlesVerificacaoBloqueioReprogramacaoExercicio1()
        {
            WebControl[] controles = { txtFEASReprogramadoExercicio1 };
            return controles;        
        }

        private WebControl[] LoadControlesVerificacaoBloqueioReprogramacaoExercicio2()
        {
            WebControl[] controles = { txtFEASReprogramadoExercicio2 };
            return controles;
        }

        private WebControl[] LoadControlesVerificacaoBloqueioReprogramacaoExercicio3()
        {
            WebControl[] controles = { txtFEASReprogramadoExercicio3 };
            return controles;
        }

        private WebControl[] LoadControlesVerificacaoBloqueioReprogramacaoExercicio4()
        {
            WebControl[] controles = { txtFEASReprogramadoExercicio4 };
            return controles;
        }
        #endregion


        protected void txtIdade1013AnosExercicio2_TextChanged(object sender, EventArgs e)
        {
            int total = 0;

            if (!String.IsNullOrEmpty(txtIdade1013AnosExercicio2.Text))
            {
                composicao1013Anos2023 = Convert.ToInt32(txtIdade1013AnosExercicio2.Text);
            }
            if (!String.IsNullOrEmpty(txtIdade1415AnosExercicio2.Text))
            {
                composicao1415Anos2023 = Convert.ToInt32(txtIdade1415AnosExercicio2.Text);
            }
             if (!String.IsNullOrEmpty(txtIdade1617AnosExercicio2.Text))
            {
                composicao1617Anos2023 = Convert.ToInt32(txtIdade1617AnosExercicio2.Text);
            }
            
           total = composicao1617Anos2023 + composicao1415Anos2023 + composicao1013Anos2023;

           lblTotalExercicio2.Text = Convert.ToString(total);

        }

        protected void txtIdade1415AnosExercicio2_TextChanged(object sender, EventArgs e)
        {
            int total = 0;

            if (!String.IsNullOrEmpty(txtIdade1013AnosExercicio2.Text))
            {
                composicao1013Anos2023 = Convert.ToInt32(txtIdade1013AnosExercicio2.Text);
            }
           if (!String.IsNullOrEmpty(txtIdade1415AnosExercicio2.Text))
            {
                composicao1415Anos2023 = Convert.ToInt32(txtIdade1415AnosExercicio2.Text);
            }
            if (!String.IsNullOrEmpty(txtIdade1617AnosExercicio2.Text))
            {
                composicao1617Anos2023 = Convert.ToInt32(txtIdade1617AnosExercicio2.Text);
            }

            total = composicao1617Anos2023 + composicao1415Anos2023 + composicao1013Anos2023;

            lblTotalExercicio2.Text = Convert.ToString(total);
        }

        protected void txtIdade1617AnosExercicio2_TextChanged(object sender, EventArgs e)
        {
            int total = 0;

            if (!String.IsNullOrEmpty(txtIdade1013AnosExercicio2.Text))
            {
                composicao1013Anos2023 = Convert.ToInt32(txtIdade1013AnosExercicio2.Text);
            }
            if (!String.IsNullOrEmpty(txtIdade1415AnosExercicio2.Text))
            {
                composicao1415Anos2023 = Convert.ToInt32(txtIdade1415AnosExercicio2.Text);
            }
            if (!String.IsNullOrEmpty(txtIdade1617AnosExercicio2.Text))
            {
                composicao1617Anos2023 = Convert.ToInt32(txtIdade1617AnosExercicio2.Text);
            }

            total = composicao1617Anos2023 + composicao1415Anos2023 + composicao1013Anos2023;

            lblTotalExercicio2.Text = Convert.ToString(total);
        }

        private bool ExecutaProgramaFortalecimentoCad() 
        {
            bool executa;

            if ((!String.IsNullOrEmpty(txtMetaPactuada2022.Text)))
            {
                if (txtMetaPactuada2022.Text != "0")
                {
                    executa = true;
                    return executa;
                }
                else
                {
                    executa = false;
                }
            }
            else
            {
                executa = false;
            }


            if (!String.IsNullOrEmpty(txtValorRepasseEstadual2022.Text))
            {
                if (txtValorRepasseEstadual2022.Text != "0,00")
	            {
                    executa = true;
                    return executa;
	            }
                else
	            {
                    executa = false;
	            }
            }
            else
            {
                executa = false;
            }


            if ((!String.IsNullOrEmpty(txtMetaPactuada2023.Text)))
            {
                if (txtMetaPactuada2023.Text != "0")
                {
                    executa = true;
                    return executa;
                }
                else
                {
                    executa = false;
                }
            }
            else
            {
                executa = false;
            }


            if (!String.IsNullOrEmpty(txtValorRepasseEstadual2023.Text))
            {
                if (txtValorRepasseEstadual2023.Text != "0,00")
                {
                    executa = true;
                    return executa;
                }
                else
                {
                    executa = false;
                }
            }
            else
            {
                executa = false;
            }


            if ((!String.IsNullOrEmpty(txtMetaPactuada2024.Text)))
            {
                if (txtMetaPactuada2024.Text != "0")
                {
                    executa = true;
                    return executa;
                }
                else
                {
                    executa = false;
                }
            }
            else
            {
                executa = false;
            }


            if (!String.IsNullOrEmpty(txtValorRepasseEstadual2024.Text))
            {
                if (txtValorRepasseEstadual2024.Text != "0,00")
                {
                    executa = true;
                    return executa;
                }
                else
                {
                    executa = false;
                }
            }
            else
            {
                executa = false;
            }

            if ((!String.IsNullOrEmpty(txtMetaPactuada2025.Text)))
            {
                if (txtMetaPactuada2025.Text != "0")
                {
                    executa = true;
                    return executa;
                }
                else
                {
                    executa = false;
                }
            }
            else
            {
                executa = false;
            }


            if (!String.IsNullOrEmpty(txtValorRepasseEstadual2025.Text))
            {
                if (txtValorRepasseEstadual2025.Text != "0,00")
                {
                    executa = true;
                    return executa;
                }
                else
                {
                    executa = false;
                }
            }
            else
            {
                executa = false;
            }

            return executa;
        }

        private bool ExecutaProgramaFortalecimentoVigilancia()
        {
            bool executa;

            if (!String.IsNullOrEmpty(txtValorRepasseEstadual2022.Text))
            {
                if (txtValorRepasseEstadual2022.Text != "0,00")
                {
                    executa = true;
                    return executa;
                }
                else
                {
                    executa = false;
                }
            }
            else
            {
                executa = false;
            }



            if (!String.IsNullOrEmpty(txtValorRepasseEstadual2023.Text))
            {
                if (txtValorRepasseEstadual2023.Text != "0,00")
                {
                    executa = true;
                    return executa;
                }
                else
                {
                    executa = false;
                }
            }
            else
            {
                executa = false;
            }


            if (!String.IsNullOrEmpty(txtValorRepasseEstadual2024.Text))
            {
                if (txtValorRepasseEstadual2024.Text != "0,00")
                {
                    executa = true;
                    return executa;
                }
                else
                {
                    executa = false;
                }
            }
            else
            {
                executa = false;
            }



            if (!String.IsNullOrEmpty(txtValorRepasseEstadual2025.Text))
            {
                if (txtValorRepasseEstadual2025.Text != "0,00")
                {
                    executa = true;
                    return executa;
                }
                else
                {
                    executa = false;
                }
            }
            else
            {
                executa = false;
            }

            return executa;
        }

        protected void chkNaoHaTecnico_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNaoHaTecnico.Checked)
            {
                txtNomeTecnico.Enabled = false;
                Telefone.Enabled = false;
                Celular.Enabled = false;
                txtEmailInstitucional.Enabled = false;
                
                txtNomeTecnico.Text = "";
                Telefone.Text = "";
                Celular.Text = "";
                txtEmailInstitucional.Text = "";

            }
            else
            {
                txtNomeTecnico.Enabled = true;
                Telefone.Enabled = true;
                Celular.Enabled = true;
                txtEmailInstitucional.Enabled = true;
            }
        }

        protected void btnExercicio1_Click(object sender, EventArgs e)
        {
            selecionaReprogramacao(Exercicios[0]);
        }

        protected void btnExercicio2_Click(object sender, EventArgs e)
        {
            selecionaReprogramacao(Exercicios[1]);
        }

        protected void btnExercicio3_Click(object sender, EventArgs e)
        {
            selecionaReprogramacao(Exercicios[2]);
        }

        protected void btnExercicio4_Click(object sender, EventArgs e)
        {
            selecionaReprogramacao(Exercicios[3]);
        }

        protected void chkTecnicoReferencia_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string lstMsg = String.Empty;


                if (Tecnico.Count() > 0 && Tecnico != null)
                {
                    lstMsg = "Não é possível selecionar o campo, pois, há técnico(s) cadastrado(s).";
                }

                if (!String.IsNullOrEmpty(lstMsg))
                    throw new Exception(lstMsg);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), Util.GetJavascriptDialogError(ex.Message), true);
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

        }

        protected void btnAdicionarTecnico_Click(object sender, EventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            var tecnico = new TransferenciaRendaTecnicoReferenciaInfo();
            string erroMsg = String.Empty;

            tecnico.IdTransferenciaRenda = Convert.ToInt32(Session["idTransferencia"]);

            tecnico.NomeTecnico = txtNomeTecnicoReferencia.Text;
            tecnico.NomeEmail = txtEmailTecnicoReferencia.Text;
            tecnico.NomeUnidadeLotacao = txtUnidadeLotacao.Text;

            //validação
            try
            {
                if (chkTecnicoReferencia.Checked)
                    erroMsg = "Favor desmarcar a opção Não há técnico responsável por este programa.";

                if (!String.IsNullOrEmpty(erroMsg))
                    throw new Exception(erroMsg);

                new ValidadoresTransferenciaRendaTecnicoReferencia().Validar(tecnico);
            }
            catch (Exception ex)
            {
                lblInconsistencias.Text = ex.Message.Replace(System.Environment.NewLine, "<br/>");
                tbInconsistencias.Visible = true;
                return;
            }

            Tecnico = Tecnico ?? new List<TransferenciaRendaTecnicoReferenciaInfo>();
            
            Tecnico.Add(tecnico);

            carregarTecnicoReferencia();

            txtNomeTecnicoReferencia.Text = "";
            txtEmailTecnicoReferencia.Text = "";
            txtUnidadeLotacao.Text = "";
        }

        protected void lstTecnicoReferencia_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                int sequencia = e.Item.DataItemIndex + 1;
                ((Label)e.Item.FindControl("lblSequencia")).Text = (e.Item.DataItemIndex + 1).ToString();
                WebControl[] controles = { ((ImageButton)e.Item.FindControl("btnExcluirTecnico")) };
                Permissao.VerificarPermissaoControles(controles, Session);
            }
        }

        protected void lstTecnicoReferencia_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            SessaoPmas.VerificarSessao(this);

            try
            {
                switch (e.CommandName)
                {
                    case "Excluir":
                        if (Tecnico == null || Tecnico.Count == 0)
                            break;
                        Tecnico.RemoveAt(e.Item.DataItemIndex);
                        carregarTecnicoReferencia();
                        var script = Util.GetJavaScriptDialogOK("Tecnico Excluído com sucesso!");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                var script = Util.GetJavaScriptDialogOK(ex.Message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
            }
        }

    }
}