<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RInformacoesFMAS.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RInformacoesFMAS" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center"
                            style="height: 22px;" width="30" rowspan="2">Seq.
                        </th>
                        <th align="center"
                            width="150" rowspan="2">Município
                        </th>
                        <th align="center"
                            width="130" rowspan="2">DRADS
                        </th>
                        <th align="center"
                            width="120" rowspan="2">CNPJ
                        </th>
                        <th align="center"
                            width="60" rowspan="2">Condição
                            <br />
                            do CNPJ
                        </th>
                        <th align="center"
                            width="80" rowspan="2">Lei de criação
                            <br />
                            do FMAS                            
                        </th>
                        <th align="center"
                            width="100" rowspan="2">Está legalmente
                            <br />
                            regulamentado?                            
                        </th>
                        <th align="center"
                            width="80" rowspan="2">É unidade
                            <br />
                            orçamentária?                          
                        </th>
                        <th align="center"
                            width="200" rowspan="2">Nome do gestor atual
                        </th>
                        <th align="center"
                            width="200" rowspan="2">O gestor do FMAS é
                        </th>
                        <th align="center"
                            width="424" colspan="4">Previsão de alocação de recursos
                        </th>
                                <th align="center"
                            width="424" colspan="4">Valores aprovados na Lei Orçamentária
                        </th>

                    </tr>
                    <tr class="info" style="background-color: #7cc8ff;">
                        <th align="center" width="106">Recursos
                            <br />
                            municipais
                        </th>
                        <th align="center" width="106">Recursos oriundos<br />
                            do FEAS
                        </th>
                        <th align="center" width="106">Recursos oriundos<br />
                            do FNAS
                        </th>
                        <th align="center" width="106">Total
                        </th>
                   
                        <th align="center" width="106">Alocados no FMAS
                        </th>
                        <th align="center" width="106">Não passam pelo FMAS
                        </th>

                        <th align="center" width="106">Total
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <td align="right" colspan="4">
                            <b>Totais:</b>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalCondicaoCNPJ" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">&nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalRegulamentado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalOrcamentaria" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right" colspan="5">&nbsp;
                        </td>
                        <%--   <td align="right">&nbsp;
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
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "CNPJ") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "CondicaoCNPJ") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "Lei") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "Regulamentado") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "Orcamentaria") %>&nbsp;
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "NomeGestor") %>&nbsp;
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "TipoGestor") %>&nbsp;
                </td>

                <td align="center">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS")) %>
                </td>
                <td align="center">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS")) %>
                </td>
                <td align="center">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS")) %>
                </td>

                <td align="center">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total")) %>
                </td>
                <td align="center">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorRecursosFMAS")) %>
                </td>
                <td align="center">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorNaoAlocadoFMAS")) %>
                </td>
                <td align="center">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "TotalRecursosFMAS")) %>
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
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "CNPJ") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "CondicaoCNPJ") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "Lei") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "Regulamentado") %>&nbsp;
                </td>
                <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "Orcamentaria") %>&nbsp;
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "NomeGestor") %>&nbsp;
                </td>
                <td align="left">
                    <%#DataBinder.Eval(Container.DataItem, "TipoGestor") %>&nbsp;
                </td>
                <td align="center">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFMAS")) %>
                </td>
                <td align="center">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFEAS")) %>
                </td>
                <td align="center">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorFNAS")) %>
                </td>
                <td align="center">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Total")) %>
                </td>
                <td align="center">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorRecursosFMAS")) %>
                </td>
                <td align="center">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorNaoAlocadoFMAS")) %>
                </td>
                <td align="center">
                    <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "TotalRecursosFMAS")) %>
                </td>
                <%--  <td align="center">
                    <%#DataBinder.Eval(Container.DataItem, "Decreto") %>&nbsp;
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
