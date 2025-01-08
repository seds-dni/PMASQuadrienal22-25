<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true" CodeBehind="RServicosIntermunicipais.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RServicosIntermunicipais" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table {
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBoundGrupo">
        <LayoutTemplate>
            <table cellspacing="0" id="tbReport" runat="server" cellpadding="0" border="0" class="ui-widget ui-widget-content ui-corner-all">
                <thead>
                    <tr style="background-color: #a6c9e2;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="30" rowspan="2">Seq.
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="160" colspan="2">Códigos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="120" rowspan="2">Município
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="120" rowspan="2">DRADS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="70" rowspan="2">Tipo da <br />unidade
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="70" rowspan="2">CNPJ
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="220" rowspan="2">Nome da unidade
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="200" rowspan="2">Local de execução<br />
                            dos serviços
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="100" rowspan="2">Cidade
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120" rowspan="2">Proteção Social
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="250" rowspan="2">Tipo serviço
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100" rowspan="2">Abrangência
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120" rowspan="2">Usuários
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" rowspan="2">Sexo
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100" rowspan="2">Região de moradia
                        </th>
                        <th align="center" colspan="2" class="ui-state-default ui-th-column ui-th-ltr freezing_report">Número de trabalhadores
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="150" rowspan="2">Demanda prioritária do<br />
                            serviço proveniente de
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100" rowspan="2">Previsão mensal de<br />
                            n&#186; de atendidos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100" rowspan="2">Previsão anual de<br />
                            n&#186; de atendidos
                        </th>
                        <th align="center" colspan="9" class="ui-state-default ui-th-column ui-th-ltr freezing_report">Origem dos recursos de cofinanciamento via fundos (valores anuais)
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" rowspan="2">Recursos privados
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100" rowspan="2">Total de recursos
                        </th>
                    </tr>
                    <tr style="background-color: #a6c9e2;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="60">Unidade
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="100">Local de execução<br />
                            ou ID-SUAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="60">Local de<br /> execução
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="50">Serviço
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">FMAS (R$)
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">FMDCA (R$)
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">FMI (R$)
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">FEAS (R$)
                        </th>
                           <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">FEAS reprogramado(R$)
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">FEDCA (R$)
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">FEI (R$)
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">FNAS (R$)
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">FNDCA (R$)
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">FNI (R$)
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="height: 22px; background-color: #a6c9e2;">
                        <td align="right" colspan="18" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <b>Totais:</b>
                        </td>
                        <%--<td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblNumeroTrabalhadoresLocalExecucao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblNumeroTrabalhadoresServico" runat="server" Font-Bold="true" />
                        </td>--%>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">&nbsp;
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblNumeroAtendidosMensal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblNumeroAtendidosAnual" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFMAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFMDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFMI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFEAS" runat="server" Font-Bold="true" />
                        </td>
                         <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFEASAnoAnterior" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFEDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFEI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFNAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFNDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFNI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalPrivado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
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
                        <td class="align-center">
                            <%#DataBinder.Eval(Container.DataItem, "CNPJ") %>&nbsp
                        </td>
                        <td class="align-left">
                            <%#DataBinder.Eval(Container.DataItem, "UnidadeResponsavel") %>
                        </td>
                        <td class="align-left">
                            <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                        </td>
                        <td class="align-center">
                            <%#DataBinder.Eval(Container.DataItem, "Cidade") %>
                        </td>
                        <td class="align-left">
                            <%#DataBinder.Eval(Container.DataItem, "ProtecaoSocial") %>
                        </td>
                        <td class="align-left">
                            <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                        </td>
                        <td class="align-left">
                            <%#DataBinder.Eval(Container.DataItem, "Abrangencia") %>
                        </td>
                        <td class="align-left">
                            <%#DataBinder.Eval(Container.DataItem, "Usuarios") %>
                        </td>
                        <td class="align-left">
                            <%#DataBinder.Eval(Container.DataItem, "Sexo") %>
                        </td>
                        <td class="align-left">
                            <%#DataBinder.Eval(Container.DataItem, "RegiaoMoradia") %>
                        </td>
                        <td class="align-center">
                            <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadores")) %>
                        </td>
                        <td class="align-center">
                            <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadoresServico")) %>
                        </td>
                        <td class="align-left">
                            <%#DataBinder.Eval(Container.DataItem, "CaracteristicasTerritorio") %>
                        </td>
                        <td class="align-center">
                            <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosMensal")) %>
                        </td>
                        <td class="align-center">
                            <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosAnual")) %>
                        </td>
                        <td class="align-right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                        </td>
                        <td class="align-right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMDCA"))%>
                        </td>
                        <td class="align-right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMI"))%>
                        </td>
                        <td class="align-right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                        </td>
                        <td class="align-right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEASAnoAnterior"))%>
                        </td>
                        <td class="align-right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEDCA"))%>
                        </td>
                        <td class="align-right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEI"))%>
                        </td>
                        <td class="align-right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%>
                        </td>
                        <td class="align-right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNDCA"))%>
                        </td>
                        <td class="align-right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNI"))%>
                        </td>
                        <td class="align-right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorPrivado"))%>
                        </td>
                        <td class="align-right">
                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total"))%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <tr class="bg-alternative row">
                <td colspan="18" style="height: 22px;" align="right">
                    <b>Totais:</b>
                </td>
                <%-- <td class="align-left">
                    <b><%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadores")) %></b>
                </td>
                <td class="align-left">
                    <b><%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadoresServico")) %></b>
                </td>--%>
                <td class="align-left">&nbsp;
                </td>
                <td class="align-center">
                    <b><%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosMensal")) %></b>
                </td>
                <td class="align-center">
                    <b><%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosAnual")) %></b>
                </td>
                <td class="align-right">
                    <b><%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%></b>
                </td>
                <td class="align-right">
                    <b><%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMDCA"))%></b>
                </td>
                <td class="align-right">
                    <b><%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMI"))%></b>
                </td>
                <td class="align-right">
                    <b><%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%></b>
                </td>
                  <td class="align-right">
                    <b><%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEASAnoAnterior"))%></b>
                </td>
                <td class="align-right">
                    <b><%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEDCA"))%></b>
                </td>
                <td class="align-right">
                    <b><%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEI"))%></b>
                </td>
                <td class="align-right">
                    <b><%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%></b>
                </td>
                <td class="align-right">
                    <b><%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNDCA"))%></b>
                </td>
                <td class="align-right">
                    <b><%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNI"))%></b>
                </td>
                <td class="align-right">
                    <b><%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorPrivado"))%></b>
                </td>
                <td class="align-right">
                    <b><%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total"))%></b>
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
