<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CTransferenciaRenda.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.CTransferenciaRenda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <table class="ui-widget ui-widget-content ui-corner-all" cellspacing="2" cellpadding="0"
                width="880" align="center" border="0">
                <tr>
                    <td class="ui-state-default ui-widget-header ui-corner-top">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="height: 30px; padding-left: 10px;">
                                    <img src="../Styles/Icones/redeprotecao.gif" align="absMiddle" />
                                    <b style="font-size: 18px;">49</b> <b>- Programas Federais de Transferência de Renda
                                    </b>
                                </td>
                                <td align="right">
                                    <a href="#" runat="server" id="linkAlteracoesQuadro43" visible="false">
                                        <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                    </a>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListView ID="lstProgramasFederais" runat="server" DataKeyNames="IdTipoTransferenciaRenda"
                            OnItemCommand="lstProgramas_ItemCommand" OnItemDataBound="lstProgramas_ItemDataBound">
                            <LayoutTemplate>
                                <table class="ui-jqgrid ui-widget ui-widget-content ui-corner-all" cellspacing="0"
                                    cellpadding="0" border="0">
                                    <thead>
                                        <tr>
                                            <th colspan="9" class="ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix"
                                                style="height: 20px;">
                                                <span class="ui-jqgrid-title">Programas Federais</span>
                                            </th>
                                        </tr>
                                        <tr class="ui-jqgrid-labels">
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="60" rowspan="2">
                                                Visualizar
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="330" rowspan="2">
                                                Nome
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="100" rowspan="2">
                                                Aderiu ao<br />
                                                Programa
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="240" colspan="3" style="height: 22px;
                                                padding-top: 3px;" valign="top">
                                                Serviços associados
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="150" rowspan="2">
                                                Previsão de<br />
                                                Repasse<br />
                                                Anual
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="50" rowspan="2">
                                                Excluir
                                            </th>
                                        </tr>
                                        <tr class="ui-jqgrid-labels">
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="90">
                                                Visualizar/<br />
                                                Editar
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="80">
                                                Total de<br />
                                                Unidades
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="80">
                                                Total de<br />
                                                Serviços
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="jqgfirstrow" style="height: auto;">
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="ui-widget-content row">
                                    <td align="center" style="height: 22px;">
                                        <asp:ImageButton runat="server" ID="btnVis" ToolTip="Visualizar Transferência de Renda"
                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Visualizar" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' />
                                    </td>
                                    <td class="align-left">
                                        <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                                    </td>
                                    <td class="align-center">
                                        <%#((Int32)DataBinder.Eval(Container.DataItem, "Aderiu")) > 0 ? "Sim" : "Não" %>
                                    </td>
                                    <td id="tdNao" align="center" runat="server" visible="false" colspan="3">
                                        -------------
                                    </td>
                                    <td align="center" id="tdVisualizarServicos" runat="server">
                                        <asp:ImageButton runat="server" ID="btnVisServicos" ToolTip="Visualizar Serviços Associados"
                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' />
                                    </td>
                                    <td class="align-center" id="tdUnidades" runat="server">
                                        <%#DataBinder.Eval(Container.DataItem, "TotalUnidades") %>
                                    </td>
                                    <td class="align-center" id="tdServicos" runat="server">
                                        <%#DataBinder.Eval(Container.DataItem, "TotalServicos") %>
                                    </td>
                                    <td class="align-center">
                                        <%#((Decimal)DataBinder.Eval(Container.DataItem, "PrevisaoAnualRepasse")).ToString("N2") %>
                                    </td>
                                    <td class="align-center">
                                        <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                            CommandName="Excluir" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>'
                                            CausesValidation="false" OnClientClick="return confirm('Deseja realmente remover a Transferência de Renda?');" />&nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div align="center" style="width: 100%;">
                                    <b class="titulo">Não existem registro de Programas de Transferência de Renda</b>
                                </div>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </td>
                </tr>
            </table>
            <br />
            <table class="ui-widget ui-widget-content ui-corner-all" cellspacing="2" cellpadding="0"
                width="880" align="center" border="0">
                <tr>
                    <td class="ui-state-default ui-widget-header ui-corner-top">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="height: 30px; padding-left: 10px;">
                                    <img src="../Styles/Icones/redeprotecao.gif" align="absMiddle" />
                                    <b style="font-size: 18px;">52</b> <b>- Programas Estaduais de Transferência de Renda
                                    </b>
                                </td>
                                <td align="right">
                                    <a href="#" runat="server" id="linkAlteracoesQuadro46" visible="false">
                                        <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                    </a>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListView ID="lstProgramasEstaduais" runat="server" DataKeyNames="IdTipoTransferenciaRenda"
                            OnItemCommand="lstProgramas_ItemCommand" OnItemDataBound="lstProgramas_ItemDataBound">
                            <LayoutTemplate>
                                <table class="ui-jqgrid ui-widget ui-widget-content ui-corner-all" cellspacing="0"
                                    cellpadding="0" border="0">
                                    <thead>
                                        <tr>
                                            <th colspan="9" class="ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix"
                                                style="height: 20px;">
                                                <span class="ui-jqgrid-title">Programas Estaduais</span>
                                            </th>
                                        </tr>
                                        <tr class="ui-jqgrid-labels">
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="60" rowspan="2">
                                                Visualizar
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="330" rowspan="2">
                                                Nome
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="100" rowspan="2">
                                                Aderiu ao<br />
                                                Programa
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="240" colspan="3" style="height: 22px;
                                                padding-top: 3px;" valign="top">
                                                Serviços Associados
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="150" rowspan="2">
                                                Previsão de<br />
                                                Repasse<br />
                                                Anual
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="50" rowspan="2">
                                                Excluir
                                            </th>
                                        </tr>
                                        <tr class="ui-jqgrid-labels">
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="90">
                                                Visualizar/<br />
                                                Editar
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="80">
                                                Total de<br />
                                                Unidades
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="80">
                                                Total de<br />
                                                Serviços
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="jqgfirstrow" style="height: auto;">
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="ui-widget-content row">
                                    <td align="center" style="height: 22px;">
                                        <asp:ImageButton runat="server" ID="btnVis" ToolTip="Visualizar Transferência de Renda"
                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Visualizar" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' />
                                    </td>
                                    <td class="align-left">
                                        <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                                    </td>
                                    <td class="align-center">
                                        <%#((Int32)DataBinder.Eval(Container.DataItem, "Aderiu")) > 0 ? "Sim" : "Não" %>
                                    </td>
                                    <td id="tdNao" align="center" runat="server" visible="false" colspan="3">
                                        -------------
                                    </td>
                                    <td align="center" id="tdVisualizarServicos" runat="server">
                                        <asp:ImageButton runat="server" ID="btnVisServicos" ToolTip="Visualizar Serviços Associados"
                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' />
                                    </td>
                                    <td class="align-center" id="tdUnidades" runat="server">
                                        <%#DataBinder.Eval(Container.DataItem, "TotalUnidades") %>
                                    </td>
                                    <td class="align-center" id="tdServicos" runat="server">
                                        <%#DataBinder.Eval(Container.DataItem, "TotalServicos") %>
                                    </td>
                                    <td class="align-center">
                                        <%#((Decimal)DataBinder.Eval(Container.DataItem, "PrevisaoAnualRepasse")).ToString("N2") %>
                                    </td>
                                    <td class="align-center">
                                        <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                            CommandName="Excluir" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>'
                                            CausesValidation="false" OnClientClick="return confirm('Deseja realmente remover a Transferência de Renda?');" />&nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div align="center" style="width: 100%;">
                                    <b class="titulo">Não existem registro de Programas de Transferência de Renda municipais</b>
                                </div>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr id="trSPSolidario" runat="server" visible="false">
                    <td align="left">
                        <asp:HiddenField ID="hfSPSolidario" Value="0" runat="server" />
                        <fieldset>
                            <legend><b class="titulo">Recursos para o Programa São Paulo Solidário</b></legend>
                            <table>
                                <tr>
                                    <td valign="top">
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <b>Valor do recurso repassado via FEAS para a Busca Ativa:</b><br />
                                                    <asp:TextBox ID="txtFEAS" runat="server" Text="0,00" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <b>Mês e ano em que foi realizado o repasse para a Busca Ativa:</b><br />
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Mês :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlMesRepasseFEAS" runat="server">
                                                                    <asp:ListItem Value="0" Text="[Selecione o mês]" Selected="True" />
                                                                    <asp:ListItem Value="1" Text="Janeiro" />
                                                                    <asp:ListItem Value="2" Text="Fevereiro" />
                                                                    <asp:ListItem Value="3" Text="Março" />
                                                                    <asp:ListItem Value="4" Text="Abril" />
                                                                    <asp:ListItem Value="5" Text="Maio" />
                                                                    <asp:ListItem Value="6" Text="Junho" />
                                                                    <asp:ListItem Value="7" Text="Julho" />
                                                                    <asp:ListItem Value="8" Text="Agosto" />
                                                                    <asp:ListItem Value="9" Text="Setembro" />
                                                                    <asp:ListItem Value="10" Text="Outubro" />
                                                                    <asp:ListItem Value="11" Text="Novembro" />
                                                                    <asp:ListItem Value="12" Text="Dezembro" />
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                Ano:
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlAnoRepasseFEAS" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAnoRepasseFEAS_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0" Text="[Selecione o ano]" Selected="True" />
                                                                    <asp:ListItem Value="2011" Text="2011" />
                                                                    <asp:ListItem Value="2012" Text="2012" />
                                                                    <asp:ListItem Value="2013" Text="2013" />
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="trRepasseFEAS" runat="server" visible="false">
                                                <td colspan="2">
                                                    <b>Este valor foi retido no FMAS para utilização em 2013? </b>
                                                    <br />
                                                    <asp:RadioButtonList ID="rblRepasseFEAS" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Sim" Selected="True" />
                                                        <asp:ListItem Value="0" Text="Não" />
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top">
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <b>Valor do recurso repassado via FEAS para a Agenda da Família:</b><br />
                                                    <asp:TextBox ID="txtFEASAgendaFamilia" runat="server" Text="0,00" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <b>Mês e ano em que foi realizado o repasse para a Agenda da Família:</b><br />
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Mês :
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlMesRepasseFEASAgendaFamilia" runat="server">
                                                                    <asp:ListItem Value="0" Text="[Selecione o mês]" Selected="True" />
                                                                    <asp:ListItem Value="1" Text="Janeiro" />
                                                                    <asp:ListItem Value="2" Text="Fevereiro" />
                                                                    <asp:ListItem Value="3" Text="Março" />
                                                                    <asp:ListItem Value="4" Text="Abril" />
                                                                    <asp:ListItem Value="5" Text="Maio" />
                                                                    <asp:ListItem Value="6" Text="Junho" />
                                                                    <asp:ListItem Value="7" Text="Julho" />
                                                                    <asp:ListItem Value="8" Text="Agosto" />
                                                                    <asp:ListItem Value="9" Text="Setembro" />
                                                                    <asp:ListItem Value="10" Text="Outubro" />
                                                                    <asp:ListItem Value="11" Text="Novembro" />
                                                                    <asp:ListItem Value="12" Text="Dezembro" />
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                Ano:
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlAnoRepasseFEASAgendaFamilia" runat="server">
                                                                    <asp:ListItem Value="0" Text="[Selecione o ano]" Selected="True" />
                                                                    <asp:ListItem Value="2013" Text="2013" />
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" SkinID="button-save"
                                            Text="Salvar" Width="89px" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <br />
            <table class="ui-widget ui-widget-content ui-corner-all" cellspacing="2" cellpadding="0"
                width="880" align="center" border="0">
                <tr>
                    <td class="ui-state-default ui-widget-header ui-corner-top">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="height: 30px; padding-left: 10px;">
                                    <img src="../Styles/Icones/redeprotecao.gif" align="absMiddle" />
                                    <b style="font-size: 18px;">55</b> <b>- Programas Municipais de Transferência de Renda
                                    </b>
                                </td>
                                <td align="right">
                                    <a href="#" runat="server" id="linkAlteracoesQuadro50" visible="false">
                                        <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                    </a>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListView ID="lstProgramasMunicipais" runat="server" DataKeyNames="IdTipoTransferenciaRenda"
                            OnItemCommand="lstProgramas_ItemCommand" OnItemDataBound="lstProgramas_ItemDataBound">
                            <LayoutTemplate>
                                <table class="ui-jqgrid ui-widget ui-widget-content ui-corner-all" cellspacing="0"
                                    cellpadding="0" border="0">
                                    <thead>
                                        <tr>
                                            <th colspan="9" class="ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix"
                                                style="height: 20px;">
                                                <span class="ui-jqgrid-title">Programas Municipais</span>
                                            </th>
                                        </tr>
                                        <tr class="ui-jqgrid-labels">
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="60" rowspan="2">
                                                Visualizar
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="430" rowspan="2">
                                                Nome
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="240" colspan="3" style="height: 22px;
                                                padding-top: 3px;" valign="top">
                                                Serviços Associados
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="150" rowspan="2">
                                                Previsão de<br />
                                                Repasse<br />
                                                Anual
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="50" rowspan="2">
                                                Excluir
                                            </th>
                                        </tr>
                                        <tr class="ui-jqgrid-labels">
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="90">
                                                Visualizar/<br />
                                                Editar
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="80">
                                                Total de<br />
                                                Unidades
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="80">
                                                Total de<br />
                                                Serviços
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="jqgfirstrow" style="height: auto;">
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="ui-widget-content row">
                                    <td align="center" style="height: 22px;">
                                        <asp:ImageButton runat="server" ID="btnVis" ToolTip="Visualizar Transferência de Renda"
                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Visualizar" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' />
                                    </td>
                                    <td class="align-left">
                                        <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                                    </td>
                                    <td id="tdNao" align="center" runat="server" visible="false" colspan="3">
                                        -------------
                                    </td>
                                    <td align="center" id="tdVisualizarServicos" runat="server">
                                        <asp:ImageButton runat="server" ID="btnVisServicos" ToolTip="Visualizar Serviços Associados"
                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' />
                                    </td>
                                    <td class="align-center" id="tdUnidades" runat="server">
                                        <%#DataBinder.Eval(Container.DataItem, "TotalUnidades") %>
                                    </td>
                                    <td class="align-center" id="tdServicos" runat="server">
                                        <%#DataBinder.Eval(Container.DataItem, "TotalServicos") %>
                                    </td>
                                    <td class="align-center">
                                        <%#((Decimal)DataBinder.Eval(Container.DataItem, "PrevisaoAnualRepasse")).ToString("N2") %>
                                    </td>
                                    <td class="align-center">
                                        <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                            CommandName="Excluir" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>'
                                            CausesValidation="false" OnClientClick="return confirm('Deseja realmente remover a Transferência de Renda?');" />&nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div align="center" style="width: 100%;">
                                    <b class="titulo">Não existem registros de Programas de Transferência de Renda Municipais.</b>
                                </div>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="padding-top: 20px;">
                        <asp:Button ID="btnIncluir" runat="server" Text="Incluir Programa de Transferência de Renda Municipal"
                            SkinID="button-add" CausesValidation="False" Width="336px" PostBackUrl="~/BlocoIII/FTransferenciaRenda.aspx">
                        </asp:Button>
                    </td>
                </tr>
            </table>
            <table width="880" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="CProgramasProjetos.aspx">
                           <span class="mif-arrow-left"></span>
                            Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="CBeneficioEventual.aspx">Próximo
                           <span class="mif-arrow-right"></span></a>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
