<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FEvolucaoRedeSocioassistencial.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoII.FEvolucaoRedeSocioassistencial" %>

<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <contenttemplate>
             <form name="frmPrefeitura">
                  <div class="accordion" data-role="accordion">
                    <div class="frame active">
                        <div class="heading">
                             <a href="#" runat="server" id="linkAlteracoesQuadro3" visible="false">
                                        <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                                    </a>&nbsp;
                                    2.3 - Evolução da rede de atendimento<span class="mif-earth icon"></span>
                                   
                        </div>
                           <div class="content">
                            <div class="formInput" data-text="Evolução Rede Socioassistencial">
                                <div class="grid">
                                    <div class="row">
                                        <div class="cell">
                                        <table class="table striped border bordered" cellspacing="0"
                                        cellpadding="0" border="0" width="800px">
                                        <thead class="info">
                                        <tr  style="height: 25px;">
                                            <td  align="center" rowspan="2" width="320">Indicador
                                            </td>
                                            <td align="center" width="120" rowspan="2" >Unidade
                                            </td>
                                            <td rowspan="2" align="center" width="70">Nota<br />
                                                explicativa
                                            </td>
                                            <td align="center" colspan="4">Valores
                                            </td>
                                            <td align="center" width="70" rowspan="2">Fonte
                                            </td>
                                        </tr>
                                          <tr style="height: 25px;">
                                            <td width="50px" align="center">2018</td>
                                            <td align="center" width="50" >2019</td>
                                            <td align="center" width="50" >2020</td>
                                            <td align="center" width="50" >2021</td>
                                        </tr>
                                      </thead>
                                      <tbody>
                                        <tr style="height: 30px;">
                                            <td align="left">
                                                Serviços socioassistenciais da proteção social básica</td>
                                            <td align="center">
                                                Serviços
                                            </td>
                                            <td align="center">
                                               <button id="btnAjudaServicosSocioassistenciais" class="note" onclick="return false;"></button>
                                            </td>
                                            <td align="right" height="25">
                                                <asp:Label ID="lblNumServicosBasica2018" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">   <asp:Label ID="lblNumServicosBasica2019" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblNumServicosBasica2020" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblNumServicosBasica2021" runat="server"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:HyperLink ID="HyperLink20" NavigateUrl="http://www.pmas.sp.gov.br" Target="_blank"
                                                    runat="server" Text="PMASweb"></asp:HyperLink>
                                            </td>

                                        </tr>
                                        <tr  style="height: 30px;">
                                            <td align="left">Serviços socioassistenciais da proteção social especial de média complexidade
                                            </td>
                                              <td align="center">
                                                  Serviços
                                            </td>
                                            <td align="center">
                                               <button id="btnAjudaServicosMediaComplexidade" class="note" onclick="return false;"></button>
                                            </td>
                                            <td align="right" height="25">
                                                <asp:Label ID="lblNumServicosMedia2018" runat="server"></asp:Label>
                                            </td>
                                            <td align="right"><asp:Label ID="lblNumServicosMedia2019" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                                  <asp:Label ID="lblNumServicosMedia2020" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblNumServicosMedia2021" runat="server"></asp:Label>
                                             </td>
                                            <td align="center">
                                                   <asp:HyperLink ID="HyperLink6" NavigateUrl="http://www.pmas.sp.gov.br" Target="_blank"
                                                    runat="server" Text="PMASweb"></asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr style="height:30px;">
                                            <td align="left">Serviços socioassistenciais da proteção social especial de alta complexidade</td>
                                               <td align="center">
                                                  Serviços</td>
                                            <td align="center">
                                                            <button id="btnAjudaServicosAltaComplexidade" class="note" onclick="return false;"></button>
                                                        </td>
                                            <td align="right" height="25">
                                                <asp:Label ID="lblNumServicosAlta2018" runat="server"></asp:Label>
                                            </td>
                                            <td align="right"> <asp:Label ID="lblNumServicosAlta2019" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                               <asp:Label ID="lblNumServicosAlta2020" runat="server"></asp:Label>
                                            </td>
                                           <td align ="right">
                                               <asp:Label ID="lblNumServicosAlta2021" runat="server"></asp:Label>
                                           </td>
                                            <td align="center">
                                                  <asp:HyperLink ID="HyperLink7" NavigateUrl="http://www.pmas.sp.gov.br" Target="_blank"
                                                    runat="server" Text="PMASweb"></asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr style="height: 30px;">
                                            <td align="left">
                                                Serviços socioassistenciais não tipificados</td>
                                            <td align="center">
                                                    Serviços
                                            </td>
                                            <td align="center">
                                               <button id="btnAjudaServicoNaoTipificado" class="note" onclick="return false;"></button>
                                            </td>
                                            <td align="right" height="25">
                                                <asp:Label ID="lblServicoNaoTipificados2018" runat="server"></asp:Label>
                                            </td>
                                            <td align="right"> <asp:Label ID="lblServicoNaoTipificados2019" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                               <asp:Label ID="lblServicoNaoTipificados2020" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblServicoNaoTipificados2021" runat="server"></asp:Label>
                                            </td>
                                            <td align="center">
                                                  <asp:HyperLink ID="HyperLink8" NavigateUrl="http://www.pmas.sp.gov.br" Target="_blank"
                                                    runat="server" Text="PMASweb"></asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr style="height: 22px;">
                                            <td align="left">Número de CRAS implantados no Munícipio
                                            </td>
                                             <td align="center">CRAS</td>
                                            <td align="center">
                                                 <button id="btnAjudaCRAS" class="note" onclick="return false;"></button>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblCRASImplantados2018" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">   <asp:Label ID="lblCRASImplantados2019" runat="server"></asp:Label></td>
                                             <td align="right">   <asp:Label ID="lblCRASImplantados2020" runat="server"></asp:Label></td>

                                            <td align="right"> 
                                                <asp:Label ID="lblCRASImplantados2021" runat="server"></asp:Label>
                                            </td>
                                            
                                            <td align="center"> 
                                                   <asp:HyperLink ID="HyperLink4" NavigateUrl="http://www.pmas.sp.gov.br" Target="_blank"
                                                    runat="server" Text="PMASweb"></asp:HyperLink>

                                            </td>
                                        </tr>
                                        <tr style="height: 22px;">
                                            <td align="left">Número de CREAS implantados no Munícipio</td>
                                            <td align="center">CREAS</td>
                                            <td align="center">
                                                 <button id="btnAjudaCREASImplantados" class="note" onclick="return false;"></button>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblCREASImplantados2018" runat="server"></asp:Label>
                                            </td>
                                            <td align="right"> <asp:Label ID="lblCREASImplantados2019" runat="server"></asp:Label></td>
                                            <td align="right"> <asp:Label ID="lblCREASImplantados2020" runat="server"></asp:Label></td>
                                            
                                            <td align="right"> <asp:Label ID="lblCREASImplantados2021" runat="server"></asp:Label></td>

                                           <td  align="center">    <asp:HyperLink ID="HyperLink3" NavigateUrl="http://www.pmas.sp.gov.br" Target="_blank"
                                                    runat="server" Text="PMASweb"></asp:HyperLink>

                                           </td>
                                        </tr>
                                        <tr style="height: 22px;">
                                            <td align="left">Número de Centro Pop Implantados
                                            </td>
                                                 <td align="center">Centros Pop</td>
                                            <td align="center">
                                                   <button id="btnAjudaCentroPOP" class="note" onclick="return false;"></button>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblCentroPOPImplantados2018" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">   <asp:Label ID="lblCentroPOPImplantados2019" runat="server"></asp:Label></td>
                                            <td align="right">   <asp:Label ID="lblCentroPOPImplantados2020" runat="server"></asp:Label></td>
                                            <td align="right">   <asp:Label ID="lblCentroPOPImplantados2021" runat="server"></asp:Label></td>
                                           
                                            <td align="center">    <asp:HyperLink ID="HyperLink5" NavigateUrl="http://www.pmas.sp.gov.br" Target="_blank"
                                                    runat="server" Text="PMASweb"></asp:HyperLink></asp:HyperLink></td>
                                        </tr>
                                        <tr style="height: 22px;">
                                            <td align="left">Beneficiários BPC - Idosos
                                            </td>
                                               <td align="center">Pessoas</td>
                                            <td align="center">
                                                      <button id="btnAjudaBPCIdosos" class="note" onclick="return false;"></button>
                                            </td>
                                            <td align="right"><asp:Label ID="lblBPCIdosos2018" runat="server"></asp:Label></td>
                                            <td align="right"><asp:Label ID="lblBPCIdosos2019" runat="server"></asp:Label></td>
                                            <td align="right"><asp:Label ID="lblBPCIdosos2020" runat="server"></asp:Label></td>
                                            <td align="right"><asp:Label ID="lblBPCIdosos2021" runat="server"></asp:Label></td>

                                            <td align="center"> 
                                                    <asp:HyperLink ID="HyperLink2" NavigateUrl="http://www.pmas.sp.gov.br" Target="_blank" runat="server" Text="PMASweb"></asp:HyperLink>
                                            </td>

                                        </tr>
                                        <tr style="height: 22px;">
                                            <td align="left">Beneficiários BPC - Pessoas com deficiência
                                            </td>
                                              <td align="center">Pessoas</td>
                                            <td align="center">
                                                      <button id="btnAjudaBPCDeficiencias" class="note" onclick="return false;"></button>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblBPCDeficentes2018" runat="server"></asp:Label>
                                            </td>
                                            <td align="right">   <asp:Label ID="lblBPCDeficentes2019" runat="server"></asp:Label></td>
                                             <td align="right">   <asp:Label ID="lblBPCDeficentes2020" runat="server"></asp:Label></td>
                                            <td align="right">   <asp:Label ID="lblBPCDeficentes2021" runat="server"></asp:Label></td>

                                            <td align="center"> 
                                                
                                                    <asp:HyperLink ID="HyperLink1" NavigateUrl="http://www.pmas.sp.gov.br" Target="_blank" runat="server" Text="PMASweb"></asp:HyperLink>

                                            </td>
                                        </tr>
                                      </tbody>
                                    </table>
                                        </div>
                                    <div class="row">
                          <div class="cell">
                                  <strong> Este quadro permite ao município elaborar uma análise sobre a rede de proteção social disponível e as demandas por proteção social dos territórios,
                               aproveitando os indicadores aqui apresentados e outros considerados relevantes. 
                              <br />É importante considerar a quantidade, diversidade e qualidade da oferta de serviços, projetos, programas e benefícios socioassistenciais, bem como eventuais necessidades de reordenamento e adequação dos serviços e atendimento à população.
                              <br />  A perspectiva da política pública supõe a identificação de demandas na direção da universalização do acesso e da construção de respostas concretas a estas demandas, que alcancem a todos.</strong>
                              <br />
                              <br />
                                    <asp:TextBox Width="100%" ID="txtCaracterizacao" runat="server" TextMode="MultiLine"
                                        Height="302px" MaxLength="3000"></asp:TextBox>
                                    <br />
                                    <skm:textboxcounter ID="NameCounter" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 4000 caracteres."
                                        Font-Bold="True" TextBoxControlId="txtCaracterizacao" maxcharacterlength="3000" />
                                    <br />
                                    <asp:button ID="btnSalvarCaracterizacao" runat="server" SkinID="button-save" Width="89px"
                                        Text="Salvar" onClick="btnSalvarCaracterizacao_Click" />
                           </div>
                                    </div>

                                    </div>
                                </div>
                               </div>
                        </div>
                      </div>
                 </form>
             <table width="100%" align="center" class="ui-text">
                 <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="FPopulacaoVulnerabilidade.aspx">
                          <span class="mif-arrow-left"></span>
                            Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="FSituacaoVulnerabilidade.aspx">Próximo
                           <span class="mif-arrow-right"></span></a>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdfId" runat="server" Value="0" />
        </contenttemplate>
    <script type="text/javascript">
        $(function () {
            $('#btnAjudaServicosSocioassistenciais').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Nº de serviços socioassistenciais PSB', content: "Quantidade de serviços socioassistenciais do nível de proteção social básica que estavam em funcionamento nos anos de 2018 a 2021 (referência: dezembro)." });
                }, 500);
            });
            $('#btnAjudaServicosMediaComplexidade').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Nº de serviços socioassistenciais PSE Média Complexidade', content: "Quantidade de serviços socioassistenciais do nível de proteção social especial de média complexidade que estavam em funcionamento nos anos de 2018 a 2021 (referência: dezembro)." });
                }, 500);
            });
            $('#btnAjudaServicosAltaComplexidade').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Nº de serviços socioassistenciais PSE Alta Complexidade', content: "Quantidade de serviços socioassistenciais do nível de proteção social especial de alta complexidade que estavam em funcionamento nos anos de 2018 a 2021 (referência: dezembro)." });
                }, 500);
            });
            $('#btnAjudaServicoNaoTipificado').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Nº de serviços socioassistenciais não tipificados', content: "Quantidade de serviços socioassistenciais de qualquer dos três níveis de proteção social que estavam em funcionamento nos anos de 2018 a 2021 (referência: dezembro), mas que não estavam adequados de acordo com a Tipificação Nacional dos Serviços Socioasssitenciais _ Resolução 109 do Conselho Nacional de Assistência Social (CNAS)." });
                }, 500);
            });
            $('#btnAjudaCRAS').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Nº de CRAS implantados no Município', content: "Quantidade de Centros de Referência da Assistência Social (CRAS) implantados e em funcionamento no município, nos anos de 2018 a 2021 (referência: dezembro)." });
                }, 500);
            });

            $('#btnAjudaCREASImplantados').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Nº de CREAS implantados no Município', content: "Quantidade de Centros de Referência Especializados da Assistência Social (CREAS) implantados e em funcionamento no município, nos anos de 2018 a 2021 (referência: dezembro)." });
                }, 500);
            });
            $('#btnAjudaCentroPOP').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Nº de Centro Pop implantados no Município', content: "Quantidade de Centros de Referência Especializados para População em Situação de Rua (Centro Pop) implantados e em funcionamento no município, nos anos de 2018 a 2021 (referência: dezembro)." });
                }, 500);
            });
            $('#btnAjudaBPCIdosos').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Beneficiários BPC - Idosos', content: "O Benefício de Prestação Continuada da Assistência Social - BPC Idosos assegura a transferência mensal de 1 (um) salário mínimo ao idoso, com 65 anos ou mais, devendo ser comprovado que a pessoa idosa não possui meios de garantir o próprio sustento, nem tê-lo provido por sua família. A renda mensal familiar per capita deve ser inferior a ¼ (um quarto) do salário mínimo vigente." });
                }, 500);
            });
            $('#btnAjudaBPCDeficiencias').click(function () {
                setTimeout(function () {
                    $.Notify({ keepOpen: true, type: 'default', icon: '<span class="\mif-vpn-publ\"></span>"', caption: 'Beneficiários BPC - Pessoas com deficiência', content: 'O Benefício de Prestação Continuada da Assistência Social - BPC Pessoas com deficiência assegura a transferência mensal de 1 (um) salário mínimo à pessoa com deficiência, de qualquer idade, com impedimentos de longo prazo, de natureza física, mental, intelectual ou sensorial, devendo ser comprovado que a pessoa com deficiência não possui meios de garantir o próprio sustento, nem tê-lo provido por sua família. A renda mensal familiar per capita deve ser inferior a ¼ (um quarto) do salário mínimo vigente.' });
                }, 500);
            });
        });
    </script>
</asp:Content>
