<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CServicosRecursosFinanceirosPublico.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoII.CServicosRecursosFinanceirosPublico" %>

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
                            </a>3.4 - Serviços socioassistenciais executados no 
                                        <asp:Label ID="lblLocalExecucao" runat="server" />
                            <span class="mif-organization icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Serviços">
                                <div class="grid">
                                    <asp:ListView ID="lstRecursos" runat="server" OnItemCommand="lstRecursos_ItemCommand"
                                        OnItemDataBound="lstRecursos_ItemDataBound">
                                        
                                        <LayoutTemplate>
                                            <table class="table striped border bordered" cellspacing="0"
                                                cellpadding="0" border="0">
                                         
                                                 <thead class="info">
                                                    <tr>
                                                        <th width="100">Visualizar/<br />
                                                            Editar
                                                        </th>
                                                        <th width="320">Tipo de serviço
                                                        </th>
                                                        <th width="220">Usuários
                                                        </th>
                                                        <th width="140">Capacidade mensal de pessoas/famílias atendidas</th>
                                                        <th width="150">Cofinanciamento estadual
                                                        </th>
                                                        <th width="50">Desativar
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
                                                <td class="heading" colspan="8">
                                                    <b>Proteção Social:</b>
                                                    <%#DataBinder.Eval(Container.DataItem, "Key") %>
                                                </td>
                                            </tr>
                                            <asp:ListView ID="lstItems" runat="server" DataSource='<%#Eval("Items") %>' OnItemDataBound="lstItems_ItemDataBound">
                                                <LayoutTemplate>
                                                    <tr id="itemPlaceholder" runat="server">
                                                    </tr>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td align="center">
                                                            <a href="VServicoRecursoFinanceiroPublico.aspx?id=<%# Server.UrlEncode(Genericos.clsCrypto.Encrypt(DataBinder.Eval(Container.DataItem, "Id").ToString())) %>&idLocal=<%=Server.UrlEncode(Request.QueryString["idLocal"])%>&idUnidade=<%=Server.UrlEncode(Request.QueryString["idUnidade"])%>">
                                                                <img src="../Styles/Icones/find.png" alt="Visualizar Serviço" border="0" /></a>&nbsp;
                                                                <%#MontarBotaoEditar(DataBinder.Eval(Container.DataItem, "Id").ToString()) %>
                                                        </td>
                                                        <td>
                                                            <%#DataBinder.Eval(Container.DataItem, "obj.TipoServico") %>
                                                        </td>
                                                        <td class="align-center">
                                                            <%#DataBinder.Eval(Container.DataItem, "obj.Usuario") %>
                                                        </td>
                                                        <td align="center">
     
                                                               <div style="width: 100%; height: 22px; padding: 2px; font-size: 10px; text-align: center;">
                                                                <asp:Repeater ID="rptCapacidade" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "Cofinanciamentos") %>'>
                                                                    <HeaderTemplate>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Panel ID="painelCapacidadeExercicio1" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2022)? true : false %>'>
                                                                            <div style="width: 100%; float: left; padding: 4px; margin-top: 2px; clear:both;">
                                                                                <div style="width: 50%; float: left; padding-left: 2px; ">
                                                                                    <div style="width: 90%; padding: 3px; border-bottom:1px solid #b5d4e4;">
                                                                                        <b>2022</b>
                                                                                    </div>
                                                                                </div>
                                                                                <div style="width: 50%; float:left; position:relative; height: 75%; padding: -2px; font-size: 10px; text-align: center;">
                                                                                    <div style="width: 50%; float: left; padding-left: 2px; padding: 3px;">
                                                                                       <%# DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimento")  %>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>

                                                                        <asp:Panel ID="painelCapacidadeExercicio2" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2023)? true : false %>'>
                                                                            <div style="width: 100%; float: left; padding: 4px; margin-top: 2px; clear:both;">
                                                                                <div style="width: 50%; float: left; padding-left: 2px;">
                                                                                    <div style="width: 90%; padding: 3px; border-bottom:1px solid #b5d4e4;">
                                                                                        <b>2023</b>
                                                                                    </div>
                                                                                </div>
                                                                                <div style="width: 50%; float:left; position:relative; height: 75%; padding: -2px; font-size: 10px; text-align: center;">
                                                                                    <div style="width: 50%; float: left; padding-left: 2px; padding: 3px;">
                                                                                        <%# DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimento")  %>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>

                                                                          <asp:Panel ID="painelCapacidadeExercicio3" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2024)? true : false %>'>
                                                                            <div style="width: 100%; float: left; padding: 4px; margin-top: 2px; clear:both;">
                                                                                <div style="width: 50%; float: left; padding-left: 2px;">
                                                                                    <div style="width: 90%; padding: 3px; border-bottom:1px solid #b5d4e4;">
                                                                                        <b>2024</b>
                                                                                    </div>
                                                                                </div>
                                                                                <div style="width: 50%; float:left; position:relative; height: 75%; padding: -2px; font-size: 10px; text-align: center;">
                                                                                    <div style="width: 50%; float: left; padding-left: 2px; padding: 3px;">
                                                                                        <%# DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimento") %>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>

                                                                          <asp:Panel ID="painelCapacidadeExercicio4" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2025)? true : false %>'>
                                                                            <div style="width: 100%; float: left; padding: 4px; margin-top: 2px; clear:both;">
                                                                                <div style="width: 50%; float: left; padding-left: 2px;">
                                                                                    <div style="width: 90%; padding: 3px; border-bottom:1px solid #b5d4e4;">
                                                                                        <b>2025</b>
                                                                                    </div>
                                                                                </div>
                                                                                <div style="width: 50%; float:left; position:relative; height: 75%; padding: -2px; font-size: 10px; text-align: center;">
                                                                                    <div style="width: 50%; float: left; padding-left: 2px; padding: 3px;">
                                                                                        <%# DataBinder.Eval(Container.DataItem, "CapacidadeMensalAtendimento") %>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>

                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>


                                                        </td>

                                                        <td align="center">
                                                            <div style="width: 100%; height: 22px; padding: 2px; font-size: 10px; text-align: center;">
                                                                <asp:Repeater ID="rptCofinanciamentos" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "Cofinanciamentos") %>'>
                                                                    <HeaderTemplate>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Panel ID="painelConfinanciamentoExercicio1" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2022)? true : false %>'>
                                                                            <div style="width: 100%; float: left; padding: 4px; margin-top: 2px; clear:both;">
                                                                                <div style="width: 50%; float: left; padding-left: 2px; ">
                                                                                    <div style="width: 90%; padding: 3px; border-bottom:1px solid #b5d4e4;">
                                                                                        <b>2022</b>
                                                                                    </div>
                                                                                </div>
                                                                                <div style="width: 50%; float:left; position:relative; height: 75%; padding: -2px; font-size: 10px; text-align: center;">
                                                                                    <div style="width: 50%; float: left; padding-left: 2px; padding: 3px;">
                                                                                        <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual") ) %>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>

                                                                        <asp:Panel ID="painelConfinanciamentoExercicio2" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2023)? true : false %>'>
                                                                            <div style="width: 100%; float: left; padding: 4px; margin-top: 2px; clear:both;">
                                                                                <div style="width: 50%; float: left; padding-left: 2px;">
                                                                                    <div style="width: 90%; padding: 3px; border-bottom:1px solid #b5d4e4;">
                                                                                        <b>2023</b>
                                                                                    </div>
                                                                                </div>
                                                                                <div style="width: 50%; float:left; position:relative; height: 75%; padding: -2px; font-size: 10px; text-align: center;">
                                                                                    <div style="width: 50%; float: left; padding-left: 2px; padding: 3px;">
                                                                                        <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual") ) %>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>

                                                                          <asp:Panel ID="painelConfinanciamentoExercicio3" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2024)? true : false %>'>
                                                                            <div style="width: 100%; float: left; padding: 4px; margin-top: 2px; clear:both;">
                                                                                <div style="width: 50%; float: left; padding-left: 2px;">
                                                                                    <div style="width: 90%; padding: 3px; border-bottom:1px solid #b5d4e4;">
                                                                                        <b>2024</b>
                                                                                    </div>
                                                                                </div>
                                                                                <div style="width: 50%; float:left; position:relative; height: 75%; padding: -2px; font-size: 10px; text-align: center;">
                                                                                    <div style="width: 50%; float: left; padding-left: 2px; padding: 3px;">
                                                                                        <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual") ) %>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>

                                                                          <asp:Panel ID="painelConfinanciamentoExercicio4" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2025)? true : false %>'>
                                                                            <div style="width: 100%; float: left; padding: 4px; margin-top: 2px; clear:both;">
                                                                                <div style="width: 50%; float: left; padding-left: 2px;">
                                                                                    <div style="width: 90%; padding: 3px; border-bottom:1px solid #b5d4e4;">
                                                                                        <b>2025</b>
                                                                                    </div>
                                                                                </div>
                                                                                <div style="width: 50%; float:left; position:relative; height: 75%; padding: -2px; font-size: 10px; text-align: center;">
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

                                                        <td class="align-center">
                                                            <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                CommandName="Excluir" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>'
                                                                CausesValidation="false" OnClientClick="return confirm('Deseja realmente desativar este serviço?');" />
                                                            <%-- <%#MontarBotaoExcluir((Seds.PMAS.QUADRIENAL.Entidades.Estruturas.ConsultaServicosRecursosFinanceirosPublicoInfo)Container.DataItem) %>--%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <div align="center" style="width: 100%;">
                                                <br />
                                                <b class="titulo">Não existe registro de serviços e recursos financeiros</b>
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:Button ID="btnAdicionarServico" runat="server" Text="Adicionar Serviços e Recursos Financeiros"
                                                SkinID="button-save" OnClick="btnAdicionarServico_Click" />&nbsp;
                                            <asp:Button ID="btnServicosDesativados" runat="server" Text="Serviços Desativados" SkinID="button-save" OnClick="btnServicosDesativados_Click"></asp:Button>
                                            &nbsp;<asp:Button ID="btnVoltar" runat="server" Text="Voltar" SkinID="button-save" OnClick="btnVoltar_Click" />
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
