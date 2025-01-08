<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FAspectosGerais.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoVII.FAspectosGerais" %>

<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmAspectosGerais">
                <div class="accordion" data-role="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <b>7.4 - Aspectos gerais</b>
                            <a href="#" runat="server" id="linkAlteracoesQuadro71" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>&nbsp;
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Aspectos Gerais">
                                <div class="row" id="trSim" runat="server" visible="false">
                                    <div class="cell" align="left">
                                        <b>De que forma os resultados obtidos com a vigilância sociassistencial, monitoramento
                                        e avaliação contribuem para o aprimoramento das ações?</b><br />
                                        <asp:CheckBoxList ID="chkAprimoramentos" runat="server" />
                                    </div>
                                    <div class="row">
                                        <div class="cell" align="left">
                                            <b>Realizou adesão ao Sistema MSE ?</b><br />
                                            <asp:RadioButtonList ID="rblMSE" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblMSE_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="1" Text="Sim" />
                                                <asp:ListItem Value="0" Text="Não" Selected="True" />
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="cell" align="left" id="trExpliqueNaoMse" runat="server">
                                            <asp:TextBox runat="server" ID="txtExpliqueNaoMse" TextMode="MultiLine" MaxLength="1000" Height="64px" Width="50%" placeholder="Qual o motivo de não aderir ? "></asp:TextBox>    
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell" align="left">
                                            <b>Possui sistema informatizado próprio utilizado para vigilância socioassistencial,
                                        monitoramento ou avaliação?</b><br />
                                            <asp:RadioButtonList ID="rblSistemaInformatizado" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Text="Sim" />
                                                <asp:ListItem Value="0" Text="Não" Selected="True" />
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Registre nos campos a seguir as informações sobre as principais e mais recentes pesquisas,
                                        estudos ou levantamentos realizados pelo município, voltados à Assistência Social
                                        e, em especial, aquelas voltadas à vigilância socioassistencial, monitoramento ou
                                        avaliação.</b>
                                            <br />
                                        </div>
                                    </div>
                                    <div class="row" runat="server" visible="false" id="trAdicionarPesquisa">
                                        <div class="cell" align="center">
                                            <asp:Button ID="btnAdicionarPesquisa" runat="server" Text="Adicionar pesquisa" Width="150px"
                                                OnClick="btnAdicionarPesquisa_Click"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="row" runat="server" visible="false" id="trPesquisa1">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Pesquisa 1:</b></legend><b>Período de realização:</b><br />
                                            <div class="row">
                                                <div class="cell">
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtPeriodo1" runat="server" MaxLength="50" Width="206px" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Objetivo:</b><br />
                                                    <div class="input-control textarea">
                                                        <asp:TextBox ID="txtObjetivo1" runat="server" MaxLength="150" TextMode="MultiLine"
                                                            Height="42px" Width="550px" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <skm:TextBoxCounter ID="NameCountertxtObjetivo1" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 150 caracteres."
                                                        Font-Size="9px" TextBoxControlId="txtObjetivo1" MaxCharacterLength="150" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Metodologia:</b><br />
                                                    <div class="input-control textarea">
                                                        <asp:TextBox ID="txtMetodologia1" runat="server" MaxLength="300" TextMode="MultiLine"
                                                            Height="60px" Width="550px" />
                                                    </div>
                                                    <br />
                                                    <skm:TextBoxCounter ID="TextBoxCountertxtMetodologia1" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 300 caracteres."
                                                        Font-Size="9px" TextBoxControlId="txtMetodologia1" MaxCharacterLength="300" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Resultados:</b><br />
                                                    <div class="input-control textarea">
                                                        <asp:TextBox ID="txtResultados1" runat="server" MaxLength="300" TextMode="MultiLine"
                                                            Height="60px" Width="550px" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <skm:TextBoxCounter ID="TextBoxCountertxtResultados1" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 300 caracteres."
                                                        Font-Size="10px" TextBoxControlId="txtResultados1" MaxCharacterLength="300" />
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="row" runat="server" visible="false" id="trPesquisa2">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Pesquisa 2:</b></legend>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Período de realização:</b><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtPeriodo2" runat="server" MaxLength="50" Width="206px" />
                                                    </div>
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Objetivo:</b><br />
                                                    <div class="input-control textarea mid-size">
                                                        <asp:TextBox ID="txtObjetivo2" runat="server" MaxLength="150" TextMode="MultiLine"
                                                            Height="42px" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <skm:TextBoxCounter ID="TextBoxCountertxtObjetivo2" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 150 caracteres."
                                                        Font-Size="9px" TextBoxControlId="txtObjetivo2" MaxCharacterLength="150" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Metodologia:</b><br />
                                                    <div class="input-control textarea">
                                                        <asp:TextBox ID="txtMetodologia2" runat="server" MaxLength="300" TextMode="MultiLine"
                                                            Height="60px" Width="550px" />
                                                    </div>
                                                    <br />
                                                    <skm:TextBoxCounter ID="TextBoxCountertxtMetodologia2" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 300 caracteres."
                                                        Font-Size="9px" TextBoxControlId="txtMetodologia2" MaxCharacterLength="300" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Resultados:</b><br />
                                                    <div class="input-control textarea">
                                                        <asp:TextBox ID="txtResultados2" runat="server" MaxLength="300" TextMode="MultiLine"
                                                            Height="60px" Width="550px" />
                                                    </div>
                                                    <br />
                                                    <skm:TextBoxCounter ID="TextBoxCountertxtResultados2" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 300 caracteres."
                                                        Font-Size="9px" TextBoxControlId="txtResultados2" MaxCharacterLength="300" />
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="row" runat="server" visible="false" id="trPesquisa3">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Pesquisa 3:</b></legend>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Período de realização:</b><br />
                                                    <div class="input-control text">
                                                        <asp:TextBox ID="txtPeriodo3" runat="server" MaxLength="50" Width="206px" />
                                                    </div>
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Objetivo:</b><br />
                                                    <div class="input-control textarea mid-size">
                                                        <asp:TextBox ID="txtObjetivo3" runat="server" MaxLength="150" TextMode="MultiLine"
                                                            Height="42px" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <skm:TextBoxCounter ID="TextBoxCountertxtObjetivo3" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 150 caracteres."
                                                        Font-Size="9px" TextBoxControlId="txtObjetivo3" MaxCharacterLength="150" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Metodologia:</b><br />
                                                    <div class="input-control textarea">
                                                        <asp:TextBox ID="txtMetodologia3" runat="server" MaxLength="300" TextMode="MultiLine"
                                                            Height="60px" Width="550px" />
                                                    </div>
                                                    <br />
                                                    <skm:TextBoxCounter ID="TextBoxCountertxtMetodologia3" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 300 caracteres."
                                                        Font-Size="9px" TextBoxControlId="txtMetodologia3" MaxCharacterLength="300" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Resultados:</b><br />
                                                    <div class="input-control textarea">
                                                        <asp:TextBox ID="txtResultados3" runat="server" MaxLength="300" TextMode="MultiLine"
                                                            Height="60px" Width="550px" />
                                                    </div>
                                                    <br />
                                                    <skm:TextBoxCounter ID="TextBoxCountertxtResultados3" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 300 caracteres."
                                                        Font-Size="9px" TextBoxControlId="txtResultados3" MaxCharacterLength="300" />
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="row" style="visibility: hidden;">
                                        <div class="cell">
                                            <asp:HiddenField ID="hdfPesquisa1" Value="0" runat="server" />
                                            <asp:HiddenField ID="hdfPesquisa2" Value="0" runat="server" />
                                            <asp:HiddenField ID="hdfPesquisa3" Value="0" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell" align="center">
                                            <asp:Button ID="btnSalvar" runat="server" SkinID="button-save" Text="Salvar" Width="89px"
                                                OnClick="btnSalvar_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="trNao" runat="server" visible="true">
                                    <div class="cell" align="center" style="padding-top: 20px; padding-bottom: 20px;">
                                        <b class="titulo">Não existe registro de ações de vigilância, monitoramento ou avaliação</b>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                    width="700" align="center" border="0">
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
            </form>
            <table width="100%" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="FAvaliacao.aspx">
                            <span class="mif-arrow-left" />Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">&nbsp;
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdfId" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
