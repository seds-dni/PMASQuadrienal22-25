<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FOrgaoGestor.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoI.FOrgaoGestor" %>

<%@ Register Src="../Controles/rg.ascx" TagName="rg" TagPrefix="uc1" %>
<%@ Register Src="../Controles/cpf.ascx" TagName="cpf" TagPrefix="uc2" %>
<%@ Register Src="../Controles/cnpj.ascx" TagName="cnpj" TagPrefix="uc1" %>
<%@ Register Src="../Controles/cep.ascx" TagName="cep" TagPrefix="uc2" %>
<%@ Register Src="../Controles/telefone.ascx" TagName="telefone" TagPrefix="uc3" %>
<%@ Register Src="../Controles/celular.ascx" TagName="celular" TagPrefix="uc5" %>
<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <script src="../Scripts/widgets/blocoIII/FOrgaoGestor.js"></script>
    <script type="text/javascript">



        window.onload = function () {
            StartHideOrShow();
         
        };

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args)
        {
            setTimeout(hideOrShow, 100);
        }


        function StartHideOrShow() {
            setTimeout(hideOrShow, 500);
        }


        function calculaTotais() {

            var totalSuperior = parseInt($('#MainContent_txtSuperiorServicoSocial').val()) + parseInt($('#MainContent_txtSuperiorPsicologia').val()) + parseInt($('#MainContent_txtSuperiorPedagogia').val()) + parseInt($('#MainContent_txtSociologia').val()) + parseInt($('#MainContent_txtDireito').val()) + parseInt($('#MainContent_txtSuperiorEconomiaDomestica').val()) + parseInt($('#MainContent_txtSuperiorAdministracao').val()) + parseInt($('#MainContent_txtSuperiorAntropologia').val()) + parseInt($('#MainContent_txtSuperiorContabilidade').val()) + parseInt($('#MainContent_txtSuperiorEconomia').val()) + parseInt($('#MainContent_txtSuperiorTerapiaOcupacional').val()) + parseInt($('#MainContent_txtOutros').val())

            $('#MainContent_lblTotalSuperior').text(totalSuperior);
            $('#MainContent_lblTotalSuperior').val(totalSuperior);

            var total = parseInt($('#MainContent_txtEstatutarios').val()) + parseInt($('#MainContent_txtCeletistas').val()) + parseInt($('#MainContent_txtComissionados').val()) + parseInt($('#MainContent_txtOutrosVinculos').val()) + parseInt($('#MainContent_txtEstagiarios').val()) + parseInt($('#MainContent_txtVoluntarios').val())

            $('#MainContent_lblTotal').text(total);
            $('#MainContent_lblTotal').val(total);

        }


        function hideOrShow() {

            var zero = Number(0)
            var tOutros = Number($('#MainContent_txtOutros').val())

            $('#MainContent_txtEspecificarOutros').hide()
            $('#MainContent_lblEspecificarOutros').hide()

            if (tOutros !== zero)
            {            
                $('#MainContent_txtEspecificarOutros').show()
                $('#MainContent_lblEspecificarOutros').show()
            }
            else
            {
            
                $('#MainContent_txtEspecificarOutros').hide()
                $('#MainContent_lblEspecificarOutros').hide()
            }

            $('#MainContent_txtOutros').change(function () {

           if (tOutros !== zero)
           {
            
               $('#MainContent_txtEspecificarOutros').show()
               $('#MainContent_lblEspecificarOutros').show()

            }
            else
           {
               $('#MainContent_txtEspecificarOutros').hide()
               $('#MainContent_lblEspecificarOutros').hide()
            }
            })
        }


        function CalculateTotal() {

            //Obtem e verifica nulo
            var txtEscolarizacaoBasica = document.getElementById('<%= txtEscolarizacaoBasica.ClientID %>').value;
            var txtFundamentalBasica = document.getElementById('<%=txtFundamentalBasica.ClientID%>').value;
            var txtMedioBasica = document.getElementById('<%=txtMedioBasica.ClientID%>').value;
            var txtSuperiorBasica = document.getElementById('<%=txtSuperiorBasica.ClientID%>').value;
            if (txtEscolarizacaoBasica == '') {document.getElementById('<%=txtEscolarizacaoBasica.ClientID%>').value = '0'; txtEscolarizacaoBasica = '0'  }
            if (txtFundamentalBasica == '') { document.getElementById('<%=txtFundamentalBasica.ClientID%>').value = '0'; txtFundamentalBasica = '0' }
            if (txtMedioBasica == '') { document.getElementById('<%=txtMedioBasica.ClientID%>').value = '0'; txtMedioBasica = '0'; }
            if (txtSuperiorBasica == '') { document.getElementById('<%=txtSuperiorBasica.ClientID%>').value = '0'; txtSuperiorBasica = '0'; }

            //Chamada do metodo
            PageMethods.CalcularProtecaoBasica(txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica, function (val) {
                document.getElementById('<%=lblTotalBasica.ClientID%>').innerText = val;
            });

            //Obtem e verifica nulo
            var txtEscolarizacaoEspecial = document.getElementById('<%=txtEscolarizacaoEspecial.ClientID%>').value;
            var txtFundamentalEspecial = document.getElementById('<%=txtFundamentalEspecial.ClientID%>').value;
            var txtMedioEspecial = document.getElementById('<%=txtMedioEspecial.ClientID%>').value;
            var txtSuperiorEspecial = document.getElementById('<%=txtSuperiorEspecial.ClientID%>').value;
            if (txtEscolarizacaoEspecial == '') { document.getElementById('<%=txtEscolarizacaoBasica.ClientID%>').value = '0'; txtEscolarizacaoBasica = '0' }
            if (txtFundamentalEspecial == '') { document.getElementById('<%=txtFundamentalEspecial.ClientID%>').value = '0'; txtFundamentalEspecial = '0' }
            if (txtMedioEspecial == '') { document.getElementById('<%=txtMedioEspecial.ClientID%>').value = '0'; txtMedioEspecial = '0'; }
            if (txtSuperiorEspecial == '') { document.getElementById('<%=txtSuperiorEspecial.ClientID%>').value = '0'; txtSuperiorEspecial = '0'; }

            //Chamada do metodo
            PageMethods.CalcularProtecaoEspecial(txtEscolarizacaoEspecial, txtFundamentalEspecial, txtMedioEspecial, txtSuperiorEspecial, function (val) {
                document.getElementById('<%=lblTotalEspecial.ClientID%>').innerText = val;
            });

            //Obtem e verifica nulo
            var txtEscolarizacaoSocioassistencial = document.getElementById('<%=txtEscolarizacaoSocioassistencial.ClientID%>').value;
            var txtFundamentalSocioassistencial = document.getElementById('<%=txtFundamentalSocioassistencial.ClientID%>').value;
            var txtMedioSocioassistencial = document.getElementById('<%=txtMedioSocioassistencial.ClientID%>').value;
            var txtSuperiorSocioassistencial = document.getElementById('<%=txtSuperiorSocioassistencial.ClientID%>').value;
            if (txtEscolarizacaoEspecial == '') { document.getElementById('<%=txtEscolarizacaoSocioassistencial.ClientID%>').value = '0'; txtEscolarizacaoSocioassistencial = '0' }
            if (txtFundamentalEspecial == '') { document.getElementById('<%=txtFundamentalSocioassistencial.ClientID%>').value = '0'; txtFundamentalSocioassistencial = '0' }
            if (txtMedioSocioassistencial == '') { document.getElementById('<%=txtMedioSocioassistencial.ClientID%>').value = '0'; txtMedioSocioassistencial = '0'; }
            if (txtSuperiorSocioassistencial == '') { document.getElementById('<%=txtSuperiorSocioassistencial.ClientID%>').value = '0'; txtSuperiorSocioassistencial = '0'; }

            //Chamada do metodo
            PageMethods.CalcularVigilancia(txtEscolarizacaoSocioassistencial, txtFundamentalSocioassistencial, txtMedioSocioassistencial, txtSuperiorSocioassistencial, function (val) {
                document.getElementById('<%=lblTotalSocioassistencial.ClientID%>').innerText = val;
            });


            //Obtem e verifica nulo
            var txtEscolarizacaoGestaoSuas = document.getElementById('<%=txtEscolarizacaoGestaoSuas.ClientID%>').value;
            var txtFundamentalGestaoSuas = document.getElementById('<%=txtFundamentalGestaoSuas.ClientID%>').value;
            var txtMedioGestaoSuas = document.getElementById('<%=txtMedioGestaoSuas.ClientID%>').value;
            var txtSuperiorGestaoSuas = document.getElementById('<%=txtSuperiorGestaoSuas.ClientID%>').value;
            if (txtEscolarizacaoEspecial == '') { document.getElementById('<%=txtEscolarizacaoGestaoSuas.ClientID%>').value = '0'; txtEscolarizacaoGestaoSuas = '0' }
            if (txtFundamentalEspecial == '') { document.getElementById('<%=txtFundamentalGestaoSuas.ClientID%>').value = '0'; txtFundamentalGestaoSuas = '0' }
            if (txtMedioSocioassistencial == '') { document.getElementById('<%=txtMedioGestaoSuas.ClientID%>').value = '0'; txtMedioGestaoSuas = '0'; }
            if (txtSuperiorSocioassistencial == '') { document.getElementById('<%=txtSuperiorGestaoSuas.ClientID%>').value = '0'; txtSuperiorGestaoSuas = '0'; }

            //Chamada do metodo
            PageMethods.CalcularVigilancia(txtEscolarizacaoGestaoSuas, txtFundamentalGestaoSuas, txtMedioGestaoSuas, txtSuperiorGestaoSuas, function (val) {
                document.getElementById('<%=lblTotalGestaoSuas.ClientID%>').innerText = val;
            });


            var txtEscolarizacaoTransferencia = document.getElementById('<%=txtEscolarizacaoTransferencia.ClientID%>').value;
            var txtFundamentalTransferencia = document.getElementById('<%=txtFundamentalTransferencia.ClientID%>').value;
            var txtMedioTransferencia = document.getElementById('<%=txtMedioTransferencia.ClientID%>').value;
            var txtSuperiorTransferencia = document.getElementById('<%=txtSuperiorTransferencia.ClientID%>').value;
            if (txtEscolarizacaoTransferencia == '') { document.getElementById('<%=txtEscolarizacaoTransferencia.ClientID%>').value = '0'; txtEscolarizacaoTransferencia = '0' }
            if (txtFundamentalTransferencia == '') { document.getElementById('<%=txtFundamentalTransferencia.ClientID%>').value = '0'; txtFundamentalTransferencia = '0' }
            if (txtMedioTransferencia == '') { document.getElementById('<%=txtMedioTransferencia.ClientID%>').value = '0'; txtMedioTransferencia = '0'; }
            if (txtSuperiorTransferencia == '') { document.getElementById('<%=txtSuperiorTransferencia.ClientID%>').value = '0'; txtSuperiorTransferencia = '0'; }
            //var valores = [txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica];
            PageMethods.CalcularGestaoTransferencia(txtEscolarizacaoTransferencia, txtFundamentalTransferencia, txtMedioTransferencia, txtSuperiorTransferencia, function (val) {
                document.getElementById('<%=lblTotalTransferencia.ClientID%>').innerText = val;
            });

            var txtEscolarizacaoCadUnico = document.getElementById('<%=txtEscolarizacaoCadUnico.ClientID%>').value;
            var txtFundamentalCadUnico = document.getElementById('<%=txtFundamentalCadUnico.ClientID%>').value;
            var txtMedioCadUnico = document.getElementById('<%=txtMedioCadUnico.ClientID%>').value;
            var txtSuperiorCadUnico = document.getElementById('<%=txtSuperiorCadUnico.ClientID%>').value;
            if (txtEscolarizacaoCadUnico == '') { document.getElementById('<%=txtEscolarizacaoCadUnico.ClientID%>').value = '0'; txtEscolarizacaoCadUnico = '0' }
            if (txtFundamentalCadUnico == '') { document.getElementById('<%=txtFundamentalCadUnico.ClientID%>').value = '0'; txtFundamentalCadUnico = '0' }
            if (txtMedioCadUnico == '') { document.getElementById('<%=txtMedioCadUnico.ClientID%>').value = '0'; txtMedioCadUnico = '0'; }
            if (txtSuperiorCadUnico == '') { document.getElementById('<%=txtSuperiorCadUnico.ClientID%>').value = '0'; txtSuperiorCadUnico = '0'; }
            //var valores = [txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica];
            PageMethods.CalcularGestaoCadUnico(txtEscolarizacaoCadUnico, txtFundamentalCadUnico, txtMedioCadUnico, txtSuperiorCadUnico, function (val) {
                document.getElementById('<%=lblTotalCadUnico.ClientID%>').innerText = val;
            });


            var txtEscolarizacaoGestaoFinanceira = document.getElementById('<%=txtEscolarizacaoGestaoFinanceira.ClientID%>').value;
            var txtFundamentalGestaoFinanceira = document.getElementById('<%=txtFundamentalGestaoFinanceira.ClientID%>').value;
            var txtMedioGestaoFinanceira = document.getElementById('<%=txtMedioGestaoFinanceira.ClientID%>').value;
            var txtSuperiorGestaoFinanceira = document.getElementById('<%=txtSuperiorGestaoFinanceira.ClientID%>').value;
            if (txtEscolarizacaoGestaoFinanceira == '') { document.getElementById('<%=txtEscolarizacaoGestaoFinanceira.ClientID%>').value = '0'; txtEscolarizacaoGestaoFinanceira = '0' }
            if (txtFundamentalGestaoFinanceira == '') { document.getElementById('<%=txtFundamentalGestaoFinanceira.ClientID%>').value = '0'; txtFundamentalGestaoFinanceira = '0' }
            if (txtMedioGestaoFinanceira == '') { document.getElementById('<%=txtMedioGestaoFinanceira.ClientID%>').value = '0'; txtMedioGestaoFinanceira = '0'; }
            if (txtSuperiorGestaoFinanceira == '') { document.getElementById('<%=txtSuperiorGestaoFinanceira.ClientID%>').value = '0'; txtSuperiorGestaoFinanceira = '0'; }
            //var valores = [txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica];
            PageMethods.CalcularGestaoFinanceira(txtEscolarizacaoGestaoFinanceira, txtFundamentalGestaoFinanceira, txtMedioGestaoFinanceira, txtSuperiorGestaoFinanceira, function (val) {
                document.getElementById('<%=lblTotalGestaoFinanceira.ClientID%>').innerText = val;
            });

            var txtEscolarizacaoSUAS = document.getElementById('<%=txtEscolarizacaoSUAS.ClientID%>').value;
            var txtFundamentalSUAS = document.getElementById('<%=txtFundamentalSUAS.ClientID%>').value;
            var txtMedioSUAS = document.getElementById('<%=txtMedioSUAS.ClientID%>').value;
            var txtSuperiorSUAS = document.getElementById('<%=txtSuperiorSUAS.ClientID%>').value;
            if (txtEscolarizacaoSUAS == '') { document.getElementById('<%=txtEscolarizacaoSUAS.ClientID%>').value = '0'; txtEscolarizacaoSUAS = '0' }
            if (txtFundamentalSUAS == '') { document.getElementById('<%=txtFundamentalSUAS.ClientID%>').value = '0'; txtFundamentalSUAS = '0' }
            if (txtMedioSUAS == '') { document.getElementById('<%=txtMedioSUAS.ClientID%>').value = '0'; txtMedioSUAS = '0'; }
            if (txtSuperiorSUAS == '') { document.getElementById('<%=txtSuperiorSUAS.ClientID%>').value = '0'; txtSuperiorSUAS = '0'; }
            //var valores = [txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica];
            PageMethods.CalcularTrabalhoSUAS(txtEscolarizacaoSUAS, txtFundamentalSUAS, txtMedioSUAS, txtSuperiorSUAS, function (val) {
                document.getElementById('<%=lblTotalSUAS.ClientID%>').innerText = val;
            });

            var txtEscolarizacaoRegulacaoSUAS = document.getElementById('<%=txtEscolarizacaoRegulacaoSUAS.ClientID%>').value;
            var txtFundamentalRegulacaoSUAS = document.getElementById('<%=txtFundamentalRegulacaoSUAS.ClientID%>').value;
            var txtMedioRegulacaoSUAS = document.getElementById('<%=txtMedioRegulacaoSUAS.ClientID%>').value;
            var txtSuperiorRegulacaoSUAS = document.getElementById('<%=txtSuperiorRegulacaoSUAS.ClientID%>').value;
            if (txtEscolarizacaoRegulacaoSUAS == '') { document.getElementById('<%=txtEscolarizacaoRegulacaoSUAS.ClientID%>').value = '0'; txtEscolarizacaoRegulacaoSUAS = '0' }
            if (txtFundamentalRegulacaoSUAS == '') { document.getElementById('<%=txtFundamentalRegulacaoSUAS.ClientID%>').value = '0'; txtFundamentalRegulacaoSUAS = '0' }
            if (txtMedioRegulacaoSUAS == '') { document.getElementById('<%=txtMedioRegulacaoSUAS.ClientID%>').value = '0'; txtMedioRegulacaoSUAS = '0'; }
            if (txtSuperiorRegulacaoSUAS == '') { document.getElementById('<%=txtSuperiorRegulacaoSUAS.ClientID%>').value = '0'; txtSuperiorRegulacaoSUAS = '0'; }
            //var valores = [txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica];
            PageMethods.CalcularTrabalhoSUAS(txtEscolarizacaoRegulacaoSUAS, txtFundamentalRegulacaoSUAS, txtMedioRegulacaoSUAS, txtSuperiorRegulacaoSUAS, function (val) {
                document.getElementById('<%=lblTotalRegulacaoSUAS.ClientID%>').innerText = val;
            });

                   
            var txtEscolarizacaoOutraEquipe = document.getElementById('<%=txtEscolarizacaoOutraEquipe.ClientID%>').value;
            var txtFundamentalOutraEquipe = document.getElementById('<%=txtFundamentalOutraEquipe.ClientID%>').value;
            var txtMedioOutraEquipe = document.getElementById('<%=txtMedioOutraEquipe.ClientID%>').value;
            var txtSuperiorOutraEquipe = document.getElementById('<%=txtSuperiorOutraEquipe.ClientID%>').value;
            if (txtEscolarizacaoOutraEquipe == '') { document.getElementById('<%=txtEscolarizacaoOutraEquipe.ClientID%>').value = '0'; txtEscolarizacaoOutraEquipe = '0' }
            if (txtFundamentalOutraEquipe == '') { document.getElementById('<%=txtFundamentalOutraEquipe.ClientID%>').value = '0'; txtFundamentalOutraEquipe = '0' }
            if (txtMedioOutraEquipe == '') { document.getElementById('<%=txtMedioOutraEquipe.ClientID%>').value = '0'; txtMedioOutraEquipe = '0'; }
            if (txtSuperiorOutraEquipe == '') { document.getElementById('<%=txtSuperiorOutraEquipe.ClientID%>').value = '0'; txtSuperiorOutraEquipe = '0'; }
            //var valores = [txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica];
            PageMethods.CalcularOutraEquipe(txtEscolarizacaoOutraEquipe, txtFundamentalOutraEquipe, txtMedioOutraEquipe, txtSuperiorOutraEquipe, function (val) {
                document.getElementById('<%=lblTotalOutraEquipe.ClientID%>').innerText = val;
            });

            var valoresEscolarizacao = [txtEscolarizacaoBasica, txtEscolarizacaoEspecial, txtEscolarizacaoSocioassistencial, txtEscolarizacaoGestaoSuas, txtEscolarizacaoTransferencia, txtEscolarizacaoCadUnico, txtEscolarizacaoGestaoFinanceira, txtEscolarizacaoSUAS, txtEscolarizacaoRegulacaoSUAS,txtEscolarizacaoOutraEquipe];
            PageMethods.CalcularValores(valoresEscolarizacao, function (val) {
                console.log(val);
                document.getElementById('<%=lblTotalEscolarizacao.ClientID%>').innerText = val;
            });

            var valoresFundamental = [txtFundamentalBasica, txtFundamentalEspecial, txtFundamentalSocioassistencial, txtFundamentalGestaoSuas, txtFundamentalTransferencia, txtFundamentalCadUnico, txtFundamentalGestaoFinanceira, txtFundamentalSUAS, txtFundamentalRegulacaoSUAS, txtFundamentalOutraEquipe];
            PageMethods.CalcularValores(valoresFundamental, function (val) {
                console.log(val);
                document.getElementById('<%=lblTotalFundamental.ClientID%>').innerText = val;
            });

            var valoresMedio = [txtMedioBasica, txtMedioEspecial, txtMedioSocioassistencial, txtMedioGestaoSuas, txtMedioTransferencia, txtMedioCadUnico, txtMedioGestaoFinanceira, txtMedioSUAS, txtMedioRegulacaoSUAS, txtMedioOutraEquipe];
            PageMethods.CalcularValores(valoresMedio, function (val) {
                console.log(val);
                document.getElementById('<%=lblTotalMedio.ClientID%>').innerText = val;
            });

            var valoresSuperior = [txtSuperiorBasica, txtSuperiorEspecial, txtSuperiorSocioassistencial, txtSuperiorGestaoSuas, txtSuperiorTransferencia, txtSuperiorCadUnico, txtSuperiorGestaoFinanceira, txtSuperiorSUAS, txtSuperiorRegulacaoSUAS, txtSuperiorOutraEquipe];
            PageMethods.CalcularValores(valoresSuperior, function (val) {
                console.log(val);
                document.getElementById('<%=lblTotalSuperior.ClientID%>').innerText = val;
            });


            var valores = [txtEscolarizacaoBasica, txtEscolarizacaoEspecial, txtEscolarizacaoSocioassistencial, txtEscolarizacaoGestaoSuas, txtEscolarizacaoTransferencia, txtEscolarizacaoCadUnico, txtEscolarizacaoGestaoFinanceira, txtEscolarizacaoSUAS, txtEscolarizacaoRegulacaoSUAS, txtEscolarizacaoOutraEquipe,
                txtFundamentalBasica, txtFundamentalEspecial, txtFundamentalSocioassistencial, txtFundamentalGestaoSuas, txtFundamentalTransferencia, txtFundamentalCadUnico, txtFundamentalGestaoFinanceira, txtFundamentalSUAS, txtFundamentalRegulacaoSUAS, txtFundamentalOutraEquipe,
                txtMedioBasica, txtMedioEspecial, txtMedioSocioassistencial, txtMedioGestaoSuas, txtMedioTransferencia, txtMedioCadUnico, txtMedioGestaoFinanceira, txtMedioSUAS, txtMedioRegulacaoSUAS, txtMedioOutraEquipe,
                txtSuperiorBasica, txtSuperiorEspecial, txtSuperiorSocioassistencial, txtSuperiorGestaoSuas, txtSuperiorTransferencia, txtSuperiorCadUnico, txtSuperiorGestaoFinanceira, txtSuperiorSUAS, txtSuperiorRegulacaoSUAS, txtSuperiorOutraEquipe];
            PageMethods.CalcularValores(valores, function (val) {
                console.log(val);
                document.getElementById('<%=lblTotal.ClientID%>').innerText = val;
            });

        }
    </script>

    <asp:UpdatePanel ID="pnlCadastro" runat="server" >
        <ContentTemplate>
            <br />
            <form name="frmOrgaoGestor">
                <div class="accordion" data-role="accordion" data-close-any="true">
                    <div class="frame" id="fraOrgaoGestor" runat="server">
                        <div class="heading">
                            1.3 - Identificação do Orgão Gestor da Assistência Social
                           <span class="mif-home icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Órgão Gestor" >
                                <div class="grid">
                                    <div class="row" id="alteracaoQuadro3" runat="server">
                                        <div class="cell" align="right">
                                            <a href="#" runat="server" id="linkAlteracoes" visible="false">
                                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado &nbsp;  &nbsp; 
                                            </a>
                                        </div>
                                    </div>
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>CNPJ:</b><br />
                                            <uc1:cnpj ID="txtCNPJ" runat="server" ToolTip="Informe o CNPJ do Orgão Gestor" />
                                        </div>
                                        <div class="cell">
                                            <b>Nome do Órgão Gestor da Assistência Social:</b><br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtNome" runat="server" Width="400px" MaxLength="120" ToolTip="Informe o CNPJ do Orgão Gestor"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row cells3">
                                        <div class="cell ">
                                            <b>O Órgão Gestor da Assistência Social é:</b><br />
                                            <div class="input-control text">
                                                <asp:DropDownList ID="ddlEstruturaOrgaoGestor" runat="server" Width="192px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlEstruturaOrgaoGestor_SelectedIndexChanged">
                                                </asp:DropDownList>

                                            </div>
                                            <div class="cell colspan2">
                                                &nbsp;&nbsp;<asp:Label ID="lblOutroOrgaoGestor" Text="Especificar:" runat="server"
                                                    Visible="false"></asp:Label><asp:TextBox ID="txtOutroOrgaoGestor" Visible="false"
                                                        runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row cell">
                                        <uc2:cep ID="cep1" runat="server" />
                                    </div>
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>Telefone fixo:</b><br />
                                            <uc3:telefone ID="telefone" runat="server" />
                                        </div>
                                        <div class="cell">
                                            <b>Telefone celular:</b><br />
                                            <uc5:celular ID="celular" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>E-mail institucional:</b><br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtEmail" runat="server" Width="200px" MaxLength="60"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Site :</b><br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtSite" runat="server" Width="150px" MaxLength="60"></asp:TextBox>
                                            </div>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                         <asp:CheckBox ID="chkPossuiSite" runat="server" AutoPostBack="true" Text="Não possui site"
                                             OnCheckedChanged="chkPossuiSite_CheckedChanged" />
                                        </div>
                                    </div>


                                    <fieldset class="border-blue ">
                                        <legend class="lgnd"><b class="fg-blue">Lei de criação do Órgão Gestor:</b></legend>
                                        <div class="row cells2">
                                            <div class="row cells2">
                                                <div class="cell">
                                                    <b>Nº da Lei:</b><br />
                                                    <asp:TextBox ID="txtNumeroLeiCriacaoOrgaoGestor" runat="server" Width="70px" MaxLength="5"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroLeiCriacaoOrgaoGestor"
                                                        runat="server" TargetControlID="txtNumeroLeiCriacaoOrgaoGestor" FilterType="Numbers" />
                                                    /
                                                    <asp:TextBox ID="txtAnoLeiCriacaoOrgaoGestor" runat="server" Width="42px" MaxLength="2"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAnoLeiCriacaoOrgaoGestor"
                                                        runat="server" TargetControlID="txtAnoLeiCriacaoOrgaoGestor" FilterType="Numbers" />
                                                    (Ex: 129/11)
                                                </div>
                                                <div class="cell">
                                                    <b>Data de publicação da Lei:</b><br />
                                                    <uc4:data ID="txtDtCriacaoOrgaoGestor" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row cells3">
                                                <div class="cell">
                                                    <b>
                                                        <asp:Label ID="lblHouveAlteracao" runat="server">Houve alteração na Lei de criação ?</asp:Label></b><br />
                                                    <asp:RadioButtonList ID="rblAlteracaoLei" runat="server" CssClass="RadioButton" RepeatDirection="Horizontal"
                                                        AutoPostBack="True" Height="16px" OnSelectedIndexChanged="rblAlteracaoLei_SelectedIndexChanged">
                                                        <asp:ListItem Text="Sim" Value="1" />
                                                        <asp:ListItem Text="Não" Value="0" Selected="True" />
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div id="tdLeiAlterada" runat="server" visible="false" class="cell">
                                                    <b>
                                                        <asp:Label ID="lblLeiAlteracao" runat="server">&nbsp;&nbsp;Nº da Lei de Alteração:</asp:Label></b><br />
                                                    <asp:TextBox ID="txtNumeroLei" runat="server" Width="70px" MaxLength="5"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroLei" runat="server"
                                                        TargetControlID="txtNumeroLei" FilterType="Numbers" />
                                                    /
                                                <asp:TextBox ID="txtAnoLei" runat="server" Width="42px" MaxLength="2"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAnoLei" runat="server"
                                                        TargetControlID="txtAnoLei" FilterType="Numbers" />
                                                    (Ex: 129/11)
                                                </div>
                                                <div id="tdDataLeiAlterada" runat="server" class="cell" visible="false">
                                                    <b>
                                                        <asp:Label ID="lblDataAlteracao" runat="server">Data de publicação da Lei: </asp:Label></b><br />
                                                    <uc4:data ID="txtDataAlteracao" runat="server" />
                                                </div>
                                            </div>
                                    </fieldset>

                                    <fieldset class="border-blue ">
                                        <legend class="lgnd"><b class="fg-blue">Lei do SUAS:</b></legend>
                                            <div class="row cells3">
                                                <div class="cell">
                                                    <b>
                                                        <asp:Label ID="lblLeiDoSuas" runat="server">O município possui Lei do SUAS ?</asp:Label></b><br />
                                                    <asp:RadioButtonList ID="rblLeiDoSuas" runat="server" CssClass="RadioButton" RepeatDirection="Horizontal" AutoPostBack="True" Height="16px" OnSelectedIndexChanged="rblLeiDoSuas_SelectedIndexChanged">
                                                        <asp:ListItem Text="Sim" Value="1" />
                                                        <asp:ListItem Text="Não" Value="0" Selected="True" />
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div id="tdLeiDoSuas" runat="server" visible="false" class="cell">
                                                    <b>
                                                        <asp:Label ID="lblNumeroLeiDoSuas" runat="server">&nbsp;&nbsp;Nº da Lei:</asp:Label></b><br />
                                                    <asp:TextBox ID="txtNumeroLeiSuas" runat="server" Width="70px" MaxLength="5"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroLeiSuas" runat="server"
                                                        TargetControlID="txtNumeroLeiSuas" FilterType="Numbers" />
                                                    /
                                                <asp:TextBox ID="txtAnoLeiSuas" runat="server" Width="42px" MaxLength="2"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtAnoLeiSuas" runat="server"
                                                        TargetControlID="txtAnoLeiSuas" FilterType="Numbers" />
                                                    (Ex: 129/11)
                                                </div>
                                                <div id="tdDataLeiSuas" runat="server" class="cell" visible="false">
                                                    <b>
                                                        <asp:Label ID="lblDataPublicacaoLeiDoSuas" runat="server">Data de publicação da Lei: </asp:Label></b><br />
                                                    <uc4:data ID="txtDataPublicacaoLei" runat="server" />
                                                </div>
                                            </div>
                                    </fieldset>

                                    <div class="row">
                                        <div class="cell">
                                            <br />
                                            <asp:Button ID="btnSalvarOrgaoGestorAS" runat="server" SkinID="button-save" Text="Salvar" OnClick="btnSalvarOrgaoGestorDadoBasico_Click"></asp:Button>
                                        </div>

                                        <div class="row">
                                            <div class="cell">
                                                <table id="tblInconsistenciaIdentificacaoOrgaoGestorAS" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                                    width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                                                    <tr>
                                                        <td style="padding: 15px 10px 2px 15px">
                                                            <span class="mif-warning mif-2x"></span>
                                                            <b style='color: #000000 !important'>Verifique
                                                            as inconsistências:</b>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 10px 10px 12px 45px;">
                                                            <asp:Label ID="lblInconsistenciaIdentificacaoOrgaoGestorAS" ForeColor="Red" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="frame" id="fraRH" runat="server">
                    <div class="heading">
                        1.4 - Estrutura e Recursos humanos do órgão gestor
                           <span class="mif-home icon"></span>
                    </div>
                    <div class="content">
                        <div class="formInput" data-text="Recursos Humanos">
                            <div class="grid">
                                <div class="row no-margin-bottom" runat="server" visible="false" id="alteracoesQuadro5">
                                    <div class="cell" align="right">
                                        <a href="#" runat="server" id="linkAlteracoesQuadro5" visible="false">
                                            <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                        </a>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="row cell-auto-size">
                                        Caso o Órgão Gestor não seja exclusivo da política de Assistência Social, informe apenas o número de trabalhadores vinculados à Assistência Social e não o número total de trabalhadores.<br />
                                        Preencha o quadro abaixo indicando a existência ou não de cada uma das equipes citadas e informando o número de trabalhadores de cada uma delas, segundo a escolaridade.
                                    </div>
                                </div>

                                <div class="row">
                                    <div id="Quadrienal" >
                                        <asp:Button ID="btnExercicio1" runat="server" Width="113px" OnClick="btnLoadExercicio1_Click"></asp:Button>
                                        <asp:Button ID="btnExercicio2" runat="server" Width="113px" OnClick="btnLoadExercicio2_Click"></asp:Button>
                                        <asp:Button ID="btnExercicio3" runat="server" Width="113px" OnClick="btnLoadExercicio3_Click"></asp:Button>
                                        <asp:Button ID="btnExercicio4" runat="server" Width="113px" OnClick="btnLoadExercicio4_Click"></asp:Button>
                                    </div>
                                    <div class="cell">
                                        <table class="table striped border bordered" cellspacing="0"
                                            cellpadding="0" border="0">
                                            <thead class="info">
                                                <tr>
                                                    <th width="250" rowspan="2" colspan="2">O  Órgão Gestor mantém equipe específica para:</th>
                                                    <th colspan="5">N° de trabalhadores (não incluir voluntários ou estagiários)</th>
                                                </tr>
                                                <tr>
                                                    <th width="90" text-align="center">Sem escolarização
                                                    </th>
                                                    <th width="90">Nível fundamental
                                                    </th>
                                                    <th width="90">Nível médio
                                                    </th>
                                                    <th width="90">Nível superior
                                                    </th>
                                                    <th width="90">Total
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Proteção Social Básica
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rblEquipeBasica" runat="server"
                                                            OnSelectedIndexChanged="rblEquipeBasica_SelectedIndexChanged"
                                                            Width="100" RepeatDirection="Horizontal" AutoPostBack="true">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEscolarizacaoBasica" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFundamentalBasica" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMedioBasica" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSuperiorBasica" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 16px;">
                                                        <strong>
                                                            <asp:Label ID="lblTotalBasica" Text="0" runat="server"></asp:Label></strong>
                                                    </td>
                                                </tr>
                                                <tr id="trEstruturarBasica" runat="server" visible="false" style="height: 22px;">
                                                    <td colspan="5" align="right"><b>Existe intenção de estruturar esta equipe no órgão gestor nos próximos anos?</b> </td>
                                                    <td colspan="2">
                                                        <asp:RadioButtonList ID="rblEstruturacaoBasica" runat="server"
                                                            Width="100" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr class="info">
                                                    <td>Proteção Social Especial
                                                    </td>
                                                    <td> 
                                                        <asp:RadioButtonList ID="rblEquipeEspecial" runat="server"
                                                            OnSelectedIndexChanged="rblEquipeEspecial_SelectedIndexChanged"
                                                            Width="100" RepeatDirection="Horizontal" AutoPostBack="True">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEscolarizacaoEspecial" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFundamentalEspecial" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMedioEspecial" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSuperiorEspecial" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 16px;">
                                                        <strong>
                                                            <asp:Label ID="lblTotalEspecial" runat="server" Enabled="false" Text="0"></asp:Label></strong>
                                                    </td>
                                                </tr>
                                                <tr id="trEstruturarEspecial" class="info" runat="server" visible="false">
                                                    <td colspan="5" align="right"><b>Existe intenção de estruturar esta equipe no órgão gestor nos próximos anos?</b> </td>
                                                    <td colspan="2">
                                                        <asp:RadioButtonList ID="rblEstruturacaoEspecial" runat="server"
                                                            Width="100" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Vigilância Socioassistencial
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rblEquipeSocioassistencial" runat="server" Width="100"
                                                            OnSelectedIndexChanged="rblEquipeSocioassistencial_SelectedIndexChanged"
                                                            RepeatDirection="Horizontal" AutoPostBack="True">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEscolarizacaoSocioassistencial" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFundamentalSocioassistencial" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMedioSocioassistencial" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSuperiorSocioassistencial" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 16px;">
                                                        <strong>
                                                            <asp:Label ID="lblTotalSocioassistencial" runat="server" Text="0"></asp:Label></strong>
                                                    </td>
                                                </tr>

                                                <tr id="trEstruturarSocioassistencial" runat="server" visible="false">
                                                    <td colspan="5" align="right"><b>Existe intenção de estruturar esta equipe no órgão gestor nos próximos anos?</b> </td>
                                                    <td colspan="2">
                                                        <asp:RadioButtonList ID="rblEstruturacaSocioassistencial" runat="server"
                                                            Width="100" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>

                                                <tr class="info">
                                                    <td>Gestão do SUAS</td>

                                                    <td>
                                                        <asp:RadioButtonList ID="rblGestaoSuas" runat="server" Width="100"
                                                            OnSelectedIndexChanged="rblGestaoSuas_SelectedIndexChanged"
                                                            RepeatDirection="Horizontal" AutoPostBack="True">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEscolarizacaoGestaoSuas" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFundamentalGestaoSuas" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMedioGestaoSuas" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSuperiorGestaoSuas" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 16px;">
                                                        <strong>
                                                            <asp:Label ID="lblTotalGestaoSuas" runat="server" Text="0"></asp:Label></strong>
                                                    </td>
                                                </tr>

                                                <tr id="trGestaoDoSuas" runat="server" visible="false">
                                                    <td colspan="5" align="right"><b>Existe intenção de estruturar esta equipe no órgão gestor nos próximos anos?</b> </td>
                                                    <td colspan="2">
                                                        <asp:RadioButtonList ID="rblGestaoDoSuas" runat="server"
                                                            Width="100" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>

                                                <tr class="info">
                                                    
                                                    <td>Gestão de Benefícios/Transferência de Renda
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rdlEquipeTransferencia" runat="server" Width="100"
                                                            OnSelectedIndexChanged="rdlEquipeTransferencia_SelectedIndexChanged"
                                                            RepeatDirection="Horizontal" AutoPostBack="True" onclick="">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEscolarizacaoTransferencia" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFundamentalTransferencia" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMedioTransferencia" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSuperiorTransferencia" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 16px;">
                                                        <strong>
                                                            <asp:Label ID="lblTotalTransferencia" runat="server" Text="0"></asp:Label></strong>
                                                    </td>
                                                </tr>

                                                <tr id="trEstruturarTransferencia" runat="server" class="info" visible="false">
                                                    <td colspan="5" align="right"><b>Existe intenção de estruturar esta equipe no órgão gestor nos próximos anos?</b> </td>
                                                    <td colspan="2">
                                                        <asp:RadioButtonList ID="rblEstuturacaoTransferencia" runat="server"
                                                            Width="100" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Gestão do Cadastro Único
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rblEquipeCadUnico" runat="server" Width="100"
                                                            OnSelectedIndexChanged="rblEquipeCadUnico_SelectedIndexChanged"
                                                            RepeatDirection="Horizontal" AutoPostBack="True">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEscolarizacaoCadUnico" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFundamentalCadUnico" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMedioCadUnico" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSuperiorCadUnico" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 16px;">
                                                        <strong>
                                                            <asp:Label ID="lblTotalCadUnico" runat="server" Text="0"></asp:Label></strong>
                                                    </td>
                                                </tr>
                                                <tr id="trEstruturarCadUnico" runat="server" visible="false">
                                                    <td colspan="5" align="right"><b>Existe intenção de estruturar esta equipe no órgão gestor nos próximos anos?</b> </td>
                                                    <td colspan="2">
                                                        <asp:RadioButtonList ID="rblEstruturacaoCadUnico" runat="server"
                                                            Width="100" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr class="info">
                                                    <td>Gestão Financeira e Orçamentária
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rblEquipeGestaoFinanceira" runat="server" Width="100"
                                                            OnSelectedIndexChanged="rblEquipeGestaoFinanceira_SelectedIndexChanged"
                                                            RepeatDirection="Horizontal" AutoPostBack="True">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEscolarizacaoGestaoFinanceira" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFundamentalGestaoFinanceira" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMedioGestaoFinanceira" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSuperiorGestaoFinanceira" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 16px;">
                                                        <strong>
                                                            <asp:Label ID="lblTotalGestaoFinanceira" Text="0" runat="server"></asp:Label></strong>
                                                    </td>
                                                </tr>
                                                <tr id="trEstruturarGestaoFinanceira" runat="server" class="info" visible="false">
                                                    <td colspan="5" align="right"><b>Existe intenção de estruturar esta equipe no órgão gestor nos próximos anos?</b> </td>
                                                    <td colspan="2">
                                                        <asp:RadioButtonList ID="rblEstruturarGestaoFinanceira" runat="server"
                                                            Width="100" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Gestão do Trabalho no SUAS
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rblEquipeGestaoSuas" runat="server" Width="100"
                                                            OnSelectedIndexChanged="rblEquipeGestaoSuas_SelectedIndexChanged"
                                                            RepeatDirection="Horizontal" AutoPostBack="true">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEscolarizacaoSUAS" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFundamentalSUAS" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMedioSUAS" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSuperiorSUAS" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 16px;">
                                                        <strong>
                                                            <asp:Label ID="lblTotalSUAS" runat="server" Text="0"></asp:Label></strong>
                                                    </td>
                                                </tr>
                                                <tr id="trEstruturarSuas" runat="server" visible="false">
                                                    <td colspan="5" align="right"><b>Existe intenção de estruturar esta equipe no órgão gestor nos próximos anos?</b> </td>
                                                    <td colspan="2">
                                                        <asp:RadioButtonList ID="rblEstruturarGestaoSuas" runat="server"
                                                            Width="100" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr class="info">
                                                    <td>Regulação do Suas
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rblEquipeRegulacaoSUAS" runat="server" Width="100"
                                                            RepeatDirection="Horizontal" AutoPostBack="True"
                                                            OnSelectedIndexChanged="rblEquipeRegulacaoSUAS_SelectedIndexChanged">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEscolarizacaoRegulacaoSUAS" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFundamentalRegulacaoSUAS" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMedioRegulacaoSUAS" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSuperiorRegulacaoSUAS" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 16px;">
                                                        <strong>
                                                            <asp:Label ID="lblTotalRegulacaoSUAS" Text="0" runat="server"></asp:Label></strong>
                                                    </td>
                                                </tr>
                                                <tr id="trEstruturarRegulacaoSUAS" class="info" runat="server" visible="false">
                                                    <td colspan="5" align="right"><b>Existe intenção de estruturar esta equipe no órgão gestor nos próximos anos?</b> </td>
                                                    <td colspan="2">
                                                        <asp:RadioButtonList ID="rblEstruturarRegulacaoSUAS" runat="server"
                                                            Width="100" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
<%--                                           <tr>
                                                    <td>Execução dos serviços socioassistenciais da rede direta
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rblEquipeRedeDireta" runat="server" Width="100"
                                                            RepeatDirection="Horizontal" AutoPostBack="true"
                                                            OnSelectedIndexChanged="rblEquipeRedeDireta_SelectedIndexChanged">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                              <td>
                                                        <asp:TextBox ID="txtEscolarizacaoRedeDireta" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFundamentalRedeDireta" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMedioRedeDireta" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSuperiorRedeDireta" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 16px;">
                                                        <strong>
                                                            <asp:Label ID="lblTotalRedeDireta" runat="server" Text="0"></asp:Label></strong>
                                                    </td>
                                                </tr>--%>
                                                <tr id="trEstruturarRedeDireta" runat="server" visible="false">
                                                    <td colspan="5" align="right"><b>Existe intenção de estruturar esta equipe no órgão gestor nos próximos anos?</b> </td>
                                                    <td colspan="2">
                                                        <asp:RadioButtonList ID="rblEstruturarRedeDireta" runat="server"
                                                            Width="100" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Existem trabalhadores que não pertencem a nenhuma das equipes citadas acima ou que trabalham em diversas destas equipes concomitantemente?
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rblOutrasEquipes" runat="server" Width="100"
                                                            OnSelectedIndexChanged="rblOutrasEquipes_SelectedIndexChanged"
                                                            RepeatDirection="Horizontal" AutoPostBack="True">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEscolarizacaoOutraEquipe" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFundamentalOutraEquipe" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMedioOutraEquipe" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSuperiorOutraEquipe" Width="60" MaxLength="4" runat="server" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 16px;">
                                                        <strong>
                                                            <asp:Label ID="lblTotalOutraEquipe" runat="server" Text="0"></asp:Label></strong>
                                                    </td>
                                                </tr>
                                                <tr id="trEstruturarOutraEquipe" runat="server" visible="false">
                                                    <td colspan="5" align="right"><b>Existe intenção de estruturar esta equipe no órgão gestor nos próximos anos?</b> </td>
                                                    <td colspan="2">
                                                        <asp:RadioButtonList ID="rblEstruturarOutraEquipe" runat="server"
                                                            Width="100" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </tbody>
                                            <tfoot>
                                                <tr class="info">
                                                    <td colspan="2" align="right"><strong>Total</strong>
                                                    </td>
                                                    <td>
                                                        <strong>
                                                            <asp:Label ID="lblTotalEscolarizacao" runat="server" Text="0"></asp:Label></strong>
                                                    </td>
                                                    <td>
                                                        <strong>
                                                            <asp:Label ID="lblTotalFundamental" runat="server" Text="0"></asp:Label></strong>
                                                    </td>
                                                    <td>
                                                        <strong>
                                                            <asp:Label ID="lblTotalMedio" runat="server" Text="0"></asp:Label></strong>
                                                    </td>
                                                    <td>
                                                        <strong>
                                                            <asp:Label ID="lblTotalSuperior" runat="server" Text="0"></asp:Label></strong>
                                                    </td>
                                                    <td>
                                                        <strong>
                                                            <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label></strong>
                                                    </td>
                                                </tr>
                                            </tfoot>
                                        </table>
                                    </div>
                                </div>
                                <div class="row cells2">
                                    <div class="cell">
                                        <div class="row">
                                            <div class="cell">
                                                <b>Indique a área de formação dos trabalhadores que possuem nível superior:</b>
                                            </div>
                                        </div>
                                        <div class="row cells2">
                                            <div class="cell">
                                                <asp:TextBox ID="txtSuperiorServicoSocial" runat="server" Width="60px" MaxLength="4"></asp:TextBox>&nbsp;Serviço Social
                                                                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorServicoSocial"
                                                                         runat="server" TargetControlID="txtSuperiorServicoSocial" FilterType="Numbers" />
                                            </div>
                                            <div class="cell">
                                                <asp:TextBox ID="txtSuperiorAdministracao" runat="server" Width="60px" MaxLength="4"></asp:TextBox>&nbsp;Administração
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorAdministracao"
                                                                        runat="server" TargetControlID="txtSuperiorAdministracao" FilterType="Numbers" />
                                            </div>
                                        </div>
                                        <div class="row cells2">
                                            <div class="cell">
                                                <asp:TextBox ID="txtSuperiorPsicologia" runat="server" CssClass="campoTexto" Width="60px"
                                                    MaxLength="4" AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Psicologia
                                                                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorPsicologia"
                                                                         runat="server" TargetControlID="txtSuperiorPsicologia" FilterType="Numbers" />
                                            </div>
                                            <div class="cell">
                                                <asp:TextBox ID="txtSuperiorAntropologia" runat="server" Width="60px" MaxLength="4"></asp:TextBox>&nbsp;Antropologia
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorAntropologia"
                                                                        runat="server" TargetControlID="txtSuperiorAntropologia" FilterType="Numbers" />
                                            </div>
                                        </div>
                                        <div class="row cells2">
                                            <div class="cell">
                                                <asp:TextBox ID="txtSuperiorPedagogia" runat="server" CssClass="campoTexto" Width="60px"
                                                    MaxLength="4" AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Pedagogia
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorPedagogia"
                                                    runat="server" TargetControlID="txtSuperiorPedagogia" FilterType="Numbers" />
                                            </div>
                                            <div class="cell">
                                                <asp:TextBox ID="txtSuperiorContabilidade" runat="server" Width="60px" MaxLength="4"></asp:TextBox>&nbsp;Contabilidade
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorContabilidade"
                                                    runat="server" TargetControlID="txtSuperiorContabilidade" FilterType="Numbers" />
                                            </div>
                                        </div>
                                        <div class="row cells2">
                                            <div class="cell">
                                                <asp:TextBox ID="txtSociologia" runat="server" CssClass="campoTexto" Width="60px"
                                                    MaxLength="4" AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Sociologia
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSociologia" runat="server"
                                                    TargetControlID="txtSociologia" FilterType="Numbers" />
                                            </div>
                                            <div class="cell">
                                                <asp:TextBox ID="txtSuperiorEconomia" runat="server" Width="60px" MaxLength="4"></asp:TextBox>&nbsp;Economia
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorEconomia"
                                                    runat="server" TargetControlID="txtSuperiorEconomia" FilterType="Numbers" />
                                            </div>
                                        </div>
                                        <div class="row cells2">
                                            <div class="cell">
                                                <asp:TextBox ID="txtDireito" runat="server" CssClass="campoTexto" Width="60px" MaxLength="4"
                                                    AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Direito
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtDireito" runat="server"
                                                    TargetControlID="txtDireito" FilterType="Numbers" />
                                            </div>
                                            <div class="cell">
                                                <asp:TextBox ID="txtSuperiorTerapiaOcupacional" runat="server" Width="60px" MaxLength="4"></asp:TextBox>&nbsp;Terapia
                                                Ocupacional
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorTerapiaOcupacional"
                                                    runat="server" TargetControlID="txtSuperiorTerapiaOcupacional" FilterType="Numbers" />
                                            </div>
                                      
                                        </div>

                                        <div class="row cells2">
                                            <div class="cell">
                                                <asp:TextBox ID="txtSuperiorEconomiaDomestica" runat="server" CssClass="campoTexto"
                                                    Width="60px" MaxLength="4" AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Economia
                                                Doméstica
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtSuperiorEconomiaDomestica"
                                                    runat="server" TargetControlID="txtSuperiorEconomiaDomestica" FilterType="Numbers" />
                                            </div>
                                            <div class="cell">
                                                <asp:TextBox id="txtOutros" runat="server" onchange="javascript: hideOrShow(this);" CssClass="campoTexto" 
                                                    Width="60px" MaxLength="4" AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Outros
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtOutros"
                                                    runat="server" TargetControlID="txtOutros" FilterType="Numbers" />
                                            </div>
                                        </div>
                                              
                                    </div>
                                    <div class="cell">
                                        <div class="row">
                                            <div class="cell">
                                                <b>Indique o tipo de vínculo dos trabalhadores:</b>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell">
                                                <asp:TextBox ID="txtEstatutarios" runat="server" CssClass="campoTexto" Width="60px"
                                                    MaxLength="4"></asp:TextBox>&nbsp;Estatutários
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                                                                            runat="server" TargetControlID="txtEstatutarios" FilterType="Numbers" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell">
                                                <asp:TextBox ID="txtCeletistas" runat="server" Text="" CssClass="campoTexto" Width="60px"></asp:TextBox>&nbsp;Empregados públicos celetistas 
                                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                                            TargetControlID="txtCeletistas" FilterType="Numbers" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell">
                                                <asp:TextBox ID="txtComissionados" runat="server" Text="" CssClass="campoTexto" Width="60px"></asp:TextBox>&nbsp; Apenas comissionados 
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                    TargetControlID="txtComissionados" FilterType="Numbers" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell">
                                                <asp:TextBox ID="txtOutrosVinculos" runat="server" Text="" CssClass="campoTexto" Width="60px"></asp:TextBox>&nbsp;Outros vínculos trabalhistas 
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                    TargetControlID="txtOutrosVinculos" FilterType="Numbers" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell">
                                                <asp:TextBox ID="txtEstagiarios" runat="server" Text="" CssClass="campoTexto" Width="60px"></asp:TextBox>&nbsp;Estagiários
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                    TargetControlID="txtEstagiarios" FilterType="Numbers" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell">
                                                <asp:TextBox ID="txtVoluntarios" runat="server" Text="" CssClass="campoTexto" Width="60px"></asp:TextBox>&nbsp;Voluntários
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                    TargetControlID="txtVoluntarios" FilterType="Numbers" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                               
                                <div class="row" >
                                            <div class="cell" id="txtEspecificarOutros" runat="server">
                                                <b>Especificar (Outros)</b><br />
                                                <div class="input-control text full-size">
                                                    <asp:TextBox ID="lblEspecificarOutros" MaxLength="60" OnChange ="hideOrShow()" runat="server" CssClass="campoTexto"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                 <div class="row" aling="center">
                                    <div class="cell2">
                                        <br />
                                        Existe intenção de aumentar o número de trabalhadores do órgão gestor nos próximos anos?
                                    <br />
                                        <asp:RadioButtonList ID="rblAumentarEquipe" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Sim" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Não" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="cell">
                                        <br />
                                        <asp:Button ID="btnSalvar" runat="server" SkinID="button-save" Text="Salvar" OnClick="btnSalvar_Click"></asp:Button>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                            width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                                            <tr>
                                                <td style="padding: 15px 10px 2px 15px">
                                                    <span class="mif-warning mif-2x"></span>
                                                    <%-- <img src="../Styles/Icones/messagebox_warning.png" align="absMiddle" />--%><b style='color: #000000 !important'>Verifique
                                                    as inconsistências:</b>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 10px 10px 12px 45px;">
                                                    <asp:Label ID="lblInconsistencias" ForeColor="Red" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>




                <div class="frame" id="fraGestor" runat="server">
                    <div class="heading">
                        <a href="#" runat="server" id="linkAlteracoesQuadro7" visible="false">
                            <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                        </a>
                        1.5 - Identificação do Gestor Municipal de Assistência Social
                             <span class="mif-home icon"></span>
                    </div>
                    <div class="content">
                        <div class="formInput" data-text="Gestor Municipal">
                            <div class="grid">
                                <div class="row cells3">
                                    <div class="cell">
                                        <b>Nome</b><br />
                                        <div class="input-control select full-size">
                                            <asp:DropDownList ID="ddlUsuario" runat="server" Width="250px">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="cell">
                                        <b>CPF</b><br />
                                        <div class="input-control text">
                                            <uc2:cpf ID="txtCPF" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row cells4">
                                    <div class="cell">
                                        <div class="input-control">
                                            <b>RG:</b><br />
                                            <uc1:rg ID="txtRG" runat="server" />
                                        </div>
                                    </div>
                                    <div class="cell">
                                        <b>Data da emissão:</b><br />
                                        <uc4:data ID="txtDataEmissao" runat="server" />
                                    </div>
                                    <div class="cell">
                                        <b>Sigla do órgão emissor:</b><br />
                                        <div class="input-control text">
                                            <asp:TextBox ID="txtOrgEmissor" runat="server" Width="70px" MaxLength="6"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="cell">
                                        <b>U.F.:</b><br />
                                        <div class="input-control select">
                                            <asp:DropDownList ID="ddlUFEmissor" Height="33px" runat="server">
                                                <asp:ListItem Value="0" Text="[Escolha uma Opção]" Selected="True" />
                                                <asp:ListItem Value="1" Text="AC" />
                                                <asp:ListItem Value="2" Text="AL" />
                                                <asp:ListItem Value="3" Text="AM" />
                                                <asp:ListItem Value="4" Text="AP" />
                                                <asp:ListItem Value="5" Text="BA" />
                                                <asp:ListItem Value="6" Text="CE" />
                                                <asp:ListItem Value="7" Text="DF" />
                                                <asp:ListItem Value="8" Text="ES" />
                                                <asp:ListItem Value="9" Text="GO" />
                                                <asp:ListItem Value="10" Text="MA" />
                                                <asp:ListItem Value="11" Text="MG" />
                                                <asp:ListItem Value="12" Text="MS" />
                                                <asp:ListItem Value="13" Text="MT" />
                                                <asp:ListItem Value="14" Text="PA" />
                                                <asp:ListItem Value="15" Text="PB" />
                                                <asp:ListItem Value="16" Text="PE" />
                                                <asp:ListItem Value="17" Text="PI" />
                                                <asp:ListItem Value="18" Text="PR" />
                                                <asp:ListItem Value="19" Text="RJ" />
                                                <asp:ListItem Value="20" Text="RN" />
                                                <asp:ListItem Value="21" Text="RO" />
                                                <asp:ListItem Value="22" Text="RR" />
                                                <asp:ListItem Value="23" Text="RS" />
                                                <asp:ListItem Value="24" Text="SC" />
                                                <asp:ListItem Value="25" Text="SE" />
                                                <asp:ListItem Value="26" Text="SP" />
                                                <asp:ListItem Value="27" Text="TO" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row cells2">
                                    <div class="cell">
                                        <b>Cargo:</b><br />
                                        <div class="input-control select">
                                            <asp:DropDownList ID="ddlCargo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCargo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="cell" id="trOutros" runat="server" visible="false">
                                        <b>Especificar:</b>
                                        <div class="input-control text">
                                            <asp:TextBox ID="txtCargoOutro" runat="server" MaxLength="60" Width="200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <fieldset class="border-blue">
                                        <legend class="lgnd"><b class="fg-blue">Contato Institucional</b></legend>
                                        <div class="row cells2">
                                            <div class="cell">
                                                <b>Telefone fixo:</b><br />
                                                <uc3:telefone ID="txtTelefone" runat="server" />
                                            </div>
                                            <div class="cell">
                                                <b>Telefone celular:</b><br />
                                                <uc5:celular ID="txtCelular" runat="server" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell">
                                                <b>E-mail:</b><br />
                                                <div class="input-control text mid-size">
                                                    <asp:TextBox ID="txtEmailGestor" runat="server" MaxLength="60" CausesValidation="True"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldse>
                                </div>
                                <div class="row">
                                    <fieldset class="border-blue ">
                                        <legend class="lgnd"><b class="fg-blue">Período de gestão</b></legend>
                                        <div class="row cells2">
                                            <div class="cell">
                                                <b>Decreto / Portaria de nomeação:</b><br />
                                                <div class="input-control text mid-size">
                                                    <asp:TextBox ID="txtDecretoPortariaGestor" runat="server" MaxLength="8" />
                                                </div>
                                            </div>
                                            <div class="cell">
                                                <b>Data de publicação do Decreto/ Portaria:</b><br />
                                                <div class="input-control text">
                                                    <uc4:data ID="txtDataDecretoGestor" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells3">
                                            <div class="cell">
                                                <b>Data de início:</b><br />
                                                <uc4:data ID="txtdata" runat="server" />
                                            </div>
                                            <div class="cell">
                                                <b>Data de término:</b><br />
                                                <uc4:data ID="txtDataTerminoGestao" runat="server" Enabled="false" />
                                            </div>
                                            <div class="cell">
                                                <br />
                                                <asp:Button ID="btnSalvarTerminoGestao" runat="server" Text="Finalizar" SkinID="button-save" Enabled="false" OnClick="btnSalvarTerminoGestao_Click" CausesValidation="false"></asp:Button>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="row">
                                    <fieldset class="border-blue">
                                        <legend class="lgnd"><b class="fg-blue">Escolaridade e formação</b></legend>
                                        <div class="row cells3">
                                            <div class="cell">
                                                <b>Escolaridade</b><br />
                                                <div class="input-control select">
                                                    <asp:DropDownList ID="ddlEscolaridade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEscolaridade_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="cell" id="tdFormacaoAcademica" runat="server" visible="false">
                                                <b>Área de formação acadêmica</b><br />
                                                <div class="input-control select mid-size">
                                                    <asp:DropDownList ID="ddlFormacaoAcademica" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFormacaoProfissional_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="cell" id="trOutraFormacao" runat="server" visible="false">
                                                <b>Especificar</b><br />
                                                <div class="input-control text full-size">
                                                    <asp:TextBox ID="txtOutraAreaFormacao" MaxLength="60" runat="server" CssClass="campoTexto"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="row cells3">
                                    <div class="cell">
                                        <asp:Button ID="btnSalvarGestor" runat="server" SkinID="button-save" Text="Salvar" OnClick="btnSalvarGestor_Click"
                                            ValidationGroup="vgCampos"></asp:Button>
                                    </div>
                                    <div class="cell">
                                        <asp:Button ID="btnEditar" runat="server" SkinID="button-save" Text="Atualizar dados do gestor atual"
                                            OnClick="btnEditar_Click" CausesValidation="false" CssClass="btn btn-primary button-edit"></asp:Button>
                                    </div>
                                    <div class="cell">
                                        <asp:Button ID="btnSubstituir" runat="server" SkinID="button-save" Width="230px" Text="Registrar dados do novo gestor"
                                            OnClick="btnSubstituir_Click" CausesValidation="false"></asp:Button>
                                    </div>
                                </div>
                                <div class="row">
                                    <fieldset class="border-blue">
                                        <legend class="lgnd"><b class="fg-blue">Gestores municipais de Assistência Social anteriores</b></legend>
                                        <a href="#" runat="server" id="linkAlteracoesQuadro8" visible="false">
                                            <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                        </a>&nbsp;
                                <%--<div class="grid">--%>
                                        <%--  <div class="subheader">
                                    </div>--%>
                                        <div class="row">
                                            <div class="cell">
                                                <asp:ListView ID="lstGestores" runat="server" OnItemCommand="lstGestores_ItemCommand"
                                                    DataKeyNames="Id" OnItemDataBound="lstGestores_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <table class="table striped border" cellspacing="0"
                                                            cellpadding="0" border="0" width="100%">
                                                            <thead class="info">
                                                                <tr>
                                                                    <th width="20" style="height: 22px;"></th>
                                                                    <th width="250">Nome
                                                                    </th>
                                                                    <th width="180">Período de gestão
                                                                    </th>
                                                                    <th width="100">Excluir
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <%--    <tr class="jqgfirstrow" style="height: auto;">
                                                                    <td style="height: 0px;"></td>
                                                                    <td style="height: 0px;"></td>
                                                                    <td style="height: 0px;"></td>
                                                                    <td style="height: 0px;"></td>
                                                                </tr>--%>
                                                                <tr id="itemPlaceholder" runat="server">
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="height: 22px;">
                                                                <asp:Label ID="lblSequencia" runat="server" />
                                                            </td>
                                                            <td align="left">
                                                                <%#DataBinder.Eval(Container.DataItem, "Nome") %></a>
                                                            </td>
                                                            <td align="center">
                                                                <%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DataNomeacao")).ToString("dd/MM/yyyy") %>
                                        -
                                        <%#DataBinder.Eval(Container.DataItem, "DataTerminoGestao") == null ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DataTerminoGestao")).ToString("dd/MM/yyyy") %>
                                                            </td>
                                                            <td align="center">
                                                                <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                    CommandName="Excluir_Gestor" CausesValidation="false" OnClientClick="return confirm('Deseja realmente remover o gestor municipal?');" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <EmptyDataTemplate>
                                                        <div align="center" style="width: 100%;">
                                                            <b class="titulo">Não existe registro de outros gestores neste período</b>
                                                        </div>
                                                    </EmptyDataTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                        <%-- </div>--%>
                                    </fieldset>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <table id="tbInconsistenciasGestor" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                            width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                                            <tr>
                                                <td style="padding: 15px 10px 2px 15px">
                                                    <span class="mif-warning mif-2x"></span>
                                                    <b style='color: #000000 !important'>Verifique
                                                    as inconsistências:</b>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 10px 10px 12px 45px;">
                                                    <asp:Label ID="lblInconsistenciasGestor" ForeColor="Red" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>





            </form>
            <table width="100%" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="FPrefeitura.aspx">
                            <span class="mif-arrow-left"></span>
                            Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="FFundoMunicipal.aspx">Próximo
                            <%--<img src="../Styles/Icones/forward.png" align="absMiddle" border="0" />--%><span class="mif-arrow-right"></span></a>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdfIdGestor" runat="server" Value="0" />
            <asp:HiddenField ID="hdfIdOrgaoGestor" runat="server" Value="0" />
            <asp:HiddenField ID="hdfAno" runat="server" Value="2022" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
