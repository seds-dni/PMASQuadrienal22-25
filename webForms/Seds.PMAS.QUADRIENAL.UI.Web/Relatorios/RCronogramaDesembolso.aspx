<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true"
    CodeBehind="RCronogramaDesembolso.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RCronogramaDesembolso" %>

<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table {
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:ListView ID="lst" runat="server" OnItemDataBound="lst_ItemDataBound" Visible="false">
        <LayoutTemplate>
            <table id="tbReport" runat="server" cellspacing="0" cellpadding="0" border="0" class="table border bordered">
                <thead>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <th align="center"
                            width="30" rowspan="4">Seq.
                        </th>
                        <th align="center"
                            width="150" rowspan="4">Município
                        </th>
                        <th align="center"
                            width="150" rowspan="4">DRADS
                        </th>
                        <th align="center"
                            rowspan="4" width="200">Cronograma Referente a
                        </th>
                        <th align="center"
                            colspan="8" width="200">Rede direta
                        </th>
                        <th align="center"
                            colspan="8" width="200">Rede indireta
                        </th>
                        <th align="center"
                            width="150" rowspan="4">Total
                        </th>
                    </tr>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <th align="center"
                            width="100" colspan="4">Custeio</th>
                        <th align="center"
                            width="100" colspan="4">Investimento</th>
                        <th align="center"
                            width="100" colspan="4">Custeio</th>
                        <th align="center"
                            width="100" colspan="4">Investimento</th>
                    </tr>

                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <th align="center"
                            width="50" colspan="2">
                            Recursos Humanos
                        </th>
                        <th align="center"
                            width="50" colspan="2">
                            Outras Despesas de Custeio
                        </th>
                        <th align="center"
                            width="50" colspan="2">
                            Aquisição de Equipamentos
                        </th>
                        <th align="center"
                            width="50" colspan="2">
                            Obras
                        </th>

                        <th align="center"
                            width="50" colspan="2">
                            Recursos Humanos
                        </th>
                        <th align="center"
                            width="50" colspan="2">
                            Outras Despesas de Custeio
                        </th>
                        <th align="center"
                            width="50" colspan="2">
                            Aquisição de Equipamentos
                        </th>
                        <th align="center"
                            width="50" colspan="2">
                            Obras
                        </th>

                    </tr>
                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                        <th align="center"
                            width="25">Exercício<br />
                            atual
                        </th>
                        <th align="center"
                            width="25">Reprogramado<br />
                            ano anterior
                        </th>
                        <th align="center"
                            width="25">Exercício<br />
                            atual
                        </th>
                        <th align="center"
                            width="25">Reprogramado<br />
                            ano anterior
                        </th>
                        <th align="center"
                            width="25">Exercício<br />
                            atual
                        </th>
                        <th align="center"
                            width="25">Reprogramado<br />
                            ano anterior
                        </th>
                        <th align="center"
                            width="25">Exercício<br />
                            atual
                        </th>
                        <th align="center"
                            width="25">Reprogramado<br />
                            ano anterior
                        </th>
                        <th align="center"
                            width="25">Exercício<br />
                            atual
                        </th>
                        <th align="center"
                            width="25">Reprogramado<br />
                            ano anterior
                        </th>
                        <th align="center"
                            width="25">Exercício<br />
                            atual
                        </th>
                        <th align="center"
                            width="25">Reprogramado<br />
                            ano anterior
                        </th>
                        <th align="center"
                            width="25">Exercício<br />
                            atual
                        </th>
                        <th align="center"
                            width="25">Reprogramado<br />
                            ano anterior
                        </th>
                        <th align="center"
                            width="25">Exercício<br />
                            atual
                        </th>
                        <th align="center"
                            width="25">Reprogramado<br />
                            ano anterior
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server">
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="info" style="height: 22px; background-color: #a6c9e2;">
                        <td align="right" colspan="4">
                            <b>Totais:</b>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalRecursosHumanosPublica" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalRecursosHumanosPublicaReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalCusteioOutrasDespesasPublica" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalCusteioOutrasDespesasPublicaReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalInvestimentoAquisicaoPublica" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalInvestimentoAquisicaoPublicaReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalInvestimentosObrasPublica" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalInvestimentosObrasPublicaReprogramado" runat="server" Font-Bold="true" />
                        </td>


                        <td align="right">
                            <asp:Label ID="lblTotalRecursosHumanos" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalRecursosHumanosReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalCusteioOutrasDespesasPrivada" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalCusteioOutrasDespesasPrivadaReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalInvestimentoAquisicaoPrivada" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalInvestimentoAquisicaoPrivadaReprogramado" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalInvestimentosObrasPrivada" runat="server" Font-Bold="true" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTotalInvestimentosObrasPrivadaReprogramado" runat="server" Font-Bold="true" />
                        </td>


                        <td align="right">
                            <asp:Label ID="lblTotalGeral" runat="server" Font-Bold="true" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td class="info" style="height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "TipoProtecao") %>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "RecursosHumanosPublica")).ToString("N2")%>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "RecursosHumanosPublicaReprogramado")).ToString("N2")%>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "CusteioPublica")).ToString("N2")%>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "CusteioPublicaReprogramado")).ToString("N2")%>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "InvestimentoAquisicaoDeEquipamentosPublico")).ToString("N2") %>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "InvestimentoEquipamentosPublicoReprogramado")).ToString("N2") %>
                </td>


                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "InvestimentoObrasPublico")).ToString("N2") %>
                </td>                
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "InvestimentosObrasReprogramadoPublico")).ToString("N2") %>
                </td>


                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "RecursosHumanos")).ToString("N2") %>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "RecursosHumanosReprogramado")).ToString("N2") %>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "CusteioPrivada")).ToString("N2") %>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "CusteioPrivadaReprogramado")).ToString("N2") %>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "InvestimentoAquisicaoDeEquipamentosPrivado")).ToString("N2") %>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "InvestimentoEquipamentosPrivadoReprogramado")).ToString("N2") %>
                </td>
                                

                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "InvestimentoObrasPrivado")).ToString("N2") %>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "InvestimentosObrasReprogramadoPrivado")).ToString("N2") %>
                </td>

                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "Total")).ToString("N2") %>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color: #F3F3F3 !important;">
                <td class="info" style="height: 22px;">
                    <asp:Label ID="lblSequencia" runat="server" />
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Municipio") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "Drads") %>
                </td>
                <td class="align-left">
                    <%#DataBinder.Eval(Container.DataItem, "TipoProtecao") %>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "RecursosHumanosPublica")).ToString("N2")%>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "RecursosHumanosPublicaReprogramado")).ToString("N2")%>
                </td>

                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "CusteioPublica")).ToString("N2") %>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "CusteioPublicaReprogramado")).ToString("N2") %>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "InvestimentoAquisicaoDeEquipamentosPublico")).ToString("N2")%>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "InvestimentoEquipamentosPublicoReprogramado")).ToString("N2")%>
                </td>
                

                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "InvestimentoObrasPublico")).ToString("N2") %>
                </td>                
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "InvestimentosObrasReprogramadoPublico")).ToString("N2") %>
                </td>


                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "RecursosHumanos")).ToString("N2") %>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "RecursosHumanosReprogramado")).ToString("N2") %>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "CusteioPrivada")).ToString("N2") %>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "CusteioPrivadaReprogramado")).ToString("N2") %>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "InvestimentoAquisicaoDeEquipamentosPrivado")).ToString("N2") %>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "InvestimentoEquipamentosPrivadoReprogramado")).ToString("N2") %>
                </td>

                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "InvestimentoObrasPrivado")).ToString("N2") %>
                </td>
                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "InvestimentosObrasReprogramadoPrivado")).ToString("N2") %>
                </td>


                <td class="align-right">
                    <%#((Decimal)DataBinder.Eval(Container.DataItem, "Total")).ToString("N2") %>
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
