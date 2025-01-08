<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="HistoricoAlteracoesCompleto.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.HistoricoAlteracoesCompleto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="pnlConsulta">
        <ContentTemplate>
            <form>
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <b>Histórico Completo de Alterações</b>
                            <span class="mif-flow-cascade icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Histórico PMAS">
                                <div class="grid">
                                    <div class="row">
                                        <asp:ListView ID="lstHistorico" runat="server">
                                            <LayoutTemplate>
                                                <table class="table border bordered">
                                                    <thead class="info">
                                                        <tr style="height: 22px;">
                                                            <th width="80">Data/Horário
                                                            </th>
                                                            <th width="200">Responsável
                                                            </th>
                                                            <th width="170">Quadro
                                                            </th>
                                                            <th width="450">Descrição
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
                                                        <b>
                                                            <%#((DateTime)DataBinder.Eval(Container.DataItem, "DataHorario")).ToString("dd/MM/yyyy HH:mm") %></b>
                                                    </td>
                                                    <td class="align-center">
                                                        <%#DataBinder.Eval(Container.DataItem, "Usuario")%>
                                                    </td>
                                                    <td align="center">
                                                        <%#DataBinder.Eval(Container.DataItem, "Quadro")%>
                                                    </td>
                                                    <td class="align-left">
                                                        <%#((String)DataBinder.Eval(Container.DataItem, "Descricao")).Replace(System.Environment.NewLine,"<br/>")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
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
