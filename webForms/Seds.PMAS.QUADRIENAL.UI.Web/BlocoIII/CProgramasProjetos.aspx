<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CProgramasProjetos.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.CProgramasProjetos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <script src="../Scripts/Util.js" type="text/javascript"></script>
            <form name="frmProgramaProjeto">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            3.15 - Programas e projetos
                            <a href="#" runat="server" id="linkAlteracoesQuadroFederais" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado</a>
                            <span class="mif-organization icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Programas e Projetos">
                                <div class="grid">
                                    <div class="row">
                                        <div class="frame active">
                                            <div class="subheading">
                                                <a href="#" runat="server" id="linkAlteracoesQuadroLocalPublico" visible="false">
                                                    <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                                </a>&nbsp;
                                              3.15.a - Programas/projetos federais
                                            </div>
                                            <div class="content">
                                                <div class="formInput" data-text="Programas Federais">
                                                    <div class="subgrid">
                                                        <asp:ListView ID="lstProgramasFederais" runat="server" DataKeyNames="Id" OnItemDataBound="lstProgramasFederais_ItemDataBound"
                                                            OnItemCommand="lstProgramasFederais_ItemCommand">
                                                            <LayoutTemplate>
                                                                <table class="table striped border bordered" cellspacing="0"
                                                                    cellpadding="0" border="0">
                                                                    <thead class="info">
                                                                        <tr>
                                                                            <th width="20" style="height: 22px;"
                                                                                rowspan="2"></th>
                                                                            <th width="60" rowspan="2">Visualizar/Editar
                                                                            </th>
                                                                            <th width="510" rowspan="2">Programa/Projeto
                                                                            </th>
                                                                            <th width="100" rowspan="2">Executa<br />
                                                                                o programa?
                                                                            </th>
                                                                            <th width="260" colspan="2" style="height: 22px; padding-top: 3px;"
                                                                                valign="top">Integração com serviços
                                                                            </th>
                                                                            <th width="50" rowspan="2">Excluir
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th>Visualizar
                                                                            </th>
                                                                            <th>Total de<br />
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
                                                                    <td style="height: 22px;">
                                                                        <asp:Label ID="lblSequencia" runat="server" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:ImageButton runat="server" ID="btnVis" ToolTip="Visualizar Programa/Projeto"
                                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Visualizar" />
                                                                    </td>
                                                                    <td class="align-left">
                                                                        <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                                                                        <asp:HiddenField runat="server" ID="hdfNome" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <%#((Int32)DataBinder.Eval(Container.DataItem, "Aderiu")) > 0 ? "Sim" : "Não" %>
                                                                    </td>
                                                                    <td id="tdNao" align="center" runat="server" visible="false" colspan="2">-------------
                                                                    </td>
                                                                    <td align="center" runat="server" id="tdVisualizarServicos">
                                                                        <asp:ImageButton runat="server" ID="btnVisServicosRecursosFinanceiros" ToolTip="Visualizar Serviços e Recursos Financeiros"
                                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" />
                                                                    </td>
                                                                    <td class="align-center" runat="server" id="tdTotalServicos">
                                                                        <%#DataBinder.Eval(Container.DataItem, "TotalServicos") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                            CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('ATENÇÃO!!!\nExcluir este programa/projeto fará com que todas as associações registradas nos serviços em relação a ele sejam apagadas.\nCaso queira apenas modificar alguma informação registrada neste programa/projeto, clique no ícone da primeira coluna à esquerda para editar os registros.\n\nDeseja realmente excluir este programa/projeto?');" />&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <div align="center" style="width: 100%;">
                                                                    <b class="titulo">Não existe registro de Programas/Projetos federais</b>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="frame active">
                                            <div class="subheading">
                                                  3.15.b - Programas Estaduais
                                                <a href="#" runat="server" id="linkAlteracoesQuadroEstaduais" visible="false">
                                                    <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                                </a>&nbsp;
                                            </div>
                                            <div class="content">
                                                <div class="formInput" data-text="Programas Estaduais">
                                                    <div class="subgrid">
                                                        <asp:ListView ID="lstProgramasEstaduais" runat="server" DataKeyNames="Id" OnItemDataBound="lstProgramasEstaduais_ItemDataBound"
                                                            OnItemCommand="lstProgramasEstaduais_ItemCommand">
                                                            <LayoutTemplate>
                                                                <table class="table striped border bordered" cellspacing="0"
                                                                    cellpadding="0" border="0">
                                                                    <thead class="info">
                                                                        <tr>
                                                                            <th width="20" style="height: 22px;"
                                                                                rowspan="2"></th>
                                                                            <th width="60" rowspan="2">Visualizar/Editar
                                                                            </th>
                                                                            <th width="510" rowspan="2">Programa/Projeto
                                                                            </th>
                                                                            <th width="100" rowspan="2">Executa<br />
                                                                                o programa?
                                                                            </th>
                                                                            <th width="260" colspan="2" style="height: 22px; padding-top: 3px;"
                                                                                valign="top">Integração com serviços
                                                                            </th>
                                                                            <th width="50" rowspan="2">Excluir
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th>Visualizar
                                                                            </th>
                                                                            <th>Total de<br />
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
                                                                    <td style="height: 22px;">
                                                                        <asp:Label ID="lblSequencia" runat="server" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:ImageButton runat="server" ID="btnVis" ToolTip="Visualizar Programa/Projeto"
                                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Visualizar" />
                                                                    </td>
                                                                    <td class="align-left">
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.Nome") %>
                                                                        <asp:HiddenField runat="server" ID="hdfNomeEstadual" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <%#((Int32)DataBinder.Eval(Container.DataItem, "obj.Aderiu")) > 0 ? "Sim" : "Não" %>
                                                                    </td>
                                                                    <td id="tdNao" align="center" runat="server" visible="false" colspan="2">-------------
                                                                    </td>
                                                                    <td align="center" runat="server" id="tdVisualizarServicos">
                                                                        <asp:ImageButton runat="server" ID="btnVisServicosRecursosFinanceiros" ToolTip="Visualizar Serviços e Recursos Financeiros"
                                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" />
                                                                    </td>
                                                                    <td class="align-center" runat="server" id="tdTotalServicos">
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.TotalServicos") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                            CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('ATENÇÃO!!!\nExcluir este programa/projeto fará com que todas as associações registradas nos serviços em relação a ele sejam apagadas.\nCaso queira apenas modificar alguma informação registrada neste programa/projeto, clique no ícone da primeira coluna à esquerda para editar os registros.\n\nDeseja realmente excluir este programa/projeto?');" />&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <div align="center" style="width: 100%;">
                                                                    <b class="titulo">Não existe registro de Programas/Projetos estaduais</b>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="frame active">
                                            <div class="subheading">
                                                  3.15.b.1 - Projetos Estaduais
                                                <a href="#" runat="server" id="A1" visible="false">
                                                    <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                                </a>&nbsp;
                                            </div>
                                            <div class="content">
                                                <div class="formInput" data-text="Projetos Estaduais">
                                                    <div class="subgrid">
                                                        <asp:ListView ID="lstProgramasEstaduaisUm" runat="server" DataKeyNames="Id"  OnItemDataBound="lstProgramasEstaduaisUm_ItemDataBound" OnItemCommand="lstProgramasEstaduaisUm_ItemCommand">
                                                            <LayoutTemplate>
                                                                <table class="table striped border bordered" cellspacing="0"
                                                                    cellpadding="0" border="0">
                                                                    <thead class="info">
                                                                        <tr>
                                                                            <th width="20" style="height: 22px;"
                                                                                rowspan="2"></th>
                                                                            <th width="60" rowspan="2">Visualizar/Editar
                                                                            </th>
                                                                            <th width="510" rowspan="2">Programa/Projeto
                                                                            </th>
                                                                            <th width="100" rowspan="2">Executa<br />
                                                                                o programa?
                                                                            </th>
                                                                            <th width="260" colspan="2" style="height: 22px; padding-top: 3px;"
                                                                                valign="top">Integração com serviços
                                                                            </th>
                                                                            <th width="50" rowspan="2">Excluir
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th>Visualizar
                                                                            </th>
                                                                            <th>Total de<br />
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
                                                                    <td style="height: 22px;">
                                                                        <asp:Label ID="lblSequencia" runat="server" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:ImageButton runat="server" ID="btnVis" ToolTip="Visualizar Programa/Projeto"
                                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Visualizar" />
                                                                    </td>
                                                                    <td class="align-left">
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.Nome") %>
                                                                        <asp:HiddenField runat="server" ID="hdfNomeEstadual" />
                                                                    </td>
                                                                    <td align="center">
                                                                        <%#((Int32)DataBinder.Eval(Container.DataItem, "obj.Aderiu")) > 0 ? "Sim" : "Não" %>
                                                                    </td>
                                                                    <td id="tdNao" align="center" runat="server" visible="false" colspan="2">-------------
                                                                    </td>
                                                                    <td align="center" runat="server" id="tdVisualizarServicos">
                                                                        <asp:ImageButton runat="server" ID="btnVisServicosRecursosFinanceiros" ToolTip="Visualizar Serviços e Recursos Financeiros"
                                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" />
                                                                    </td>
                                                                    <td class="align-center" runat="server" id="tdTotalServicos">
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.TotalServicos") %>
                                                                    </td>
                                                                    <td class="align-center">
                                                                        <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                            CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('ATENÇÃO!!!\nExcluir este programa/projeto fará com que todas as associações registradas nos serviços em relação a ele sejam apagadas.\nCaso queira apenas modificar alguma informação registrada neste programa/projeto, clique no ícone da primeira coluna à esquerda para editar os registros.\n\nDeseja realmente excluir este programa/projeto?');" />&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <div align="center" style="width: 100%;">
                                                                    <b class="titulo">Não existe registro de Programas/Projetos estaduais</b>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="frame active">
                                            <div class="subheading">
                                                  3.15.c - Programas/projetos Municipais
                                                <asp:ImageButton ID="btnInfo" runat="server" ImageUrl="~/Styles/Icones/help.png"
                                                    ImageAlign="AbsMiddle" OnClientClick="return false;" />

                                                <a href="#" runat="server" id="linkAlteracoesQuadro40" visible="false">
                                                    <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                                </a>&nbsp;
                                            </div>
                                            <asp:Panel ID="pnlAjuda" runat="server" CssClass="ajuda" Width="500px" Height="350px">
                                                <div id="btnCloseParent" style="float: right; z-index: 1;">
                                                    <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                                        ToolTip="Fechar" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                                                </div>
                                                <div>
                                                    <p>
                                                        Programas compreendem ações integradas e complementares com objetivos, tempo e área
                                                de abrangência definidos para qualificar, incentivar e melhorar os benefícios e
                                                os serviços socioassistenciais.
                                                    </p>
                                                    <p>
                                                        Projeto é um esforço temporário empreendido para alcançar um objetivo específico.
                                                É um trabalho empreendido com responsabilidade de execução e resultado esperado,
                                                com quantificação de benefícios e prazo de execução pré-estabelecidos.
                                                    </p>
                                                    <p>
                                                        Programas tendem a ter objetivos mais amplos e, por isso, períodos maiores de tempo
                                                para execução e alcance do objetivo, enquanto que projetos tendem a ter objetivos
                                                mais específicos e períodos menores de duração.
                                                    </p>
                                                    <p>
                                                        Serviços socioassistenciais constituem-se de um conjunto de atividades continuadas
                                                e organicamente articuladas, com objetivos definidos e que respondem, mediante ações
                                                profissionalizadas, às seguranças afiançadas pela Assistência Social, efetivando
                                                os direitos socioassistenciais. São prestados em uma determinada unidade física
                                                e se destinam a afiançar aquisições sociais que resultam do exercício capacitador
                                                de vínculos sociais e do desenvolvimento da autonomia por meio de metodologias de
                                                trabalho social e trabalho socioeducativo.
                                                    </p>
                                                </div>
                                            </asp:Panel>
                                            <ajaxToolkit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnInfo">
                                                <Animations>
                                                    <OnClick>
                                                        <Sequence AnimationTarget="pnlAjuda">
                                                        <EnableAction AnimationTarget="btnInfo" Enabled="false" />
                                                        <StyleAction Attribute="display" Value="block" />                                    
                                                        <Parallel>
                                                            <FadeIn Duration="1" Fps="20" />
                                                        </Parallel>
                                                        </Sequence>
                                                    </OnClick>
                                                </Animations>
                                            </ajaxToolkit:AnimationExtender>
                                            <ajaxToolkit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                                                <Animations>
                                                        <OnClick>
                                                            <Sequence AnimationTarget="pnlAjuda">                                                    
                                                                <Parallel Duration=".3" Fps="15">                                                        
                                                                    <FadeOut />
                                                                </Parallel>                        
                                                                <%--  Reset the sample so it can be played again --%>
                                                                <StyleAction Attribute="display" Value="none"/>                                                                                                       
                        
                                                                <%--  Enable the button so it can be played again --%>
                                                                <EnableAction AnimationTarget="btnInfo" Enabled="true" />
                                                            </Sequence>
                                                        </OnClick>
                                                        <OnMouseOver>
                                                            <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                                                        </OnMouseOver>
                                                        <OnMouseOut>
                                                            <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                                                        </OnMouseOut>
                                                </Animations>
                                            </ajaxToolkit:AnimationExtender>
                                            <div class="content">
                                                <div class="formInput" data-text="Programas Municipais">
                                                    <div class="subgrid">
                                                        <div class="row">

                                                            <div class="cell" style="padding-left: 5px;">
                                                            Registre neste bloco as informações sobre os programas ou projetos da política de Assistência Social mantidos pelo próprio município.
                                                            Devem ser registrados apenas programas/projetos que já estejam em funcionamento no município. 
                                                            Caso ainda estejam em fase de planejamento devem ser registrados no Bloco 6 - Planejamento. 
                                                                <br />Para iniciar o registro, clique no botão “Incluir Programa/Projeto”.
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <asp:ListView ID="lstProgramas" runat="server" DataKeyNames="Id" OnItemDataBound="lstProgramasMunicipais_ItemDataBound"
                                                                OnItemCommand="lstProgramasMunicipais_ItemCommand">
                                                                <LayoutTemplate>
                                                                    <table class="table border bordered" cellspacing="0"
                                                                        cellpadding="0" border="0">
                                                                        <thead class="info">
                                                                            <tr>
                                                                                <th width="20" style="height: 22px;"
                                                                                    rowspan="2"></th>
                                                                                <th width="60" rowspan="2">Visualizar/Editar
                                                                                </th>
                                                                                <th width="510" rowspan="2">Nome
                                                                                </th>
                                                                                <th width="360" colspan="2" style="height: 22px; padding-top: 3px;"
                                                                                    valign="top">Integração com serviços
                                                                                </th>
                                                                                <th width="50" rowspan="2">Excluir
                                                                                </th>
                                                                            </tr>
                                                                            <tr class="ui-jqgrid-labels">
                                                                                <th>Visualizar
                                                                                </th>
                                                                                <th>Total de<br />
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
                                                                        <td style="height: 22px;">
                                                                            <asp:Label ID="lblSequencia" runat="server" />
                                                                        </td>
                                                                        <td align="center">
                                                                            <asp:ImageButton runat="server" ID="btnVis" ToolTip="Visualizar Programa/Projeto"
                                                                                ImageUrl="~/Styles/Icones/find.png" CommandName="Visualizar" />
                                                                        </td>
                                                                        <td class="align-left">
                                                                            <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                                                                            <asp:HiddenField runat="server" ID="hdfNome" />
                                                                            <asp:HiddenField runat="server" ID="hdfTipoProgramaTransferencia" />
                                                                        </td>
                                                                        <td id="tdNao" align="center" runat="server" visible="false" colspan="2">-------------
                                                                        </td>
                                                                        <td align="center" runat="server" id="tdVisualizarServicos">
                                                                            <asp:ImageButton runat="server" ID="btnVisServicosRecursosFinanceiros" ToolTip="Visualizar Serviços e Recursos Financeiros"
                                                                                ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" />
                                                                        </td>
                                                                        <td class="align-center" runat="server" id="tdTotalServicos">
                                                                            <%#DataBinder.Eval(Container.DataItem, "TotalServicos") %>
                                                                        </td>
                                                                        <td class="align-center">
                                                                            <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                                CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('ATENÇÃO!!!\nExcluir este programa/projeto fará com que todas as associações registradas nos serviços em relação a ele sejam apagadas.\nCaso queira apenas modificar alguma informação registrada neste programa/projeto, clique no ícone da primeira coluna à esquerda para editar os registros.\n\nDeseja realmente excluir este programa/projeto?');" />&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <EmptyDataTemplate>
                                                                    <div align="center" style="width: 100%;">
                                                                        <b class="titulo">Não existe registro de Programas/Projetos municipais</b>
                                                                    </div>
                                                                </EmptyDataTemplate>
                                                            </asp:ListView>
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell" align="center">
                                                                <asp:Button ID="btnIncluir" runat="server" Text="Incluir Programa/Projeto" SkinID="button-save"
                                                                    CausesValidation="False" Width="200px" PostBackUrl="~/BlocoIII/FProgramaProjetoCadastro.aspx?p=m"></asp:Button>
                                                                &nbsp; &nbsp;
                                                              <%--  <asp:Button ID="btnIncluirTransferenciaRenda" runat="server" Text="Incluir Programa de Transferência de Renda Municipal"
                                                                    SkinID="button-save" CausesValidation="False" Width="360px" PostBackUrl="~/BlocoIII/FTransferenciaRenda.aspx"></asp:Button>--%>
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






            <%--           <table class="ui-widget ui-widget-content ui-corner-all" cellspacing="2" cellpadding="0"
                width="1000" align="center" border="0">
                <tr>
                    <td class="ui-state-default ui-widget-header ui-corner-top">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="height: 30px; padding-left: 10px;">
                                    <img src="../Styles/Icones/redeprotecao.gif" align="absMiddle" />
                                    <b style="font-size: 18px;"></b>

                                </td>
                                <td align="right"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
            </table>
            <br />--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
