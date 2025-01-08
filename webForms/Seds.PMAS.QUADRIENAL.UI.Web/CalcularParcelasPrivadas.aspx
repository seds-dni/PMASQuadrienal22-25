<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"
    CodeBehind="CalcularParcelasPrivadas.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.CalcularParcelasPrivadas" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <table class="ui-widget ui-widget-content ui-corner-all" cellspacing="2" cellpadding="0"
        width="100%" border="0">
        <tr>
            <td class="ui-state-default ui-widget-header ui-corner-top">
                <asp:ListView ID="lstPrivadas" runat="server">
                    <LayoutTemplate>
                        <table class="ui-jqgrid ui-widget ui-widget-content ui-corner-all" cellspacing="0"
                            cellpadding="0" border="0" width="750">
                            <thead>
                                <tr>
                                    <th colspan="14" class="ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix"
                                        style="height: 20px;">
                                        <span class="ui-jqgrid-title">Parcelas Privadas</span>
                                    </th>
                                </tr>
                                <tr class="ui-jqgrid-labels" style="height: 22px;">
                                    <th class="ui-state-default ui-th-column ui-th-ltr" width="80">Prefeitura
                                    </th>
                                    <th class="ui-state-default ui-th-column ui-th-ltr" width="80">Protecao
                                    </th>
                                    <th class="ui-state-default ui-th-column ui-th-ltr" width="120">Mes 1
                                    </th>
                                    <th class="ui-state-default ui-th-column ui-th-ltr" width="120">mes 2
                                    </th>
                                    <th class="ui-state-default ui-th-column ui-th-ltr" width="120">mes 2
                                    </th>
                                    <th class="ui-state-default ui-th-column ui-th-ltr" width="120">mes 4
                                    </th>
                                    <th class="ui-state-default ui-th-column ui-th-ltr" width="120">mes 5
                                    </th>
                                    <th class="ui-state-default ui-th-column ui-th-ltr" width="120">mes 6
                                    </th>
                                    <th class="ui-state-default ui-th-column ui-th-ltr" width="120">mes 7
                                    </th>
                                    <th class="ui-state-default ui-th-column ui-th-ltr" width="120">mes 8
                                    </th>
                                    <th class="ui-state-default ui-th-column ui-th-ltr" width="120">mes 9
                                    </th>
                                    <th class="ui-state-default ui-th-column ui-th-ltr" width="120">mes 10
                                    </th>
                                    <th class="ui-state-default ui-th-column ui-th-ltr" width="120">mes 11
                                    </th>
                                    <th class="ui-state-default ui-th-column ui-th-ltr" width="120">mes 12
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr class="jqgfirstrow" style="height: auto;">
                                    <td style="height: 0px;"></td>
                                    <td style="height: 0px;"></td>
                                    <td style="height: 0px;"></td>
                                    <td style="height: 0px;"></td>
                                    <td style="height: 0px;"></td>
                                    <td style="height: 0px;"></td>
                                    <td style="height: 0px;"></td>
                                    <td style="height: 0px;"></td>
                                    <td style="height: 0px;"></td>
                                    <td style="height: 0px;"></td>
                                    <td style="height: 0px;"></td>
                                    <td style="height: 0px;"></td>
                                </tr>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </tbody>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr class="ui-widget-content row" style="height: 22px;">
                            <td class="align-center">
                                <%#DataBinder.Eval(Container.DataItem, "IdPrefeitura")%>
                            </td>
                            <td class="align-center">
                                <%#DataBinder.Eval(Container.DataItem, "IdTipoProtecao")%>
                            </td>
                            <td class="align-center">
                                <%#DataBinder.Eval(Container.DataItem, "valorServicosTerceirosMes1", "{0:C}")%>
                            </td>
                            <td class="align-center">
                                <%#DataBinder.Eval(Container.DataItem, "valorServicosTerceirosMes2", "{0:C}" )%>
                            </td>
                            <td class="align-center">
                                <%#DataBinder.Eval(Container.DataItem, "valorServicosTerceirosMes3", "{0:C}")%>
                            </td>
                            <td class="align-center">
                                <%#DataBinder.Eval(Container.DataItem, "valorServicosTerceirosMes4", "{0:C}")%>
                            </td>
                            <td class="align-center">
                                <%#DataBinder.Eval(Container.DataItem, "valorServicosTerceirosMes5", "{0:C}")%>
                            </td>
                            <td class="align-center">
                                <%#DataBinder.Eval(Container.DataItem, "valorServicosTerceirosMes6", "{0:C}")%>
                            </td>
                            <td class="align-center">
                                <%#DataBinder.Eval(Container.DataItem, "valorServicosTerceirosMes7", "{0:C}")%>
                            </td>
                            <td class="align-center">
                                <%#DataBinder.Eval(Container.DataItem, "valorServicosTerceirosMes8", "{0:C}")%>
                            </td>
                            <td class="align-center">
                                <%#DataBinder.Eval(Container.DataItem, "valorServicosTerceirosMes9", "{0:C}")%>
                            </td>
                            <td class="align-center">
                                <%#DataBinder.Eval(Container.DataItem, "valorServicosTerceirosMes10", "{0:C}")%>
                            </td>
                            <td class="align-center">
                                <%#DataBinder.Eval(Container.DataItem, "valorServicosTerceirosMes11", "{0:C}")%>
                            </td>
                            <td class="align-center">
                                <%#DataBinder.Eval(Container.DataItem, "valorServicosTerceirosMes12", "{0:C}")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnSalvar" runat="server" SkinID="button-save" Text="Salvar" Width="89px"
                    Height="25px" OnClick="btnSalvar_Click"></asp:Button>

            </td>

        </tr>

            <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                width="100%" align="center" border="0">
                <tr>
                    <td align="left" class="ui-state-highlight titulo" style="padding: 2px 10px 2px 10px;">
                        <img src="../Styles/Icones/messagebox_warning.png" align="absMiddle" /><b style='color: #000000 !important'>Verifique
                                        as inconsistências:</b>
                        <br />
                        <br />
                        <asp:Label ID="lblInconsistencias" ForeColor="Red" runat="server" />
                    </td>
                </tr>
            </table>
        </tr>
    </table>
    <%--<asp:GridView ID="gridParcelasPublicas" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="idPrefeitura" HeaderText="Prefeitura" />
            <asp:BoundField DataField="idTipoProtecao" HeaderText="Proteção" />
            <asp:BoundField DataField="ValorServicosTerceirosMes1" HeaderText="parcela 1" />
             <asp:BoundField DataField="ValorServicosTerceirosMes2" HeaderText="parcela 2" />
             <asp:BoundField DataField="ValorServicosTerceirosMes3" HeaderText="parcela 3" />
             <asp:BoundField DataField="ValorServicosTerceirosMes4" HeaderText="parcela 4" />
             <asp:BoundField DataField="ValorServicosTerceirosMes5" HeaderText="parcela 5" />
             <asp:BoundField DataField="ValorServicosTerceirosMes6" HeaderText="parcela 6" />
             <asp:BoundField DataField="ValorServicosTerceirosMes7" HeaderText="parcela 7" />
             <asp:BoundField DataField="ValorServicosTerceirosMes8" HeaderText="parcela 8" />
             <asp:BoundField DataField="ValorServicosTerceirosMes9" HeaderText="parcela 9" />
             <asp:BoundField DataField="ValorServicosTerceirosMes10" HeaderText="parcela 10" />
             <asp:BoundField DataField="ValorServicosTerceirosMes11" HeaderText="parcela 11" />
             <asp:BoundField DataField="ValorServicosTerceirosMes12" HeaderText="parcela 12" />
        </Columns>
    </asp:GridView>--%>
</asp:Content>
