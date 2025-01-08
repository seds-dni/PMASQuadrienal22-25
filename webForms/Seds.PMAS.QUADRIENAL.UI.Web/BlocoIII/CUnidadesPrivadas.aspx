<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CUnidadesPrivadas.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.CUnidadesPrivadas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmUnidadePrivada">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            3.11 - Organizações da Sociedade Civil
                            <span class="mif-home icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="rede indireta">
                                <div class="grid">
                                    <div class="row cells5">
                                        <div class="cell colspan4">
                                            Cadastrar as organizações da sociedade civil (de atendimento, assessoramento e/ou de defesa e garantia de direitos) da rede indireta de proteção social:
                                        </div>
                                        <div class="cell">
                                            <a href="#" runat="server" id="linkAlteracoesQuadro36" visible="false">
                                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                            </a>
                                        </div>
                                    </div>
                                    <div class="row cells2">
                                        <div class="cell">
                                            Localizar nome ou CNPJ da Organização:
                                            <div class="input-control text mid-size">
                                                <asp:TextBox runat="server" ID="txtLocalizar"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <asp:Button ID="btnLocalizar" runat="server" Text="Localizar" SkinID="button-find"
                                                OnClick="btnLocalizar_Click" />
                                            <asp:Button ID="btnLimparBusca" runat="server" Text="Limpar Busca" OnClick="btnLimparBusca_Click" />
                                        </div>
                                    </div>
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
                                                                <th width="70">Visualizar/<br />
                                                                    Editar
                                                                </th>
                                                                <th width="130">CNPJ
                                                                </th>
                                                                <th width="300">Razão social
                                                                </th>
                                                                <th width="100">Código PMAS
                                                                </th>

                                                                <th width="200">Forma de atuação
                                                                </th>
                                                                <th width="100">Total de locais<br />
                                                                    de execução
                                                                </th>
                                                                <th width="100">Valor do<br />
                                                                    cofinanciamento<br />
                                                                    estadual
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
                                                    <tr>
                                                        <td style="height: 22px;">
                                                            <asp:Label ID="lblSequencia" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:ImageButton runat="server" ID="btnVisUnidade" ToolTip="Visualizar Unidade" ImageUrl="~/Styles/Icones/find.png"
                                                                CommandName="Visualizar" />
                                                        </td>
                                                        <td align="center">
                                                            <%#Convert.ToDouble(DataBinder.Eval(Container.DataItem, "obj.CNPJ")).ToString(@"00\.000\.000\/0000\-00") %>
                                                        </td>
                                                        <td align="left">
                                                            <%#DataBinder.Eval(Container.DataItem, "obj.RazaoSocial") %>
                                                        </td>

                                                        <td align="center">
                                                            <%#DataBinder.Eval(Container.DataItem, "Id") %>
                                                        </td>
                                                        <td align="center">
                                                            <%#DataBinder.Eval(Container.DataItem, "obj.FormaAtuacao") %></td>
                                                        <td class="align-center">
                                                            <%#DataBinder.Eval(Container.DataItem, "obj.TotalLocais") %>
                                                        </td>
                                                        <td align="center">
                                                              <div style="width: 100%; height: 22px; padding: 2px; font-size: 10px; text-align: center;">
                                                                <asp:Repeater ID="rptCofinanciamentos" runat="server" DataSource='<%#DataBinder.Eval(Container.DataItem, "Cofinanciamentos") %>'>
                                                                    <HeaderTemplate>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Panel ID="painelConfinanciamentoExercicio1" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2022)? true : false %>'>
                                                                            <div style="width: 100%; float: left; padding: 4px; margin-top: 2px; clear: both;">
                                                                                <div style="width: 50%; float: left; padding-left: 2px;">
                                                                                    <div style="width: 90%; padding: 3px; border-bottom:1px solid #b5d4e4;">
                                                                                        <b>2022</b>
                                                                                    </div>
                                                                                </div>
                                                                                <div style="width: 50%; float: left; position: relative; height: 75%; padding: -2px; font-size: 10px; text-align: center;">
                                                                                    <div style="width: 50%; float: left; padding-left: 2px; padding: 3px;">
                                                                                        <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual") ) %>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>

                                                                        <asp:Panel ID="painelConfinanciamentoExercicio2" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2023)? true : false %>'>
                                                                            <div style="width: 100%; float: left; padding: 4px; margin-top: 2px; clear: both;">
                                                                                <div style="width: 50%; float: left; padding-left: 2px;">
                                                                                    <div style="width: 90%; padding: 3px; border-bottom:1px solid #b5d4e4;">
                                                                                        <b>2023</b>
                                                                                    </div>
                                                                                </div>
                                                                                <div style="width: 50%; float: left; position: relative; height: 75%; padding: -2px; font-size: 10px; text-align: center;">
                                                                                    <div style="width: 50%; float: left; padding-left: 2px; padding: 3px;">
                                                                                        <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual") ) %>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>

                                                                        <asp:Panel ID="painelConfinanciamentoExercicio3" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2024)? true : false %>'>
                                                                            <div style="width: 100%; float: left; padding: 4px; margin-top: 2px; clear: both;">
                                                                                <div style="width: 50%; float: left; padding-left: 2px;">
                                                                                    <div style="width: 90%; padding: 3px; border-bottom:1px solid #b5d4e4;">
                                                                                        <b>2024</b>
                                                                                    </div>
                                                                                </div>
                                                                                <div style="width: 50%; float: left; position: relative; height: 75%; padding: -2px; font-size: 10px; text-align: center;">
                                                                                    <div style="width: 50%; float: left; padding-left: 2px; padding: 3px;">
                                                                                        <%#String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual") ) %>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="painelConfinanciamentoExercicio4" runat="server" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "Exercicio") == 2025)? true : false %>'>
                                                                            <div style="width: 100%; float: left; padding: 4px; margin-top: 2px; clear: both;">
                                                                                <div style="width: 50%; float: left; padding-left: 2px;">
                                                                                    <div style="width: 90%; padding: 3px; border-bottom:1px solid #b5d4e4;">
                                                                                        <b>2025</b>
                                                                                    </div>
                                                                                </div>
                                                                                <div style="width: 50%; float: left; position: relative; height: 75%; padding: -2px; font-size: 10px; text-align: center;">
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
                                                                CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('Deseja realmente desativar esta Organização?');" />&nbsp;
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <b class="titulo">Não existe registro de unidades da rede indireta</b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:Button ID="btnIncluir" runat="server" Text="Incluir outra Organização" SkinID="button-save"
                                                CausesValidation="False" Width="200px" OnClick="btnIncluir_Click"></asp:Button>
                                            &nbsp;
                                              <asp:Button ID="btnUnidadeDesativas" runat="server" Text="Organizações desativadas" SkinID="button-save"
                                                  CausesValidation="False" Width="200px" OnClick="btnUnidadeDesativas_Click"></asp:Button>
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
                        <a href="CUnidadesPublicas.aspx">
                            <span class="mif-arrow-left"></span>
                            Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">&nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
