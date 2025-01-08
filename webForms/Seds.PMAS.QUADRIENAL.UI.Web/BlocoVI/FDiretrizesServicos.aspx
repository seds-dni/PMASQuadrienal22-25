<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FDiretrizesServicos.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoVI.FDiretrizesServicos" %>

<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmUnidadesPublicas">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            Serviços socioassistenciais
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
                                            <asp:ListView ID="lstRecursos" runat="server">
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
                                                        <LayoutTemplate>
                                                            <tr id="itemPlaceholder" runat="server">
                                                            </tr>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td align="center">
                                                                    <%#MontarBotao((Seds.PMAS.QUADRIENAL.Entidades.Estruturas.ConsultarServicosDiretrizesInfo)Container.DataItem) %>
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
                                    <div class="row" align="center">
                                        <div class="cell">
                                               <asp:Button ID="btnVoltar" runat="server"  Text="Voltar"  PostBackUrl="~/BlocoVI/FDiretrizes.aspx"/>
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

     