<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RAnaliseDiagnostica.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RAnaliseDiagnostica" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr style="background-color: #7cc8ff;">
                        <th align="center" width="30">Seq.
                        </th>
                        <th align="center" width="150">Município
                        </th>
                        <th align="center" width="150">Porte
                        </th>
                        <th align="center" width="150">DRADS
                        </th>
                        <th align="center"  width="350">Situações de vulnerabilidade ou risco mais graves
                        </th>
                        <th width="100">Classificação
                        </th>
                        <th width="150">Demanda estimada
                        </th>
                        <th width="200">Número de serviços existentes<br />
                            que atendem a esta demanda
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
               <!-- <tfoot class="info">
                    <tr style="background-color:#7cc8ff; height:20px;">
                        <td align="right" colspan="6">
                           
                        </td>
                        <td align="right">
                            <asp:Label ID="lblDemanda" runat="server" Font-Bold="true" Visible="false"  />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroServicos" runat="server" Font-Bold="true" Visible="false" />
                        </td>
                    </tr>
                </tfoot>-->
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td style="background-color: #7cc8ff;height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "SituacaoVulnerabilidade")%>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "Classificacao") %>
                </td>
                <td align="center">
                    <%#String.Format("{0:N0}",DataBinder.Eval(Container.DataItem, "Demanda"))%>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "TotalServicos")%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td style="background-color: #7cc8ff;height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "SituacaoVulnerabilidade")%>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "Classificacao") %>
                </td>
                <td align="center">
                    <%#String.Format("{0:N0}",DataBinder.Eval(Container.DataItem, "Demanda"))%>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "TotalServicos")%>
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
