<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="VUnidadePrivada.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.VUnidadePrivada" %>

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
                    &nbsp;Locais de execução dos serviços socioassistenciais desta Organização desatvaido
                            
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
                                        <asp:Label ID="txtCNPJ" runat="server" />
                                    </div>
                                </div>
                                <div class="cell colspan2" id="trNome" runat="server" visible="false">
                                    <b>
                                        <asp:Label runat="server" ID="lblRazaoSocial" Text="Nome:"></asp:Label></b><br />
                                    <div class="input-control text full-size">
                                        <asp:Label ID="txtRazaoSocial" runat="server" MaxLength="120"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="trLocaisExecucao">
                            <div class="accordion" data-no-close="true">
                                <div class="frame active">
                                    <div class="heading">
                                        Informações sobre os locais de execução dos serviços desativados
                                                     
                                    </div>
                                    <div class="content">
                                        <div class="formInput">
                                            <div class="subgrid">
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
                                                                            <th width="50" rowspan="2">Visualizar</asp:Label>
                                                                            </th>
                                                                            <th width="60" rowspan="2">Código PMAS</asp:Label>
                                                                            </th>
                                                                            <th width="300" rowspan="2">Nome
                                                                            </th>
                                                                            <th width="150" rowspan="2">Responsável
                                                                            </th>
                                                                            <th width="240" colspan="3" style="height: 22px; padding-top: 3px;"
                                                                                valign="top">Serviços e recursos financeiros Desativados
                                                                            </th>
                                                                            <th width="50" rowspan="2">Data de Desativação
                                                                            </th>
                                                                        </tr>
                                                                        <tr class="ui-jqgrid-labels">
                                                                            <th width="70">Visualizar
                                                                            </th>
                                                                            <th width="80">Total de<br />
                                                                                serviços
                                                                            </th>
                                                                            <th width="100">Previsão<br />
                                                                                orçamentária
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
                                                                        <asp:ImageButton runat="server" ID="btnEditUnidade" ToolTip="Editar Unidade" ImageUrl="~/Styles/Icones/find.png"
                                                                            CommandName="Visualizar" Visible="false" />
                                                                    </td>
                                                                    <td>
                                                                        <%#DataBinder.Eval(Container.DataItem, "Id") %>
                                                                    </td>
                                                                    <td>
                                                                        <%#DataBinder.Eval(Container.DataItem, "Nome") %>
                                                                    </td>
                                                                    <td>
                                                                        <%#DataBinder.Eval(Container.DataItem, "Responsavel") %>&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton runat="server" ID="btnVisServicosRecursosFinanceiros" ToolTip="Visualizar Serviços e Recursos Financeiros"
                                                                            ImageUrl="~/Styles/Icones/find.png" CommandName="Servicos" />
                                                                    </td>
                                                                    <td>
                                                                        <%#DataBinder.Eval(Container.DataItem, "TotalServicosDesativados") %>
                                                                    </td>
                                                                    <td>
                                                                        <%#((Decimal)DataBinder.Eval(Container.DataItem, "PrevisaoOrcamentaria")).ToString("N2") %>
                                                                    </td>
                                                                    <td>   <%#((DateTime)DataBinder.Eval(Container.DataItem, "DataEncerramento")).ToShortDateString() %>
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
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="accordion" data-no-close="true">
                                <div class="frame active" id="frmInfoOrganizacao" runat="server" visible="false">
                                    <div class="heading">
                                        Informações sobre a Organização
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
                                                                <b>Nome Fantasia:</b>
                                                                <asp:Label ID="txtNomeFantasia" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="cell">
                                                                <b>Área preponderante de atuação:</b>
                                                                <asp:Label ID="ddlAreaAtuacao" runat="server" MaxLength="120"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row cells2">
                                                            <div class="cell">
                                                                <b>CEP:</b>
                                                                <asp:Label ID="lblCep" runat="server" />
                                                            </div>
                                                            <div class="cell">
                                                                <b>Endereço:</b>
                                                                <asp:Label ID="lblLogradouro" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="row cells3">
                                                            <div class="cell">
                                                                <b>Número:</b>
                                                                <asp:Label ID="lblNumero" runat="server" />
                                                            </div>
                                                            <div class="cell">
                                                                <b>Complemento:</b>
                                                                <asp:Label ID="lblComplemento" runat="server" />
                                                            </div>
                                                            <div class="cell">
                                                                <b>Bairro:</b>
                                                                <asp:Label ID="lblBairro" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <b>Cidade:</b>
                                                                <asp:Label ID="lblCidade" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="row cells2">
                                                            <div class="cell">
                                                                <b>Telefone fixo:</b>
                                                                <asp:Label ID="lblTelefone" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="cell">
                                                                <b>Telefone celular:</b>
                                                                <asp:Label ID="lblCelular" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row cells2">
                                                            <div class="cell">
                                                                <b>Email:</b>
                                                                <asp:Label runat="server" ID="txtEmail"></asp:Label>
                                                            </div>
                                                            <div class="cell">
                                                                <b>Site:</b>
                                                                <asp:Label runat="server" ID="txtHomePage"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                                <div class="row" runat="server" id="trDadosResponsavel" visible="false">
                                                    <fieldset>
                                                        <legend><b class="titulo">Dados do responsável</b></legend>
                                                        <div class="row cells2">
                                                            <div class="cell">
                                                                <b>Nome:</b>
                                                                <asp:Label runat="server" ID="txtResponsavel"></asp:Label>
                                                            </div>
                                                            <div class="cell">
                                                                <b>Cargo:</b>
                                                                <asp:Label runat="server" ID="txtCargo"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="row cells2">
                                                            <div class="cell">
                                                                <b>Início do mandato:</b>
                                                                <asp:Label ID="txtDataInicioMandato" runat="server" />
                                                            </div>
                                                            <div class="cell">
                                                                <b>Término do mandato:</b>
                                                                <asp:Label ID="txtDataTerminoMandato" runat="server" />
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
                                                        <div class="row" id="trAtendimento" runat="server" visible="false">
                                                            <div class="cell">
                                                                <asp:Label ID="chkAtendimento" runat="server" Text="De Atendimento" />
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" id="trUnidadeAtendimento" visible="false">
                                                            <div class="row" style="padding-left: 20px;">
                                                                <div class="cell">
                                                                    <b>O atendimento desta unidade refere-se a:</b><br />
                                                                    <asp:Label ID="chkServicosSocioassistencial" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" id="trAssessoramento" visible="false">
                                                            <div class="cell">
                                                                <asp:Label ID="chkAssessoramento" runat="server" Text="De Assessoramento" />
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" id="trCaracterizacaoAtividades" visible="false">
                                                            <div class="cell">
                                                                <b>As atividades desta entidade caracterizam-se como:</b><br />
                                                                <asp:Label ID="chkCaracterizacaoAtividades" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" id="trPublicoAlvo" visible="false">
                                                            <div class="cell">
                                                                <b>Público alvo:</b>
                                                                <br />
                                                                <asp:Label ID="chkPublicoAlvo" RepeatColumns="2" RepeatDirection="Horizontal" runat="server"></asp:Label>
                                                                <%--    <asp:Label ID="txtPublicoAlvo" runat="server"></asp:Label>--%>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="trDefesaDireitos" runat="server">
                                                            <div class="cell">
                                                                <asp:Label ID="chkDefesaDireitos" runat="server" Text="De Defesa e Garantia de Direitos" />
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" id="trCaracterizacaoDefesa" visible="false">
                                                            <div class="cell">
                                                                <b>As atividades desta entidade caracterizam-se como:</b><br />
                                                                <asp:Label ID="chkCaracterizacaoAtividadesDefesa" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" id="trPublicoAlvoDefesa" visible="false">
                                                            <div class="cell">
                                                                <b>Público alvo:</b>
                                                                <br />
                                                                <asp:Label ID="chkPublicoAlvoDefesa" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="row" id="trSedeAdministrativa" runat="server">
                                                            <div class="cell">
                                                                <asp:Label ID="chkSedeAdministrativa" runat="server" Text="Somente sede Administrativa" />
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                                <div class="row" runat="server" id="trCMAS" visible="false">
                                                    <fieldset>
                                                        <legend><b class="titulo">Inscrição no CMAS </b></legend>
                                                        <div class="row cells4">
                                                            <div class="cell">
                                                                <b>Situação da Inscrição:</b>
                                                                <br />
                                                                <div class="input-control select">
                                                                    <asp:Label ID="ddlSituacaoInscricao" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="cell">
                                                                <%-- <b>Data de publicação:</b>--%><br />
                                                                <asp:Label ID="txtDataPublicacao" runat="server" />
                                                            </div>
                                                            <div class="cell">
                                                                <b>
                                                                    <asp:Label ID="lblNumeroInscricao" runat="server" Text="Número de inscrição no CMAS:" /></b>
                                                                <div class="input-control text">
                                                                    <asp:Label ID="txtInscricaoCMAS" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="cell">
                                                                <b>
                                                                    <asp:Label ID="lblSituacaoAtual" runat="server" Text="Situação Atual"></asp:Label></b><br />
                                                                <div class="input-control select">
                                                                    <asp:Label ID="ddlSituacaoAtual" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                                <div class="row" runat="server" id="trDesativacao">
                                                    <fieldset>
                                                        <legend><b class="titulo">Desativação desta Organização</b></legend>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <b>Data da desativação do registro:</b>&nbsp;<asp:Label ID="lblDataDesativacao" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="cell">
                                                                <b>A desativação deste registro é devida a:</b>
                                                                <br />
                                                                    <asp:Label ID="lblMotivoDesativacao" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" id="trDataEncerramento" visible="false">
                                                            <div class="cell">
                                                                <b>
                                                                    <asp:Label ID="lblDescricaDataEncerramento" runat="server" /></b>&nbsp;<asp:Label ID="lblDataEncerramento" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="row" id="trMotivoEncerramento" runat="server" visible="false">
                                                            <div class="cell">
                                                                <b>O encerramento deveu-se a:&nbsp;</b><br />
                                                                <asp:Label ID="lblEncerramento" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="row" id="trDetalhamento" runat="server" visible="false">
                                                            <div class="cell">
                                                                <b>
                                                                    <asp:Label ID="lblDescricaoDetalhamento" runat="server" />&nbsp;</b><br />
                                                                <asp:Label ID="lblDetalhamento" runat="server" />
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
                        <div class="row">
                            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" PostBackUrl="~/BlocoIII/CUnidadesPrivadasDesativadas.aspx"
                                OnClick="btnVoltar_Click" />
                        </div>
                    </div>
                </div>
                </div>
            </form>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
