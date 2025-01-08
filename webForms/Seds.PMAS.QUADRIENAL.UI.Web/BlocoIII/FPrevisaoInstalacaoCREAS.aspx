<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FPrevisaoInstalacaoCREAS.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.FPrevisaoInstalacaoCREAS" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <table class="ui-widget ui-widget-content ui-corner-all" cellspacing="2" cellpadding="0"
                width="600" align="center" border="0">
                <tr>
                    <td class="ui-state-default ui-widget-header ui-corner-top" style="height: 30px;">
                        <table width="100%">
                            <tr>
                                <td>
                                    <img src="../Styles/Icones/redeprotecao.gif" align="absMiddle" />
                                    <b style="font-size: 18px;">16</b> <b>- Previsão de implantação CREAS</b> </td>
                                <td align="right"><a id="linkAlteracoesPrevisaoImplantacaoCREAS" runat="server" href="#" visible="false">
                                    <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />
                                    Alterado </a>&nbsp; </td>
                            </tr>
                        </table>
                    </td>

                </tr>
                </td>
                </tr>
                <tr class="bg-alternative">
                    <td align="left">
                        <b>Há demanda identificada/registrada para atendimento?</b>
                        <asp:RadioButtonList ID="rblDemandaAtendimento" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="True" OnCheckedChanged="rblDemandaAtendimento_CheckedChanged" OnSelectedIndexChanged="rblDemandaAtendimento_SelectedIndexChanged">
                            <asp:ListItem Value="1">Sim</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">Não</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr id="trTipoDemanda" runat="server" visible="false">
                    <td align="left">
                        <asp:Label ID="Label1" Font-Bold="true" Text="Identifique e quantifique a demanda existente no município:"
                            runat="server"></asp:Label>

                        <asp:ListView ID="lstDemandas" runat="server" ItemPlaceholderID="trDemandas"
                            OnItemDataBound="lstDemandas_ItemDataBound">
                            <LayoutTemplate>
                                <table width="100%">
                                    <tr id="trDemandas" runat="server">
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td width="400">
                                        <asp:CheckBox ID="chkTipoDemanda" runat="server" AutoPostBack="true" OnCheckedChanged="chkTipoDemanda_CheckedChanged" />
                                        <asp:HiddenField ID="hdfIdTipoDemanda" Value="0" runat="server" />
                                    </td>
                                    <td class="align-right">
                                        <asp:TextBox ID="txtQuantidade" runat="server"></asp:TextBox></td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr class="bg-alternative">
                    <td align="left">
                        <b>Há previsão de implantação de CREAS?</b>
                        <asp:RadioButtonList ID="rblPossuiPrevisaoInstalacao" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="True" OnCheckedChanged="rblPossuiPrevisaoInstalacao_CheckedChanged"
                            OnSelectedIndexChanged="rblPossuiPrevisaoInstalacao_SelectedIndexChanged">
                            <asp:ListItem Value="1">Sim</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">Não</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr id="trDataPrevista" runat="server" visible="false">
                    <td align="left">
                        <b>Informar a(s) data(s) prevista(s) para implantação de CREAS em 2017:</b>
                        <br />
                        <asp:TextBox ID="txtDataPrevista" runat="server" MaxLength="10"></asp:TextBox>
                        <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar" CssClass="Botao" Width="80px"
                            OnClick="btnAdicionar_Click" />
                    </td>
                </tr>
                <tr id="trMotivosNaoInstalacao" runat="server" visible="false">
                    <td align="left">
                        <asp:Label ID="lblMotivosNaoInstalacao" Font-Bold="true" Text="Indique o(s) motivo(s) para que não haja previsão de implantação de CREAS:"
                            runat="server"></asp:Label>
                        <asp:CheckBoxList ID="lstMotivosNaoInstalacao" runat="server" />
                    </td>
                </tr>
                <tr id="trDatas" runat="server">
                    <td>
                        <asp:ListView ID="lstDatas" runat="server" OnItemCommand="lstDatas_ItemCommand" OnItemDataBound="lstDatas_ItemDataBound">
                            <LayoutTemplate>
                                <table class="ui-jqgrid ui-widget ui-widget-content ui-corner-all" cellspacing="0"
                                    cellpadding="0" border="0">
                                    <thead>
                                        <tr class="ui-jqgrid-labels">
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="20" style="height: 22px;"></th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="400">Previsão de implantação de CREAS
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="80">Excluir
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="jqgfirstrow" style="height: auto;">
                                            <td style="height: 0px;"></td>
                                            <td style="height: 0px;"></td>
                                            <td style="height: 0px;"></td>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="ui-widget-content row">
                                    <td class="ui-state-default jqgrid-rownum row" style="height: 22px;">
                                        <asp:Label ID="lblSequencia" runat="server" />
                                    </td>
                                    <td align="left">
                                        <b>
                                            <%#((DateTime)DataBinder.Eval(Container.DataItem, "Data")).ToShortDateString() %></b>
                                    </td>
                                    <td class="align-center">
                                        <asp:ImageButton ID="btnExcluirPrevisao" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                            CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('Deseja realmente remover a previsão de implantação?');" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
            </table>
            <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                width="600" align="center" border="0">
                <tr>
                    <td class="ui-state-highlight titulo" style="padding: 2px 10px 2px 10px;">
                        <img src="../Styles/Icones/messagebox_warning.png" align="absMiddle" /><b style='color: #000000 !important'>Verifique
                            as inconsistências:</b>
                        <br />
                        <br />
                        <asp:Label ID="lblInconsistencias" ForeColor="Red" runat="server" />
                    </td>
                </tr>
            </table>
            <div align="center">
                <asp:Button ID="btnSalvar" runat="server" SkinID="button-save" Text="Salvar" Width="89px"
                    OnClick="btnSalvar_Click"></asp:Button>
                &nbsp;<asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
