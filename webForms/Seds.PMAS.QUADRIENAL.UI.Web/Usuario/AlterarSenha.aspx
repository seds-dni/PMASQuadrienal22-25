<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AlterarSenha.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Usuario.AlterarSenha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form>
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            Alterar Senha
                           <span class="mif-lock icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Alterar Senha">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <b>Senha atual:</b><br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtSenhaAtual" runat="server" TextMode="Password"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Nova senha:</b><br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtNovaSenha" runat="server" TextMode="Password"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Confirmar senha:</b><br />
                                            <div class="input-control text">
                                                <asp:TextBox ID="txtConfirmacaoSenha" runat="server" TextMode="Password"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <div class="input-control text">
                                                <asp:Button ID="btnSalvar" runat="server" SkinID="button-save" Text="Salvar" OnClick="btnSalvar_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                                width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                                                <tr>
                                                    <td style="padding: 15px 10px 2px 15px">
                                                        <span class="mif-warning mif-2x"></span>
                                                        <b style='color: #000000 !important'>Verifique
                                                    as inconsistências:</b>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 10px 10px 12px 45px;">
                                                        <asp:Label ID="lblInconsistencias" ForeColor="Red" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
