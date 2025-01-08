<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RIntegracaoServicos.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RIntegracaoServicos" %>

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
                            width="250" rowspan="2">DRADS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="70" rowspan="2">Tipo de unidade
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="120" rowspan="2">Nome da unidade
                        </th>

                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="120" rowspan="2">Local de execução<br />
                            dos serviços
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120" rowspan="2">Proteção social
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="250" rowspan="2">Tipo de serviço
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100" rowspan="2">Previsão mensal de<br />
                            n&#186; de atendidos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100" rowspan="2">Previsão anual de<br />
                            n&#186; de atendidos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="200" rowspan="2">Usuários
                        </th>

                        <th align="center" colspan="18" class="ui-state-default ui-th-column ui-th-ltr freezing_report" style="height: 22px;">Integração do serviço com programas e benefícios<br />
                            (n&#186; de usuários vinculados a programas de transferência de renda e/ou benefícios continuados e/ou benefícios eventuais)
                        </th>
                    </tr>
                    <tr style="background-color: #a6c9e2;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="60">Unidade
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="100">local de execução<br />
                            ou ID-SUAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">Ação Jovem
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">Renda Cidadã
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">ACESSUAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">São Paulo
                            <br />
                            Solidário
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">Vivaleite
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">Bom Prato
                        </th>
                          <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">Família <br />Paulista
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">Programa/ <br />projeto municipal
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">Benefício Idoso
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">Bolsa Família
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">PETI
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">PTR Municipal
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">BPC Idosos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">BPC PCD
                        </th>

                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">Auxílio Natalidade
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">Auxílio Funeral
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">Calamidades
                            <br />
                            e emergências
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" style="font-size: 10px;">Vulnerabilidade<br />
                            temporária
                        </th>

                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="height: 22px; background-color: #a6c9e2;">
                        <td align="right" colspan="10" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <b>Totais:</b>
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalNumeroAtendidosMensal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalNumeroAtendidosAnual" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">&nbsp;
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAcaoJovem" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalRendaCidada" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalACESSUAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalSPSolidario" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalVivaleite" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalBomprato" runat="server" Font-Bold="true" />
                        </td>
                         <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFamiliaPaulista" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalProgramaMunicipal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalRendaCidadaBeneficioIdoso" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalBolsaFamilia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalPETI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalPTRMunicipal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalBPCIdosos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalBPCPCD" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAuxilioNatalidade" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalAuxilioFuneral" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalCalamidadeEmergencia" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalVulnerabilidadeTemporaria" runat="server" Font-Bold="true" />
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
                    <%#DataBinder.Eval(Container.DataItem, "Servico.CodigoUnidade") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.IdLocal")%>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.Municipio")%>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.Drads")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.TipoUnidade")%>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.UnidadeResponsavel")%>
                </td>

                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.LocalExecucao")%>
                </td>

                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.ProtecaoSocial")%>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.TipoServico")%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "Servico.NumeroAtendidosMensal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "Servico.NumeroAtendidosAnual"))%>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.Usuarios")%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "AcaoJovem"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "RendaCidada"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "Acessuas"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "SaoPauloSolidario"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "Vivaleite"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "BomPrato"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "FamiliaPaulista"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "ProgramaMunicipal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "RendaCidadaBeneficioIdoso"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "BolsaFamilia"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "PETI"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "PTRMunicipal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "BPCIdosos"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "BPCPCD"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "AuxilioNatalidade"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "AuxilioFuneral"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "CalamidadeEmergencia"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "VulnerabilidadeTemporaria"))%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="row" style="background-color: #F3F3F3 !important;">
                <td class="ui-state-default jqgrid-rownum row" style="height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.CodigoUnidade") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.IdLocal")%>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.Municipio")%>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.Drads")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.TipoUnidade")%>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.UnidadeResponsavel")%>
                </td>

                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.LocalExecucao")%>
                </td>

                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.ProtecaoSocial")%>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.TipoServico")%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "Servico.NumeroAtendidosMensal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "Servico.NumeroAtendidosAnual"))%>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Servico.Usuarios")%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "AcaoJovem"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "RendaCidada"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "Acessuas"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "SaoPauloSolidario"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "Vivaleite"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "BomPrato"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "FamiliaPaulista"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "ProgramaMunicipal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "RendaCidadaBeneficioIdoso"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "BolsaFamilia"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "PETI"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "PTRMunicipal"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "BPCIdosos"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "BPCPCD"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "AuxilioNatalidade"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "AuxilioFuneral"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "CalamidadeEmergencia"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "VulnerabilidadeTemporaria"))%>
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
