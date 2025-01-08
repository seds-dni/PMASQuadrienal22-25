<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RAcoesPlanejadas.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RAcoesPlanejadas" %>

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
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            width="30" rowspan="2">
                            Seq.
                        </th>                        
                        <th align="center"
                            width="120" rowspan="2">
                            Município
                        </th>
                        <th align="center"
                            width="120" rowspan="2">
                            Porte
                        </th>
                        <th align="center"
                            width="200" rowspan="2">
                            DRADS
                        </th>
                        <th align="center"
                            width="250" rowspan="2">
                            Eixo da ação
                        </th>                        
                        <th align="center"
                            width="300" rowspan="2">
                            Identificação da ação
                        </th>   
                        <th align="center"
                            width="300" rowspan="2">
                            Denominação da ação
                        </th>
                        <th align="center"
                            width="180" colspan="2">
                            Previsão
                        </th>
                        <th align="center"
                            width="100" rowspan="2">
                            Estimativa total<br /> de custo
                        </th>
                        <th align="center" colspan="11" style="height:22px;">
                            Previsão de fontes e/ou valores dos recursos financeiros para execução desta ação 
                        </th>                                               
                    </tr>
                     <tr style="background-color: #7cc8ff;">
                        <th align="center" width="60">
                            Início
                        </th>
                        <th align="center" width="60">
                            Término
                        </th>
                        <th align="center" width="80" style="font-size:10px;">
                            FMAS
                        </th>
                        <th align="center" width="80" style="font-size:10px;">
                            Orçamento <br /> municipal
                        </th>
                         <th align="center" width="80" style="font-size:10px;">
                            Outros fundos<br /> municipais
                        </th>
                        <th align="center" width="80" style="font-size:10px;">
                            FEAS
                        </th>
                         <th align="center" width="80" style="font-size:10px;">
                            Orçamento <br />estadual
                        </th>
                         <th align="center" width="80" style="font-size:10px;">
                            Outros fundos <br />estaduais
                        </th>                        
                        <th align="center" width="80" style="font-size:10px;">
                            FNAS
                        </th>
                        <th align="center" width="80" style="font-size:10px;">
                            Orçamento <br />federal
                        </th>
                         <th align="center" width="80" style="font-size:10px;">
                            Outros fundos<br /> nacionais
                        </th> 
                        <th align="center" width="80" style="font-size:10px;">
                            IGD-PBF
                        </th>
                        <th align="center" width="80" style="font-size:10px;">
                            IGD-SUAS
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <td align="right" colspan="9" >
                            <b>Totais:</b>
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalEstimativaCusto" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFMAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalOrcamentoMunicipal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalOutrosFundosMunicipais" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFEAS" runat="server" Font-Bold="true" />
                        </td>
                       <td align="right" >
                            <asp:Label ID="lblTotalOrcamentoEstadual" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalOutrosFundosEstaduais" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalFNAS" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalOrcamentoFederal" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalOutrosFundosFederais" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalIGDPBF" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblTotalIGDSUAS" runat="server" Font-Bold="true" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="info" style="background-color: #7cc8ff; height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>                
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                  <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Eixo") %>
                </td>                
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Acao") %>
                </td>                
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "PrevisaoInicio") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "PrevisaoTermino") %>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorEstimativaCusto"))%>
                </td>
                <td align="right">
                    <%#getValor(((Boolean)DataBinder.Eval(Container.DataItem, "FonteFMAS")), ((Decimal?)DataBinder.Eval(Container.DataItem, "ValorFMAS")))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteOrcamentoMunicipal"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorOrcamentoMunicipal"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteOutrosFundosMunicipais"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorOutrosFundosMunicipais"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteFEAS"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteOrcamentoEstadual"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorOrcamentoEstadual"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteOutrosFundosEstaduais"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorOutrosFundosEstaduais"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteFNAS"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteOrcamentoFederal"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorOrcamentoFederal"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteOutrosFundosFederais"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorOutrosFundosFederais"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteIGDPBF"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorIGDPBF"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteIGDSUAS"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorIGDSUAS"))%>
                </td>                
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td class="info" style="background-color: #7cc8ff; height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                  <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Porte") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Eixo") %>
                </td>                
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Acao") %>
                </td>                
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "PrevisaoInicio") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "PrevisaoTermino") %>
                </td>
                <td align="right">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorEstimativaCusto"))%>
                </td>
                <td align="right">
                    <%#getValor(((Boolean)DataBinder.Eval(Container.DataItem, "FonteFMAS")), ((Decimal?)DataBinder.Eval(Container.DataItem, "ValorFMAS")))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteOrcamentoMunicipal"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorOrcamentoMunicipal"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteOutrosFundosMunicipais"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorOutrosFundosMunicipais"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteFEAS"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorFEAS"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteOrcamentoEstadual"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorOrcamentoEstadual"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteOutrosFundosEstaduais"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorOutrosFundosEstaduais"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteFNAS"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorFMAS"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteOrcamentoFederal"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorOrcamentoFederal"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteOutrosFundosFederais"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorOutrosFundosFederais"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteIGDPBF"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorIGDPBF"))%>
                </td>
                <td align="right">
                    <%#getValor((Boolean)DataBinder.Eval(Container.DataItem, "FonteIGDSUAS"), (Decimal?)DataBinder.Eval(Container.DataItem, "ValorIGDSUAS"))%>
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
