<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RAcoesAvaliacao.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RAcoesAvaliacao" %>

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
            <table cellspacing="0" id="tbReport" runat="server" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info">
                        <th align="center" 
                            width="30" rowspan="2">
                            Seq.
                        </th>                        
                        <th align="center" 
                            width="180" rowspan="2">
                            Município
                        </th>
                        <th align="center" 
                            width="180" rowspan="2">
                            DRADS
                        </th>
                        <th align="center" 
                            width="100" rowspan="2">
                            Realiza ações<br /> de avaliação?
                        </th>                        
                        <th align="center"  colspan="2" style="height:22px;">
                            Quem executa essa avaliação?
                        </th>   
                                             
                        <th align="center"  colspan="4">
                            Procedimentos utilizados<br /> para avaliação
                        </th>                                                
                        <th align="center"  width="80" rowspan="2">
                            Realização<br /> de pesquisas
                        </th>                        
                    </tr>
                     <tr class="info">                        
                        <th align="center" 
                            width="100">
                            Orgão Gestor
                        </th>
                        <th align="center" 
                            width="150">
                            Serviço <br />terceirizado
                        </th>
                        <th align="center" 
                            width="120">
                            Levantamento de opiniões<br /> junto aos usuários
                        </th>                        
                        <th align="center" 
                            width="120">
                            Levantamento de<br /> dados quantitativos
                        </th>                        
                        <th align="center" 
                            width="120">
                            Análise de registros<br /> e documentos
                        </th>                        
                        <th align="center" 
                            width="120">
                            Utilização de<br /> indicadores sociais
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="height: 22px;" class="info">
                        <td align="right" colspan="3">
                            <b>Totais:</b>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalAvaliaAcoes" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalAvaliadoOrgaoGestor" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalAvaliadoTerceirizado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalLevantamentoOpiniao" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalLevantamentoDados" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalAnaliseRegistros" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalUtilizacaoIndicadores" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalRealizaPesquisa" runat="server" Font-Bold="true" />
                        </td>                        
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td style="height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>               
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "AvaliaAcoes")%>
                </td>                
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "AvaliadoOrgaoGestor")%>
                </td>                
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "AvaliadoTerceirizado")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "LevantamentoOpiniao")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "LevantamentoDados")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "AnaliseRegistros")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "UtilizacaoIndicadores")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "RealizaPesquisa")%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td style="height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "AvaliaAcoes")%>
                </td>                
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "AvaliadoOrgaoGestor")%>
                </td>                
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "AvaliadoTerceirizado")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "LevantamentoOpiniao")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "LevantamentoDados")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "AnaliseRegistros")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "UtilizacaoIndicadores")%>
                </td>
                <td class="align-center">
                    <%#DataBinder.Eval(Container.DataItem, "RealizaPesquisa")%>
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
