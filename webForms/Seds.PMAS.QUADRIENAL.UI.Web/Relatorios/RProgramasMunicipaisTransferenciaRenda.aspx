<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RProgramasMunicipaisTransferenciaRenda.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RProgramasMunicipaisTransferenciaRenda" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="ui-widget ui-widget-content ui-corner-all">
                <thead>
                    <tr>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            style="background-color: #a6c9e2;" width="30">
                            Seq.
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="150">
                            Município
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="250">
                            DRADS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="400">
                            Nome do programa
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                             width="150">
                            Beneficiários
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="120">
                            Previsão mensal <br />de beneficiários
                        </th>
                         <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                             width="100">
                            Previsão anual<br /> de repasse
                        </th>
                         <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                             width="120" colspan="2">
                            Parcerias
                        </th>
                         <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                             width="120" colspan="2">
                            Integração com<br />
                            serviços
                        </th>
                    </tr>                    
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>             
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
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "NomePrograma") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Beneficiarios") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroBeneficiarios")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "Repasse"))%>
                </td>
                <td class="align-right">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceria") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalParcerias")) %>
                </td>
                <td class="align-right">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiIntegracao") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalServicosAssociados")) %>
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
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "NomePrograma") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Beneficiarios") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroBeneficiarios")) %>
                </td>                
                <td class="align-right">
                    <%#String.Format("{0:C2}", DataBinder.Eval(Container.DataItem, "Repasse"))%>
                </td>
                <td class="align-right">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceria") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalParcerias")) %>
                </td>
                <td class="align-right">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiIntegracao") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalServicosAssociados")) %>
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
