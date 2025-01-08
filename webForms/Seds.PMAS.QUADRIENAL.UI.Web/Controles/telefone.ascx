<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="telefone.ascx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Controles.telefone" %>
<div class="input control-text">
    <asp:TextBox ID="txtDDD" runat="server" Width="42px" MaxLength="2"></asp:TextBox>
    &nbsp;
        <asp:TextBox ID="txtTelefone" runat="server" Width="110px" MaxLength="9"></asp:TextBox>
    <ajaxToolkit:FilteredTextBoxExtender ID="ftbeDDD" runat="server" TargetControlID="txtDDD" FilterType="Numbers" />
</div>
<%--<ajaxToolkit:MaskedEditExtender ID="mkeFone"
    TargetControlID="txtTelefone" runat="server" MaskType="Number" Mask="9999-9999" InputDirection="LeftToRight" ClearMaskOnLostFocus="false"/>--%>