<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true" CodeBehind="RIndicadores.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RIndicadores" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellpadding="0" border="0" class="table table-header-rotated">
                <thead>
                    <tr style="background-color: #a6c9e2; height: 22px;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            style="background-color: #a6c9e2; height: 22px;" width="30" rowspan="2">Seq.
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="250" rowspan="2">Município
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="250" rowspan="2">DRADS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            colspan="11">Território e demografia
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            colspan="12">População e vulnerabilidade Social
                        </th>

                    </tr>
                    <tr style="background-color: #a6c9e2;">
                        <th class="rotate">
                            <div><span>Área&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>População&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>Densidade&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>Migratório</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>Crescimento&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>Natalidade&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>< 15 anos&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>> 60 anos&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>Domicílios&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>Urbanização</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>Esgoto</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>< ¼ do SM</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>> ¼ do SM</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>Empregos</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>% fora da escola</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>PCD</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>Dependência</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>IRPS 2000</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>IRPS 2010</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>Gini 2000</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>Gini 2010</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>IPVS G5</span></div>
                        </th>
                        <th class="rotate">
                            <div><span>IPVS G7</span></div>
                        </th>
                    </tr>
                    <%--<tr style="background-color: #a6c9e2;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">FMAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">Orçamento
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">Outros fundos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">FEAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">Orçamento
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">Outros fundos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">FNAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">Orçamento
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">Outros fundos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">IGD-PBF
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">IGD-SUAS
                        </th>
                    </tr>--%>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tbody>
                    <tr id="Tr1" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="height: 22px; background-color: #a6c9e2;">
                        <%--<td align="right" colspan="5" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <b>Totais:</b>
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalMetaSEDS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalMetaMunicipio" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right" colspan="4"></td>
                        <td align="right"  class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFamiliasBeneficiarias" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right" colspan="2"></td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFMAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalOrcamentoMunicipal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFundoMunicipal" runat="server" Font-Bold="true" />
                        </td>
                         <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalFEAS" runat="server" Font-Bold="true" />
                        </td>
                         <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalOrcamentoEstadual" runat="server" Font-Bold="true" />
                        </td>
                          <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalFundoEstadual" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFNAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalOrcamentoFederal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFundoFederal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalIGDPBF" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalIGDSUAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalRecursos" runat="server" Font-Bold="true" />
                        </td>--%>
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
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "NumeroHabitantes")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "AreaTerritorial")) %>
                </td>
                <td class="align-left">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "DensidadeDemografica")) %>
                </td>
                <td class="align-left">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "CrescimentoAnual")) %>
                </td>
                <td class="align-left">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "DensidadeDemografica")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "SaldoMigratorioAnual")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "TaxaNatalidade")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "PessoasAbaixo15Anos") )%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "PessoasAcima60Anos") )%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "DomiciliosPermanentes") )%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "SaneamentoBasico")) %>
                </td>
                <td class="align-right"></td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "DomiciliosInferiorUmQuartoNumero") )%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "EmpregosFormais") )%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "Menores15Anos")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "PessoasDeficientesNumero")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RazaoDependencia")) %>
                </td>
                <td class="align-right">
                    <%#DataBinder.Eval(Container.DataItem, "IRPS2010") %>
                </td>
                <td class="align-right">
                        <%#DataBinder.Eval(Container.DataItem, "IRPS2012") %>
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
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "NumeroHabitantes")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "AreaTerritorial")) %>
                </td>
                <td class="align-left">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "DensidadeDemografica")) %>
                </td>
                <td class="align-left">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "CrescimentoAnual")) %>
                </td>
                <td class="align-left">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "DensidadeDemografica")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "SaldoMigratorioAnual")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "TaxaNatalidade")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "PessoasAbaixo15Anos") )%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "PessoasAcima60Anos") )%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "DomiciliosPermanentes") )%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "SaneamentoBasico")) %>
                </td>
                <td class="align-right">
                   <%-- <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "DomiciliosInferior70Percentual")) %>--%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "DomiciliosInferiorUmQuartoNumero") )%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "EmpregosFormais") )%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "Menores15Anos")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "PessoasDeficientesNumero")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "RazaoDependencia")) %>
                </td>
                <td class="align-right">
                    <%#DataBinder.Eval(Container.DataItem, "IRPS2010") %>
                </td>
                <td class="align-right">
                    <%#DataBinder.Eval(Container.DataItem, "IRPS2012") %>
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
