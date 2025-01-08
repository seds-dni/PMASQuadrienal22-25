<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="celular.ascx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Controles.celular" %>

<asp:TextBox ID="txtDDDCelular" runat="server" Width="42px" MaxLength=2></asp:TextBox> &nbsp;&nbsp;
<asp:TextBox ID="txtCelular" runat="server" MaxLength="10" Width="115px"></asp:TextBox>
<ajaxToolkit:FilteredTextBoxExtender ID="ftbeDDDCelular" runat="server" TargetControlID="txtDDDCelular" FilterType="Numbers" />
<%--<ajaxToolkit:MaskedEditExtender ID="mkeFone"
    TargetControlID="txtCelular" runat="server" Mask="99999-9999" MaskType="Number" InputDirection="LeftToRight" ClearMaskOnLostFocus="false" />--%>