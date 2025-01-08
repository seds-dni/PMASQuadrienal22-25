<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Pendencias.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Pendencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form name="frm">
        <div class="accordion">
            <div class="frame active">
                <div class="heading">
                    Pendências do Plano Municipal de Assistência Social de 
                <asp:Label ID="lblMunicipio" runat="server" />
                </div>
                <div class="content">
                    <div class="formInput" data-text="Pendências">
                        <div class="grid">
                            <div class="row">
                                <div class="cell">
                                    <b>Legenda:</b><br />
                                    <i>- Informações consistentes =&nbsp;</i><span class="mif-checkmark fg-green"></span><br />
                                    <i>- Informações inconsistentes =&nbsp;</i><span class="mif-cross fg-red"></span><br />
                                    </ul>
                                </div>
                            </div>
                            <div class="row cells2">
                                <div class="cell">
                                    <div class="treeview" data-role="treeview">
                                        <ul>
                                            <li class="node collapsed">
                                                <span class="leaf"><span class="mif-contacts-dialer mif-2x fg-lightGreen"></span><b>&nbsp;1 - Identificação</b></span>
                                                <span class="node-toggle"></span>
                                                <ul>
                                                    <li><span class="leaf">Identificação da Prefeitura municipal &nbsp;<span id="imgMunicipio" runat="server"></span></span></li>
                                                    <li><span class="leaf">Identificação do Prefeito &nbsp;<span id="imgPrefeito" runat="server"></span></span>
                                                    </li>
                                                    <li><span class="leaf">Identificação do Orgão Gestor da Assistencia Social &nbsp;<span id="imgOrgaoGestor" runat="server"></span></span></li>
                                                    <li><span class="leaf">Estrutura e Recursos Humanos do Órgão Gestor &nbsp;<span id="imgRH" runat="server"></span></li>
                                                    <li><span class="leaf">Identificação do Gestor Municipal de Assistência Social &nbsp;<span id="imgGestorMunicipal" runat="server"></span></li>
                                                    <li><span class="leaf">Identificação do Fundo Municipal de Assistência Social &nbsp;<span id="imgFundoMunicipal" runat="server"></span></li>
                                                    <li><span class="leaf">Identificação do Gestor do Municipal de Assistência Social &nbsp;<span id="imgGestorFundoMunicipal" runat="server"></span></li>
                                                    <li><span class="leaf">Conselhos existentes no município &nbsp;<span id="imgConselhosMunicipais" runat="server"></span></li>
                                                </ul>
                                            </li>
                                            <li class="node collapsed">
                                                <span class="leaf"><span class="mif-earth2 mif-2x fg-lightGreen"></span><b>&nbsp;2 - Análise diagnóstica</b></span>
                                                <span class="node-toggle"></span>
                                                <ul>
                                                    <li><span class="leaf">Território e demografia &nbsp;<span id="imgTerritorioDemografia" runat="server"></span></span></li>
                                                    <li><span class="leaf">População e vulnerabilidade social &nbsp;<span id="imgPopulacaoVulnerabilidade" runat="server"></span></span></li>
                                                    <li><span class="leaf">Evolução da rede de atendimento &nbsp;<span id="imgEvolucaoRedeAtendimento" runat="server"></span></span></li>
                                                    <li><span class="leaf">Situações de vulnerabilidade &nbsp;<span id="imgSituacaoVulnerabilidade" runat="server"></span></li>
                                                    <li><span class="leaf">Análise e Interpretação &nbsp;<span id="imgAnaliseInterpretacao" runat="server"></span></span></li>
                                                </ul>
                                            </li>
                                            <li class="node collapsed">
                                                <span class="leaf"><span class="mif-organization mif-2x fg-lightGreen"></span><b>&nbsp;3 - Rede de proteção social</b></span>
                                                <span class="node-toggle"></span>
                                                <ul>
                                                    <li><span class="leaf">CRAS &nbsp;<span id="imgCRAS" runat="server"></span></span></li>
                                                    <li><span class="leaf">CREAS &nbsp;<span id="imgCREAS" runat="server"></span></span></li>
                                                    <li><span class="leaf">Centro POP &nbsp;<span id="imgCentroPOP" runat="server"></span></span></li>
                                                    <li><span class="leaf">Outros Locais de execução da direta &nbsp;<span id="imgExecutoraPublica" runat="server"></span></span></li>
                                                    <li><span class="leaf">Outros Locais de execução da indireta &nbsp;<span id="imgExecutoraPrivada" runat="server"></span></span></li>
                                                    <li><span class="leaf">Programas / Projetos &nbsp;<span id="imgProgramas" runat="server"></span></span></li>
                                                    <li><span class="leaf">Benefícios Eventuais&nbsp;<span id="imgBeneficios" runat="server"></span></span></li>
                                                    <li><span class="leaf">Benefícios Continuados &nbsp;<span id="imgBeneficiosContinuados" runat="server"></span></span></li>
                                                </ul>
                                            </li>
                                            <li class="node collapsed">
                                                <span class="leaf"><span class="mif-insert-template mif-2x fg-lightGreen"></span><b>&nbsp;4 - Interfaces com outras políticas públicas</b></span>
                                                <span class="node-toggle"></span>
                                                <ul>
                                                    <li><span class="leaf">Educação &nbsp;<span id="imgEducacao" runat="server"></span></span></li>
                                                    <li><span class="leaf">Saúde &nbsp;<span id="imgSaude" runat="server"></span></span></li>
                                                    <li><span class="leaf">Segurança Alimentar &nbsp;<span id="imgAlimentacao" runat="server"></span></span></li>
                                                    <li><span class="leaf">Emprego, trabalho e renda &nbsp;<span id="imgEmprego" runat="server"></span></span></li>
                                                    <li><span class="leaf">Outras políticas públicas &nbsp;<span id="imgOutrasPoliticas" runat="server"></span></span></li>
                                                </ul>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="cell">
                                    <div class="treeview" data-role="treeview">
                                        <ul>
                                            <li class="node collapsed">
                                                <span class="leaf"><span class="mif-dollar2 mif-2x fg-lightGreen"></span><b>&nbsp;5 - Financiamento</b></span>
                                                <span class="node-toggle"></span>
                                                <ul>
                                                    <li><span class="leaf">Fontes de recursos do FMAS &nbsp;<span id="imgFontesFinanciamento" runat="server"></span></span></li>
                                                    <li><span class="leaf">Cronograma de Desembolso - Proteção Social Básica &nbsp;<span id="imgCronogramaBasica" runat="server"></span></li>
                                                    <li><span class="leaf">Cronograma de Desembolso - Proteção Social Especial de Média Complexidade &nbsp;<span id="imgCronogramaMedia" runat="server"></span></li>
                                                    <li><span class="leaf">Cronograma de Desembolso - Proteção Social Especial de Alta Complexidade  &nbsp;<span id="imgCronogramaAlta" runat="server"></span></li>
                                                    <li><span class="leaf">Cronograma de Desembolso - Programas e Projetos &nbsp;<span id="imgCronogramaProgramas" runat="server"></span></li>
                                                    <li><span class="leaf">Cronograma de Desembolso - Benefícios Eventuais  &nbsp;<span id="imgCronogramaBeneficios" runat="server"></span></li>
                                                </ul>
                                            </li>
                                            <li class="node collapsed">
                                                <span class="leaf"><span class="mif-list-numbered mif-2x fg-lightGreen"></span><b>&nbsp;6 - Planejamento</b></span>
                                                <span class="node-toggle"></span>
                                                <ul>
                                                    <li><span class="leaf">Descrições das ações &nbsp;<span id="imgAcoes" runat="server"></span></span></li>
                                                </ul>
                                            </li>
                                            <li class="node collapsed">
                                                <span class="leaf"><span class="mif-meter mif-2x fg-lightGreen"></span><b>&nbsp;7 - Vigilância Socioassistencial </b></span>
                                                <span class="node-toggle"></span>
                                                <ul>
                                                    <li><span class="leaf">Vigilância socioassistencial &nbsp;<span id="imgVigilancia" runat="server"></span></span></li>
                                                    <li><span class="leaf">Monitoramento  &nbsp;<span id="imgMonitoramento" runat="server"></span></span></li>
                                                    <li><span class="leaf">Avaliação &nbsp;<span id="imgAvaliacao" runat="server"></span></span></li>
                                                    <li><span class="leaf">Aspectos Gerais&nbsp;<span id="imgAspectosGerais" runat="server"></span></li>
                                                </ul>
                                            </li>
                                            <li class="node collapsed" id="bloco7" runat="server" visible="false">
                                                <span class="leaf"><span class="mif-users mif-2x fg-lightGreen"></span><b>&nbsp;4 - Interfaces com outras políticas públicas</b></span>
                                                <span class="node-toggle"></span>
                                                <ul>
                                                    <li><span class="leaf">Educação &nbsp;<span id="imgCMAS" runat="server"></span></span></li>
                                                    <li><span class="leaf">Saúde &nbsp;<span id="imgParecerCMAS" runat="server"></span></span></li>
                                                </ul>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell">
                                    <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                        width="100%" class="bg-yellow-2  fg-black" style="border: 1px dashed blue" align="center" border="0">
                                        <tr>
                                            <td style="padding: 15px 10px 2px 15px">
                                                <span class="mif-warning mif-2x"></span>
                                                <b style='color: #000000 !important'>Verifique
                                                    as inconsistências:</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 10px 10px 12px 45px; ">
                                                <asp:Label ID="lblInconsistencias" ForeColor="Red" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell">
                                    <table id="tbAlertas" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                        width="100%" align="center" border="0">
                                        <tr>
                                            <td class="titulo" style="padding: 2px 10px 2px 10px;">
                                                  <span class="mif-compass fg-red mif-2x"></span><b style='color: #000000 !important'> &nbsp;Alertas:</b>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblAlertas" ForeColor="Red" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
