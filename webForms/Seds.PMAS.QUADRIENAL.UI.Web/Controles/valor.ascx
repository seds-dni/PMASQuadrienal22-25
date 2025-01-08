<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="valor.ascx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Controles.valor" %>
<asp:TextBox ID="txtValor" runat="server" MaxLength="17"></asp:TextBox>
<ajaxToolkit:MaskedEditExtender ID="mkeValor" TargetControlID="txtValor" runat="server" Mask="999,999,999.99" MaskType="Number" AcceptNegative="None" InputDirection="RightToLeft"  DisplayMoney="Left" />            
