<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SelecionarRelatorio.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.SelecionarRelatorio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .ui-state-default.ui-corner-all.textbox[Disabled] {
            background: #ebebeb;
            color: gray;
            border-color: gray;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnBusca" runat="server">
        <ContentTemplate>
            <div class="accordion">
                <div class="frame active">
                    <div class="heading">
                        Relatórios Descritivos
                             <a href="#" runat="server" id="linkAlteracoesQuadro10" visible="false">
                                 <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                             </a>
                        <span class="mif-home icon"></span>
                    </div>
                    <div class="content">
                        <div class="formInput">
                            <div class="grid">
                                <div class="row">
                                    <div class="cell">
                                        <div class="row cells3">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                Selecione um relatório:
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select full-size">
                                                    <asp:DropDownList ID="ddlRelatorioDescritivo" runat="server" OnSelectedIndexChanged="ddlRelatorioDescritivo_SelectedIndexChanged"
                                                        AutoPostBack="True" Style="margin-left: 0px">
                                                        <asp:ListItem Value="0">[Selecione uma opção]</asp:ListItem>
                                                        <asp:ListItem Value="1">1. Informações municipais básicas</asp:ListItem>
                                                        <asp:ListItem Value="2">2. Informações básicas por DRADS</asp:ListItem>
                                                        <%--<asp:ListItem Value="3">3. Estruturação do órgão gestor da Assistência Social</asp:ListItem>--%>
                                                        <asp:ListItem Value="4">4. Recursos humanos do órgão gestor</asp:ListItem>
                                                        <asp:ListItem Value="5">5. Recursos humanos dos serviços</asp:ListItem>
                                                        <asp:ListItem Value="6">6. Informações sobre o FMAS</asp:ListItem>
                                                        <asp:ListItem Value="7">7. Conselhos existentes nos municípios</asp:ListItem>
                                                        <%--<asp:ListItem Value="8">8. Diagnóstico socioterritorial</asp:ListItem>--%>
                                                        <asp:ListItem Value="9">9. Situacões de vulnerabilidade e/ou risco social</asp:ListItem>
                                                        <%--<asp:ListItem Value="10">10. Presença de povos tradicionais e/ou grupos específicos nos municípios</asp:ListItem>
                                                        <asp:ListItem Value="11">11. Organizações e unidades públicas</asp:ListItem>--%>
                                                        <asp:ListItem Value="12">12. Rede de serviços socioassistenciais</asp:ListItem>
                                                        <%-- <asp:ListItem Value="13">13. Atendimentos específicos realizados pelos serviços socioassistenciais</asp:ListItem>--%>
                                                        <%--
                                                            <asp:ListItem Value="14">14. Funcionamento dos CRAS</asp:ListItem>
                                                            <asp:ListItem Value="15">15. Funcionamento dos CREAS</asp:ListItem>
                                                            <asp:ListItem Value="16">16. Funcionamento dos Centros POP</asp:ListItem>--%>
                                                        <asp:ListItem Value="17">17. Programas e projetos</asp:ListItem>
                                                        
                                                        <%--<asp:ListItem Value="18">18. Nº de beneficiários e recursos financeiros dos Benefícios Continuados (BPC)</asp:ListItem>--%>
                                                        <asp:ListItem Value="19">19. Informações sobre os benefícios eventuais</asp:ListItem>
                                                        <%--  <asp:ListItem Value="20">20. Integração entre  serviços, programas e benefícios</asp:ListItem>--%>
                                                        <%--<asp:ListItem Value="21">21. Ações planejadas no PMAS</asp:ListItem>--%>
                                                        <asp:ListItem Value="22">22. Ações de vigilância socioassistencial</asp:ListItem>
                                                        <asp:ListItem Value="23">23. Ações de monitoramento</asp:ListItem>
                                                        <asp:ListItem Value="24">24. Ações de avaliação</asp:ListItem>
                                                       <%-- <asp:ListItem Value="25">25. Serviços estadualizados</asp:ListItem>--%>
                                                        <%-- <asp:ListItem Value="26">26. Serviços intermunicipais</asp:ListItem>--%>
                                                        <%-- <asp:ListItem Value="27">27. Distribuição dos recursos do cofinanciamento estadual, segundo as proteções sociais</asp:ListItem>--%>
                                                        <asp:ListItem Value="28">28. Distribuição dos recursos do cofinanciamento estadual, segundo os programas de trabalho</asp:ListItem>
                                                        <asp:ListItem Value="29">29. Cronogramas de Desembolso</asp:ListItem>
                                                        <%--<asp:ListItem Value="30">30. Prestação de Contas</asp:ListItem>--%>
                                                        <asp:ListItem Value="31">31. Serviços Regionalizados</asp:ListItem>
                                                        <asp:ListItem Value="32">32. AEPETI</asp:ListItem>
                                                        <asp:ListItem Value="33">33. Prestação de Contas Proteção social Básica</asp:ListItem>
                                                        <asp:ListItem Value="34">34. Prestação de Contas Proteção social de Média Complexidade</asp:ListItem>
                                                        <asp:ListItem Value="35">35. Prestação de Contas Proteção social de Alta Complexidade</asp:ListItem>
                                                        <asp:ListItem Value="36">36. Prestação de Contas Beneficios Eventuais</asp:ListItem>
                                                        <asp:ListItem Value="37">37. Prestação de Contas Programas & Projetos</asp:ListItem>
                                                        <asp:ListItem Value="38">38. Status Prestação de Contas</asp:ListItem>
                                                        <asp:ListItem Value="39">39. Status Lei Orçamentaria</asp:ListItem>
                                                        <asp:ListItem Value="40">40. Status Execução Financeira</asp:ListItem>
                                                        <asp:ListItem Value="41">41. Auxílio-Reclusão / Pensão por Morte</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells5" visible="false" runat="server" id="trSituacoesVulnerabilidade">
                                            <div class="cell colspan2" align="right" style="height: 171px;">
                                                Situações de vulnerabilidade ou risco social atendidas pelos serviços:<br />
                                                <br />
                                                <div style="width: 350px; height: 120px; overflow: auto; padding: 0 5px 0 0">
                                                    <asp:ListBox runat="server" ID="lstSituacoesVulnerabilidadeDisponiveis" SelectionMode="Multiple"
                                                        Width="350px" Style="overflow: hidden;" Rows="10"></asp:ListBox>
                                                </div>
                                            </div>

                                            <div class="cell" align="center" style="height: 171px">
                                                <asp:Button runat="server" ID="btnIncluirItemSituacaoVulnerabilidade" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnIncluirItemSituacaoVulnerabilidade_Click" /><br />
                                                <asp:Button runat="server" ID="btnIncluirListaSituacaoVulnerabilidade" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;"
                                                    Width="29px" OnClick="btnIncluirListaSituacaoVulnerabilidade_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirItemSituacaoVulnerabilidade" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnExcluirItemSituacaoVulnerabilidade_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirListaSituacaoVulnerabilidade" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;"
                                                    Width="29px" OnClick="btnExcluirListaSituacaoVulnerabilidade_Click" /><br />
                                            </div>
                                            <div class="cell colspan2" align="left" style="height: 171px">
                                                Situações de vulnerabilidade ou risco social atendidas pelos serviços selecionadas:<br />
                                                <asp:ListBox runat="server" Rows="10" ID="lstSituacoesVulnerabilidadeSelecionadas"
                                                    SelectionMode="Multiple" Height="120px" Width="350px"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="row cells5" visible="false" runat="server" id="trSituacoesEspecificas">
                                            <div class="cell colspan2" align="right" style="height: 171px">
                                                Situações específicas vivenciadas pelos usuários atendidos pelos serviços:<br />
                                                <br />
                                                <div style="width: 350px; height: 120px; overflow: auto; padding: 0 5px 0 0;">
                                                    <asp:ListBox runat="server" Rows="10" ID="lstSituacoesEspecificasDisponiveis" SelectionMode="Multiple"
                                                        Width="350px"></asp:ListBox>
                                                </div>
                                            </div>
                                            <div class="cell" align="center" style="height: 171px">
                                                <asp:Button runat="server" ID="btnIncluirItemSituacaoEspecifica" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnIncluirItemSituacaoEspecifica_Click" /><br />
                                                <asp:Button runat="server" ID="btnIncluirListaSituacaoEspecifica" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnIncluirListaSituacaoEspecifica_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirItemSituacaoEspecifica" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnExcluirItemSituacaoEspecifica_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirListaSituacaoEspecifica" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnExcluirListaSituacaoEspecifica_Click" /><br />
                                            </div>
                                            <div class="cell colspan2" align="left" style="height: 171px">
                                                Situações específicas vivenciadas pelos usuários atendidos pelos serviços selecionadas:<br />
                                                <asp:ListBox runat="server" Rows="10" ID="lstSituacoesEspecificasSelecionadas" SelectionMode="Multiple"
                                                    Height="120px" Width="350px"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="row cells3" id="trMunicipioEscolha" runat="server" visible="false">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                Município:
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select mid-size">
                                                    <asp:DropDownList ID="ddlMunicipio" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells3" id="trTipoBeneficioEventual" runat="server" visible="false">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                Tipo de Benefício:
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select low-size">
                                                    <asp:DropDownList ID="ddlTipoBeneficioEventual" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Text="Selecione" Selected="True" />
                                                        <asp:ListItem Value="1" Text="Auxílio Natalidade" />
                                                        <asp:ListItem Value="2" Text="Auxílio Funeral" />
                                                        <asp:ListItem Value="3" Text="Calamidades Públicas e Emergências" />
                                                        <asp:ListItem Value="4" Text="Vulnerabilidade Temporária" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" id="trTipoCronograma" runat="server" visible="false">
                                            <div class="cell">
                                                <div class="row cells5">
                                                    <div class="cell colspan2" align="right" style="vertical-align: middle; padding: 10px;">
                                                        Cronograma referente a:<br />
                                                        <asp:ListBox runat="server" Rows="10" ID="lstCronogramas" SelectionMode="Multiple"
                                                            Height="160" Width="220" Style="margin-left: 0px"></asp:ListBox>
                                                    </div>
                                                    <div class="cell" align="center" style="vertical-align: bottom; padding: 20px;">
                                                        <asp:Button runat="server" ID="btnIncluirCronograma" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnIncluirCronograma_Click" /><br />
                                                        <asp:Button runat="server" ID="btnIncluirListaCronograma" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnIncluirListaCronograma_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluirCronograma" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnExcluirCronograma_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluirListaCronogramas" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnExcluirListaCronogramas_Click" /><br />
                                                    </div>
                                                    <div class="cell colspan2" align="left" style="vertical-align: middle; padding: 10px;">
                                                        Cronogramas selecionados:<br />
                                                        <asp:ListBox runat="server" Rows="10" ID="lstCronogramasEscolhidos" SelectionMode="Multiple"
                                                            Height="160" Width="220"></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div class="row" id="trTotalCronograma" align="center" runat="server" visible="false">
                                                    <div class="cell">
                                                        <asp:CheckBox ID="chkTotalCronograma" runat="server" Text="Mostrar somente valor o Total de todas as proteções" Font-Bold="True" OnCheckedChanged="chkTotalCronograma_CheckedChanged" AutoPostBack="True" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells3" id="trTipoProtecao" runat="server" visible="false">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                <asp:Label ID="Label6" runat="server" Width="107px">Tipo de proteção:</asp:Label>
                                            </div>
                                            <div class="cell colpan2" align="left">
                                                <div class="input-control select full-size">
                                                    <asp:DropDownList ID="ddlTipoProtecao" runat="server" OnSelectedIndexChanged="rblTipoProtecao_SelectedIndexChanged"
                                                        RepeatDirection="Horizontal" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells3" id="trTipoServico" runat="server" visible="false">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                <asp:Label ID="Label1" runat="server">Tipo de serviço:</asp:Label>
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select mid-size">
                                                    <asp:DropDownList ID="ddlTipoServico" runat="server" OnSelectedIndexChanged="ddlTipoServico_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells3" id="trServicoSubtificado" runat="server" visible="false">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                <asp:Label ID="Label4" runat="server" Width="200px">Especifique:</asp:Label>
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select full-size">
                                                    <asp:DropDownList ID="ddlServicoSubtipificado" runat="server"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlServicoSubtipificado_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells3" id="trProblemaSocial" runat="server" visible="false">
                                            <div class="cell " align="right" style="vertical-align: middle; padding: 10px;">
                                                <asp:Label ID="lblProblemaSocial" runat="server" Width="244px">Situação de vulnerabilidade ou risco social:</asp:Label>
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select full-size">
                                                    <asp:DropDownList ID="ddlProblemaSocial" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div class="input-control select low-size">
                                                    <asp:DropDownList ID="ddlProblemaSocialCondicao" runat="server">
                                                        <asp:ListItem Value="OU" Text="OU" />
                                                        <asp:ListItem Value="E" Text="E" />
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div class="input-control select full-size">
                                                    <asp:DropDownList ID="ddlProblemaSocial2" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells3" id="trAbrangenciaServico" runat="server" visible="false">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                <asp:Label ID="lblAbrangenciaServico" runat="server">Abrangência:</asp:Label>
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select mid-size">
                                                    <asp:DropDownList ID="ddlAbrangenciaServico" runat="server">
                                                        <asp:ListItem Value="0" Text=" Selecione " Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Municipal"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Intermunicipal"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" id="trLabelCaracteristicasUsuarios" runat="server" visible="false">
                                            <div class="cell" align="center" style="vertical-align: middle; padding: 10px;">
                                                <asp:Label ID="Label3" Text="Características dos usuários" Font-Bold="true" Font-Underline="true"
                                                    runat="server">
                                                </asp:Label>
                                            </div>
                                        </div>
                                        <div class="row cells3" id="trPublicoAlvo" runat="server" visible="false">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                Usuários:
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select mid-size">
                                                    <asp:DropDownList ID="ddlPublicoAlvo" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells3" visible="false" runat="server" id="trExercicio">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                Exercício:
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select full-size">
                                                    <asp:DropDownList ID="ddlExercicio" runat="server">
                                                        <asp:ListItem Value="0">[Selecione uma opção:]</asp:ListItem>
                                                        <asp:ListItem Value="2022">2022</asp:ListItem>
                                                        <asp:ListItem Value="2023">2023</asp:ListItem>
                                                        <asp:ListItem Value="2024">2024</asp:ListItem>
                                                        <asp:ListItem Value="2025">2025</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells3" visible="false" runat="server" id="trExercicioAuxilioReclusaoPensaoMorte">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                Exercício:
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select full-size">
                                                    <asp:DropDownList ID="ddlExercicioAuxilioReclusaoPensaoMorte" runat="server">
                                                        <asp:ListItem Value="0">[Selecione uma opção:]</asp:ListItem>
                                                        <asp:ListItem Value="2024">2024</asp:ListItem>
                                                        <asp:ListItem Value="2025">2025</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row cells3" id="trSexo" runat="server" visible="false">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                Sexo:
                                            </div>
                                            <div class="cell colspan2" align="left" colspan="2">
                                                <asp:DropDownList ID="ddlSexo" runat="server">
                                                    <asp:ListItem Value="0" Text=" Selecione " Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Feminino"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Masculino"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Ambos os sexos"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row cells3" id="trRegiaoMoradia" runat="server" visible="false">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                Região moradia:
                                            </div>
                                            <td align="left" colspan="2">
                                                <asp:DropDownList ID="ddlRegiaoMoradia" runat="server">
                                                    <asp:ListItem Value="0" Text=" Selecione " Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Zona Urbana"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Zona Rural"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Ambas"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </div>
                                        <div class="row cells3" id="trCaracteristicasTerritorio" runat="server" visible="false">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                Demanda prioritária do serviço proveniente de:
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select mid-size">
                                                    <asp:DropDownList ID="ddlCaracteristicasTerritorio" runat="server">
                                                        <asp:ListItem Value="0" Text=" Selecione " Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="10" Text="Adensamento populacional decorrente de instalação prisional" />
                                                        <asp:ListItem Value="11" Text="População flutuante decorrente de instalação prisional" />
                                                        <asp:ListItem Value="1" Text="Assentamento" />
                                                        <asp:ListItem Value="2" Text="Comunidade indígena" />
                                                        <asp:ListItem Value="3" Text="Comunidade quilombola" />
                                                        <asp:ListItem Value="7" Text="Morador de habitação subnormal" />
                                                        <asp:ListItem Value="9" Text="População ribeirinha/calhas de rios" />
                                                        <asp:ListItem Value="8" Text="Nenhuma das características citadas" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells5" visible="false" runat="server" id="trTipoExecutora">
                                            <div class="cell colspan2" align="right">
                                                Locais de execução disponíveis<br />
                                                <asp:ListBox runat="server" Rows="8" ID="lstTipoExecutorasDisponiveis" SelectionMode="Multiple"
                                                    Width="234px" />
                                            </div>
                                            <div class="cell" align="center">
                                                <asp:Button runat="server" ID="btnIncluirItemTipoExecutora" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnIncluirItemTipoExecutora_Click" /><br />
                                                <asp:Button runat="server" ID="btnIncluirListaTipoExecutora" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnIncluirListaTipoExecutora_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirItemTipoExecutora" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnExcluirItemTipoExecutora_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirListaTipoExecutora" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnExcluirListaTipoExecutora_Click" /><br />
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                Locais de execução selecionados<br>
                                                <asp:ListBox runat="server" Rows="8" ID="lstTipoExecutorasSelecionadas" SelectionMode="Multiple"
                                                    Width="234px"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="row cells5" visible="false" runat="server" id="trAcoes">
                                            <div class="cell colspan2" align="right">
                                                Tipos de ações disponíveis<br>
                                                <asp:ListBox runat="server" Rows="10" ID="lstTiposAcoesDisponiveis" SelectionMode="Multiple"
                                                    Height="120px" Width="319px" Style="margin-left: 0px"></asp:ListBox>
                                            </div>
                                            <div class="cell" align="center">
                                                <asp:Button runat="server" ID="btnIncluirAcoes" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnIncluirAcoes_Click" /><br />
                                                <asp:Button runat="server" ID="btnIncluirListaAcoes" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnIncluirListaAcoes_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirAcoes" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnExcluirAcoes_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirListaAcoes" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnExcluirListaAcoes_Click" /><br />
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                Tipos de ações selecionadas<br />
                                                <asp:ListBox runat="server" Rows="10" ID="lstTiposAcoesSelecionadas" SelectionMode="Multiple"
                                                    Height="120px" Width="335px"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="row cells5" visible="false" runat="server" id="trProgramas">
                                            <div class="cell colspan2" align="right">
                                                Tipos de programas disponíveis<br />
                                                <asp:ListBox runat="server" Rows="10" ID="lstProgramasDisponiveis" SelectionMode="Multiple"
                                                    Height="140px" Width="234px" Style="margin-left: 0px">
                                                    <asp:ListItem Value="5">Ação Jovem</asp:ListItem>
                                                    <asp:ListItem Value="6">Renda Cidadã</asp:ListItem>
                                                    <asp:ListItem Value="1">BPC - Idoso</asp:ListItem>
                                                    <asp:ListItem Value="2">BPC - PCD</asp:ListItem>
                                                    <asp:ListItem Value="3">Bolsa Família</asp:ListItem>
                                                    <asp:ListItem Value="4">PETI</asp:ListItem>
                                                    <asp:ListItem Value="8">Municipais</asp:ListItem>
                                                </asp:ListBox>
                                            </div>
                                            <div class="cell" align="center">
                                                <asp:Button runat="server" ID="btnIncluirProgramas" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnIncluirProgramas_Click" /><br />
                                                <asp:Button runat="server" ID="btnIncluirListaProgramas" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnIncluirListaProgramas_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirProgramas" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnExcluirProgramas_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirListaProgramas" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnExcluirListaProgramas_Click" /><br />
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                Tipos de programas selecionados<br />
                                                <asp:ListBox runat="server" Rows="10" ID="lstProgramasSelecionadas" SelectionMode="Multiple"
                                                    Height="140px" Width="234px"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="row" id="trAbrangencia" runat="server" visible="true">
                                            <div class="cell">
                                                <div class="row cells3">
                                                    <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                        <asp:Label ID="lblAbrangenciaRelatorioddlRelatorio" runat="server" Width="170px">Abrangência :</asp:Label>
                                                    </div>
                                                    <div class="cell colspan2" align="left" colspan="2">
                                                        <div class="input-control select low-size">
                                                            <asp:DropDownList ID="ddlRelatorio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRelatorio_SelectedIndexChanged"
                                                                Style="width: 194px">
                                                                <asp:ListItem Value="0">[Selecione uma opção:]</asp:ListItem>
                                                                <asp:ListItem Value="1">Estado</asp:ListItem>
                                                                <asp:ListItem Value="2">Drads</asp:ListItem>
                                                                <asp:ListItem Value="3">Município</asp:ListItem>
                                                                <asp:ListItem Value="4">Macrorregião</asp:ListItem>
                                                                <asp:ListItem Value="5">Região Metropolitana</asp:ListItem>
                                                                <asp:ListItem Value="6">Porte do munícipio</asp:ListItem>
                                                                <asp:ListItem Value="7">Nível de Gestão do munícípio</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row cells5" visible="false" runat="server" id="trDrads">
                                                    <div class="cell colspan2" align="right">
                                                        DRADS disponíveis<br />
                                                        <asp:ListBox runat="server" Rows="10" ID="lstDradsDisponiveis" SelectionMode="Multiple"
                                                            Height="120px" Width="150px"></asp:ListBox>
                                                    </div>
                                                    <div class="cell" align="center" style="height: 141px">
                                                        <asp:Button runat="server" ID="btnIncluirItemDrads" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnIncluirItemDrads_Click" /><br />
                                                        <asp:Button runat="server" ID="btnIncluirListaDrads" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnIncluirListaDrads_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluirItemDrads" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnExcluirItemDrads_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluirListaDrads" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnExcluirListaDrads_Click" /><br />
                                                    </div>
                                                    <div class="cell colspan2" align="left">
                                                        DRADS selecionadas<br />
                                                        <asp:ListBox runat="server" Rows="10" ID="lstDradsSelecionadas" SelectionMode="Multiple"
                                                            Height="120px" Width="150px"></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div class="row cells5" visible="false" runat="server" id="trMunicipio">
                                                    <div class="cell colspan2" align="right">
                                                        Municipios disponíveis<br>
                                                        <asp:ListBox runat="server" Rows="10" ID="lstMunicipiosDisponiveis" SelectionMode="Multiple"
                                                            Height="120px" Width="200px"></asp:ListBox>
                                                    </div>
                                                    <div class="cell" align="center">
                                                        <asp:Button runat="server" ID="btnIncluirMunicipio" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnIncluirMunicipio_Click" /><br />
                                                        <asp:Button runat="server" ID="btnIncluirListaMunicipio" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnIncluirListaMunicipio_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluirMunicipio" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnExcluirMunicipio_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluirListaMunicipio" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnExcluirListaMunicipio_Click" /><br />
                                                    </div>
                                                    <div class="cell colspan2" align="left">
                                                        Municipios selecionados<br>
                                                        <asp:ListBox runat="server" Rows="10" ID="lstMunicipiosSelecionados" SelectionMode="Multiple"
                                                            Height="120px" Width="200px"></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div class="row cells5" visible="false" runat="server" id="trMacroRegiao">
                                                    <div class="cell colspan2" align="right">
                                                        Macrorregiões disponiveis<br />
                                                        <asp:ListBox runat="server" Rows="10" ID="lstMacroRegiaoDisponivel" SelectionMode="Multiple"
                                                            Height="120px" Width="150px"></asp:ListBox>
                                                    </div>
                                                    <div class="cell" align="center">
                                                        <asp:Button runat="server" ID="btnIncluirItemMacroRegiao" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnIncluirItemMacroRegiao_Click" /><br />
                                                        <asp:Button runat="server" ID="btnIncluirListaMacroRegiao" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                            OnClick="btnIncluirListaMacroRegiao_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluirItemMacroRegiao" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnExcluirItemMacroRegiao_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluirListaMacroRegiao" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                            OnClick="btnExcluirListaMacroRegiao_Click" /><br />
                                                    </div>
                                                    <div class="cell colspan2" align="left">
                                                        Macrorregiões selecionadas<br>
                                                        <asp:ListBox runat="server" Rows="10" ID="lstmacroRegiaoSelecionada" SelectionMode="Multiple"
                                                            Height="120px" Width="150px"></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div class="row cells5" visible="false" runat="server" id="trRegiaoMetropolitana">
                                                    <div class="cell colspan2" align="right" style="vertical-align: middle; padding: 10px;">
                                                        Regiões Metropolitanas disponiveis<br>
                                                        <asp:ListBox runat="server" Rows="10" ID="lstRegiaoMetropolitanaDisponivel" SelectionMode="Multiple"
                                                            Height="150px" Width="200px"></asp:ListBox>
                                                    </div>
                                                    <div class="cell" align="center">
                                                        <asp:Button runat="server" ID="btnIncluirItemRegiaoMetropolitana" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                            OnClick="btnIncluirItemRegiaoMetropolitana_Click" /><br />
                                                        <asp:Button runat="server" ID="btnIncluirListaRegiaoMetropolitana" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                            OnClick="btnIncluirListaRegiaoMetropolitana_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluirItemRegiaoMetropolitana" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                            OnClick="btnExcluirItemRegiaoMetropolitana_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluirListaRegiaoMetropolitana" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                            OnClick="btnExcluirListaRegiaoMetropolitana_Click" /><br />
                                                    </div>
                                                    <div class="cell colspan2" align="left" style="vertical-align: middle; padding: 10px;">
                                                        Regiões Metropolitanas selecionadas<br>
                                                        <asp:ListBox runat="server" Rows="10" ID="lstRegiaoMetropolitanaSelecionada" SelectionMode="Multiple"
                                                            Height="150px" Width="200px"></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div class="row cells5" visible="false" runat="server" id="trPorteMunicipio">
                                                    <div class="cell colspan2" align="right">
                                                        Porte do Município disponiveis<br>
                                                        <asp:ListBox runat="server" Rows="10" ID="lstPorteDisponiveis" SelectionMode="Multiple"
                                                            Height="120px" Width="150px">
                                                            <asp:ListItem Value="1">Pequeno I</asp:ListItem>
                                                            <asp:ListItem Value="2">Pequeno II</asp:ListItem>
                                                            <asp:ListItem Value="3">Médio</asp:ListItem>
                                                            <asp:ListItem Value="4">Grande</asp:ListItem>
                                                            <asp:ListItem Value="5">Metropóle</asp:ListItem>
                                                        </asp:ListBox>
                                                    </div>
                                                    <div class="cell" align="center">
                                                        <asp:Button runat="server" ID="btnIncluirItemPorteMunicipio" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                            OnClick="btnIncluirItemPorteMunicipio_Click" /><br />
                                                        <asp:Button runat="server" ID="btnIncluirListaPorteMunicipio" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                            OnClick="btnIncluirListaPorteMunicipio_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluirItemPorteMunicipio" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                            OnClick="btnExcluirItemPorteMunicipio_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluirListaPorteMunicipio" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                            OnClick="btnExcluirListaPorteMunicipio_Click" /><br />
                                                    </div>
                                                    <div class="cell colspan2" align="left">
                                                        Porte do Município selecionados<br>
                                                        <asp:ListBox runat="server" Rows="10" ID="lstPorteSelecionados" SelectionMode="Multiple"
                                                            Height="120px" Width="150px"></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div class="row cells5" visible="false" runat="server" id="trAbrangenciaProgramaProjeto">
                                                    <div class="cell colspan2" align="right">
                                                        Abrangência do programa/projeto<br>
                                                        <asp:ListBox runat="server" Rows="10" ID="lstAbrangenciaProgramaProjeto" SelectionMode="Multiple"
                                                            Height="60px" Width="200px">
                                                            <asp:ListItem Text="Municipal"></asp:ListItem>
                                                            <asp:ListItem Text="Estadual"></asp:ListItem>
                                                            <asp:ListItem Text="Nacional"></asp:ListItem>
                                                        </asp:ListBox>
                                                    </div>
                                                    <div class="cell" align="center">
                                                        <asp:Button runat="server" ID="btnIncluirAbrangenciaProgramaProjeto" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnIncluirAbrangenciaProgramaProjeto_Click" /><br />
                                                        <asp:Button runat="server" ID="btnIncluirListaAbrangenciaProgramaProjeto" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                            OnClick="btnIncluirListaAbrangenciaProgramaProjeto_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluirItemAbrangenciaProgramaProjeto" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnExcluirItemAbrangenciaProgramaProjeto_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluirListaAbrangenciaProgramaProjeto" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                            OnClick="btnExcluirListaAbrangenciaProgramaProjeto_Click" /><br />
                                                    </div>
                                                    <div class="cell colspan2" align="left">
                                                        Abrangência do programa/projeto selecionados<br>
                                                        <asp:ListBox runat="server" Rows="10" ID="lstAbrangenciaProgramaProjetoSelecionados" SelectionMode="Multiple"
                                                            Height="60px" Width="200px"></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div class="row cells5" visible="false" runat="server" id="trNivelGestao">
                                                    <div class="cell colspan2" align="right" style="vertical-align: middle; padding: 10px;">
                                                        Nível de Gestão disponiveis<br>
                                                        <asp:ListBox runat="server" Rows="10" ID="lstNivelGestaoDisponiveis" SelectionMode="Multiple"
                                                            Height="120px" Width="200">
                                                            <asp:ListItem Value="1" Selected="True">Inicial</asp:ListItem>
                                                            <asp:ListItem Value="0">Básica</asp:ListItem>
                                                            <asp:ListItem Value="2">Plena</asp:ListItem>
                                                            <asp:ListItem Value="3">Não habilitado</asp:ListItem>
                                                        </asp:ListBox>
                                                    </div>
                                                    <div class="cell" align="center">
                                                        <asp:Button runat="server" ID="btnIncluirItemNivelGestao" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnIncluirItemNivelGestao_Click" /><br />
                                                        <asp:Button runat="server" ID="btnIncluirListaNivelGestao" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                            OnClick="btnIncluirListaNivelGestao_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluirItemNivelGestao" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnExcluirItemNivelGestao_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluirListaNivelGestao" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                            OnClick="btnExcluirListaNivelGestao_Click" /><br />
                                                    </div>
                                                    <div class="cell colspan2" align="left" style="vertical-align: middle; padding: 10px;">
                                                        Nível de Gestão selecionados<br>
                                                        <asp:ListBox runat="server" Rows="10" ID="lstNivelGestaoSelecionados" SelectionMode="Multiple"
                                                            Height="150px" Width="200px"></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div class="row" visible="false" id="trFinanciamento" runat="server">
                                                    <div class="cell" align="center" colspan="3">
                                                        <asp:CheckBox ID="chkConfinanciamento" runat="server" Text="Mostrar apenas serviços que recebem cofinanciamento estadual." />
                                                        <br />
                                                        <asp:CheckBox ID="chkFuncionamento" runat="server" Text="Mostrar apenas serviços em funcionamento." AutoPostBack="true" OnCheckedChanged="chkFuncionamento_CheckedChanged" />
                                                        <br />
                                                        <asp:CheckBox ID="chkDesativado" runat="server" Text="Mostrar apenas serviços desativados."  AutoPostBack="true" OnCheckedChanged="chkDesativado_CheckedChanged"/>
                                                    </div>
                                                </div>
                                                <div class="row cells3" visible="false" id="trTipoUnidade" runat="server">
                                                    <div class="cell" align="right" style="padding: 10px">
                                                        <asp:Label ID="Label5" runat="server" Width="200px">Organizações/Unidades públicas:</asp:Label>
                                                    </div>
                                                    <div class="cell colspan2" align="left" colspan="2">
                                                        <div class="input-control select mid-size">
                                                            <asp:DropDownList ID="ddlTipoUnidade" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoUnidade_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">[Selecione]</asp:ListItem>
                                                                <asp:ListItem Value="1">Unidades públicas</asp:ListItem>
                                                                <asp:ListItem Value="2">Organizações da Sociedade Civil</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row cells5" visible="false" id="trFormaAtuacao" runat="server">

                                                    <div class="cell colspan2" align="right" style="vertical-align: middle; padding: 10px;">
                                                        Forma de Atuação disponiveis<br>
                                                        <asp:ListBox runat="server" Rows="10" ID="lstFormaAtuacaoDisponiveis" SelectionMode="Multiple"
                                                            Height="120px" Width="200"></asp:ListBox>
                                                    </div>
                                                    <div class="cell" align="center">
                                                        <asp:Button runat="server" ID="btnIncluirFormaAtuacao" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnIncluirFormaAtuacao_Click" /><br />
                                                        <asp:Button runat="server" ID="btnIncluirListaFormaAtuacao" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                            OnClick="btnIncluirListaFormaAtuacao_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluirItemFormaAtuacao" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px" OnClick="btnExcluirItemFormaAtuacao_Click" /><br />
                                                        <asp:Button runat="server" ID="btnExcluiristaFormaAtuacao" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                            OnClick="btnExcluiristaFormaAtuacao_Click" /><br />
                                                    </div>
                                                    <div class="cell colspan2" align="left" style="vertical-align: middle; padding: 10px;">
                                                        Forma de Atuação selecionados<br>
                                                        <asp:ListBox runat="server" Rows="10" ID="lstFormaAtuacaoSelecionados" SelectionMode="Multiple"
                                                            Height="150px" Width="200px"></asp:ListBox>
                                                    </div>
                                                    <%--  <div class="cell" align="right" style="padding: 10px;">
                                                        <asp:Label ID="lblFormaAtuacao" runat="server" Width="170px">:</asp:Label>
                                                    </div>
                                                    <div class="cell colspan2" align="left">
                                                        <div class="input-control select mid-size">
                                                            <asp:DropDownList ID="ddlFormaAtuacao" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>--%>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row" id="trDataReferencia" runat="server" visible="false">
                                            <div class="cell">
                                                <div class="row cells3">

                                                    <div class="cell" runat="server" align="right" style="vertical-align: middle; padding: 10px;">
                                                        Data de Referência:
                                                    </div>
                                                    <div class="cell colspan2" align="left">
                                                        <asp:TextBox ID="txtDataInicial" runat="server" Enabled="false" Style="width: 100px; float: left; "></asp:TextBox>&nbsp
                                                        <asp:ImageButton ID="imgBntDataInicial" runat="server" ImageUrl="~/Styles/Icones/Calendar_scheduleHS.png" OnClick="imgBntDataInicial_Click" Style="float: left" />&nbsp                                        
                                                        <asp:Calendar AutoPostBack="true" ID="DataInicial" runat="server" Visible="false"  OnSelectionChanged="DataInicial_SelectionChanged" OnDayRender="DataInicial_DayRender" Style="float: left" ></asp:Calendar>
                                                        &nbsp&nbsp
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="cell" align="center">
                                                <asp:Button ID="btnRelatorioDescritivo" runat="server" Text="Gerar Relatório" Width="123px"
                                                    OnClick="btnRelatorioDescritivo_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="frame active">
                    <div class="heading">
                        Relatórios Quantitativos
                             <a href="#" runat="server" id="A1" visible="false">
                                 <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                             </a>
                        <span class="mif-home icon"></span>
                    </div>
                    <div class="content">
                        <div class="formInput">
                            <div class="grid">
                                <div class="row">
                                    <div class="cell">
                                        <div class="row cells3">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                Selecione um relatório:
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select full-size">
                                                    <asp:DropDownList ID="ddlRelatoriosQuantitativos" runat="server" OnSelectedIndexChanged="ddlRelatoriosQuantitativos_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                        <asp:ListItem Value="0">[Selecione uma opção]</asp:ListItem>
                                                        <asp:ListItem Value="1">1. Distribuição dos municípios segundo porte e nível de gestão </asp:ListItem>
                                                        <asp:ListItem Value="2">2. Quantidade de unidades públicas, organizações, locais de execução e serviços</asp:ListItem>
                                                        <asp:ListItem Value="3">3. Distribuição dos municípios de acordo com as situações de vulnerabilidade apontadas</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row cells3" visible="false" runat="server" id="trTipoExecutoraAbragenciaQuantitativo">
                                        <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                            Abrangência :
                                        </div>
                                        <div class="cell colspan2" align="left">
                                            <div class="input-control select low-size">
                                                <asp:DropDownList ID="ddlAbrangenciaRelatorioQuantitativo" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlAbrangenciaRelatorioQuantitativo_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">[Selecione uma opção:]</asp:ListItem>
                                                    <asp:ListItem Value="1">Estado</asp:ListItem>
                                                    <asp:ListItem Value="2">DRADS</asp:ListItem>
                                                    <asp:ListItem Value="3">Município</asp:ListItem>
                                                    <asp:ListItem Value="4">Macrorregião</asp:ListItem>
                                                    <asp:ListItem Value="5">Região Metropolitana</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row cells3" visible="false" runat="server" id="trDradsQuantitativo">
                                        <div class="cell" align="right" style="height: 141px">
                                            DRADS disponíveis<br />
                                            <asp:ListBox runat="server" Rows="10" ID="lstDradsDisponiveisQuantitativo" SelectionMode="Multiple"
                                                Height="120px" Width="200px"></asp:ListBox>
                                        </div>
                                        <div class="cell" align="center" style="height: 141px">
                                            <asp:Button runat="server" ID="btnIncluirItemDradsQuantitativo" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                OnClick="btnIncluirItemDradsQuantitativo_Click" /><br />
                                            <asp:Button runat="server" ID="btnIncluirListaDradsQuantitativo" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                OnClick="btnIncluirListaDradsQuantitativo_Click" /><br />
                                            <asp:Button runat="server" ID="btnExcluirItemDradsQuantitativo" Width="29px"
                                                OnClick="btnExcluirItemDradsQuantitativo_Click" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" /><br />
                                            <asp:Button runat="server" ID="btnExcluirListaDradsQuantitativo" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                OnClick="btnExcluirListaDradsQuantitativo_Click" /><br />
                                        </div>
                                        <div class="cell" align="left" style="height: 141px">
                                            DRADS selecionadas<br />
                                            <asp:ListBox runat="server" Rows="10" ID="lstDradsSelecionadasQuantitativo" SelectionMode="Multiple"
                                                Height="120px" Width="200px"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="row cells3" visible="false" runat="server" id="trMunicipiosQuantitativo">
                                        <div class="cell" align="right">
                                            Municipios disponíveis<br />
                                            <asp:ListBox runat="server" Rows="10" ID="lstMunicipiosDisponiveisQuantitativo" SelectionMode="Multiple"
                                                Height="120px" Width="150px"></asp:ListBox>
                                        </div>
                                        <div class="cell" align="center">
                                            <asp:Button runat="server" ID="btnIncluirMunicipioQuantitativo" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                OnClick="btnIncluirMunicipioQuantitativo_Click" /><br />
                                            <asp:Button runat="server" ID="btnIncluirListaMunicipioQuantitativo" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                OnClick="btnIncluirListaMunicipioQuantitativo_Click" /><br />
                                            <asp:Button runat="server" ID="btnExcluirMunicipioQuantitativo" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                OnClick="btnExcluirMunicipioQuantitativo_Click" /><br />
                                            <asp:Button runat="server" ID="btnExcluirListaMunicipioQuantitativo" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                OnClick="btnExcluirListaMunicipioQuantitativo_Click" /><br />
                                        </div>
                                        <div class="cell" align="left">
                                            Municipios selecionados<br />
                                            <asp:ListBox runat="server" Rows="10" ID="lstMunicipiosSelecionadosQuantitativo"
                                                SelectionMode="Multiple" Height="120px" Width="200px"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="row cells3" visible="false" runat="server" id="trMacroRegiaoQuantitativo">
                                        <div class="cell" align="right">
                                            Macrorregiões disponiveis<br>
                                            <asp:ListBox runat="server" Rows="10" ID="lstMacroRegiaoDisponivelQuantitativo" SelectionMode="Multiple"
                                                Height="120px" Width="200px"></asp:ListBox>
                                        </div>
                                        <div class="cell" align="center">
                                            <asp:Button runat="server" ID="btnIncluirItemMacroRegiaoQuantitativo" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                OnClick="btnIncluirItemMacroRegiaoQuantitativo_Click" /><br />
                                            <asp:Button runat="server" ID="btnIncluirListaMacroRegiaoQuantitativo" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;"
                                                Width="29px" OnClick="btnIncluirListaMacroRegiaoQuantitativo_Click" /><br />
                                            <asp:Button runat="server" ID="btnExcluirItemMacroRegiaoQuantitativo" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                OnClick="btnExcluirItemMacroRegiaoQuantitativo_Click" /><br />
                                            <asp:Button runat="server" ID="btnExcluirListaMacroRegiaoQuantitativo" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;"
                                                Width="29px" OnClick="btnExcluirListaMacroRegiaoQuantitativo_Click" /><br />
                                        </div>
                                        <div class="cell" align="left">
                                            Macrorregiões selecionadas<br>
                                            <asp:ListBox runat="server" Rows="10" ID="lstmacroRegiaoSelecionadaQuantitativo"
                                                SelectionMode="Multiple" Height="120px" Width="200px"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="row cells3" visible="false" runat="server" id="trRegiaoMetropolitanaQuantitativo">
                                        <div class="cell" align="right">
                                            Regiões Metropolitanas disponiveis<br>
                                            <asp:ListBox runat="server" Rows="10" ID="lstRegiaoMetropolitanaDisponivelQuantitativo"
                                                SelectionMode="Multiple" Height="120px" Width="200px"></asp:ListBox>
                                        </div>
                                        <div class="cell" align="center">
                                            <asp:Button runat="server" ID="btnIncluirItemRegiaoMetropolitanaQuantitativo" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;"
                                                Width="29px" OnClick="btnIncluirItemRegiaoMetropolitanaQuantitativo_Click" /><br />
                                            <asp:Button runat="server" ID="btnIncluirListaRegiaoMetropolitanaQuantitativo" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;"
                                                Width="29px" OnClick="btnIncluirListaRegiaoMetropolitanaQuantitativo_Click" /><br />
                                            <asp:Button runat="server" ID="btnExcluirItemRegiaoMetropolitanaQuantitativo" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;"
                                                Width="29px" OnClick="btnExcluirItemRegiaoMetropolitanaQuantitativo_Click" /><br />
                                            <asp:Button runat="server" ID="btnExcluirListaRegiaoMetropolitanaQuantitativo" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;"
                                                Width="29px" OnClick="btnExcluirListaRegiaoMetropolitanaQuantitativo_Click" /><br />
                                        </div>
                                        <div class="cell" align="left">
                                            Regiões Metropolitanas selecionadas<br>
                                            <asp:ListBox runat="server" Rows="10" ID="lstRegiaoMetropolitanaSelecionadaQuantitativo"
                                                SelectionMode="Multiple" Height="120px" Width="200px"></asp:ListBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="cell" align="center">
                                            <asp:Button ID="btnRelatorioQuantitativo" runat="server" Text="Gerar Relatório" CssClass="Botao"
                                                Width="123px" OnClick="btnRelatorioQuantitativo_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="frame active">
                    <div class="heading">
                        Relatórios Informações cadastrais
                             <a href="#" runat="server" id="A2" visible="false">
                                 <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                             </a>
                        <span class="mif-home icon"></span>
                    </div>
                    <div class="content">
                        <div class="formInput">
                            <div class="grid">
                                <div class="row">
                                    <div class="cell">
                                        <div class="row cells3">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                Selecione um relatório:
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select full-size">
                                                    <asp:DropDownList ID="ddlRelatorioCadastral" runat="server" OnSelectedIndexChanged="ddlRelatorioCadastral_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                        <asp:ListItem Value="0">[Selecione uma opção]</asp:ListItem>
                                                        <asp:ListItem Value="1">1. Cadastro das Prefeituras Municipais</asp:ListItem>
                                                        <asp:ListItem Value="2">2. Cadastro dos Órgãos Gestores Municipais da Assistência Social</asp:ListItem>
                                                        <asp:ListItem Value="3">3. Cadastro dos Conselhos</asp:ListItem>
                                                        <asp:ListItem Value="4">4. Cadastro dos locais de execução dos serviços</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells3" id="trTipoConselho" runat="server" visible="false">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                Tipo de Conselho:
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select mid-size">
                                                    <asp:DropDownList ID="ddlTipoConselho" runat="server" RepeatDirection="Horizontal">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells3" id="trTipoProtecaoCadastral" runat="server" visible="false">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                Proteção Social:
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select full-size">
                                                    <asp:DropDownList ID="ddlTipoProtecaoCadastral" runat="server" OnSelectedIndexChanged="rblTipoProtecaoCadastral_SelectedIndexChanged"
                                                        RepeatDirection="Horizontal" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells3" id="trTipoServicoCadastral" runat="server" visible="false">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                <asp:Label ID="Label2" runat="server" Width="107px">Tipo de serviço:</asp:Label>
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select full-size">
                                                    <asp:DropDownList ID="ddlTipoServicoCadastral" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlTipoServicoCadastral_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells3" id="trServicoSubtificadoCadastral" runat="server" visible="false">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                <asp:Label ID="Label7" runat="server" Width="107px">Especifique :</asp:Label>
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select full-size">
                                                    <asp:DropDownList ID="ddlServicoSubtipificadoCadastral" runat="server"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlServicoSubtipificadoCadastral_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells3" id="trPublicoAlvoCadastral" runat="server" visible="false">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                Usuários:
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                <div class="input-control select full-size">
                                                    <asp:DropDownList ID="ddlPublicoAlvoCadastral" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells5" visible="false" runat="server" id="trTipoExecutoraCadastral">
                                            <div class="cell colspan2" align="right">
                                                Locais de execução disponíveis<br />
                                                <asp:ListBox runat="server" Rows="8" ID="lstTipoExecutorasDisponiveisCadastral" SelectionMode="Multiple"
                                                    Height="120px" Width="200px">
                                                    <asp:ListItem Value="3">Apenas CRAS</asp:ListItem>
                                                    <asp:ListItem Value="4">Apenas CREAS</asp:ListItem>
                                                    <asp:ListItem Value="5">Apenas Centro Pop</asp:ListItem>
                                                    <asp:ListItem Value="1">Outros locais da rede direta</asp:ListItem>
                                                    <asp:ListItem Value="2">Locais de execução da rede indireta</asp:ListItem>
                                                </asp:ListBox>
                                            </div>
                                            <div class="cell" align="center">
                                                <asp:Button runat="server" ID="btnIncluirItemTipoExecutoraCadastral" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnIncluirItemTipoExecutoraCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnIncluirListaTipoExecutoraCadastral" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnIncluirListaTipoExecutoraCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirItemTipoExecutoraCadastral" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnExcluirItemTipoExecutoraCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirListaTipoExecutoraCadastral" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnExcluirListaTipoExecutoraCadastral_Click" /><br />
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                Locais de execução selecionados<br />
                                                <asp:ListBox runat="server" Rows="8" ID="lstTipoExecutorasSelecionadasCadastral"
                                                    SelectionMode="Multiple" Height="120px" Width="200px"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="row cells3" visible="false" runat="server" id="trTipoExecutoraAbragenciaCadastral">
                                            <div class="cell" align="right" style="vertical-align: middle; padding: 10px;">
                                                <asp:Label ID="lblAbrangenciaRelatorio" runat="server" Width="170px">Abrangência :</asp:Label>
                                            </div>
                                            <div class="cell colspan2" align="left" colspan="2">
                                                <div class="input-control select mid-size">
                                                    <asp:DropDownList ID="ddlAbrangenciaRelatorioCadastral" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlAbrangenciaRelatorioCadastral_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">[Selecione uma opção:]</asp:ListItem>
                                                        <asp:ListItem Value="1">Estado</asp:ListItem>
                                                        <asp:ListItem Value="2">DRADS</asp:ListItem>
                                                        <asp:ListItem Value="3">Município</asp:ListItem>
                                                        <asp:ListItem Value="4">Macrorregião</asp:ListItem>
                                                        <asp:ListItem Value="5">Região Metropolitana</asp:ListItem>
                                                        <asp:ListItem Value="6">Porte do munícipio</asp:ListItem>
                                                        <asp:ListItem Value="7">Nível de Gestão do munícípio</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cells5" visible="false" runat="server" id="trDradsCadastral">
                                            <div class="cell colspan2" align="right" style="height: 141px">
                                                DRADS disponíveis<br />
                                                <asp:ListBox runat="server" Rows="10" ID="lstDradsDisponiveisCadastral" SelectionMode="Multiple"
                                                    Height="120px" Width="200px"></asp:ListBox>
                                            </div>
                                            <div class="cell" align="center" style="height: 141px">
                                                <asp:Button runat="server" ID="btnIncluirItemDradsCadastral" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnIncluirItemDradsCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnIncluirListaDradsCadastral" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnIncluirListaDradsCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirItemDradsCadastral" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnExcluirItemDradsCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirListaDradsCadastral" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnExcluirListaDradsCadastral_Click" /><br />
                                            </div>
                                            <div class="cell colspan2" align="left" style="height: 141px">
                                                DRADS selecionadas<br />
                                                <asp:ListBox runat="server" Rows="10" ID="lstDradsSelecionadasCadastral" SelectionMode="Multiple"
                                                    Height="120px" Width="150px"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="row cells5" visible="false" runat="server" id="trMunicipiosCadastral">
                                            <div class="cell colspan2" align="right">
                                                Municipios disponíveis<br />
                                                <asp:ListBox runat="server" Rows="10" ID="lstMunicipiosDisponiveisCadastral" SelectionMode="Multiple"
                                                    Height="120px" Width="200px"></asp:ListBox>
                                            </div>
                                            <div class="cell" align="center">
                                                <asp:Button runat="server" ID="btnIncluirMunicipioCadastral" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnIncluirMunicipioCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnIncluirListaMunicipioCadastral" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnIncluirListaMunicipioCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirMunicipioCadastral" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnExcluirMunicipioCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirListaMunicipioCadastral" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnExcluirListaMunicipioCadastral_Click" /><br />
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                Municipios selecionados<br />
                                                <asp:ListBox runat="server" Rows="10" ID="lstMunicipiosSelecionadosCadastral" SelectionMode="Multiple"
                                                    Height="120px" Width="200px"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="row cells5" visible="false" runat="server" id="trMacroRegiaoCadastral">
                                            <div class="cell colspan2" align="right">
                                                Macrorregiões disponiveis<br />
                                                <asp:ListBox runat="server" Rows="10" ID="lstMacroRegiaoDisponivelCadastral" SelectionMode="Multiple"
                                                    Height="120px" Width="200px"></asp:ListBox>
                                            </div>
                                            <div class="cell" align="center">
                                                <asp:Button runat="server" ID="btnIncluirItemMacroRegiaoCadastral" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnIncluirItemMacroRegiaoCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnIncluirListaMacroRegiaoCadastral" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnIncluirListaMacroRegiaoCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirItemMacroRegiaoCadastral" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnExcluirItemMacroRegiaoCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirListaMacroRegiaoCadastral" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnExcluirListaMacroRegiaoCadastral_Click" /><br />
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                Macrorregiões selecionadas<br>
                                                <asp:ListBox runat="server" Rows="10" ID="lstmacroRegiaoSelecionadaCadastral" SelectionMode="Multiple"
                                                    Height="120px" Width="200px"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="row cells5" visible="false" runat="server" id="trRegiaoMetropolitanaCadastral">
                                            <div class="cell colspan2" align="right">
                                                Regiões Metropolitanas disponiveis<br />
                                                <asp:ListBox runat="server" Rows="10" ID="lstRegiaoMetropolitanaDisponivelCadastral"
                                                    SelectionMode="Multiple" Height="120px" Width="200px"></asp:ListBox>
                                            </div>
                                            <div class="cell" align="center">
                                                <asp:Button runat="server" ID="btnIncluirItemRegiaoMetropolitanaCadastral" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;"
                                                    Width="29px" OnClick="btnIncluirItemRegiaoMetropolitanaCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnIncluirListaRegiaoMetropolitanaCadastral" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;"
                                                    Width="29px" OnClick="btnIncluirListaRegiaoMetropolitanaCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirItemRegiaoMetropolitanaCadastral" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;"
                                                    Width="29px" OnClick="btnExcluirItemRegiaoMetropolitanaCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirListaRegiaoMetropolitanaCadastral" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;"
                                                    Width="29px" OnClick="btnExcluirListaRegiaoMetropolitanaCadastral_Click" /><br />
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                Regiões Metropolitanas selecionadas<br />
                                                <asp:ListBox runat="server" Rows="10" ID="lstRegiaoMetropolitanaSelecionadaCadastral"
                                                    SelectionMode="Multiple" Height="120px" Width="200px"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="row cells5" visible="false" runat="server" id="trPorteMunicipioCadastral">
                                            <div class="cell colspan2" align="right">
                                                Porte do Município disponiveis<br />
                                                <asp:ListBox runat="server" Rows="10" ID="lstPorteDisponiveisCadastral" SelectionMode="Multiple"
                                                    Height="120px" Width="200px">
                                                    <asp:ListItem Value="1">Pequeno I</asp:ListItem>
                                                    <asp:ListItem Value="2">Pequeno II</asp:ListItem>
                                                    <asp:ListItem Value="3">Médio</asp:ListItem>
                                                    <asp:ListItem Value="4">Grande</asp:ListItem>
                                                    <asp:ListItem Value="5">Metropóle</asp:ListItem>
                                                </asp:ListBox>
                                            </div>
                                            <div class="cell" align="center">
                                                <asp:Button runat="server" ID="btnIncluirItemPorteMunicipioCadastral" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnIncluirItemPorteMunicipioCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnIncluirListaPorteMunicipioCadastral" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;"
                                                    Width="29px" OnClick="btnIncluirListaPorteMunicipioCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirItemPorteMunicipioCadastra" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnExcluirItemPorteMunicipioCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirListaPorteMunicipioCadastral" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;"
                                                    Width="29px" OnClick="btnExcluirListaPorteMunicipioCadastral_Click" /><br />
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                Porte do Município selecionados<br />
                                                <asp:ListBox runat="server" Rows="10" ID="lstPorteSelecionadosCadastral" SelectionMode="Multiple"
                                                    Height="120px" Width="200px"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="row cells5" visible="false" runat="server" id="trNivelGestaoCadastral">
                                            <div class="cell colspan2" align="right">
                                                Nível de Gestão disponiveis<br />
                                                <asp:ListBox runat="server" Rows="10" ID="lstNivelGestaoDisponiveisCadastral" SelectionMode="Multiple"
                                                    Height="120px" Width="200px">
                                                    <asp:ListItem Value="1" Selected="True">Inicial</asp:ListItem>
                                                    <asp:ListItem Value="0">Básica</asp:ListItem>
                                                    <asp:ListItem Value="2">Plena</asp:ListItem>
                                                    <asp:ListItem Value="3">Não habilitado</asp:ListItem>
                                                </asp:ListBox>
                                            </div>
                                            <div class="cell" align="center">
                                                <asp:Button runat="server" ID="btnIncluirItemNivelGestaoCadastral" Style="background-image: url('Styles/Icones/arrow.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnIncluirItemNivelGestaoCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnIncluirListaNivelGestaoCadastral" Style="background-image: url('Styles/Icones/forward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnIncluirListaNivelGestaoCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirItemNivelGestaoCadastral" Style="background-image: url('Styles/Icones/arrow-left.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnExcluirItemNivelGestaoCadastral_Click" /><br />
                                                <asp:Button runat="server" ID="btnExcluirListaNivelGestaoCadastral" Style="background-image: url('Styles/Icones/backward.png'); background-repeat: no-repeat; background-position: center;" Width="29px"
                                                    OnClick="btnExcluirListaNivelGestaoCadastral_Click" /><br />
                                            </div>
                                            <div class="cell colspan2" align="left">
                                                Nível de Gestão selecionados<br />
                                                <div class="input-control select full-size">
                                                    <asp:ListBox runat="server" Rows="10" ID="lstNivelGestaoSelecionadosCadastral" SelectionMode="Multiple"
                                                        Height="120px" Width="200px"></asp:ListBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell" align="center">
                                                <asp:Button ID="btnRelatorioCadastral" runat="server" Text="Gerar Relatório" CssClass="Botao"
                                                    Width="123px" OnClick="btnRelatorioCadastral_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
