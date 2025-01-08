<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="HistoricoAlteracoes.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.HistoricoAlteracoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="pnlConsulta">
        <ContentTemplate>
            <br />
            <form name="frmHistorico">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            Alterações mais recentes
                           <span class="mif-file-binary icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Alterações do sistema">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <asp:ListView ID="lstHistorico" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table striped border"  cellspacing="0"
                                                        cellpadding="0" border="0" width="100%">
                                                        <thead>
                                                            <tr style="height: 22px;">
                                                                <th class="ui-th-ltr" width="80">Data/Horário
                                                                </th>
                                                                <th class="ui-th-ltr" width="200">Responsável
                                                                </th>
                                                                <th class="ui-th-ltr" width="170">Quadro
                                                                </th>
                                                                <th class="ui-th-ltr" width="450">Descrição
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                      <%--      <tr class="jqgfirstrow" style="height: auto;">
                                                                <td style="height: 0px;"></td>
                                                                <td style="height: 0px;"></td>
                                                                <td style="height: 0px;"></td>
                                                                <td style="height: 0px;"></td>
                                                            </tr>--%>
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
                                                        <td align="align-center">
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
                                    <div class="row">
                                        <div class="cell">
                                            <asp:Button ID="btnVoltar" CssClass="btn btn-primary button-save" runat="server" Text="Voltar" OnClientClick="javascript:history.go(-1);return false;" />
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
