<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RProgramasTransferenciaRenda.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RProgramasTransferenciaRenda" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="ui-widget ui-widget-content ui-corner-all">
                <thead>
                    <tr style="background-color: #a6c9e2;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            style="background-color: #a6c9e2;" width="30" rowspan="3">
                            Seq.
                        </th>                        
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="150" rowspan="3">
                            Município
                        </th>                        
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="150" rowspan="3">
                            DRADS
                        </th>                        
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="18" style="height:22px;">
                            Programas estaduais
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="12" style="height:22px;">
                            Programas federais
                        </th>                 
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="2" style="height:22px;">
                            Programas municipais
                        </th> 
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="150" rowspan="3">
                            Previsão total de repasse anual
                        </th>      
                    </tr>    
                    <tr style="background-color: #a6c9e2;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="6" style="height:22px;">
                            Ação Jovem
                        </th>                        
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="6">
                            Renda Cidadã
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="6">
                            Renda Cidadã - Benefício Idoso
                        </th>                                           
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="6">
                            Bolsa Família
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="6">
                            PETI
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" rowspan="2" width="100">
                            Estimativa mensal de beneficiários
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" rowspan="2" width="100">
                            Previsão anual de repasse
                        </th>                        
                    </tr> 
                    <tr style="background-color: #a6c9e2;">
                       <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120">
                            Meta pactuada para 2015
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120">
                            Previsão anual de repasse
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120" colspan="2">
                            Parcerias
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120" colspan="2">
                            Integração com<br />
                            serviços
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120">
                            Meta pactuada para 2015
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120">
                            Previsão anual de repasse
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120" colspan="2">
                            Parcerias
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120" colspan="2">
                            Integração com<br />
                            serviços
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120">
                            Meta pactuada para 2015
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120">
                            Previsão anual de repasse
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120" colspan="2">
                            Parcerias
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120" colspan="2">
                            Integração com<br />
                            serviços
                        </th>                        
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120">
                            Estimativa mensal de beneficiários
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120">
                            Previsão anual de repasse
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120" colspan="2">
                            Parcerias
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120" colspan="2">
                            Integração com<br />
                            serviços
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120">
                            Estimativa mensal de beneficiários
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120">
                            Previsão anual de repasse
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120" colspan="2">
                            Parcerias
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120" colspan="2">
                            Integração com<br />
                            serviços
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="height: 22px; background-color: #a6c9e2;">
                        <td align="right" colspan="3" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <b>Totais:</b>
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalAcaoJovemMeta" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalAcaoJovemRepasse" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalAcaoJovemParceria" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalAcaoJovemParcerias" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalAcaoJovemIntegracao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalAcaoJovemServicosAssociados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalRendaCidadaMeta" runat="server" Font-Bold="true" />
                        </td>                        
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalRendaCidadaRepasse" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalRendaCidadaParceria" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalRendaCidadaParcerias" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalRendaCidadaIntegracao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalRendaCidadaServicosAssociados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalRendaCidadaIdosoMeta" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalRendaCidadaIdosoRepasse" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalRendaCidadaIdosoParceria" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalRendaCidadaIdosoParcerias" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalRendaCidadaIdosoIntegracao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalRendaCidadaIdosoServicosAssociados" runat="server" Font-Bold="true" />
                        </td>                       
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalBolsaFamiliaBeneficiarios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalBolsaFamiliaRepasse" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalBolsaFamiliaParceria" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalBolsaFamiliaParcerias" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalBolsaFamiliaIntegracao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalBolsaFamiliaServicosAssociados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalPETIBeneficiarios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalPETIRepasse" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalPETIParceria" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalPETIParcerias" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalPETIIntegracao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalPETIServicosAssociados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalMunicipaisBeneficiarios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalMunicipaisRepasse" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr class="ui-widget-content row">
                <td class="ui-state-default jqgrid-rownum row" style="height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>               
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>             
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "AcaoJovemMeta"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}",DataBinder.Eval(Container.DataItem, "AcaoJovemRepasse"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaAcaoJovem") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "AcaoJovemTotalParcerias"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiIntegracaoAcaoJovem") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "AcaoJovemTotalServicosAssociados"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "RendaCidadaMeta"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}",DataBinder.Eval(Container.DataItem, "RendaCidadaRepasse"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaRendaCidada") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "RendaCidadaTotalParcerias"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiIntegracaoRendaCidada") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "RendaCidadaTotalServicosAssociados"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "RendaCidadaIdosoMeta"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "RendaCidadaIdosoRepasse"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaRendaCidadaIdoso")%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "RendaCidadaIdosoTotalParcerias"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiIntegracaoRendaCidadaIdoso")%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "RendaCidadaIdosoTotalServicosAssociados"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "BolsaFamiliaBeneficiarios"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "BolsaFamiliaRepasse"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaBolsaFamilia") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "BolsaFamiliaTotalParcerias"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiIntegracaoBolsaFamilia") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "BolsaFamiliaTotalServicosAssociados"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "PETIBeneficiarios"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "PETIRepasse"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaPETI") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "PETITotalParcerias"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiIntegracaoPETI") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "PETITotalServicosAssociados"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MunicipaisBeneficiarios"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "MunicipaisRepasse"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "TotalRepasse"))%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="row" style="background-color: #F3F3F3 !important;">
                <td class="ui-state-default jqgrid-rownum row" style="height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>             
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "AcaoJovemMeta"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}",DataBinder.Eval(Container.DataItem, "AcaoJovemRepasse"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaAcaoJovem") %>
                </td>             
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "AcaoJovemTotalParcerias"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiIntegracaoAcaoJovem") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "AcaoJovemTotalServicosAssociados"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "RendaCidadaMeta"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}",DataBinder.Eval(Container.DataItem, "RendaCidadaRepasse"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaRendaCidada") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "RendaCidadaTotalParcerias"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiIntegracaoRendaCidada") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "RendaCidadaTotalServicosAssociados"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "RendaCidadaIdosoMeta"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "RendaCidadaIdosoRepasse"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaRendaCidadaIdoso")%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "RendaCidadaIdosoTotalParcerias"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiIntegracaoRendaCidadaIdoso")%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "RendaCidadaIdosoTotalServicosAssociados"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "BolsaFamiliaBeneficiarios"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "BolsaFamiliaRepasse"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaBolsaFamilia") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "BolsaFamiliaTotalParcerias"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiIntegracaoBolsaFamilia") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "BolsaFamiliaTotalServicosAssociados"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "PETIBeneficiarios"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "PETIRepasse"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaPETI") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "PETITotalParcerias"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiIntegracaoPETI") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "PETITotalServicosAssociados"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MunicipaisBeneficiarios"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "MunicipaisRepasse"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "TotalRepasse"))%>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EmptyDataTemplate>
            <div align="center" style="width: 100%;">
                <b class="titulo">Não foi localizado nenhum registro de acordo com as características
                    selecionadas</b>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
