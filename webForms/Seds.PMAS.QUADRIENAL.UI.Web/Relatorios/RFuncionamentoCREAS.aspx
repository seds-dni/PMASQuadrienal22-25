<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RFuncionamentoCREAS.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RFuncionamentoCREAS" %>

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
                            style="background-color: #a6c9e2;" width="30" rowspan="3">Seq.
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40" rowspan="3">Código do PMAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="60" rowspan="3">ID-CREAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="250" rowspan="3">Nome do CREAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="150" rowspan="3">Município
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="150" rowspan="3">DRADS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="100" rowspan="3">Data de implantação
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="150" rowspan="3">Bairro onde se localiza
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="80" rowspan="3">Previsão anual<br />
                            de atendidos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" rowspan="3" width="100">Dias de funcionamento<br />
                            por semana                  
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" rowspan="3" width="100">Horas de funcionamento<br />
                            por semana
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="70" rowspan="3">N&#186; total
                            <br />
                            de funcionários
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="10" style="height: 22px;">Serviços desenvolvidos no CREAS
                        </th>
                    </tr>
                    <tr style="background-color: #a6c9e2;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" rowspan="2" width="200">PAEFI
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="3" style="height: 22px;" width="300">Serviço especializado em abordagem social
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="2" width="200">Serviço de proteção social a adolescente<br />
                            em cumprimento de medida socioeducativa 
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="3" width="300">Serviço de proteção especial para pessoas<br />
                            com deficiência, idosas e suas famílias
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="120" rowspan="2">Serviço
                            <br />
                            não tipificado
                        </th>
                    </tr>
                    <tr style="background-color: #a6c9e2; font-size: 10px;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">apenas crianças<br />
                            e adolescentes
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">apenas jovens, adultos,<br />
                            idosos e famílias
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">crianças, adolescentes, jovens,<br />
                            adultos, idosos e famílias
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">Liberdade Assistida
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">Prestação de serviços<br />
                            à comunidade
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">pessoas com deficiência,<br />
                            seus familiares e cuidadores
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">idosos, seus familiares<br />
                            e cuidadores
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100">pessoas com deficiência e idosos,<br />
                            seus familiares e cuidadores
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="height: 22px; background-color: #a6c9e2;">
                        <td align="right" colspan="8" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <b>Totais:</b>
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalNumeroAtendidos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg" colspan="2">&nbsp;
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalFuncionarios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicoPAEFI" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicoEspecializadoAbordagemSocialCriancas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicoEspecializadoAbordagemSocialJovens" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicoEspecializadoAbordagemSocialAdultos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicoLiberdadeAssistida" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicoPrestacaoServicoComunidade" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicoProtecaoPessoasDeficientes" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicoProtecaoPessoasIdosas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicoProtecaoFamilias" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicoNaoTipificado" runat="server" Font-Bold="true" />
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
                    <%#DataBinder.Eval(Container.DataItem, "Id") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "IDCREAS") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "DataImplantacao") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Bairro") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidos"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "DiasSemana") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "HorasSemana") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Funcionarios"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoPAEFI")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoEspecializadoAbordagemSocialCriancas")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoEspecializadoAbordagemSocialJovens")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoEspecializadoAbordagemSocialAdultos")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoLiberdadeAssistida")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoPrestacaoServicoComunidade")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoProtecaoPessoasDeficientes")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoProtecaoPessoasIdosas")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoProtecaoFamilias")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoNaoTipificado")%>&nbsp;
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="row" style="background-color: #F3F3F3 !important;">
                <td class="ui-state-default jqgrid-rownum row" style="height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "Id") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "IDCREAS") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "DataImplantacao") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Bairro") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidos"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "DiasSemana") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "HorasSemana") %>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "Funcionarios"))%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoPAEFI")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoEspecializadoAbordagemSocialCriancas")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoEspecializadoAbordagemSocialJovens")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoEspecializadoAbordagemSocialAdultos")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoLiberdadeAssistida")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoPrestacaoServicoComunidade")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoProtecaoPessoasDeficientes")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoProtecaoPessoasIdosas")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoProtecaoFamilias")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoNaoTipificado")%>&nbsp;
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
