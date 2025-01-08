<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FSituacaoVulnerabilidade.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoII.FSituacaoVulnerabilidade" %>

<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <script type="text/javascript">
    
        function exibeBtnExcluir()
        {
            for (var i = 0; i <= 10; i++) {

                $('#MainContent_lstAnaliseDiagnostica_btnExcluir_' + i).show();

            }
        }
        function ocultaBtnExcluir()
        {
            for (var i = 0; i <= 10; i++) {

                $('#MainContent_lstAnaliseDiagnostica_btnExcluir_' + i).hide();

            }
        }
        
    </script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmSituacaoVulnerabilidade">
                <div class="accordion" data-role="accordion" data-close-any="true">
                    <div class="frame active">
                        <div class="heading">
                            2.4 - Situações de vulnerabilidade e/ou risco existentes no município</b>
                                 </span><a style="float: right; margin-right: 5%;" href="#" runat="server" id="linkAlteracoesQuadro15" visible="false">
                                     <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado</a>
                        </div>
                        <div class="formInput" data-text="Vulnerabilidades">
                            <div class="grid">

                                    <div id="Quadrienal">
                                        <asp:Button ID="btnExercicio1" runat="server" Width="113px" OnClick="btnExercicio1_Click"></asp:Button>
                                        <asp:Button ID="btnExercicio2" runat="server" Width="113px" OnClick="btnExercicio2_Click"></asp:Button>
                                        <asp:Button ID="btnExercicio3" runat="server" Width="113px" OnClick="btnExercicio3_Click"></asp:Button>
                                        <asp:Button ID="btnExercicio4" runat="server" Width="113px" OnClick="btnExercicio4_Click"></asp:Button>
                                    </div>

                                <div class="row">
                                    <div class="cell">
                                        <b>Indique se existe no município algum dos povos e comunidades tradicionais listados abaixo:</b>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <asp:CheckBox ID="chkCigano" Text="Ciganos" runat="server" OnCheckedChanged="chkCigano_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                    </div>
                                </div>
                                <div class="row left30" id="trQtdCiganos" runat="server" visible="false">
                                    Informe quantas famílias existem nesta comunidade tradicional:
                                        <div class="input-control text">
                                            <asp:TextBox ID="txtNumeroCiganos" runat="server"></asp:TextBox>
                                        </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <asp:CheckBox ID="chkExtrativistas" Text="Extrativistas" runat="server" OnCheckedChanged="chkExtrativistas_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                    </div>
                                </div>
                                <div class="row left30" id="trQtdExtrativistas" runat="server" visible="false">
                                    Informe quantas famílias existem nesta comunidade tradicional:
                                    <div class="input-control text">
                                        <asp:TextBox ID="txtNumeroExtrativistas" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <asp:CheckBox ID="chkPescadores" Text="Pescadores Artesanais" runat="server" OnCheckedChanged="chkPescadores_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                    </div>
                                </div>
                                <div class="row left30" id="trQtdPescadores" runat="server" visible="false">
                                    Informe quantas famílias existem nesta comunidade tradicional: 
                                    <div class="input-control text">
                                        <asp:TextBox ID="txtNumeroPescadores" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <asp:CheckBox ID="chkAfro" Text="Comunidade tradicional de matriz africana" runat="server" OnCheckedChanged="chkAfro_CheckedChanged" AutoPostBack="true" Enabled="false"/>
                                    </div>
                                </div>
                                <div class="row left30" id="trQtdAfros" runat="server" visible="false">
                                    Informe quantas famílias existem nesta comunidade tradicional:
                                      <div class="input-control text">
                                          <asp:TextBox ID="txtNumeroAfro" runat="server"></asp:TextBox>
                                      </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <asp:CheckBox ID="chkRibeirinha" Text="Comunidade ribeirinha" runat="server" OnCheckedChanged="chkRibeirinha_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                    </div>
                                </div>
                                <div class="row left30" id="trQtdRibeirinha" runat="server" visible="false">
                                    Informe quantas famílias existem nesta comunidade tradicional:
                                      <div class="input-control text">
                                          <asp:TextBox ID="txtNumeroRibeirinha" runat="server"></asp:TextBox>
                                      </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <asp:CheckBox ID="chkIndigenas" Text="Indígenas" runat="server" OnCheckedChanged="chkIndigenas_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                    </div>
                                </div>
                                <div class="row left30" id="trQtdIndigenas" runat="server" visible="false">
                                    Informe quantas famílias existem nesta comunidade tradicional:
                                      <div class="input-control text">
                                          <asp:TextBox ID="txtNumeroIndigenas" runat="server"></asp:TextBox>
                                      </div>
                                </div>
                                     <div class="row">
                                    <div class="cell">
                                        <asp:CheckBox ID="chkQuilombolas" Text="Quilombolas" runat="server" OnCheckedChanged="chkQuilombolas_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                    </div>
                                </div>
                                <div class="row left30" id="trQtdQuilombolas" runat="server" visible="false">
                                    Informe quantas famílias existem nesta comunidade tradicional:
                                      <div class="input-control text">
                                          <asp:TextBox ID="txtNumeroQuilombolas" runat="server"></asp:TextBox>
                                      </div>
                                </div>

                                <div class="row">
                                    <div class="cell">
                                        <asp:CheckBox ID="chkNaoExisteComunidade" Text="Não existe no município nenhuma das comunidades citadas" runat="server" OnCheckedChanged="chkNaoExisteComunidade_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <b>Indique se existe no município algum dos grupos específicos listados abaixo:</b>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <asp:CheckBox ID="chkAgricultores" Text="Agricultores familiares" runat="server" OnCheckedChanged="chkAgricultores_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                    </div>
                                </div>
                                <div class="row left30" id="trQtdAgricultores" runat="server" visible="false">
                                    Informe quantas famílias existem neste grupo específico:
                                    <div class="input-control text">
                                        <asp:TextBox ID="txtNumeroAgricultores" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <asp:CheckBox ID="chkAcampamentos" Text="Acampamentos" runat="server" OnCheckedChanged="chkAcampamentos_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                    </div>
                                </div>
                                <div class="row left30" id="trQtdAcampamentos" runat="server" visible="false">
                                    Informe quantas famílias existem neste grupo específico:
                                      <div class="input-control text">
                                          <asp:TextBox ID="txtNumeroAcampamentos" runat="server"></asp:TextBox>
                                      </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <asp:CheckBox ID="chkInstalacaoPrisional" Text="População flutuante decorrente de instalação prisional" runat="server" OnCheckedChanged="chkInstalacaoPrisional_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                    </div>
                                </div>
                                <div class="row left30" id="trQtdInstalacaoPrisional" runat="server" visible="false">
                                    Informe quantas famílias existem neste grupo específico:
                                        <div class="input-control text">
                                            <asp:TextBox ID="txtNumeroInstalacaoPrisional" runat="server"></asp:TextBox>
                                        </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <asp:CheckBox ID="chkTrabalhoSazonal" Text="Trabalhadores sazonais" runat="server" OnCheckedChanged="chkTrabalhoSazonal_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                    </div>
                                </div>
                                <div class="row left30" id="trQtdTrabalhoSazonal" runat="server" visible="false">
                                    Informe quantas famílias existem neste grupo específico: 
                                    <div class="input-control text">
                                        <asp:TextBox ID="txtNumeroTrabalhoSazonal" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <asp:CheckBox ID="chkAglomerado" Text="Aglomerados subnormais" runat="server" OnCheckedChanged="chkAglomerado_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                    </div>
                                </div>
                                <div class="row left30" id="trQtdAglomerado" runat="server" visible="false">
                                    Informe quantas famílias existem neste grupo específico:
                                      <div class="input-control text">
                                          <asp:TextBox ID="txtNumeroAglomerado" runat="server"></asp:TextBox>
                                      </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <asp:CheckBox ID="chkOutroAssentamento" Text="Assentamentos precários e/ou irregulares" runat="server" OnCheckedChanged="chkOutroAssentamento_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                    </div>
                                </div>
                                <div class="row left30" id="trQtdOutroAssentamento" runat="server" visible="false">
                                    <div class="cell">
                                        Informe quantas famílias existem neste grupo específico:
                                      <div class="input-control text">
                                          <asp:TextBox ID="txtNumeroOutroAssentamento" runat="server"></asp:TextBox>
                                      </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <asp:CheckBox ID="chkNaoExisteComunidadeCitada" Text="Não existe no município nenhum dos grupos citados" runat="server" OnCheckedChanged="chkNaoExisteComunidadeCitada_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <asp:Button ID="btnSalvarComunidades" runat="server" SkinID="button-save" Width="89px"
                                            Text="Salvar" Enabled="false" AutoPostBack="true" OnClick="btnSalvarComunidades_Click"/>
                                        <%--<asp:Button ID="Button1" runat="server" SkinID="button-save" Width="89px"
                                            Text="Salvar" Enabled="false" OnClick="btnSalvarComunidades_Click" AutoPostBack="true" />--%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <strong>Com base na realidade vivenciada pelas equipes nos serviços, o município deve apontar até dez situações de maior vulnerabilidade ou risco presentes nos territórios, considerando para tanto a demanda gerada (número de usuários, quantidade de atendimento, entre outros), e a gravidade das situações existentes, entre outros fatores.</strong>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <b>Situações de vulnerabilidade ou risco social:</b>
                                        <br />
                                        <div class="input-control select full-size">
                                            <asp:DropDownList ID="ddlSituacaoVulnerabilidade" runat="server" AutoPostBack="True" >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <b>Classificação:</b>
                                            <asp:ImageButton ID="btnAjudaClassificacao" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Styles/Icones/help.png" OnClientClick="return false;" />
                                            <asp:Panel ID="pnlAjudaClassificacao" runat="server" CssClass="ajuda" Height="110px" Width="400px">
                                                <div style="float: right;">
                                                    <asp:LinkButton ID="lnkCloseAjudaClassificacao" runat="server" OnClientClick="return false;" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" Text="X" ToolTip="Fechar" />
                                                </div>
                                                <div>
                                                    <p>
                                                        As situações devem ser classificadas de 1 até 10 segundo uma escala descendente de intensidade com que ela se apresenta em seu Município. Assim, no caso de serem enumeradas 10 situações, a mais grave deve ser classificada com o nível 1 e a menos grave com o nível 10.
                                                    </p>
                                                </div>
                                            </asp:Panel>
                                            <ajaxToolkit:AnimationExtender ID="OpenAnimationClassificacao" runat="server" TargetControlID="btnAjudaClassificacao">
                                                <Animations>
                                        <OnClick>
                                            <Sequence AnimationTarget="pnlAjudaClassificacao">
                                            <EnableAction AnimationTarget="btnAjudaClassificacao" Enabled="false" />
                                            <StyleAction Attribute="display" Value="block" />                                    
                                            <Parallel>
                                                <FadeIn Duration="1" Fps="20" />                                    
                                            </Parallel>
                                            </Sequence>
                                        </OnClick>
                                                </Animations>
                                            </ajaxToolkit:AnimationExtender>
                                            <ajaxToolkit:AnimationExtender ID="CloseAnimationAjudaClassificacao" runat="server" TargetControlID="lnkCloseAjudaClassificacao">
                                                <Animations>
                                                    <OnClick>
                                                        <Sequence AnimationTarget="pnlAjudaClassificacao">                                                    
                                                            <Parallel Duration=".3" Fps="15">                                                        
                                                                <FadeOut />
                                                            </Parallel>                        
                                                            <%--  Reset the sample so it can be played again --%>
                                                            <StyleAction Attribute="display" Value="none"/>                                                                                                       
                        
                                                            <%--  Enable the button so it can be played again --%>
                                                            <EnableAction AnimationTarget="btnAjudaClassificacao" Enabled="true" />
                                                        </Sequence>
                                                    </OnClick>
                                                    <OnMouseOver>
                                                        <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                                                    </OnMouseOver>
                                                    <OnMouseOut>
                                                        <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                                                    </OnMouseOut>
                                                </Animations>
                                            </ajaxToolkit:AnimationExtender>
                                            <br />
                                            <div class="input-control select">
                                                <asp:DropDownList ID="ddlClassificacao" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <td align="left"><b>Demanda estimada no município:<asp:ImageButton ID="btnAjudaDemanda" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Styles/Icones/help.png" OnClientClick="return false;" />
                                            </b>
                                                <asp:Panel ID="pnlAjudaDemanda" runat="server" CssClass="ajuda" Height="270px" Width="400px">
                                                    <div style="float: right;">
                                                        <asp:LinkButton ID="lnkCloseAjudaDemanda" runat="server" OnClientClick="return false;" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" Text="X" ToolTip="Fechar" />
                                                    </div>
                                                    <div>
                                                        <p>
                                                            Entende-se por demanda a existência de necessidades que exigem intervenções de natureza socioassistencial. Essas demandas podem ser apresentadas espontaneamente pelos usuários ou identificadas pelo exercício da vigilância socioassistencial no município.
                                                                    <br />
                                                            O ideal é que a estimativa da demanda seja baseada num prévio mapeamento das vulnerabilidades e riscos que se apresentam nos territórios, e sua quantificação, o que servirá para um planejamento mais seguro das ações que devem ser realizadas.
                                                                    <br />
                                                            De qualquer forma, por demanda estimada entende-se, aqui, a indicação quantitativa do número de pessoas que estejam vivenciando as situações de vulnerabilidade ou risco apontadas neste quadro.
                                                        </p>
                                                    </div>
                                                </asp:Panel>
                                                <ajaxToolkit:AnimationExtender ID="OpenAnimationDemanda" runat="server" TargetControlID="btnAjudaDemanda">
                                                    <Animations>
                                        <OnClick>
                                            <Sequence AnimationTarget="pnlAjudaDemanda">
                                            <EnableAction AnimationTarget="btnAjudaDemanda" Enabled="false" />
                                            <StyleAction Attribute="display" Value="block" />                                    
                                            <Parallel>
                                                <FadeIn Duration="1" Fps="20" />                                    
                                            </Parallel>
                                            </Sequence>
                                        </OnClick>
                                                    </Animations>
                                                </ajaxToolkit:AnimationExtender>
                                                <ajaxToolkit:AnimationExtender ID="CloseAnimationDemanda" runat="server" TargetControlID="lnkCloseAjudaDemanda">
                                                    <Animations>
                                                    <OnClick>
                                                        <Sequence AnimationTarget="pnlAjudaDemanda">                                                    
                                                            <Parallel Duration=".3" Fps="15">                                                        
                                                                <FadeOut />
                                                            </Parallel>                        
                                                            <%--  Reset the sample so it can be played again --%>
                                                            <StyleAction Attribute="display" Value="none"/>                                                                                                       
                        
                                                            <%--  Enable the button so it can be played again --%>
                                                            <EnableAction AnimationTarget="btnAjudaDemanda" Enabled="true" />
                                                        </Sequence>
                                                    </OnClick>
                                                    <OnMouseOver>
                                                        <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                                                    </OnMouseOver>
                                                    <OnMouseOut>
                                                        <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                                                    </OnMouseOut>
                                                    </Animations>
                                                </ajaxToolkit:AnimationExtender>
                                                <br />
                                                <div class="input-control text full-size">
                                                    <asp:TextBox ID="txtDemanda" runat="server" Width="80px" />
                                                </div>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtendertxtDemanda" runat="server" FilterType="Numbers" TargetControlID="txtDemanda" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:Button ID="btnSalvar" runat="server" Enabled="false" OnClick="btnSalvar_Click" SkinID="button-add" Text="Adicionar" />
                                            
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:ListView ID="lstAnaliseDiagnostica" OnItemDataBound="lstAnaliseDiagnostica_ItemDataBound" runat="server" DataKeyNames="Id" OnItemCommand="lstAnaliseDiagnostica_ItemCommand">
                                                <LayoutTemplate>
                                                    <table border="0" cellpadding="0" cellspacing="0" class="table striped border bordered">
                                                        <thead class="info">
                                                            <tr>
                                                                <th width="650">Situações de vulnerabilidade ou risco mais graves </th>
                                                                <th width="100">Classificação </th>
                                                                <th width="150">Demanda estimada no município</th>
                                                                <th width="100">Excluir </th>
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
                                                        <td class="align-center">
                                                            <asp:ImageButton ID="btnExcluir" 
                                                                runat="server" 
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
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <table id="tbInconsistencias" runat="server" visible="false" cellspacing="2" cellpadding="0"
                                                width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                                                <tr>
                                                    <td style="padding: 15px 10px 2px 15px">
                                                        <span class="mif-warning mif-2x"></span>
                                                        <%-- <img src="../Styles/Icones/messagebox_warning.png" align="absMiddle" />--%><b style='color: #000000 !important'>Verifique
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
            </form>
            <table width="100%" align="center" class="ui-text">
                <tr>
                    <td width="50%" align="left" style="padding-top: 10px;">
                        <a href="FEvolucaoRedeSocioassistencial.aspx">
                            <span class="mif-arrow-left"></span>
                            Anterior</a>
                    </td>
                    <td width="50%" align="right" style="padding-top: 10px;">
                        <a href="FAnaliseInterpretacao.aspx">Próximo<span class="mif-arrow-right"></span></a>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdfId" runat="server" Value="0" />
            <asp:HiddenField ID="hdfIdComunidade" runat="server" />
            <asp:HiddenField ID="hdfAno" runat="server" Value="2022" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
