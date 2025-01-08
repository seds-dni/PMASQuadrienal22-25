<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FProgramaProjetoCofinanciamento.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.FProgramaProjetoCofinanciamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmUnidadesPublicas">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            Serviços socioassistenciais onde são atendidos beneficiários do 
                             <asp:Label ID="lblProgramaProjeto" runat="server" />
                            <a href="#" runat="server" id="linkAlteracoesQuadro42" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>&nbsp;
                            <span class="mif-organization icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Serviços">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <asp:ListView ID="lstRecursos" runat="server" OnItemCommand="lstRecursos_ItemCommand">
                                                <LayoutTemplate>
                                                    <table class="table border bordered" cellspacing="0"
                                                        cellpadding="0" border="0" width="100%">
                                                        <thead class="info">
                                                            <tr>
                                                                <th width="50">Visualizar
                                                                </th>
                                                                <th width="80">Tipo de<br />
                                                                    Rede
                                                                </th>
                                                                <th width="220">Unidade/Organização
                                                                </th>
                                                                <th width="320">Tipo de serviço
                                                                </th>
                                                                <th width="180">Usuários
                                                                </th>
                                                                <th width="100">Nº de usuários<br />
                                                                    vinculados a<br />
                                                                    este programa
                                                                </th>
                                                                <%--   <th  width="50">Excluir
                                            </th>--%>
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
                                                        <td colspan="7">
                                                            <b>Proteção Social:</b>
                                                            <%#DataBinder.Eval(Container.DataItem, "Key") %>
                                                        </td>
                                                    </tr>
                                                    <asp:ListView ID="lstItems" runat="server" DataSource='<%#Eval("Items") %>'>
                                                        <%--      OnItemDataBound="lstItems_ItemDataBound">--%>
                                                        <LayoutTemplate>
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td align="center">
                                                                    <%#MontarBotao((Seds.PMAS.QUADRIENAL.Entidades.Estruturas.ConsultaProgramaProjetoCofinanciamentoInfo)Container.DataItem) %>
                                                                </td>
                                                                <td align="center">
                                                                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                                                                </td>
                                                                <td>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Unidade") %>
                                                                </td>
                                                                <td>
                                                                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                                                                </td>
                                                                <td class="align-center">
                                                                    <%#DataBinder.Eval(Container.DataItem, "Usuario") %>
                                                                </td>
                                                                <td class="align-center">
                                                                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroUsuarios")) %>
                                                                </td>
                                                                <%--       <td class="align-center">
                                                <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                    CommandName="Excluir" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>'
                                                    CausesValidation="false" OnClientClick="return confirm('Deseja realmente remover o serviço?');" />
                                            </td>--%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <br />
                                                        <b class="titulo">Não existe registro de programas / benefícios associados a este serviço</b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:ListView ID="lstRecursosAmigoIdoso" runat="server" OnItemCommand="lstRecursos_ItemCommand">
                                                <LayoutTemplate>
                                                    <table class="table border bordered" cellspacing="0"
                                                        cellpadding="0" border="0">
                                                        <thead class="info">
                                                            <tr>
                                                                <th width="50">Visualizar
                                                                </th>
                                                                <th width="80">Tipo de<br />
                                                                    Unidade
                                                                </th>
                                                                <th width="220">Unidade
                                                                </th>
                                                                <th width="320">Tipo de serviço
                                                                </th>
                                                                <th width="180">Usuários
                                                                </th>
                                                                <th width="100">Nº de usuários<br />
                                                                    vinculados a<br />
                                                                    este programa
                                                                </th>
                                                                <th width="50">Excluir
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
                                                        <td colspan="7">
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
                                                                    <%#MontarBotao((Seds.PMAS.QUADRIENAL.Entidades.Estruturas.ConsultaProgramaProjetoCofinanciamentoInfo)Container.DataItem) %>
                                                                </td>
                                                                <td align="center">
                                                                    <%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %>
                                                                </td>
                                                                <td>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Unidade") %>
                                                                </td>
                                                                <td>
                                                                    <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                                                                </td>
                                                                <td class="align-center">
                                                                    <%#DataBinder.Eval(Container.DataItem, "Usuario") %>
                                                                </td>
                                                                <td class="align-center">
                                                                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroUsuarios")) %>
                                                                </td>
                                                                <td class="align-center">
                                                                    <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                        CommandName="Excluir" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>'
                                                                        CausesValidation="false" OnClientClick="return confirm('Deseja realmente remover o serviço?');" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <br />
                                                        <b class="titulo">Não existe registro de serviços associados a este programa</b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <div class="row" align="center">
                                        <div class="cell">
                                               <asp:Button ID="btnVoltar" runat="server"  Text="Voltar"  PostBackUrl="~/BlocoIII/CProgramasProjetos.aspx"/>
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
