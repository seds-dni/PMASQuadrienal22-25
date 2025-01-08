<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FHistorico.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoV.FHistorico" %>

<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <script src="../Scripts/dataFormat.js" type="text/javascript"></script>

    <form name="frmHistorico">

        <div class="formInput" data-text="Histórico Prestação de Contas">
            <asp:Label runat="server" ID="Label1" Text="Data" Font-Bold="True"></asp:Label>
            <br />
            <asp:Label runat="server" ID="lblData"></asp:Label>
            <br />
            <br />
            <asp:Label runat="server" ID="Label2" Text="Situação:" Font-Bold="True"></asp:Label>
            <br />
            <asp:Label runat="server" ID="lblSituacao"></asp:Label>
            <br />
            <br />
            <asp:Label runat="server" ID="Label4" Text="Responsável:" Font-Bold="True"></asp:Label>
            <br />
            <asp:Label runat="server" ID="lblNome"></asp:Label>
            <br />
            <br />
            <asp:Label runat="server" ID="Label6" Text ="Descricao Motivo:" Font-Bold="True"></asp:Label>
            <br />
            <asp:Label runat="server" ID="lblDescricaoMotivo"></asp:Label>
        </div>
        <div class="frame">
            <asp:Button runat="server" ID="btnVoltar" Text="Voltar" PostBackUrl="~/BlocoV/FPrestacaoDeContas.aspx"/>
        </div>


        <asp:HiddenField ID="hdfAno" runat="server" Value="" />
    </form>
</asp:Content>