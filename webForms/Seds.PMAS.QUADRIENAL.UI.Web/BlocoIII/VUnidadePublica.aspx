<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="VUnidadePublica.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.VUnidadePublica" %>

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
                           Locais onde são desenvolvidos os serviços da rede direta desativados
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
                                            <asp:Button ID="btnLocalizarLocal" runat="server" SkinID="button-save" Text="Pesquisar local de execução" Width="209px"
                                                OnClick="btnLocalizarLocal_Click"></asp:Button>
                                            <asp:Button ID="btnVoltar0" runat="server"
                                                PostBackUrl="~/BlocoIII/CUnidadesPublicasDesativadas.aspx" Text="Voltar" />
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
                                                3.2.a - Locais de execução dos serviços da rede direta&nbsp;desativados
                                                  <a href="#" runat="server" id="linkAlteracoesQuadroLocalPublico" visible="false">
                                                      <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                                  </a>
                                            </div>
                                            <div class="content">
                                                <div class="formInput">
                                                    <div class="subgrid">
                                                        <asp:ListView ID="lstLocais" runat="server" DataKeyNames="Id" OnItemDataBound="lstLocais_ItemDataBound" OnItemCommand="lstLocais_ItemCommand">
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
                                                                            <th width="220" rowspan="2"><%--Bruno V. --%>
                                                                            Responsável
                                                                            </th>
                                                                            <th width="240" colspan="3" style="height: 22px; padding-top: 3px;"
                                                                                valign="top">Serviços socioassistenciais
                                                                            </th>
                                                                            <th width="50" rowspan="2">Data de desativação
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
                                                                        <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#DataBinder.Eval(Container.DataItem, "Responsavel") %>&nbsp;
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:ImageButton runat="server" ID="btnVisServicosRecursosFinanceiros" ToolTip="Visualizar/Adicionar/Editar Serviços e Recursos Financeiros"
                                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" />
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#DataBinder.Eval(Container.DataItem, "TotalServicos") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#((Decimal)DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual")).ToString("N2") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#((DateTime)DataBinder.Eval(Container.DataItem, "DataEncerramento")).ToShortDateString() %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <div align="center" style="width: 100%;">
                                                                    <b class="titulo">Não existe registro de locais de execução desativados</b>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
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
                                                        <asp:ListView ID="lstCRAS" runat="server" DataKeyNames="Id" OnItemCommand="lstCRAS_ItemCommand" OnItemDataBound="lstCRAS_ItemDataBound">
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
                                                                            <th width="50" rowspan="2">Data de desativação
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
                                                                        <%#DataBinder.Eval(Container.DataItem, "IdCRAS") %>
                                                                    </td>
                                                                    <td class="align-left">
                                                                        <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#DataBinder.Eval(Container.DataItem, "Coordenador") %>&nbsp;
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:ImageButton runat="server" ID="btnVisServicosRecursosFinanceiros" ToolTip="Visualizar Serviços e Recursos Financeiros"
                                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" />
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#DataBinder.Eval(Container.DataItem, "TotalServicosDesativados") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#((Decimal)DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual")).ToString("N2") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#((DateTime)DataBinder.Eval(Container.DataItem, "DataEncerramento")).ToShortDateString() %>
                                                                    </td>
                                                                    <td style="display: none;">
                                                                        <%#DataBinder.Eval(Container.DataItem, "Id") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <div align="center" style="width: 100%;">
                                                                    <b class="titulo">Não existe registro de CRAS desativados</b>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
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
                                                        <asp:ListView ID="lstCREAS" runat="server" DataKeyNames="Id" OnItemDataBound="lstCREAS_ItemDataBound" OnItemCommand="lstCREAS_ItemCommand">
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
                                                                            <th width="50" rowspan="2">Data de desativação
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
                                                                        <%#DataBinder.Eval(Container.DataItem, "IdCREAS") %>
                                                                    </td>
                                                                    <td class="align-left">
                                                                        <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#DataBinder.Eval(Container.DataItem, "Coordenador") %>&nbsp;
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:ImageButton runat="server" ID="btnVisServicosRecursosFinanceiros" ToolTip="Visualizar Serviços e Recursos Financeiros"
                                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" />
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#DataBinder.Eval(Container.DataItem, "TotalServicosDesativados") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#((Decimal)DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual")).ToString("N2") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#((DateTime)DataBinder.Eval(Container.DataItem, "DataEncerramento")).ToShortDateString() %>
                                                                    </td>
                                                                    <td style="display: none;">
                                                                        <%#DataBinder.Eval(Container.DataItem, "Id") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <div align="center" style="width: 100%;">
                                                                    <b class="titulo">Não existe registro de CREAS desativados</b>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
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
                                                        <asp:ListView ID="lstCentroPOP" runat="server" DataKeyNames="Id" OnItemDataBound="lstCentroPOP_ItemDataBound" OnItemCommand="lstCentroPOP_ItemCommand">
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
                                                                            <th width="50" rowspan="2">Data de desativação
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
                                                                        <%#DataBinder.Eval(Container.DataItem, "IdCREAS") %>
                                                                    </td>
                                                                    <td class="align-left">
                                                                        <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#DataBinder.Eval(Container.DataItem, "Coordenador") %>&nbsp;
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:ImageButton runat="server" ID="btnVisServicosRecursosFinanceiros" ToolTip="Visualizar Serviços e Recursos Financeiros"
                                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" />
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#DataBinder.Eval(Container.DataItem, "TotalServicosDesativados") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#((Decimal)DataBinder.Eval(Container.DataItem, "ValorCofinanciamentoEstadual")).ToString("N2") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <%#((DateTime)DataBinder.Eval(Container.DataItem, "DataEncerramento")).ToShortDateString() %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <div align="center" style="width: 100%;">
                                                                    <b class="titulo">Não existe registro de CREAS desativados</b>
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
                        </div>
                    </div>
                </div>
            </form>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
