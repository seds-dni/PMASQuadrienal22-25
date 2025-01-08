<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RAtividadesServicosSocioassistenciais.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RAtividadesServicosSocioassistenciais" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table
        {
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound">
        <LayoutTemplate>
            <table cellspacing="0" id="tbReport" runat="server" cellpadding="0" border="0" class="ui-widget ui-widget-content ui-corner-all">
                <thead>
                    <tr style="background-color: #a6c9e2;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="30" rowspan="2">
                            Seq.
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="160" colspan="2">
                            Códigos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="120" rowspan="2">
                            Município
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="120" rowspan="2">
                            DRADS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="70" rowspan="2">
                            Tipo da unidade
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="120" rowspan="2">
                            Nome da unidade
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="120" rowspan="2">
                            Local de execução dos serviços
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="120" rowspan="2">
                            Proteção Social
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="150" rowspan="2">
                            Tipo serviço
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="120" rowspan="2">
                            Usuários
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="100" rowspan="2">
                            Previsão mensal de n&#186; de atendidos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="100" rowspan="2">
                            Previsão anual de n&#186; de atendidos
                        </th>
                        <th align="center" colspan="42" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            style="height: 22px;">
                            Atividades desenvolvidas (vide legenda abaixo)
                        </th>
                    </tr>
                    <tr style="background-color: #a6c9e2;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="60">
                            Unidade
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="100">
                            Local de execução ou ID-SUAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            1
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            2
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            3
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            4
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            5
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            6
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            7
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            8
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            9
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            10
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            11
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            12
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            13
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            14
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            15
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            16
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            17
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            18
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            19
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            20
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            21
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            22
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            23
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            24
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            25
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            26
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            27
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            28
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            29
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            30
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            31
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            32
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            33
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            34
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            35
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            36
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            37
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            38
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            39
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            40
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            41
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40">
                            42
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="height: 22px; background-color: #a6c9e2;">
                        <td align="right" colspan="11" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <b>Totais:</b>
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalNumeroAtendidosMensal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalNumeroAtendidosAnual" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade1" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade2" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade3" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade4" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade5" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade6" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade7" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade8" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade9" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade10" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade11" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade12" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade13" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade14" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade15" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade16" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade17" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade18" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade19" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade20" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade21" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade22" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade23" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade24" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade25" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade26" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade27" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade28" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade29" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade30" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade31" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade32" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade33" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade34" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade35" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade36" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade37" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade38" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade39" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade40" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade41" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAtividade42" runat="server" Font-Bold="true" />
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
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "CodigoUnidade") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "IdLocal")%>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "UnidadeResponsavel") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "ProtecaoSocial") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Usuarios") %>
                </td>
                <td class="align-left">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosMensal")) %>
                </td>
                <td class="align-left">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosAnual")) %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade1")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade2")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade3")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade4")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade5")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade6")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade7")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade8")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade9")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade10")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade11")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade12")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade13")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade14")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade15")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade16")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade17")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade18")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade19")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade20")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade21")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade22")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade23")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade24")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade25")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade26")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade27")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade28")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade29")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade30")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade31")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade32")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade33")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade34")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade35")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade36")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade37")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade38")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade39")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade40")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade41")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade42")%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="row" style="background-color: #F3F3F3 !important;">
                <td class="ui-state-default jqgrid-rownum row" style="height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "CodigoUnidade") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "IdLocal")%>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "UnidadeResponsavel") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "ProtecaoSocial") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Usuarios") %>
                </td>
                <td class="align-left">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosMensal")) %>
                </td>
                <td class="align-left">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosAnual")) %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade1")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade2")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade3")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade4")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade5")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade6")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade7")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade8")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade9")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade10")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade11")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade12")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade13")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade14")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade15")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade16")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade17")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade18")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade19")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade20")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade21")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade22")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade23")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade24")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade25")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade26")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade27")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade28")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade29")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade30")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade31")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade32")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade33")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade34")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade35")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade36")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade37")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade38")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade39")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade40")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade41")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Atividade42")%>
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
    <br /><br />
    <table cellspacing="0" cellpadding="0" border="0" class="ui-widget ui-widget-content ui-corner-all">
        <tr>
            <td colspan="6" class="ui-state-default ui-th-column" style="height:22px;">
                Legenda colunas 1 a 42
            </td>
        </tr>
        <tr class="row" style="height: 22px;">
            <td class="align-left">
                1 - Acolhida
            </td>
            <td class="align-left">
                8 - Orientação sociofamiliar
            </td>
            <td class="align-left">
                15 - Cadastramento socioeconômico
            </td>
            <td class="align-left">
                22 - Mobilização e fortalecimento de redes sociais de apoio
            </td>
            <td class="align-left">
                29 - Articulação com outras politicas setoriais
            </td>
            <td class="align-left">
                36 - Atividades comunitárias
            </td>
        </tr>
        <tr class="row" style="background-color: #F3F3F3 !important; height: 22px;">
            <td class="align-left">
                2 - Escuta
            </td>
            <td class="align-left">
                9 - Orientação jurídico-social
            </td>
            <td class="align-left">
                16 - Apoio à família na sua função protetiva
            </td>
            <td class="align-left">
                23 - Mobilização para o exercicio da cidadania
            </td>
            <td class="align-left">
                30 - Conhecimento do território
            </td>
            <td class="align-left">
                37 - Atividades de convívio e de organização da vida cotidiana
            </td>
        </tr>
        <tr class="row" style="height: 22px;">
            <td class="align-left">
                3 - Estudo social
            </td>
            <td class="align-left">
                10 - Atendimento psicossocial
            </td>
            <td class="align-left">
                17 - Desenvolvimento do convívio familiar, grupal e social
            </td>
            <td class="align-left">
                24 - Abrigamento
            </td>
            <td class="align-left">
                31 - Referência e contrarreferência
            </td>
            <td class="align-left">
                38 - Grupo socioeducativos
            </td>
        </tr>
        <tr class="row" style="background-color: #F3F3F3 !important; height: 22px;">
            <td class="align-left">
                4 - Visita domiciliar
            </td>
            <td class="align-left">
                11 - Orientação e encaminhamentos para a rede de serviços locais
            </td>
            <td class="align-left">
                18 - Identificação e mobilização de família extensa ou ampliada
            </td>
            <td class="align-left">
                25 - Ações voltadas para o desabrigamento
            </td>
            <td class="align-left">
                32 - Busca ativa
            </td>
            <td class="align-left">
                39 - Acompanhamento da frequência escolar
            </td>
        </tr>
        <tr class="row" style="height: 22px;">
            <td class="align-left">
                5 - Elaboração de Plano de Acompanhamento Familiar - PAF
            </td>
            <td class="align-left">
                12 - Promoção de acesso a documentação pessoal
            </td>
            <td class="align-left">
                19 - Fortalecimento da função protetiva da família
            </td>
            <td class="align-left">
                26 - Articulação da rede de serviços socioassistenciais
            </td>
            <td class="align-left">
                33 - Campanhas socioeducativas
            </td>
            <td class="align-left">
                40 - Atividades artísticas/culturais
            </td>
        </tr>
        <tr class="row" style="background-color: #F3F3F3 !important; height: 22px;">
            <td class="align-left">
                6 - Elaboração de Plano Individual de Acompanhamento - PIA
            </td>
            <td class="align-left">
                13 - Reingresso escolar
            </td>
            <td class="align-left">
                20 - Desenvolvimento de autonomia pessoal
            </td>
            <td class="align-left">
                27 - Articulação com o Sistema de Garantia de Direitos
            </td>
            <td class="align-left">
                34 - Produção de orientações técnicas e materiais informativos
            </td>
            <td class="align-left">
                41 - Atividades físicas e esportivas
            </td>
        </tr>
        <tr class="row" style="height: 22px;">
            <td class="align-left">
                7 - Elaboração de relatórios e/ou prontuários
            </td>
            <td class="align-left">
                14 - Diagnóstico e encaminhamento para cadastramento socioeconômico
            </td>
            <td class="align-left">
                21 - Informação, comunicação e defesa de direitos
            </td>
            <td class="align-left">
                28 - Articulação com orgãos de capacitação e preparação para o trabalho
            </td>
            <td class="align-left">
                35 - Realização de palestras
            </td>
            <td class="align-left">
                42 - Atividades laborterápicas
            </td>
        </tr>
    </table>
</asp:Content>
