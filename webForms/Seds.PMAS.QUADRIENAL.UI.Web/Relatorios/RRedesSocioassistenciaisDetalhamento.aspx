<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RRedesSocioassistenciaisDetalhamento.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RRedesSocioassistenciaisDetalhamento" %>

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
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            width="30" rowspan="2">Seq.
                        </th>
                        <th align="center"
                            style="width: 200px !important;" colspan="2">Códigos
                        </th>
                        <th align="center"
                            style="width: 200px !important;" rowspan="2">Município
                        </th>
                        <th align="center"
                            width="120" rowspan="2">Porte
                        </th>
                        <th align="center"
                            width="120" rowspan="2">Distrito
                        </th>
                        <th align="center"
                            width="120" rowspan="2">DRADS
                        </th>
                        <th align="center"
                            width="120" rowspan="2">Tipo de rede
                        </th>
                        <th align="center"
                            width="250" rowspan="2">Nome da organização/unidade
                        </th>

                        <th align="center"
                            width="220" rowspan="2">Local de execução
                            dos serviços
                        </th>
                        <th align="center" width="220" rowspan="2">Proteção social
                        </th>
                        <th align="center" width="360" rowspan="2">Tipo de serviço
                        </th>
                        <th align="center" width="120" rowspan="2">Usuários
                        </th>
                        <th align="center" width="150" rowspan="2">Capacidade mensal de atendimento
                        </th>
                        <th align="center" width="100" colspan="4">Média mensal de atendimento
                        </th>
                        <%-- <th align="center" rowspan="2" width="100">Número de<br />
                            trabalhadores<br />
                            do serviço
                        </th>--%>
                        <%--   <th align="center" width="150" rowspan="2">Demanda prioritária do<br />
                            serviço proveniente de
                        </th>--%>
                        <th align="center" width="250" rowspan="2">Situações de vulnerabilidade<br />
                            ou risco social atendidas
                        </th>
                        <th align="center" width="250" rowspan="2">Principais situações
                            <br />
                            vivenciadas pelos usuários
                        </th>
                        <th align="center" width="300" rowspan="2">Trabalho social desenvolvido
                        </th>
                        <%--    <th align="center" colspan="10">Origem dos recursos de cofinanciamento via fundos<br />
                            (valores anuais)
                        </th>
                        <th align="center" width="80" rowspan="2">Recursos privados
                        </th>
                        <th align="center" width="80" rowspan="2">Valor do convênio<br />
                            estadualizado
                        </th>
                        <th align="center" width="100" rowspan="2">Total de recursos
                        </th>--%>
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            width="100">Organização/<br />
                            Unidade
                        </th>
                        <th align="center"
                            width="120">local de execução<br />
                            ou ID-SUAS
                        </th>
                        <th width="50">2017</th>
                        <th width="50">2018</th>
                        <th width="50">2019</th>
                        <th width="50">2020</th>
                        <%-- <th align="center"
                            width="60">
                            Local de<br />execução
                        </th>
                        <th align="center"
                            width="60">
                            Serviço
                        </th> --%>
                        <%-- <th align="center" width="80">FMAS (R$)
                        </th>
                        <th align="center" width="80">FMDCA (R$)
                        </th>
                        <th align="center" width="80">FMI (R$)
                        </th>
                        <th align="center" width="80">FEAS (R$)
                        </th>
                        <th align="center" width="80">FEAS reprogramado(R$)
                        </th>
                        <th align="center" width="80">FEDCA (R$)
                        </th>
                        <th align="center" width="80">FEI (R$)
                        </th>
                        <th align="center" width="80">FNAS (R$)
                        </th>
                        <th align="center" width="80">FNDCA (R$)
                        </th>
                        <th align="center" width="80">FNI (R$)
                        </th>--%>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <td align="right" colspan="13">
                            <b>Totais:</b>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2017" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2018" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2019" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2020" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2017" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2018" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2019" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2020" runat="server" Font-Bold="true" />
                        </td>
                        <td colspan="3"></td>
                        <%--  <td align="right" >
                            <asp:Label ID="lblNumeroTrabalhadoresLocalExecucao" runat="server" Font-Bold="true" />
                        </td>--%>
                        <%-- <td align="right">
                            <asp:Label ID="lblNumeroTrabalhadoresServico" runat="server" Font-Bold="true" />
                        </td>--%>
                        <%--  <td align="right" colspan="4">&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEASAnoAnterior" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPrivado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalEstadualizado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
                        </td>--%>
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="info" style="background-color: #7cc8ff; height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "CodigoUnidade") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "IdLocal")%>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "DistritoSaoPaulo") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "UnidadeResponsavel") %>
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                </td>

                <td>
                    <%#DataBinder.Eval(Container.DataItem, "ProtecaoSocial") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Usuarios") %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumertoTotalAtendidosMensal")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2017")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2018")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2019")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2020")) %>
                </td>
                <%-- <td >
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadoresServico")) %>
                </td>--%>

                <%-- <td >
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadores")) %>
                </td>--%>
                <%--   <td >
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadoresServico")) %>
                </td>--%>
                <%--   <td >
                    <%#DataBinder.Eval(Container.DataItem, "CaracteristicasTerritorio") %>
                </td>--%>
                <td>
                    <asp:ListView ID="lstSituacoesVulnerabilidades" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "SituacoesVulnerabilidade")%>'>
                        <LayoutTemplate>
                            <ul>
                                <li id="itemPlaceholder" runat="server"></li>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <%#Container.DataItem %>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
                <td>
                    <asp:ListView ID="lstSituacoesEspecificas" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "SituacoesEspecificas")%>'>
                        <LayoutTemplate>
                            <ul>
                                <li id="itemPlaceholder" runat="server"></li>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <%#Container.DataItem %>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
                <td>
                    <asp:ListView ID="lstAtividadesSocioassistenciais" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "AtividadesSocioassistenciais")%>'>
                        <LayoutTemplate>
                            <ul>
                                <li id="itemPlaceholder" runat="server"></li>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <%#Container.DataItem %>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
                <%-- <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMDCA"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMI"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEASAnoAnterior"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEDCA"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEI"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNDCA"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNI"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorPrivado"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorEstadualizado"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total"))%>
                </td>--%>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td class="info" style="background-color: #7cc8ff; height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "CodigoUnidade") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "IdLocal")%>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "DistritoSaoPaulo") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "UnidadeResponsavel") %>
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                </td>

                <td>
                    <%#DataBinder.Eval(Container.DataItem, "ProtecaoSocial") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Usuarios") %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumertoTotalAtendidosMensal")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2017")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2018")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2019")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaTotal2020")) %>
                </td>
                <%--      <td >
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadoresServico")) %>
                </td>--%>
                <%-- <td >
                    <%#DataBinder.Eval(Container.DataItem, "CaracteristicasTerritorio") %>
                </td>--%>
                <td>
                    <asp:ListView ID="lstSituacoesVulnerabilidades" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "SituacoesVulnerabilidade")%>'>
                        <LayoutTemplate>
                            <ul>
                                <li id="itemPlaceholder" runat="server"></li>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <%#Container.DataItem %>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
                <td>
                    <asp:ListView ID="lstSituacoesEspecificas" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "SituacoesEspecificas")%>'>
                        <LayoutTemplate>
                            <ul>
                                <li id="itemPlaceholder" runat="server"></li>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <%#Container.DataItem %>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
                <td>
                    <asp:ListView ID="lstAtividadesSocioassistenciais" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "AtividadesSocioassistenciais")%>'>
                        <LayoutTemplate>
                            <ul>
                                <li id="itemPlaceholder" runat="server"></li>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <%#Container.DataItem %>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
                <%--  <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMDCA"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMI"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEASAnoAnterior"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEDCA"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEI"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNDCA"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNI"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorPrivado"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorEstadualizado"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total"))%>
                </td>--%>
            </tr>
        </AlternatingItemTemplate>
        <EmptyDataTemplate>
            <div align="center" style="width: 100%;">
                <b class="titulo">Não foi localizado nenhum registro de acordo com as características
                    selecionadas</b>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>

    <asp:ListView ID="lstServicoPSC" runat="server" OnItemDataBound="lst_ItemDataBound" Visible="false">
        <LayoutTemplate>
            <table cellspacing="0" id="tbReport" runat="server" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            width="30" rowspan="3">Seq.
                        </th>
                        <th align="center"
                            style="width: 200px !important;" colspan="2">Organização/Unidade
                        </th>
                        <th align="center"
                            style="width: 200px !important;" rowspan="3">Município
                        </th>
                        <th align="center"
                            width="120" rowspan="3">Porte
                        </th>
                        <th align="center"
                            width="120" rowspan="3">Distrito
                        </th>
                        <th align="center"
                            width="120" rowspan="3">DRADS
                        </th>
                        <th align="center"
                            width="100" rowspan="3">Tipo de rede
                        </th>
                        <th align="center"
                            width="220" rowspan="3">Nome da organização/unidade
                        </th>

                        <th align="center"
                            width="200" rowspan="3">Local de execução
                            <br />
                            dos serviços
                        </th>
                        <th align="center" width="220" rowspan="3">Proteção social
                        </th>
                        <th align="center" width="360" rowspan="3">Tipo de serviço
                        </th>
                        <th align="center" width="120" rowspan="3">Usuários
                        </th>
                        <th align="center" width="100" colspan="8">Capacidade mensal de atendimento
                        </th>
                        <th align="center" width="100" colspan="8">Média mensal de atendimento
                        </th>
                        <%-- <th align="center" rowspan="2" width="100">Número de<br />
                            trabalhadores<br />
                            do serviço
                        </th>--%>
                        <%--   <th align="center" width="150" rowspan="2">Demanda prioritária do<br />
                            serviço proveniente de
                        </th>--%>
                        <th align="center" width="250" rowspan="3">Situações de vulnerabilidade<br />
                            ou risco social atendidas
                        </th>
                        <th align="center" width="250" rowspan="3">Principais situações
                            <br />
                            vivenciadas pelos usuários
                        </th>
                        <th align="center" width="300" rowspan="3">Trabalho social desenvolvido
                        </th>
                        <%--    <th align="center" colspan="10">Origem dos recursos de cofinanciamento via fundos<br />
                            (valores anuais)
                        </th>
                        <th align="center" width="80" rowspan="2">Recursos privados
                        </th>
                        <th align="center" width="80" rowspan="2">Valor do convênio<br />
                            estadualizado
                        </th>
                        <th align="center" width="100" rowspan="2">Total de recursos
                        </th>--%>
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            width="60" rowspan="2">Unidade
                        </th>
                        <th align="center"
                            width="120" rowspan="2">local de execução<br />
                            ou ID-SUAS
                        </th>
                        <th colspan="2">2017</th>
                        <th colspan="2">2018</th>
                        <th colspan="2">2019</th>
                        <th colspan="2">2020</th>
                        <%-- <th align="center"
                            width="60">
                            Local de<br />execução
                        </th>
                        <th align="center"
                            width="60">
                            Serviço
                        </th> --%>
                        <%-- <th align="center" width="80">FMAS (R$)
                        </th>
                        <th align="center" width="80">FMDCA (R$)
                        </th>
                        <th align="center" width="80">FMI (R$)
                        </th>
                        <th align="center" width="80">FEAS (R$)
                        </th>
                        <th align="center" width="80">FEAS reprogramado(R$)
                        </th>
                        <th align="center" width="80">FEDCA (R$)
                        </th>
                        <th align="center" width="80">FEI (R$)
                        </th>
                        <th align="center" width="80">FNAS (R$)
                        </th>
                        <th align="center" width="80">FNDCA (R$)
                        </th>
                        <th align="center" width="80">FNI (R$)
                        </th>--%>
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th>LA</th>
                        <th>PSC</th>
                        <th>LA</th>
                        <th>PSC</th>
                        <th>LA</th>
                        <th>PSC</th>
                        <th>LA</th>
                        <th>PSC</th>
                        <th>LA</th>
                        <th>PSC</th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <td align="right" colspan="13">
                            <b>Totais:</b>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2017" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensalPSC2017" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2018" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensalPSC2018" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2019" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensalPSC2019" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensal2020" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblNumeroAtendidosMensalPSC2020" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2017" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensalPSC2017" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2018" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensalPSC2018" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2019" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensalPSC2019" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensal2020" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMediaMensalPSC2020" runat="server" Font-Bold="true" />
                        </td>
                        <td colspan="3"></td>
                        <%--  <td align="right" >
                            <asp:Label ID="lblNumeroTrabalhadoresLocalExecucao" runat="server" Font-Bold="true" />
                        </td>--%>
                        <%-- <td align="right">
                            <asp:Label ID="lblNumeroTrabalhadoresServico" runat="server" Font-Bold="true" />
                        </td>--%>
                        <%--  <td align="right" colspan="4">&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFMI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEASAnoAnterior" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFEI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalFNI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalPrivado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalEstadualizado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
                        </td>--%>
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="info" style="background-color: #7cc8ff; height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "CodigoUnidade") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "IdLocal")%>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "DistritoSaoPaulo") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "UnidadeResponsavel") %>
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                </td>

                <td>
                    <%#DataBinder.Eval(Container.DataItem, "ProtecaoSocial") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Usuarios") %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosMensal")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosServicoMensal")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensal2017")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalPSC2017")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensal2018")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalPSC2018")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensal2019")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalPSC2019")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensal2020")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalPSC2020")) %>
                </td>
                <%-- <td >
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadoresServico")) %>
                </td>--%>

                <%-- <td >
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadores")) %>
                </td>--%>
                <%--   <td >
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadoresServico")) %>
                </td>--%>
                <%--   <td >
                    <%#DataBinder.Eval(Container.DataItem, "CaracteristicasTerritorio") %>
                </td>--%>
                <td>
                    <asp:ListView ID="lstSituacoesVulnerabilidades" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "SituacoesVulnerabilidade")%>'>
                        <LayoutTemplate>
                            <ul>
                                <li id="itemPlaceholder" runat="server"></li>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <%#Container.DataItem %>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
                <td>
                    <asp:ListView ID="lstSituacoesEspecificas" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "SituacoesEspecificas")%>'>
                        <LayoutTemplate>
                            <ul>
                                <li id="itemPlaceholder" runat="server"></li>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <%#Container.DataItem %>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
                <td>
                    <asp:ListView ID="lstAtividadesSocioassistenciais" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "AtividadesSocioassistenciais")%>'>
                        <LayoutTemplate>
                            <ul>
                                <li id="itemPlaceholder" runat="server"></li>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <%#Container.DataItem %>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
                <%-- <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMDCA"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMI"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEASAnoAnterior"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEDCA"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEI"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNDCA"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNI"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorPrivado"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorEstadualizado"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total"))%>
                </td>--%>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td class="info" style="background-color: #7cc8ff; height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "CodigoUnidade") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "IdLocal")%>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "DistritoSaoPaulo") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "UnidadeResponsavel") %>
                </td>

                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                </td>

                <td>
                    <%#DataBinder.Eval(Container.DataItem, "ProtecaoSocial") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                </td>
                <td>
                    <%#DataBinder.Eval(Container.DataItem, "Usuarios") %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosMensal")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosServicoMensal")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensal2017")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalPSC2017")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensal2018")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalPSC2018")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensal2019")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalPSC2019")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensal2020")) %>
                </td>
                <td>
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MediaMensalPSC2020")) %>
                </td>
                </td>
                <%--      <td >
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalTrabalhadoresServico")) %>
                </td>--%>
                <%-- <td >
                    <%#DataBinder.Eval(Container.DataItem, "CaracteristicasTerritorio") %>
                </td>--%>
                <td>
                    <asp:ListView ID="lstSituacoesVulnerabilidades" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "SituacoesVulnerabilidade")%>'>
                        <LayoutTemplate>
                            <ul>
                                <li id="itemPlaceholder" runat="server"></li>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <%#Container.DataItem %>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
                <td>
                    <asp:ListView ID="lstSituacoesEspecificas" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "SituacoesEspecificas")%>'>
                        <LayoutTemplate>
                            <ul>
                                <li id="itemPlaceholder" runat="server"></li>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <%#Container.DataItem %>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
                <td>
                    <asp:ListView ID="lstAtividadesSocioassistenciais" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "AtividadesSocioassistenciais")%>'>
                        <LayoutTemplate>
                            <ul>
                                <li id="itemPlaceholder" runat="server"></li>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <%#Container.DataItem %>
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
                <%--  <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMDCA"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMI"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEASAnoAnterior"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEDCA"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEI"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNDCA"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNI"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorPrivado"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorEstadualizado"))%>
                </td>
                <td >
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total"))%>
                </td>--%>
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
