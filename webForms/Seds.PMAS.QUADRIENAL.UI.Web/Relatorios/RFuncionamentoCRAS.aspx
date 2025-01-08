<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RFuncionamentoCRAS.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RFuncionamentoCRAS" %>

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
                            style="background-color: #a6c9e2;" width="30" rowspan="3">
                            Seq.
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="40" rowspan="3">
                            Código do PMAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="60" rowspan="3">
                            ID-CRAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="380" rowspan="3">
                            Nome do CRAS
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="150" rowspan="3">
                            Município
                        </th>                        
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="150" rowspan="3">
                            DRADS
                        </th>
                         <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="100" rowspan="3">
                            Data de Implantação
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="150" rowspan="3">
                            Bairro onde se localiza
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="80" rowspan="3">
                            N&#186; de famílias<br /> referenciadas
                        </th>   
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="80" rowspan="3">
                            Previsão anual de<br /> famílias atendidas
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="70" rowspan="3">
                            Dias de funcionamento<br /> por semana                  
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="70" rowspan="3">
                            Horas de funcionamento<br /> por semana
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="70" rowspan="3">
                            N&#186; total de<br /> funcionários
                        </th>                        
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="70" rowspan="3">
                            Possui equipe<br /> volante
                        </th>                        
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="9" style="height:22px;">
                            Serviços desenvolvidos no CRAS
                        </th>                        
                    </tr>    
                    <tr style="background-color: #a6c9e2;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="120" rowspan="2">
                            PAIF
                        </th>                        
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="4" style="height:22px;">
                            SCFV
                        </th>                   
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" colspan="3">
                            Serviço de PSB no domicílio
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report"
                            width="120" rowspan="2">
                            Serviço não<br /> tipificado
                        </th> 
                    </tr> 
                    <tr style="background-color: #a6c9e2;">
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="70">
                            0 a 6 anos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="70">
                            6 a 15 anos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="70">
                            15 a 17 anos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="70">
                            Idosos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report">
                            PCD
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report">
                            Idosos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report">
                            PCD e idosos
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
                            <asp:Label ID="lblTotalFamiliasReferenciadas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalFamiliasAtendidas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg" colspan="2">
                              &nbsp;
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalFuncionarios" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalEquipeVolante" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalPAIF" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicoConvivencia6anos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicoConvivencia15anos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicoConvivencia17anos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicoConvivencia60anos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicoProtecaoPessoasDeficientes" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicoProtecaoPessoasIdosas" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right bg">
                            <asp:Label ID="lblTotalServicoProtecaoPessoasDeficientesIdosas" runat="server" Font-Bold="true" />
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
                    <%#DataBinder.Eval(Container.DataItem, "IDCRAS") %>
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
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "FamiliasReferenciadas"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "FamiliasAtendidas"))%>
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
                    <%#DataBinder.Eval(Container.DataItem, "PossuiEquipeVolante")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiPAIF")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoConvivencia6anos")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoConvivencia15anos")%>&nbsp;
                </td>                
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoConvivencia17anos")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoConvivencia60anos")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoProtecaoPessoasDeficientes")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoProtecaoPessoasIdosas")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoProtecaoPessoasDeficientesIdosas")%>&nbsp;
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
                    <%#DataBinder.Eval(Container.DataItem, "IDCRAS") %>
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
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "FamiliasReferenciadas"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "FamiliasAtendidas"))%>
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
                    <%#DataBinder.Eval(Container.DataItem, "PossuiEquipeVolante")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiPAIF")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoConvivencia6anos")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoConvivencia15anos")%>&nbsp;
                </td>                
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoConvivencia17anos")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoConvivencia60anos")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoProtecaoPessoasDeficientes")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoProtecaoPessoasIdosas")%>&nbsp;
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "PossuiServicoProtecaoPessoasDeficientesIdosas")%>&nbsp;
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
