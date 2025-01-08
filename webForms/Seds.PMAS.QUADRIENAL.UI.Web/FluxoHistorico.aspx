<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FluxoHistorico.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.FluxoHistorico" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="pnlConsulta">
        <ContentTemplate>
            <form name="frmAnaliseInterpretacao">
                <div class="accordion" data-role="accordion" data-close-any="true">
                    <div class="frame active">
                        <div class="heading">
                            <b>Histórico do Plano</b>
                            Municipal -
                                    <asp:Label runat="server" ID="lblMunicipio" Text=""></asp:Label>
                            <span class="mif-history icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Histórico do Plano">
                                    <div class="row">
                                        <asp:ListView ID="lstHistorico" runat="server">
                                            <LayoutTemplate>
                                                <table class="table border bordered" cellspacing="0"
                                                    cellpadding="0" border="0">
                                                    <thead>
                                                        <tr class="info" style="height: 22px;">
                                                            <th width="80">Data
                                                            </th>
                                                            <th width="120">Situação
                                                            </th>
                                                            <th width="200">Responsável
                                                            </th>
                                                            <th width="400">Descrição/Motivo
                                                            </th>
                                                            <th width="60">Visualizar
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server">
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr style="height: 22px;">
                                                    <td align="center">
                                                        <b><%#((DateTime)DataBinder.Eval(Container.DataItem, "Data")).ToString("dd/MM/yyyy HH:mm") %></b>
                                                    </td>
                                                    <td class="align-center">
                                                        <%#DataBinder.Eval(Container.DataItem, "Situacao")%>
                                                    </td>
                                                    <td class="align-center">
                                                        <%#DataBinder.Eval(Container.DataItem, "Usuario")%>
                                                    </td>
                                                    <td class="align-left">
                                                        <%#((String)DataBinder.Eval(Container.DataItem, "Descricao")).Length > 150 ? (((String)DataBinder.Eval(Container.DataItem, "Descricao")).Substring(0,150) + " ...") : DataBinder.Eval(Container.DataItem, "Descricao")%>
                                                    </td>
                                                    <td align="center">
                                                        <a href="VHistorico.aspx?id=<%# Server.UrlEncode(Genericos.clsCrypto.Encrypt(DataBinder.Eval(Container.DataItem, "Id").ToString())) %>">
                                                            <img src="Styles/Icones/find.png" alt="Visualizar" border="0" /></a>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                    <div class="row">
                                        <div class="cell" align="center">
                                            <br />
                                            <asp:Button runat="server" ID="btnVoltar" Text="Voltar" OnClick="btnVoltar_Click" />
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
