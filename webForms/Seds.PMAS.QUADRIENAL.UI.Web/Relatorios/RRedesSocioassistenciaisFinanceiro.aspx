<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RRedesSocioassistenciaisFinanceiro.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RRedesSocioassistenciaisFinanceiro" %>

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
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120" rowspan="2">
                            Proteção Social
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="150" rowspan="2">
                            Tipo serviço
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100" rowspan="2">
                            Abrangência
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100" rowspan="2">
                            Previsão mensal de n&#186; de atendidos
                        </th>      
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100" rowspan="2">
                            Previsão anual de n&#186; de atendidos
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="120" rowspan="2">
                            Usuários
                        </th>                        
                        <th align="center" colspan="6" class="ui-state-default ui-th-column ui-th-ltr freezing_report">
                            Origem dos recursos de cofinanciamento via fundos (valores anuais)
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" rowspan="2">
                            Recursos privados
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80" rowspan="2">
                            Valor do convênio estadualizado
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="100" rowspan="2">
                            Total de recursos
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
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">
                            FMAS (R$)
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">
                            FMDCA (R$)
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">
                            FEAS (R$)
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">
                            FEDCA (R$)
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">
                            FNAS (R$)
                        </th>
                        <th align="center" class="ui-state-default ui-th-column ui-th-ltr freezing_report" width="80">
                            FNDCA (R$)
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
                            <asp:Label ID="lblNumeroAtendidosMensal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblNumeroAtendidosAnual" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            &nbsp;
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFMAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFMDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFEAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFEDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFNAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalFNDCA" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalPrivado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotalEstadualizado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" class="ui-state-default ui-th-column ui-th-ltr align-right">
                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
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
                    <%#DataBinder.Eval(Container.DataItem, "Abrangencia") %>
                </td>
                <td class="align-left">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosMensal")) %>
                </td>
                <td class="align-left">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosAnual")) %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Usuarios") %>
                </td>                             
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMDCA"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEDCA"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNDCA"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorPrivado"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorEstadualizado"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total"))%>
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
                    <%#DataBinder.Eval(Container.DataItem, "Abrangencia") %>
                </td>
                <td class="align-left">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosMensal")) %>
                </td>
                <td class="align-left">
                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidosAnual")) %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Usuarios") %>
                </td>               
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMDCA"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEDCA"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNDCA"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorPrivado"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorEstadualizado"))%>
                </td>
                <td class="align-right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total"))%>
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
