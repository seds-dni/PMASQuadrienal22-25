<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FPrevisaoOrcamentaria.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoV.FPrevisaoOrcamentaria" %>

<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmConselhos">
                <div class="accordion" data-role="accordion" data-close-any="true">
                    <div class="frame">
                        <div class="heading">
                            5.1.a - Previsão DE COFINANCIAMENTO E REPASSES para 2021 (valores anuais)
                             <a href="#" runat="server" id="A1" visible="false">
                                 <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                             </a>
                            <span class="mif-dollar2 icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="previsão orçamentária">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            Os valores constantes nas tabelas abaixo referem-se à previsão orçamentária elaborada no ano de 2021 para o PMAS 2022, devendo ser utilizados apenas como referência para a elaboração do PMAS atual.
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:ListView ID="lvPrevisaoOrcamentariaExercicio0" runat="server" DataKeyNames="IdTipoProtecao"
                                                OnItemDataBound="lstPrevisaoOrcamentariaExercicio0_ItemDataBound">
                                                <LayoutTemplate>
                                                    <table class="table border bordered" cellspacing="0"
                                                        cellpadding="0" border="0">
                                                        <thead class="info">
                                                            <tr>
                                                                <th colspan="9"
                                                                    style="height: 20px;">
                                                                    <span class="ui-jqgrid-title">
                                                                        <img src="../Styles/Icones/kcalc.png" align="absMiddle" />&nbsp;&nbsp;Serviços socioassistenciais
                                                        - Valores e origem dos recursos financeiros de cofinanciamento</span>
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <th width="200" rowspan="3">Tipo de Proteção
                                                                </th>
                                                                <th style="height: 22px;" colspan="6">Cofinanciamentos
                                                                </th>
                                                                <th width="100" rowspan="3">Privado
                                                                </th>
                                                                <th width="100" rowspan="3">Total
                                                                </th>
                                                            </tr>
                                                            <tr style="height: 22px;">
                                                                <th width="200" colspan="2">Municipal
                                                                </th>
                                                                <th width="200" colspan="2">Estadual
                                                                </th>
                                                                <th width="200" colspan="2">Federal
                                                                </th>
                                                            </tr>
                                                            <tr style="height: 22px;">
                                                                <th width="100">Rede Pública
                                                                </th>
                                                                <th width="100">Rede Privada
                                                                </th>
                                                                <th width="100">Rede Pública
                                                                </th>
                                                                <th width="100">Rede Privada
                                                                </th>
                                                                <th width="100">Rede Pública
                                                                </th>
                                                                <th width="100">Rede Privada
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </tbody>
                                                        <tfoot>
                                                            <tr style="height: 22px;">
                                                                <td align="right">
                                                                    <b>Sub-Total:</b>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePublicaMunicipal" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePrivadaMunicipal" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePublicaEstadual" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePrivadaEstadual" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePublicaFederal" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePrivadaFederal" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalPrivado" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotal" runat="server" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 22px;">
                                                                <td align="right">
                                                                    <b>Total:</b>
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblTotalMunicipalExercicio0" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblTotalEstadualExercicio0" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblTotalFederalExercicio0" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblTotalPrivadoExercicio0" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblTotalGeralExercicio0" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                        </tfoot>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr style="height: 22px;">
                                                        <td align="center">
                                                            <b>
                                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "TipoProtecao"))%></b>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaMunicipal")) %>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaMunicipal"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaEstadual"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaEstadual"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaFederal"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaFederal"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "Privado"))%>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotal" runat="server" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <b class="titulo">Não existe registro neste período</b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table class="table border bordered" cellspacing="0"
                                                cellpadding="0" border="0">
                                                <thead class="info">
                                                    <tr>
                                                        <th colspan="6"
                                                            style="height: 20px;">
                                                            <span class="ui-jqgrid-title">
                                                                <img src="../Styles/Icones/kcalc.png" align="absMiddle" />&nbsp;&nbsp;Programas
                                                desenvolvidos no município - Valores e origem dos recursos financeiros </span>
                                                        </th>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <th width="300">
                                                            Nome do Programa
                                                        </th>
                                                        <th width="140">Municipal
                                                        </th>
                                                        <th width="140">Estadual
                                                        </th>
                                                        <th width="140">Federal
                                                        </th>
                                                        <th width="140">Privados
                                                        </th>
                                                        <th width="140">Total
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="height: 22px;">
                                                        <td align="left">ACESSUAS
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASPrivadoExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">Programa São Paulo Solidário
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSPrivadosExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">Programa São Paulo Amigo do Idoso
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAIMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAIEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAIFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAIPrivadosExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAITotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">Programas e Projetos Municipais
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMPrivadosExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">Programas Prospera Família
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFPrivadosExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">Fortalecimento do CadÚnico
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCPrivadosExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>

                                                </tbody>
                                                <tfoot>
                                                    <tr style="height: 22px;">
                                                        <td align="right">
                                                            <b>Total:</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalMunicipalExercicio0" Text="0,00" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalEstadualExercicio0" Text="0,00" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalFederalExercicio0" Text="0,00" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalPrivadosExercicio0" Text="0,00" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalGeralExercicio0" Text="0,00" runat="server" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table class="table border bordered" cellspacing="0"
                                                cellpadding="0" border="0" width="100%">
                                                <thead class="info">
                                                    <tr>
                                                        <th colspan="6"
                                                            style="height: 20px;">
                                                            <span class="ui-jqgrid-title">
                                                                <img src="../Styles/Icones/kcalc.png" align="absMiddle" />&nbsp;&nbsp;Transferência
                                                direta de renda - Valores e origem dos recursos financeiros de repasse</span>
                                                        </th>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <th width="300">Tipo de Transferência/Benefício
                                                        </th>
                                                        <th width="140">Municipal
                                                        </th>
                                                        <th width="140">Estadual
                                                        </th>
                                                        <th width="140">Federal
                                                        </th>
                                                        <th width="140">Privados
                                                        </th>
                                                        <th width="140">Total
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="height: 22px;">
                                                        <td align="left">Benefícios Eventuais
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvPrivadosExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">BPC - Idosos
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososPrivadosExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">BPC - PCD
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDPrivadosExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">Ação Jovem
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemPrivadosExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">Renda Cidadã
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaPrivadosExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">Renda Cidadã - Benefício Idoso
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoPrivadosExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">Bolsa Família
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaPrivadosExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">Programa Municipal de transferência de renda
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaPrivadosExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaMunicipalTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr style="height: 22px;">
                                                        <td align="right">
                                                            <b>Total:</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalMunicipalExercicio0" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalEstadualExercicio0" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalFederalExercicio0" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalPrivadosExercicio0" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalGeralExercicio0" runat="server" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table class="table border bordered" cellspacing="0"
                                                cellpadding="0" border="0" width="100%">
                                                <thead class="info">
                                                    <tr>
                                                        <th colspan="6"
                                                            style="height: 20px;">
                                                            <span class="ui-jqgrid-title">
                                                                <img src="../Styles/Icones/kcalc.png" align="absMiddle" />&nbsp;&nbsp;Resumo geral
                                                - Cofinanciamentos e repasses</span>
                                                        </th>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <th width="250">Destinação
                                                        </th>
                                                        <th width="150">Municipal
                                                        </th>
                                                        <th width="150">Estadual
                                                        </th>
                                                        <th width="150">Federal
                                                        </th>
                                                        <th width="150">Privados
                                                        </th>
                                                        <th width="150">Total
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <%--  <tr class="jqgfirstrow" style="height: auto;">
                                                        <td style="height: 0px;"></td>
                                                        <td style="height: 0px;"></td>
                                                        <td style="height: 0px;"></td>
                                                        <td style="height: 0px;"></td>
                                                        <td style="height: 0px;"></td>
                                                        <td style="height: 0px;"></td>
                                                    </tr>--%>
                                                    <tr style="height: 22px;">
                                                        <td align="left">Serviços socioassistenciais
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblServicosSocioAssMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblServicosSocioAssEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblServicosSocioAssFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblServicosSocioAssPrivadosExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblServicosSocioAssTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">Benefícios
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBeneficiosMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBeneficiosEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBeneficiosFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBeneficiosPrivadosExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBeneficiosTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">Transferência direta de renda
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTransferenciaRendaMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTransferenciaRendaEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTransferenciaRendaFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTransferenciaRendaPrivadosExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTransferenciaRendaTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">Incentivos à gestão
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblIncentivoGestaoMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblIncentivoGestaoEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblIncentivoGestaoFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblIncentivoGestaoPrivadosExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblIncentivoGestaoTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">Programa e Projetos
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramasMunicipalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramasEstadualExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramasFederalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramasPrivadoExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramasTotalExercicio0" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr style="height: 22px;">
                                                        <td align="center">
                                                            <b>Total:</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotalResumoGeralMunicipalExercicio0" runat="server" Font-Bold="true">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotalResumoGeralEstadualExercicio0" runat="server" Font-Bold="true">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotalResumoGeralFederalExercicio0" runat="server" Font-Bold="true">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotalResumoGeralPrivadosExercicio0" runat="server" Font-Bold="true">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotalResumoGeralTotalExercicio0" runat="server" Font-Bold="true">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table class="table border bordered">
                                                <tr>
                                                    <td width="400">
                                                        <b>Lei Orçamentária Municipal da Assistência Social</b>
                                                    </td>
                                                    <td width="300">
                                                        <b>Valor Aprovado:</b>
                                                        <asp:Label ID="lblValorAprovadoLei" runat="server" />
                                                    </td>
                                                    <td width="200">
                                                        <b>Nº:</b>
                                                        <asp:Label ID="lblLei" runat="server" />
                                                    </td>
                                                    <td>
                                                        <b>Data:</b>
                                                        <asp:Label ID="lblDataLei" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>









                    <div class="frame">
                        <div class="heading">
                            5.1.b - Previsão DE COFINANCIAMENTO E REPASSES para 2022 (valores anuais)
                             <a href="#" runat="server" id="linkAlteracoesQuadro10" visible="false">
                                 <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                             </a>
                            <span class="mif-dollar2 icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="previsão orçamentária">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            Os valores constantes nas tabelas abaixo serão preenchidos automaticamente pelo sistema à medida que sejam registrados nos demais blocos de informação.
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:ListView ID="lvPrevisaoOrcamentaria" runat="server" DataKeyNames="IdTipoProtecao"
                                                OnItemDataBound="lstPrevisaoOrcamentariaExercicio1_ItemDataBound">
                                                <LayoutTemplate>
                                                    <table class="table border bordered" cellspacing="0"
                                                        cellpadding="0" border="0" width="100%">
                                                        <thead class="info">
                                                            <tr>
                                                                <th colspan="10"
                                                                    style="height: 20px;">
                                                                    <span class="mif-calculator2"></span>
                                                                    &nbsp;&nbsp;Serviços socioassistenciais - Valores e origem dos recursos financeiros de cofinanciamento
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <th width="200" rowspan="3">Tipo de Proteção
                                                                </th>
                                                                <th style="height: 22px;" colspan="6">Cofinanciamentos
                                                                </th>
                                                                <th width="100" rowspan="3">Recursos próprios<br />
                                                                    das Organizações
                                                                </th>
                                                                <th width="100" rowspan="3">Total
                                                                </th>
                                                            </tr>
                                                            <tr style="height: 22px;">
                                                                <th width="200" colspan="2">Municipal
                                                                </th>
                                                                <th width="200" colspan="2">Estadual
                                                                </th>
                                                                <th width="200" colspan="2">Federal
                                                                </th>
                                                            </tr>
                                                            <tr style="height: 22px;">
                                                                <th width="100">Rede Direta
                                                                </th>
                                                                <th width="100">Rede Indireta
                                                                </th>
                                                                <th width="100">Rede Direta
                                                                </th>
                                                                <th width="100">Rede Indireta
                                                                </th>
                                                                <th width="100">Rede Direta
                                                                </th>
                                                                <th width="100">Rede Indireta
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </tbody>
                                                        <tfoot>
                                                            <tr style="height: 22px;">
                                                                <td class="info" align="right">
                                                                    <b>Sub-Total:</b>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePublicaMunicipal" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePrivadaMunicipal" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePublicaEstadual" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePrivadaEstadual" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePublicaFederal" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePrivadaFederal" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalPrivado" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotal" runat="server" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                            <tr class="info" style="height: 22px;">
                                                                <td align="right">
                                                                    <b>Total:</b>
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblTotalMunicipal" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblTotalEstadual" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblTotalFederal" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblTotalPrivado" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblTotalGeral" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                        </tfoot>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="center">
                                                            <b>
                                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "TipoProtecao"))%></b>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaMunicipal")) %>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaMunicipal"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaEstadual"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaEstadual"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaFederal"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaFederal"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "Privado"))%>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotal" runat="server" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <b class="titulo">Não existe registro neste período</b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table class="table border bordered" cellspacing="0"
                                                cellpadding="0" border="0" width="100%">
                                                <thead class="info">
                                                    <tr>
                                                        <th colspan="6"
                                                            style="height: 20px;">
                                                            <span class="mif-calculator2"></span>
                                                            &nbsp;&nbsp;Programas desenvolvidos no município - Valores e origem dos recursos financeiros
                                                        </th>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <th width="300">Nome do Programa 
                                                        </th>
                                                        <th width="140">Municipal
                                                        </th>
                                                        <th width="140">Estadual
                                                        </th>
                                                        <th width="140">Federal
                                                        </th>
                                                        <th width="140">Total
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>ACESSUAS</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASMunicipal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASEstadual" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASFederal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASTotal" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programa Criança Feliz</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSMunicipal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSEstadual" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSFederal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSTotal" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Ações estratégicas do PETI</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAPETIMunicipal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAPETIEstadual" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAPETIFederal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAPETITotal" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programa São Paulo Amigo do Idoso</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAIMunicipal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAIEstadual" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAIFederal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAITotal" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programas e Projetos Municipais</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMMunicipal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMEstadual" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMFederal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMTotal" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Fortalecimento do CadÚnico</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCMunicipal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCEstadual" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCFederal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCTotal" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info"  align="left"><b>Fortalecimento da Vigilância Socioassistencial</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFVMunicipal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFVEstadual" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFVFederal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFVTotal" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr class="info" style="height: 22px;">
                                                        <td align="right">
                                                            <b>Total:</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalMunicipal" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalEstadual" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalFederal" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalGeral" runat="server" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table cellspacing="0" class="table border bordered"
                                                cellpadding="0" border="0" width="100%">
                                                <thead class="info">
                                                    <tr>
                                                        <th colspan="6"
                                                            style="height: 20px;">
                                                            <span class="mif-calculator2"></span>
                                                            &nbsp;&nbsp;Transferência 
                                        direta de renda - Valores e origem dos recursos financeiros de repasse</th>

                                                    </tr>
                                                    <tr class="ui-jqgrid-labels" style="height: 22px;">
                                                        <th width="300">Tipo de Transferência/Benefício
                                                        </th>
                                                        <th width="140">Municipal
                                                        </th>
                                                        <th width="140">Estadual
                                                        </th>
                                                        <th width="140">Federal
                                                        </th>
                                                        <th width="140">Total
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Benefícios Eventuais</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvMunicipal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvEstadual" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvFederal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvTotal" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>BPC - Idosos</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososMunicipal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososEstadual" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososFederal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososTotal" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>BPC - PCD</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDMunicipal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDEstadual" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDFederal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDTotal" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Ação Jovem</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemMunicipal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemEstadual" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemFederal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemTotal" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Renda Cidadã</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaMunicipal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaEstadual" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaFederal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaTotal" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Renda Cidadã - Benefício Idoso</b></td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoMunicipal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoEstadual" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoFederal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoTotal" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programas Prospera Família</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFMunicipal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFEstadual" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFFederal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFTotal" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Bolsa Família</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaMunicipal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaEstadual" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaFederal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaTotal" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programa municipal de transferência de renda</b></td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaMunicipal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaEstadual" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaFederal" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaMunicipalTotal" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr class="info" style="height: 22px;">
                                                        <td align="right">
                                                            <b>Total:</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalMunicipal" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalEstadual" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalFederal" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalGeral" runat="server" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <fieldset class="border-blue">
                                                <legend class="lgnd"><b class="fg-blue">Previsão de cofinanciamentos e repasses para 2022</b></legend>
                                                <table border="0" cellpadding="0" cellspacing="0" class="table border bordered" width="100%">
                                                    <thead class="info">
                                                        <tr style="height: 22px;">
                                                            <th width="250">Destinação </th>
                                                            <th width="150">Municipal </th>
                                                            <th width="150">Estadual </th>
                                                            <th width="150">Federal </th>
                                                            <th width="150">Recursos próprios das Organizações</th>
                                                            <th width="150">Total </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr style="height: 22px;">
                                                            <td align="left">Serviços socioassistenciais </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssMunicipal" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssEstadual" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssFederal" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssPrivados" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssTotal" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 22px;">
                                                            <td align="left">Benefícios Eventuais</td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosMunicipal" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosEstadual" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosFederal" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosPrivados" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosTotal" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 22px;">
                                                            <td align="left">Transferência direta de renda</td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaMunicipal" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaEstadual" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaFederal" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaPrivados" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaTotal" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 22px;">
                                                            <td align="left">Incentivos à gestão </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoMunicipal" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoEstadual" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoFederal" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoPrivados" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoTotal" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 22px;">
                                                            <%--Bruno V.--%>
                                                            <td align="left">Programas e Projetos</td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasMunicipal" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasEstadual" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasFederal" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasPrivado" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasTotal" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                    <tfoot>
                                                        <tr class="info" style="height: 22px;">
                                                            <td align="center"><b>Total:</b> </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralMunicipal" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralEstadual" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralFederal" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralPrivados" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralTotal" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </tfoot>
                                                </table>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
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









                    <div class="frame">
                        <div class="heading">
                            5.1.c - Previsão DE COFINANCIAMENTO E REPASSES para 2023 (valores anuais)
                             <a href="#" runat="server" id="linkAlteracoesQuadro10_Exercicio2" visible="false">
                                 <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                             </a>
                            <span class="mif-dollar2 icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="previsão orçamentária">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            Os valores constantes nas tabelas abaixo serão preenchidos automaticamente pelo sistema à medida que sejam registrados nos demais blocos de informação.
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:ListView ID="lvPrevisaoOrcamentariaExercicio2" runat="server" DataKeyNames="IdTipoProtecao"
                                                OnItemDataBound="lstPrevisaoOrcamentariaExercicio2_ItemDataBound">
                                                <LayoutTemplate>
                                                    <table class="table border bordered" cellspacing="0"
                                                        cellpadding="0" border="0" width="100%">
                                                        <thead class="info">
                                                            <tr>
                                                                <th colspan="10"
                                                                    style="height: 20px;">
                                                                    <span class="mif-calculator2"></span>
                                                                    &nbsp;&nbsp;Serviços socioassistenciais
                                                    - Valores e origem dos recursos financeiros de cofinanciamento
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <th width="200" rowspan="3">Tipo de Proteção
                                                                </th>

                                                                <th style="height: 22px;" colspan="6">Cofinanciamentos
                                                                </th>

                                                                <th width="100" rowspan="3">Recursos próprios<br />
                                                                    das Organizações
                                                                </th>

                                                                <th width="100" rowspan="3">Total
                                                                </th>
                                                            </tr>

                                                            <tr style="height: 22px;">
                                                                <th width="200" colspan="2">Municipal
                                                                </th>
                                                                <th width="200" colspan="2">Estadual
                                                                </th>
                                                                <th width="200" colspan="2">Federal
                                                                </th>
                                                            </tr>
                                                            <tr style="height: 22px;">
                                                                <th width="100">Rede Direta
                                                                </th>
                                                                <th width="100">Rede Indireta
                                                                </th>
                                                                <th width="100">Rede Direta
                                                                </th>
                                                                <th width="100">Rede Indireta
                                                                </th>
                                                                <th width="100">Rede Direta
                                                                </th>
                                                                <th width="100">Rede Indireta
                                                                </th>
                                                            </tr>

                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </tbody>
                                                        <tfoot>
                                                            <tr style="height: 22px;">

                                                                <td class="info" align="right">
                                                                    <b>Sub-Total:</b>
                                                                </td>

                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePublicaMunicipal" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePrivadaMunicipal" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePublicaEstadual" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePrivadaEstadual" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePublicaFederal" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePrivadaFederal" runat="server" Font-Bold="true" />
                                                                </td>

                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalPrivado" runat="server" Font-Bold="true" />
                                                                </td>

                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotal" runat="server" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                            <tr class="info" style="height: 22px;">
                                                                <td align="right">
                                                                    <b>Total:</b>
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblTotalMunicipal" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblTotalEstadual" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblTotalFederal" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblTotalPrivado" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblTotalGeral" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                        </tfoot>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="center">
                                                            <b>
                                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "TipoProtecao"))%></b>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaMunicipal")) %>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaMunicipal"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaEstadual"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaEstadual"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaFederal"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaFederal"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "Privado"))%>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotal" runat="server" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <b class="titulo">Não existe registro neste período</b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table class="table border bordered" cellspacing="0" cellpadding="0" border="0" width="100%">
                                                <thead class="info">
                                                    <tr>
                                                        <th colspan="6"
                                                            style="height: 20px;">
                                                            <span class="mif-calculator2"></span>
                                                            &nbsp;&nbsp;Programas desenvolvidos no município - Valores e origem dos recursos financeiros
                                                        </th>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <th width="300">Nome do Programa 
                                                        </th>
                                                        <th width="140">Municipal
                                                        </th>
                                                        <th width="140">Estadual
                                                        </th>
                                                        <th width="140">Federal
                                                        </th>
                                                        <th width="140">Total
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>ACESSUAS</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASEstadualExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASFederalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASTotalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programa Criança Feliz</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSEstadualExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSFederalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSTotalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Ações estratégicas do PETI</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAPETIMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAPETIEstadualExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAPETIFederalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAPETITotalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programa São Paulo Amigo do Idoso</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAIMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAIEstadualExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAIFederalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAITotalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programas e Projetos Municipais</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMEstadualExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMFederalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMTotalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Fortalecimento do CadÚnico</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCEstadualExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCFederalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCTotalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Fortalecimento da Vigilância Socioassistencial</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFVMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFVEstadualExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFVFederalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFVTotalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr class="info" style="height: 22px;">
                                                        <td align="right">
                                                            <b>Total:</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalMunicipalExercicio2" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalEstadualExercicio2" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalFederalExercicio2" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalGeralExercicio2" runat="server" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table cellspacing="0" class="table border bordered"
                                                cellpadding="0" border="0" width="100%">
                                                <thead class="info">
                                                    <tr>
                                                        <th colspan="6"
                                                            style="height: 20px;">
                                                            <span class="mif-calculator2"></span>
                                                            &nbsp;&nbsp;Transferência 
                                        direta de renda - Valores e origem dos recursos financeiros de repasse</th>

                                                    </tr>
                                                    <tr class="ui-jqgrid-labels" style="height: 22px;">
                                                        <th width="300">Tipo de Transferência/Benefício
                                                        </th>
                                                        <th width="140">Municipal
                                                        </th>
                                                        <th width="140">Estadual
                                                        </th>
                                                        <th width="140">Federal
                                                        </th>
                                                        <th width="140">Total
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Benefícios Eventuais</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvEstadualExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvFederalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvTotalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>BPC - Idosos</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososEstadualExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososFederalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososTotalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>BPC - PCD</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDEstadualExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDFederalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDTotalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Ação Jovem</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemEstadualExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemFederalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemTotalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Renda Cidadã</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaEstadualExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaFederalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaTotalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Renda Cidadã - Benefício Idoso</b></td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoEstadualExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoFederalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoTotalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programas Prospera Família</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFEstadualExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFFederalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFTotalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Bolsa Família</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaEstadualExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaFederalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaTotalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programa municipal de transferência de renda</b></td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaEstadualExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaFederalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaMunicipalTotalExercicio2" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr class="info" style="height: 22px;">
                                                        <td align="right">
                                                            <b>Total:</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalMunicipalExercicio2" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalEstadualExercicio2" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalFederalExercicio2" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalGeralExercicio2" runat="server" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <fieldset class="border-blue">
                                                <legend class="lgnd"><b class="fg-blue">Previsão de cofinanciamentos e repasses para 2023</b></legend>
                                                <table border="0" cellpadding="0" cellspacing="0" class="table border bordered" width="100%">
                                                    <thead class="info">
                                                        <tr style="height: 22px;">
                                                            <th width="250">Destinação </th>
                                                            <th width="150">Municipal </th>
                                                            <th width="150">Estadual </th>
                                                            <th width="150">Federal </th>
                                                            <th width="150">Recursos próprios das Organizações</th>
                                                            <th width="150">Total </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr style="height: 22px;">
                                                            <td align="left">Serviços socioassistenciais </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssEstadualExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssFederalExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssPrivadosExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssTotalExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 22px;">
                                                            <td align="left">Benefícios Eventuais</td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosEstadualExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosFederalExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosPrivadosExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosTotalExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 22px;">
                                                            <td align="left">Transferência direta de renda</td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaEstadualExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaFederalExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaPrivadosExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaTotalExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 22px;">
                                                            <td align="left">Incentivos à gestão </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoEstadualExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoFederalExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoPrivadosExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoTotalExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 22px;">
                                                            <%--Bruno V.--%>
                                                            <td align="left">Programas e Projetos</td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasMunicipalExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasEstadualExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasFederalExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasPrivadoExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasTotalExercicio2" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                    <tfoot>
                                                        <tr class="info" style="height: 22px;">
                                                            <td align="center"><b>Total:</b> </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralMunicipalExercicio2" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralEstadualExercicio2" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralFederalExercicio2" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralPrivadosExercicio2" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralTotalExercicio2" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </tfoot>
                                                </table>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table id="Table1" runat="server" visible="false" cellspacing="2" cellpadding="0"
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
                                                        <asp:Label ID="Label91" ForeColor="Red" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                       
                    </div>
                    <div class="frame">
                        <div class="heading">
                            5.1.D - Previsão DE COFINANCIAMENTO E REPASSES para 2024 (valores anuais)
                             <a href="#" runat="server" id="linkAlteracoesQuadro10_Exercicio3" visible="false">
                                 <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                             </a>
                            <span class="mif-dollar2 icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="previsão orçamentária">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            Os valores constantes nas tabelas abaixo serão preenchidos automaticamente pelo sistema à medida que sejam registrados nos demais blocos de informação.
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:ListView ID="lvPrevisaoOrcamentariaExercicio3" runat="server" DataKeyNames="IdTipoProtecao"
                                                OnItemDataBound="lstPrevisaoOrcamentariaExercicio3_ItemDataBound">
                                                <LayoutTemplate>
                                                    <table class="table border bordered" cellspacing="0"
                                                        cellpadding="0" border="0" width="100%">
                                                        <thead class="info">
                                                            <tr>
                                                                <th colspan="10"
                                                                    style="height: 20px;">
                                                                    <span class="mif-calculator2"></span>
                                                                    &nbsp;&nbsp;Serviços socioassistenciais
                                                    - Valores e origem dos recursos financeiros de cofinanciamento
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <th width="200" rowspan="3">Tipo de Proteção
                                                                </th>

                                                                <th style="height: 22px;" colspan="6">Cofinanciamentos
                                                                </th>

                                                                <th width="100" rowspan="3">Recursos próprios<br />
                                                                    das Organizações
                                                                </th>

                                                                <th width="100" rowspan="3">Total
                                                                </th>
                                                            </tr>

                                                            <tr style="height: 22px;">
                                                                <th width="200" colspan="2">Municipal
                                                                </th>
                                                                <th width="200" colspan="2">Estadual
                                                                </th>
                                                                <th width="200" colspan="2">Federal
                                                                </th>
                                                            </tr>
                                                            <tr style="height: 22px;">
                                                                <th width="100">Rede Direta
                                                                </th>
                                                                <th width="100">Rede Indireta
                                                                </th>
                                                                <th width="100">Rede Direta
                                                                </th>
                                                                <th width="100">Rede Indireta
                                                                </th>
                                                                <th width="100">Rede Direta
                                                                </th>
                                                                <th width="100">Rede Indireta
                                                                </th>
                                                            </tr>

                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </tbody>
                                                        <tfoot>
                                                            <tr style="height: 22px;">

                                                                <td class="info" align="right">
                                                                    <b>Sub-Total:</b>
                                                                </td>

                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePublicaMunicipal" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePrivadaMunicipal" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePublicaEstadual" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePrivadaEstadual" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePublicaFederal" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePrivadaFederal" runat="server" Font-Bold="true" />
                                                                </td>

                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalPrivado" runat="server" Font-Bold="true" />
                                                                </td>

                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotal" runat="server" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                            <tr class="info" style="height: 22px;">
                                                                <td align="right">
                                                                    <b>Total:</b>
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblTotalMunicipal" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblTotalEstadual" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblTotalFederal" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblTotalPrivado" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblTotalGeral" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                        </tfoot>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="center">
                                                            <b>
                                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "TipoProtecao"))%></b>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaMunicipal")) %>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaMunicipal"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaEstadual"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaEstadual"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaFederal"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaFederal"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "Privado"))%>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotal" runat="server" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <b class="titulo">Não existe registro neste período</b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table class="table border bordered" cellspacing="0" cellpadding="0" border="0" width="100%">
                                                <thead class="info">
                                                    <tr>
                                                        <th colspan="6"
                                                            style="height: 20px;">
                                                            <span class="mif-calculator2"></span>
                                                            &nbsp;&nbsp;Programas desenvolvidos no município - Valores e origem dos recursos financeiros
                                                        </th>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <th width="300">Nome do Programa 
                                                        </th>
                                                        <th width="140">Municipal
                                                        </th>
                                                        <th width="140">Estadual
                                                        </th>
                                                        <th width="140">Federal
                                                        </th>
                                                        <th width="140">Total
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>ACESSUAS</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASEstadualExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASFederalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASTotalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programa Criança Feliz”</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSEstadualExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSFederalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSTotalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Ações estratégicas do PETI</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAPETIMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAPETIEstadualExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAPETIFederalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAPETITotalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programa São Paulo Amigo do Idoso</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAIMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAIEstadualExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAIFederalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAITotalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programas e Projetos Municipais</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMEstadualExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMFederalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMTotalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Fortalecimento do CadÚnico</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCEstadualExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCFederalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCTotalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Fortalecimento da Vigilância Socioassistencial</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFVMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFVEstadualExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFVFederalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFVTotalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr class="info" style="height: 22px;">
                                                        <td align="right">
                                                            <b>Total:</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalMunicipalExercicio3" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalEstadualExercicio3" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalFederalExercicio3" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalGeralExercicio3" runat="server" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table cellspacing="0" class="table border bordered"
                                                cellpadding="0" border="0" width="100%">
                                                <thead class="info">
                                                    <tr>
                                                        <th colspan="6"
                                                            style="height: 20px;">
                                                            <span class="mif-calculator2"></span>
                                                            &nbsp;&nbsp;Transferência 
                                        direta de renda - Valores e origem dos recursos financeiros de repasse</th>

                                                    </tr>
                                                    <tr class="ui-jqgrid-labels" style="height: 22px;">
                                                        <th width="300">Tipo de Transferência/Benefício
                                                        </th>
                                                        <th width="140">Municipal
                                                        </th>
                                                        <th width="140">Estadual
                                                        </th>
                                                        <th width="140">Federal
                                                        </th>
                                                        <th width="140">Total
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Benefícios Eventuais</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvEstadualExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvFederalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvTotalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>BPC - Idosos</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososEstadualExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososFederalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososTotalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>BPC - PCD</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDEstadualExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDFederalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDTotalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Ação Jovem</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemEstadualExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemFederalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemTotalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Renda Cidadã</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaEstadualExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaFederalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaTotalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Renda Cidadã - Benefício Idoso</b></td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoEstadualExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoFederalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoTotalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programas Prospera Família</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFEstadualExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFFederalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFTotalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Bolsa Família</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaEstadualExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaFederalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaTotalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programa municipal de transferência de renda</b></td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaEstadualExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaFederalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaMunicipalTotalExercicio3" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr class="info" style="height: 22px;">
                                                        <td align="right">
                                                            <b>Total:</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalMunicipalExercicio3" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalEstadualExercicio3" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalFederalExercicio3" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalGeralExercicio3" runat="server" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <fieldset class="border-blue">
                                                <legend class="lgnd"><b class="fg-blue">Previsão de cofinanciamentos e repasses para 2024</b></legend>
                                                <table border="0" cellpadding="0" cellspacing="0" class="table border bordered" width="100%">
                                                    <thead class="info">
                                                        <tr style="height: 22px;">
                                                            <th width="250">Destinação </th>
                                                            <th width="150">Municipal </th>
                                                            <th width="150">Estadual </th>
                                                            <th width="150">Federal </th>
                                                            <th width="150">Recursos próprios das Organizações</th>
                                                            <th width="150">Total </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr style="height: 22px;">
                                                            <td align="left">Serviços socioassistenciais </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssEstadualExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssFederalExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssPrivadosExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssTotalExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 22px;">
                                                            <td align="left">Benefícios Eventuais</td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosEstadualExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosFederalExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosPrivadosExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosTotalExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 22px;">
                                                            <td align="left">Transferência direta de renda</td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaEstadualExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaFederalExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaPrivadosExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaTotalExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 22px;">
                                                            <td align="left">Incentivos à gestão </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoEstadualExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoFederalExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoPrivadosExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoTotalExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 22px;">
                                                            <%--Bruno V.--%>
                                                            <td align="left">Programas e Projetos</td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasMunicipalExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasEstadualExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasFederalExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasPrivadoExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasTotalExercicio3" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                    <tfoot>
                                                        <tr class="info" style="height: 22px;">
                                                            <td align="center"><b>Total:</b> </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralMunicipalExercicio3" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralEstadualExercicio3" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralFederalExercicio3" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralPrivadosExercicio3" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralTotalExercicio3" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </tfoot>
                                                </table>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table id="Table2" runat="server" visible="false" cellspacing="2" cellpadding="0"
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
                                                        <asp:Label ID="Label1" ForeColor="Red" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="frame">
                        <div class="heading">
                            5.1.E - Previsão DE COFINANCIAMENTO E REPASSES para 2025 (valores anuais)
                             <a href="#" runat="server" id="linkAlteracoesQuadro10_Exercicio4" visible="false">
                                 <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                             </a>
                            <span class="mif-dollar2 icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="previsão orçamentária">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            Os valores constantes nas tabelas abaixo serão preenchidos automaticamente pelo sistema à medida que sejam registrados nos demais blocos de informação.
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:ListView ID="lvPrevisaoOrcamentariaExercicio4" runat="server" DataKeyNames="IdTipoProtecao"
                                                OnItemDataBound="lstPrevisaoOrcamentariaExercicio4_ItemDataBound">
                                                <LayoutTemplate>
                                                    <table class="table border bordered" cellspacing="0"
                                                        cellpadding="0" border="0" width="100%">
                                                        <thead class="info">
                                                            <tr>
                                                                <th colspan="10"
                                                                    style="height: 20px;">
                                                                    <span class="mif-calculator2"></span>
                                                                    &nbsp;&nbsp;Serviços socioassistenciais
                                                    - Valores e origem dos recursos financeiros de cofinanciamento
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <th width="200" rowspan="3">Tipo de Proteção
                                                                </th>

                                                                <th style="height: 22px;" colspan="6">Cofinanciamentos
                                                                </th>

                                                                <th width="100" rowspan="3">Recursos próprios<br />
                                                                    das Organizações
                                                                </th>

                                                                <th width="100" rowspan="3">Total
                                                                </th>
                                                            </tr>

                                                            <tr style="height: 22px;">
                                                                <th width="200" colspan="2">Municipal
                                                                </th>
                                                                <th width="200" colspan="2">Estadual
                                                                </th>
                                                                <th width="200" colspan="2">Federal
                                                                </th>
                                                            </tr>
                                                            <tr style="height: 22px;">
                                                                <th width="100">Rede Direta
                                                                </th>
                                                                <th width="100">Rede Indireta
                                                                </th>
                                                                <th width="100">Rede Direta
                                                                </th>
                                                                <th width="100">Rede Indireta
                                                                </th>
                                                                <th width="100">Rede Direta
                                                                </th>
                                                                <th width="100">Rede Indireta
                                                                </th>
                                                            </tr>

                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </tbody>
                                                        <tfoot>
                                                            <tr style="height: 22px;">

                                                                <td class="info" align="right">
                                                                    <b>Sub-Total:</b>
                                                                </td>

                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePublicaMunicipal" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePrivadaMunicipal" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePublicaEstadual" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePrivadaEstadual" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePublicaFederal" runat="server" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalRedePrivadaFederal" runat="server" Font-Bold="true" />
                                                                </td>

                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotalPrivado" runat="server" Font-Bold="true" />
                                                                </td>

                                                                <td align="center">
                                                                    <asp:Label ID="lblSubTotal" runat="server" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                            <tr class="info" style="height: 22px;">
                                                                <td align="right">
                                                                    <b>Total:</b>
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblTotalMunicipal" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblTotalEstadual" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    <asp:Label ID="lblTotalFederal" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblTotalPrivado" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Label ID="lblTotalGeral" runat="server" Text="0,00" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                        </tfoot>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="center">
                                                            <b>
                                                                <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "TipoProtecao"))%></b>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaMunicipal")) %>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaMunicipal"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaEstadual"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaEstadual"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePublicaFederal"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RedePrivadaFederal"))%>
                                                        </td>
                                                        <td align="center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "Privado"))%>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblTotal" runat="server" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <b class="titulo">Não existe registro neste período</b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table class="table border bordered" cellspacing="0" cellpadding="0" border="0" width="100%">
                                                <thead class="info">
                                                    <tr>
                                                        <th colspan="6"
                                                            style="height: 20px;">
                                                            <span class="mif-calculator2"></span>
                                                            &nbsp;&nbsp;Programas desenvolvidos no município - Valores e origem dos recursos financeiros
                                                        </th>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <th width="300">Nome do Programa 
                                                        </th>
                                                        <th width="140">Municipal
                                                        </th>
                                                        <th width="140">Estadual
                                                        </th>
                                                        <th width="140">Federal
                                                        </th>
                                                        <th width="140">Total
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>ACESSUAS</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASEstadualExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASFederalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblACESSUASTotalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programa Criança Feliz”</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSEstadualExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSFederalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPSTotalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Ações estratégicas do PETI</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAPETIMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAPETIEstadualExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAPETIFederalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAPETITotalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programa São Paulo Amigo do Idoso</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAIMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAIEstadualExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAIFederalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblSPAITotalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programas e Projetos Municipais</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMEstadualExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMFederalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPMTotalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Fortalecimento do CadÚnico</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCEstadualExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCFederalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFCTotalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Fortalecimento da Vigilância Socioassistencial</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFVMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFVEstadualExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFVFederalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFVTotalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>

                                                </tbody>
                                                <tfoot>
                                                    <tr class="info" style="height: 22px;">
                                                        <td align="right">
                                                            <b>Total:</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalMunicipalExercicio4" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalEstadualExercicio4" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalFederalExercicio4" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgDesenvTotalGeralExercicio4" runat="server" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table cellspacing="0" class="table border bordered"
                                                cellpadding="0" border="0" width="100%">
                                                <thead class="info">
                                                    <tr>
                                                        <th colspan="6"
                                                            style="height: 20px;">
                                                            <span class="mif-calculator2"></span>
                                                            &nbsp;&nbsp;Transferência 
                                        direta de renda - Valores e origem dos recursos financeiros de repasse</th>

                                                    </tr>
                                                    <tr class="ui-jqgrid-labels" style="height: 22px;">
                                                        <th width="300">Tipo de Transferência/Benefício
                                                        </th>
                                                        <th width="140">Municipal
                                                        </th>
                                                        <th width="140">Estadual
                                                        </th>
                                                        <th width="140">Federal
                                                        </th>
                                                        <th width="140">Total
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Benefícios Eventuais</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvEstadualExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvFederalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBEvTotalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>BPC - Idosos</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososEstadualExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososFederalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCIdososTotalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>BPC - PCD</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDEstadualExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDFederalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBPCPCDTotalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Ação Jovem</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemEstadualExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemFederalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblAcaoJovemTotalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Renda Cidadã</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaEstadualExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaFederalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaTotalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Renda Cidadã - Benefício Idoso</b></td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoEstadualExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoFederalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRendaCidadaBeneficioIdosoTotalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programas Prospera Família</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFEstadualExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFFederalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPFTotalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Bolsa Família</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaEstadualExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaFederalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblBolsaFamiliaTotalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td class="info" align="left"><b>Programa municipal de transferência de renda</b></td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaEstadualExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaFederalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaMunicipalTotalExercicio4" runat="server">0,00</asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr class="info" style="height: 22px;">
                                                        <td align="right">
                                                            <b>Total:</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalMunicipalExercicio4" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalEstadualExercicio4" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalFederalExercicio4" runat="server" Font-Bold="true" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblProgramaTotalGeralExercicio4" runat="server" Font-Bold="true" />
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <fieldset class="border-blue">
                                                <legend class="lgnd"><b class="fg-blue">Previsão de cofinanciamentos e repasses para 2025</b></legend>
                                                <table border="0" cellpadding="0" cellspacing="0" class="table border bordered" width="100%">
                                                    <thead class="info">
                                                        <tr style="height: 22px;">
                                                            <th width="250">Destinação </th>
                                                            <th width="150">Municipal </th>
                                                            <th width="150">Estadual </th>
                                                            <th width="150">Federal </th>
                                                            <th width="150">Recursos próprios das Organizações</th>
                                                            <th width="150">Total </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr style="height: 22px;">
                                                            <td align="left">Serviços socioassistenciais </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssEstadualExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssFederalExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssPrivadosExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblServicosSocioAssTotalExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 22px;">
                                                            <td align="left">Benefícios Eventuais</td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosEstadualExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosFederalExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosPrivadosExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblBeneficiosTotalExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 22px;">
                                                            <td align="left">Transferência direta de renda</td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaEstadualExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaFederalExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaPrivadosExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTransferenciaRendaTotalExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 22px;">
                                                            <td align="left">Incentivos à gestão </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoEstadualExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoFederalExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoPrivadosExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblIncentivoGestaoTotalExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 22px;">
                                                            <%--Bruno V.--%>
                                                            <td align="left">Programas e Projetos</td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasMunicipalExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasEstadualExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasFederalExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasPrivadoExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblProgramasTotalExercicio4" runat="server">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                    <tfoot>
                                                        <tr class="info" style="height: 22px;">
                                                            <td align="center"><b>Total:</b> </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralMunicipalExercicio4" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralEstadualExercicio4" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralFederalExercicio4" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralPrivadosExercicio4" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalResumoGeralTotalExercicio4" runat="server" Font-Bold="true">0,00</asp:Label>
                                                            </td>
                                                        </tr>
                                                    </tfoot>
                                                </table>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table id="Table3" runat="server" visible="false" cellspacing="2" cellpadding="0"
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
                                                        <asp:Label ID="Label2" ForeColor="Red" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table width="100%" align="center" class="ui-text">
                            <tr>
                                <td align="right" style="padding-top: 10px;">
                                    <a href="FLeiOrcamentaria.aspx">Próximo
                                    <span class="mif-arrow-right"></span></a>
                                </td>
                            </tr>
                        </table>
                    </div>


                </div>
            </form>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
