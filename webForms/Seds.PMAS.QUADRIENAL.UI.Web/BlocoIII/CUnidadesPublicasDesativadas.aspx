<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CUnidadesPublicasDesativadas.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.CUnidadesPublicasDesativadas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmUnidadesPublicas">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                           Unidades da Rede Direta Desativadas
                            <span class="mif-organization icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Rede Direta">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <asp:ListView ID="lstUnidades" runat="server" DataKeyNames="Id" OnItemDataBound="lstUnidades_ItemDataBound"
                                                OnItemCommand="lstUnidades_ItemCommand">
                                                <LayoutTemplate>
                                                    <table class="table striped border bordered" cellspacing="0"
                                                        cellpadding="0" border="0">
                                                        <thead class="info">
                                                            <tr>
                                                                <th width="20" style="height: 22px;"></th>
                                                                <th width="50">Visualizar/<br />
                                                                    Editar
                                                                </th>
                                                                <th width="150">CNPJ
                                                                </th>
                                                                <th width="100">Código no PMAS
                                                                </th>
                                                                <th width="500">Razão social
                                                                </th>
                                                                <%--Bruno V.--%>
                                                                <th width="100">Quantidade<br />
                                                                    de CRAS
                                                                </th>
                                                                <%--Bruno V.--%>
                                                                <th width="100">Quantidade<br />
                                                                    de CREAS
                                                                </th>
                                                                <%--Bruno V.--%>
                                                                <th width="100">Quantidade<br />
                                                                    de Centros-POP
                                                                </th>
                                                                <th width="100">Outros locais<br />
                                                                    de execução
                                                                </th>
                                                                <th width="130">Valor do<br />
                                                                    cofinanciamento<br />
                                                                    estadual
                                                                </th>
                                                                <th width="50">Data da Desativação
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
                                                    <tr>
                                                        <td class="info" style="height: 22px;">
                                                            <asp:Label ID="lblSequencia" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:ImageButton runat="server" ID="btnVisUnidade" ToolTip="Visualizar Unidade" ImageUrl="~/Styles/Icones/find.png"
                                                                CommandName="Visualizar" />
                                                        </td>
                                                        <td align="center">
                                                            <%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "CNPJ")).ToString(@"00\.000\.000\/0000\-00") %>
                                                        </td>
                                                        <th width="120"><%#DataBinder.Eval(Container.DataItem, "Id") %>
                                                        </th>
                                                        <td class="align-left">
                                                            <%#DataBinder.Eval(Container.DataItem, "RazaoSocial") %>
                                                        </td>
                                                        <td class="align-center">
                                                            <%#DataBinder.Eval(Container.DataItem, "TotalCRASDesativados") %>
                                                        </td>
                                                        <td class="align-center">
                                                            <%#DataBinder.Eval(Container.DataItem, "TotalCREASDesativados") %>
                                                        </td>
                                                        <td class="align-center">
                                                            <%#DataBinder.Eval(Container.DataItem, "TotalCentroPOPDesativados") %>
                                                        </td>
                                                        <td class="align-center">
                                                            <%#DataBinder.Eval(Container.DataItem, "TotalLocaisDesativados") %>
                                                        </td>
                                                        <td class="align-center">
                                                            <%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual")) %>
                                                        </td>
                                                          <td class="align-center">
                                                               <%#(DataBinder.Eval(Container.DataItem, "DataDesativacao") == null ? (DateTime)DataBinder.Eval(Container.DataItem, "DataRegistroLog") : (DateTime)DataBinder.Eval(Container.DataItem, "DataDesativacao")).ToShortDateString() %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <b class="titulo">Não existe registro de unidades públicas desativadas</b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" SkinID="button-save"
                                                CausesValidation="False" Width="80px" OnClick="btnVoltar_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
            <table width="100%" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="FAnaliseDiagnostica.aspx">
                            <span class="mif-arrow-left"></span>
                            Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="CUnidadesPrivadas.aspx">Próximo
                               <span class="mif-arrow-right"></span></a>
                    </td>
                </tr>
            </table>

        </ContentTemplate>


    </asp:UpdatePanel>
</asp:Content>
