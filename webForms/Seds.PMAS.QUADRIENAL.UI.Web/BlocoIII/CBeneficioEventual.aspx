<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CBeneficioEventual.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.CBeneficioEventual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmBeneficioEventual">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            3.24 - Benefícios Eventuais oferecidos pelo Município
                            <a href="#" runat="server" id="linkAlteracoesQuadro52" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado</a>
                            <span class="mif-organization icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Benefícios Eventuais">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <asp:ListView ID="lst" runat="server" DataKeyNames="Id" OnItemCommand="lst_ItemCommand"
                                                OnItemDataBound="lst_ItemDataBound">
                                                <LayoutTemplate>
                                                    <table class="table border bordered" cellspacing="0"
                                                        cellpadding="0" border="0">
                                                        <thead class="info">
                                                            <tr>
                                                                <th colspan="9" style="height: 20px;">
                                                                    <span>Benefícios Eventuais</span>
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <th width="60" rowspan="2">Visualizar
                                                                </th>
                                                                <th width="330" rowspan="2">Nome
                                                                </th>
                                                                <th width="100" rowspan="2">Município oferta<br />
                                                                    este benefício?
                                                                </th>
                                                                <th width="240" colspan="3" style="height: 22px; padding-top: 3px;"
                                                                    valign="top">Integração com serviços
                                                                </th>
                                                                <th width="150" rowspan="2">Previsão<br />
                                                                    orçamentária
                                                                </th>
                                                                <th width="50" rowspan="2">Excluir
                                                                </th>
                                                            </tr>
                                                            <tr class="ui-jqgrid-labels">
                                                                <th width="90">Visualizar/<br />
                                                                    Editar
                                                                </th>
                                                                <th width="80">Total de<br />
                                                                    Unidades
                                                                </th>
                                                                <th width="80">Total de<br />
                                                                    serviços
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
                                                        <td align="center" style="height: 22px;">
                                                            <asp:ImageButton runat="server" ID="btnVis" ToolTip="Visualizar Benefício" ImageUrl="~/Styles/Icones/find.png"
                                                                CommandName="Visualizar" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "obj.IdTipoBeneficioEventual") %>' />
                                                        </td>
                                                        <td class="align-left">
                                                            <%#DataBinder.Eval(Container.DataItem, "obj.Nome") %>
                                                        </td>
                                                        <td class="align-center">
                                                            <%#((Int32)DataBinder.Eval(Container.DataItem, "Id"))  > 0 ? "Sim" : "Não" %>
                                                        </td>
                                                        <td id="tdNao" align="center" runat="server" visible="false" colspan="3">-------------
                                                        </td>
                                                        <td align="center" id="tdVisualizarServicos" runat="server">
                                                            <asp:ImageButton runat="server" ID="btnVisServicos" ToolTip="Visualizar Serviços Associados"
                                                                ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' />
                                                        </td>
                                                        <td class="align-center" id="tdUnidades" runat="server">
                                                            <%#DataBinder.Eval(Container.DataItem, "obj.TotalUnidades") %>
                                                        </td>
                                                        <td class="align-center" id="tdServicos" runat="server">
                                                            <%#DataBinder.Eval(Container.DataItem, "obj.TotalServicos") %>
                                                        </td>
                                                        <td class="align-center">
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
                                                            <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png" CommandName="Excluir" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "obj.IdTipoBeneficioEventual") %>'
                                                                CausesValidation="false" OnClientClick="return confirm('ATENÇÃO!!!\nExcluir este benefício fará com que todas as associações registradas nos serviços em relação a ele sejam apagadas.\nCaso queira apenas modificar alguma informação registrada neste benefício, clique no ícone da primeira coluna à esquerda para editar os registros.\n\nDeseja realmente excluir este Benefício?');" />&nbsp;
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%;">
                                                        <b class="titulo">Não existem registro de Benefícios Eventuais</b>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
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
                        <a href="CProgramasProjetos.aspx">
                            <span class="mif-arrow-left"></span>
                            Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="CBeneficiosContinuados.aspx">Próximo
                           <span class="mif-arrow-right"></span></a>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
