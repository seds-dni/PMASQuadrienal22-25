<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cpf.ascx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Controles.cpf" %>
<asp:TextBox runat="server" ID="txtCPF" width="125"  MaxLength="14"></asp:TextBox>
<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1"
    TargetControlID="txtCPF" 
    runat="server"
    Mask="999,999,999-99"
    MaskType="Number" 
    InputDirection="LeftToRight" ClearMaskOnLostFocus="false"/>
