<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FBeneficioEventualServicos.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.FBeneficioEventualServicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmBeneficioEventual">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            Integração com a Rede Socioassistencial
                            <a href="#" runat="server" id="linkAlteracoesQuadro78" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado</a>
                            <span class="mif-organization icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Benefícios Continuados">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <asp:ListView ID="lstRecursos" runat="server" OnItemCommand="lstRecursos_ItemCommand">
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
                                                                <th width="300">Tipo de serviço
                                                                </th>
                                                                <th width="120">Usuários
                                                                </th>
                                                                <th width="250px">
                                                                    <div style="clear: both; width: 250px;">Número de atendidos</div>
                                                                    <br />
                                                                    <div style="width: 250px">
                                                                        <div style="width: 20%; position: relative; float: left; text-align: center;">2021</div>
                                                                        <div style="width: 20%; position: relative; float: left; text-align: center; border-left: 1px solid black;">2022</div>
                                                                        <div style="width: 20%; position: relative; float: left; text-align: center; border-left: 1px solid black;">2023</div>
                                                                        <div style="width: 20%; position: relative; float: left; text-align: center; border-left: 1px solid black;">2024</div>
                                                                    </div>
                                                                </th>

                                                                <th width="120">Usuários que
                                                                    recebem
                                                                    benefícios eventuais
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
                                                        <td colspan="10">
                                                            <b>Proteção Social:</b>
                                                            <%#DataBinder.Eval(Container.DataItem, "Key") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <asp:ListView ID="lstItems" runat="server" DataSource='<%#Eval("Items") %>' OnItemDataBound="lstItems_ItemDataBound">
                                                            <LayoutTemplate>
                                                                <tr id="itemPlaceholder" runat="server">
                                                                </tr>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>

                                                                    <td align="center">
                                                                        <%#MontarBotao((Seds.PMAS.QUADRIENAL.Entidades.Estruturas.ConsultaPrefeituraBeneficioEventualRecursoFinanceiroInfo)Container.DataItem) %>
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
                                                                        <div style="width: 250px">
                                                                            <asp:Repeater runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "NumerosAtendidos") %>'>
                                                                                <ItemTemplate>
                                                                                    <div style="width: 20%; position: relative; float: left; text-align: center; border-right: <%# Container.ItemIndex == 4 ? "0px solid black" : "1px solid black" %> ">
                                                                                        <%#String.Format("{0:0,0}", DataBinder.Eval(Container.DataItem, "Value"))%>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>

                                                                    </td>


                                                                    <td align="center">
                                                                        <%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroBeneficiarios")) %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <br />
                                                        <b class="titulo">Não existe registro de serviços associados a este benefício</b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell" align="center">
                                            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" PostBackUrl="~/BlocoIII/CBeneficioEventual.aspx" />
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
