<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cnpj.ascx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Controles.cnpj" %>
<asp:TextBox ID="txtCNPJ" width="180" runat="server" MaxLength="18"></asp:TextBox>
    <ajaxToolkit:MaskedEditExtender ID="mkeCNPJ" 
    TargetControlID="txtCNPJ" 
    runat="server"
    Mask="99,999,999/9999-99"
    MaskType="Number" 
    InputDirection="LeftToRight" ClearMaskOnLostFocus="false"/>