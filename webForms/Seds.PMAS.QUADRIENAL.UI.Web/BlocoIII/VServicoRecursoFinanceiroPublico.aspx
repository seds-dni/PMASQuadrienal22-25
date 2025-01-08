<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VServicoRecursoFinanceiroPublico.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoIII.VServicoRecursoFinanceiroPublico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmUnidadesPublicas">
                <div class="accordion" data-role="accordion" data-no-close="true">
                    <div class="frame active">
                        <div class="heading">
                            3.4.b - Informações sobre este serviço
                            <span class="mif-organization icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Unidades Públicas">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <b>Tipo de proteção social:</b><br />
                                            <asp:Label ID="lblTipoProtecao" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row cells2">
                                        <div class="cell">
                                            <b>Tipo de serviço:</b><br />
                                            <asp:Label ID="lblTipoServico" runat="server" />
                                            <div class="row" id="tbNaoTipificado" runat="server" visible="false">
                                                <div class="cell">
                                                    <div class="row">
                                                        <div class="cell">
                                                            <b>Especificação do tipo de serviço :</b><br />
                                                            <asp:Label ID="lblNaoTipificado" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="cell">
                                                            <b>Objetivo do tipo de serviço :</b><br />
                                                            <asp:Label ID="lblObjetivoNaoTipificado" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="cell">
                                            <b>Usuários:</b><br />
                                            <asp:Label ID="lblPublicoAlvo" runat="server" />
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="cell">
                                            <b>Abrangência do Serviço:</b><br />
                                            <asp:Label ID="lblAbrangencia" runat="server" />
                                        </div>
                                    </div>
                                    <div runat="server" id="trAbrangencia" class="row" visible="false">
                                        <div class="cell">
                                            <b>O municipio é Sede do Servico ?</b><br />
                                            <asp:Label ID="lblSedeServico" runat="server"></asp:Label><br /><br />
                                        </div>
                                    </div>
                                    <div runat="server" id="trOfertaOuSede" class="row" visible="false">
                                        <div class="cell">
                                            <b><asp:Label ID="lblOfertaOuSede" runat="server" ></asp:Label></b><br />
                                            <asp:TextBox  ID="txtOfertaOuSede" runat="server" Enabled="false" TextMode="MultiLine" style="width:50%;"></asp:TextBox><br /><br />
                                        </div>
                                    </div>
                                    <div id="trTituloFormaJuridica" runat="server" class="row" visible="false">
                                        <div class="cell">
                                            <b>Qual a forma jurídica que regulamenta a oferta Regional do Serviço ?</b><br />
                                            <asp:Label ID="lblJuridica" runat="server" ></asp:Label><br />
                                        </div>
                                    </div>
                                    <div runat="server" id="trJuridica" visible="false" class="row">
                                        <div class="cell">
                                            <b>Dados do Consórcio</b><br />
                                            <asp:Label ID="lblNomeDoConsorcio" runat="server" ></asp:Label><br />
                                            <asp:Label ID="lblCNPJ" runat="server" ></asp:Label><br />
                                            <asp:Label ID="lblMunicipioSede" runat="server" ></asp:Label><br /><br />
                                            <b>Municípios envolvidos : </b><br />
                                            <asp:textbox ID="txtMunicipiosEnvolvidos" runat="server" textmode="MultiLine" Enabled="false" style="width:50%;"></asp:textbox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell" colspan="2">
                                            <b>Nome do técnico responsável pelo serviço:</b><br />
                                            <asp:Label ID="lblNomeTecnicoResponsavel" runat="server" />
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="cell">
                                            <b>Este serviço atende exclusiva ou prioritariamente usuários que pertencem a alguma das comunidades tradicionais ou grupos específicos listados abaixo?</b><br />
                                            <asp:Label ID="lblCaracteristicasTerritorio" runat="server" />
                                        </div>
                                    </div>

                                    <div class="row" runat="server" id="tdServicoOfertado">
                                        <div class="cell">
                                            <b>Este serviço é ofertado em:</b><br />
                                            <asp:Label ID="lblOfertaServico" runat="server" />
                                        </div>
                                    </div>

                                    <div class="row">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Caracterização dos Usuários:</b></legend>
                                            <div class="row cells2">
                                                <div class="cell">
                                                    <b>Sexo:</b><br />
                                                    <asp:Label ID="lblSexo" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    <b>Região de moradia dos usuários:</b><br />
                                                    <asp:Label ID="lblMoradiaUsuarios" runat="server" />
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="cell">
                                                    <b>Principais situações de vulnerabilidade identificadas dentre os usuários que são atendidos por este serviço:</b><br />
                                                    <asp:Label ID="lblSituacoesEspecificas" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class ="cell" runat="server" id="trCriancasAuxilioReclusao2024" visible="false">
                                                    <b>Exercicio 2024</b><br /><br />
                                                    <b>O serviço atende crianças aptas para receber o auxílio-reclusão previsto no art. 80 da Lei nº 8.213/1991?</b><br />
                                                    <asp:Label ID="lblCriancasAuxilioReclusao2024" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class ="cell" runat="server" id="trAuxilioReclusaoRequerimentosFeitos2024" visible="false">
                                                    <b>Quantos requerimentos foram feitos em 2024 ?</b><br />
                                                    <asp:Label ID="lblAuxilioReclusaoRequerimentosFeitos2024" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class ="cell" runat="server" id="trAuxilioReclusaoRequerimentosAprovados2024" visible="false">
                                                    <b>Quantos requerimentos foram aprovados ?</b><br />
                                                    <asp:Label ID="lblAuxilioReclusaoRequerimentosAprovados2024" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class ="cell" runat="server" id="trAuxilioReclusaoRequerimentosNegados2024" visible="false">
                                                    <b>Quantos requerimentos foram negados ?</b><br />
                                                    <asp:Label ID="lblAuxilioReclusaoRequerimentosNegados2024" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            
                                            <div class="row">
                                                <div class ="cell" runat="server" id="trCriancasPensaoMorte2024" visible="false">
                                                    <b>O serviço atende crianças aptas para receber pensão por morte regulamentada pela Lei nº 8.213/1991, nos artigos 74 e 79?</b><br />
                                                    <asp:Label ID="lblCriancasPensaoMorte2024" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class ="cell" runat="server" id="trPensaoMorteRequerimentosFeitos2024" visible="false">
                                                    <b>Quantos requerimentos foram feitos em 2024 ?</b><br />
                                                    <asp:Label ID="lblPensaoMorteRequerimentosFeitos2024" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class ="cell" runat="server" id="trPensaoMorteRequerimentosAprovados2024" visible="false">
                                                    <b>Quantos requerimentos foram aprovados ?</b><br />
                                                    <asp:Label ID="lblPensaoMorteRequerimentosAprovados2024" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class ="cell" runat="server" id="trPensaoMorteRequerimentosNegados2024" visible="false">
                                                    <b>Quantos requerimentos foram negados ?</b><br />
                                                    <asp:Label ID="lblPensaoMorteRequerimentosNegados2024" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class ="cell" runat="server" id="trCriancasAuxilioReclusao2025" visible="false">
                                                    <b>Exercicio 2025</b><br /><br />
                                                    <b>O serviço atende crianças aptas para receber o auxílio-reclusão previsto no art. 80 da Lei nº 8.213/1991?</b><br />
                                                    <asp:Label ID="lblCriancasAuxilioReclusao2025" runat="server"></asp:Label>
                                                </div>
                                             </div>
                                            <div class="row">
                                                <div class ="cell" runat="server" id="trAuxilioReclusaoRequerimentosFeitos2025" visible="false">
                                                    <b>Quantos requerimentos foram feitos em 2025 ?</b><br />
                                                    <asp:Label ID="lblAuxilioReclusaoRequerimentosFeitos2025" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class ="cell" runat="server" id="trAuxilioReclusaoRequerimentosAprovados2025" visible="false">
                                                    <b>Quantos requerimentos foram aprovados ?</b><br />
                                                    <asp:Label ID="lblAuxilioReclusaoRequerimentosAprovados2025" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class ="cell" runat="server" id="trAuxilioReclusaoRequerimentosNegados2025" visible="false">
                                                    <b>Quantos requerimentos foram negados ?</b><br />
                                                    <asp:Label ID="lblAuxilioReclusaoRequerimentosNegados2025" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                           
											
                                            <div class="row">
                                                <div class ="cell" runat="server" id="trCriancasPensaoMorte2025" visible="false">
                                                    <b>O serviço atende crianças aptas para receber pensão por morte regulamentada pela Lei nº 8.213/1991, nos artigos 74 e 79?</b><br />
                                                    <asp:Label ID="lblCriancasPensaoMorte2025" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class ="cell" runat="server" id="trPensaoMorteRequerimentosFeitos2025" visible="false">
                                                    <b>Quantos requerimentos foram feitos em 2025 ?</b><br />
                                                    <asp:Label ID="lblPensaoMorteRequerimentosFeitos2025" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class ="cell" runat="server" id="trPensaoMorteRequerimentosAprovados2025" visible="false">
                                                    <b>Quantos requerimentos foram aprovados ?</b><br />
                                                    <asp:Label ID="lblPensaoMorteRequerimentosAprovados2025" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class ="cell" runat="server" id="trPensaoMorteRequerimentosNegados2025" visible="false">
                                                    <b>Quantos requerimentos foram negados ?</b><br />
                                                    <asp:Label ID="lblPensaoMorteRequerimentosNegados2025" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </div>
                                    <div class="row">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Recursos Humanos:</b></legend>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Número de trabalhadores, segundo a escolaridade:</b>
                                                </div>
                                            </div>
                                            <div class="row cells5">
                                                <div class="cell">
                                                    Sem escolarização:<br />
                                                    <asp:Label ID="lblSemEscolaridade" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    Nível fundamental:<br />
                                                    <asp:Label ID="lblFundamental" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    Nível médio:<br />
                                                    <asp:Label ID="lblMedio" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    Nível superior:<br />
                                                    <asp:Label ID="lblSuperior" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    Total:<br />
                                                    <asp:Label ID="lblTotal" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Área de formação dos trabalhadores que possuem nível superior:</b>
                                                </div>
                                            </div>
                                            <div class="row cells5">
                                                <div class="cell">
                                                    Serviço Social:
                                                    <br />
                                                    <asp:Label ID="lblSuperiorServicoSocial" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    Psicologia:
                                                    <br />
                                                    <asp:Label ID="lblSuperiorPsicologia" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    Pedagogia:<br />
                                                    <asp:Label ID="lblSuperiorPedagogia" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    Sociologia:<br />
                                                    <asp:Label ID="lblSociologia" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    Terapia Ocupacional:<br />
                                                    <asp:Label ID="lblSuperiorTerapiaOcupacional" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row cells5">
                                                <div class="cell">
                                                    Direito:
                                                    <br />
                                                    <asp:Label ID="lblDireito" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    Antropologia:
                                                    <br />
                                                    <asp:Label ID="lblSuperiorAntropologia" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    Economia:<br />
                                                    <asp:Label ID="lblSuperiorEconomia" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    Musicoterapia:<br />
                                                    <asp:Label ID="lblSuperiorMusicoTerapia" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    Economia Doméstica:<br />
                                                    <asp:Label ID="lblSuperiorEconomiaDomestica" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Com relação a este serviço, indique o número de:</b>
                                                </div>
                                            </div>
                                            <div class="row cells3">
                                                <div class="cell">
                                                    Estagiários<br />
                                                    <asp:Label ID="lblEstagiarios" runat="server" />
                                                </div>
                                                <div class="cell colspan2">
                                                    Voluntários<br />
                                                    <asp:Label ID="lblVoluntarios" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Número de trabalhadores deste serviço que:</b>
                                                </div>
                                            </div>
                                            <div class="row cells3">
                                                <div class="cell">
                                                    Trabalham exclusivamente neste serviço<br />
                                                    <asp:Label ID="lblExclusivoServico" runat="server" />
                                                </div>
                                                <div class="cell colspan2">
                                                    Trabalham também em outros serviços socioassistenciais ou no órgão gestor do município<br />
                                                    <asp:Label ID="lblOutroServicos" runat="server" />
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="row">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Funcionamento:</b></legend>
                                            <div class="row cells2">
                                                <div class="cell">
                                                    <b>Data de início de funcionamento deste serviço:</b><br />
                                                    <asp:Label ID="lblDataInicio" runat="server" />
                                                </div>
                                                <div id="tdEncerramento" class="cell" visible="false" runat="server">
                                                    <b>
                                                        <asp:Label ID="lblDataEncerramentoServico" runat="server" Text="Data de encerramento deste serviço:"></asp:Label></b><br />
                                                    <asp:Label ID="lblDataEncerramento" runat="server" />
                                                </div>
                                            </div>
                                            <div id="trMotivoEncerramento" class="row" runat="server" visible="false">
                                                <div class="cell">
                                                    <b>Motivo da desativação deste serviço:</b><br />
                                                    <asp:Label ID="lblMotivoEncerramento" runat="server" />
                                                </div>
                                            </div>
                                            <div id="trDetalhamentoDesativacao" class="row" runat="server" visible="false">
                                                <div class="cell">
                                                    <b>
                                                        <asp:Label ID="lblDescricaoDetalhamento" runat="server"></asp:Label></b><br />
                                                    <asp:Label ID="lblDetalhamento" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <%-----------------------------------------------------------------------------------------------------%>
                                            <%------------------------------------- FUNCIONAMENTO -------------------------------------------------%>
                                            <%-----------------------------------------------------------------------------------------------------%>
                                            <div id="layout_capacidade_media_mensal" runat="server">
                                                <div class="row cells2">
                                                    <%------------------------------------- Capacidade -------------------------------------------------%>
                                                    <div class="cell">
                                                        <b>Capacidade mensal de atendimento deste serviço:</b><br />
                                                        <p>
                                                            <b>2022: </b>
                                                            <asp:Label ID="lblCapacidadeExercicio1" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 
                                                        <b>2023: </b>
                                                            <asp:Label ID="lblCapacidadeExercicio2" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 
                                                        <b>2024: </b>
                                                            <asp:Label ID="lblCapacidadeExercicio3" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 
                                                        <b>2025: </b>
                                                            <asp:Label ID="lblCapacidadeExercicio4" runat="server" />
                                                        </p>
                                                    </div>
                                                    <%------------------------------------- MM -------------------------------------------------%>
                                                    <div class="cell">
                                                        <b>Média mensal de
                                                            <asp:Label ID="lblPAIF2" runat="server" Text="pessoas" />
                                                            atendidas:</b><br />
                                                        <p>
                                                            <b>2021: </b>
                                                            <asp:Label ID="lblMediaMensalExercicio1" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 
                                                        <b>2022: </b>
                                                            <asp:Label ID="lblMediaMensalExercicio2" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 
                                                        <b>2023: </b>
                                                            <asp:Label ID="lblMediaMensalExercicio3" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 
                                                        <b>2024: </b>
                                                            <asp:Label ID="lblMediaMensalExercicio4" runat="server" />
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="layout_capacidade_media_mensal_la_psc" runat="server">
                                                <div class="row cells2">
                                                    <%----------------------------------------CAPA LA----------------------------------------------%>
                                                    <div class="cell">
                                                        <b>Capacidade mensal de atendimento deste serviço:</b><br />
                                                        <p>
                                                            <b>LA: 2022: </b>
                                                            <asp:Label ID="lblCapacidadeLAExercicio1" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 
                                                    <b>2023: </b>
                                                            <asp:Label ID="lblCapacidadeLAExercicio2" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 
                                                    <b>2024: </b>
                                                            <asp:Label ID="lblCapacidadeLAExercicio3" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 
                                                    <b>2025: </b>
                                                            <asp:Label ID="lblCapacidadeLAExercicio4" runat="server" />
                                                        </p>
                                                        <%----------------------------------------CAPA PSC----------------------------------------------%>
                                                        <p>
                                                            <b>PSC: 2022: </b>
                                                            <asp:Label ID="lblCapacidadePSCExercicio1" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 
                                                    <b>2023: </b>
                                                            <asp:Label ID="lblCapacidadePSCExercicio2" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 
                                                    <b>2024: </b>
                                                            <asp:Label ID="lblCapacidadePSCExercicio3" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 
                                                    <b>2025: </b>
                                                            <asp:Label ID="lblCapacidadePSCExercicio4" runat="server" />
                                                        </p>
                                                    </div>
                                                    <%----------------------------------------MM LA----------------------------------------------%>
                                                    <div class="cell">
                                                        <b>Média mensal de
                                                        <asp:Label ID="Label3" runat="server" Text="pessoas" />
                                                            atendidas:</b><br />
                                                    <p><b>LA: 2021: </b>
                                                            <asp:Label ID="lblMediaMensalLAExercicio1" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 
                                                    <b>2022: </b>
                                                            <asp:Label ID="lblMediaMensalLAExercicio2" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 
                                                    <b>2023: </b>
                                                            <asp:Label ID="lblMediaMensalLAExercicio3" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 
                                                    <b>2024: </b>
                                                            <asp:Label ID="lblMediaMensalLAExercicio4" runat="server" />
                                                        </p>
                                                        <%----------------------------------------MM PSC----------------------------------------------%>
                                                        <p>
                                                            <b>PSC: 2021: </b>
                                                            <asp:Label ID="lblMediaMensalPSCExercicio1" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 

                                                            <b>2022: </b>
                                                            <asp:Label ID="lblMediaMensalPSCExercicio2" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 

                                                            <b>2023: </b>
                                                            <asp:Label ID="lblMediaMensalPSCExercicio3" runat="server" />
                                                            &nbsp; &nbsp; | &nbsp; &nbsp; 

                                                            <b>2024: </b>
                                                            <asp:Label ID="lblMediaMensalPSCExercicio4" runat="server" />
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="row cells2">
                                                <div class="cell">
                                                    <b>Este serviço funciona quantas horas por semana?</b><br />
                                                    <asp:Label ID="lblHorasSemana" runat="server" />
                                                </div>
                                                <div class="cell">
                                                    <b>Este serviço funciona em quantos dias por semana?</b><br />
                                                    <asp:Label ID="lblDiasSemana" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Trabalho realizado por este serviço:</b><br />
                                                    <asp:Label ID="lblAtividades" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <b>Segundo a avaliação do órgão gestor municipal, este serviço:</b><br />
                                                    <asp:Label ID="lblAvaliacaoGestor" runat="server" />
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>

                                    <div class="row" id="trRecursosFinanceiros" runat="server" visible="false">
                                        <div class="cell">
                                            <fieldset class="border-blue">
                                                <legend class="lgnd"><b class="titulo fg-blue">Recursos Financeiros</b></legend>


                                                <div class="frame" id="frame1_5">
                                                    <asp:HiddenField ID="hdfAno" runat="server" />
                                                    <asp:HiddenField ID="hdfExercicio" runat="server" />
                                                    <div id="recursoFinanceiroExercicioAno2" class="tabcontrol" data-role="tabcontrol">
                                                        <ul class="tabs">
                                                            <asp:Repeater runat="server" ID="rptFundosAbas">
                                                                <ItemTemplate>
                                                                    <li id="Li1" runat="server" onclick='$("#MainContent_hdnExercicio").val("2022")'>
                                                                        <a href="#frame1_5_<%#DataBinder.Eval(Container.DataItem, "Exercicio")%>">
                                                                            <%#DataBinder.Eval(Container.DataItem, "Exercicio") %></a></li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ul>
                                                        <div class="frames">
                                                            <asp:Repeater runat="server" ID="rptFundosConteudo">
                                                                <ItemTemplate>
                                                                    <div class="frame" id='frame1_5_<%#DataBinder.Eval(Container.DataItem, "Exercicio")%>'>
                                                                        <fieldset class="border-blue">
                                                                            <legend class="lgnd"><b class="fg-blue">Recursos Financeiros</b></legend>
                                                                            <div class="row">
                                                                                <fieldset class="border-blue">
                                                                                    <legend class="lgnd"><b class="fg-blue">Fundos Municipais</b></legend>
                                                                                    <div class="row cells3">
                                                                                        <div class="cell">
                                                                                            <b>Assistência Social:</b><br />
                                                                                            <div class="input-control text">
                                                                                                <asp:Label ID="txtFMAS" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ValorMunicipalAssistencia", "{0:0.00}")%>' Style="text-align: right;" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="cell">
                                                                                            <b>Direitos da Criança e do Adolescente:</b><br />
                                                                                            <div class="input-control text">
                                                                                                <asp:Label ID="txtFMDCA" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ValorMunicipalFMDCA", "{0:0.00}")%>' Style="text-align: right;" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="cell">
                                                                                            <b>Idoso:</b>
                                                                                            <br />
                                                                                            <div class="input-control text">
                                                                                                <asp:Label ID="txtFMI" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ValorMunicipalFMI", "{0:0.00}")%>' Style="text-align: right;" />
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
                                                                                                <asp:Label ID="txtFEAS" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ValorEstadualAssistencia", "{0:0.00}")%>' Style="text-align: right;" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="cell" id="trFeasAnterior" runat="server">
                                                                                            <b>FEAS - Reprogramação Ano Anterior:</b><br />
                                                                                            <div class="input-control text">
                                                                                                <asp:Label ID="txtFEASAnoAnterior" runat="server" Enabled="false" Text='<%#DataBinder.Eval(Container.DataItem, "ValorEstadualAssistenciaAnoAnterior", "{0:0.00}")%>' Style="text-align: right;" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="cell">
                                                                                            <b>Direitos da Criança e do Adolescente:</b><br />
                                                                                            <div class="input-control text">
                                                                                                <asp:Label ID="txtFEDCA" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ValorEstadualFEDCA", "{0:0.00}")%>' Style="text-align: right;" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="row cells3">
                                                                                        <div class="cell">
                                                                                            <b>Idoso:</b><br />
                                                                                            <div class="input-control text">
                                                                                                <asp:Label ID="txtFEI" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ValorEstadualFEI", "{0:0.00}")%>' Style="text-align: right;" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="cell">
                                                                                            <b>FEAS - Demandas Parlamentares</b><br />
                                                                                            <div class="input-control text">
                                                                                                <asp:Label ID="txtFeasDemandasParlamentares" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ValorEstadualDemandasParlamentares", "{0:0.00}")%>' Style="text-align: right;" ></asp:Label>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="cell">
                                                                                            <b>FEAS - Reprogramacao Demandas Parlamentares</b><br />
                                                                                            <div class="input-control text">
                                                                                                <asp:Label ID="txtFeasReprogramacaoDemandasParlamentares" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ValorEstadualDemandasParlamentaresReprogramacao", "{0:0.00}")%>' Style="text-align: right;" ></asp:Label>
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
                                                                                                <asp:Label ID="txtFNAS" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ValorFederalAssistencia", "{0:0.00}")%>' Style="text-align: right;" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="cell">
                                                                                            <b>Direitos da Criança e do Adolescente:</b>
                                                                                            <br />
                                                                                            <div class="input-control text">
                                                                                                <asp:Label ID="txtFNDCA" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ValorFederalFNDCA", "{0:0.00}")%>' Style="text-align: right;" />
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="cell">
                                                                                            <b>Idoso:</b><br />
                                                                                            <div class="input-control text">
                                                                                                <asp:Label ID="txtFNI" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ValorFederalFNI", "{0:0.00}")%>' Style="text-align: right;" />
                                                                                            </div>
                                                                                        </div>

                                                                                    </div>
                                                                                </fieldset>
                                                                            </div>
                                                                            <div id="trRecursosFinanceirosPublicos" runat="server" visible="true">
                                                                                <fieldset class="border-blue">
                                                                                    <legend class="lgnd"><b class="fg-blue">Outras Fontes Financeiras</b></legend>
                                                                                    <div class="row mid-size" id="tdlstRecursosAdicionados" runat="server" visible="true">
                                                                                        <asp:ListView ID="lstRecursosAdicionados" runat="server" DataKeyNames="Id" DataSource='<%# DataBinder.Eval(Container.DataItem, "ServicoRecursoFinanceiroPublicoFontesRecursosInfo") %>'>
                                                                                            <LayoutTemplate>
                                                                                                <table border="0" cellpadding="0" cellspacing="0" class="table striped border bordered">
                                                                                                    <thead class="info">
                                                                                                        <tr>
                                                                                                            <th width="250px">Fonte de Recurso</th>
                                                                                                            <th width="100px">Valor do Recurso</th>
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
                                                                                                    <td align="center"><%#DataBinder.Eval(Container.DataItem, "NomeFonteRecurso", "{0:0.00}") %></td>
                                                                                                    <td align="center"><%#DataBinder.Eval(Container.DataItem, "ValorFonteRecurso", "{0:0.00}") %></td>
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
                                                                            <fieldset class="border-blue">
                                                                                <legend class="lgnd"><b class="fg-blue">Demandas Parlamentares</b></legend>
                                                                                <div class="row">
                                                                                       <div class="row cells2">
                                                                                           <div class="cell">
                                                                                               <b>Código / Número da Demanda Parlamentar:</b><br />
                                                                                               <div class="input-control text">
                                                                                                   <asp:Label ID="txtCodigoDemandaExercicio1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CodigoDemandaParlamentar", "{0:0.00}")%>' Style="text-align: right;" />
                                                                                               </div>
                                                                                           </div>
                                                                                           <div class="cell">
                                                                                               <b>Objeto da Demanda Parlamentar:</b><br />
                                                                                               <div class="input-control text">
                                                                                                   <asp:Label ID="txtObjetoDemandaExercicio1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ObjetoDemandaParlamentar", "{0:0.00}")%>' Style="text-align: right;" />
                                                                                               </div>
                                                                                           </div>
                                                                                       </div>
                                                                                       <div class="row cells2">
                                                                                           <div class="cell">
                                                                                               <b>Contrapartida Municipal:</b><br />
                                                                                               <asp:Label ID="txtContrapartida" runat="server" Text='<%#(((Boolean)DataBinder.Eval(Container.DataItem, "ContrapartidaMunicipal") == true) ? "Sim" : "Não") %>' Style="text-align: right;" /> 
                                                                                           </div>
                                                                                           <div id="trValorContraExercicio1" class="cell" runat="server">
                                                                                               <b>Valor contrapartida:</b><br />
                                                                                               <div class="input-control text">
                                                                                                   <asp:Label ID="txtValorContraExercicio1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ValorContrapartidaMunicipal", "{0:0.00}")%>' Style="text-align: right;" /> 
                                                                                               </div>
                                                                                           </div>
                                                                                       </div>
                                                                                </div>
                                                                            </fieldset>


                                                                        </fieldset>
                                                                    </div>

                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div>

                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>



                                <div class="row" id="trServicosAssociados" runat="server">
                                    <div class="cell">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="titulo fg-blue">Integração com programas, projetos e benefícios:</b></legend>
                                            <fieldset id="fldProgramas" runat="server" class="border-blue">
                                                <legend class="lgnd fg-blue">Programas</legend>
                                                <div class="row">
                                                    <div class="cell" style="font-weight: bold">
                                                        <asp:Label ID="lblServicosProgramas" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </fieldset>
                                            <fieldset id="fldTransferenciaRenda" runat="server" class="border-blue">
                                                <legend class="lgnd fg-blue">Transferência de Renda</legend>
                                                <div class="row">
                                                    <div class="cell" style="font-weight: bold">
                                                        <asp:Label ID="lblServicosTransferenciaRenda" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </fieldset>
                                            <fieldset id="fldBeneficiosEventuais" runat="server" class="border-blue">
                                                <legend class="lgnd fg-blue">Benefícios Eventuais</legend>
                                                <div class="row">
                                                    <div class="cell" style="font-weight: bold">
                                                        <asp:Label ID="lblServicosBeneficiosEventuais" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </fieldset>
                                            <fieldset id="fldBeneficiosContinuados" runat="server" class="border-blue">
                                                <legend class="lgnd fg-blue">Benefícios Continuados</legend>
                                                <div class="row">
                                                    <div class="cell" style="font-weight: bold">
                                                        <asp:Label ID="lblServicosBeneficiosContinuados" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                         </fieldset>
                               

                                        </fieldset>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="cell" align="center">
                                        <asp:Button ID="btnVoltar" runat="server" Text="Voltar"
                                            OnClientClick="javascript:history.go(-1);return false;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </form>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
