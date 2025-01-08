<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true" CodeBehind="RSaoPauloAmigoIdoso.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RSaoPauloAmigoIdoso" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="ui-widget ui-widget-content ui-corner-all">
                <thead>
                    <tr style="background-color: #a6c9e2;height:22px;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            style="background-color: #a6c9e2;height:22px;" width="30" rowspan="2">
                            Seq.
                        </th>                                                
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="150" rowspan="2">
                            Município
                        </th>                        
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="150" rowspan="2">
                            DRADS
                        </th>                        
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="2" width="200">
                            Renda Cidadã - Benefício Idoso
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="2" width="200">
                            Repasse para construção de equipamentos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" rowspan="2" width="200" colspan="2">
                            Parcerias
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" rowspan="2" width="200" colspan="2">
                            Integração com<br />
                            serviços
                        </th>
                    </tr>  
                    <tr style="background-color: #a6c9e2;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120">
                            Meta pactuada<br />
                            para 2015
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">
                            Previsão anual de<br />
                            repasse
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120">
                            Centro Dia
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">
                            Centro de Convivência<br />
                            do Idoso
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
                            <asp:Label ID="lblTotalMetaPactuada" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalPrevisaoAnualRepasse" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalCentroDiaIdoso" runat="server" Font-Bold="true" />
                        </td>  
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalCentroConvivenciaIdoso" runat="server" Font-Bold="true" />
                        </td>                      
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalPossuiParceria" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalParcerias" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalPossuiServicosAssociados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicosAssociados" runat="server" Font-Bold="true" />
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
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "MetaPactuada"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "ValorPrevisaoAnualRepasse"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}",DataBinder.Eval(Container.DataItem, "ValorDiaIdoso"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "ValorConvivenciaIdoso"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "PossuiParceriaFormal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalParcerias"))%>
                </td>
                <td class="align-right">
                    <%#Convert.ToInt32(DataBinder.Eval(Container.DataItem, "TotalServicosAssociados")) == 0 ? "Não" : "Sim"%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalServicosAssociados"))%>
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
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "MetaPactuada"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "ValorPrevisaoAnualRepasse"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}",DataBinder.Eval(Container.DataItem, "ValorDiaIdoso"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "ValorConvivenciaIdoso"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "PossuiParceriaFormal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalParcerias"))%>
                </td>
                <td class="align-right">
                    <%#Convert.ToInt32(DataBinder.Eval(Container.DataItem, "TotalServicosAssociados")) == 0 ? "Não" : "Sim"%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "TotalServicosAssociados"))%>
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
