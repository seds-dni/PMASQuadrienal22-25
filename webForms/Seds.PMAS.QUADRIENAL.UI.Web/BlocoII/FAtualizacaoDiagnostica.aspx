<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="FAtualizacaoDiagnostica.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoII.FAtualizacaoDiagnostica" %>

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
                            2.6 - Atualizações Anuais</b>
                                <span class="mif-earth icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Atualizações Anuais">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <b>Atualização realizada no 2º semestre de 2022:</b><br />
                                            <div class="input-control text big-input full-size">
                                                <asp:TextBox ID="txtAtualizacaoExercicio1" runat="server" Height="302px" MaxLength="4000" TextMode="MultiLine"></asp:TextBox>
                                                <br />
                                            </div>
                                            <skm:TextBoxCounter ID="NameCounter2018" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 4000 caracteres."
                                                Font-Bold="True" TextBoxControlId="txtAtualizacaoExercicio1" maxcharacterlength="4000" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Atualização realizada no 2º semestre de 2023:</b><br />
                                            <div class="input-control text big-input full-size">
                                                <asp:TextBox ID="txtAtualizacaoExercicio2" runat="server" Height="302px" MaxLength="4000" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <br />
                                            <skm:TextBoxCounter ID="NameCounter2019" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 4000 caracteres."
                                                Font-Bold="True" TextBoxControlId="txtAtualizacaoExercicio2" maxcharacterlength="4000" />
                                            <br />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Atualização realizada no 2º semestre de 2024:</b>
                                            <div class="input-control text big-input full-size">
                                                <asp:TextBox ID="txtAtualizacaoExercicio3" runat="server" Height="302px" MaxLength="4000" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                            <br />
                                            <skm:TextBoxCounter ID="NameCounter2020" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 4000 caracteres."
                                                Font-Bold="True" TextBoxControlId="txtAtualizacaoExercicio3" maxcharacterlength="4000" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:Button ID="btnSalvarAtualizacoes" runat="server" SkinID="button-save" Width="89px"
                                                Text="Salvar" OnClick="btnSalvarAtualizacoes_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
            </form>
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
            <table width="100%" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="FAnaliseInterpretacao.aspx">
                            <span class="mif-arrow-left"></span>
                            Anterior</a>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdfId" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
