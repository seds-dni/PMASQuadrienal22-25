<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FUnidadePrivada.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.FUnidadePrivada" %>

<%@ Register Src="../Controles/cnpj.ascx" TagName="cnpj" TagPrefix="uc1" %>
<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<%@ Register Src="../Controles/cep.ascx" TagName="cep" TagPrefix="uc2" %>
<%@ Register Src="../Controles/telefone.ascx" TagName="telefone" TagPrefix="uc3" %>
<%@ Register Src="../Controles/celular.ascx" TagName="celular" TagPrefix="uc5" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .toggle {
        }

        .displayObject {
            display: none;
        }
    </style>
    <script type="text/javascript">
         
        $(function () {
            //bindEvents();
        });

        //function bindEvents() {
        //    $(".toggle").click(function () {
        //        var opt = {};
        //        $(this).parent().parent().next().find(".displayObject").first().toggle("blind", opt, 500);
        //        if ($(this).text().indexOf("expandir") >= 0) {
        //            $(this).text($(this).text().replace("expandir", "ocultar"));
        //            sessionStorage.info = "E";
        //        }
        //        else {
        //            $(this).text($(this).text().replace("ocultar", "expandir"));
        //            sessionStorage.info = "O";
        //        }
        //    });

        //    if (sessionStorage.info) {
        //        if (sessionStorage.info === "E") {
        //            $(".toggle").trigger("click");
        //        }
        //    }

        //    window.onbeforeunload = function (e) {
        //        sessionStorage.info = null;
        //    }
        //}

        function bindEvents() {
            $(".heading").click(function () {
                var opt = {};
                //$(this).parent().parent().next().find(".displayObject").first().toggle("blind", opt, 500);
                if ($(this).text().indexOf("expandir") >= 0) {
                    $(this).text($(this).text().replace("expandir", "ocultar"));
                    sessionStorage.info = "E";
                }
                else {
                    $(this).text($(this).text().replace("ocultar", "expandir"));
                    sessionStorage.info = "O";
                }
            });
            if (sessionStorage.info) {
                if (sessionStorage.info === "E") {
                    $(".heading").trigger("click");
                }
            }

            window.onbeforeunload = function (e) {
                sessionStorage.info = null;
            }
        };
    </script>
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(bindEvents);
            </script>
            <br />
            <form name="frmUnidadePrivada">
                <div class="head">
                    &nbsp;3.12 - Locais de execução dos serviços socioassistenciais desta Organização
                            
                            <span class="mif-organization icon"></span>
                </div>
                <div class="content">
                    <div class="formInput" data-text="rede indireta">
                        <div class="grid">
                            <div class="row cell-auto-size" id="trCodigoOrganizacao" runat="server" visible="false">
                                <div class="cell">
                                    <b>Código da Organização:</b><br />
                                    <asp:Label ID="lblCodigoUnidade" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row cells4">
                                <div class="cell">
                                    <b>CNPJ:</b><br />
                                    <div class="input-control text">
                                        <uc1:cnpj ID="txtCNPJ" runat="server" />
                                    </div>
                                </div>
                                <div class="cell colspan2" id="trNome" runat="server" visible="false">
                                    <b>
                                        <asp:Label runat="server" ID="lblRazaoSocial" Text="Nome:"></asp:Label></b><br />
                                    <div class="input-control text full-size">
                                        <asp:TextBox ID="txtRazaoSocial" runat="server" MaxLength="120"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="cell" style="text-align: right;">
                                    <a href="#" runat="server" id="linkAlteracoesQuadro37" visible="false">
                                        <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                    </a>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell">
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="button-find" OnClick="btnBuscar_Click" />
                                </div>
                            </div>
                            <div class="row cell-auto-size" runat="server" id="trInformacoesNaoEncontrado" visible="false">
                                <div class="cell">
                                    <b>
                                        <asp:Label ID="lblNaoEncontrado" CssClass="titulo" Text="Esta unidade não está cadastrada na base de dados do sistema Pró Social!"
                                            Visible="false" runat="server"></asp:Label></b>
                                    <b>
                                        <asp:Label ID="Label1" CssClass="titulo" Text="Por favor, preencha os campos abaixo com as informações solicitadas:"
                                            runat="server"></asp:Label></b>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="trLocaisExecucao">
                            <div class="accordion" data-role="accordion" data-no-close="true">
                                <div class="frame">
                                    <div class="heading">
                                        Informações sobre os locais de execução dos serviços (expandir)
                                                     
                                    </div>
                                    <div class="content">
                                        <div class="formInput">
                                            <div class="subgrid">
                                                <div class="row">
                                                    <div class="cell" style="text-align: right; padding: 10px;">
                                                        <a href="#" runat="server" id="lnkAlteracoesLocais" visible="false">
                                                            <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <asp:ListView ID="lstLocais" runat="server" DataKeyNames="Id" OnItemCommand="lstLocais_ItemCommand"
                                                            OnItemDataBound="lstLocais_ItemDataBound">
                                                            <LayoutTemplate>
                                                                <table class="table striped border bordered" cellspacing="" cellspacing="0"
                                                                    cellpadding="0" border="0">
                                                                    <thead class="info">
                                                                        <tr>
                                                                            <th width="20" style="height: 22px;"
                                                                                rowspan="2"></th>
                                                                            <th width="50" rowspan="2">Visualizar/<br />
                                                                                Editar</asp:Label>
                                                                            </th>
                                                                            <th width="60" rowspan="2">Código PMAS</asp:Label>
                                                                            </th>
                                                                            <th width="300" rowspan="2">Nome
                                                                            </th>
                                                                            <th width="150" rowspan="2">Responsável
                                                                            </th>
                                                                            <th width="240" colspan="3" style="height: 22px; padding-top: 3px;"
                                                                                valign="top">Serviços e recursos financeiros
                                                                            </th>
                                                                            <th width="50" rowspan="2">Desativar
                                                                            </th>
                                                                        </tr>
                                                                        <tr class="ui-jqgrid-labels">
                                                                            <th width="70">Visualizar/<br />
                                                                                Editar
                                                                            </th>
                                                                            <th width="80">Total de<br />
                                                                                serviços
                                                                            </th>
                                                                            <th width="100">Valor do<br />
                                                                                Cofinanciamento Estadual
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
                                                                    <td style="text-align: center;">
                                                                        <asp:ImageButton runat="server" ID="btnVisUnidade" ToolTip="Visualizar Unidade" ImageUrl="~/Styles/Icones/find.png"
                                                                            CommandName="Visualizar" Visible="false" />
                                                                        <asp:ImageButton runat="server" ID="btnEditUnidade" ToolTip="Editar Unidade" ImageUrl="~/Styles/Icones/edit.png"
                                                                            CommandName="Visualizar" Visible="false" />
                                                                    </td>
                                                                    <td>
                                                                        <%#DataBinder.Eval(Container.DataItem, "Id") %>
                                                                    </td>
                                                                    <td>
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.Nome") %>
                                                                    </td>
                                                                    <td>
                                                                        <%#DataBinder.Eval(Container.DataItem, "obj.Responsavel") %>&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton runat="server" ID="btnVisServicosRecursosFinanceiros" ToolTip="Visualizar Serviços e Recursos Financeiros"
                                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" />
                                                                    </td>
                                                                    <td>
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
                                                                    <td>
                                                                        <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                            CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('Deseja realmente desativar este Local de Execução?');" />&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <div align="center" style="width: 100%;">
                                                                    <b class="titulo">Não existe registro de locais de execução</b>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="cell" align="center">
                                                        <asp:Button ID="btnLocalExecucao" runat="server" SkinID="button-save" Text="Adicionar local de execução"
                                                            OnClick="btnLocalExecucao_Click" Visible="false" Width="200"></asp:Button>&nbsp;
                                                               <asp:Button ID="btnLocaisDesativados" runat="server" SkinID="button-save" Text="Locais de execução desativados"
                                                            OnClick="btnLocaisDesativados_Click" Width="220"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="accordion" data-role="accordion" data-no-close="true">
                                <div class="frame" id="frmInfoOrganizacao" runat="server" visible="false">
                                    <div class="heading">
                                        Informações sobre a Organização (ocultar)
                                    </div>
                                    <div class="content">
                                        <div class="formInput">
                                            <div class="grid">
                                                <div class="row" runat="server" id="trSituacao" visible="false">
                                                    <fieldset>
                                                        <div class="row cells2">
                                                            <div class="cell" style="width: 30%;">
                                                                <b>Situação no Pró Social:</b>
                                                                <asp:Label runat="server" ID="lblSituacao"></asp:Label>
                                                            </div>
                                                            <div class="cell" runat="server" id="tdMotivo" visible="false">
                                                                <b>Motivo:</b>
                                                                <asp:Label runat="server" ID="lblMotivoSituacao"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                                <div class="row" runat="server" id="trDadosGerais" visible="false">
                                                    <fieldset>
                                                        <div class="row cells3">
                                                            <div class="cell colspan2">
                                                                <b>Nome Fantasia:</b><br />
                                                                <div class="input-control text full-size">
                                                                    <asp:TextBox ID="txtNomeFantasia" runat="server" MaxLength="120"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="cell">
                                                                <b>Área preponderante de atuação:</b><br />
                                                                <div class="input-control select mid-size">
                                                                    <asp:DropDownList ID="ddlAreaAtuacao" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <uc2:cep ID="cep1" runat="server" />
                                                        <div class="row cells3">
                                                            <div class="cell">
                                                                <b>Telefone:</b>
                                                                <uc3:telefone ID="txtTelefone" runat="server" />
                                                            </div>
                                                            <div class="cell colspan2">
                                                                <b>Celular:</b><br />
                                                                <uc5:celular ID="txtCelular" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="row cells2">
                                                            <div class="cell">
                                                                <b>Email:</b><br />
                                                                <div class="input-control text mid-size">
                                                                    <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="cell">
                                                                <b>Site:</b><br />
                                                                <div class="input-control text mid-size">
                                                                    <asp:TextBox runat="server" ID="txtHomePage"></asp:TextBox>
                                                                </div>
                                                                <asp:CheckBox ID="chkPossuiSite" runat="server" AutoPostBack="true" Text="Não possui site"
                                                                    OnCheckedChanged="chkPossuiSite_CheckedChanged" />
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                                <div class="row" runat="server" id="trDadosResponsavel" visible="false">
                                                    <fieldset>
                                                        <legend><b class="titulo">Dados do responsável</b></legend>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <b>Nome:</b><br />
                                                                <div class="input-control text mid-size">
                                                                    <asp:TextBox runat="server" ID="txtResponsavel"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <b>Cargo:</b><br />
                                                                <div class="input-control text mid-size">
                                                                    <asp:TextBox runat="server" ID="txtCargo"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row cells2">
                                                            <div class="cell">
                                                                <b>Início do mandato:</b><br />
                                                                <uc4:data ID="txtDataInicioMandato" runat="server" />
                                                            </div>
                                                            <div class="cell">
                                                                <br />
                                                                <b>Término do mandato:</b>
                                                                <uc4:data ID="txtDataTerminoMandato" runat="server" />
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                                <div class="row" runat="server" id="trCaracterizacaoEntidade" visible="false">
                                                    <fieldset>
                                                        <legend><b class="titulo">Caracterização da organização</b></legend>

                                                        <div class="row">
                                                            <div class="cell">
                                                                <b>De acordo com a Resolução nº 14, de 15 de maio de 2014, do CNAS, esta entidade caracteriza-se como sendo de:</b><br />
                                                            </div>
                                                        </div>
                                                        <div class="row" id="trAtendimento" runat="server">
                                                            <div class="cell">
                                                                <asp:CheckBox ID="chkAtendimento" runat="server" Text="De Atendimento" AutoPostBack="true" OnCheckedChanged="chkAtendimento_CheckedChanged" />
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" id="trUnidadeAtendimento" visible="false">
                                                            <div class="row" style="padding-left: 20px;">
                                                                <div class="cell">
                                                                    <b>O atendimento desta unidade refere-se a:</b><br />
                                                                    <asp:CheckBoxList ID="chkServicosSocioassistencial" runat="server" RepeatDirection="Horizontal" RepeatColumns="3"  AutoPostBack="true" OnSelectedIndexChanged="chkServicosSocioassistencial_SelectedIndexChanged"/>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" id="trAssessoramento">
                                                            <div class="cell">
                                                                <asp:CheckBox ID="chkAssessoramento" runat="server" Text="De Assessoramento" AutoPostBack="true" OnCheckedChanged="chkAssessoramento_CheckedChanged" />
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" id="trCaracterizacaoAtividades" visible="false">
                                                            <div class="cell">
                                                                <b>As atividades desta entidade caracterizam-se como:</b><br />
                                                                <asp:CheckBoxList ID="chkCaracterizacaoAtividades" RepeatDirection="Horizontal" RepeatColumns="2"
                                                                    runat="server">
                                                                </asp:CheckBoxList>
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" id="trPublicoAlvo" visible="false">
                                                            <div class="cell">
                                                                <b>Público alvo:</b>
                                                                <asp:ImageButton ID="btnAjudaPublicoAlvo" runat="server" ImageUrl="~/Styles/Icones/help.png"
                                                                    ImageAlign="AbsMiddle" OnClientClick="return false;" Style="background-color: transparent; border-width: 0px;" />
                                                                <asp:Panel ID="pnlAjudaPublicoAlvo" runat="server" CssClass="ajuda" Width="330px"
                                                                    Height="120px">
                                                                    <div style="float: right;">
                                                                        <asp:LinkButton ID="lnkCloseAjudaPublicoAlvo" runat="server" OnClientClick="return false;"
                                                                            Text="X" ToolTip="Fechar" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                                                                    </div>
                                                                    <div>
                                                                        <p>
                                                                            Identifique nesse campo não só o tipo de público alcançado
                                                                                <br />
                                                                            pelas ações desta unidade, mas também localize territorialmente
                                                                                <br />
                                                                            no município onde esses trabalhos acontecem.
                                                                        </p>
                                                                    </div>
                                                                </asp:Panel>
                                                                <ajaxToolkit:AnimationExtender ID="OpenAnimationPublicoAlvo" runat="server" TargetControlID="btnAjudaPublicoAlvo">
                                                                    <Animations>
                                                                            <OnClick>
                                                                                <Sequence AnimationTarget="pnlAjudaPublicoAlvo">
                                                                                <EnableAction AnimationTarget="btnAjudaPublicoAlvo" Enabled="false" />
                                                                                <StyleAction Attribute="display" Value="block" />                                    
                                                                                <Parallel>
                                                                                    <FadeIn Duration="1" Fps="20" />                                    
                                                                                </Parallel>
                                                                                </Sequence>
                                                                            </OnClick>
                                                                    </Animations>
                                                                </ajaxToolkit:AnimationExtender>
                                                                <ajaxToolkit:AnimationExtender ID="CloseAnimationPublicoAlvo" runat="server" TargetControlID="lnkCloseAjudaPublicoAlvo">
                                                                    <Animations>
                                                                        <OnClick>
                                                                            <Sequence AnimationTarget="pnlAjudaPublicoAlvo">                                                    
                                                                                <Parallel Duration=".3" Fps="15">                                                        
                                                                                    <FadeOut />
                                                                                </Parallel>                        
                                                                                <%--  Reset the sample so it can be played again --%>
                                                                                <StyleAction Attribute="display" Value="none"/>                                                                                                       
                        
                                                                                <%--  Enable the button so it can be played again --%>
                                                                                <EnableAction AnimationTarget="btnAjudaPublicoAlvo" Enabled="true" />
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
                                                                <br />
                                                                <asp:CheckBoxList ID="chkPublicoAlvo" RepeatColumns="2" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                                <%--    <asp:TextBox ID="txtPublicoAlvo" runat="server"></asp:TextBox>--%>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="trDefesaDireitos" runat="server">
                                                            <div class="cell">
                                                                <asp:CheckBox ID="chkDefesaDireitos" runat="server" Text="De Defesa e Garantia de Direitos" AutoPostBack="true" OnCheckedChanged="chkDefesaDireitos_CheckedChanged" />
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" id="trCaracterizacaoDefesa" visible="false">
                                                            <div class="cell">
                                                                <b>As atividades desta entidade caracterizam-se como:</b><br />
                                                                <asp:CheckBoxList ID="chkCaracterizacaoAtividadesDefesa" RepeatDirection="Horizontal" RepeatColumns="2"
                                                                    runat="server">
                                                                </asp:CheckBoxList>
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" id="trPublicoAlvoDefesa" visible="false">
                                                            <div class="cell">
                                                                <b>Público alvo:</b>
                                                                <asp:ImageButton ID="btnAjudaPublicoAlvoDefesa" runat="server" ImageUrl="~/Styles/Icones/help.png"
                                                                    ImageAlign="AbsMiddle" OnClientClick="return false;" Style="background-color: transparent; border-width: 0px;" />
                                                                <asp:Panel ID="pnlAjudaPublicoAlvoDefesa" runat="server" CssClass="ajuda" Width="330px"
                                                                    Height="120px">
                                                                    <div style="float: right;">
                                                                        <asp:LinkButton ID="lnkCloseAjudaPublicoAlvoDefesa" runat="server" OnClientClick="return false;"
                                                                            Text="X" ToolTip="Fechar" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                                                                    </div>
                                                                    <div>
                                                                        <p>
                                                                            Identifique nesse campo não só o tipo de público alcançado
                                                                                <br />
                                                                            pelas ações desta unidade, mas também localize territorialmente
                                                                                <br />
                                                                            no município onde esses trabalhos acontecem.
                                                                        </p>
                                                                    </div>
                                                                </asp:Panel>
                                                                <ajaxToolkit:AnimationExtender ID="OpenAnimationPublicoAlvoDefesa" runat="server" TargetControlID="btnAjudaPublicoAlvoDefesa">
                                                                    <Animations>
                                                                            <OnClick>
                                                                                <Sequence AnimationTarget="pnlAjudaPublicoAlvo">
                                                                                <EnableAction AnimationTarget="btnAjudaPublicoAlvo" Enabled="false" />
                                                                                <StyleAction Attribute="display" Value="block" />                                    
                                                                                <Parallel>
                                                                                    <FadeIn Duration="1" Fps="20" />                                    
                                                                                </Parallel>
                                                                                </Sequence>
                                                                            </OnClick>
                                                                    </Animations>
                                                                </ajaxToolkit:AnimationExtender>
                                                                <ajaxToolkit:AnimationExtender ID="CloseAnimationPublicoAlvoDefesa" runat="server" TargetControlID="lnkCloseAjudaPublicoAlvoDefesa">
                                                                    <Animations>
                                                                        <OnClick>
                                                                            <Sequence AnimationTarget="pnlAjudaPublicoAlvoDefesa">                                                    
                                                                                <Parallel Duration=".3" Fps="15">                                                        
                                                                                    <FadeOut />
                                                                                </Parallel>                        
                                                                                <%--  Reset the sample so it can be played again --%>
                                                                                <StyleAction Attribute="display" Value="none"/>                                                                                                       
                        
                                                                                <%--  Enable the button so it can be played again --%>
                                                                                <EnableAction AnimationTarget="btnAjudaPublicoAlvoDefesa" Enabled="true" />
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
                                                                <br />
                                                                <asp:CheckBoxList ID="chkPublicoAlvoDefesa" RepeatColumns="2" RepeatDirection="Horizontal" runat="server"></asp:CheckBoxList>
                                                                <%--    <asp:TextBox ID="txtPublicoAlvo" runat="server"></asp:TextBox>--%>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <asp:CheckBox ID="chkSedeAdministrativa" runat="server" Text="Somente sede Administrativa" AutoPostBack="true" />
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                                <div class="row" runat="server" id="trCMAS" visible="false">
                                                    <fieldset>
                                                        <legend><b class="titulo">Inscrição no CMAS </b></legend>
                                                        <div class="row">
                                                            <div class="cell">
                                                                Os campos a seguir são de preenchimento exclusivo do Conselho Municipal da Assistência
                                                                    Social
                                                                    <br />
                                                            </div>
                                                        </div>
                                                        <div class="row cells4">
                                                            <div class="cell">
                                                                <b>Situação da Inscrição:</b>
                                                                <br />
                                                                <div class="input-control select">
                                                                    <asp:DropDownList ID="ddlSituacaoInscricao" runat="server" OnSelectedIndexChanged="ddlSituacaoInscricao_SelectedIndexChanged"
                                                                        AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="cell">
                                                                <%-- <b>Data de publicação:</b>--%><br />
                                                                <uc4:data ID="txtDataPublicacao" runat="server" />
                                                            </div>
                                                            <div class="cell">
                                                                <b>
                                                                    <asp:Label ID="lblNumeroInscricao" runat="server" Text="Número de inscrição no CMAS:" /></b>
                                                                <div class="input-control text">
                                                                    <asp:TextBox ID="txtInscricaoCMAS" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="cell">
                                                                <b>
                                                                    <asp:Label ID="lblSituacaoAtual" runat="server" Text="Situação Atual"></asp:Label></b><br />
                                                                <div class="input-control select">
                                                                    <asp:DropDownList ID="ddlSituacaoAtual" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="frame" id="fraLocaisExecucao" runat="server" visible="false">
                                    <div class="row" id="trServicoSocioassistencial" runat="server" visible="false">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                            width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                            <tr>
                                <td style="padding: 15px 10px 2px 15px">
                                    <span class="mif-warning mif-2x"></span>
                                    <%-- <img src="../Styles/Icones/messagebox_warning.png" align="absMiddle" />--%><b style='color: #000000 !important'>Verifique
                                                    as inconsistências:</b>

                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 10px 10px 12px 45px;">
                                    <asp:Label ID="lblInconsistencias" ForeColor="Red" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <div class="row">
                            <div class="cell" align="center">
                                <asp:Button ID="btnSalvar" runat="server" SkinID="button-save" Text="Salvar" Width="89px"
                                    OnClick="btnSalvar_Click"></asp:Button>
                                &nbsp;<asp:Button ID="btnVoltar" runat="server" Text="Voltar" PostBackUrl="~/BlocoIII/CUnidadesPrivadas.aspx"
                                    OnClick="btnVoltar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="chkAtendimento"/>
            <asp:PostBackTrigger ControlID="chkServicosSocioassistencial" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
