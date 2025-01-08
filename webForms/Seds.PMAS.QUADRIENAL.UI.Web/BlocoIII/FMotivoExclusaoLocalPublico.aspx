<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FMotivoExclusaoLocalPublico.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.FMotivoExclusaoLocalPublico" %>

<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form name="frmPrefeitura">
        <div class="accordion" data-role="accordion">
            <div class="frame active">
                <div class="heading">
                    Desativação deste Local de Execução
                </div>
                <div class="content">
                    <div class="formInput">
                        <div class="grid">
                            <div class="row">
                                <fieldset class="border-blue">
                                    <legend class="lgnd"><b class="fg-blue"></b></legend>
                                    <div class="row">
                                        <div class="cell">
                                            Para proceder à desativação deste local de execução no sistema PMASweb, registre as informações solicitadas.
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Data da desativação do registro:&nbsp;</b><asp:Label ID="lblDataExclusaoRegistro" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>A desativação deste local de execução é devida a:</b><br />
                                                    <asp:RadioButtonList ID="rblMotivoExclusao" runat="server" OnSelectedIndexChanged="rblMotivoExclusao_SelectedIndexChanged" AutoPostBack="true"></asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row" id="trDataEncerramento" runat="server" visible="false">
                                                <div class="cell">
                                                    <b>
                                                       Data do encerramento das atividades deste local de execução:&nbsp;</b><uc4:data ID="txtDataEncerramento" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row" id="trMotivoEncerramento" runat="server" visible="false">
                                                <div class="cell">
                                                    <b>O encerramento das atividades deste local de execução deve-se a:</b><br />
                                                    <asp:RadioButtonList ID="rblMotivoEncerramento" runat="server"></asp:RadioButtonList>
                                                </div>
                                            </div>


                                            <div class="row" id="trDetalhamento" runat="server" visible="false">
                                                <div class="cell">
                                                    <div class="input-control text big-input full-size" data-role="input">
                                                        <b>
                                                            <asp:Label ID="lblDetalhamento" runat="server" Text="Detalhamento sobre o motivo do encerramento das atividades deste local de execução:"></asp:Label></b><br />
                                                        <asp:TextBox Width="100%" ID="txtDetalhamento" runat="server" TextMode="MultiLine"
                                                            Height="302px" MaxLength="2000"></asp:TextBox>
                                                        <button class="button helper-button clear"><span class="mif-cross"></span></button>
                                                        <br />
                                                    </div>
                                                    <skm:TextBoxCounter ID="NameCounter" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 2000 caracteres."
                                                        Font-Bold="True" TextBoxControlId="txtDetalhamento" maxcharacterlength="2000" />
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell" align="center">
                                                    <asp:Button ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" SkinID="button-save" Text="Salvar" Width="89px" OnClientClick="return confirm('Tem certeza que deseja desativar o local de execução?');" />
                                                    &nbsp;<asp:Button ID="btnVoltar" runat="server" OnClick="btnVoltar_Click" Text="Voltar" />
                                                </div>
                                            </div>
                                            <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                                width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                                                <tr>
                                                    <td style="padding: 15px 10px 2px 15px">
                                                        <span class="mif-warning mif-2x"></span>
                                                        <%-- <img src="../Styles/Icones/messagebox_warning.png" align="absMiddle" />--%><b style='color: #000000 !important'>Verifique
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
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SecondContent" runat="server">
</asp:Content>
