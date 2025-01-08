<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CServicosRecursosFinanceirosCREASDesativado.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.CServicosRecursosFinanceirosCREASDesativado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmUnidadesPublicas">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <a href="#" runat="server" id="linkAlteracoesQuadro23" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>&nbsp;
                          Serviços socioassistenciais desativados no&nbsp;
                          <asp:Label ID="lblCREAS" runat="server" />
                            <span class="mif-organization icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Serviços">
                                <div class="grid">
                                    <div class="row">
                                        <asp:ListView ID="lstRecursos" runat="server">
                                            <LayoutTemplate>
                                                <table class="table striped border bordered" cellspacing="0"
                                                    cellpadding="0" border="0">
                                                    <thead class="info">
                                                        <tr>
                                                            <th width="100">Visualizar/<br />
                                                                Editar
                                                            </th>
                                                            <th width="350">Tipo de serviço
                                                            </th>
                                                            <th width="220">Usuários
                                                            </th>
                                                            <th width="140">Capacidade mensal de pessoas/famílias atendidas</th>
                                                            <th width="150">Valor do cofinanciamento estadual
                                                            </th>
                                                            <%--<th width="150">Previsão<br />
                                                                orçamentária
                                                            </th>--%>
                                                            <th width="50">Desativado
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
                                                    <td colspan="8">
                                                        <b>Proteção Social:</b>
                                                        <%#DataBinder.Eval(Container.DataItem, "Key") %>
                                                    </td>
                                                </tr>
                                                <asp:ListView ID="lstItems" runat="server" DataSource='<%#Eval("Items") %>'>
                                                    <LayoutTemplate>
                                                        <tr id="itemPlaceholder" runat="server">
                                                        </tr>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td align="center">
                                                                <a href="VServicoRecursoFinanceiroCREAS.aspx?id=<%# Server.UrlEncode(Genericos.clsCrypto.Encrypt(DataBinder.Eval(Container.DataItem, "Id").ToString())) %>&idCentro=<%=Server.UrlEncode(Request.QueryString["id"])%>&idUnidade=<%=Server.UrlEncode(Request.QueryString["idUnidade"])%>">
                                                                    <img src="../Styles/Icones/find.png" alt="Visualizar Serviço" border="0" /></a>&nbsp;
                                                            </td>
                                                            <td>
                                                                <%#DataBinder.Eval(Container.DataItem, "obj.TipoServico") %>
                                                            </td>
                                                            <td class="align-center">
                                                                <%#DataBinder.Eval(Container.DataItem, "obj.Usuario") %>
                                                            </td>

                                                            <td align="center">
                                                                <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "obj.PrevisaoMensalNumeroAtendidos")) %>
                                                            </td>
                                                            <%--<td align="center">
                                                                <%#((Decimal)DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual")).ToString("N2") %>
                                                            </td>--%>
                                                            <td align="center">
                                                                <div style="width: 100%; height: 22px; padding: 2px; font-size: 10px; text-align: center;">
                                                                    <asp:Repeater ID="rptCofinanciamentos" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "Cofinanciamentos") %>'>
                                                                        <HeaderTemplate>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Panel ID="painelConfinanciamentoExercicio1" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2018)? true : false %>'>
                                                                                <div style="width: 50%; float: left; padding-left: 2px; margin-top: 2px;">
                                                                                    <div style="width: 50%; float: left; padding-left: 2px;">
                                                                                        <div style="width: 90%; padding: 3px;">
                                                                                            <b>2018</b>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div style="width: 50%; height: 75%; padding: 2px; font-size: 10px; text-align: center;">
                                                                                        <div style="width: 50%; float: left; padding-left: 2px; padding: 3px;">
                                                                                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual") ) %>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </asp:Panel>

                                                                            <asp:Panel ID="painelConfinanciamentoExercicio2" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2019)? true : false %>'>
                                                                                <div style="width: 50%; float: left; padding-left: 2px; margin-top: 2px;">
                                                                                    <div style="width: 50%; float: left; padding-left: 2px;">
                                                                                        <div style="width: 90%; padding: 3px;">
                                                                                            <b>2019</b>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div style="width: 50%; height: 75%; padding: 2px; font-size: 10px; text-align: center;">
                                                                                        <div style="width: 50%; float: left; padding-left: 2px; padding: 3px;">
                                                                                            <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual") ) %>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </asp:Panel>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </div>
                                                            </td>
                                                            <%--<td align="center">
                                                                <%#((Decimal)DataBinder.Eval(Container.DataItem, "obj.PrevisaoOrcamentaria")).ToString("N2") %>
                                                            </td>--%>
                                                            <td class="align-center">
                                                                <%#((DateTime)DataBinder.Eval(Container.DataItem, "obj.DataEncerramentoServico")).ToShortDateString() %>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <div align="center" style="width: 100%;">
                                                    <br />
                                                    <b class="titulo">Não existe registro de serviços desativados</b>
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <asp:Button ID="btnVoltar" runat="server" Text="Voltar" SkinID="button-save" OnClick="btnVoltar_Click" />
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
