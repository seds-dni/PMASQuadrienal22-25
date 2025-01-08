<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true" CodeBehind="RProgramaFamiliaPaulista.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RProgramaFamiliaPaulista" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table {
            border-collapse: collapse;
        }

            table.alternative-table {
                border: none!important;
                width: 730px;
            }

        td .alternative-cell {
            padding: 0!important;
            /*border-left-style: none!Important;*/
        }

        .row td.alternative-table-align-center {
            padding: 0!important;
            text-align: center!important;
        }

        td.alternative-table-align-left {
            /*border-right-style: none!important;*/
            text-align: left!important;
        }
         td.alternative-table-align-right {
            /*border-right-style: none!important;*/
            text-align: right!important;
        }

        tr.alternative-table-row {
            padding: 0!important;
        }
        /*.row td.align-center {
            border-left-style: none !important;
            border-right-width: 1px;
            border-right-color: #a6c9e2;
            border-right-style: solid;
            text-align: center!important;
        }*/
        /*td.alternative-table-align-center {
            border-right-color: #a6c9e2;
            border-right-style: solid;
            text-align: center!important;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellpadding="0" border="0" class="ui-widget ui-widget-content ui-corner-all">
                <thead>
                    <tr style="background-color: #a6c9e2; height: 22px;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            style="background-color: #a6c9e2; height: 22px;" width="30" rowspan="3">Seq.
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="200" rowspan="3">Município
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="200" rowspan="3">DRADS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="100" rowspan="3">Data de
                            <br />
                            aprovação do<br />
                            Plano de Ação<br />
                            pelo CMAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="100" rowspan="3">Data de adesão<br />
                            ao programa
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="100" rowspan="3">Meta SEDS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="100" rowspan="3">Meta município
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            colspan="3">Coordenador municipal do programa
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            colspan="4">Irformações sobre o território
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            colspan="11">Previsão de fontes e/ou valores dos recursos financeiros para execução do programa
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="100" rowspan="3">Total
                        </th>
                    </tr>
                    <tr style="background-color: #a6c9e2;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="250" rowspan="2">Nome
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="80" rowspan="2">Telefone
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="200" rowspan="2">Email
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="62" rowspan="2">Identificação
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="70" rowspan="2">N&#186; de famílias<br />
                            beneficiárias
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="350" rowspan="2">Bairros
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="200" rowspan="2">Responsável
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            colspan="3">Municipal
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            colspan="3">Estadual
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            colspan="5">Federal
                        </th>
                    </tr>
                    <tr style="background-color: #a6c9e2;">
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
                    </tr>
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
                        <td align="right" colspan="5" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <b>Totais:</b>
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalMetaSEDS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalMetaMunicipio" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right" colspan="4"></td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
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
                    <%#DataBinder.Eval(Container.DataItem, "DataAprovacao", "{0:dd/MM/yyyy}") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "DataAdesaoPrograma", "{0:dd/MM/yyyy}") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "MetaSeds")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "MetaMunicipio")) %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Telefone") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Email") %>
                </td>
                <td colspan="4" class="alternative-cell">
                    <asp:ListView ID="lstIdentificacoesTerritorios" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "IdentificacoesTerritorios")%>'>
                        <ItemTemplate>
                            <table cellpadding="0" border="0" class="alternative-table">
                                <tr height="25" class="alternative-table-row">
                                    <td width="72" class="alternative-table-align-center"><%#DataBinder.Eval(Container.DataItem, "NumeroIdentificacao") %></td>
                                    <td width="66" class="alternative-table-align-right"><%#DataBinder.Eval(Container.DataItem, "NumeroBeneficiarios") %></td>
                                    <td class="alternative-table-align-left" width="344"><%#DataBinder.Eval(Container.DataItem, "IdentificacaoTerritorio") %></td>
                                    <td class="alternative-table-align-left" width="195"><%#DataBinder.Eval(Container.DataItem, "NomeResponsavel") %></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorFMAS")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorOrcamentoMunicipal")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorFundoMunicipal") )%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorFEAS") )%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorOrcamentoEstadual") )%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorFundoEstadual")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorFNAS") )%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorOrcamentoFederal") )%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorFundoFederal")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorIGDPBF")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorIGDSUAS")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorTotal")) %>
                </td>
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
                    <%#DataBinder.Eval(Container.DataItem, "DataAprovacao", "{0:dd/MM/yyyy}") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "DataAdesaoPrograma", "{0:dd/MM/yyyy}") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MetaSeds") )%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "MetaMunicipio")) %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Telefone") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Email") %>
                </td>
                <td colspan="4" class="alternative-cell">
                    <asp:ListView ID="lstIdentificacoesTerritorios" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "IdentificacoesTerritorios")%>'>
                        <ItemTemplate>
                            <table cellpadding="0" border="0" id="tbinformacoesterritorio" class="alternative-table">
                                <tr height="25" class="alternative-table-row">
                                    <td width="72" class="alternative-table-align-center"><%#DataBinder.Eval(Container.DataItem, "NumeroIdentificacao") %></td>
                                    <td width="66" class="alternative-table-align-right"><%#DataBinder.Eval(Container.DataItem, "NumeroBeneficiarios") %></td>
                                    <td class="alternative-table-align-left" width="344"><%#DataBinder.Eval(Container.DataItem, "IdentificacaoTerritorio") %></td>
                                    <td class="alternative-table-align-left" width="195"><%#DataBinder.Eval(Container.DataItem, "NomeResponsavel") %></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorFMAS")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorOrcamentoMunicipal")) %>
                </td>

                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorFundoMunicipal")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorFEAS")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorOrcamentoEstadual")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorFundoEstadual")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorFNAS")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorOrcamentoFederal")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorFundoFederal")) %>
                </td>
                <td class="align-left">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorIGDPBF")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorIGDSUAS")) %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorTotal") )%>
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

