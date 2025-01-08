<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FAnaliseInterpretacao.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoII.FAnaliseInterpretacao" %>

<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmAnaliseInterpretacao">
                <div class="accordion" data-role="accordion" data-close-any="true">
                    <div class="frame active">
                        <div class="heading">
                            <a href="#" runat="server" id="linkAlteracoesQuadro14" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado &nbsp;&nbsp;
                            </a>
                            2.5 - Análise e interpretação</b>
                                <span class="mif-earth icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Análise e interpretação">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <b>Território e demografia:</b><br />
                                            <div class="input-control text big-input full-size">
                                                <asp:Label ID="txtCaracterizacaoDemografia" runat="server" Enabled="false" ReadOnly="true"></asp:Label>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>População e Vulnerabilidade:</b><br />
                                            <div class="input-control text big-input full-size">
                                                <asp:Label ID="txtCaracterizacaoPopulacao" runat="server" Enabled="false" ReadOnly="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Evolução da rede de atendimento</b>
                                            <div class="input-control text big-input full-size">
                                                <asp:Label ID="txtCaracterizacaoRedeSocioAssistencial" runat="server" Enabled="false" ReadOnly="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Análise e interpretação</b><br />
                                            <div class="input-control text big-input full-size">
                                                <asp:TextBox ID="txtAnaliseInterpretacao" runat="server" Height="302px" MaxLength="4000" TextMode="MultiLine" Width="980px"></asp:TextBox>
                                            </div>
                                            <br />
                                            <skm:TextBoxCounter ID="NameCounter" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 4000 caracteres."
                                                Font-Bold="True" TextBoxControlId="txtAnaliseInterpretacao" maxcharacterlength="4000" />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:button ID="btnSalvarCaracterizacao" runat="server" SkinID="button-save" Width="89px" Text="Salvar" onClick="btnSalvarCaracterizacao_Click" />
                                </div>
                            </div>
                            
                        </div>
                        <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0" width="100%" align="center" border="0">
                            <tr>
                                <td class="ui-state-highlight titulo" style="padding: 2px 10px 2px 10px;">
                                    <img src="../Styles/Icones/messagebox_warning.png" align="absMiddle" /><b style='color: #000000 !important'>Verifique as inconsistências:</b>
                                    <br />
                                    <br />
                                    <asp:Label ID="lblInconsistencias" ForeColor="Red" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
            </form>

            <table width="100%" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="FSituacaoVulnerabilidade.aspx">
                            <span class="mif-arrow-left"></span>
                            Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="FAtualizacaoDiagnostica.aspx">Próximo<span class="mif-arrow-right"></span></a>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdfId" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
