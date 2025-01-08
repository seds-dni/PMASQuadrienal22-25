<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FUnidadePublica.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.FUnidadePublica" %>

<%@ Register Src="../Controles/cnpj.ascx" TagName="cnpj" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmUnidadesPublicas">
                <div class="accordion" data-no-close="true">
                    <div class="frame active">
                        <div class="heading">
                            &nbsp;3.2 - Locais onde são desenvolvidos os serviços da rede direta
                            <span class="mif-organization icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Unidades Públicas">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <b>Código da Unidade:</b><br />
                                            <asp:Label ID="lblCodigoUnidade" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row cells4">
                                        <div class="cell colspan2">
                                            <b>Nome da Unidade Pública:</b><br />
                                            <div class="input-control text full-size">
                                                <asp:TextBox ID="txtNome" runat="server" MaxLength="120"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>CNPJ:</b><br />
                                            <div class="input-control text">
                                                <uc1:cnpj ID="txtCNPJ" runat="server" />
                                            </div>
                                        </div>
                                        <div class="cell" align="right">
                                            <a href="#" runat="server" id="linkAlteracoesQuadro17" visible="false">
                                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                            </a>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:Button ID="btnSalvar" runat="server" SkinID="button-save" Text="Salvar" Width="89px"
                                                OnClick="btnSalvar_Click"></asp:Button>
                                            <asp:Button ID="btnLocalizarLocal" runat="server" SkinID="button-save" Text="Pesquisar local de execução" Width="209px"
                                                OnClick="btnLocalizarLocal_Click"></asp:Button>
                                            <asp:Button ID="btnVoltar0" runat="server"
                                                PostBackUrl="~/BlocoIII/CUnidadesPublicas.aspx" Text="Voltar" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                                width="100%" align="center" border="0">
                                                <tr>
                                                    <td class="ui-state-highlight titulo" style="padding: 2px 10px 2px 10px;">
                                                        <img src="../Styles/Icones/messagebox_warning.png" align="absMiddle" /><b style='color: #000000 !important'>Verifique
                                                    as inconsistências:</b>
                                                        <br />
                                                        <br />
                                                        <asp:Label ID="lblInconsistencias" ForeColor="Red" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row" runat="server" id="trLocalizar" visible="false">
                                        <fieldset>
                                            <legend>Pesquisar local de execução</legend>
                                            <div class="row cells2">
                                                <div class="cell">
                                                    Nome (ou parte do nome):
                                        <div class="input-control text full-size">
                                            <asp:TextBox runat="server" ID="txtLocalizar"></asp:TextBox>
                                        </div>
                                                </div>
                                                <div class="cell bottom">
                                                    <br />
                                                    <asp:Button ID="btnLocalizar" runat="server" Text="Pesquisar" SkinID="button-find"
                                                        OnClick="btnLocalizar_Click" />
                                                    <asp:Button ID="btnLimparBusca" runat="server" Text="Limpar busca" OnClick="btnLimparBusca_Click" />
                                                </div>
                                            </div>
                                        </fieldset>

                                    </div>
                                    <div class="row" runat="server" id="trLocaisExecucao">
                                        <div class="frame active">
                                            <div class="subheading">
                                                3.2.a - Locais de execução dos serviços da rede direta&nbsp;
                                                  <a href="#" runat="server" id="linkAlteracoesQuadroLocalPublico" visible="false">
                                                      <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                                  </a>
                                            </div>
                                            <div class="content">
                                                <div class="formInput">
                                                    <div class="subgrid">
                                                        <asp:ListView ID="lstLocais" runat="server" DataKeyNames="Id" OnItemCommand="lstLocais_ItemCommand"
                                                            OnItemDataBound="lstLocais_ItemDataBound">
                                                            <LayoutTemplate>
                                                                <table class="table striped border bordered" cellspacing="0"
                                                                    cellpadding="0" border="0">
                                                                    <thead class="info">
                                                                        <tr>
                                                                            <th width="40" style="height: 22px;"
                                                                                rowspan="2"></th>
                                                                            <th width="80" rowspan="2">Visualizar/Editar
                                                                            </th>
                                                                            <th width="80" rowspan="2">Código PMAS
                                                                            </th>
                                                                            <th width="300" rowspan="2">Nome
                                                                            </th>
                                                                            <th width="220" rowspan="2">Responsável
                                                                            </th>
                                                                            <th width="240" colspan="3" style="height: 22px; padding-top: 3px;"
                                                                                valign="top">Serviços socioassistenciais
                                                                            </th>
                                                                            <th width="50" rowspan="2">Desativar
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th width="80">Visualizar/<br />
                                                                                Editar
                                                                            </th>
                                                                            <th width="80">Total de<br />
                                                                                serviços
                                                                            </th>
                                                                            <th width="100">Cofinanciamento<br />
                                                                                estadual
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
                                                                        <asp:ImageButton runat="server" ID="btnVisUnidade" ToolTip="Visualizar Local de Execução"
                                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Visualizar" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <%#DataBinder.Eval(Container.DataItem, "Id") %>
                                                                    </td>
                                                                    <td class="align-left">
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.Nome") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.Responsavel") %>&nbsp;
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:ImageButton runat="server" ID="btnVisServicosRecursosFinanceiros" ToolTip="Visualizar/Adicionar/Editar Serviços e Recursos Financeiros"
                                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" />
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.TotalServicos") %>
                                                                    </td>
                                                                    <td align="align-center">
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
                                                                            CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('Deseja realmente desativar este Local de Execução?');" />&nbsp;
                                                                      <%--   <%#MontarBotaoExcluirLocalPublico((Seds.PMAS.QUADRIENAL.Entidades.Estruturas.ConsultaLocalExecucaoPublicoInfo)Container.DataItem) %>--%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <div align="center" style="width: 100%;">
                                                                    <b class="titulo">Não foi encontrado nenhum registro de local de execução</b>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                        <div class="row">
                                                            <asp:Button ID="btnLocalExecucao" runat="server" SkinID="button-Save" Text="Adicionar local de execução"
                                                                OnClick="btnLocalExecucao_Click" Visible="false"></asp:Button>&nbsp;
                                                              <asp:Button ID="btnLocalExecucaoDesativado" runat="server" SkinID="button-Save" Text="Locais da rede direta desativados"
                                                                  OnClick="btnLocalExecucaoDesativado_Click"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="frame active">
                                            <div class="subheading">
                                                <a href="#" runat="server" id="linkAlteracoesQuadroCRAS" visible="false">
                                                    <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />
                                                    Alterado
                                                </a>&nbsp;3.2.b - Centro de Referência de Assistência Social (CRAS)
                                            </div>
                                            <div class="content">
                                                <div class="formInput">
                                                    <div class="subgrid">
                                                        <asp:ListView ID="lstCRAS" runat="server" DataKeyNames="Id" OnItemDataBound="lstCRAS_ItemDataBound"
                                                            OnItemCommand="lstCRAS_ItemCommand">
                                                            <LayoutTemplate>
                                                                <table class="table striped border bordered" cellspacing="0"
                                                                    cellpadding="0" border="0">
                                                                    <thead class="info">
                                                                        <tr>
                                                                            <th width="20" style="height: 22px;"
                                                                                rowspan="2"></th>
                                                                            <th width="60" rowspan="2">Visualizar
                                                                            </th>
                                                                            <th width="120" rowspan="2">ID CRAS
                                                                            </th>
                                                                            <th width="280" rowspan="2">Nome
                                                                            </th>
                                                                            <th width="150" rowspan="2">Coordenador
                                                                            </th>
                                                                            <th width="260" colspan="3" style="height: 22px; padding-top: 3px;"
                                                                                valign="top">Serviços socioassistenciais
                                                                            </th>
                                                                            <th width="50" rowspan="2">Desativar
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th width="90">Visualizar/<br />
                                                                                Editar
                                                                            </th>
                                                                            <th width="80">Total de<br />
                                                                                serviços
                                                                            </th>
                                                                            <th width="100">Cofinanciamento<br />
                                                                                estadual
                                                                            </th>
                                                                            <th style="display: none;">Id
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
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.IdCRAS") %>
                                                                    </td>
                                                                    <td class="align-left">
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.Nome") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.Coordenador") %>&nbsp;
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:ImageButton runat="server" ID="btnVisServicosRecursosFinanceiros" ToolTip="Visualizar Serviços e Recursos Financeiros"
                                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" />
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.TotalServicos") %>
                                                                    </td>
                                                                    <td align="align-center">
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
                                                                            CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('Deseja realmente desativar este CRAS?');" />&nbsp;
                                                                    </td>
                                                                    <td style="display: none;">
                                                                        <%#DataBinder.Eval(Container.DataItem, "Id") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <div align="center" style="width: 100%;">
                                                                    <b class="titulo">Não foi encontrado nenhum registro de CRAS</b>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                        <div class="row">
                                                            <asp:Button ID="btnIncluirCRAS" runat="server" Text="Incluir CRAS" SkinID="button-Save"
                                                                CausesValidation="False" OnClick="btnIncluirCRAS_Click"></asp:Button>
                                                            <asp:Button ID="btnCRASDesativado" runat="server" Text="CRAS Desativados" SkinID="button-Save"
                                                                CausesValidation="False" OnClick="btnCRASDesativado_Click">
                                                            </asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="frame active">
                                            <div class="subheading">
                                                <a href="#" runat="server" id="linkAlteracoesQuadroCREAS" visible="false">
                                                    <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                                </a>&nbsp;
                                                  3.2.C - Centro de Referência Especializado de Assistência Social (CREAS) 
                                            </div>
                                            <div class="content">
                                                <div class="formInput">
                                                    <div class="subgrid">
                                                        <asp:ListView ID="lstCREAS" runat="server" DataKeyNames="Id" OnItemDataBound="lstCREAS_ItemDataBound"
                                                            OnItemCommand="lstCREAS_ItemCommand">
                                                            <LayoutTemplate>
                                                                <table class="table striped border bordered" cellspacing="0"
                                                                    cellpadding="0" border="0">
                                                                    <thead class="info">
                                                                        <tr class="ui-jqgrid-labels">
                                                                            <th width="20" style="height: 22px;"
                                                                                rowspan="2"></th>
                                                                            <th width="60" rowspan="2">Visualizar
                                                                            </th>
                                                                            <th width="120" rowspan="2">IDCREAS
                                                                            </th>
                                                                            <th width="280" rowspan="2">Nome
                                                                            </th>
                                                                            <th width="150" rowspan="2">Coordenador
                                                                            </th>
                                                                            <th width="260" colspan="3" style="height: 22px; padding-top: 3px;"
                                                                                valign="top">Serviços socioassistenciais
                                                                            </th>
                                                                            <th width="50" rowspan="2">Desativar
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th width="90">Visualizar/<br />
                                                                                Editar
                                                                            </th>
                                                                            <th width="80">Total de<br />
                                                                                serviços
                                                                            </th>
                                                                            <th width="100">Cofinanciamento<br />
                                                                                estadual
                                                                            </th>
                                                                            <th style="display: none;">Id
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
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.IdCREAS") %>
                                                                    </td>
                                                                    <td class="align-left">
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.Nome") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.Coordenador") %>&nbsp;
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:ImageButton runat="server" ID="btnVisServicosRecursosFinanceiros" ToolTip="Visualizar Serviços e Recursos Financeiros"
                                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" />
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.TotalServicos") %>
                                                                    </td>
                                                                    <td align="align-center">
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
                                                                            CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('Deseja realmente desativar este CREAS?');" />&nbsp;
                                                                    </td>
                                                                    <td style="display: none;">
                                                                        <%#DataBinder.Eval(Container.DataItem, "Id") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <div align="center" style="width: 100%;">
                                                                    <b class="titulo">Não foi encontrado nenhum registro de CREAS</b>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                        <div class="row">
                                                            <asp:Button ID="btnIncluirCREAS" runat="server" Text="Incluir CREAS" SkinID="button-save"
                                                                CausesValidation="False" Width="160px" OnClick="btnIncluirCREAS_Click"></asp:Button>
                                                            <asp:Button ID="Button1" runat="server" Text="CREAS Desativados" SkinID="button-Save"
                                                                CausesValidation="False" OnClick="btnCREASDesativado_Click"></asp:Button>
                                                            <%--&nbsp;<asp:Button ID="btnPrevisaoInstalacaoCREAS" runat="server" SkinID="button-save" Text="Visualizar/Incluir previsão de implantação de CREAS"
                                                                OnClick="btnPrevisaoInstalacaoCREAS_Click" />--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="frame active">
                                            <div class="subheading">
                                                <a href="#" runat="server" id="linkAlteracoesQuadroCentroPOP" visible="false">
                                                    <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                                </a>
                                                3.2.D - Centro de Referência Especializado para População em Situação de Rua (Centro POP)
                                            </div>
                                            <div class="content">
                                                <div class="formInput">
                                                    <div class="subgrid">
                                                        <asp:ListView ID="lstCentroPOP" runat="server" DataKeyNames="Id" OnItemDataBound="lstCentroPOP_ItemDataBound"
                                                            OnItemCommand="lstCentroPOP_ItemCommand">
                                                            <LayoutTemplate>
                                                                <table class="table striped border bordered" cellspacing="0"
                                                                    cellpadding="0" border="0">
                                                                    <thead class="info">
                                                                        <tr>
                                                                            <th width="20" style="height: 22px;"
                                                                                rowspan="2"></th>
                                                                            <th width="60" rowspan="2">Visualizar
                                                                            </th>
                                                                            <th width="120" rowspan="2">IDCREAS
                                                                            </th>
                                                                            <th width="280" rowspan="2">Nome
                                                                            </th>
                                                                            <th width="150" rowspan="2">Coordenador
                                                                            </th>
                                                                            <th width="260" colspan="3" style="height: 22px; padding-top: 3px;"
                                                                                valign="top">Serviços socioassistenciais
                                                                            </th>
                                                                            <th width="50" rowspan="2">Desativar
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th width="90">Visualizar/<br />
                                                                                Editar
                                                                            </th>
                                                                            <th width="80">Total de<br />
                                                                                serviços
                                                                            </th>
                                                                            <th width="100">Cofinanciamento<br />
                                                                                estadual
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
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.IdCREAS") %>
                                                                    </td>
                                                                    <td class="align-left">
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.Nome") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.Coordenador") %>&nbsp;
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:ImageButton runat="server" ID="btnVisServicosRecursosFinanceiros" ToolTip="Visualizar Serviços e Recursos Financeiros"
                                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" />
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.TotalServicos") %>
                                                                    </td>
                                                                    <td align="align-center">
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
                                                                                                <div style="width:90%; padding: 3px; border-bottom:1px solid #b5d4e4;">
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
                                                                            CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('Deseja realmente desativar este Centro POP?');" />&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <div align="center" style="width: 100%;">
                                                                    <b class="titulo">Não foi encontrado nenhum registro de Centro POP</b>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <asp:Button ID="btnIncluirCentroPOP" runat="server" Text="Incluir Centro POP" SkinID="button-save"
                                                                    CausesValidation="False" Width="160px" OnClick="btnIncluirCentroPOP_Click"></asp:Button>
                                                                <asp:Button ID="btnCentroPOPDesativado" runat="server" Text="Centro POP Desativados" SkinID="button-Save"
                                                                    CausesValidation="False" OnClick="btnCentroPOPDesativado_Click"></asp:Button>
                                                                <%--   &nbsp;<asp:Button ID="btnPrevisaoInstalacaoCentroPOP" SkinID="button-save" runat="server" Text="Visualizar/Incluir previsão de implantação de Centro POP"
                                                                    OnClick="btnPrevisaoInstalacaoCentroPOP_Click" />--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
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
