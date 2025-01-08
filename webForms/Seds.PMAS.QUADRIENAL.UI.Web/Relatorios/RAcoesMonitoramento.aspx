<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RAcoesMonitoramento.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RAcoesMonitoramento" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table {
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound">
        <LayoutTemplate>
            <table cellspacing="0" id="tbReport" runat="server" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info">
                        <th align="center"
                            width="30" rowspan="2">Seq.
                        </th>
                        <th align="center"
                            width="120" rowspan="2">Município
                        </th>
                        <th align="center"
                            width="250" rowspan="2">DRADS
                        </th>
                        <th align="center"
                            width="80" rowspan="2">Realiza ações
                            <br />
                            de monitoramento?
                        </th>
                        <th align="center" colspan="2" style="height: 22px;">Quem executa
                            <br />
                            esse monitoramento?
                        </th>
                        <th align="center" colspan="2" style="height: 22px;">O que é monitorado
                        </th>
                        <th align="center" colspan="4">Procedimentos utilizados para monitoramento
                        </th>
                    </tr>
                    <tr class="info">
                        <th align="center"
                            width="100">Orgão Gestor
                        </th>
                        <th align="center"
                            width="100">Serviço terceirizado
                        </th>
                        <th align="center"
                            width="80">Rede pública
                        </th>
                        <th align="center"
                            width="80">Rede privada
                        </th>
                        <th align="center"
                            width="100">Envio de informações<br />
                            pelo serviços
                        </th>
                        <th align="center"
                            width="100">Reuniões com<br />
                            executores
                        </th>
                        <th align="center"
                            width="100">Reuniões com<br />
                            usuários
                        </th>
                        <th align="center"
                            width="100">Visitas de<br />
                            supervisão
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="height: 22px;" class="info">
                        <td align="right" colspan="3">
                            <b>Totais:</b>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalRealizaMonitoramento" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalOperacionalizadoOrgaoGestor" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalOperacionalizadoTerceirizado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalMonitoradoRedePublica" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalMonitoradoRedePrivada" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalEnvioInformacoes" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalReunioesExecutores" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalReuniaoUsuarios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalVisitasSupervisao" runat="server" Font-Bold="true" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td style="height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "RealizaMonitoramento")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "OperacionalizadoOrgaoGestor")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "OperacionalizadoTerceirizado")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "MonitoradoRedePublica")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "MonitoradoRedePrivada")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "EnvioInformacoes")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "ReunioesExecutores")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "ReuniaoUsuarios")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "VisitasSupervisao")%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td cstyle="height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "RealizaMonitoramento")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "OperacionalizadoOrgaoGestor")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "OperacionalizadoTerceirizado")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "MonitoradoRedePublica")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "MonitoradoRedePrivada")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "EnvioInformacoes")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "ReunioesExecutores")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "ReuniaoUsuarios")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "VisitasSupervisao")%>
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
