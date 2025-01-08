<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FExecucaoFinanceira.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoV.FExecucaoFinanceira" %>

<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <script src="../Scripts/dataFormat.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmConselhos">
                <div class="accordion">
                  
                    <div class="frame active">
                        <div class="heading">
                          <b>5.4.a - Execução financeira</b>
                            <span class="mif-dollar2 icon"></span>
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="execução financeira">
                                <div class="grid">
                                    <div class="row">
                                        <div id="Quadrienal">
                                            <asp:Button ID="btnExercicio1" runat="server" Width="113px" OnClick="btnLoadExercicio1_Click"></asp:Button>
                                            <asp:Button ID="btnExercicio2" runat="server" Width="113px" OnClick="btnLoadExercicio2_Click"></asp:Button>
                                            <asp:Button ID="btnExercicio3" runat="server" Width="113px" OnClick="btnLoadExercicio3_Click"></asp:Button>
                                            <asp:Button ID="btnExercicio4" runat="server" Width="113px" OnClick="btnLoadExercicio4_Click"></asp:Button>
                                        </div>
                                        <div class="cell">
                                          <center> <asp:Label runat="server" Text="" ID="lblExecucaoDisponibilidade" ></asp:Label></center>
                                        </div>
                                        <br />
                                        <div class="cell">
                                            <p style="text-align:center" runat="server" id="pInformacoes" visible="false">
                                                As informações dos quadros abaixo são registradas pelo próprio órgão gestor municipal de Assistência Social
                                                e não há verificação de sua exatidão por parte da Secretaria Estadual de Desenvolvimento Social (SEDS).
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table class="table border bordered" cellspacing="0" cellpadding="0" border="0" width="100%">
                                                <thead class="info">
                                                    <tr>
                                                        <th align="center" width="80" style="height: 40px;">Origem dos<br />
                                                            recursos
                                                        </th>
                                                        <th align="center" width="180">Destinações
                                                        </th>
                                                        <th align="center" width="120">Previsão inicial de<br />
                                                            repasse
                                                        </th>
                                                        <th align="center" width="100">Recursos<br />
                                                            disponibilizados
                                                        </th>
                                                        <th align="center" width="150">Resultado de<br />
                                                            aplicações financeiras
                                                        </th>
                                                        <th align="center" width="100">Valores executados
                                                        </th>
                                                        <th align="center" width="100">Valores passíveis de reprogramação
                                                        </th>
                                                        <th align="center" width="100">Valores devolvidos
                                                        </th>
                                                        <th align="center" width="100">Porcentagens
                                        <br />
                                                            de Execução
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <%-- <tr class="jqgfirstrow" style="height: auto;">
                                                        <td style="height: 0px;"></td>
                                                        <td style="height: 0px;"></td>
                                                        <td style="height: 0px;"></td>
                                                        <td style="height: 0px;"></td>
                                                        <td style="height: 0px;"></td>
                                                        <td style="height: 0px;"></td>
                                                        <td style="height: 0px;"></td>
                                                        <td style="height: 0px;"></td>
                                                        <td style="height: 0px;"></td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td id="tdFMASTitulo" runat="server" class="info" align="center" >
                                                            <b>Recursos<br />
                                                                municipais<br />
                                                                (FMAS)</b>
                                                        </td>
                                                        <td align="left">Serviços da Proteção Social Básica
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASPrevisaoInicialBasica" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASRecursosDisponibilizadosBasica" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASResultadoAppFinanceirasBasica" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASValoresExecutadosBasica" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASValoresReprogramadosBasica" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASValoresDevolvidosBasica" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASPorcentagensExecucaoBasica" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left"> Serviços da Proteção Social Especial de Média Complexidade 
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASPrevisaoInicialMedia" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASRecursosDisponibilizadosMedia" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASResultadoAppFinanceirasMedia" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASValoresExecutadosMedia" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASValoresReprogramadosMedia" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASValoresDevolvidosMedia" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASPorcentagensExecucaoMedia" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td align="left"> Serviços da Proteção Social Especial de Alta Complexidade
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASPrevisaoInicialAlta" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASRecursosDisponibilizadosAlta" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASResultadoAppFinanceirasAlta" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASValoresExecutadosAlta" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASValoresReprogramadosAlta" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASValoresDevolvidosAlta" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASPorcentagensExecucaoAlta" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>

                                                    <tr runat="server" ID = "trFMASBeneficiosEventuais">
                                                        <td align="left">Benefícios eventuais
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASPrevisaoInicialBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASRecursosDisponibilizadosBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASResultadoAppFinanceirasBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASValoresExecutadosBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASValoresReprogramadosBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASValoresDevolvidosBeneficiosEventuais" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASPorcentagensExecucaoBeneficiosEventuais" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>
													
													<tr runat="server" ID = "trFMASProgramasProjetos">
                                                        <td align="left">Programas & projetos
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASPrevisaoInicialProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASRecursosDisponibilizadosProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASResultadoAppFinanceirasProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASValoresExecutadosProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASValoresReprogramadosProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASValoresDevolvidosProgramasProjetos" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFMASPorcentagensExecucaoProgramasProjetos" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>

                                                    <tr class="info" style="height: 20px">
                                                        <td align="right">
                                                            <b>Totais</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFMASPrevisaoInicial" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFMASRecursosDisponibilizados" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFMASResultadoAppFinanceiras" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFMASValoresExecutados" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFMASValoresReprogramados" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFMASValoresDevolvidos" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFMASPorcentagensExecucao" runat="server" />
                                                        </td>
                                                    </tr>

                                                    <tr runat="server" id="trEstadual" >
                                                        <td class="info" align="center" rowspan="5">
                                                            <b>Recursos<br />
                                                                estaduais<br />
                                                                (FEAS)</b>
                                                        </td>
                                                        <td>Reprogramação do exercício anterior
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtPrevisaoInicialReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtRecursosDisponibilizadosReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtResultadosAplicacaoReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtValoresExecutadosReprogramacao" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtValoresReprogramacao" runat="server" Width="110px" Enabled="false" Text="0,00" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtValoresDevolvidosReprogramacao" Text="0,00" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtPorcentagensDevolucaoReprogramacao" runat="server" Width="80px" Enabled="false" Text="0,00%" Style="text-align: right" />
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="trEstadualBasica">
                                                        <td align="left">Básica
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASPrevisaoInicialBasica" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASRecursosDisponibilizadosBasica" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASResultadoAppFinanceirasBasica" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASValoresExecutadosBasica" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASValoresReprogramadosBasica" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASValoresDevolvidosBasica" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASPorcentagensExecucaoBasica" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="trEstadualMedia">
                                                        <td align="left">Especial de média complexidade
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASPrevisaoInicialMedia" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASRecursosDisponibilizadosMedia" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASResultadoAppFinanceirasMedia" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASValoresExecutadosMedia" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASValoresReprogramadosMedia" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASValoresDevolvidosMedia" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASPorcentagensExecucaoMedia" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="trEstadualAlta">
                                                        <td align="left">Especial de alta complexidade
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASPrevisaoInicialAlta" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASRecursosDisponibilizadosAlta" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASResultadoAppFinanceirasAlta" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASValoresExecutadosAlta" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASValoresReprogramadosAlta" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASValoresDevolvidosAlta" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFEASPorcentagensExecucaoAlta" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>
                                                    <tr class="info" style="height: 20px" runat="server" id="trEstadualTotais">
                                                        <td align="right">
                                                            <b>Totais</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFEASPrevisaoInicial" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFEASRecursosDisponibilizados" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFEASResultadoAppFinanceiras" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFEASValoresExecutados" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFEASValoresReprogramados" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFEASValoresDevolvidos" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFEASPorcentagensExecucao" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td id="tdRecursosFederais" runat="server"  class="info" align="center">
                                                            <b>Recursos<br />
                                                                federais<br />
                                                                (FNAS)</b>
                                                        </td>
                                                        <td align="left">Serviços da Proteção Social Básica
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASPrevisaoInicialBasica" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASRecursosDisponibilizadosBasica" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASResultadoAppFinanceirasBasica" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresExecutadosBasica" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresReprogramadosBasica" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresDevolvidosBasica" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASPorcentagensExecucaoBasica" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trFNASMedia" runat="server">
                                                        <td align="left">Especial de média complexidade
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASPrevisaoInicialMedia" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASRecursosDisponibilizadosMedia" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASResultadoAppFinanceirasMedia" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresExecutadosMedia" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresReprogramadosMedia" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresDevolvidosMedia" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASPorcentagensExecucaoMedia" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trFNASAlta" runat="server">
                                                        <td align="left">Especial de alta complexidade
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASPrevisaoInicialAlta" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASRecursosDisponibilizadosAlta" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASResultadoAppFinanceirasAlta" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresExecutadosAlta" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresReprogramadosAlta" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresDevolvidosAlta" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASPorcentagensExecucaoAlta" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trFNASProtecaoSocialEspecial" runat="server">
                                                        <td align="left">Serviços da Proteção Social Especial
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASPrevisaoInicialProtecaoSocialEspecial" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASRecursosDisponibilizadosProtecaoSocialEspecial" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASResultadoAppFinanceirasProtecaoSocialEspecial" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresExecutadosProtecaoSocialEspecial" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresReprogramadosProtecaoSocialEspecial" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresDevolvidosProtecaoSocialEspecial" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASPorcentagensExecucaoProtecaoSocialEspecial" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trFNASProgramasProjetos" runat="server">
                                                        <td align="left">Programas e projetos
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASPrevisaoInicialProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASRecursosDisponibilizadosProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASResultadoAppFinanceirasProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresExecutadosProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresReprogramadosProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresDevolvidosProgramasProjetos" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASPorcentagensExecucaoProgramasProjetos" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trFNASIncentivoGestao" runat="server">
                                                        <td align="left">Incentivos à gestão (IGDs)
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASPrevisaoInicialIncentivoGestao" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASRecursosDisponibilizadosIncentivoGestao" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASResultadoAppFinanceirasIncentivoGestao" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresExecutadosIncentivoGestao" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresReprogramadosIncentivoGestao" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresDevolvidosIncentivoGestao" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASPorcentagensExecucaoIncentivoGestao" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trFNASExercicioAnterior" runat="server">
                                                        <td align="left">Reprogramação do exercício anterior
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASPrevisaoInicialExercicioAnterior" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASRecursosDisponibilizadosExercicioAnterior" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASResultadoAppFinanceirasExercicioAnterior" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresExecutadosExercicioAnterior" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresReprogramadosExercicioAnterior" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASValoresDevolvidosExercicioAnterior" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtFNASPorcentagensExecucaoExercicioAnterior" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>
                                                    <tr class="info" style="height: 20px">
                                                        <td align="right">
                                                            <b>Totais</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFNASPrevisaoInicial" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFNASRecursosDisponibilizados" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFNASResultadoAppFinanceiras" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFNASValoresExecutados" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFNASValoresReprogramados" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFNASValoresDevolvidos" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblFNASPorcentagensExecucao" runat="server" />
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td id="tdTotal" runat="server" class="info" align="center" >
                                                            <b>Total de<br />
                                                                recursos<br />
                                                                do FMAS<br />
                                                                e FNAS</b>
                                                        </td>
                                                        <td align="left">Serviços da Proteção Social Básica
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtPrevisaoInicialBasica" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtRecursosDisponibilizadosBasica" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtResultadoAppFinanceirasBasica" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtValoresExecutadosBasica" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtValoresReprogramadosBasica" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtValoresDevolvidosBasica" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtPorcentagensExecucaoBasica" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>

                                                    <tr id="trTotalMedia" runat="server">
                                                        <td align="left">Especial de média complexidade
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtPrevisaoInicialMedia" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtRecursosDisponibilizadosMedia" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtResultadoAppFinanceirasMedia" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtValoresExecutadosMedia" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtValoresReprogramadosMedia" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtValoresDevolvidosMedia" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtPorcentagensExecucaoMedia" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>

                                                    <tr id="trTotalAlta" runat="server">
                                                        <td align="left">Especial de alta complexidade
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtPrevisaoInicialAlta" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtRecursosDisponibilizadosAlta" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtResultadoAppFinanceirasAlta" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtValoresExecutadosAlta" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtValoresReprogramadosAlta" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtValoresDevolvidosAlta" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtPorcentagensExecucaoAlta" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>

                                                    <tr id="trEspecialTotal" runat="server">
                                                        <td align="left">Serviços da Proteção Social Especial
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalPrevisaoInicialProtecaoSocialEspecial" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalRecursosDisponibilizadosProtecaoSocialEspecial" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalResultadoAppFinanceirasProtecaoSocialEspecial" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalValoresExecutadosProtecaoSocialEspecial" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalValoresReprogramadosProtecaoSocialEspecial" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalValoresDevolvidosProtecaoSocialEspecial" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalPorcentagensExecucaoProtecaoSocialEspecial" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>

                                                    <tr id="trBeneficiosEventuaisTotal" runat="server">
                                                        <td align="left">Benefícios eventuais
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalPrevisaoInicialBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalRecursosDisponibilizadosBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalResultadoAppFinanceirasBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalValoresExecutadosBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalValoresReprogramadosBeneficiosEventuais" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalValoresDevolvidosBeneficiosEventuais" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalPorcentagensExecucaoBeneficiosEventuais" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>

                                                    <tr id="trProgramasProjetosTotal" runat="server">
                                                        <td align="left">Programas e projetos
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalPrevisaoInicialProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalRecursosDisponibilizadosProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalResultadoAppFinanceirasProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalValoresExecutadosProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalValoresReprogramadosProgramasProjetos" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalValoresDevolvidosProgramasProjetos" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalPorcentagensExecucaoProgramasProjetos" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trIncentivoTotal" runat="server">
                                                        <td align="left">Incentivos à gestão (IGDs)
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalPrevisaoInicialIncentivoGestao" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalRecursosDisponibilizadosIncentivoGestao" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalResultadoAppFinanceirasIncentivoGestao" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalValoresExecutadosIncentivoGestao" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalValoresReprogramadosIncentivoGestao" runat="server" Width="110px" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalValoresDevolvidosIncentivoGestao" runat="server" Width="110px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtTotalPorcentagensExecucaoIncentivoGestao" runat="server" Width="80px" Enabled="false" Style="text-align: right" />
                                                        </td>
                                                    </tr>

                                                    <tr class="info" style="height: 20px">
                                                        <td align="right">
                                                            <b>Totais</b>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPrevisaoInicial" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblRecursosDisponibilizados" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblResultadoAppFinanceiras" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblValoresExecutados" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblValoresReprogramados" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblValoresDevolvidos" runat="server" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label ID="lblPorcentagensExecucao" runat="server" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell" align="center">
                                            <asp:Button ID="btnCalcular" Text="Calcular" runat="server" OnClick="btnCalcular_Click" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Comentários do órgão gestor municipal da Assistência Social sobre a execução financeira dos recursos municipais e federais</b></legend>
                                            <div class="row">
                                                <div class="cell">
                                                    <div class="input-control textarea full-size">
                                                        <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" MaxLength="5000"
                                                            Height="64px" Width="100%" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell" align="center">
                                                    <skm:TextBoxCounter ID="NameCounter" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 5000 caracteres."
                                                        Font-Bold="True" TextBoxControlId="txtComentario" />
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="row">
                                        <div class="cell" align="center">
                                            <asp:Button ID="btnSalvar" TabIndex="16" runat="server" Width="89px" Text="Salvar"
                                                SkinID="button-save" OnClick="btnSalvar_Click"></asp:Button>
                                        </div>
                                    </div>
                                    <div class="row" runat="server" id="trFinalizarGestorCalculo" visible="false">
                                        <div class="cell" align="center">
                                            <asp:Button ID="btnFinalizarCalculo" TabIndex="16" runat="server" Width="146px" Text="Finalizar e enviar"
                                                SkinID="button-save" OnClientClick="return confirm('Tem certeza que deseja finalizar o registro das informações sobre a execução dos recursos financeiros estaduais? Caso escolha finalizar, não poderá ser feita mais nenhuma alteração nestes dados sem autorização do CMAS.');"
                                                OnClick="btnFinalizarCalculo_Click"></asp:Button>
                                        </div>
                                    </div>
                                    
                                    <div class="row" runat="server" id="trComentarioCMAS" >
                                      <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Comentários e Parecer do Conselho Municipal de Assistência Social</b></legend>
                                            <div class="row">
                                                <div class="cell">
                                                    <div class="input-control textarea full-size">
                                                        <asp:TextBox ID="txtComentario2" runat="server" TextMode="MultiLine" MaxLength="5000"
                                                            Height="64px" Width="100%" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell" align="center">
                                                    <skm:TextBoxCounter ID="NameCounter2" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 5000 caracteres."
                                                        Font-Bold="True" TextBoxControlId="txtComentario2" />
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="row" runat="server" id="trDeliberacao" >
                                        <div class="cell" align="left" style="background-color:#7CC8FF">
                                        <b>Deliberação do Conselho Municipal de Assistência Social</b>
                                        </div>
                                        <div class="cell" style="border:solid 1px #d9d9d9">
                                            <asp:RadioButtonList runat="server" ID="rblDeliberacao" RepeatDirection="Horizontal">
                                                <asp:ListItem Value ="1">Aprovado</asp:ListItem>
                                                <asp:ListItem Value ="3">Rejeitado</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="cell" style="border:solid 1px #d9d9d9;height:80px">
                                            <br />
                                            &nbsp&nbsp<asp:Label runat="server"><b>Data da reunião:</b></asp:Label>&nbsp <asp:TextBox runat="server"  ID="txtDataReuniao" onkeypress="mascaraData(this)" Width="110px" MaxLength="10" ></asp:TextBox> &nbsp&nbsp

                                            <asp:Label runat="server"><b>Nº de conselheiros participantes com direito a voto:</b></asp:Label>&nbsp <asp:TextBox runat="server" ID="txtNumeroConselheiros"  Width="110px" MaxLength="5" ></asp:TextBox> &nbsp&nbsp
                                            
                                            <asp:Label runat="server"><b>Nº da Ata:</b></asp:Label>&nbsp <asp:TextBox runat="server" ID="txtNumeroAta" Width="110px" MaxLength="5"></asp:TextBox> &nbsp&nbsp
                                            
                                            <asp:Label runat="server"><b>Nº da Resolução:</b></asp:Label>&nbsp <asp:TextBox runat="server" ID="txtNumeroResolucao" Width="110px" MaxLength="5"></asp:TextBox> &nbsp&nbsp
                                            
                                            <asp:Label runat="server"><b>Data de publicação:</b></asp:Label>&nbsp <asp:TextBox runat="server" ID="txtDataPublicacao" Width="110px" MaxLength="10" onkeypress="mascaraData(this)"> </asp:TextBox>
                                        </div>
                                        <div class="cell">
                                            <asp:Button ID="btnSalvarCMAS" runat="server" Text="Salvar" OnClick="btnSalvarCMAS_Click" />&nbsp&nbsp
                                            <asp:Button ID="btnDevolverCMAS" runat="server" Text="Devolver ao órgão gestor para alterações" OnClick="btnDevolverCMAS_Click" />
                                        </div>
                                    </div>

                                    <div class="row" runat="server" id="trAprovacaoDRADS" visible="false">
                                        <div class="cell" align="center">
                                            <div class="row">
                                                <div class="cell">
                                                    A DRADS está de acordo com os dados informados no quadro de execução financeira?
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell" align="center">
                                                    <asp:RadioButtonList ID="rblAprovacaoDRADS" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Sim" />
                                                        <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell" align="center">
                                                    <asp:Button ID="btnSalvarAprovacaoDRADS" TabIndex="16" runat="server" Width="89px"
                                                        Text="Salvar" SkinID="button-save" OnClick="btnSalvarAprovacaoDRADS_Click"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" runat="server" id="trAprovacaoCMAS" visible="false">
                                        <div class="cell" align="center">
                                            <div class="row">
                                                <div class="cell">
                                                    <span style="text-align: center;">O CMAS aprova os valores informados na execução financeira?
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell" align="center">
                                                    <asp:RadioButtonList ID="rblAprovacaoCMAS" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Sim" />
                                                        <asp:ListItem Value="0" Text="Não" Selected="True" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell" align="center">
                                                    <asp:Button ID="btnSalvarAprovacaoCMAS" TabIndex="16" runat="server" Width="89px"
                                                        Text="Salvar" SkinID="button-save" OnClick="btnSalvarAprovacaoCMAS_Click"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" runat="server" id="trCancelarAprovacao" visible="false">
                                        <div class="cell" align="center">
                                            <div class="row">
                                                <div class="cell">
                                                    Deseja desbloquear quadro de Execução Financeira?
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="cell">
                                                    <asp:Button ID="btnCancelarAprovacao" TabIndex="16" runat="server" Width="146px"
                                                        Text="Desbloquear" SkinID="button-save" OnClick="btnCancelarAprovacao_Click"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
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
                <asp:HiddenField ID="hdfAno" runat="server" Value="" />
            </form>
            <table width="100%" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="FCronogramaDesembolso.aspx?idTipo=Bnzvd7mnCuQnCoKaLRcymQ%3d%3d">Próximo
                           <span class="mif-arrow-right"></span></a>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
