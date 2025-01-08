<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VProgramaProjetoDetalhe.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.VProgramaProjetoDetalhe" %>

<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <table class="ui-widget ui-widget-content ui-corner-all" cellspacing="2" cellpadding="0"
                width="600" align="center" border="0">
                <tr>
                    <td class="ui-state-default ui-widget-header ui-corner-top" style="height: 30px; padding-left: 10px;">
                        <img src="../Styles/Icones/redeprotecao.gif" align="absMiddle" />
                        <b style="font-size: 18px;">
                            <asp:Label ID="lblNumeracao" runat="server" Text="46" />
                            - </b>
                        <b>
                            <asp:Label ID="lblTituloPrograma" runat="server"></asp:Label></b></td>
                </tr>
                <tr>
                    <td align="left">
                        <table width="100%">
                            <tr class="bg-alternative">
                                <td colspan="2">
                                    <b>Nome:</b><br />
                                    <asp:Label ID="lblPrograma" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <b>
                                        <asp:Label ID="lblNomePrograma" runat="server" Text="Objetivo"></asp:Label></b><br />
                                    <asp:TextBox ID="txtNome" runat="server" TextMode="MultiLine" Height="51" Width="668px" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <fieldset>
                                        <legend><b class="titulo">
                                            <asp:Label ID="lblQuadro" runat="server" Text="Previsão anual de número de beneficiários e valor de repasse" /></b></legend>
                                        <table width="100%" align="left" id="divBeneficiariosBPCIdosoPessoaDeficiencia" visible="false"
                                            runat="server">
                                            <tr>
                                                <td align="left" valign="top">
                                                    <b>Nº de beneficiários:</b><br />
                                                    <asp:TextBox ID="txtBPCIdosoPessoaDeficienciaNumeroAtendidos" Style="text-align: right"
                                                        runat="server" MaxLength="6" TabIndex="17" Width="72px"></asp:TextBox>
                                                    (referência: julho/2016)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <b>Previsão anual de valor de repasse:</b>
                                                    <br />
                                                    R$
                                                    <asp:Label runat="server" ID="lblBPCIdosoPessoaDeficienciaValor"></asp:Label>&nbsp;
                                                    (SM X 12 meses X nº de beneficiários)
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr runat="server" id="trBPCEscola" visible="false">
                                <td colspan="2">
                                    <fieldset>
                                        <legend><b class="titulo">Programa BPC na Escola</b></legend>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="4">
                                                    <strong>O município aderiu ao Programa BPC na Escola?</strong>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:RadioButtonList ID="rbAderiuBPCnaEscola" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr id="trAdesaoBPCEscola" runat="server" visible="false">
                                                <td><b>Data de adesão ao programa:</b><br />
                                                </td>
                                                <td>
                                                    <uc4:data ID="txtDataImplantacao" runat="server" />
                                                </td>
                                                <td><b>Número de beneficiários do Programa:</b></td>
                                                <td>
                                                    <asp:TextBox ID="txtNumeroBeneficiariosBPC" Width="100" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <fieldset>
                                        <legend><b class="titulo">Articulações promovidas</b></legend><b>Existem parcerias estabelecidas, formal ou informalmente, para a concessão deste benefício?</b><br />
                                        <asp:RadioButtonList ID="rblParcerias" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="1" Text="Sim" />
                                            <asp:ListItem Value="0" Text="Não" Selected="True" />
                                        </asp:RadioButtonList>
                                        <table width="100%" id="tbParcerias" runat="server" visible="false">
                                            <tr>
                                                <td colspan="2">
                                                    <b>Informe com quem foram formalizadas as parcerias para a execução deste programa/projeto:</b><br />
                                                    <asp:DropDownList ID="ddlParceria" runat="server">
                                                        <asp:ListItem Selected="True">Selecione</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="350">
                                                    <b>Nome do Órgão:</b><br />
                                                    <asp:TextBox ID="txtNomeOrgao" runat="server" Width="340px" Enabled="false" />
                                                </td>
                                                <td>
                                                    <b>Tipo da Parceria:</b><br />
                                                    <asp:DropDownList ID="ddlTipoParceria" runat="server" Enabled="false">
                                                        <asp:ListItem Selected="True">Selecione</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btnAdicionarParceria" runat="server" SkinID="button-add" Text="Adicionar"
                                                        Width="100px" Enabled="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:ListView ID="lstParcerias" runat="server">
                                                        <LayoutTemplate>
                                                            <table class="ui-jqgrid ui-widget ui-widget-content ui-corner-all" cellspacing="0"
                                                                cellpadding="0" border="0">
                                                                <thead>
                                                                    <tr class="ui-jqgrid-labels">
                                                                        <th class="ui-state-default ui-th-column ui-th-ltr" width="20" style="height: 22px;"></th>
                                                                        <th class="ui-state-default ui-th-column ui-th-ltr" width="220">Nome do Órgão
                                                                        </th>
                                                                        <th class="ui-state-default ui-th-column ui-th-ltr" width="250">Parcerias
                                                                        </th>
                                                                        <th class="ui-state-default ui-th-column ui-th-ltr" width="125">Tipo da Parceria
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr class="jqgfirstrow" style="height: auto;">
                                                                        <td style="height: 0px;"></td>
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
                                                                    <%#DataBinder.Eval(Container.DataItem, "NomeOrgao") %>
                                                                </td>
                                                                <td align="left">
                                                                    <%#DataBinder.Eval(Container.DataItem, "Parceria.Nome") %>
                                                                </td>
                                                                <td align="left">
                                                                    <%#DataBinder.Eval(Container.DataItem, "TipoParceria.Nome") %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </td>
                                            </tr>

                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
