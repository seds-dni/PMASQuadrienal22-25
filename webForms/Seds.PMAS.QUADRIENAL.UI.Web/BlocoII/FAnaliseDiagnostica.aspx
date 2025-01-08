<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FAnaliseDiagnostica.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoII.FAnaliseDiagnostica" %>

<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmPrefeitura">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <a href="#" runat="server" id="linkAlteracoesQuadro14" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>&nbsp;
                            2.1 -Território e demografia
                           <span class="mif-earth icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="Território Demografia">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                            <table class="table striped border bordered" cellspacing="0"
                                                cellpadding="0" border="0">
                                                <thead class="info">
                                                    <tr>
                                                        <th align="center" valign="middle" rowspan="2" width="350">Indicador
                                                        </th>
                                                        <th align="center" width="70" rowspan="2">Unidade</th>
                                                        <th rowspan="2" width="70" align="center">Nota<br />Explicativa
                                                        </th>
                                                        <th align="center" rowspan="2" width="100">Referência
                                                        </th>
                                                        <th align="center" width="220" colspan="3">Valores
                                                        </th>

                                                        <th align="center" width="70" rowspan="2">Fonte
                                                        </th>
                                                    </tr>
                                                    <tr style="height: 25px;">
                                                        <th width="100">Município
                                                        </th>
                                                        <th align="center" width="100">DRADS
                                                        </th>
                                                        <th align="center" width="100">Estado
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="height: 22px;">
                                                        <td align="left">
                                                            <asp:Label ID="Label21" Text="Área territorial" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="center">Km²
                                                        </td>
                                                        <td align="center">
                                                            <button id="btnAjudaAreaTerritorial" class="note" onclick="return false;"></button>
                                                        </td>
                                                        <td align="center">2020
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblAreaTerritorial" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblAreaTerritorialDRADS" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblAreaTerritorialEstado" runat="server">248.219,627</asp:Label>
                                                        </td>

                                                        <td align="center">
                                                            <asp:HyperLink ID="HyperLink1" NavigateUrl="http://www.ibge.gov.br" Target="_blank"
                                                                runat="server" Text="IBGE"></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">
                                                            <asp:Label ID="Label20" Text="Estimativa do número de habitantes" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="center">Pessoas
                                                        </td>
                                                        <td align="center">
                                                            <button id="btnAjudaNumeroHabitantes" class="note" onclick="return false;"></button>
                                                        </td>
                                                        <td align="center">2020
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblNumeroHabitantes" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblNumeroHabitantesDRADS" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotalNumeroHabitantesEstado" runat="server">43.674.533</asp:Label>
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
                                                        <td align="center">
                                                            <button id="btnAjudaDensidade" class="note" onclick="return false;"></button>
                                                        </td>
                                                        <td align="center">2020
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblDensidade" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblDensidadeDRADS" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblDensidadeEstado" runat="server">176</asp:Label>
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
                                                        <td align="center">
                                                            <button id="btnAjudaTaxaGeometrica" class="note"
                                                                onclick="return false;">
                                                            </button>

                                                        </td>
                                                        <td align="center">2020-2030
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTaxaGeometrica" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTaxaGeometricaDRADS" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTaxaGeometricaEstado" runat="server">0,83</asp:Label>
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
                                                        <td align="center">
                                                            <button id="btnAjudaGrauUrbanizacao" class="note" onclick="return false;"></button>
                                                        </td>
                                                        <td align="center">2020
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblGrauUrbanizacao" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblGrauUrbanizacaoDRADS" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblGrauUrbanizacaoEstado" runat="server">96,37</asp:Label>
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
                                                        <td align="center">
                                                            <button id="btnAjudaDomiciliosPermanentes" class="note" onclick="return false;"></button>
                                                        </td>
                                                        <td align="center">2020
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblDomicilios" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblDomiciliosDRADS" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblDomiciliosEstado" runat="server">14.537.082</asp:Label>
                                                        </td>

                                                        <td align="center">
                                                            <asp:HyperLink ID="HyperLink10" NavigateUrl="http://www.seade.gov.br/" Target="_blank"
                                                                runat="server" Text="SEADE"></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 22px;">
                                                        <td align="left">Numero de pessoas por domicílio (estimativa)
                                                        </td>
                                                        <td align="center">Pessoas</td>
                                                        <td align="center" rowspan="2">
                                                            <button id="btnAjudaPessoasDomicilio" class="note" onclick="return false;"></button>
                                                        </td>
                                                        <td align="center">2020
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblPessoasDomicilios" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblPessoasDomiciliosDRADS" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="lblTotallblPessoasDomiciliosEstado" runat="server">3,0</asp:Label>
                                                        </td>

                                                        <td align="center">
                                                            <asp:HyperLink ID="HyperLink3" Target="_blank" NavigateUrl="http://www.seade.gov.br/"
                                                                runat="server" Text="SEADE"></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <div class="margin15 no-margin-left no-margin-right sub-header text-light">
                                                <strong>Este quadro possibilita ao município elaborar uma análise com base não apenas nos indicadores territoriais e demográficos aqui apresentados, 
                                    mas também utilizando as observações ou pesquisas realizadas no Município.
                                    <br />
                                                    É importante perceber o município como espaço geográfico, mas também considerar: como são utilizados os espaços da cidade e quem os utiliza; o processo histórico de formação dos territórios, 
                                    bem como as relações sociais e coletivas que influenciam estes territórios e a demografia do município.
                                                </strong>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <div class="input-control text big-input full-size" data-role="input">
                                                <asp:TextBox Width="100%" ID="txtCaracterizacao" runat="server" TextMode="MultiLine"
                                                    Height="302px" MaxLength="4000"></asp:TextBox>
                                                <button class="button helper-button clear"><span class="mif-cross"></span></button>
                                                <br />
                                            </div>
                                            <skm:TextBoxCounter ID="NameCounter" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 4000 caracteres."
                                                Font-Bold="True" TextBoxControlId="txtCaracterizacao" MaxCharacterLength="4000" />
                                            <br />
                                            <asp:Button ID="btnSalvarCaracterizacao" runat="server" SkinID="button-save" Width="89px"
                                                Text="Salvar" OnClick="btnSalvarCaracterizacao_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
            </form>
            <tr>
                <td>
                    <br />
                    <table id="tbInconsistencias" runat="server" align="center" border="0" cellpadding="0" cellspacing="2" visible="false" width="100%">
                        <tr>
                            <td class="ui-state-highlight titulo" style="padding: 2px 10px 2px 10px;">
                                <img src="../Styles/Icones/messagebox_warning.png" align="absMiddle" />
                                <b style="color: #000000 !important">Verifique as inconsistências:</b>
                                <br />
                                <br />
                                <asp:Label ID="lblInconsistencias" runat="server" ForeColor="Red" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>

            <table width="1000" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">&nbsp;
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="FPopulacaoVulnerabilidade.aspx">Próximo
                            <span class="mif-arrow-right"></span></a>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdfId" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(function () {
            $('#btnAjudaAreaTerritorial').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Área territorial', content: "Dados extraídos da tabela de áreas dos municípios do IBGE. As áreas disponibilizadas foram calculadas em ambiente de Sistema de Informações Geográficas, utilizando-se a Projeção Cônica Equivalente de Albers. O sistema de referência utilizado foi o Sistema de Referência Geocêntrico para as Américas (SIRGAS2000).A Malha Municipal de Referência utilizada para os cálculos é a Malha Municipal Digital 2020, disponibilizada no sítio do IBGE através do seguinte endereço:https://www.ibge.gov.br/geociencias/organizacao-do-territorio/malhas-territoriais" });
                }, 500);
            });
            $('#btnAjudaNumeroHabitantes').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Número de habitantes', content: "As populações aqui apresentadas resultam de projeções elaboradas pelo método dos componentes demográficos. Este método considera as tendências de fecundidade, mortalidade e migração, a partir das estatísticas vitais processadas na Fundação SEADE e a formulação de hipóteses de comportamento futuro para estes componentes." });
                }, 500);
            });
            $('#btnAjudaDensidade').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Densidade demográfica', content: "Cálculo realizado pela CGE/SEDS a partir dos dados de áreas territoriais do IBGE e da estimativa da população para 2021 elaborada pela Fundação SEADE." });
                }, 500);
            });

            $('#btnAjudaTaxaGeometrica').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Taxa geométrica de  crescimento anual da população', content: "Expressa, em termos percentuais, o crescimento médio da população em determinado período de tempo. Geralmente, considera-se que a população experimenta um crescimento exponencial ou geométrico." });
                }, 500);
            });
            $('#btnAjudaGrauUrbanizacao').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Grau de Urbanização', content: "Percentual da população urbana em relação à população total do município, das DRADS e do estado de São Paulo calculado a partir da estimativa da população para 2020 elaborada pela Fundação SEADE." });
                }, 500);
            });
            $('#btnAjudaDomiciliosPermanentes').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Domicílios particulares permanentes (estimativa)', content: "Segundo definições do IBGE, domicílio é o local construído para servir exclusivamente à habitação e tem a finalidade de servir de moradia a uma ou mais pessoas. O domicílio é considerado particular quando o relacionamento entre seus ocupantes é ditado por laços de parentesco, de dependência doméstica ou por normas de convivência (IBGE). Os dados de 2020 correspondem a projeção realizada pela Fundação Seade, tendo como ponto de partida a projeção da população residente nos municípios do Estado de São Paulo." });
                }, 500);
            });
            $('#btnAjudaPessoasDomicilio').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Número de pessoas por domicílio (estimativa)', content: "Número de habitantes residentes de uma unidade geográfica em determinado momento em relação ao número de domicílios desta mesma unidade. Calculado de acordo com as estimativas SEADE para 2020." });
                }, 500);
            });



        });

    </script>
</asp:Content>
