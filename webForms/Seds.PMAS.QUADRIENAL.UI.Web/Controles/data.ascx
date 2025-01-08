<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="data.ascx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Controles.data" %>

<div class="cell">
    <div class="input-control text">
        <asp:TextBox runat="server" ID="txtData" Width="100px" MaxLength="10"></asp:TextBox>
        <asp:ImageButton ID="ImgBntCalc" runat="server" ImageUrl="~/Styles/Icones/Calendar_scheduleHS.png" CausesValidation="False" />
    </div>
</div>
<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
    TargetControlID="txtData"
    Mask="99/99/9999"
    MaskType="Date" />
<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtData" PopupButtonID="ImgBntCalc" />
