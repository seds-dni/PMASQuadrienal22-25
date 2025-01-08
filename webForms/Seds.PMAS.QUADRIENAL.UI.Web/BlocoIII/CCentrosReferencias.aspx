<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CCentrosReferencias.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoII.CCentrosReferencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <table class="ui-widget ui-widget-content ui-corner-all" cellspacing="2" cellpadding="0"
                width="900" align="center" border="0">
                <tr>
                    <td class="ui-state-default ui-widget-header ui-corner-top">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="height: 30px; padding-left: 10px;">
                                    <img src="../Styles/Icones/redeprotecao.gif" align="absMiddle" />
                                    <b style="font-size: 18px;">20</b> <b>- CRAS - Centro de Referência de Assistência Social</b>
                                </td>
                                <td align="right">
                                    <a href="#" runat="server" id="linkAlteracoesQuadro20" visible="false">
                                        <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                    </a>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 5px;">
                        Cadastrar os CRAS implantados e já em funcionamento no município, como também a
                        previsão de implantação de novas unidades.
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px;">
                        Localizar nome :
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px;">
                        <table>
                            <tr>
                                <td>
                                    Nome:
                                    <asp:TextBox runat="server" ID="txtLocalizarCRAS" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnLocalizarCRAS" runat="server" Text="Localizar" SkinID="button-find"
                                        OnClick="btnLocalizarCRAS_Click" />
                                    <asp:Button ID="btnLimparBuscaCRAS" runat="server" Text="Limpar Busca" OnClick="btnLimparBuscaCRAS_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListView ID="lstCRAS" runat="server" DataKeyNames="Id" OnItemDataBound="lstCRAS_ItemDataBound"
                            OnItemCommand="lstCRAS_ItemCommand">
                            <LayoutTemplate>
                                <table class="ui-jqgrid ui-widget ui-widget-content ui-corner-all" cellspacing="0"
                                    cellpadding="0" border="0">
                                    <thead>
                                        <tr>
                                            <th colspan="9" class="ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix"
                                                style="height: 20px;">
                                                <span class="ui-jqgrid-title">CRAS</span>
                                            </th>
                                        </tr>
                                        <tr class="ui-jqgrid-labels">
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="20" style="height: 22px;"
                                                rowspan="2">
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="60" rowspan="2">
                                                Visualizar
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="120" rowspan="2">
                                                Código do CRAS
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="280" rowspan="2">
                                                Nome
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="150" rowspan="2">
                                                Coordenador
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="260" colspan="3" style="height: 22px;
                                                padding-top: 3px;" valign="top">
                                                Serviços e recursos financeiros
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="50" rowspan="2">
                                                Excluir
                                            </th>
                                        </tr>
                                        <tr class="ui-jqgrid-labels">
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="90">
                                                Visualizar/<br />
                                                Editar
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="80">
                                                Total de<br />
                                                serviços
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="100">
                                                Previsão<br />
                                                orçamentária
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="jqgfirstrow" style="height: auto;">
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="ui-widget-content row">
                                    <td class="ui-state-default jqgrid-rownum row" style="height: 22px;">
                                        <asp:Label ID="lblSequencia" runat="server" />
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton runat="server" ID="btnVisUnidade" ToolTip="Visualizar Unidade" ImageUrl="~/Styles/Icones/find.png"
                                            CommandName="Visualizar" />
                                    </td>
                                    <td align="center">
                                        <%#DataBinder.Eval(Container.DataItem, "Id") %>
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
                                        <%#DataBinder.Eval(Container.DataItem, "TotalServicos") %>
                                    </td>
                                    <td class="align-center">
                                        <%#((Decimal)DataBinder.Eval(Container.DataItem, "PrevisaoOrcamentaria")).ToString("N2") %>
                                    </td>
                                    <td class="align-center">
                                        <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                            CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('Deseja realmente remover o CRAS?');" />&nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div align="center" style="width: 100%;">
                                    <b class="titulo">Não existe registro de CRAS neste município</b>
                                </div>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="padding-top: 20px;">
                        <asp:Button ID="btnIncluirCRAS" runat="server" Text="Incluir CRAS" SkinID="button-add"
                            CausesValidation="False" Width="160px" OnClick="btnIncluirCRAS_Click"></asp:Button>
                        &nbsp;<asp:Button ID="btnPrevisaoInstalacaoCRAS" runat="server" Text="Visualizar/Incluir Previsão de Implantação de CRAS"
                            Height="22px" OnClick="btnPrevisaoInstalacaoCRAS_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <table class="ui-widget ui-widget-content ui-corner-all" cellspacing="2" cellpadding="0"
                width="900" align="center" border="0">
                <tr>
                    <td class="ui-state-default ui-widget-header ui-corner-top">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="height: 30px; padding-left: 10px;">
                                    <img src="../Styles/Icones/redeprotecao.gif" align="absMiddle" />
                                    <b style="font-size: 18px;">25</b> <b>- CREAS - Centro de Referência Especializado de
                                        Assistência Social</b>
                                </td>
                                <td align="right">
                                    <a href="#" runat="server" id="linkAlteracoesQuadro25" visible="false">
                                        <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                    </a>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 5px;">
                        Cadastrar os CREAS implantados e já em funcionamento no município, como também a
                        previsão de implantação de novas unidades.
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px;">
                        Localizar nome :
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px;">
                        <table>
                            <tr>
                                <td>
                                    Nome:
                                    <asp:TextBox runat="server" ID="txtLocalizarCREAS" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnLocalizarCREAS" runat="server" Text="Localizar" SkinID="button-find"
                                        OnClick="btnLocalizarCREAS_Click" />
                                    <asp:Button ID="btnLimparBuscaCREAS" runat="server" Text="Limpar Busca" OnClick="btnLimparBuscaCREAS_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListView ID="lstCREAS" runat="server" DataKeyNames="Id" OnItemDataBound="lstCREAS_ItemDataBound"
                            OnItemCommand="lstCREAS_ItemCommand">
                            <LayoutTemplate>
                                <table class="ui-jqgrid ui-widget ui-widget-content ui-corner-all" cellspacing="0"
                                    cellpadding="0" border="0">
                                    <thead>
                                        <tr>
                                            <th colspan="9" class="ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix"
                                                style="height: 20px;">
                                                <span class="ui-jqgrid-title">CREAS</span>
                                            </th>
                                        </tr>
                                        <tr class="ui-jqgrid-labels">
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="20" style="height: 22px;"
                                                rowspan="2">
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="60" rowspan="2">
                                                Visualizar
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="120" rowspan="2">
                                                Código do CREAS
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="280" rowspan="2">
                                                Nome
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="150" rowspan="2">
                                                Coordenador
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="260" colspan="3" style="height: 22px;
                                                padding-top: 3px;" valign="top">
                                                Serviços e recursos financeiros
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="50" rowspan="2">
                                                Excluir
                                            </th>
                                        </tr>
                                        <tr class="ui-jqgrid-labels">
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="90">
                                                Visualizar/<br />
                                                Editar
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="80">
                                                Total de<br />
                                                serviços
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="100">
                                                Previsão<br />
                                                orçamentária
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="jqgfirstrow" style="height: auto;">
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="ui-widget-content row">
                                    <td class="ui-state-default jqgrid-rownum row" style="height: 22px;">
                                        <asp:Label ID="lblSequencia" runat="server" />
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton runat="server" ID="btnVisUnidade" ToolTip="Visualizar Unidade" ImageUrl="~/Styles/Icones/find.png"
                                            CommandName="Visualizar" />
                                    </td>
                                    <td align="center">
                                        <%#DataBinder.Eval(Container.DataItem, "Id") %>
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
                                        <%#DataBinder.Eval(Container.DataItem, "TotalServicos") %>
                                    </td>
                                    <td class="align-center">
                                        <%#((Decimal)DataBinder.Eval(Container.DataItem, "PrevisaoOrcamentaria")).ToString("N2") %>
                                    </td>
                                    <td class="align-center">
                                        <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                            CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('Deseja realmente remover o CREAS?');" />&nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div align="center" style="width: 100%;">
                                    <b class="titulo">Não existe registro de CREAS neste município</b>
                                </div>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="padding-top: 20px;">
                        <asp:Button ID="btnIncluirCREAS" runat="server" Text="Incluir CREAS" SkinID="button-add"
                            CausesValidation="False" Width="160px" OnClick="btnIncluirCREAS_Click"></asp:Button>
                        &nbsp;<asp:Button ID="btnPrevisaoInstalacaoCREAS" runat="server" Text="Visualizar/Incluir Previsão de Implantação de CREAS"
                            Height="22px" OnClick="btnPrevisaoInstalacaoCREAS_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <table class="ui-widget ui-widget-content ui-corner-all" cellspacing="2" cellpadding="0"
                width="900" align="center" border="0">
                <tr>
                    <td class="ui-state-default ui-widget-header ui-corner-top">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="height: 30px; padding-left: 10px;">
                                    <img src="../Styles/Icones/redeprotecao.gif" align="absMiddle" />
                                    <b style="font-size: 18px;">31</b> <b>- Centro POP - Centro de Referência Especializado
                                        para População em Situação de Rua</b>
                                </td>
                                <td align="right">
                                    <a href="#" runat="server" id="linkAlteracoesQuadro31" visible="false">
                                        <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                    </a>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 5px;">
                        Cadastrar os Centros POP implantados e já em funcionamento no município, como também
                        a previsão de implantação de novas unidades.
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px;">
                        Localizar nome :
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px;">
                        <table>
                            <tr>
                                <td>
                                    Nome:
                                    <asp:TextBox runat="server" ID="txtLocalizarCentroPOP" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnLocalizarCentroPOP" runat="server" Text="Localizar" SkinID="button-find"
                                        OnClick="btnLocalizarCentroPOP_Click" />
                                    <asp:Button ID="btnLimparBuscaCentroPOP" runat="server" Text="Limpar Busca" OnClick="btnLimparBuscaCentroPOP_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListView ID="lstCentroPOP" runat="server" DataKeyNames="Id" OnItemDataBound="lstCentroPOP_ItemDataBound"
                            OnItemCommand="lstCentroPOP_ItemCommand">
                            <LayoutTemplate>
                                <table class="ui-jqgrid ui-widget ui-widget-content ui-corner-all" cellspacing="0"
                                    cellpadding="0" border="0">
                                    <thead>
                                        <tr>
                                            <th colspan="9" class="ui-jqgrid-titlebar ui-widget-header ui-corner-top ui-helper-clearfix"
                                                style="height: 20px;">
                                                <span class="ui-jqgrid-title">Centro POP</span>
                                            </th>
                                        </tr>
                                        <tr class="ui-jqgrid-labels">
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="20" style="height: 22px;"
                                                rowspan="2">
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="60" rowspan="2">
                                                Visualizar
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="120" rowspan="2">
                                                Código do<br />
                                                Centro POP
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="280" rowspan="2">
                                                Nome
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="150" rowspan="2">
                                                Coordenador
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="260" colspan="3" style="height: 22px;
                                                padding-top: 3px;" valign="top">
                                                Serviços e recursos financeiros
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="50" rowspan="2">
                                                Excluir
                                            </th>
                                        </tr>
                                        <tr class="ui-jqgrid-labels">
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="90">
                                                Visualizar/<br />
                                                Editar
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="80">
                                                Total de<br />
                                                serviços
                                            </th>
                                            <th class="ui-state-default ui-th-column ui-th-ltr" width="100">
                                                Previsão<br />
                                                orçamentária
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr class="jqgfirstrow" style="height: auto;">
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                            <td style="height: 0px;">
                                            </td>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="ui-widget-content row">
                                    <td class="ui-state-default jqgrid-rownum row" style="height: 22px;">
                                        <asp:Label ID="lblSequencia" runat="server" />
                                    </td>
                                    <td align="center">
                                        <asp:ImageButton runat="server" ID="btnVisUnidade" ToolTip="Visualizar Unidade" ImageUrl="~/Styles/Icones/find.png"
                                            CommandName="Visualizar" />
                                    </td>
                                    <td align="center">
                                        <%#DataBinder.Eval(Container.DataItem, "Id") %>
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
                                        <%#DataBinder.Eval(Container.DataItem, "TotalServicos") %>
                                    </td>
                                    <td class="align-center">
                                        <%#((Decimal)DataBinder.Eval(Container.DataItem, "PrevisaoOrcamentaria")).ToString("N2") %>
                                    </td>
                                    <td class="align-center">
                                        <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                            CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('Deseja realmente desativar este Centro POP?');" />&nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div align="center" style="width: 100%;">
                                    <b class="titulo">Não existe registro de Centro POP neste município</b>
                                </div>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="padding-top: 20px;">
                        <asp:Button ID="btnIncluirCentroPOP" runat="server" Text="Incluir Centro POP" SkinID="button-add"
                            CausesValidation="False" Width="160px" OnClick="btnIncluirCentroPOP_Click"></asp:Button>
                        &nbsp;<asp:Button ID="btnPrevisaoInstalacaoCentroPOP" runat="server" Text="Visualizar/Incluir Previsão de Implantação de Centro POP"
                            Height="22px" OnClick="btnPrevisaoInstalacaoCentroPOP_Click" />
                    </td>
                </tr>
            </table>
            <table width="900" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="CUnidadesPublicas.aspx">
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
