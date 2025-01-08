<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cep.ascx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Controles.cep" %>
<form name="cep">
    <div class="row cells2">
        <div class="cell">
            <b>CEP:</b><br />
            <div class="input-control text">
                <asp:TextBox ID="txtCEP" runat="server" MaxLength="9"></asp:TextBox>
                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2"
                    TargetControlID="txtCEP"
                    runat="server"
                    Mask="99999-999"
                    MaskType="Number"
                    InputDirection="LeftToRight" ClearMaskOnLostFocus="false" />
            </div>
            <div class="input-control">
                <asp:Button ID="cmdPesqCEP" runat="server" CssClass="image-button primary mif-contacts-dialer"
                    Width="113px" Text="Pesquisar CEP" SkinID="button-find"
                    CausesValidation="False" OnClick="cmdPesqCEP_Click"></asp:Button>
            </div>
        </div>
        <div class="cell">
            <b>Endereço:</b><br />
            <div class="input-control text">
                <asp:TextBox ID="txtLogradouro" runat="server" Width="300px" MaxLength="70"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="row cells3">
        <div class="cell">
            <b>Número:</b><br />
            <div class="input-control text">
                <asp:TextBox ID="txtNumero" runat="server" Width="88px" MaxLength="20"></asp:TextBox>
            </div>
        </div>
        <div class="cell">
            <b>Complemento:</b><br />
            <div class="input-control text">
                <asp:TextBox ID="txtComplemento" runat="server" Width="150px" MaxLength="20"></asp:TextBox>
            </div>
        </div>
        <div class="cell">
            <b>Bairro:</b><br />
            <div class="input-control text">
                <asp:TextBox ID="txtBairro" runat="server" Width="250px" MaxLength="40"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="row cells3">
        <div class="row">
            <div class="cell">
                <b>Cidade:</b><br />
                <div class="input-control text">
                    <asp:TextBox ID="txtCidade" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
                </div>
            </div>
        </div>
</form>
