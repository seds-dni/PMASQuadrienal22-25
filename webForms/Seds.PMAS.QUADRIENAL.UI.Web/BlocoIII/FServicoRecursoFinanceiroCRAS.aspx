<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FServicoRecursoFinanceiroCRAS.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.FServicoRecursoFinanceiroCRAS" %>

<%@ Register Src="../Controles/data.ascx" TagName="data" TagPrefix="uc4" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <script type="text/javascript">
        function CalculateTotal() {
            var txtSemEscolaridade = document.getElementById('<%=txtSemEscolaridade.ClientID%>').value;
            var txtNivelFundamental = document.getElementById('<%=txtNivelFundamental.ClientID%>').value;
            var txtNivelMedio = document.getElementById('<%=txtNivelMedio.ClientID%>').value;
            var txtSuperior = document.getElementById('<%=txtSuperior.ClientID%>').value;
            if (txtSemEscolaridade == '') { document.getElementById('<%=txtSemEscolaridade.ClientID%>').value = '0'; txtSemEscolaridade = '0' }
            if (txtNivelFundamental == '') { document.getElementById('<%=txtNivelFundamental.ClientID%>').value = '0'; txtNivelFundamental = '0'; }
            if (txtNivelMedio == '') { document.getElementById('<%=txtNivelMedio.ClientID%>').value = '0'; txtNivelMedio = '0'; }
            if (txtSuperior == '') { document.getElementById('<%=txtSuperior.ClientID%>').value = '0'; txtSuperior = '0'; }
            var valores = [txtSemEscolaridade, txtNivelMedio, txtNivelFundamental, txtSuperior];
            PageMethods.CalcularValores(valores, function (val) {
                document.getElementById('<%=lblTotalRh.ClientID%>').value = val;
                document.getElementById('<%=hidTotalEscolaridade.ClientID%>').value = val;
            });
        }

        function exibeBtnExcluirExercicio1() {

            $('#MainContent_lstRecursosAdicionadosExercicio1_btnExcluirExercicio1_0').show();
        }
        function ocultaBtnExcluirExercicio1() {

            $('#MainContent_lstRecursosAdicionadosExercicio1_btnExcluirExercicio1_0').hide();

        }

        function exibeBtnExcluirExercicio2() {

            $('#MainContent_lstRecursosAdicionadosExercicio2_btnExcluirExercicio2_0').show();
        }
        function ocultaBtnExcluirExercicio2() {

            $('#MainContent_lstRecursosAdicionadosExercicio2_btnExcluirExercicio2_0').hide();

        }

        function exibeBtnExcluirExercicio3() {

            $('#MainContent_lstRecursosAdicionadosExercicio3_btnExcluirExercicio3_0').show();
        }
        function ocultaBtnExcluirExercicio3() {

            $('#MainContent_lstRecursosAdicionadosExercicio3_btnExcluirExercicio3_0').hide();

        }

        function exibeBtnExcluirExercicio4() {

            $('#MainContent_lstRecursosAdicionadosExercicio4_btnExcluirExercicio4_0').show();
        }
        function ocultaBtnExcluirExercicio4() {

            $('#MainContent_lstRecursosAdicionadosExercicio4_btnExcluirExercicio4_0').hide();

        }
    </script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmUnidadesPublicas">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            3.8.1 - Informações sobre este serviço
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Unidades Públicas">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            Para finalizar o registro de um serviço é necessário preencher as 5 abas disponíveis: Caracterização do serviço, Caracterização dos usuários, Recursos humanos, Funcionamento e Recursos financeiros.<br />
                                            Apenas nesta última aba está disponível um botão “Salvar” que, quando acionado, fará com que o sistema verifique se todas as informações necessárias foram inseridas e gravará estas informações, salvando o registro. 
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <div id="Tabs" class="tabcontrol" data-role="tabcontrol">
                                                <ul class="tabs">
                                                    <li id="frame1_1" runat="server"><a href="#frame1_1">Caracterização do Serviço</a></li>
                                                    <li id="frame1_2" runat="server"><a href="#frame1_2">Caracterização dos Usuários</a></li>
                                                    <li id="frame1_3" runat="server"><a href="#frame1_3">Recursos Humanos</a></li>
                                                    <li id="frame1_4" runat="server"><a href="#frame1_4">Funcionamento</a></li>
                                                    <li id="frame1_5" runat="server"><a href="#frame1_5">Recursos Financeiros</a></li>
                                                </ul>
                                                <div class="frames">
                                                    <div class="frame" id="frame1_1">
                                                        <div class="row">
                                                            <fieldset class="border-blue">
                                                                <legend class="lgnd"><b class="fg-blue">Serviço</b></legend>
                                                                <div class="row">
                                                                    <div class="cell">
                                                                        <b>Selecione o nível de proteção social:</b><br />
                                                                        <asp:RadioButtonList ID="rblTipoProtecao" runat="server" RepeatDirection="Horizontal"
                                                                            AutoPostBack="True"
                                                                            OnSelectedIndexChanged="rblTipoProtecao_SelectedIndexChanged">
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                                <div class="row cells3">
                                                                    <div class="cell colspan2">
                                                                        <b>Tipo de serviço:</b><br />
                                                                        <div class="input-control select full-size">
                                                                            <asp:DropDownList ID="ddlTipoServico" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoServico_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <b>Usuários:</b><br />
                                                                        <div class="input-control select full-size">
                                                                            <asp:DropDownList ID="ddlPublicoAlvo" runat="server" AutoPostBack="True"
                                                                                OnSelectedIndexChanged="ddlPublicoAlvo_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row" id="tbNaoTipificado" runat="server" visible="false">
                                                                    <div class="cell">
                                                                        <div class="input-control select mid-size">
                                                                            <asp:DropDownList ID="ddlTipoServicoNaoTipificado" runat="server" AutoPostBack="True"
                                                                                OnSelectedIndexChanged="ddlTipoServicoNaoTipificado_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row" id="tbNaoTipificadoDetalhado" runat="server" visible="false">
                                                                    <div class="cell">
                                                                        <b>Especifique qual o tipo de serviço :</b><br />
                                                                        <asp:TextBox ID="txtNaotipificado" runat="server" Width="100%"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="row" id="tbNaoTipificadoObjetivo" runat="server" visible="false">
                                                                    <div class="cell">
                                                                        <b>Especifique qual o objetivo deste serviço:</b><br />
                                                                        <asp:TextBox ID="txtObjetivoNaoTipificado" runat="server" Width="100%"
                                                                            Height="40px" TextMode="MultiLine"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="cell">
                                                                        <b>Abrangência do Serviço:</b><br />
                                                                        <div class="input-control select">
                                                                            <asp:DropDownList ID="ddlAbrangencia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAbrangencia_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row" id="trSedeServico" runat="server" visible="false">
                                                                    <div class="cell">
                                                                        <b>O município é Sede do serviço ?</b><br />
                                                                        <asp:RadioButtonList runat="server" ID="rblAbrangencia" RepeatDirection="Vertical" AutoPostBack="true" OnSelectedIndexChanged="rblAbrangencia_SelectedIndexChanged">
                                                                            <asp:ListItem Value="1">Sim</asp:ListItem>
                                                                            <asp:ListItem Value="0">Não</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                    <div class="cell" runat="server" id="trMunicipioParticipaOferta" visible="false" >
                                                                        <div class="cell">
                                                                            <b>&nbsp Indicar quais municípios participam da oferta do serviço :</b><br />
                                                                            <asp:TextBox runat="server" ID="txtMunicipioParticipaOferta" TextMode="MultiLine" style="width:50%;margin-left:10px"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell" runat="server" id="trMunicipioSede" visible="false">
                                                                        <div class="cell">
                                                                            <b>&nbsp Indicar qual município é Sede do serviço :</b><br />
                                                                            <asp:TextBox runat="server" ID="txtMunicipioSede" TextMode="MultiLine" style="width:50%;margin-left:10px"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row" runat="server" id="trFormaJuridica" visible="false">
                                                                    <div class="cell">
                                                                        <div id="Div1" class="input-control select  mid-size" runat="server">
                                                                            <b>Qual a forma jurídica que regulamenta a oferta Regional do Serviço ?</b><br />
                                                                            <asp:DropDownList runat="server" ID="ddlFormaJuridica" AutoPostBack="true" OnSelectedIndexChanged="ddlFormaJuridica_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row" runat="server" id="trConsorcioPublico" visible="false">
                                                                    <div class="cell" style="padding-left:6px">
                                                                       <asp:Label ID="lblNomeConsorcio" runat="server">Nome do consórcio :</asp:Label>&nbsp<asp:TextBox runat="server" ID="txtNomeConsorcio" style="width:40%;margin-left:15px"></asp:TextBox>
                                                                    </div>
                                                                    <br /><br />
                                                                    <div class="cell" style="padding-left:6px">
                                                                     <asp:Label ID="lblCNPJ" runat="server">CNJP:</asp:Label>&nbsp<asp:TextBox runat="server" ID="txtCNPJConsorcio" placeholder="00.000.000/0000-00" oninput="javascript:aplicarFormatacao(this, '00.000.000/0000-00')" style="width:15%;margin-left:100px"></asp:TextBox>
                                                                    </div>
                                                                    <br /><br />
                                                                    <div class="cell" style="padding-left:6px">
                                                                        <asp:Label ID="lblMunicipioSede" runat="server">Município Sede:</asp:Label>&nbsp<asp:TextBox runat="server" ID="txtMunicipioSedeConsorcio" style="width:40%;margin-left:42px"></asp:TextBox>
                                                                    </div>
                                                                    <br /><br />
                                                                    <div class="cell" style="padding-left:6px">
                                                                        <asp:Label ID="lblMunicipioEnvolvido" runat="server">Municípios envolvidos :</asp:Label>&nbsp<asp:TextBox runat="server" ID="txtMunicipiosEnvolvidos" TextMode="MultiLine" style="width:50%" ></asp:TextBox>
                                                                    </div>
                                                                </div>


                                                                <div class="row cells2">
                                                                    <div class="cell">
                                                                        <b>Nome da pessoa responsável por este serviço:</b><br />
                                                                        <div class="input-control text full-size">
                                                                            <asp:TextBox ID="txtTecnicoResponsavel" runat="server" Width="432px"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <br />
                                                                        <br />
                                                                        <b>
                                                                            <asp:CheckBox runat="server" ID="chkNaoPossuiTecnicoResponsavel" Text="Não há responsável por este serviço"
                                                                                AutoPostBack="true" OnCheckedChanged="chkNaoPossuiTecnicoResponsavel_CheckedChanged" /></b><br />
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="cell">
                                                                        <b>Este serviço atende exclusiva ou prioritariamente usuários que pertencem a alguma das comunidades tradicionais ou grupos específicos listados abaixo?</b>
                                                                        <asp:RadioButtonList ID="rblCaracteristicasTerritorio" runat="server">
                                                                            <asp:ListItem Value="1" Text="Ciganos" />
                                                                            <asp:ListItem Value="2" Text="Extrativistas" />
                                                                            <asp:ListItem Value="3" Text="Pescadores artesanais" />
                                                                            <asp:ListItem Value="4" Text="Comunidade tradicional de matriz africana" />
                                                                            <asp:ListItem Value="5" Text="Comunidade ribeirinha" />
                                                                            <asp:ListItem Value="6" Text="Indígenas" />
                                                                            <asp:ListItem Value="7" Text="Quilombolas" />
                                                                            <asp:ListItem Value="8" Text="Agricultores familiares" />
                                                                            <asp:ListItem Value="9" Text="Acampados" />
                                                                            <asp:ListItem Value="15" Text="População em situação de rua" />
                                                                            <asp:ListItem Value="16" Text="Pessoas com deficiência" />
                                                                            <asp:ListItem Value="10" Text="População flutuante decorrente de instalação prisional" />
                                                                            <asp:ListItem Value="11" Text="Trabalhadores sazonais" />
                                                                            <asp:ListItem Value="12" Text="Aglomerados subnormais" />
                                                                            <asp:ListItem Value="13" Text="Assentados" />
                                                                            <asp:ListItem Value="14" Text="Nenhuma das alternativas anteriores" Selected="True" />
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="cell" runat="server" id="trCaracteristicaOferta">
                                                                        <b>Este serviço é ofertado em:</b>
                                                                        <asp:RadioButtonList ID="rblCaracteristicaOferta" runat="server">
                                                                            <asp:ListItem Value="2" Text="CDPCD – Centro Dia para Pessoas com Deficiência"/>
                                                                            <asp:ListItem Value="3" Text="CDI – Centro Dia Idoso"/>
                                                                            <asp:ListItem Value="4" Text="CDIPCD – Centro Dia para Idosos e Pessoas com Deficiência" />
                                                                            <asp:ListItem Value="5" Text="Domicílio"/>
                                                                            <asp:ListItem Value="1" Text="Nenhuma das alternativas anteriores" Selected="True" />
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                </div>

                                                                <div class="row" id="trAssociacaoProgramas" runat="server" visible="true">
                                                                    <div class="cell">
                                                                        <fieldset class="border-blue">
                                                                            <legend class="lgnd"><b class="fg-blue">Integração do serviço com programas, projetos ou benefícios:</b></legend>
                                                                            <div class="row">
                                                                                <div class="cell">
                                                                                    <div class="row">
                                                                                        <div class="cell">
                                                                                            <b>Os usuários deste serviço são também atendidos em algum programa, projeto ou recebem algum tipo de transferência direta de renda ou benefício socioassistencial?</b><br />
                                                                                            <asp:RadioButtonList ID="rblIntegracaoRede" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblIntegracaoRede_SelectedIndexChanged" AutoPostBack="True">
                                                                                                <asp:ListItem Value="1" Text="Sim" />
                                                                                                <asp:ListItem Value="0" Text="Não" />
                                                                                            </asp:RadioButtonList>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row" id="trProgramasBeneficios" runat="server" visible="false">
                                                                                        <div class="subframe active">
                                                                                            <div class="content border-blue">
                                                                                                <div class="row cells4">
                                                                                                    <div class="cell colspan3">
                                                                                                        <br />
                                                                                                        Informe o(s) programa(s) ou benefício(s) em que os usuários deste serviço estão incluídos.
                                                                                                             <br />
                                                                                                        <br />
                                                                                                        Se desejar registrar o atendimento dos usuários deste serviço em <b>Programas de Âmbito Municipal</b> ou através de 
                                                                                                <b>Benefícios Eventuais, preencha antes as páginas destes programas e benefícios,</b> a partir do quadro 38 do bloco III 
                                                                                                (Programas Municipais) ou a partir do quadro 41 do bloco III (Benefícios Eventuais).
                                                                                                <br />
                                                                                                    </div>
                                                                                                    <div class="cell" style="text-align: right;">
                                                                                                        <a id="linkAlteracoesQuadro42" runat="server" href="#" visible="false">
                                                                                                            <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />
                                                                                                            Alterado </a>&nbsp; 
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row cell-auto-size">
                                                                                                    <div class="cell">
                                                                                                        <b>Programa ou Benefício:</b><br />
                                                                                                        <div class="input-control select mid-size">
                                                                                                            <asp:DropDownList ID="ddlProgramaBeneficio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProgramaBeneficio_SelectedIndexChanged">
                                                                                                            </asp:DropDownList>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row cell-auto-size" id="trRendaCidadaBeneficioIdoso" runat="server" visible="false">
                                                                                                    <div class="cell">
                                                                                                        <b>
                                                                                                            <asp:Label ID="lblBenificiarios" runat="server"></asp:Label>
                                                                                                        </b>
                                                                                                        <br />
                                                                                                        <div class="input-control text">
                                                                                                            <asp:TextBox ID="txtNumeroUsuarios" runat="server" MaxLength="6" Width="67px" />
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row cell-auto-size">
                                                                                                    <div class="cell">
                                                                                                        <asp:Button ID="btnAdicionar" runat="server" OnClick="btnAdicionar_Click" SkinID="button-save" Text="Adicionar" Width="100px" />
                                                                                                        &nbsp;<asp:Button ID="Button1" SkinID="button-save" runat="server" PostBackUrl="~/BlocoIII/CProgramasProjetos.aspx" Text="Voltar" />
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row cell-auto-size">
                                                                                                    <div class="cell">

                                                                                                        <asp:Repeater ID="rptProgramaTemp" runat="server">
                                                                                                            <HeaderTemplate>
                                                                                                                <table border="0" cellpadding="0" cellspacing="0" rules="all" class="table striped border bordered">
                                                                                                                    <tr class="info">
                                                                                                                        <th scope="col" style="width: 80px">Programa
                                                                                                                        </th>
                                                                                                                        <th scope="col" style="width: 120px">Tipo de serviço
                                                                                                                        </th>
                                                                                                                        <th scope="col" style="width: 100px">Usuários
                                                                                                                        </th>
                                                                                                                        <th scope="col" style="width: 100px">Nº de usuários vinculados a este programa
                                                                                                                        </th>
                                                                                                                             <th width="50">Excluir </th>
                                                                                                                    </tr>
                                                                                                            </HeaderTemplate>
                                                                                                            <ItemTemplate>
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblCustomerId" runat="server" Text='<%# Eval("Nome") %>' />
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblContactName" runat="server" Text='<%# Eval("TipoServico") %>' />
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("Usuario") %>' />
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("NumeroAtendidos") %>' />
                                                                                                                    </td>
                                                                                                                    <td >
                                                                                                                        <asp:ImageButton 
                                                                                                                            id="btnExcluir" 
                                                                                                                            runat="server"  
                                                                                                                            CausesValidation="false" 
                                                                                                                            CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' 
                                                                                                                            OnClick="btnExcluir_Click"  ImageUrl="~/Styles/Icones/editdelete.png" 
                                                                                                                            OnClientClick="return confirm('Deseja realmente desfazer esta associação entre serviço e programa?');" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                </table>
                                                                                                            </FooterTemplate>
                                                                                                        </asp:Repeater>





                                                                                                    </div>
                                                                                                </div>
                                                                                                <table id="Table1" runat="server" align="center" border="0" cellpadding="0" cellspacing="2" visible="false" width="100%">
                                                                                                    <tr>
                                                                                                        <td class="ui-state-highlight titulo" style="padding: 2px 10px 2px 10px;">
                                                                                                            <img src="../Styles/Icones/messagebox_warning.png" align="absMiddle" />
                                                                                                            <b style="color: #000000 !important">Verifique as inconsistências:</b>
                                                                                                            <br />
                                                                                                            <br />
                                                                                                            <asp:Label ID="Label1" runat="server" ForeColor="Red" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                                <div class="row">
                                                                                                    <div class="cell">
                                                                                                        <asp:ListView ID="lstRecursos" runat="server" OnItemCommand="lstRecursos_ItemCommand">
                                                                                                            <LayoutTemplate>
                                                                                                                <table border="0" cellpadding="0" cellspacing="0" class="table striped border bordered">
                                                                                                                    <thead class="info">
                                                                                                                        <tr>
                                                                                                                            <th width="50">Visualizar </th>
                                                                                                                            <th width="80">Tipo de rede</th>
                                                                                                                            <th width="220">Unidade/Organização</th>
                                                                                                                            <th width="220">Programa</th>
                                                                                                                            <th width="320">Tipo de serviço</th>
                                                                                                                            <th width="180">Usuários</th>
                                                                                                                            <th width="100">Nº de usuários<br />
                                                                                                                                vinculados a<br />
                                                                                                                                este programa </th>
                                                                                                                            <th width="50">Excluir </th>
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
                                                                                                                    <td colspan="8"><b>Proteção Social:</b> <%#DataBinder.Eval(Container.DataItem, "Key") %></td>
                                                                                                                </tr>
                                                                                                                <asp:ListView ID="lstItems" runat="server" DataSource='<%#Eval("Items") %>' OnItemDataBound="lstItems_ItemDataBound">
                                                                                                                    <LayoutTemplate>
                                                                                                                        <tr id="itemPlaceholder" runat="server">
                                                                                                                        </tr>
                                                                                                                    </LayoutTemplate>
                                                                                                                    <ItemTemplate>
                                                                                                                        <tr>
                                                                                                                            <td align="center"><%#MontarBotao((Seds.PMAS.QUADRIENAL.Entidades.Estruturas.ConsultaProgramaProjetoServicoCofinanciamentoInfo)Container.DataItem) %></td>
                                                                                                                            <td align="center"><%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %></td>
                                                                                                                            <td><%#DataBinder.Eval(Container.DataItem, "Unidade") %></td>
                                                                                                                            <td><%#DataBinder.Eval(Container.DataItem, "Nome") %></td>
                                                                                                                            <td><%#DataBinder.Eval(Container.DataItem, "TipoServico") %></td>
                                                                                                                            <td class="align-center"><%#DataBinder.Eval(Container.DataItem, "Usuario") %></td>
                                                                                                                            <td class="align-center"><%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroUsuarios")) %></td>
                                                                                                                            <td class="align-center">
                                                                                                                                <asp:ImageButton ID="btnExcluir" runat="server" CausesValidation="false" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") +","+ DataBinder.Eval(Container.DataItem,"TipoCofinanciamento") %>' CommandName="Excluir" ImageUrl="~/Styles/Icones/editdelete.png" OnClientClick="return confirm('Deseja realmente desfazer esta associação entre serviço e programa?');" />
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:ListView>
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
                                                                                                <div class="row">
                                                                                                    <div class="cell">
                                                                                                        <asp:ListView ID="lstRecursosAmigoIdoso" runat="server" OnItemCommand="lstRecursos_ItemCommand">
                                                                                                            <LayoutTemplate>
                                                                                                                <table border="0" cellpadding="0" cellspacing="0" class="table striped border bordered">
                                                                                                                    <thead class="info">
                                                                                                                        <tr>
                                                                                                                            <th width="50">Visualizar </th>
                                                                                                                            <th width="80">Tipo de<br />
                                                                                                                                Unidade </th>
                                                                                                                            <th width="220">Unidade </th>
                                                                                                                            <th width="320">Tipo de serviço </th>
                                                                                                                            <th width="180">Usuários </th>
                                                                                                                            <th width="100">Nº de usuários<br />
                                                                                                                                vinculados a<br />
                                                                                                                                este programa </th>
                                                                                                                            <th width="50">Excluir </th>
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
                                                                                                                    <td colspan="8"><b>Proteção Social:</b> <%#DataBinder.Eval(Container.DataItem, "Key") %></td>
                                                                                                                </tr>
                                                                                                                <asp:ListView ID="lstItems" runat="server" DataSource='<%#Eval("Items") %>' OnItemDataBound="lstItems_ItemDataBound">
                                                                                                                    <LayoutTemplate>
                                                                                                                        <tr id="itemPlaceholder" runat="server">
                                                                                                                        </tr>
                                                                                                                    </LayoutTemplate>
                                                                                                                    <ItemTemplate>
                                                                                                                        <tr>
                                                                                                                            <td align="center"><%#MontarBotao((Seds.PMAS.QUADRIENAL.Entidades.Estruturas.ConsultaProgramaProjetoServicoCofinanciamentoInfo)Container.DataItem) %></td>
                                                                                                                            <td align="center"><%#DataBinder.Eval(Container.DataItem, "TipoUnidade") %></td>
                                                                                                                            <td><%#DataBinder.Eval(Container.DataItem, "Unidade") %></td>
                                                                                                                            <td><%#DataBinder.Eval(Container.DataItem, "TipoServico") %></td>
                                                                                                                            <td class="align-center"><%#DataBinder.Eval(Container.DataItem, "Usuario") %></td>
                                                                                                                            <td class="align-center"><%#String.Format("{0:0,0}",DataBinder.Eval(Container.DataItem, "NumeroUsuarios")) %></td>
                                                                                                                            <td class="align-center">
                                                                                                                                <asp:ImageButton 
                                                                                                                                    id="btnExcluir" 
                                                                                                                                    runat="server" 
                                                                                                                                    CausesValidation="false" 
                                                                                                                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Id") %>' 
                                                                                                                                    CommandName="Excluir" 
                                                                                                                                    ImageUrl="~/Styles/Icones/editdelete.png" 
                                                                                                                                    OnClientClick="return confirm('Deseja realmente desfazer esta associação entre serviço e programa?');" />
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:ListView>
                                                                                                                </tr>
                                                                                                            </ItemTemplate>
                                                                                                            <EmptyDataTemplate>
                                                                                                                <div align="center" style="width: 100%;">
                                                                                                                    <br />
                                                                                                                    <b class="titulo">Não existe registro de serviços associados a este programa</b>
                                                                                                                </div>
                                                                                                            </EmptyDataTemplate>
                                                                                                        </asp:ListView>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="row">
                                                                                                    <div class="cell">
                                                                                                        <asp:Button ID="btnSalvarRecursoPrograma" runat="server" Text="Salvar" SkinID="button-save"
                                                                                                            Width="89px" OnClick="btnSalvarRecursoPrograma_Click" ValidationGroup="vgCampos" Visible="false" />
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </fieldset>
                                                                    </div>

                                                                </div>
                                                            </fieldset>
                                                        </div>
                                                    </div>
                                                    <div class="frame" id="frame1_2">
                                                        <div class="row">
                                                            <fieldset class="border-blue">
                                                                <legend class="lgnd"><b class="fg-blue">Usuários</b></legend>
                                                                <div class="row cells2">
                                                                    <div class="cell">
                                                                        <fieldset class="border-blue">
                                                                            <legend class="lgnd"><b class="fg-blue">Sexo:</b></legend>
                                                                            <asp:RadioButtonList ID="rblSexo" runat="server">
                                                                                <asp:ListItem Value="1" Text="Feminino"></asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="Masculino"></asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="Ambos os sexos"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </fieldset>
                                                                    </div>
                                                                    <div class="cell">
                                                                        <fieldset class="border-blue">
                                                                            <legend class="lgnd"><b class="fg-blue">Região de moradia dos usuários:</b></legend>
                                                                            <asp:RadioButtonList ID="rblMoradiaUsuarios" runat="server">
                                                                                <asp:ListItem Value="1" Text="Zona Urbana"></asp:ListItem>
                                                                                <asp:ListItem Value="2" Text="Zona Rural"></asp:ListItem>
                                                                                <asp:ListItem Value="3" Text="Ambas"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </fieldset>
                                                                    </div>
                                                                </div>
                                                                <div class="cell">
                                                                    <b>Assinale dentre as opções abaixo quais são as principais situações de vulnerabilidade identificadas dentre os usuários que são atendidos por este serviço:</b><br />
                                                                    <asp:CheckBoxList ID="lstSituacoesEspecificas" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" />
                                                                </div>
                                                            </fieldset>
                                                        </div>
                                                    </div>
                                                    <div class="frame" id="frame1_3">
                                                        <fieldset class="border-blue">
                                                            <legend class="lgnd"><b class="fg-blue">Recursos Humanos
                                                            </b></legend>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Indique o número de trabalhadores remunerados, segundo a escolaridade:</b>
                                                                </div>
                                                                <asp:HiddenField ID="hidTotalEscolaridade" runat="server" />
                                                            </div>
                                                            <div class="row cells5">
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtSemEscolaridade" runat="server" Width="48px" MaxLength="4"></asp:TextBox>Sem Escolarização
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtNivelFundamental" runat="server" CssClass="campoTexto" Width="48px"
                                                                        MaxLength="4"></asp:TextBox>Nível Fundamental
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtNivelMedio" runat="server" Width="48px" MaxLength="4"></asp:TextBox>
                                                                    Nível Médio
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtSuperior" runat="server" Width="48px" MaxLength="4"></asp:TextBox>
                                                                    Nivel Superior
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="lblTotalRh" Enabled="false" runat="server" Text="" CssClass="campoTexto"
                                                                        Width="48px"></asp:TextBox>Total
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Indique a área de formação dos trabalhadores que possuem nível superior:</b>
                                                                </div>
                                                            </div>
                                                            <div class="row cells5">
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtSuperiorServicoSocial" runat="server" Width="48px" MaxLength="4"></asp:TextBox>&nbsp;Serviço
                                                                Social
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtSuperiorPsicologia" runat="server" Width="48px" MaxLength="4"
                                                                        AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Psicologia
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtSuperiorPedagogia" runat="server" Width="48px" MaxLength="4"
                                                                        AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Pedagogia
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtSociologia" runat="server" Width="48px" MaxLength="4" AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Sociologia
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtSuperiorTerapiaOcupacional" runat="server" Width="48px" MaxLength="4"
                                                                        AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Terapia Ocupacional
                                                                </div>
                                                            </div>
                                                            <div class="row cells5">
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtDireito" runat="server" Width="48px" MaxLength="4" AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Direito
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtSuperiorAntropologia" runat="server" Width="48px" MaxLength="4"
                                                                        AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Antropologia
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtSuperiorEconomia" runat="server" Width="48px" MaxLength="4" AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Economia
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtSuperiorMusicoTerapia" runat="server" Width="48px" MaxLength="4"
                                                                        AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Musicoterapia
                                                                </div>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtSuperiorEconomiaDomestica" runat="server" Width="48px" MaxLength="4"
                                                                        AutoCompleteType="Disabled"></asp:TextBox>&nbsp;Economia Doméstica
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Com relação a este serviço, indique o número de:</b>
                                                                </div>
                                                            </div>
                                                            <div class="row cells3">
                                                                <%--<div class="cell">
                                                                    <asp:TextBox ID="txtPosGraduacao" runat="server" Width="48px" MaxLength="4"></asp:TextBox>&nbsp;Pós-Graduação
                                                                </div>--%>
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtEstagiarios" runat="server" Text="" Width="48px"></asp:TextBox>&nbsp;Estagiários
                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtEstagiarios" runat="server"
                                                                    TargetControlID="txtEstagiarios" FilterType="Numbers" />
                                                                </div>
                                                                <div class="cell colspan2">
                                                                    <asp:TextBox ID="txtVoluntarios" runat="server" Text="" Width="48px"></asp:TextBox>&nbsp;Voluntários
                                                                   <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtVoluntarios" runat="server"
                                                                       TargetControlID="txtVoluntarios" FilterType="Numbers" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Indique o número de trabalhadores deste serviço que:</b>
                                                                </div>
                                                            </div>
                                                            <div class="row cells3">
                                                                <div class="cell">
                                                                    <asp:TextBox ID="txtExclusivoServico" runat="server" Text="" Width="48px"></asp:TextBox>&nbsp;Trabalham exclusivamente neste serviço. 
                                                                   <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtExclusivoServico" runat="server"
                                                                       TargetControlID="txtExclusivoServico" FilterType="Numbers" />
                                                                </div>
                                                                <div class="cell colspan2">
                                                                    <asp:TextBox ID="txtOutroServicos" runat="server" Text="" Width="48px"></asp:TextBox>&nbsp;Trabalham também em outros serviços socioassistenciais ou no órgão gestor do município.
                                                                   <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtOutroServicos" runat="server"
                                                                       TargetControlID="txtOutroServicos" FilterType="Numbers" />
                                                                </div>
                                                        </fieldset>
                                                    </div>
                                                  

                                                    <div class="frame" id="frame1_4">
                                                        <fieldset class="border-blue">
                                                            <legend class="lgnd"><b class="fg-blue">Funcionamento</b></legend>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Data de início de funcionamento deste serviço:</b>
                                                                    <uc4:data ID="txtDataInicio" runat="server" />

                                                                </div>
                                                            </div>

                                                            
                                                            <div class="row">
                                                                <%--Capacidade--%>
                                                                <div id="layout_capacidade" runat="server">
                                                                    <fieldset class="border-blue">
                                                                        <legend class="lgnd"><b class="fg-blue">Capacidade mensal de atendimento deste serviço em
                                                                            <asp:Label ID="lblIndicadorPeriodoCapacidade" runat="server"></asp:Label></b>:</b></legend>
                                                                        <div class="row" id="container_capacidade">
                                                                            <div class="col-lg-12">
                                                                                <table class="full_border">
                                                                                    <tbody>
                                                                                        <tr>

                                                                                            <td rowspan="2">
                                                                                                <asp:Label ID="lblCapacidade" runat="server" Font-Bold="true" Visible="false"></asp:Label>
                                                                                                <b></td>
                                                                                            <td style="text-align: center"><b>2022</b></td>
                                                                                            <td style="text-align: center"><b>2023</b></td>
                                                                                            <td style="text-align: center"><b>2024</b></td>
                                                                                            <td style="text-align: center"><b>2025</b></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtCapacidadeExercicio1" runat="server" MaxLength="5" Width="62px"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtCapacidadeExercicio1" runat="server" FilterType="Numbers" TargetControlID="txtCapacidadeExercicio1" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtCapacidadeExercicio2" runat="server" MaxLength="5" Width="62px"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtCapacidadeExercicio2" runat="server" FilterType="Numbers" TargetControlID="txtCapacidadeExercicio2" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtCapacidadeExercicio3" runat="server" MaxLength="5" Width="62px"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtCapacidadeExercicio3" runat="server" FilterType="Numbers" TargetControlID="txtCapacidadeExercicio3" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtCapacidadeExercicio4" runat="server" MaxLength="5" Width="62px"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtCapacidadeExercicio4" runat="server" FilterType="Numbers" TargetControlID="txtCapacidadeExercicio4" />
                                                                                            </td>
                                                                                        </tr>

                                                                                    </tbody>
                                                                                </table>

                                                                            </div>
                                                                        </div>
                                                                    </fieldset>
                                                                </div>

                                                                <%--Media Mensal--%>
                                                                <div class="row" id="layout_media_mensal" runat="server">
                                                                    <fieldset class="border-blue">
                                                                        <legend class="lgnd"><b class="fg-blue">Média mensal de atendidos deste serviço em
                                                                            <asp:Label ID="lblIndicadorPeriodoMediaMensal" runat="server"></asp:Label></b> </b></legend>
                                                                        <div class="row" id="container_media_mensal">
                                                                            <div class="col-lg-12">
                                                                                <table class="full_border">
                                                                                    <tbody>
                                                                                        <tr>
                                                                                            <td rowspan="2">
                                                                                                <b>
                                                                                                    <asp:Label ID="lblMediaMensal" runat="server" Font-Bold="true" Style="padding: 10px;" ></asp:Label></b>
                                                                                                <b></td>
                                                                                            <td style="text-align: center"><b>2021</b></td>
                                                                                            <td style="text-align: center"><b>2022</b></td>
                                                                                            <td style="text-align: center"><b>2023</b></td>
                                                                                            <td style="text-align: center"><b>2024</b></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtMediaMensalExercicio1" runat="server" MaxLength="5" Width="60"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxMediaMensalExercicio1" runat="server" FilterType="Numbers" TargetControlID="txtMediaMensalExercicio1" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtMediaMensalExercicio2" runat="server" MaxLength="5" Width="60"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxMediaMensalExercicio2" runat="server" FilterType="Numbers" TargetControlID="txtMediaMensalExercicio2" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtMediaMensalExercicio3" runat="server" MaxLength="5" Width="60"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxMediaMensalExercicio3" runat="server" FilterType="Numbers" TargetControlID="txtMediaMensalExercicio3" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtMediaMensalExercicio4" runat="server" MaxLength="5" Width="60"></asp:TextBox>
                                                                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxMediaMensalExercicio4" runat="server" FilterType="Numbers" TargetControlID="txtMediaMensalExercicio4" />
                                                                                            </td>
                                                                                        </tr>

                                                                                    </tbody>
                                                                                </table>

                                                                            </div>
                                                                        </div>
                                                                    </fieldset>
                                                                </div>

                                                            </div>




                                                            <div class="row cells2">
                                                                <div class="cell">
                                                                    <b>Este serviço funciona quantas horas por semana?</b><br />
                                                                    <asp:RadioButtonList ID="rblHorasSemana" runat="server" RepeatDirection="Horizontal"
                                                                        RepeatColumns="3">
                                                                        <asp:ListItem Value="1" Text="Até 20 horas" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="de 21 a 39 horas"></asp:ListItem>
                                                                        <asp:ListItem Value="3" Text="40 horas"></asp:ListItem>
                                                                        <asp:ListItem Value="4" Text="mais de 40 horas"></asp:ListItem>
                                                                        <asp:ListItem Value="5" Text="ininterrupto (24 horas / 7 dias)"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                                <div class="cell">
                                                                    <b>Este serviço funciona em quantos dias por semana?</b><br />
                                                                    <asp:RadioButtonList ID="rblDiasSemana" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="1" Text="1" Selected="True" />
                                                                        <asp:ListItem Value="2" Text="2" />
                                                                        <asp:ListItem Value="3" Text="3" />
                                                                        <asp:ListItem Value="4" Text="4" />
                                                                        <asp:ListItem Value="5" Text="5" />
                                                                        <asp:ListItem Value="6" Text="6" />
                                                                        <asp:ListItem Value="7" Text="7" />
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Trabalho realizado por este serviço (apontar somente as ações que são de fato realizadas por este serviço)</b>
                                                                    <asp:CheckBoxList ID="lstAtividades" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="cell">
                                                                    <b>Segundo a avaliação do órgão gestor municipal, este serviço: </b>
                                                                    <br />
                                                                    <asp:RadioButtonList ID="rblAvaliacaoGestor" runat="server">
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                    <div class="frame" id="frame1_5">
                                                        <asp:HiddenField ID="hdnExercicio" runat="server" />
                                                        <div id="recursoFinanceiroExercicio" class="tabcontrol" data-role="tabcontrol">
                                                            <ul class="tabs">
                                                                <li id="frame1_5_Ano1" runat="server" onclick='$("#MainContent_hdnExercicio").val("2022")'><a href="#frame1_5_1">2022</a></li>
                                                                <li id="frame1_5_Ano2" runat="server" onclick='$("#MainContent_hdnExercicio").val("2023")'><a href="#frame1_5_2">2023</a></li>
                                                                <li id="frame1_5_Ano3" runat="server" onclick='$("#MainContent_hdnExercicio").val("2024")'><a href="#frame1_5_3">2024</a></li>
                                                                <li id="frame1_5_Ano4" runat="server" onclick='$("#MainContent_hdnExercicio").val("2025")'><a href="#frame1_5_4">2025</a></li>
                                                            </ul>
                                                            <div class="frames">
                                                                <div class="frame" id="frame1_5_1">
                                                                    <fieldset class="border-blue">
                                                                        <legend class="lgnd"><b class="fg-blue">Recursos Financeiros</b></legend>
                                                                        <div class="row" id="trConvenioEstadualizado" runat="server" visible="false">
                                                                            <div class="cell">
                                                                                <b>Este serviço possui convênio firmado com o Estado ?</b><br />
                                                                                <asp:RadioButtonList ID="rblEstadualizado" runat="server"
                                                                                    RepeatDirection="Horizontal" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="rblEstadualizado_SelectedIndexChanged">
                                                                                    <asp:ListItem Value="1" Text="Sim" />
                                                                                    <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row" id="trValorEstadualizado" runat="server" visible="false">
                                                                            <div class="cell">
                                                                                <b>Valor anual do convênio:</b><br />
                                                                                <div class="input-control text">
                                                                                    <asp:TextBox ID="txtValorEstadualizado" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Fundos Municipais</b></legend>
                                                                                <div class="row cells3">
                                                                                    <div class="cell">
                                                                                        <b>Assistência Social:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFMASExercicio1" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Direitos da Criança e do Adolescente:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFMDCAExercicio1" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Idoso:</b>
                                                                                        <br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFMIExercicio1" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </fieldset>
                                                                        </div>
                                                                        <div class="row">
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Fundos Estaduais</b></legend>
                                                                                <div class="row cells3">
                                                                                    <div class="cell">
                                                                                        <b>Assistência Social:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFEASExercicio1" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell" id="trFeasAnterior" runat="server">
                                                                                        <b>FEAS - Reprogramação Ano Anterior:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFEASAnoAnteriorExercicio1" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Direitos da Criança e do Adolescente:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFEDCAExercicio1" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row cells3">
                                                                                    <div class="cell">
                                                                                        <b>Idoso:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFEIExercicio1" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="cell" id="trFeasDemandas" runat="server">
                                                                                        <b>FEAS - Demandas Parlamentares</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox runat="server" ID="txtFEASDemandasExercicio1" Text="0,00" Style="text-align:right;"/>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="cell">
		                                                                                <b>FEAS - Reprogramação Demandas Parlamentares</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox runat="server" ID="txtFEASReprogramacaoDemandasParlamentaresExercicio1" Text="0,00" Style="text-align:right;"/>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </fieldset>
                                                                        </div>
                                                                        <div class="row">
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Fundos Nacionais</b></legend>
                                                                                <div class="row cells3">
                                                                                    <div class="cell">
                                                                                        <b>Assistência Social:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFNASExercicio1" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Direitos da Criança e do Adolescente:</b>
                                                                                        <br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFNDCAExercicio1" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Idoso:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFNIExercicio1" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </fieldset>
                                                                        </div>
                                                                        <div id="trRecursosFinanceirosCRAS" runat="server" visible="true">
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Outras Fontes Financeiras</b></legend>
                                                                                <div class="row">
                                                                                    <div class="cell">
                                                                                        <b>Existem outras fontes de financiamento para custeio deste serviço que não passam pelo FMAS?</b><br />
                                                                                        <asp:RadioButtonList ID="rblOutrasFontesExercicio1" runat="server" RepeatDirection="Horizontal"
                                                                                            AutoPostBack="True" OnSelectedIndexChanged="rblOutrasFontesExercicio1_SelectedIndexChanged">
                                                                                            <asp:ListItem Value="1" Text="Sim" />
                                                                                            <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                                                        </asp:RadioButtonList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row cells2" runat="server" id="trMotivoEstadualizadoExercicio1" visible="false">
                                                                                    <div class="cell">
                                                                                        <b>Indique qual é a outra fonte de recursos para este serviço</b><br />
                                                                                        <div class="input-control text full-size">
                                                                                            <asp:TextBox ID="txtNomeRecursoExercicio1" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Indique o valor dos recursos para este serviço</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtValorRecursoExercicio1" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row" id="trAddRecursoExercicio1" runat="server" visible="false">
                                                                                    <div class="cell">
                                                                                        <asp:Button ID="btnAdicionarRecursoExercicio1" runat="server" Width="89" Text="Adicionar" OnClick="btnAdicionarRecursoExercicio1_Click" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row mid-size" id="tdlstRecursosAdicionadosExercicio1" runat="server" visible="false">
                                                                                    <asp:ListView ID="lstRecursosAdicionadosExercicio1" OnItemDataBound="lstRecursosAdicionadosExercicio1_ItemDataBound" runat="server" DataKeyNames="Id" OnItemCommand="lstRecursosAdicionadosExercicio1_ItemCommand">
                                                                                        <LayoutTemplate>
                                                                                            <table border="0" cellpadding="0" cellspacing="0" class="table striped border bordered">
                                                                                                <thead class="info">
                                                                                                    <tr>
                                                                                                        <th width="25px"></th>
                                                                                                        <th width="250px">Fonte de Recurso</th>
                                                                                                        <th width="100px">Valor do Recurso</th>
                                                                                                        <th width="50px">Excluir </th>
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
                                                                                                    <asp:Label ID="lblSequenciaExercicio1" runat="server" />
                                                                                                </td>
                                                                                                <td align="center"><%#DataBinder.Eval(Container.DataItem, "NomeFonteRecurso") %></td>
                                                                                                <td align="center"><%#((Decimal)DataBinder.Eval(Container.DataItem, "ValorFonteRecurso")).ToString("N2") %></td>
                                                                                                <td align="center">
                                                                                                    <asp:ImageButton 
                                                                                                        ID="btnExcluirExercicio1" 
                                                                                                        runat="server" 
                                                                                                        CausesValidation="false" 
                                                                                                        Visible='<%#DataBinder.Eval(Container.DataItem, "Liberado")%>'
                                                                                                        CommandName="Excluir" 
                                                                                                        ImageUrl="~/Styles/Icones/editdelete.png" 
                                                                                                        OnClientClick="return confirm('Deseja realmente excluir esse registro?');" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                        <EmptyDataTemplate>
                                                                                            <div align="center" style="width: 100%;">
                                                                                                <b class="titulo">Não existe registro.</b>
                                                                                            </div>
                                                                                        </EmptyDataTemplate>
                                                                                    </asp:ListView>
                                                                                </div>
                                                                            </fieldset>
                                                                        </div>

                                                                        <div id="trDemandasExercicio1" runat="server" visible="true"> 
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Recursos financeiros Demandas Parlamentares</b></legend>
                                                                             
                                                                            <div class="row cells2">
                                                                                <div class="cell">
                                                                                    <b>Código / Número da Demanda Parlamentar:</b><br />
                                                                                    <div class="input-control text">
                                                                                        <asp:TextBox ID="txtCodigoDemandaExercicio1" runat="server" Text=" " Style="text-align: right;" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="cell">
                                                                                    <b>Objeto da Demanda Parlamentar:</b><br />
                                                                                    <div class="input-control text">
                                                                                        <asp:TextBox ID="txtObjetoDemandaExercicio1" runat="server" Text=" " Style="text-align: right;" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row cells2">
                                                                                    <div class="cell">
                                                                                        <b>Contrapartida Municipal:</b><br />
                                                                                        <asp:RadioButtonList ID="rblContraPartida1" runat="server" RepeatDirection="Horizontal"
                                                                                            AutoPostBack="True" OnSelectedIndexChanged="rblContraPartida1_SelectedIndexChanged">
                                                                                            <asp:ListItem Value="1" Text="Sim" />
                                                                                            <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                                                        </asp:RadioButtonList>
                                                                                    </div>
                                                                                <div id="trValorContraExercicio1" class="cell" visible="false" runat="server">
                                                                                    <b></b><br />
                                                                                    <div class="input-control text">
                                                                                        <asp:TextBox ID="txtValorContraExercicio1" runat="server" Text="0,00" Style="text-align: right;" /> 
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                           </fieldset>
                                                                        </div>

                                                                        <div class="row">
                                                                            <div class="cell" align="center">
                                                                                <asp:Button ID="btnSalvarExercicio1" runat="server" Text="Salvar" SkinID="button-save"
                                                                                    Width="89px" OnClick="btnSalvar_Click" ValidationGroup="vgCampos" />
                                                                            </div>
                                                                        </div>
                                                                    </fieldset>
                                                                </div>
                                                                <div class="frame" id="frame1_5_2">
                                                                    <fieldset class="border-blue">
                                                                        <legend class="lgnd"><b class="fg-blue">Recursos Financeiros</b></legend>
                                                                        <div class="row">
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Fundos Municipais</b></legend>
                                                                                <div class="row cells3">
                                                                                    <div class="cell">
                                                                                        <b>Assistência Social:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFMASExercicio2" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Direitos da Criança e do Adolescente:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFMDCAExercicio2" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Idoso:</b>
                                                                                        <br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFMIExercicio2" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </fieldset>
                                                                        </div>
                                                                        <div class="row">
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Fundos Estaduais</b></legend>
                                                                                <div class="row cells3">
                                                                                    <div class="cell">
                                                                                        <b>Assistência Social:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFEASExercicio2" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell" id="Div2" runat="server">
                                                                                        <b>FEAS - Reprogramação Ano Anterior:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFEASAnoAnteriorExercicio2" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Direitos da Criança e do Adolescente:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFEDCAExercicio2" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row cells3">
                                                                                    <div class="cell">
                                                                                        <b>Idoso:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFEIExercicio2" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="cell" id="Div8" runat="server">
                                                                                        <b>FEAS - Demandas Parlamentares</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox runat="server" ID="txtFEASDemandasExercicio2" Text="0,00" Style="text-align:right;"/>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="cell">
		                                                                                <b>FEAS - Reprogramação Demandas Parlamentares</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox runat="server" ID="txtFEASReprogramacaoDemandasParlamentaresExercicio2" Text="0,00" Style="text-align:right;"/>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </fieldset>
                                                                        </div>
                                                                        <div class="row">
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Fundos Nacionais</b></legend>
                                                                                <div class="row cells3">
                                                                                    <div class="cell">
                                                                                        <b>Assistência Social:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFNASExercicio2" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Direitos da Criança e do Adolescente:</b>
                                                                                        <br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFNDCAExercicio2" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Idoso:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFNIExercicio2" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </fieldset>
                                                                        </div>
                                                                        <div id="Div3" runat="server" visible="true">
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Outras Fontes Financeiras</b></legend>
                                                                                <div class="row">
                                                                                    <div class="cell">
                                                                                        <b>Existem outras fontes de financiamento para custeio deste serviço que não passam pelo FMAS?</b><br />
                                                                                        <asp:RadioButtonList ID="rblOutrasFontesExercicio2" runat="server" RepeatDirection="Horizontal"
                                                                                            AutoPostBack="True" OnSelectedIndexChanged="rblOutrasFontesExercicio2_SelectedIndexChanged">
                                                                                            <asp:ListItem Value="1" Text="Sim" />
                                                                                            <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                                                        </asp:RadioButtonList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row cells2" runat="server" id="trMotivoEstadualizadoExercicio2" visible="false">
                                                                                    <div class="cell">
                                                                                        <b>Indique qual é a outra fonte de recursos para este serviço</b><br />
                                                                                        <div class="input-control text full-size">
                                                                                            <asp:TextBox ID="txtNomeRecursoExercicio2" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Indique o valor dos recursos para este serviço</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtValorRecursoExercicio2" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row" id="trAddRecursoExercicio2" runat="server" visible="false">
                                                                                    <div class="cell">
                                                                                        <asp:Button ID="btnAdicionarRecursoExercicio2" runat="server" Width="89" Text="Adicionar" OnClick="btnAdicionarRecursoExercicio2_Click" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row mid-size" id="tdlstRecursosAdicionadosExercicio2" runat="server" visible="false">
                                                                                    <asp:ListView ID="lstRecursosAdicionadosExercicio2" OnItemDataBound="lstRecursosAdicionadosExercicio2_ItemDataBound" runat="server" DataKeyNames="Id" OnItemCommand="lstRecursosAdicionadosExercicio2_ItemCommand">
                                                                                        <LayoutTemplate>
                                                                                            <table border="0" cellpadding="0" cellspacing="0" class="table striped border bordered">
                                                                                                <thead class="info">
                                                                                                    <tr>
                                                                                                        <th width="25px"></th>
                                                                                                        <th width="250px">Fonte de Recurso</th>
                                                                                                        <th width="100px">Valor do Recurso</th>
                                                                                                        <th width="50px">Excluir </th>
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
                                                                                                    <asp:Label ID="lblSequenciaExercicio2" runat="server" />
                                                                                                </td>
                                                                                                <td align="center"><%#DataBinder.Eval(Container.DataItem, "NomeFonteRecurso") %></td>
                                                                                                <td align="center"><%#((Decimal)DataBinder.Eval(Container.DataItem, "ValorFonteRecurso")).ToString("N2") %></td>
                                                                                                <td align="center">
                                                                                                    <asp:ImageButton 
                                                                                                        id="btnExcluirExercicio2" 
                                                                                                        runat="server"
                                                                                                        Visible='<%#DataBinder.Eval(Container.DataItem, "Liberado")%>' 
                                                                                                        CausesValidation="false" 
                                                                                                        CommandName="Excluir" 
                                                                                                        ImageUrl="~/Styles/Icones/editdelete.png" 
                                                                                                        OnClientClick="return confirm('Deseja realmente excluir esse registro?');" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                        <EmptyDataTemplate>
                                                                                            <div align="center" style="width: 100%;">
                                                                                                <b class="titulo">Não existe registro.</b>
                                                                                            </div>
                                                                                        </EmptyDataTemplate>
                                                                                    </asp:ListView>
                                                                                </div>
                                                                            </fieldset>
                                                                        </div>

                                                                        <div id="trDemandasExercicio2" runat="server" visible="true"> 
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Recursos financeiros Demandas Parlamentares</b></legend>
                                                                             
                                                                            <div class="row cells2">
                                                                                <div class="cell">
                                                                                    <b>Código / Número da Demanda Parlamentar:</b><br />
                                                                                    <div class="input-control text">
                                                                                        <asp:TextBox ID="txtCodigoDemandaExercicio2" runat="server" Text=" " Style="text-align: right;" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="cell">
                                                                                    <b>Objeto da Demanda Parlamentar:</b><br />
                                                                                    <div class="input-control text">
                                                                                        <asp:TextBox ID="txtObjetoDemandaExercicio2" runat="server" Text=" " Style="text-align: right;" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row cells2">
                                                                                    <div class="cell">
                                                                                        <b>Contrapartida Municipal:</b><br />
                                                                                        <asp:RadioButtonList ID="rblContraPartida2" runat="server" RepeatDirection="Horizontal"
                                                                                            AutoPostBack="True" OnSelectedIndexChanged="rblContraPartida2_SelectedIndexChanged">
                                                                                            <asp:ListItem Value="1" Text="Sim" />
                                                                                            <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                                                        </asp:RadioButtonList>
                                                                                    </div>
                                                                                <div id="trValorContraExercicio2" class="cell" visible="false" runat="server">
                                                                                    <b></b><br />
                                                                                    <div class="input-control text">
                                                                                        <asp:TextBox ID="txtValorContraExercicio2" runat="server" Text="0,00" Style="text-align: right;" /> 
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                           </fieldset>
                                                                        </div>																

                                                                        <div class="row">
                                                                            <div class="cell" align="center">
                                                                                <asp:Button ID="btnSalvarExercicio2" runat="server" Text="Salvar" SkinID="button-save"
                                                                                    Width="89px" OnClick="btnSalvar_Click" ValidationGroup="vgCampos" />
                                                                            </div>
                                                                        </div>
                                                                    </fieldset>
                                                                </div>

                                                                <div class="frame" id="frame1_5_3">
                                                                    <fieldset class="border-blue">
                                                                        <legend class="lgnd"><b class="fg-blue">Recursos Financeiros</b></legend>
                                                                        <div class="row">
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Fundos Municipais</b></legend>
                                                                                <div class="row cells3">
                                                                                    <div class="cell">
                                                                                        <b>Assistência Social:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFMASExercicio3" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Direitos da Criança e do Adolescente:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFMDCAExercicio3" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Idoso:</b>
                                                                                        <br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFMIExercicio3" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </fieldset>
                                                                        </div>
                                                                        <div class="row">
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Fundos Estaduais</b></legend>
                                                                                <div class="row cells3">
                                                                                    <div class="cell">
                                                                                        <b>Assistência Social:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFEASExercicio3" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell" id="Div4" runat="server">
                                                                                        <b>FEAS - Reprogramação Ano Anterior:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFEASAnoAnteriorExercicio3" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Direitos da Criança e do Adolescente:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFEDCAExercicio3" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row cells3">
                                                                                    <div class="cell">
                                                                                        <b>Idoso:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFEIExercicio3" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="cell" id="Div9" runat="server">
                                                                                        <b>FEAS - Demandas Parlamentares</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox runat="server" ID="txtFEASDemandasExercicio3" Text="0,00" Style="text-align:right;"/>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="cell">
		                                                                                <b>FEAS - Reprogramação Demandas Parlamentares</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox runat="server" ID="txtFEASReprogramacaoDemandasParlamentaresExercicio3" Text="0,00" Style="text-align:right;"/>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </fieldset>
                                                                        </div>
                                                                        <div class="row">
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Fundos Nacionais</b></legend>
                                                                                <div class="row cells3">
                                                                                    <div class="cell">
                                                                                        <b>Assistência Social:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFNASExercicio3" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Direitos da Criança e do Adolescente:</b>
                                                                                        <br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFNDCAExercicio3" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Idoso:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFNIExercicio3" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </fieldset>
                                                                        </div>
                                                                        <div id="Div5" runat="server" visible="true">
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Outras Fontes Financeiras</b></legend>
                                                                                <div class="row">
                                                                                    <div class="cell">
                                                                                        <b>Existem outras fontes de financiamento para custeio deste serviço que não passam pelo FMAS?</b><br />
                                                                                        <asp:RadioButtonList ID="rblOutrasFontesExercicio3" runat="server" RepeatDirection="Horizontal"
                                                                                            AutoPostBack="True" OnSelectedIndexChanged="rblOutrasFontesExercicio3_SelectedIndexChanged">
                                                                                            <asp:ListItem Value="1" Text="Sim" />
                                                                                            <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                                                        </asp:RadioButtonList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row cells2" runat="server" id="trMotivoEstadualizadoExercicio3" visible="false">
                                                                                    <div class="cell">
                                                                                        <b>Indique qual é a outra fonte de recursos para este serviço</b><br />
                                                                                        <div class="input-control text full-size">
                                                                                            <asp:TextBox ID="txtNomeRecursoExercicio3" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Indique o valor dos recursos para este serviço</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtValorRecursoExercicio3" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row" id="trAddRecursoExercicio3" runat="server" visible="false">
                                                                                    <div class="cell">
                                                                                        <asp:Button ID="btnAdicionarRecursoExercicio3" runat="server" Width="89" Text="Adicionar" OnClick="btnAdicionarRecursoExercicio3_Click" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row mid-size" id="tdlstRecursosAdicionadosExercicio3" runat="server" visible="false">
                                                                                    <asp:ListView ID="lstRecursosAdicionadosExercicio3" OnItemDataBound="lstRecursosAdicionadosExercicio3_ItemDataBound" runat="server" DataKeyNames="Id" OnItemCommand="lstRecursosAdicionadosExercicio3_ItemCommand">
                                                                                        <LayoutTemplate>
                                                                                            <table border="0" cellpadding="0" cellspacing="0" class="table striped border bordered">
                                                                                                <thead class="info">
                                                                                                    <tr>
                                                                                                        <th width="25px"></th>
                                                                                                        <th width="250px">Fonte de Recurso</th>
                                                                                                        <th width="100px">Valor do Recurso</th>
                                                                                                        <th width="50px">Excluir </th>
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
                                                                                                    <asp:Label ID="lblSequenciaExercicio3" runat="server" />
                                                                                                </td>
                                                                                                <td align="center"><%#DataBinder.Eval(Container.DataItem, "NomeFonteRecurso") %></td>
                                                                                                <td align="center"><%#((Decimal)DataBinder.Eval(Container.DataItem, "ValorFonteRecurso")).ToString("N2") %></td>
                                                                                                <td align="center">
                                                                                                    <asp:ImageButton 
                                                                                                        id="btnExcluirExercicio3" 
                                                                                                        runat="server" 
                                                                                                        Visible='<%#DataBinder.Eval(Container.DataItem, "Liberado")%>'
                                                                                                        CausesValidation="false" 
                                                                                                        CommandName="Excluir" 
                                                                                                        ImageUrl="~/Styles/Icones/editdelete.png" 
                                                                                                        OnClientClick="return confirm('Deseja realmente excluir esse registro?');" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                        <EmptyDataTemplate>
                                                                                            <div align="center" style="width: 100%;">
                                                                                                <b class="titulo">Não existe registro.</b>
                                                                                            </div>
                                                                                        </EmptyDataTemplate>
                                                                                    </asp:ListView>
                                                                                </div>
                                                                            </fieldset>
                                                                        </div>

                                                                        <div id="trDemandasExercicio3" runat="server" visible="true"> 
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Recursos financeiros Demandas Parlamentares</b></legend>
                                                                             
                                                                            <div class="row cells2">
                                                                                <div class="cell">
                                                                                    <b>Código / Número da Demanda Parlamentar:</b><br />
                                                                                    <div class="input-control text">
                                                                                        <asp:TextBox ID="txtCodigoDemandaExercicio3" runat="server" Text=" " Style="text-align: right;" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="cell">
                                                                                    <b>Objeto da Demanda Parlamentar:</b><br />
                                                                                    <div class="input-control text">
                                                                                        <asp:TextBox ID="txtObjetoDemandaExercicio3" runat="server" Text=" " Style="text-align: right;" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row cells2">
                                                                                    <div class="cell">
                                                                                        <b>Contrapartida Municipal:</b><br />
                                                                                        <asp:RadioButtonList ID="rblContraPartida3" runat="server" RepeatDirection="Horizontal"
                                                                                            AutoPostBack="True" OnSelectedIndexChanged="rblContraPartida3_SelectedIndexChanged">
                                                                                            <asp:ListItem Value="1" Text="Sim" />
                                                                                            <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                                                        </asp:RadioButtonList>
                                                                                    </div>
                                                                                <div id="trValorContraExercicio3" class="cell" visible="false" runat="server">
                                                                                    <b></b><br />
                                                                                    <div class="input-control text">
                                                                                        <asp:TextBox ID="txtValorContraExercicio3" runat="server" Text="0,00" Style="text-align: right;" /> 
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                           </fieldset>
                                                                        </div>

                                                                        <div class="row">
                                                                            <div class="cell" align="center">
                                                                                <asp:Button ID="btnSalvarExercicio3" runat="server" Text="Salvar" SkinID="button-save"
                                                                                    Width="89px" OnClick="btnSalvar_Click" ValidationGroup="vgCampos" />
                                                                            </div>
                                                                        </div>
                                                                    </fieldset>
                                                                </div>

                                                                <div class="frame" id="frame1_5_4">
                                                                    <fieldset class="border-blue">
                                                                        <legend class="lgnd"><b class="fg-blue">Recursos Financeiros</b></legend>
                                                                        <div class="row">
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Fundos Municipais</b></legend>
                                                                                <div class="row cells3">
                                                                                    <div class="cell">
                                                                                        <b>Assistência Social:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFMASExercicio4" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Direitos da Criança e do Adolescente:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFMDCAExercicio4" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Idoso:</b>
                                                                                        <br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFMIExercicio4" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </fieldset>
                                                                        </div>
                                                                        <div class="row">
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Fundos Estaduais</b></legend>
                                                                                <div class="row cells3">
                                                                                    <div class="cell">
                                                                                        <b>Assistência Social:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFEASExercicio4" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell" id="Div6" runat="server">
                                                                                        <b>FEAS - Reprogramação Ano Anterior:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFEASAnoAnteriorExercicio4" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Direitos da Criança e do Adolescente:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFEDCAExercicio4" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row cells3">
                                                                                    <div class="cell">
                                                                                        <b>Idoso:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFEIExercicio4" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="cell" id="Div10" runat="server">
                                                                                        <b>FEAS - Demandas Parlamentares</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox runat="server" ID="txtFEASDemandasExercicio4" Text="0,00" Style="text-align:right;"/>
                                                                                        </div>
                                                                                    </div>

                                                                                    <div class="cell">
		                                                                                <b>FEAS - Reprogramação Demandas Parlamentares</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox runat="server" ID="txtFEASReprogramacaoDemandasParlamentaresExercicio4" Text="0,00" Style="text-align:right;"/>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </fieldset>
                                                                        </div>
                                                                        <div class="row">
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Fundos Nacionais</b></legend>
                                                                                <div class="row cells3">
                                                                                    <div class="cell">
                                                                                        <b>Assistência Social:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFNASExercicio4" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Direitos da Criança e do Adolescente:</b>
                                                                                        <br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFNDCAExercicio4" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Idoso:</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtFNIExercicio4" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </fieldset>
                                                                        </div>
                                                                        <div id="Div7" runat="server" visible="true">
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Outras Fontes Financeiras</b></legend>
                                                                                <div class="row">
                                                                                    <div class="cell">
                                                                                        <b>Existem outras fontes de financiamento para custeio deste serviço que não passam pelo FMAS?</b><br />
                                                                                        <asp:RadioButtonList ID="rblOutrasFontesExercicio4" runat="server" RepeatDirection="Horizontal"
                                                                                            AutoPostBack="True" OnSelectedIndexChanged="rblOutrasFontesExercicio4_SelectedIndexChanged">
                                                                                            <asp:ListItem Value="1" Text="Sim" />
                                                                                            <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                                                        </asp:RadioButtonList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row cells2" runat="server" id="trMotivoEstadualizadoExercicio4" visible="false">
                                                                                    <div class="cell">
                                                                                        <b>Indique qual é a outra fonte de recursos para este serviço</b><br />
                                                                                        <div class="input-control text full-size">
                                                                                            <asp:TextBox ID="txtNomeRecursoExercicio4" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="cell">
                                                                                        <b>Indique o valor dos recursos para este serviço</b><br />
                                                                                        <div class="input-control text">
                                                                                            <asp:TextBox ID="txtValorRecursoExercicio4" runat="server" Text="0,00" Style="text-align: right;" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row" id="trAddRecursoExercicio4" runat="server" visible="false">
                                                                                    <div class="cell">
                                                                                        <asp:Button ID="btnAdicionarRecursoExercicio4" runat="server" Width="89" Text="Adicionar" OnClick="btnAdicionarRecursoExercicio4_Click" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row mid-size" id="tdlstRecursosAdicionadosExercicio4" runat="server" visible="false">
                                                                                    <asp:ListView ID="lstRecursosAdicionadosExercicio4" OnItemDataBound="lstRecursosAdicionadosExercicio4_ItemDataBound" runat="server" DataKeyNames="Id" OnItemCommand="lstRecursosAdicionadosExercicio4_ItemCommand">
                                                                                        <LayoutTemplate>
                                                                                            <table border="0" cellpadding="0" cellspacing="0" class="table striped border bordered">
                                                                                                <thead class="info">
                                                                                                    <tr>
                                                                                                        <th width="25px"></th>
                                                                                                        <th width="250px">Fonte de Recurso</th>
                                                                                                        <th width="100px">Valor do Recurso</th>
                                                                                                        <th width="50px">Excluir </th>
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
                                                                                                    <asp:Label ID="lblSequenciaExercicio4" runat="server" />
                                                                                                </td>
                                                                                                <td align="center"><%#DataBinder.Eval(Container.DataItem, "NomeFonteRecurso") %></td>
                                                                                                <td align="center"><%#((Decimal)DataBinder.Eval(Container.DataItem, "ValorFonteRecurso")).ToString("N2") %></td>
                                                                                                <td align="center">
                                                                                                    <asp:ImageButton 
                                                                                                        id="btnExcluirExercicio4" 
                                                                                                        runat="server" 
                                                                                                        Visible='<%#DataBinder.Eval(Container.DataItem, "Liberado")%>'
                                                                                                        CausesValidation="false" 
                                                                                                        CommandName="Excluir" 
                                                                                                        ImageUrl="~/Styles/Icones/editdelete.png" 
                                                                                                        OnClientClick="return confirm('Deseja realmente excluir esse registro?');" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                        <EmptyDataTemplate>
                                                                                            <div align="center" style="width: 100%;">
                                                                                                <b class="titulo">Não existe registro.</b>
                                                                                            </div>
                                                                                        </EmptyDataTemplate>
                                                                                    </asp:ListView>
                                                                                </div>
                                                                            </fieldset>
                                                                        </div>

                                                                        <div id="trDemandasExercicio4" runat="server" visible="true"> 
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Recursos financeiros Demandas Parlamentares</b></legend>
                                                                             
                                                                            <div class="row cells2">
                                                                                <div class="cell">
                                                                                    <b>Código / Número da Demanda Parlamentar:</b><br />
                                                                                    <div class="input-control text">
                                                                                        <asp:TextBox ID="txtCodigoDemandaExercicio4" runat="server" Text=" " Style="text-align: right;" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="cell">
                                                                                    <b>Objeto da Demanda Parlamentar:</b><br />
                                                                                    <div class="input-control text">
                                                                                        <asp:TextBox ID="txtObjetoDemandaExercicio4" runat="server" Text=" " Style="text-align: right;" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row cells2">
                                                                                    <div class="cell">
                                                                                        <b>Contrapartida Municipal:</b><br />
                                                                                        <asp:RadioButtonList ID="rblContraPartida4" runat="server" RepeatDirection="Horizontal"
                                                                                            AutoPostBack="True" OnSelectedIndexChanged="rblContraPartida4_SelectedIndexChanged">
                                                                                            <asp:ListItem Value="1" Text="Sim" />
                                                                                            <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                                                        </asp:RadioButtonList>
                                                                                    </div>
                                                                                <div id="trValorContraExercicio4" class="cell" visible="false" runat="server">
                                                                                    <b></b><br />
                                                                                    <div class="input-control text">
                                                                                        <asp:TextBox ID="txtValorContraExercicio4" runat="server" Text="0,00" Style="text-align: right;" /> 
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                           </fieldset>
                                                                        </div>	

                                                                        <div class="row">
                                                                            <div class="cell" align="center">
                                                                                <asp:Button ID="btnSalvarExercicio4" runat="server" Text="Salvar" SkinID="button-save"
                                                                                    Width="89px" OnClick="btnSalvar_Click" ValidationGroup="vgCampos" />
                                                                            </div>
                                                                        </div>
                                                                    </fieldset>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell" align="center">

                                            <asp:Button ID="btnVoltar" runat="server" Text="Voltar"
                                                OnClick="btnVoltar_Click" CausesValidation="false" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
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
                    </div>
                </div>

            </form>
            <asp:HiddenField ID="hdfIdRecursosHumanos" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
