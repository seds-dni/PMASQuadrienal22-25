<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FParecerConselhoMunicipal.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoVIII.FParecerConselhoMunicipal" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="../Controles/cnpj.ascx" TagName="cnpj" TagPrefix="uc1" %>
<%@ Register Src="../Controles/cep.ascx" TagName="cep" TagPrefix="uc2" %>
<%@ Register Src="../Controles/telefone.ascx" TagName="telefone" TagPrefix="uc3" %>
<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                function openCustom() {
                    $.Dialog({
                        title: "<span class='mif-warning mif-ani-flash mif-ani-slow'></span>  Confirmação do Parecer",
                        content: '"Constitui responsabilidade de todos os usuários deste sistema, dentre outras coisas, zelar pela integridade, fidedignidade e disponibilidade dos dados e informações aqui contidas, além da guarda e bom uso da senha que lhe é fornecida. As informações declaradas pelos agentes públicos possuem fé pública e estão sujeitas a responsabilização administrativa e demais sanções cabíveis."<br/>Deseja salvar o Parecer?',
                        actions: [
                            {
                                title: "Sim",
                                cls: "fg-white bg-orange",
                                onclick: function (el) {
                                    <%=this.Page.ClientScript.GetPostBackEventReference(new PostBackOptions(this.btnSalvar))%>;
                                    $(el).data('dialog').close();
                                }
                            },
                    {
                        title: "Não",
                        cls: "js-dialog-close fg-white bg-orange"
                    }
                        ],
                        options: {
                            modal: false,
                            overlay: true,
                            overlayColor: 'op-dark',
                            overlayClickClose: false,
                            type: 'warning', // success, alert, warning, info
                            place: 'center', // center, top-left, top-center, top-right, center-left, center-right, bottom-left, bottom-center, bottom-right
                            position: 'default',
                            content: false,
                            hide: false,
                            width: '500',
                            height: '350',
                            background: 'default',
                            color: 'default',
                            //closeButton: false,
                            //windowsStyle: false,
                            //show: false,
                            //href: false,
                            contentType: 'default', // video
                            closeAction: true,
                            closeElement: ".js-dialog-close",
                        }
                    });
                    }
            </script>

            <%--       <script type="text/javascript">
                $(function () {
                    $('#MainContent_btnSalvar').click(function (e) {
                        e.preventDefault();
                        $('#dialog').dialog({
                            buttons: {
                                'Sim': function () {
                                    $(this).dialog('close');
                                    __doPostBack('ctl00$MainContent$btnSalvar', '');
                                    return true;
                                },
                                'Não': function () {
                                    $(this).dialog('close');
                                    return false;
                                }
                            }
                        });
                        $('#msg').html(;
                        $('#dialog').dialog('open');
                    });
                });
            </script>--%>
            <form name="frmPrefeitura">
                <div class="accordion" data-role="accordion" data-close-any="true">
                    <div class="frame">
                        <div class="heading">
                            8.3 - Parecer do Conselho Municipal de Assistência Social sobre o PMAS
                           <span class="mif-bubbles icon"></span><a style="float: right; margin-right: 5%;" href="#" runat="server" id="linkAlteracoesQuadro72" visible="false">
                               <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                           </a>&nbsp;
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="CMAS">
                                <div class="grid">
                                    <div class="row">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Dados sobre controle social:</b></legend>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>O CMAS acompanhou a execução do PMAS de 2021?</b><br />
                                                    <asp:RadioButtonList ID="rblAvaliandoExecucao" runat="server" RepeatDirection="Horizontal"
                                                        TabIndex="3">
                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                        <asp:ListItem Value="0" Selected="True">Não</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Comentários:</b><br />
                                                    <div class="input-control textarea full-size">
                                                        <asp:TextBox ID="txtComentarioAvaliandoExecucao" runat="server" TextMode="MultiLine"
                                                            Height="49px" Width="570px" MaxLength="500" />

                                                    </div>
                                                    <skm:TextBoxCounter ID="NameCountertxtComentarioAvaliandoExecucao" runat="server"
                                                        DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 500 caracteres."
                                                        TextBoxControlId="txtComentarioAvaliandoExecucao" MaxCharacterLength="500" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>O CMAS acompanhou o repasse de recursos financeiros para a rede executora?</b><br />
                                                    <asp:RadioButtonList ID="rblrecursofinanceiro" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                        <asp:ListItem Value="0" Selected="True">Não</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Comentários:</b><br />
                                                    <div class="input-control textarea full-size">
                                                        <asp:TextBox ID="txtComentarioRecursoFinanceiro" runat="server" TextMode="MultiLine"
                                                            Height="49px" Width="570px" MaxLength="500" />
                                                    </div>
                                                    <skm:TextBoxCounter ID="TextBoxCountertxtComentarioRecursoFinanceiro" runat="server"
                                                        DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 500 caracteres."
                                                        Font-Size="9px" TextBoxControlId="txtComentarioRecursoFinanceiro" MaxCharacterLength="500" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>O CMAS acompanhou as prestações de contas?</b><br />
                                                    <asp:RadioButtonList ID="rblprestacaoconta" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                        <asp:ListItem Value="0" Selected="True">Não</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Comentários:</b><br />
                                                    <div class="input-control textarea full-size">
                                                        <asp:TextBox ID="txtPrestacaoConta" runat="server" TextMode="MultiLine" Height="49px"
                                                            Width="570px" MaxLength="500" />
                                                    </div>
                                                    <skm:TextBoxCounter ID="TextBoxCountertxtPrestacaoConta" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 500 caracteres."
                                                        Font-Size="9px" TextBoxControlId="txtPrestacaoConta" MaxCharacterLength="500" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>O CMAS efetuou acompanhamento da rede executora?</b><br />
                                                    <asp:RadioButtonList ID="rblredeexecutora" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                        <asp:ListItem Value="0" Selected="True">Não</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Comentários:</b><br />
                                                    <div class="input-control textarea full-size">
                                                        <asp:TextBox ID="txtRedeExecutora" runat="server" TextMode="MultiLine" Height="49px"
                                                            Width="570px" MaxLength="500" />
                                                    </div>
                                                    <skm:TextBoxCounter ID="TextBoxCountertxtRedeExecutora" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 500 caracteres."
                                                        TextBoxControlId="txtRedeExecutora" MaxCharacterLength="500" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Houve participação do CMAS no planejamento das ações para o PMAS 2022/2025?</b><br />
                                                    <asp:RadioButtonList ID="rblParticipacaoPlanejamentoAcoes" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                        <asp:ListItem Value="0" Selected="True">Não</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Comentários:</b><br />
                                                    <div class="input-control textarea full-size">
                                                        <asp:TextBox ID="txtParticipacaoPlanejamentoAcoes" runat="server" TextMode="MultiLine" Height="49px"
                                                            Width="570px" MaxLength="500" />
                                                    </div>
                                                    <skm:TextBoxCounter ID="TextBoxCountertxtParticipacaoPlanejamentoAcoes" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 500 caracteres."
                                                        TextBoxControlId="txtParticipacaoPlanejamentoAcoes" MaxCharacterLength="500" />
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="row">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Parecer:</b></legend><b>Descreva abaixo o parecer do CMAS sobre o Plano Municipal de Assistência Social – PMAS 2022/2025, 
                                                assim como quaisquer outros comentários que julgar pertinentes:</b><br />
                                            <div class="input-control textarea full-size">
                                                <asp:TextBox ID="txtParecer" runat="server" TextMode="MultiLine" Height="205px" Width="694px"
                                                    MaxLength="8000" />
                                            </div>
                                            <skm:TextBoxCounter ID="TextBoxCountertxtParecer" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 8000 caracteres."
                                                TextBoxControlId="txtParecer" MaxCharacterLength="8000" />
                                        </fieldset>
                                    </div>
                                    <div class="row">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="titulo">Outras Informações sobre o Parecer do Conselho Municipal de
                                            Assistência Social sobre o PMAS</b></legend>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Data em que foi emitido o parecer sobre o PMAS 2022/2025:</b><br />
                                                    <uc4:data ID="txtData" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Número de conselheiros com direito a voto que estavam presentes na reunião em que
                                                        foi emitido o parecer:</b><br />
                                                    <div class="input-control text low-size">
                                                        <asp:TextBox ID="txtNumeroConselheiros" runat="server" MaxLength="5" />
                                                    </div>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtNumeroConselheiros" runat="server" TargetControlID="txtNumeroConselheiros" FilterType="Numbers" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>O Parecer foi registrado em Ata:</b><br />
                                                    <asp:RadioButtonList ID="rblRegistradoAta" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                        <asp:ListItem Value="0" Selected="True">Não</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Nome do presidente do CMAS ou de seu representante legal:</b><br />
                                                    <div class="input-control text mid-size">
                                                        <asp:TextBox ID="txtPresidente" runat="server" Width="344px" />
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="row">
                                        <div class="cell" align="center">
                                            <asp:Button ID="btnSalvarParecer" TabIndex="16" runat="server" Width="89px" Text="Salvar"
                                                SkinID="button-save" OnClick="btnSalvarParecer_Click"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="frame" border="0" id="tbAprovacao" runat="server" visible="false">
                        <div class="heading">
                            8.4 - Validação do Parecer do Conselho Municipal de Assistência Social sobre o PMAS 2022/2025
                           <span class="mif-bubbles icon">
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="parecer do cmas">
                                <div class="row">
                                    <div class="cell" align="center">
                                        <asp:RadioButtonList RepeatDirection="Horizontal" runat="server" ID="rblAprovacao"
                                            Style="margin-left: 0px" CellSpacing="20" Height="47px" Font-Bold="true">
                                            <asp:ListItem Selected="False" Value="1" Text="Favorável(Aprova o PMAS)"></asp:ListItem>
                                            <asp:ListItem Selected="False" Value="0" Text="Desfavorável(Rejeita o PMAS)"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell" align="center">
                                        <asp:Button ID="btnSalvar" TabIndex="16" runat="server" Width="89px" Text="Salvar"
                                            SkinID="button-save" OnClick="btnSalvar_Click" ValidationGroup="vgCampos"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
            <br />
            <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                width="750" align="center" border="0">
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
            <table width="100%" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="FConselhoMunicipal.aspx">
                            <span class="mif-arrow-left" />Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">&nbsp;
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdfId" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
