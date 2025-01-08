<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FInterfacePolitica.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIV.FInterfacePolitica" %>

<%@ Register Src="~/Controles/cep.ascx" TagName="cep" TagPrefix="uc2" %>
<%@ Register Src="../Controles/telefone.ascx" TagName="telefone" TagPrefix="uc3" %>
<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmUnidadesPublicas">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                          4.1 - Interfaces com outras políticas
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Outras Políticas">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            Criamos este novo bloco de informações no PMASWeb 2018-2021 para que possam ser registradas informações acerca das interfaces entre a Assistência Social e outras políticas públicas, através de ações, programas ou projetos.
                                            Nosso objetivo é possuir um panorama geral sobre como acontecem essas articulações nos diferentes municípios, identificar a execução de outros programas estaduais na rede de assistência social, e colaborar com possíveis ações de aprimoramento e qualificação da intersetorialidade.
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <div id="Tabs" class="tabcontrol" data-role="tabcontrol">
                                                <ul class="tabs">
                                                    <li id="frame1_1" runat="server"><a href="#frame1_1">Educação</a></li>
                                                    <li id="frame1_2" runat="server"><a href="#frame1_2">Saúde</a></li>
                                                    <li id="frame1_3" runat="server"><a href="#frame1_3">Segurança Alimentar</a></li>
                                                    <li id="frame1_4" runat="server"><a href="#frame1_4">Emprego, Trabalho e Renda</a></li>
                                                    <li id="frame1_5" runat="server"><a href="#frame1_5">Outras Políticas Públicas</a></li>
                                                </ul>
                                                <div class="frames">
                                                    <div class="frame" id="frame1_1">
                                                        <fieldset class="border-blue">
                                                            <div class="row">
                                                                <asp:HiddenField ID="hidfIdEducacao" runat="server" />
                                                                <div class="cell">
                                                                    <b class="titulo">Educação:</b>
                                                                    Este quadro destina-se à inserção de informações sobre as ações que possuem interface entre os serviços de Assistência Social e a Educação.<br />
                                                                    Permite ao município descrever as principais ações estabelecidas formal ou informalmente entre CRAS, CREAS e os serviços de educação. 
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell colspan2">
                                                                    <b>Existe protocolo formal estabelecido no município para o atendimento de usuários da Assistência Social na rede de serviços de Educação (encaminhamento e acompanhamento)?</b><br />
                                                                    <asp:RadioButtonList ID="rblProtocoloFormalEducacao" OnSelectedIndexChanged="rblProtocoloFormalEducacao_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                        <asp:ListItem Value="1" Text="Sim"></asp:ListItem>
                                                                        <asp:ListItem Value="0" Text="Não"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                            <div class="row" id="trJustificativaProtocoloEducacao" runat="server" visible="false">
                                                                <div class="cell">
                                                                    <b>
                                                                        <asp:Label ID="lblJustificativaProtocoloEducacao" runat="server"></asp:Label>
                                                                    </b>
                                                                    <br />
                                                                    <div class="input-control textarea full-size">
                                                                        <asp:TextBox TextMode="MultiLine" Height="40px" ID="txtJustificativaProtocoloEducacao" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Existem intervenções conjuntas entre Assistência Social e Educação para acompanhamento de crianças e adolescentes de famílias beneficiárias do Programa Bolsa Família?</b>
                                                                    <br />
                                                                    <asp:RadioButtonList ID="rblIntervencaoConjuntaEducao" OnSelectedIndexChanged="rblIntervencaoConjuntaEducao_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                            <div class="row" id="trJustificativaIntervencao" runat="server" visible="false">
                                                                <div class="cell">
                                                                    <b>Descrição</b><br />
                                                                    <div class="input-control textarea full-size">
                                                                        <asp:TextBox TextMode="MultiLine" Height="40px" ID="txtJustificativaIntervencao" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Existem intervenções conjuntas entre Assistência Social e Educação para acompanhamento de pessoas com deficiência beneficiárias do Benefício de Prestação Continuada – BPC?</b>
                                                                    <br />
                                                                    <asp:RadioButtonList ID="rblIntervencaoConjuntaBPC" OnSelectedIndexChanged="rblIntervencaoConjuntaBPC_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                            <div class="row" id="trJustificativaConjuntaBPC" runat="server" visible="false">
                                                                <div class="cell">
                                                                    <b>Descrição</b>
                                                                    <div class="input-control textarea full-size">
                                                                        <asp:TextBox TextMode="MultiLine" Height="40px" ID="txtJustificativaConjuntaBPC" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Existem intervenções conjuntas entre Assistência Social e Educação para acompanhamento dos jovens beneficiários do programa Ação Jovem e/ou em cumprimento de medidas socioeducativas em meio aberto?</b><br />
                                                                    <asp:RadioButtonList ID="rblIntervencaoConjuntaAcaoJovem" OnSelectedIndexChanged="rblIntervencaoConjuntaAcaoJovem_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                            <div class="row" id="trJustificativaAcaoJovem" runat="server" visible="false">
                                                                <div class="cell">
                                                                    <b>Descrição</b>
                                                                    <div class="input-control textarea full-size">
                                                                        <asp:TextBox TextMode="MultiLine" Height="40px" ID="txtJustificativaAcaoJovem" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Existem outras articulações estabelecidas entre o órgão gestor da Assistência Social e o órgão gestor da Educação em seu município? </b>
                                                                    <br />
                                                                    <asp:RadioButtonList ID="rblOutrasArticulacoesEducacao" OnSelectedIndexChanged="rblOutrasArticulacoesEducacao_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                            <div class="row" id="trJustificativaOutrasArticulacoesEducacao" runat="server" visible="false">
                                                                <div class="cell">
                                                                    <b>Descrição</b>
                                                                    <div class="input-control textarea full-size">
                                                                        <asp:TextBox TextMode="MultiLine" Height="40px" ID="txtJustificativaOutrasArticulacoesEducacao" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <asp:Button ID="btnSalvarEducacao" runat="server" Text="Salvar" OnClick="btnSalvarEducacao_Click" />
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                    <div class="frame" id="frame1_2">
                                                        <fieldset class="border-blue">
                                                            <div class="row">
                                                                <asp:HiddenField ID="hdfIdSaude" runat="server" />
                                                                <div class="cell">
                                                                    <b class="titulo">Saúde:</b>
                                                                    Este quadro destina-se à inserção de informações sobre as ações que possuem interface entre os serviços de Assistência social e os serviços ou a gestão da política pública de Saúde.<br />
                                                                    Permite ao município descrever as principais ações estabelecidas formal ou informalmente entre CRAS, CREAS, Centro Pop e/ou Serviços de Acolhimento Institucional e os serviços da rede de Saúde. 
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Existe protocolo (formal) estabelecido no município para o atendimento de usuários da assistência social na rede de serviços de saúde (encaminhamento e acompanhamento)?</b><br />
                                                                    <asp:RadioButtonList ID="rblProtocoloFormalSaude" runat="server" OnSelectedIndexChanged="rblProtocoloFormalSaude_SelectedIndexChanged" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                            <div class="row" id="trProtocoloFormalSaude" runat="server" visible="false">
                                                                <div class="cell">
                                                                    <b>
                                                                        <asp:Label ID="lblProtocoloFormalSaude" runat="server"></asp:Label>
                                                                    </b>
                                                                    <br />
                                                                    <div class="input-control textarea full-size">
                                                                        <asp:TextBox TextMode="MultiLine" Height="40px" ID="txtProtocoloFormalSaude" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Existem intervenções conjuntas entre Assistência Social e Saúde para acompanhamento de famílias beneficiárias do Programa Bolsa Família? </b>
                                                                    <br />
                                                                    <asp:RadioButtonList ID="rblIntervencaoBolsaFamilia" OnSelectedIndexChanged="rblIntervencaoBolsaFamilia_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                            <div class="row" id="trJustificativaIntervencaoBolsaFamilia" runat="server" visible="false">
                                                                <div class="cell">
                                                                    <b>Descrição:</b><br />
                                                                    <div class="input-control textarea full-size">
                                                                        <asp:TextBox TextMode="MultiLine" Height="40px" ID="txtJustificativaIntervencaoBolsaFamilia" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Existem intervenções conjuntas entre Assistência Social e Saúde para acompanhamento de pessoas idosas ou pessoas com deficiência beneficiárias do Benefício de Prestação Continuada – BPC?</b>
                                                                    <br />
                                                                    <asp:RadioButtonList ID="rblIntervencaoSaudeBPC" OnSelectedIndexChanged="rblIntervencaoSaudeBPC_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="row" id="trJustificativaIntervencaoSaudeBPC" runat="server" visible="false">
                                                                    <div class="cell">
                                                                        <b>Descrição:</b><br />
                                                                        <div class="input-control textarea full-size">
                                                                            <asp:TextBox TextMode="MultiLine" Height="40px" ID="txtJustificativaIntervencaoSaudeBPC" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Existem intervenções conjuntas entre Assistência Social e Saúde para acompanhamento de famílias com crianças e adolescentes em situação de trabalho infantil, vítimas de exploração sexual ou vítimas de violência?</b>
                                                                    <br />
                                                                    <asp:RadioButtonList ID="rblIntervencaoConjuntaVitimasExploracao" OnSelectedIndexChanged="rblIntervencaoConjuntaVitimasExploracao_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="row" id="trIntervencaoConjuntaVitimasExploracao" runat="server" visible="false">
                                                                    <div class="cell">
                                                                        <b>Descrição:</b>
                                                                        <div class="input-control textarea full-size">
                                                                            <asp:TextBox TextMode="MultiLine" Height="40px" ID="txtIntervencaoConjuntaVitimasExploracao" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Existem intervenções conjuntas entre Assistência Social e Saúde para acompanhamento de adultos, idosos ou pessoas com deficiência?</b>
                                                                    <br />
                                                                    <asp:RadioButtonList ID="rblIntervencaoConjuntaSaude" OnSelectedIndexChanged="rblIntervencaoConjuntaSaude_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="row" id="trJustificativaIntervencaoConjuntaSaude" runat="server" visible="false">
                                                                    <div class="cell">
                                                                        <b>Descrição:</b>
                                                                        <div class="input-control textarea full-size">
                                                                            <asp:TextBox TextMode="MultiLine" Height="40px" ID="txtJustificativaIntervencaoConjuntaSaude" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <asp:Button ID="btnSalvarSaude" Text="Salvar" runat="server" OnClick="btnSalvarSaude_Click" />
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                    <div class="frame" id="frame1_3">
                                                        <fieldset class="border-blue">
                                                            <div class="row">
                                                                <asp:HiddenField ID="hdfIdAlimentacao" runat="server" />
                                                                <div class="cell">
                                                                    <b class="titulo">Segurança alimentar:</b>
                                                                    A política de segurança alimentar e nutricional (Lei 11.346 de 2006) abrange, dentre outros, a ampliação das condições de acesso aos alimentos; a conservação da biodiversidade e a utilização sustentável dos recursos; 
                                                                a promoção da saúde, da nutrição e da alimentação da população, incluindo-se grupos populacionais específicos e populações em situação de vulnerabilidade social; e a produção de conhecimento e o acesso à informação.
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>O município executa alguma ação relativa a segurança alimentar? </b>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="cell">
                                                                            <asp:RadioButtonList ID="rblExecutaAcaoAlimentar" runat="server" OnSelectedIndexChanged="rblExecutaAcaoAlimentar_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                                                                <asp:ListItem Value="True">Sim</asp:ListItem>
                                                                                <asp:ListItem Value="False">Não</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row" id="trExecutaAcaoAlimentar" runat="server" visible="false">
                                                                <div class="cell">
                                                                    <b>O município executa alguma das ações abaixo? </b>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="cell">
                                                                            <asp:CheckBox ID="chkRestaurantePopular" Text="Restaurante Popular" runat="server" OnCheckedChanged="chkRestaurantePopular_CheckedChanged" AutoPostBack="true" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="treeview">
                                                                        <ul>
                                                                            <div class="row" id="frmRestaurantePopular" runat="server" visible="false">
                                                                                <fieldset class="border-blue">
                                                                                    <legend class="lgnd" style="font-size: 13px;"><b class="fg-blue">Registre as seguintes informações em relação a este restaurante bom prato</b></legend>
                                                                                    <div class="cell">
                                                                                        <div class="row">
                                                                                            <div class="cell">
                                                                                                <asp:CheckBox ID="chkNaoPossuiInformacao" runat="server" Text="Não possuo essas informações pois a gestão deste programa é feita por outra política pública" OnCheckedChanged="chkNaoPossuiInformacao_CheckedChanged" AutoPostBack="true" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="cell">
                                                                                                <asp:CheckBox ID="chkGestaoDiretaEstado" runat="server" Text="Não possuo essas informações pois a gestão desse programa é feita diretamente pelo estado" OnCheckedChanged="chkGestaoDiretaEstado_CheckedChanged" AutoPostBack="true" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="cell">
                                                                                                <b>Este restaurante é conveniado à rede Bom Prato?</b><br />
                                                                                                <asp:RadioButtonList ID="rbConvenioBomPrato" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
                                                                                                    <asp:ListItem Value="1" Text="Sim"></asp:ListItem>
                                                                                                    <asp:ListItem Value="0" Text="Não"></asp:ListItem>
                                                                                                </asp:RadioButtonList>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                           <div class="cell">
                                                                                                <b>Este restaurante mantém parceria com serviço para pessoas em situação de rua?</b><br />
                                                                                                <asp:RadioButtonList ID="rbParceria" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
                                                                                                    <asp:ListItem Value="1" Text="Sim"></asp:ListItem>
                                                                                                    <asp:ListItem Value="0" Text="Não"></asp:ListItem>
                                                                                                </asp:RadioButtonList>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="cell">
                                                                                                Nome do Restaurante:<br />
                                                                                                <div class="input-control text mid-size">
                                                                                                    <asp:TextBox ID="txtNomeRestaurante" runat="server"></asp:TextBox>
                                                                                                </div>

                                                                                            </div>
                                                                                        </div>
                                                                                        <uc2:cep ID="cep1" runat="server" />
                                                                                        <div class="row cells2">
                                                                                            <div class="cell">
                                                                                                <b>Telefone fixo:</b><br />
                                                                                                <uc3:telefone ID="telefone" runat="server" />
                                                                                            </div>
                                                                                            <div class="cell">
                                                                                                <b>Data de início das atividades</b>
                                                                                                <uc4:data ID="txtDataInicioRestaurante" runat="server" />
                                                                                            </div>
                                                                                            <div class="row">
                                                                                                <div class="cell">
                                                                                                    <b>Nome da organização/unidade responsável:</b><br />
                                                                                                    <div class="input-control text mid-size">
                                                                                                        <asp:TextBox ID="txtUnidade" runat="server"></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row">
                                                                                            <div class="cell">
                                                                                                <asp:Button ID="btnAdicionarBomPrato" runat="server" Text="Adicionar" OnClick="btnAdicionarBomPrato_Click" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row" id="trlstRestaurantes" runat="server">
                                                                                            <div class="cell">
                                                                                                <asp:ListView ID="lstRestaurantes" runat="server" OnItemCommand="lstRestaurantes_ItemCommand">
                                                                                                    <LayoutTemplate>
                                                                                                        <table border="0" cellpadding="0" cellspacing="0" class="table striped border bordered">
                                                                                                            <thead class="info">
                                                                                                                <tr>
                                                                                                                    <th width="50">Nome do Restaurante</th>
                                                                                                                    <th width="150">Nome da organização/<br />
                                                                                                                        unidade responsável</th>
                                                                                                                    <th width="50">Inicio das atividades</th>
                                                                                                                    <th width="50">Telefone</th>
                                                                                                                    <th width="100">Pertence ao Bom Prato?</th>
                                                                                                                    <th width="50">Excluir</th>
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
                                                                                                            <td align="center"><%#DataBinder.Eval(Container.DataItem, "Nome") %></td>
                                                                                                            <td><%#DataBinder.Eval(Container.DataItem, "UnidadeAtendimento") %></td>
                                                                                                            <td><%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DataInicioAtividade")).ToString("dd/MM/yyyy") %></td>
                                                                                                            <td><%#DataBinder.Eval(Container.DataItem, "TelefoneFixo").ToString() %></td>
                                                                                                            <td><%#((Boolean)DataBinder.Eval(Container.DataItem, "ConvenioBomPrato")) ? "Sim" : "Não"%></td>
                                                                                                            <td class="align-center">
                                                                                                                <asp:ImageButton ID="btnExcluir" runat="server" CausesValidation="false" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Excluir" ImageUrl="~/Styles/Icones/editdelete.png" OnClientClick="return confirm('Deseja realmente remover o serviço?');" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </ItemTemplate>
                                                                                                    <EmptyDataTemplate>
                                                                                                        <div align="center" style="width: 100%;">
                                                                                                            <br />
                                                                                                            <b class="titulo">Não existe registros de restaurantes</b> <%--     <b class="titulo">Não existe registro de serviços associados a este programa</b>--%>
                                                                                                        </div>
                                                                                                    </EmptyDataTemplate>
                                                                                                </asp:ListView>
                                                                                            </div>
                                                                                        </div>

                                                                                    </div>
                                                                                </fieldset>
                                                                            </div>
                                                                        </ul>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="cell">
                                                                            <asp:CheckBox ID="chkDistribuicaoAlimentos" Text="Distribuição de alimentos " runat="server" OnCheckedChanged="chkDistribuicaoAlimentos_CheckedChanged" AutoPostBack="true" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="treeview" id="trProgramaVivaLeite" visible="false" runat="server">
                                                                        <ul>
                                                                            <div class="row">
                                                                                <div class="cell" style="margin-left: 20px;">
                                                                                    <asp:CheckBox ID="chkVivaleite" runat="server" Text="Programa estadual Vivaleite" OnCheckedChanged="chkVivaleite_CheckedChanged" AutoPostBack="true" />
                                                                                </div>
                                                                            </div>

                                                                            <div class="row" id="trProtocoloVivaleite" runat="server" visible="false">
                                                                                <fieldset class="border-blue">
                                                                                    <legend style="font-size: 13px;" class="lgnd"><b class="fg-blue">Registre as seguintes informações em relação a gestão do programa Vivaleite</b></legend>
                                                                                    <div class="row">
                                                                                        <div class="cell">
                                                                                            <b>A gestão do programa Vivaleite é realizada pelo órgão gestor de assistência social?</b>
                                                                                            <br />
                                                                                            <asp:RadioButtonList ID="rblOrgaoGestor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblOrgaoGestor_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                                                                <asp:ListItem Text="Sim" Value="1"></asp:ListItem>
                                                                                                <asp:ListItem Text="Não" Value="0"></asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row" id="trTipoDistribuicaoLeite" runat="server" visible="false">
                                                                                        <div class="cell">
                                                                                            <b>A distribuição do leite é realizada:</b>
                                                                                            <asp:RadioButtonList ID="rblDistribuidor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblDistribuidor_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                                                                <asp:ListItem Text="pelo órgão gestor" Value="1"></asp:ListItem>
                                                                                                <asp:ListItem Text="por entidades de assistência social" Value="2"></asp:ListItem>
                                                                                                 <asp:ListItem Text="por outra política pública" Value="3"></asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row" id="trUnidadesRedeIndireta" runat="server" visible="false">
                                                                                        <div class="cell">
                                                                                            <b>Assinale quais das entidades fazem a distribuição do leite em seu município:</b><br />
                                                                                            <asp:CheckBoxList ID="cblEntidades" runat="server"></asp:CheckBoxList>
                                                                                        </div>
                                                                                    </div>
                                                                                       <div class="row" id="trOutraPolitica" runat="server" visible="false">
                                                                                        <div class="cell">
                                                                                            <b>Especifique qual política:</b><br />
                                                                                            <div class="input-control text mid-size">
                                                                                                <asp:TextBox ID="txtOutraPoliticaPublica" runat="server" MaxLength="100" Width="700"></asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </fieldset>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="cell" style="margin-left: 20px;">
                                                                                    <asp:CheckBox ID="chkOutraFormaDistribuicao" Text="Outra forma de distribuição de alimentos (Não considerar cestas básicas fornecidas como benefícios eventuais)" runat="server" AutoPostBack="true" OnCheckedChanged="chkOutraFormaDistribuicao_CheckedChanged" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="row" id="trOutraFormaDistribuicao" runat="server" visible="false">
                                                                                <fieldset class="border-blue">
                                                                                    <legend style="font-size: 13px;" class="lgnd"><b class="fg-blue">Registre as seguintes informações em relação a outra forma de distruição de alimentos</b></legend>
                                                                                    <div class="row cells3">
                                                                                        <div class="cell">
                                                                                            <b>Descrição:</b><br />
                                                                                            <div class="input-control text full-size">
                                                                                                <asp:TextBox ID="txtDescricao" runat="server"></asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="cell">
                                                                                            <b>Responsável pela ação:</b><br />
                                                                                            <div class="input-control text full-size">
                                                                                                <asp:TextBox ID="txtResponsavel" runat="server"></asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="cell padding20">
                                                                                            <asp:Button ID="btnAdicionarFormaDistribuicao" Text="Adicionar" runat="server" OnClick="btnAdicionarFormaDistribuicao_Click" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row" id="trlstOutrasFormasDistribuicao" runat="server" visible="false">
                                                                                        <div class="cell">
                                                                                            <asp:ListView ID="lstOutrasFormasDistribuicao" runat="server" OnItemCommand="lstOutrasFormasDistribuicao_ItemCommand" OnItemDataBound="lstOutrasFormasDistribuicao_ItemDataBound">
                                                                                                <LayoutTemplate>
                                                                                                    <table border="0" cellpadding="0" cellspacing="0" class="table striped border bordered">
                                                                                                        <thead class="info">
                                                                                                            <tr>
                                                                                                                <th width="20"></th>
                                                                                                                <th width="150">Descrição</th>
                                                                                                                <th width="150">Responsável pela ação</th>
                                                                                                                <th width="50">Excluir</th>
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
                                                                                                        <td><%#DataBinder.Eval(Container.DataItem, "Descricao") %></td>
                                                                                                        <td><%#DataBinder.Eval(Container.DataItem, "Responsavel") %></td>
                                                                                                        <td class="align-center">
                                                                                                            <asp:ImageButton ID="btnExcluir" runat="server" CausesValidation="false" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Excluir" ImageUrl="~/Styles/Icones/editdelete.png" OnClientClick="return confirm('Deseja realmente remover o serviço?');" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                                <EmptyDataTemplate>
                                                                                                    <div align="center" style="width: 100%;">
                                                                                                        <br />
                                                                                                        <b class="titulo">Não existe registro de informações em relação a outra forma de distruição de alimentos</b>
                                                                                                    </div>
                                                                                                </EmptyDataTemplate>
                                                                                            </asp:ListView>
                                                                                        </div>
                                                                                    </div>
                                                                                </fieldset>
                                                                            </div>
                                                                        </ul>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="cell"> 
                                                                            <asp:CheckBox ID="chkOutraAcao" runat="server" Text="Outro tipo de ação" OnCheckedChanged="chkOutraAcao_CheckedChanged" AutoPostBack="true" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" id="trOutroTipoAcao" runat="server" visible="false">
                                                                        <fieldset class="border-blue">
                                                                            <legend style="font-size: 13px;" class="lgnd"><b class="fg-blue">Registre as seguintes informações em relação a outro tipo de ação</b></legend>
                                                                            <div class="row cells3">
                                                                                <div class="cell">
                                                                                    <b>Identifique qual ação é desenvolvida:</b><br />
                                                                                    <div class="input-control text full-size">
                                                                                        <asp:TextBox ID="txtAcaoDesenvolvida" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="cell">
                                                                                    <b>Órgão responsável por esta ação:</b><br />
                                                                                    <div class="input-control text full-size">
                                                                                        <asp:TextBox ID="txtOrgaoResponsavelAcao" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="cell padding20">
                                                                                    <asp:Button ID="btnAdicionarOutraFormaAcao" Text="Adicionar" runat="server" OnClick="btnAdicionarOutraFormaAcao_Click" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="row" id="trlstOutrasAcoes" runat="server" visible="false">
                                                                                <div class="cell">
                                                                                    <asp:ListView ID="lstOutrasAcoes" runat="server" OnItemCommand="lstOutrasAcoes_ItemCommand" OnItemDataBound="lstOutrasAcoes_ItemDataBound">
                                                                                        <LayoutTemplate>
                                                                                            <table border="0" cellpadding="0" cellspacing="0" class="table striped border bordered">
                                                                                                <thead class="info">
                                                                                                    <tr>
                                                                                                        <th width="20"></th>
                                                                                                        <th width="150">Ação desenvolvida</th>
                                                                                                        <th width="150">Orgão Responsável pela ação</th>
                                                                                                        <th width="50">Excluir</th>
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
                                                                                                <td><%#DataBinder.Eval(Container.DataItem, "AcaoDesenvolvida") %></td>
                                                                                                <td><%#DataBinder.Eval(Container.DataItem, "OrgaoResponsavel") %></td>
                                                                                                <td class="align-center">
                                                                                                    <asp:ImageButton ID="btnExcluir" runat="server" CausesValidation="false" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Excluir" ImageUrl="~/Styles/Icones/editdelete.png" OnClientClick="return confirm('Deseja realmente remover a ação?');" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                        <EmptyDataTemplate>
                                                                                            <div align="center" style="width: 100%;">
                                                                                                <br />
                                                                                                <b class="titulo">Não existe registro de ações desenvolvidas para esta interface</b>
                                                                                            </div>
                                                                                        </EmptyDataTemplate>
                                                                                    </asp:ListView>
                                                                                </div>
                                                                            </div>
                                                                        </fieldset>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <asp:Button ID="btnSalvarAlimentacao" runat="server" Text="Salvar" OnClick="btnSalvarAlimentacao_Click" />
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                    <div class="frame" id="frame1_4">
                                                        <fieldset class="border-blue">
                                                            <div class="row">
                                                                <asp:HiddenField ID="hdfIdEmprego" runat="server" />
                                                                <div class="cell">
                                                                    <b class="titulo">Emprego, Trabalho e Renda:</b>Este quadro destina-se à inserção de informações sobre as ações que possuem interface entre os serviços de Assistência Social e políticas para o Emprego, Trabalho e Renda. 
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Existem intervenções conjuntas entre Assistência Social e a política de Emprego, Trabalho e Renda para inserção de jovens no mundo do trabalho? </b>
                                                                    <br />
                                                                    <asp:RadioButtonList ID="rblIntervencaoPoliticaEmprego" OnSelectedIndexChanged="rblIntervencaoPoliticaEmprego_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="row" id="trJustificativaIntervencaoPoliticaEmprego" runat="server" visible="false">
                                                                    <div class="cell">
                                                                        <b>Descrição:</b>
                                                                        <div class="input-control textarea full-size">
                                                                            <asp:TextBox TextMode="MultiLine" Height="40px" ID="txtJustificativaIntervencaoPoliticaEmprego" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Existem intervenções conjuntas entre Assistência Social e a política de Emprego, Trabalho e Renda para inserção de pessoas com deficiência no mundo do trabalho?</b>
                                                                    <br />
                                                                    <asp:RadioButtonList ID="rblIntervencaoPoliticaEmpregoPCD" OnSelectedIndexChanged="rblIntervencaoPoliticaEmpregoPCD_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="row" id="trJustificativaIntervencaoPoliticaEmpregoPCD" runat="server" visible="false">
                                                                    <div class="cell">
                                                                        <b>Descrição:</b>
                                                                        <div class="input-control textarea full-size">
                                                                            <asp:TextBox TextMode="MultiLine" Height="40px" ID="txtJustificativaIntervencaoPoliticaEmpregoPCD" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Existem no município outras ações pactuadas/articuladas entre a política de Emprego, Trabalho e Renda e a Assistência Social?</b>
                                                                    <br />
                                                                    <asp:RadioButtonList ID="rblOutrasAcoesEmprego" OnSelectedIndexChanged="rblOutrasAcoesEmprego_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="row" id="trJustificativaOutrasAcoesEmprego" runat="server" visible="false">
                                                                    <div class="cell">
                                                                        <b>Descrição:</b>
                                                                        <div class="input-control textarea full-size">
                                                                            <asp:TextBox TextMode="MultiLine" Height="40px" ID="txtOutrasAcoesEmprego" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <asp:Button ID="btnSalvarEmprego" runat="server" Text="Salvar" OnClick="btnSalvarEmprego_Click" />
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                    <div class="frame" id="frame1_5">
                                                        <fieldset class="border-blue">
                                                            <div class="row">
                                                                <asp:HiddenField ID="hdfIdInterfacePublica" runat="server" />
                                                                <div class="cell">
                                                                    <b>Existem outras políticas públicas, além das citadas acima, que possuem interface com a política de assistência social, isto é, que possuem interação por meio de protocolos, fluxos ou acordos intersetoriais?</b>
                                                                    <br />
                                                                    <asp:RadioButtonList ID="rblOutrasPoliticasPublicas" OnSelectedIndexChanged="rblOutrasPoliticasPublicas_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="row" id="trOutrasPoliticasPublicas" runat="server" visible="false">
                                                                    <div class="cell">
                                                                        <b>Quais?</b>
                                                                        <div class="input-control textarea full-size">
                                                                            <asp:TextBox TextMode="MultiLine" Height="40px" ID="txtOutrasPoliticasPublicas" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Quais os principais obstáculos para o estabelecimento de protocolos e ações intersetoriais entre Assistência Social e outras políticas públicas no seu município?</b>
                                                                    <br />
                                                                    <div class="input-control textarea full-size">
                                                                        <asp:TextBox TextMode="MultiLine" Height="40px" ID="txtPrincipaisObstaculos" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Existem serviços, programas ou projetos de outras políticas públicas que são financiados com recursos da política de Assistência Social?</b>
                                                                    <br />
                                                                    <asp:RadioButtonList ID="rblOutrosProgramasFinanciados" OnSelectedIndexChanged="rblOutrosProgramasFinanciados_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="row" id="trOutrosProgramasFinanciados" runat="server" visible="false">
                                                                    <div class="cell">
                                                                        <div class="row">
                                                                            <b>Qual o serviço/programa/projeto?</b><br />
                                                                            <div class="cell">
                                                                                <div class="input-control text full-size">
                                                                                    <asp:TextBox ID="txtProgramaProjeto" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <b>A qual política pública ele pertence?</b><br />
                                                                            <div class="cell">
                                                                                <div class="input-control text full-size">
                                                                                    <asp:TextBox ID="txtPoliticaPublica" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <b>Qual o valor repassado pela política de Assistência Social? </b>
                                                                            <br />
                                                                            <div class="cell">
                                                                                <div class="input-control text">
                                                                                    <asp:TextBox ID="txtValorRepasssado" runat="server" Style="text-align: right;"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="cell">
                                                                                <asp:Button ID="btnAdicionarOutrosServicos" Text="Adicionar" runat="server" OnClick="btnAdicionarOutrosServicos_Click" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="row" id="trlstOutrosServicos" runat="server" visible="false">
                                                                            <asp:ListView ID="lstOutrosServicos" runat="server" OnItemCommand="lstOutrosServicos_ItemCommand" OnItemDataBound="lstOutrosServicos_ItemDataBound">
                                                                                <LayoutTemplate>
                                                                                    <table border="0" cellpadding="0" cellspacing="0" class="table striped border bordered">
                                                                                        <thead class="info">
                                                                                            <tr>
                                                                                                <th width="20"></th>
                                                                                                <th width="150">Serviço/Programa/Projeto</th>
                                                                                                <th width="150">Política Pública</th>
                                                                                                <th width="50">Valor Repassado</th>
                                                                                                <th width="50">Excluir</th>
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
                                                                                        <td style="height: 22px; width: 20px;">
                                                                                            <asp:Label ID="lblSequencia" runat="server" />
                                                                                        </td>
                                                                                        <td><%#DataBinder.Eval(Container.DataItem, "NomeProgramaProjeto") %></td>
                                                                                        <td><%#DataBinder.Eval(Container.DataItem, "PoliticaPublica") %></td>
                                                                                        <td align="right"><%#String.Format("{0:N2}",DataBinder.Eval(Container.DataItem, "ValorRepassePoliticaAssistencia")) %></td>
                                                                                        <td align="center">
                                                                                            <asp:ImageButton ID="btnExcluir" runat="server" CausesValidation="false" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Excluir" ImageUrl="~/Styles/Icones/editdelete.png" OnClientClick="return confirm('Deseja realmente remover o serviço?');" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <EmptyDataTemplate>
                                                                                    <div align="center" style="width: 100%;">
                                                                                        <br />
                                                                                        <b class="titulo">Não existe registro de programas / benefícios associados a este serviço</b> <%--     <b class="titulo">Não existe registro de serviços associados a este programa</b>--%>
                                                                                    </div>
                                                                                </EmptyDataTemplate>
                                                                            </asp:ListView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <asp:Button ID="btnSalvarInterface" runat="server" Text="Salvar" OnClick="btnSalvarInterface_Click" />
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <div class="row">
                                        <div class="cell">
                                            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" SkinID="button-save"
                                                Width="89px" OnClick="btnSalvar_Click" ValidationGroup="vgCampos" />
                                        </div>
                                    </div>--%>
                                    <div class="row">
                                        <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                            width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                                            <tr>
                                                <td style="padding: 15px 10px 2px 15px">
                                                    <span class="mif-warning mif-2x"></span>
                                                    <b style='color: #000000 !important'>Verifique
                                                    as inconsistências:</b>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 10px 10px 12px 45px;">
                                                    <asp:Label ID="lblInconsistencias" ForeColor="Red" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%-- </fieldset>--%>
                </div>
            </form>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
