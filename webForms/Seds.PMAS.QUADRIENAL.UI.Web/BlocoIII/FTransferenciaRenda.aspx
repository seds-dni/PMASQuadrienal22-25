<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FTransferenciaRenda.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.FTransferenciaRenda" %>

<%@ Register Src="../Controles/telefone.ascx" TagName="telefone" TagPrefix="uc3" %>
<%@ Register Src="../Controles/celular.ascx" TagName="celular" TagPrefix="uc5" %>
<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--    <style>
        .Peti { }
    </style>--%>
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server" >
        <ContentTemplate>
            <form name="frmProgramaProjeto">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <asp:Label ID="lblNumeracao" runat="server" Text="14" />
                            - 
                            <asp:Label ID="lblTitulo" runat="server" Text="Informações sobre a Transferência de Renda"></asp:Label>
                            <a href="#" runat="server" id="linkAlteracoesQuadro" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>&nbsp;
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Programas e Projetos">
                                <div class="grid">
                                    <div class="row" id="trNomePrograma" runat="server" visible="false">
                                        <div class="cell">
                                            <b>
                                                <asp:Label ID="lblNomePrograma" runat="server" Text="Nome do Programa:"></asp:Label></b><br />
                                            <div class="input-control text">
                                                <asp:Label ID="lblNome" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtNome" runat="server" MaxLength="120" Width="490px" Visible="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="cell">
                                            <b>Descrição:</b><br />
                                            <div class="input-control textarea">
                                                <asp:Label ID="lblObjetivo" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtObjetivo" runat="server" Width="668px" TextMode="MultiLine" Height="51px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="cell" id="divBeneficiarios" runat="server">
                                            <b>Beneficiários:</b><br />
                                            <div class="input-control select mid-size">
                                                <asp:Label ID="lblBeneficiarios" runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddlBeneficiarios" runat="server" Visible="false" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell" id="divCriterioElegibilidade" runat="server" visible ="false">
                                            <b>Critérios de elegibilidade:</b><br />
                                            <div class="input-control textarea">
                                                <p>
												Para ser elegível ao auxílio-aluguel, a mulher deve cumprir as seguintes condições: 
												<br />Possuir medida protetiva em vigor;
												<br />Comprovar renda familiar anterior à separação de até 2 (dois) salários-mínimos;
												<br />Residir no Estado de São Paulo;
												<br /><br />Comprovar estar em situação de vulnerabilidade por meio de:
												<br />Relatório psicossocial emitido pelo serviço de assistência social municipal;
												<br />Inscrição no Cadastro Único - CadÚnico;
												<br />Declaração da própria beneficiária.
												<p/>
                                            </div>
                                        </div>
                                    </div>	
                                    <div class="row" id="trAdesaoPrograma" runat="server" visible="false">
                                        <div class="cell">
                                            <asp:Label ID="lblPerguntaAdesao" Font-Bold="true" runat="server" />
                                            <br />
                                            <asp:RadioButtonList ID="rblAdesaoPrograma" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblAdesaoPrograma_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Text="Sim" />
                                                <asp:ListItem Value="0" Text="Não" />
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                    <div class="row" id="trDataAdesao" runat="server" visible="false">
                                        <div class="cell">
                                            <asp:Label ID="lblDescAdesao" runat="server" Font-Bold="true" /><uc4:data ID="txtDataAdesao" runat="server" />
                                        </div>
                                    </div>

                                    <div class="row" id="trAderiuCofinanciamentoPeti" runat="server" visible="false">
                                        <div class="cell" align="left" valign="top">
                                            <b>O município aderiu ao termo de aceite federal para desenvolver ações estratégicas do PETI?</b><br />
                                            <asp:RadioButtonList ID="rblAderiuCofinanciamentoPeti" runat="server" RepeatDirection="Horizontal"
                                                AutoPostBack="true" OnSelectedIndexChanged="rblAderiuCofinanciamentoPeti_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Text="Sim" />
                                                <asp:ListItem Value="0" Text="Não" Selected="True" />
                                            </asp:RadioButtonList>
                                            <div class="row" runat="server" id="divAderiuCofinanciamentoPeti" visible="false">
                                                <div class="cell">
                                                    <b>Data de assinatura do Termo de Aceite:</b><br />
                                                    <uc4:data ID="txtPetiDataAdesao" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" id="trAEPETI" runat="server" visible="true">
                                        <div class="cell">
                                            <b>Saldo existente para o exercício de 2024:</b>
                                            <div class="input-control text low-size">
                                                <asp:TextBox ID="txtValorAEPETI" runat="server" Width="100"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="trAEPETI2" runat="server" visible="true">
                                        <div class="cell">
                                            <b>Saldo existente para o exercício de 2025:</b>
                                            <div class="input-control text low-size">
                                                <asp:TextBox ID="txtValorAEPETI2" runat="server" Width="100"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="trGestorPETI" runat="server" visible="false">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Gestão municipal para o combate ao trabalho infantil</b></legend>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Informe o nome do técnico responsável pelas ações da gestão de combate ao trabalho infantil:</b>
                                                    <div class="input-control text mid-size">
                                                        <asp:TextBox ID="txtNomeGestorPETI" runat="server"></asp:TextBox>
                                                    </div>
                                                    <asp:CheckBox ID="chkNaoPossuiGestorPETI" OnCheckedChanged="chkNaoPossuiGestorPETI_CheckedChanged" AutoPostBack="true" runat="server" Text="Não há técnico responsável por estas ações" />
                                                </div>
                                            </div>
                                            <fieldset class="border-blue">
                                                <legend class="lgnd"><b class="fg-blue">Contato</b></legend>
                                                <div class="row cells3">
                                                    <div class="cell">
                                                        <b>Telefone:</b><br />
                                                        <uc3:telefone ID="txtTelefone" runat="server" />
                                                    </div>
                                                    <div class="cell colspan2">
                                                        <b>Celular:</b><br />
                                                        <uc5:celular ID="txtCelular" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <b>E-mail:</b><br />
                                                        <div class="input-control text mid-size">
                                                            <asp:TextBox ID="txtEmailGestorPETI" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                            <div class="row">
                                                <div class="cell">
                                                    <table border="0" cellpadding="0" cellspacing="0" class="table border bordered mid-size">
                                                        <thead class="info">
                                                            <tr align="center">
                                                                <td colspan="6">Número de crianças e adolescentes em situação de trabalho infantil registradas no CadÚnico
                                                                            <br />
                                                                    (mês de referência: junho)
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="height: 25px;">Ano</td>
                                                                <td align="center">10 a 13 anos </td>
                                                                <td id="tdItensDespesa" runat="server" align="center">14 a 15 anos </td>
                                                                <td align="center" style="height: 22px;">16 a 17 anos </td>
                                                                <td id="tdInvestimento" runat="server" align="center">Total </td>
                                                                <td width="150" align="center">Meta municipal<br />
                                                                    até 2025</td>
                                                            </tr>
                                                        </thead>
                                                        <tr>
                                                            <td align="right"><b>2021</b> </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtIdade1013AnosExercicio0" runat="server" Width="70" ></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtIdade1415AnosExercicio0" runat="server" Width="70" ></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtIdade1617AnosExercicio0" runat="server" Width="70" ></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalExercicio0" runat="server" Text="0"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtMetaMunicipalExercicio0" runat="server" Width="70" ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="min-height: 22px;">
                                                            <td align="right"><b>2022</b> </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtIdade1013AnosExercicio1" runat="server" Width="70"></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtIdade1415AnosExercicio1" runat="server" Width="70"></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtIdade1617AnosExercicio1" runat="server" Width="70"></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:Label ID="lblTotalExercicio1" runat="server" Text="0"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtMetaMunicipalExercicio1" runat="server" Width="70"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right"><b>2023</b> </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtIdade1013AnosExercicio2" runat="server" Width="70" Enabled="false" OnTextChanged="txtIdade1013AnosExercicio2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtIdade1415AnosExercicio2" runat="server" Width="70" Enabled="false" OnTextChanged="txtIdade1415AnosExercicio2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                            <td id="Td2" runat="server" align="center">
                                                                <asp:TextBox ID="txtIdade1617AnosExercicio2" runat="server" Width="70" Enabled="false" OnTextChanged="txtIdade1617AnosExercicio2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                            <td id="tdTotalExecucaoPublica3" runat="server" align="center">
                                                                <asp:Label ID="lblTotalExercicio2" runat="server" Width="100px" Text="0"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtMetaMunicipalExercicio2" runat="server" Width="70" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right"><b>2024</b> </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtIdade1013AnosExercicio3" runat="server" Width="70" Enabled="false" ></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtIdade1415AnosExercicio3" runat="server" Width="70" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtIdade1617AnosExercicio3" runat="server" Width="70" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td runat="server" align="center">
                                                                <asp:Label ID="lblTotalExercicio3" runat="server" Width="100px" Text="0"></asp:Label>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txtMetaMunicipalExercicio3" runat="server" Width="70" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>O município realiza planejamento de ações e metas com vistas à erradicação do trabalho infantil?</b><br />
                                                    <asp:RadioButtonList ID="rblAcoesPeti" runat="server" RepeatDirection="Horizontal"
                                                        AutoPostBack="true" OnSelectedIndexChanged="rblAcoesPeti_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" Text="Sim" />
                                                        <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                    </asp:RadioButtonList>
                                                    <div class="row" width="100%" runat="server" id="divAcoesPeti" visible="false">
                                                        <fieldset class="border-blue">
                                                            <legend class="lgnd"><b class="fg-blue">Planejamento de ações e metas</b></legend>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Eixo:</b>
                                                                    <div class="input-control select mid-size">
                                                                        <asp:DropDownList ID="ddlEixoAtuacaoPeti" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEixoAtuacaoPeti_SelectedIndexChanged" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Ação:</b>
                                                                    <div class="input-control select mid-size">
                                                                        <asp:DropDownList ID="ddlTipoAcaoPeti" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Período de realização:</b>
                                                                    <div class="input-control select low-size">
                                                                        <asp:DropDownList ID="ddlPeriodoRealizacao" runat="server">
                                                                            <asp:ListItem Value="0">[Escolha uma Opção]</asp:ListItem>
                                                                            <asp:ListItem Value="2026">Contínuo até 2025</asp:ListItem>
                                                                            <asp:ListItem Value="2022">2022</asp:ListItem>
                                                                            <asp:ListItem Value="2023">2023</asp:ListItem>
                                                                            <asp:ListItem Value="2024">2024</asp:ListItem>
                                                                            <asp:ListItem Value="2025">2025</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <asp:Button ID="btnAdicionarAcaoPeti" runat="server" SkinID="button-add" Text="Adicionar"
                                                                        Width="100px" OnClick="btnAdicionarAcaoPeti_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <asp:ListView ID="lstAcoesPETI" runat="server">
                                                                        <LayoutTemplate>
                                                                            <table class="table border bordered" cellspacing="0"
                                                                                cellpadding="0" border="0">
                                                                                <thead class="info">
                                                                                    <tr>
                                                                                        <th width="20"></th>
                                                                                        <th width="330">Ação
                                                                                        </th>
                                                                                        <th width="150">Período de realização
                                                                                        </th>
                                                                                        <th width="50" align="center">Excluir
                                                                                        </th>
                                                                                        <th width="0" style="display: none;"></th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server">
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr style="height: 22px;">
                                                                                <td colspan="5">
                                                                                    <b>Eixo de Atuação:</b>
                                                                                    <%#DataBinder.Eval(Container.DataItem, "Key") %>
                                                                                </td>
                                                                            </tr>
                                                                            <asp:ListView ID="lstItemsAcoesPETI" runat="server" DataSource='<%#Eval("Items") %>'
                                                                                OnItemDataBound="lstItemsAcoesPETI_ItemDataBound" OnItemCommand="lstItemsAcoesPETI_ItemCommand">
                                                                                <LayoutTemplate>
                                                                                    <tr id="itemPlaceholder" runat="server">
                                                                                    </tr>
                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td style="height: 22px;">
                                                                                            <asp:Label ID="lblSequencia" runat="server" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <%#DataBinder.Eval(Container.DataItem, "PETITipoAcao.Nome") %>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblPeriodoRealizacao" runat="server" />
                                                                                            <%-- <%#DataBinder.Eval(Container.DataItem, "PeriodoRealizacao" ) %>--%>
                                                                                        </td>
                                                                                        <td class="align-center">
                                                                                            <asp:ImageButton ID="btnExcluir" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                                                CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('Deseja realmente remover a ação?');" />
                                                                                        </td>
                                                                                        <td style="display: none;">
                                                                                            <asp:Label ID="lblIndex" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </div>
                                                            </div>

                                                        </fieldset>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" runat="server" id="divPetiDadosAntigos" visible="false">
                                                <div class="cell">
                                                    <div class="row">
                                                        <div class="cell">
                                                            <b>Número de beneficiários:</b><br />
                                                            <asp:TextBox ID="txtPetiNumeroBeneficiarios" Style="text-align: right" runat="server"
                                                                MaxLength="6" Width="72px"></asp:TextBox>
                                                            (referência: julho/2016)
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell">
                                                            <b>Previsão mensal do valor do repasse para ações socioeducativas:</b><br />
                                                            R$
                                                                <asp:TextBox ID="txtPetiMensalRepasse" Style="text-align: right" runat="server" MaxLength="6"
                                                                    Width="72px"></asp:TextBox>
                                                            (referência: julho/2016)
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell" align="left" valign="top">
                                                            <b>Previsão anual do valor do repasse para ações socioeducativas:</b><br />
                                                            R$
                                                                <asp:Label ID="lblPetiPrevisaoAnual" runat="server"></asp:Label>&nbsp; (Previsão
                                                                mensal x 12 meses)
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>

                                    </div>

                                    <div id="trInterlocutorMunicipal" runat="server" visible="false" >
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">
                                                 <asp:Label ID="lblQaudro2" runat="server" Text="Interlocutor Municipal" /></b>
                                            </legend>
                                            <div class="cell">
                                                <div class="row" style="margin-left:1%">
                                                    <b>Nome do técnico responsável pelo programa:</b><br />
                                                    <asp:TextBox runat="server" ID="txtNomeTecnico" Width="400px"></asp:TextBox><br />
                                                    <asp:CheckBox runat="server" ID="chkNaoHaTecnico"  OnCheckedChanged="chkNaoHaTecnico_CheckedChanged" AutoPostBack="true" Text="Não há técnico responsável por este programa."/>
                                                </div>
                                            </div>
                                            <div class="row cells2">
                                                <div class="cell">
                                                    <b>Telefone fixo:</b><br />
                                                    <uc3:telefone ID="Telefone" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    <b>Telefone celular:</b><br />
                                                    <uc5:celular ID="Celular" runat="server" />
                                                </div>
                                            </div>
                                            <div class="cell">
                                                <div class="input-control email" style="margin-left:1%;margin-bottom:1%">
                                                    <b>E-mail Institucional:</b><br />
                                                    <asp:TextBox runat="server" ID="txtEmailInstitucional"  Width="740px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>

                                    <div id="trTecnicoReferencia" runat="server" visible="false" >
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">
                                                 <asp:Label ID="lblQaudroTecnicoReferencia" runat="server" Text="Técnico de referência" /></b>
                                            </legend>
                                            <div class="cell">
                                                <div class="row" style="margin-left:1%">
                                                    <b>Nome do técnico:</b><br />
                                                    <asp:TextBox runat="server" ID="txtNomeTecnicoReferencia" Width="400px"></asp:TextBox><br />
                                                    <asp:CheckBox runat="server" ID="chkTecnicoReferencia"  OnCheckedChanged="chkTecnicoReferencia_CheckedChanged" AutoPostBack="true" Text="Não há técnico responsável por este programa."/>
                                                </div>
                                            </div>
                                            <div class="row cells2">
                                            
                                                <div class="cell input-control email" >
                                                    <b>E-mail Institucional:</b><br />
                                                    <asp:TextBox runat="server" ID="txtEmailTecnicoReferencia" placeholder=""  Width="540px"></asp:TextBox>
                                                </div>
                                            
                                                <div class="cell">
                                                    <b>Unidadede de Lotação:</b><br />
                                                    <asp:TextBox runat="server" ID="txtUnidadeLotacao" Width="540px"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="cell">
                                                    <asp:Button ID="btnAdicionarTecnico" runat="server" SkinID="button-add" Text="Adicionar"
                                                        Width="100px" onclick="btnAdicionarTecnico_Click" />                                                
                                                </div>	
                                            </div>

                                        </fieldset>
                                        <div class="row" runat="server" id="trListTecnicoReferencia" visible="false">
                                            <div class="cell">
                                                <asp:ListView ID="lstTecnicoReferencia" runat="server" OnItemDataBound="lstTecnicoReferencia_ItemDataBound"
                                                    OnItemCommand="lstTecnicoReferencia_ItemCommand">
                                                    <LayoutTemplate>
                                                        <table class="table border bordered" cellspacing="0"
                                                            cellpadding="0" border="0">
                                                            <thead class="info">
                                                                <tr>
                                                                    <th width="20" style="height: 22px;"></th>
                                                                    <th width="220">Nome do Tecnico
                                                                    </th>
                                                                    <th width="250">E-mail
                                                                    </th>
                                                                    <th width="125">Unidade de Lotação
                                                                    </th>
                                                                    <th width="50">Excluir
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
                                                            <td align="left">
                                                                <%#DataBinder.Eval(Container.DataItem, "NomeTecnico") %>
                                                            </td>
                                                            <td align="left">
                                                                <%#DataBinder.Eval(Container.DataItem, "NomeEmail") %>
                                                            </td>
                                                            <td align="left">
                                                                <%#DataBinder.Eval(Container.DataItem, "NomeUnidadeLotacao") %>
                                                            </td>
                                                            <td class="align-center">
                                                                <asp:ImageButton ID="btnExcluirTecnico" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                    CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('Deseja realmente remover o tecnico ?');" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>

                                    </div>

                                    <div id="trMetasAtendimento" class="row" runat="server" visible="false">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">
                                                <asp:Label ID="lblQuadro" runat="server" Text="Metas e atendimento" /></b></legend>

                                            <div class="row" id="divBeneficiariosBPCIdosoPessoaDeficiencia" visible="false"
                                                runat="server">
                                                <div class="cell">
                                                    <div class="row" id="trBPCIdosoNumeroAtendidos">
                                                        <div class="cell mid-size">
                                                            <table class="table striped border bordered" cellspacing="0"
                                                                cellpadding="0" border="0" width="400">
                                                                <thead class="info">
                                                                    <tr>
                                                                        <th width="40" style="height: 22px;"
                                                                            rowspan="2">Exercício</th>
                                                                        <th width="80" rowspan="2">Número de beneficiários
                                                                        </th>
                                                                        <th width="80" rowspan="2">Previsão anual do valor do repasse
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr align="center">
                                                                        <td width="40">
                                                                            <asp:Label ID="Label1" Text="2022" runat="server"></asp:Label></td>
                                                                        <td width="150">
                                                                            <asp:TextBox ID="txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio1" Enabled="false" Width="150" runat="server" />
                                                                        </td>
                                                                        <td width="150">
                                                                            <asp:Label ID="lblBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio1" runat="server" Text="0,00" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <asp:Label ID="Label3" Width="150" Text="2023" runat="server" /></td>
                                                                        <td width="150">
                                                                            <asp:TextBox ID="txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio2" Width="150" Enabled="true" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio2" runat="server" Text="0,00" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <asp:Label ID="Label5" Width="150" Text="2024" runat="server" /></td>
                                                                        <td width="150">
                                                                            <asp:TextBox ID="txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio3" Width="150" Enabled="false" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio3" runat="server" Text="0,00" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <asp:Label ID="Label7" Width="150" Text="2025" runat="server" /></td>
                                                                        <td width="150">
                                                                            <asp:TextBox ID="txtBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio4" Width="150" Enabled="false" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblBPCIdosoPessoaDeficienciaNumeroAtendidosExercicio4" runat="server" Text="0,00" />
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>

                                                        
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell">
                                                            <b>Previsão anual de valor de repasse:</b>(SM X 12 meses X nº de beneficiários)
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row mid-size" align="left" id="divBolsaFamilia" visible="false" runat="server">
                                                <div class="cell">
                                                    <table class="table border bordered"cellspacing="0" border="0">
                                                        <thead class="info">
                                                            <tr>
                                                                <th style=" width:180px">Referência: Mês de Junho</th>
                                                                <th style=" width:180px">2021</th>
                                                                <th style=" width:180px">2022</th>
                                                                <th style=" width:180px">2023</th>
                                                                <th style=" width:180px">2024</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td><div style=" width:300px"> Famílias em situação de extrema 
                                                                    pobreza cadastradas no Cadúnico : </div></td>
                                                                <td>
                                                                    <div class="input-control text low-size">
                                                                        <asp:TextBox ID="txtBolsaFamiliaEstimativaFamiliasExercicio0" Style="text-align: right" runat="server"
                                                                            MaxLength="30" Width="180" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="input-control text low-size">
                                                                        <asp:TextBox ID="txtBolsaFamiliaEstimativaFamiliasExercicio1" Style="text-align: right" runat="server"
                                                                            MaxLength="30" Width="180" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="input-control text low-size">
                                                                        <asp:TextBox ID="txtBolsaFamiliaEstimativaFamiliasExercicio2" Style="text-align: right" runat="server"
                                                                            MaxLength="30" Width="180" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="input-control text low-size">
                                                                        <asp:TextBox ID="txtBolsaFamiliaEstimativaFamiliasExercicio3" Style="text-align: right" runat="server"
                                                                            MaxLength="30" Width="180" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>  
                                                                     Famílias em situação de pobreza cadastradas no Cadúnico:
                                                                </td>
                                                                <td>
                                                                    <div class="input-control text low-size">
                                                                        <asp:TextBox ID="txtBolsaFamiliaCadastradasExercicio0" Style="text-align: right" runat="server"
                                                                            MaxLength="30" Width="180px" ></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="input-control text low-size">
                                                                        <asp:TextBox ID="txtBolsaFamiliaCadastradasExercicio1" Style="text-align: right" runat="server"
                                                                            MaxLength="30" Width="180px" ></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="input-control text low-size">
                                                                        <asp:TextBox ID="txtBolsaFamiliaCadastradasExercicio2" Style="text-align: right" runat="server"
                                                                            MaxLength="30" Width="180px" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="input-control text low-size">
                                                                        <asp:TextBox ID="txtBolsaFamiliaCadastradasExercicio3" Style="text-align: right" runat="server"
                                                                            MaxLength="30" Width="180px" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Número de famílias beneficiárias:</td>
                                                                <td>
                                                                    <div class="input-control text low-size">
                                                                        <asp:TextBox ID="txtBolsaFamiliaFamiliasBeneficiariasExercicio0" Style="text-align: right"
                                                                            runat="server" MaxLength="30" Width="180px" ></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="input-control text low-size">
                                                                        <asp:TextBox ID="txtBolsaFamiliaFamiliasBeneficiariasExercicio1" Style="text-align: right"
                                                                            runat="server" MaxLength="6" Width="180px" ></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="input-control text low-size">
                                                                        <asp:TextBox ID="txtBolsaFamiliaFamiliasBeneficiariasExercicio2" Style="text-align: right"
                                                                            runat="server" MaxLength="30" Width="180px" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="input-control text low-size">
                                                                        <asp:TextBox ID="txtBolsaFamiliaFamiliasBeneficiariasExercicio3" Style="text-align: right"
                                                                            runat="server" MaxLength="30" Width="180px" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Repasse mensal:</td>
                                                                <td>
                                                                    <div class="input-control text low-size">
                                                                        <asp:TextBox ID="txtBolsaFamiliaRepasseMensalExercicio0" Style="text-align: right" runat="server"
                                                                             Width="180px" ></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="input-control text low-size">
                                                                        <asp:TextBox ID="txtBolsaFamiliaRepasseMensalExercicio1" Style="text-align: right" runat="server"
                                                                           Width="180px" ></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="input-control text low-size">
                                                                        <asp:TextBox ID="txtBolsaFamiliaRepasseMensalExercicio2" Style="text-align: right" runat="server"
                                                                            Width="180px" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="input-control text low-size">
                                                                        <asp:TextBox ID="txtBolsaFamiliaRepasseMensalExercicio3" Style="text-align: right" runat="server"
                                                                            Width="180px" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Previsão anual do valor do repasse:</td>
                                                                <td align="right">
                                                                    <asp:Label ID="lblBolsaFamiliaPrevisaoAnualExercicio0" runat="server" Width="73px" /></td>
                                                                <td align="right">
                                                                    <asp:Label ID="lblBolsaFamiliaPrevisaoAnualExercicio1" runat="server" Width="73px" /></td>
                                                                <td align="right">
                                                                    <asp:Label ID="lblBolsaFamiliaPrevisaoAnualExercicio2" runat="server" Width="120px" /></td>
                                                                <td align="right">
                                                                    <asp:Label ID="lblBolsaFamiliaPrevisaoAnualExercicio3" runat="server" Width="73px" /></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>

                                            </div>

                                            <div class="row" align="left" id="divPETI" visible="false" runat="server">
                                                <div class="cell">
                                                   
                                                </div>
                                            </div>

                                            <div class="row" align="left" id="divAcaoRenda" visible="false" runat="server">
                                                <div class="cell">
                                                    <div class="row mid-size" id="trPrevisaoAnual" runat="server" visible="true">
                                                        <div class="cell">
                                                            <table class="table striped border bordered" cellspacing="0"
                                                                cellpadding="0" border="0" width="400">
                                                                <thead class="info">
                                                                    <tr>
                                                                        <th width="40" style="height: 22px;"
                                                                            rowspan="2">Exercício</th>
                                                                        <th width="80" rowspan="2" id="thTituloMetaPactuada" runat="server" >
                                                                            <asp:Label ID="lblTituloMetaPactuada" runat="server"></asp:Label>
                                                                        </th>
                                                                        <th width="80" rowspan="2" runat="server" id="thTituloNumeroAtendidos">Média mensal de atendidos
                                                                        </th>
                                                                        <th width="80" rowspan="2">Previsão anual do valor do repasse
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr align="center">
                                                                        <td width="40">
                                                                            <asp:Label ID="lblExercicioExercicio0" Text="2021" runat="server"></asp:Label></td>
                                                                        <td width="150">
                                                                            <asp:TextBox ID="txtMetaPactuadaExercicio0" Width="150" runat="server" Enabled="false" />
                                                                        </td>
                                                                        <td width="150">
                                                                            <asp:TextBox ID="txtmediaMensalExercicio0" Width="150" runat="server" />
                                                                        </td>
                                                                        <td width="150">
                                                                            <asp:Label ID="txtPrevisaoAnualExercicio0" runat="server" Text="0,00" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td width="40">
                                                                            <asp:Label ID="lblExercicioExercicio1" Text="2022" runat="server"></asp:Label></td>
                                                                        <td width="150">
                                                                            <asp:TextBox ID="txtMetaPactuadaExercicio1" Width="150" runat="server"  />
                                                                        </td>
                                                                        <td width="150">
                                                                            <asp:TextBox ID="txtmediaMensalExercicio1" Width="150" runat="server" />
                                                                        </td>
                                                                        <td width="150">
                                                                            <asp:Label ID="txtPrevisaoAnualExercicio1" runat="server" Text="0,00" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <asp:Label ID="lblExercicioExercicio2" Width="150" Text="2023" runat="server" /></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtMetaPactuadaExercicio2" runat="server" Width="150" Enabled="true" />
                                                                        </td>
                                                                        <td width="150">
                                                                            <asp:TextBox ID="txtmediaMensalExercicio2" Width="150" Enabled="false" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="txtPrevisaoAnualExercicio2" runat="server" Text="0,00" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <asp:Label ID="lblExercicioExercicio3" Width="150" Text="2024" runat="server" /></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtMetaPactuadaExercicio3" runat="server" Enabled="false" Width="150" />
                                                                        </td>
                                                                        <td width="150">
                                                                            <asp:TextBox ID="txtmediaMensalExercicio3" Width="150" Enabled="false" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="txtPrevisaoAnualExercicio3" runat="server" Text="0,00" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <asp:Label ID="lblExercicioExercicio4" Width="150" Text="2025" runat="server" /></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtMetaPactuadaExercicio4" Width="150" runat="server" Enabled="false" />
                                                                        </td>
                                                                        <td width="150">
                                                                            <asp:TextBox ID="txtmediaMensalExercicio4" Width="150" Enabled="false" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="txtPrevisaoAnualExercicio4" runat="server" Text="0,00" />
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                   
                                                    <div class="row">
                                                        <div class="cell" align="left">
                                                            <b>Previsão anual do valor do repasse: &nbsp; (R$
                                                    80,00 x 12 meses x Média mensal de atendidos)</b><br />
                                                            <asp:Label ID="lblAcaoRendaPrevisaoAnual" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" align="left" id="divProsperaFamilia" visible="false" runat="server">
                                                <div class="cell">
                                                    <div class="row mid-size" id="trPrevisaoAnualProsperaFamilia" runat="server" visible="true">
                                                        <div class="cell">
                                                            <table class="table striped border bordered" cellspacing="0"
                                                                cellpadding="0" border="0" width="400">
                                                                <thead class="info">
                                                                    <tr>
                                                                        <th width="40" style="height: 22px;"
                                                                            rowspan="2">Exercício</th>
                                                                        <th width="80" rowspan="2" id="thDemandaEstimada" runat="server">
                                                                            <asp:Label ID="lblDemandaEstimada" runat="server">Demanda Estimada</asp:Label>
                                                                        </th>
                                                                        <th width="80" rowspan="2" id="thNumeroAtendidos" runat="server">Número de Atendidos
                                                                        </th>
                                                                        <th width="80" rowspan="2">Valor do Repasse Estadual
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr align="center" id="trPP2021" runat="server">
                                                                        <td width="40" id="tdExercicio2021">
                                                                            <asp:Label ID="Label4" Text="2021" runat="server"></asp:Label></td>
                                                                        <td width="150"  id="tdMetaPactuada2021" runat="server">
                                                                            <asp:TextBox ID="txtMetaPactuada2021" Width="150" runat="server" Enabled="false" />
                                                                        </td>
                                                                        <td width="150" id="tdNumeroAtendidos2021" runat="server">
                                                                            <asp:TextBox ID="txtNumeroAtendidos2021" Width="150" runat="server" Enabled="false"/>
                                                                        </td>
                                                                        <td width="150" id="tdValorRepasseEstadual2021"  runat="server">
                                                                            <asp:TextBox ID="txtValorRepasseEstadual2021" Width="150" Enabled="false" runat="server"/>
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td width="40">
                                                                            <asp:Label ID="Label8" Text="2022" runat="server"></asp:Label></td>
                                                                        <td width="150" id="tdMetaPactuada2022" runat="server">
                                                                            <asp:TextBox ID="txtMetaPactuada2022" Width="150" runat="server" Enabled="false"/>
                                                                        </td>
                                                                        <td width="150" id="tdNumeroAtendidos2022" runat="server">
                                                                            <asp:TextBox ID="txtNumeroAtendidos2022" Width="150" runat="server" Enabled="false"/>
                                                                        </td>
                                                                        <td width="150">
                                                                            <asp:TextBox ID="txtValorRepasseEstadual2022" Width="150" Enabled="false" runat="server"  />
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <asp:Label ID="Label10" Width="150" Text="2023" runat="server" /></td>
                                                                        <td id="tdMetaPactuada2023" runat="server">
                                                                            <asp:TextBox ID="txtMetaPactuada2023" runat="server" Width="150" Enabled="false" />
                                                                        </td>
                                                                        <td width="150" id="tdNumeroAtendidos2023" runat="server">
                                                                            <asp:TextBox ID="txtNumeroAtendidos2023" Width="150" Enabled="false" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                           <asp:TextBox ID="txtValorRepasseEstadual2023" Width="150" Enabled="false" runat="server"  />
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <asp:Label ID="Label12" Width="150" Text="2024" runat="server" /></td>
                                                                        <td id="tdMetaPactuada2024" runat="server">
                                                                            <asp:TextBox ID="txtMetaPactuada2024" runat="server" Enabled="false" Width="150" />
                                                                        </td>
                                                                        <td width="150" id="tdNumeroAtendidos2024" runat="server">
                                                                            <asp:TextBox ID="txtNumeroAtendidos2024" Width="150" Enabled="false" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtValorRepasseEstadual2024" Width="150" Enabled="false" runat="server"  />
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <asp:Label ID="Label14" Width="150" Text="2025" runat="server" /></td>
                                                                        <td id="tdMetaPactuada2025" runat="server">
                                                                            <asp:TextBox ID="txtMetaPactuada2025" Width="150" runat="server" Enabled="false" />
                                                                        </td>
                                                                        <td width="150" id="tdNumeroAtendidos2025" runat="server">
                                                                            <asp:TextBox ID="txtNumeroAtendidos2025" Width="150" Enabled="false" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtValorRepasseEstadual2025" Width="150" Enabled="false" runat="server"  />
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                   
                                                </div>
                                            </div>
                                            <div class="row" id="divAuxilioAluguel" visible="false"
                                                runat="server">
                                                <div class="cell">
                                                    <div class="row" id="trAuxilioAluguelNumeroAtendidos">
                                                        <div class="cell mid-size">
                                                            <table class="table striped border bordered" cellspacing="0"
                                                                cellpadding="0" border="0" width="400">
                                                                <thead class="info">
                                                                    <tr>
                                                                        <th width="40" style="height: 22px;"
                                                                            rowspan="2">Exercício</th>
                                                                        <th width="80" rowspan="2">Número de beneficiárias
                                                                        </th>
																		
                                                                        <th width="80" rowspan="2">Beneficiárias Ativas
                                                                        </th>																		
																		
                                                                        <th width="80" rowspan="2">Beneficiárias que já receberam todas as parcelas
                                                                        </th>
																		
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <asp:Label ID="Label2" Width="250" Text="2024" runat="server" />
                                                                        </td>
                                                                        <td width="150">
                                                                            <asp:TextBox ID="txtAuxilioAluguelNumeroAtendidosExercicio3" Width="250" runat="server" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxAuxilioAluguelNumeroAtendidosExercicio3" runat="server" FilterType="Numbers" TargetControlID="txtAuxilioAluguelNumeroAtendidosExercicio3" />
                                                                        </td>
                                                                        <td width="150">
                                                                            <asp:TextBox ID="txtAuxilioAluguelAtivasExercicio3" Width="250" runat="server" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxAuxilioAluguelAtivasExercicio3" runat="server" FilterType="Numbers" TargetControlID="txtAuxilioAluguelAtivasExercicio3" />
                                                                        </td>																		
                                                                        <td>
                                                                            <asp:Textbox ID="txtAuxilioAluguelRecebidasExercicio3" runat="server" Width="250"/>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxAuxilioAluguelRecebidasExercicio3" runat="server" FilterType="Numbers" TargetControlID="txtAuxilioAluguelRecebidasExercicio3" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr align="center">
                                                                        <td>
                                                                            <asp:Label ID="Label6" Width="250" Text="2025" runat="server" /></td>
                                                                        <td width="150">
                                                                            <asp:TextBox ID="txtAuxilioAluguelNumeroAtendidosExercicio4" Width="250" runat="server" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxAuxilioAluguelNumeroAtendidosExercicio4" runat="server" FilterType="Numbers" TargetControlID="txtAuxilioAluguelNumeroAtendidosExercicio4" />
                                                                        </td>
                                                                        <td width="150">
                                                                            <asp:TextBox ID="txtAuxilioAluguelAtivasExercicio4" Width="250" runat="server" />
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxAuxilioAluguelAtivasExercicio4" runat="server" FilterType="Numbers" TargetControlID="txtAuxilioAluguelAtivasExercicio4" />
                                                                        </td>																		
                                                                        <td>
                                                                            <asp:TextBox ID="txtAuxilioAluguelRecebidasExercicio4" runat="server" Width="250"/>
                                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxAuxilioAluguelRecebidasExercicio4" runat="server" FilterType="Numbers" TargetControlID="txtAuxilioAluguelRecebidasExercicio4" />
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>

                                                        
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell">
                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" align="left" id="divProgramasMunicipais" visible="false" runat="server">
                                                <div class="cell">
                                                    <div class="row">
                                                        <div class="cell">
                                                            <b>Número de beneficiários:</b><br />
                                                            <asp:TextBox ID="txtProgramasMunicipaisNumeroBeneficiarios" Style="text-align: right"
                                                                runat="server" MaxLength="6" Width="72px"></asp:TextBox>
                                                            (referência: julho/2016)
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell">
                                                            <b>Previsão mensal do valor do repasse:</b><br />
                                                            R$
                                                    <asp:TextBox ID="txtProgramasMunicipaisValorRepasse" Style="text-align: right" runat="server"
                                                        MaxLength="6" Width="72px"></asp:TextBox>
                                                            (referência: julho/2016)
                                                        </div>
                                                    </div>
                                                    <div class="cell">
                                                        <div class="cell" align="left">
                                                            <b>Previsão anual do valor do repasse:</b><br />
                                                            R$
                                                    <asp:Label ID="lblProgramasMunicipaisAnualRepasse" runat="server"></asp:Label>
                                                            &nbsp;(previsão mensal x 12 meses)
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </div>

                                    <div class="row" runat="server" id="trRecursosFinanceiros" visible="false">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Previsão da(s) fonte(s) e valores dos recursos financeiros</b></legend>
                                            <div class="row">
                                                <fieldset>
                                                    <legend><b>Municipal</b></legend>
                                                    <div class="row cells3">
                                                        <div class="cell">
                                                            FMAS:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtFMAS" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="cell">
                                                            Orçamento Municipal:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtOrcamentoMunicipal" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="cell">
                                                            Outros Fundos Municipais:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtOutrosFundosMunicipais" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                            <div class="row">
                                                <fieldset>
                                                    <legend><b>Estadual</b></legend>
                                                    <div class="row cells3">
                                                        <div class="cell">
                                                            FEAS:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtFEAS" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="cell">
                                                            Orçamento Estadual:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtOrcamentoEstadual" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="cell">
                                                            Outros Fundos Estaduais:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtOutrosFundosEstaduais" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                            <div class="row">
                                                <fieldset>
                                                    <legend><b>Federal</b></legend>
                                                    <div class="row cells3">
                                                        <div class="cell">
                                                            FNAS:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtFNAS" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="cell">
                                                            Orçamento Federal:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtOrcamentoFederal" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="cell">
                                                            Outros Fundos Federais:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtOutrosFundosFederais" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row cells3">
                                                        <div class="cell">
                                                            IGD-PBF:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtIGDPBF" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="colspan2">
                                                            IGD-SUAS:<br />
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtIGDSUAS" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </fieldset>
                                    </div>

                                    <div class="row" id="trArticulacoesPromovidas" runat="server" visible="false">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Articulações promovidas</b></legend>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Existem parcerias estabelecidas, formal ou informalmente, para a concessão deste benefício?</b><br />
                                                    <asp:RadioButtonList ID="rblParcerias" runat="server" RepeatDirection="Horizontal"
                                                        AutoPostBack="true" OnSelectedIndexChanged="rblParcerias_SelectedIndexChanged">
                                                        <asp:ListItem Value="1" Text="Sim" />
                                                        <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row" id="tbParcerias" runat="server" visible="false">
                                                <div class="cell">
                                                    <div class="row">
                                                        <div class="cell" colspan="2">
                                                            <b>Informe com quem foram formalizadas as parcerias para a execução deste programa/projeto:</b><br />
                                                            <div class="input-control select mid-size">
                                                                <asp:DropDownList ID="ddlParceria" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row cells2">
                                                        <div class="cell">
                                                            <b>Nome do Órgão:</b><br />
                                                            <div class="input-control text mid-size">
                                                                <asp:TextBox ID="txtNomeOrgao" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div class="cell">
                                                            <b>Tipo da Parceria:</b><br />
                                                            <div class="input-control select mid-size">
                                                                <asp:DropDownList ID="ddlTipoParceria" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell" align="center">
                                                            <asp:Button ID="btnAdicionarParceria" runat="server" SkinID="button-add" Text="Adicionar"
                                                                Width="100px" OnClick="btnAdicionarParceria_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell">
                                                            <asp:ListView ID="lstParcerias" runat="server" OnItemDataBound="lstParcerias_ItemDataBound"
                                                                OnItemCommand="lstParcerias_ItemCommand">
                                                                <LayoutTemplate>
                                                                    <table class="table border bordered" cellspacing="0"
                                                                        cellpadding="0" border="0">
                                                                        <thead class="info">
                                                                            <tr>
                                                                                <th width="20" style="height: 22px;"></th>
                                                                                <th width="220">Nome do Órgão
                                                                                </th>
                                                                                <th width="250">Parcerias
                                                                                </th>
                                                                                <th width="125">Tipo da Parceria
                                                                                </th>
                                                                                <th width="50">Excluir
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
                                                                        <td align="left">
                                                                            <%#DataBinder.Eval(Container.DataItem, "NomeOrgao") %>
                                                                        </td>
                                                                        <td align="left">
                                                                            <%#DataBinder.Eval(Container.DataItem, "Parceria.Nome") %>
                                                                        </td>
                                                                        <td align="left">
                                                                            <%#DataBinder.Eval(Container.DataItem, "TipoParceria.Nome") %>
                                                                        </td>
                                                                        <td class="align-center">
                                                                            <asp:ImageButton ID="btnExcluirParceria" runat="server" ImageUrl="~/Styles/Icones/editdelete.png"
                                                                                CommandName="Excluir" CausesValidation="false" OnClientClick="return confirm('Deseja realmente remover a parceria?');" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>

                                    <div class="row" id="trFEASRecursosReprogramados" runat="server" visible="false">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd">
											  <b class="fg-blue">
                                                <asp:Label ID="legendRecursosFinanceiros" runat="server" Text="Reprogramação dos recursos estaduais:"></asp:Label>
											  </b>
											</legend>
												
										    <div class="row" id="trRecursosFinanceirosAbas" runat="server">
                                                <div id="Quadrienal">
                                                    <asp:Button ID="btnExercicio1" runat="server" Width="113px" OnClick="btnExercicio1_Click" ></asp:Button>
                                                    <asp:Button ID="btnExercicio2" runat="server" Width="113px" OnClick="btnExercicio2_Click" ></asp:Button>
                                                    <asp:Button ID="btnExercicio3" runat="server" Width="113px" OnClick="btnExercicio3_Click" ></asp:Button>
                                                    <asp:Button ID="btnExercicio4" runat="server" Width="113px" OnClick="btnExercicio4_Click" ></asp:Button>
                                                </div>
                                            </div>
											
                                            <div class="row" id="trRecursosFinanceirosEstadualReprogramadoExercicio1" runat="server" visible="false">
                                                <fieldset class="border-blue">
                                                    <legend class="lgnd"><b class="fg-blue">Estadual</b></legend>
                                                    <div class="row cells3">
                                                        <div class="cell" id="tdReprogramadoFEASexercicio1" runat="server">
                                                            <br />
                                                            <p class="fg-active-black"><b>FEAS - Reprogramação</b></p>
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtFEASReprogramadoExercicio1" runat="server"/>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </fieldset>
                                            </div>											
											
                                            <div class="row" id="trRecursosFinanceirosEstadualReprogramadoExercicio2" runat="server" visible="false">
                                                <fieldset class="border-blue">
                                                    <legend class="lgnd"><b class="fg-blue">Estadual</b></legend>
                                                    <div class="row cells3">
                                                        <div class="cell" id="tdReprogramadoFEASExercicio2" runat="server">
                                                            <br />
                                                            <p class="fg-active-black"><b>FEAS - Reprogramação</b></p>
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtFEASReprogramadoExercicio2" runat="server"/>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </fieldset>
                                            </div>	
											
                                            <div class="row" id="trRecursosFinanceirosEstadualReprogramadoExercicio3" runat="server" visible="false">
                                                <fieldset class="border-blue">
                                                    <legend class="lgnd"><b class="fg-blue">Estadual</b></legend>
                                                    <div class="row cells3">
                                                        <div class="cell" id="tdReprogramadoFEASexercicio3" runat="server">
                                                            <br />
                                                            <p class="fg-active-black"><b>FEAS - Reprogramação</b></p>
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtFEASReprogramadoExercicio3" runat="server"  />
                                                            </div>
                                                        </div>

                                                    </div>
                                                </fieldset>
                                            </div>											
											
                                            <div class="row" id="trRecursosFinanceirosEstadualReprogramadoExercicio4" runat="server" visible="false">
                                                <fieldset class="border-blue">
                                                    <legend class="lgnd"><b class="fg-blue">Estadual</b></legend>
                                                    <div class="row cells3">
                                                        <div class="cell" id="tdReprogramadoFEASexercicio4" runat="server">
                                                            <br />
                                                            <p class="fg-active-black"><b>FEAS - Reprogramação</b></p>
                                                            <div class="input-control text">
                                                                <asp:TextBox ID="txtFEASReprogramadoExercicio4" runat="server"/>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </fieldset>
                                            </div>												

                                        <asp:HiddenField ID="hdfAno" runat="server" />
								    </div>
                                    
                                    <div class="row">
                                        <div class="cell" align="center">
                                            <asp:Button ID="btnSalvar" runat="server" SkinID="button-save" Text="Salvar" Width="89px"
                                                OnClick="btnSalvar_Click"></asp:Button>
                                            &nbsp;<asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click" />
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="cell">
                                            <div class="row">
                                                <div class="cell">
                                                    <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                                        width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                                                        <tr>
                                                            <td style="padding: 15px 10px 2px 15px">
                                                                <span class="mif-warning mif-2x"></span>
                                                                <b style='color: #000000 !important'>Verifique as inconsistências:</b>
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
                            </div>

                        </div>
                    </div>
                </div>

            </form>
            <asp:HiddenField ID="hdfTipoTransferenciaRenda" runat="server" Value="8" />
            <asp:HiddenField ID="hdfIdPETI" runat="server" />
            <asp:HiddenField ID="hdfIdGestorAcao" runat="server" />
            <asp:HiddenField ID="hdfMunicipio" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
