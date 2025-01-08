<%@ Page Title="" Language="C#" MasterPageFile="~/Relatorios/Relatorio.Master" AutoEventWireup="true" CodeBehind="RDiagnosticoSocioterritorial.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.Relatorios.RDiagnosticoSocioterritorial" %>


<%@ MasterType VirtualPath="~/Relatorios/Relatorio.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <asp:Label ID="lblAtualizacaoAnual" runat="server"></asp:Label>
    <table id="tbReport" runat="server" width="100%">

        <tr>
            <td>
                <fieldset class="border-blue">
                    <legend class="lgnd fg-blue">A. Indicadores</legend>
                    <table id="tbAnaliseDiagnostica" runat="server" class="table border bordered" cellspacing="0"
                        cellpadding="0" border="0">
                        <thead>
                            <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                                <th align="center" width="350" rowspan="2">Indicador
                                </th>
                                <th align="center" width="70" rowspan="2">Unidade</th>
                                <th align="center" width="100" rowspan="2">Referência
                                               <br />
                                    (Ano)
                                </th>
                                <th align="center" width="220" colspan="3">Valores
                                </th>
                                <th align="center" width="70" rowspan="2">Fonte
                                </th>
                            </tr>
                            <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                                <td align="center" width="100">Município
                                </td>
                                <td align="center" width="100">DRADS
                                </td>
                                <td align="center" width="100">Estado
                                </td>
                            </tr>
                        </thead>
                        <tr style="height: 22px;">
                            <td align="left">
                                <asp:Label ID="Label21" Text="Área territorial" runat="server"></asp:Label>
                            </td>
                            <td align="center">Km²
                            </td>
                            <td align="center">2017
                            </td>
                            <td align="right">
                                <asp:Label ID="lblAreaTerritorial" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblAreaTerritorialDRADS" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblAreaTerritorialEstado" runat="server">248.223,21</asp:Label>
                            </td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink1" NavigateUrl="http://www.censo2010.ibge.gov.br" Target="_blank"
                                    runat="server" Text="IBGE"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="height: 22px;">
                            <td align="left">
                                <asp:Label ID="Label20" Text="Estimativa do Número de habitantes" runat="server"></asp:Label>
                            </td>
                            <td align="center">Pessoas
                            </td>
                            <td align="center">2017
                            </td>
                            <td align="right">
                                <asp:Label ID="lblNumeroHabitantes" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblNumeroHabitantesDRADS" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblTotalNumeroHabitantesEstado" runat="server">41.223.683</asp:Label>
                            </td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink2" NavigateUrl="http://www.seade.gov.br" Target="_blank"
                                    runat="server" Text="SEADE"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="height: 22px;">
                            <td align="left">Densidade demográfica (estimativa)
                            </td>
                            <td align="center">Hab./km²</td>
                            <td align="center">2017
                            </td>
                            <td align="right">
                                <asp:Label ID="lblDensidade" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblDensidadeDRADS" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblDensidadeEstado" runat="server">171,92</asp:Label>
                            </td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink11" NavigateUrl="http://www.seade.gov.br" Target="_blank"
                                    runat="server" Text="SEADE"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="height: 22px;">
                            <td align="left">
                                <asp:Label ID="Label1" Text="Taxa geométrica de crescimento anual da população" runat="server"></asp:Label>
                            </td>
                            <td align="center">%
                            </td>
                            <td align="center">2010-2017
                            </td>
                            <td align="right">
                                <asp:Label ID="lblTaxaGeometrica" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblTaxaGeometricaDRADS" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblTaxaGeometricaEstado" runat="server">0,82</asp:Label>
                            </td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink5" NavigateUrl="http://www.seade.gov.br" Target="_blank"
                                    runat="server" Text="SEADE"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="height: 22px;">
                            <td align="left">Grau de urbanização
                            </td>
                            <td align="center">%
                            </td>
                            <td align="center">2017
                            </td>
                            <td align="right">
                                <asp:Label ID="lblGrauUrbanizacao" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblGrauUrbanizacaoDRADS" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblGrauUrbanizacaoEstado" runat="server">96,21</asp:Label>
                            </td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink7" NavigateUrl="http://www.seade.gov.br" Target="_blank"
                                    runat="server" Text="SEADE"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="height: 22px;">
                            <td align="left">Domicílios particulares permanentes (estimativa)
                            </td>
                            <td align="center">Domicílios
                            </td>
                            <td align="center">2017
                            </td>
                            <td align="right">
                                <asp:Label ID="lblDomicilios" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblDomiciliosDRADS" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblDomiciliosEstado" runat="server">12.827.153</asp:Label>
                            </td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink10" NavigateUrl="http://www.seade.gov.br/" Target="_blank"
                                    runat="server" Text="IBGE"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="height: 22px;">
                            <td align="left">Numero de pessoas por domicílio (estimativa)
                            </td>
                            <td align="center">Domicílios
                            </td>
                            <td align="center">2017
                            </td>
                            <td align="right">
                                <asp:Label ID="lblPessoasDomiciliosMunicipio" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblPessoasDomiciliosDrads" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblPessoasDomiciliosEstado" runat="server">3,0</asp:Label>
                            </td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink3" NavigateUrl="http://www.seade.gov.br/" Target="_blank"
                                    runat="server" Text="SEADE"></asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table class="table border bordered" id="tbPopulacaoVulnerabilidade" runat="server" ellspacing="0"
                        cellpadding="0" border="0">
                        <thead>
                            <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                                <th align="center" rowspan="2" width="300">Índices e Indicadores
                                </th>
                                <th align="center" width="60" rowspan="2">Unidade
                                </th>
                                <th align="center" width="70" rowspan="2">Referência<br />
                                    (ano)
                                </th>
                                <th align="center" colspan="3">Valores
                                </th>
                                <th align="center" width="60" rowspan="2">Fonte
                                </th>
                            </tr>
                            <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                                <th width="60" align="center">Municipio
                                </th>
                                <th width="60" align="center">DRADS
                                </th>
                                <th align="center" width="60">Estado
                                </th>
                            </tr>
                        </thead>
                        <tr style="height: 22px;">
                            <td align="left" rowspan="2">População com menos de 15 anos (estimativa)</td>
                            <td align="center">Pessoas
                            </td>
                            <td align="center" rowspan="2">2017
                            </td>
                            <td align="right" height="25">
                                <asp:Label ID="lblNumPessoasAbaixoQuinzeAnosMunicipio" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblNumPessoasAbaixoQuinzeAnosDRADS" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblNumPessoasAbaixoQuinzeAnosEstado" Text="8.443.792" runat="server"></asp:Label>
                            </td>

                            <td align="center" rowspan="2">
                                <asp:HyperLink ID="HyperLink12" NavigateUrl="http://www.seade.gov.br" Target="_blank"
                                    runat="server" Text="SEADE"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">%
                            </td>
                            <td align="right" height="25">
                                <asp:Label ID="lblPercPessoasAbaixoQuinzeAnosMunicipio" runat="server"></asp:Label>
                            </td>
                            <td align="right" height="25">
                                <asp:Label ID="lblPercPessoasAbaixoQuinzeAnosDRADS" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblPercPessoasAbaixoQuinzeAnosEstado" Text="19,3" runat="server"></asp:Label>
                            </td>

                        </tr>
                        <tr style="height: 22px;">
                            <td align="left" rowspan="2">População com 60 anos ou mais (estimativa)</td>
                            <td align="center">Pessoas
                            </td>
                            <td align="center" rowspan="2">2017
                            </td>
                            <td align="right" height="25">
                                <asp:Label ID="lblNumPopulacaoSessentaAnosMunicipio" runat="server"></asp:Label>
                            </td>
                            <td align="right" height="25">
                                <asp:Label ID="lblNumPopulacaoSessentaAnosDRADS" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblNumPopulacaoSessentaAnosEstado" Text="6.119.022" runat="server"></asp:Label>
                            </td>

                            <td align="center" rowspan="2">
                                <asp:HyperLink ID="HyperLink18" NavigateUrl="http://www.seade.gov.br/" Target="_blank"
                                    runat="server" Text="SEADE"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">%
                            </td>
                            <td align="right" height="25">
                                <asp:Label ID="lblPerPopulacaoSessentaAnosMunicipio" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblPercPopulacaoSessentaAnosDRADS" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblPercPopulacaoSessentaAnosEstado" Text="14,0" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td align="left">Índice de envelhecimento</td>
                            <td align="center">Índice
                            </td>
                            <td align="center">2017
                            </td>
                            <td align="right" height="25">
                                <asp:Label ID="lblIndiceEnvelhecimentoMunicipio" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblIndiceEnvelhecimentoDRADS" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblIndiceEnvelhecimentoEstado" Text="72,00" runat="server"></asp:Label>
                            </td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink24" NavigateUrl="http://www.seade.gov.br" Target="_blank"
                                    runat="server" Text="SEADE"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td align="left">Razão de dependência</td>
                            <td align="center">%
                            </td>
                            <td align="center">2010
                            </td>
                            <td align="right" height="25">
                                <asp:Label ID="lblRazaoDependenciaMunicipio" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblRazaoDependenciaDRADS" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblRazaoDependenciaEstado" Text="28,62" runat="server"></asp:Label>
                            </td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink26" NavigateUrl="http://www.pnud.org.br/" Target="_blank"
                                    runat="server" Text="PNUD"></asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table class="table border bordered" id="tbEvolucaoRede" runat="server" cellspacing="0"
                        cellpadding="0" border="0">
                        <thead>
                            <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                                <th align="center" rowspan="2" width="320">Índices e Indicadores
                                </th>
                                <th align="center" width="120" rowspan="2">Unidade
                                </th>
                                <th align="center" colspan="3">Valores
                                </th>
                                <th align="center" width="80" rowspan="2">Fonte
                                </th>
                            </tr>
                            <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                                <th width="50px" align="center">2015
                                </th>
                                <th align="center" width="50">2016<br />
                                </th>
                                <th align="center" width="50">2017
                                </th>
                            </tr>
                        </thead>
                        <tr style="height: 30px;">
                            <td align="left">Serviços socioassistenciais da proteção social básica</td>
                            <td align="center">Serviços
                            </td>
                            <td align="right" height="25">
                                <asp:Label ID="lblNumServicosBasica2013" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblNumServicosBasica2014" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblNumServicosBasica2015" runat="server"></asp:Label>
                            </td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink13" NavigateUrl="http://www.pmas.sp.gov.br" Target="_blank"
                                    runat="server" Text="PMASweb"></asp:HyperLink>
                            </td>

                        </tr>
                        <tr style="height: 30px;">
                            <td align="left">Serviços socioassistenciais da proteção social especial de média complexidade
                            </td>
                            <td align="center">Serviços
                            </td>
                            <td align="right" height="25">
                                <asp:Label ID="lblNumServicosMedia2013" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblNumServicosMedia2014" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblNumServicosMedia2015" runat="server"></asp:Label>
                            </td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink15" NavigateUrl="http://www.pmas.sp.gov.br" Target="_blank"
                                    Text="PMASweb" runat="server"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td align="left">Serviços socioassistenciais da proteção social especial de alta complexidade</td>
                            <td align="center">Serviços</td>
                            <td align="right" height="25">
                                <asp:Label ID="lblNumServicosAlta2013" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblNumServicosAlta2014" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblNumServicosAlta2015" runat="server"></asp:Label>
                            </td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink16" NavigateUrl="http://www.pmas.sp.gov.br" Target="_blank"
                                    runat="server" Text="PMAS"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td align="left">Serviços socioassistenciais não tipificados</td>
                            <td align="center">Serviços
                            </td>
                            <td align="right" height="25">
                                <asp:Label ID="lblServicoNaoTipificados2013" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblServicoNaoTipificados2014" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblServicoNaoTipificados2015" runat="server"></asp:Label>
                            </td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink17" NavigateUrl="http://www.pmas.sp.gov.br" Target="_blank"
                                    runat="server" Text="PMASweb"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr style="height: 22px;">
                            <td align="left">Número de CRAS implantados no Munícipio
                            </td>
                            <td align="center">CRAS</td>
                            <td align="right">
                                <asp:Label ID="lblCRAS2013" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblCRAS2014" runat="server"></asp:Label></td>
                            <td align="right">
                                <asp:Label ID="lblCRAS2015" runat="server"></asp:Label></td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink21" NavigateUrl="http://www.pmas.sp.gov.br" Target="_blank"
                                    runat="server" Text="PMASweb"></asp:HyperLink></td>
                        </tr>
                        <tr style="height: 22px;">
                            <td align="left">Número de CREAS implantados no Munícipio</td>
                            <td align="center">CREAS</td>
                            <td align="right">
                                <asp:Label ID="lblCREAS2013" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblCREAS2014" runat="server"></asp:Label></td>
                            <td align="right">
                                <asp:Label ID="lblCREAS2015" runat="server"></asp:Label></td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink22" NavigateUrl="http://www.pmas.sp.gov.br" Target="_blank"
                                    runat="server" Text="PMASweb"></asp:HyperLink></td>
                        </tr>
                        <tr style="height: 22px;">
                            <td align="left">Número de Centro Pop Implantados
                            </td>
                            <td align="center">Famílias</td>
                            <td align="right">
                                <asp:Label ID="lblCentroPOP2013" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblCentroPOP2014" runat="server"></asp:Label></td>
                            <td align="right">
                                <asp:Label ID="lblCentroPOP2015" runat="server"></asp:Label></td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink23" NavigateUrl="http://www.pmas.sp.gov.br" Target="_blank"
                                    runat="server" Text="PMASweb"></asp:HyperLink></td>
                        </tr>
                        <tr style="height: 22px;">
                            <td align="left">Beneficiários BPC - Idosos
                            </td>
                            <td align="center">Pessoas</td>
                            <td align="right">
                                <asp:Label ID="lblBPCIdosos2013" runat="server"></asp:Label></td>
                            <td align="right">
                                <asp:Label ID="lblBPCIdosos2014" runat="server"></asp:Label></td>
                            <td align="right">
                                <asp:Label ID="lblBPCIdosos2015" runat="server"></asp:Label></td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink25" NavigateUrl="http://aplicacoes.mds.gov.br/sagi/portal/" Target="_blank"
                                    runat="server" Text="MDS/SAGI"></asp:HyperLink></td>
                        </tr>
                        <tr style="height: 22px;">
                            <td align="left">Beneficiários BPC - Pessoas com deficiência
                            </td>
                            <td align="center">Pessoas</td>
                            <td align="right">
                                <asp:Label ID="lblBPCDeficentes2013" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblBPCDeficentes2014" runat="server"></asp:Label></td>
                            <td align="right">
                                <asp:Label ID="lblBPCDeficentes2015" runat="server"></asp:Label></td>

                            <td align="center">
                                <asp:HyperLink ID="HyperLink27" NavigateUrl="http://aplicacoes.mds.gov.br/sagi/portal/" Target="_blank"
                                    runat="server" Text="MDS/SAGI"></asp:HyperLink></td>
                        </tr>
                    </table>
                </fieldset>

            </td>
        </tr>
        <tr>
            <td>
                <fieldset class="border-blue">
                    <legend class="lgnd fg-blue">B. Situações de vulnerabilidade ou risco social apontadas</legend>
                    <asp:ListView ID="lstAnaliseDiagnostica" runat="server">
                        <LayoutTemplate>
                            <table border="0" id="tbSituacoesVulnerabilidade" runat="server" cellpadding="0" cellspacing="0" class="table border bordered">
                                <thead>
                                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                                        <th width="520">Situações de vulnerabilidade ou risco mais graves </th>
                                        <th width="100">Classificação </th>
                                        <th width="150">Demanda estimada<br />
                                            para 2018 </th>
                                        <th width="200">Número de serviços existentes<br />
                                            que atendem esta demanda
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
                            <tr style="height: 25px;">
                                <td align="left"><%#DataBinder.Eval(Container.DataItem, "SituacaoVulnerabilidade")%></td>
                                <td align="center"><%#DataBinder.Eval(Container.DataItem, "Classificacao") %></td>
                                <td align="center"><%#String.Format("{0:N0}",DataBinder.Eval(Container.DataItem, "Demanda"))%></td>
                                <td align="center"><%#DataBinder.Eval(Container.DataItem, "TotalServicos") %></td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #F3F3F3 !important;">
                                <td align="left"><%#DataBinder.Eval(Container.DataItem, "SituacaoVulnerabilidade")%></td>
                                <td align="center"><%#DataBinder.Eval(Container.DataItem, "Classificacao") %></td>
                                <td align="center"><%#String.Format("{0:N0}",DataBinder.Eval(Container.DataItem, "Demanda"))%></td>
                                <td align="center"><%#DataBinder.Eval(Container.DataItem, "TotalServicos") %></td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <div align="center" style="width: 100%;">
                                <b class="titulo">Não existe registro.</b>
                            </div>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <br />
                    <table id="tbComunidades" runat="server" class="table striped border bordered" cellspacing="0"
                        cellpadding="0" border="0" width="100%">
                        <thead>
                            <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                                <th width="300">Povos, comunidades tradicionais e grupos específicos existentes no município</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                                <td colspan="2" style="height: 22px; background-color: #c9e6f6;"><b>Povos e comunidades tradicionais </b>
                                </td>
                            </tr>
                            <tr id="trCiganos" runat="server" visible="false">
                                <td>

                                    <asp:Label ID="lblCigano" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trExtrativistas" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblExtrativista" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trPescadores" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblPescadores" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trAfro" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblAfro" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trRibeirinha" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblRibeirinha" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trIndigenas" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblIndigenas" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trQuilombolas" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblQuilombolas" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trNenhumaComunidade" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblNenhumaComunidade" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                                <td colspan="2" style="height: 22px; background-color: #c9e6f6;"><b>Grupos específicos</b>
                                </td>
                            </tr>
                            <tr id="trAgricultores" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblAgricultores" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trAcampamentos" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblAcampamentos" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trPopulacaoPrisional" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblPopulacaoPrisional" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trTrabalhadoresSazonais" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblTrabalhadoresSazonais" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trAglomeradosSubnormais" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblAglomeradosSubnormais" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trAssentamentos" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblAssentamentos" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trNaoExisteGrupo" runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblNaoExisteGrup" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset class="border-blue">
                    <legend class="lgnd fg-blue">C. Prioridades para qualificação da rede de atendimento</legend>
                    <asp:ListView ID="lstIntencaoAcao" runat="server" DataKeyNames="IdUnidade">
                        <LayoutTemplate>
                            <table id="tbIntencaoAcao" runat="server" class="table striped border bordered" cellspacing="0"
                                cellpadding="0" border="0" width="100%">
                                <thead>
                                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                                        <th width="200">Locais de execução</th>
                                        <th width="200">Denominação do local</th>
                                        <th width="200">Necessidades apontadas</th>
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
                                <td align="left">
                                    <%#DataBinder.Eval(Container.DataItem, "TipoLocal")%>
                                </td>
                                <td align="left">
                                    <%#DataBinder.Eval(Container.DataItem, "Nome")%>
                                </td>
                                <td align="left">
                                    <%#DataBinder.Eval(Container.DataItem, "IntencaoAcao") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <div align="center" style="width: 100%;">
                                <b class="titulo">Não existe registro.</b>
                            </div>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <br />
                    <asp:ListView ID="lstServicosSocioassistencias" runat="server">
                        <LayoutTemplate>
                            <table id="tbServicosSocioAssistenciais" runat="server" class="table striped border bordered" cellspacing="0"
                                cellpadding="0" border="0" width="100%">
                                <thead>
                                    <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                                        <th width="80">Locais de execução</th>
                                        <th width="200">Denominação do local</th>
                                        <th width="100">Tipo de Serviço
                                        </th>
                                        <th width="150">Usuário
                                        </th>
                                        <th width="150">Avaliação de Serviço
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
                            <tr style="height: 22px; background-color: #c9e6f6;">
                                <td colspan="5">
                                    <b>Proteção Social:</b>
                                    <%#DataBinder.Eval(Container.DataItem, "Key") %>
                                </td>
                            </tr>
                            <tr>
                                <asp:ListView ID="lstItems" runat="server" DataSource='<%#Eval("Items") %>'>
                                    <LayoutTemplate>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td align="left">
                                                <%#DataBinder.Eval(Container.DataItem, "TipoLocal")%>
                                            </td>
                                            <td align="left">
                                                <%#DataBinder.Eval(Container.DataItem, "LocalExecucao") %>
                                            </td>
                                            <td align="left">
                                                <%#DataBinder.Eval(Container.DataItem, "TipoServico") %>
                                            </td>
                                            <td align="left">
                                                <%#DataBinder.Eval(Container.DataItem, "Usuario")%>
                                            </td>
                                            <td align="left">
                                                <%#DataBinder.Eval(Container.DataItem, "AvaliacaoServico")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </tr>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <div align="center" style="width: 100%;">
                                <b class="titulo">Não existe registro.</b>
                            </div>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <br />
                    <table id="tbRhOrgaoGestor" class="table striped border bordered" cellspacing="0"
                        cellpadding="0" border="0" width="100%" runat="server">
                        <thead>
                            <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                                <th width="100">Equipe Específica</th>
                                <th width="300">Existe intenção de estruturar esta equipe no órgão gestor nos próximos anos?</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr id="trEquipeBasica" runat="server" visible="false">
                                <td>Proteção Social Básica
                                </td>
                                <td>
                                    <asp:Label ID="lblEstrurarEquipeBasica" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trEquipeEspecial" runat="server" visible="false">
                                <td>Proteção Social Especial
                                </td>
                                <td>
                                    <asp:Label ID="lblEstrurarEquipeEspecial" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trEquipeVigilancia" runat="server" visible="false">
                                <td>Vigilância Socioassistencial
                                </td>
                                <td>
                                    <asp:Label ID="lblEstrurarEquipeVigilancia" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trEquipeGestaoBeneficios" runat="server" visible="false">
                                <td>Gestão de Benefícios/Transferência de Renda
                                </td>
                                <td>
                                    <asp:Label ID="lblEstrurarEquipeGestaoBeneficios" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trEquipeCadUnico" runat="server" visible="false">
                                <td>Gestão do Cadastro Único
                                </td>
                                <td>
                                    <asp:Label ID="lblEstrurarEquipeCadUnico" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trEquipeGestaoFinanceira" runat="server" visible="false">
                                <td>Gestão Financeira e Orçamentária
                                </td>
                                <td>
                                    <asp:Label ID="lblEstrurarEquipeGestaoFinanceira" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trEquipeGestaoSuas" runat="server" visible="false">
                                <td>Gestão do Trabalho no SUAS
                                </td>
                                <td>
                                    <asp:Label ID="lblEstrurarEquipeTrabalho" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trEquipeRegulacaoSuas" runat="server" visible="false">
                                <td>Regulação do Suas
                                </td>
                                <td>
                                    <asp:Label ID="lblEstrurarEquipeRegulacaoSuas" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trEquipeRedeDireta" runat="server" visible="false">
                                <td>Execução dos serviços socioassistenciais da rede direta
                                </td>
                                <td>
                                    <asp:Label ID="lblAumentarEquipeRedeDireta" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr style="background-color: #c9e6f6;" id="trEquipeOrgaoGestor" runat="server" visible="false">
                                <td colspan="2">
                                    <b>
                                        <asp:Label ID="lblAumentarEquipeOrgaoGestor" runat="server"></asp:Label></b>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset class="border-blue">
                    <legend class="lgnd fg-blue">D. Análise</legend>
                    <table class="table striped border bordered" id="tbAnaliseDemografica" runat="server">
                        <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                            <td>
                                <strong>Território e demografia</strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" height="100%" style="padding: 10px;">
                                <asp:Label ID="lblCaracterizacao" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table class="table striped border bordered" id="tbAnalisePopulacao" runat="server">
                        <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                            <td>
                                <strong>População e vulnerabilidade social </strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 10px;" align="left">
                                <asp:Label ID="lblCaracterizacaoPopulacao" runat="server"></asp:Label>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table class="table striped border bordered" id="tbAnaliseSituacaoVulnerabilidade" runat="server">
                        <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                            <td>
                                <strong>Evolução da rede de atendimento</strong>
                            </td>
                        </tr>
                        <tr>
                            <td class="table border bordered" height="100%" style="padding: 10px;" align="left">
                                <asp:Label ID="lblCaracterizacaoEvolucaoRede" runat="server"></asp:Label>
                                <br />
                            </td>
                        </tr>

                    </table>
                    <br />
                    <table class="table striped border bordered" id="tbAnaliseInterpretacao" runat="server">
                        <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                            <td>
                                <strong>Análise e Interpretação</strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="100%">
                                <asp:Label ID="lblAnaliseInterpretacao" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset class="border-blue">
                    <legend class="lgnd fg-blue">Atualizações anual</legend>
                    <table class="table striped border bordered" id="Table1" runat="server">
                        <tr class="info" style="background-color: #7cc8ff; height: 22px;">
                            <td>
                                <strong>Realizada no 2º semestre de 2018:</strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" height="100%" style="padding: 10px;">
                                <asp:Label ID="lblAualizacao2018" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
