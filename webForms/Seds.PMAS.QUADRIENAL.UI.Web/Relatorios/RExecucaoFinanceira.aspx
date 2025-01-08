<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RExecucaoFinanceira.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RExecucaoFinanceira" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server">
        <LayoutTemplate>
            <table cellspacing="0" id="tbReport" runat="server" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            width="30">Seq.
                        </th>
                        <th align="center"
                            width="150">Município
                        </th>
                        <th align="center"
                            width="140">DRADS
                        </th>
                        <th align="center"
                            width="180">Proteção Social
                        </th>
                        <th align="center"
                            width="120">Previsão inicial<br />
                            de repasse
                        </th>
                        <th align="center"
                            width="120">Recursos<br />
                            disponibilizados
                        </th>
                        <th align="center"
                            width="120">Resultado de aplicações
                            <br />
                            financeiras
                        </th>
                        <th align="center"
                            width="100">Valores<br />
                            executados
                        </th>
                        <th align="center"
                            width="100">Valores<br />
                            reprogramados
                        </th>
                        <th align="center"
                            width="100">Valores<br />
                            devolvidos
                        </th>
                        <th align="center"
                            width="100">Porcentagem de<br />
                            devolução
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <td align="right" colspan="4">
                            <b>Totais:</b>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPrevisaoInicialRepasse" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalRecursosDisponibilizados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalResultadoAplicacoesFinanceiras" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresExecutados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresReprogramados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalValoresDevolvidos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPorcentagemExecucao" runat="server" Font-Bold="true" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <asp:ListView ID="lstItems" runat="server" DataSource='<%#Eval("Items") %>' OnItemDataBound="lst_ItemDataBound">
                <LayoutTemplate>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="info" style="background-color: #7cc8ff; height: 22px;">
                            <asp:Label ID="lblSequencia" runat="server" />
                        </td>
                        <td align="left">
                            <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                        </td>
                        <td align="left">
                            <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                        </td>
                        <td align="left">
                            <%#DataBinder.Eval(Container.DataItem, "ProtecaoSocial") %>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "PrevisaoInicialFEAS"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "RecursoDisponibilizadoFEAS"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ResultadoAplicacaoFinanceiraFEAS"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosFEAS"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresReprogramadosFEAS"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosFEAS"))%>
                        </td>
                        <td align="right">
                            <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagemExecucaoFEAS"))%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <tr>
                <td class="info" style="background-color: #7cc8ff; height: 22px;" colspan="4" align="right">
                    <b>Sub-Total:</b>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "PrevisaoInicialFEAS"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "RecursoDisponibilizadoFEAS"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ResultadoAplicacaoFinanceiraFEAS"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresExecutadosFEAS"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresReprogramadosFEAS"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValoresDevolvidosFEAS"))%>
                </td>
                <td align="right">
                    <%#String.Format("{0:P2}", DataBinder.Eval(Container.DataItem, "PorcentagemExecucaoFEAS"))%>
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div align="center" style="width: 100%;">
                <b class="titulo">Não foi localizado nenhum registro de acordo com as características
                    selecionadas</b>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
