<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FTransferenciaRendaCofinanciamento.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.FTransferenciaRendaCofinanciamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form>
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                           <%-- <img src="../Styles/Icones/redeprotecao.gif" align="absMiddle" />--%>
                            Serviços socioassistenciais onde são atendidos beneficiários do  -
                                        <asp:Label ID="lblTransferenciaRenda" runat="server" />
                            <a href="#" runat="server" id="linkAlteracoesQuadro" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>&nbsp;
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Programas e Projetos">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <asp:ListView ID="lstRecursos" runat="server" OnItemCommand="lstRecursos_ItemCommand">
                                                <LayoutTemplate>
                                                    <table class="table border bordered" cellspacing="0"
                                                        cellpadding="0" border="0">
                                                        <thead>
                                                            <tr class="info">
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
                                                                <th width="70">Número de<br />
                                                                    atendidos
                                                                </th>
                                                                <th width="100">Nº de usuários<br />
                                                                    vinculados a<br />
                                                                    este programa
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
                                                    <asp:ListView ID="lstItems" runat="server" DataSource='<%#Eval("Items") %>' OnItemDataBound="lstItems_ItemDataBound">
                                                        <LayoutTemplate>
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td align="center">
                                                                    <%#MontarBotao((Seds.PMAS.QUADRIENAL.Entidades.Estruturas.ConsultaTransferenciaRendaCofinanciamentoInfo)Container.DataItem) %>
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
                                                                <td align="center">
                                                                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroAtendidos")) %>
                                                                </td>
                                                                <td align="center">
                                                                    <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroUsuarios")) %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <br />
                                                        <b class="titulo">Não existe registro de serviços associados a esta transferência de
                                        renda</b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <td colspan="3" align="center">
                                            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" PostBackUrl="~/BlocoIII/CProgramasProjetos.aspx" />
                                        </td>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </table>
            </form>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
