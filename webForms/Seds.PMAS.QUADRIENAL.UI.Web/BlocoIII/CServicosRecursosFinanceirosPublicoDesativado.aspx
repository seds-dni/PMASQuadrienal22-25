<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CServicosRecursosFinanceirosPublicoDesativado.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoII.CServicosRecursosFinanceirosPublicoDesativado" %>

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
                            <a href="#" runat="server" id="linkAlteracoesQuadro19" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>3.4 - Serviços socioassistenciais desativados no 
                                        <asp:Label ID="lblLocalExecucao" runat="server" />
                            <span class="mif-organization icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Serviços">
                                <div class="grid">
                                    <asp:ListView ID="lstRecursos" runat="server" OnItemCommand="lstRecursos_ItemCommand">
                                        <LayoutTemplate>
                                            <table class="table striped border bordered" cellspacing="0"
                                                cellpadding="0" border="0">
                                                <thead class="info">
                                                    <tr>
                                                        <th width="100">Visualizar
                                                        </th>
                                                        <th width="320">Tipo de serviço
                                                        </th>
                                                        <th width="220">Usuários
                                                        </th>
                                                        <th width="140">Capacidade mensal de pessoas/famílias atendidas</th>
                                                        <th width="150">Cofinanciamento estadual
                                                        </th>
                                                        <th width="150">Data de Desativação
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
                                            <tr class="frame" style="height: 22px;">
                                                <td class="heading" colspan="7">
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
                                                            <a href="VServicoRecursoFinanceiroPublico.aspx?id=<%# Server.UrlEncode(Genericos.clsCrypto.Encrypt(DataBinder.Eval(Container.DataItem, "Id").ToString())) %>&idLocal=<%=Server.UrlEncode(Request.QueryString["idLocal"])%>&idUnidade=<%=Server.UrlEncode(Request.QueryString["idUnidade"])%>">
                                                                <img src="../Styles/Icones/find.png" alt="Visualizar Serviço" border="0" /></a>&nbsp;
                                  <%--              <%#MontarBotaoEditar((Seds.PMAS.QUADRIENAL.Entidades.Estruturas.ConsultaServicosRecursosFinanceirosPublicoInfo)Container.DataItem) %>--%>
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
                                                                                <div style="width: 50%; float: left; padding-left: 2px; ">
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

                                                        <td align="center">
                                                            <%# DataBinder.Eval(Container.DataItem, "obj.DataExclusaoServico") != null ? ((DateTime)DataBinder.Eval(Container.DataItem, "obj.DataExclusaoServico")).ToShortDateString() : "" %>
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
                                    <div class="row">
                                        <div class="cell">
                                            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" SkinID="button-save" OnClick="btnVoltar_Click" />
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
