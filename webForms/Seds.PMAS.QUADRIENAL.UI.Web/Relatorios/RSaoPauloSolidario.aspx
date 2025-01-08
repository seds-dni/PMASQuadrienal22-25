<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RSaoPauloSolidario.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RSaoPauloSolidario" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="ui-widget ui-widget-content ui-corner-all">
                <thead>
                    <tr style="background-color: #a6c9e2;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            style="height: 22px;" width="30" rowspan="3">Seq.
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="150" rowspan="3">Município
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="150" rowspan="3">DRADS
                        </th>
                        <th align="left" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="18" style="height: 15px; font-size: 12px;">Busca Ativa
                        </th>
                        <th align="left" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="6" style="height: 15px; font-size: 12px;">Agenda da Família
                        </th>
                        <th align="left" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="13" style="height: 15px; font-size: 12px;">Além da Renda
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" rowspan="3" width="100" colspan="2">Integração com serviços
                        </th>
                    </tr>
                    <tr style="background-color: #a6c9e2;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" rowspan="2" width="100">Data de <br />início
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" rowspan="2" width="100">Data de <br />término
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" rowspan="2" colspan="2">Parcerias
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="3">Órgão(s) que executa(m)
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="11">Recursos financeiros para execução dessa etapa
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="4">Agendas assinadas
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="2">Órgão(s) que executa(m)
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="2" rowspan="2">Parcerias
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="11">Recursos financeiros para execução dessa etapa
                        </th>
                    </tr>
                    <tr style="background-color: #a6c9e2;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">Órgão
                            <br />
                            Gestor
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="60">CRAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">Unidade<br />
                            privada
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">FMAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">Orçamento<br />
                            municipal
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">Outros fundos
                            <br />
                            municipais
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">FEAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">Orçamento
                            <br />
                            estadual
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">Outros fundos<br />
                            estaduais
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">FNAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">Orçamento
                            <br />
                            federal
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">Outros fundos
                            <br />
                            nacionais
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">IGD-PBF
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">IGD-SUAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="60">2012
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="60">2013
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="60">2014
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="60">Total
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">Órgão <br />Gestor
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="60">CRAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">FMAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">Orçamento <br />municipal
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">Outros fundos<br /> municipais
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">FEAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">Orçamento<br /> estadual
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">Outros fundos<br /> estaduais
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">FNAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">Orçamento <br />federal
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">Outros fundos <br />nacionais
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">IGD-PBF
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">IGD-SUAS
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="height: 22px; background-color: #a6c9e2;">
                        <td align="right" colspan="5" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <b>Totais:</b>
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalPossuiParceria" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalParceria" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalBuscaAtivaOrgaoGestorExecuta" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalBuscaAtivaCRASExecuta" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalBuscaAtivaUnidadePrivadaExecuta" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorFMAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorOrcamentoMunicipal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorFundosMunicipais" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorFEAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorOrcamentoEstadual" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorFundosEstaduais" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorFNAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorOrcamentoFederal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorFundosFederais" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorIGDPBF" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorIGDSUAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalAgendaFamiliaNumeroFamilias2012" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalAgendaFamiliaNumeroFamilias2013" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalAgendaFamiliaNumeroFamilias2014" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalAgendaFamiliaNumeroFamilias" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalAgendaFamiliaOrgaoGestorExecuta" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalAgendaFamiliaCRASExecuta" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalPossuiParceriaAgendaFamilia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalParceriaAgendaFamilia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorFMASAgendaFamilia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorOrcamentoMunicipalAgendaFamilia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorFundosMunicipaisAgendaFamilia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorFEASAgendaFamilia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorOrcamentoEstadualAgendaFamilia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorFundosEstaduaisAgendaFamilia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorFNASAgendaFamilia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorOrcamentoFederalAgendaFamilia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorFundosFederaisAgendaFamilia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorIGDPBFAgendaFamilia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalValorIGDSUASAgendaFamilia" runat="server" Font-Bold="true" />
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
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BuscaAtivaInicio") %>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BuscaAtivaTermino") %>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaFormal") %>&nbsp;
                </td>
                <td class="align-center">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalParcerias")) %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BuscaAtivaOrgaoGestorExecuta")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BuscaAtivaCRASExecuta")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BuscaAtivaUnidadePrivadaExecuta")%>&nbsp;
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorFMAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorOrcamentoMunicipal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorFundoMunicipal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorFEAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorOrcamentoEstadual"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorFundoEstadual"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorFNAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorOrcamentoFederal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorFundoFederal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorIGDPBF"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorIGDSUAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaNumeroFamilias2012"))%>&nbsp;
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaNumeroFamilias2013"))%>&nbsp;
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaNumeroFamilias2014"))%>&nbsp;
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaNumeroFamiliasTotal"))%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "AgendaFamiliaOrgaoGestorExecuta")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "AgendaFamiliaCRASExecuta")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaFormalAgendaFamilia") %>&nbsp;
                </td>
                <td class="align-center">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalParceriasAgendaFamilia")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorFMAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorOrcamentoMunicipal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorFundoMunicipal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorFEAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorOrcamentoEstadual"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorFundoEstadual"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorFNAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorOrcamentoFederal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorFundoFederal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorIGDPBF"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorIGDSUAS"))%>
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
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BuscaAtivaInicio") %>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BuscaAtivaTermino") %>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaFormal") %>&nbsp;
                </td>
                <td class="align-center">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalParcerias")) %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BuscaAtivaOrgaoGestorExecuta")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BuscaAtivaCRASExecuta")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "BuscaAtivaUnidadePrivadaExecuta")%>&nbsp;
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorFMAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorOrcamentoMunicipal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorFundoMunicipal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorFEAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorOrcamentoEstadual"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorFundoEstadual"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorFNAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorOrcamentoFederal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorFundoFederal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorIGDPBF"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "BuscaAtivaValorIGDSUAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaNumeroFamilias2012"))%>&nbsp;
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaNumeroFamilias2013"))%>&nbsp;
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaNumeroFamilias2014"))%>&nbsp;
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaNumeroFamiliasTotal"))%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "AgendaFamiliaOrgaoGestorExecuta")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "AgendaFamiliaCRASExecuta")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiParceriaFormalAgendaFamilia") %>&nbsp;
                </td>
                <td class="align-center">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "TotalParceriasAgendaFamilia")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorFMAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorOrcamentoMunicipal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorFundoMunicipal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorFEAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorOrcamentoEstadual"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorFundoEstadual"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorFNAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorOrcamentoFederal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorFundoFederal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorIGDPBF"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "AgendaFamiliaValorIGDSUAS"))%>
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
