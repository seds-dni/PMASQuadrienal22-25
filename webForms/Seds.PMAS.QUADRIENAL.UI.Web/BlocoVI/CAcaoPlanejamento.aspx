<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="CAcaoPlanejamento.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoVI.CAcaoPlanejamento" %>

<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="pnlCadastro" runat="server">
        <ContentTemplate>
            <form name="frmConselhos">
                <div class="accordion">
                    <div class="frame active">
                        <div class="heading">
                            <b>6.2- Ação planejada pelo Órgão Gestor Municipal</b>
                            <a href="#" runat="server" id="linkAlteracoesQuadro60" visible="false">
                                <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado
                            </a>&nbsp;
                        </div>
                        <div class="content">
                            <div class="formInput" data-text="ações planejadas">
                                <div class="grid">
                                    <div class="row">
                                    </div>

                                    <fieldset>
                                        <div class="row">
                                            <div class="cell" style="padding: 10px;">
                                                Considerando as principais situações de vulnerabilidade existentes no município e as metas dos Planos e Pactos a serem atingidas
                         pela política de Assistência Social, registre a seguir as principais ações planejadas pelo Órgão Gestor municipal a serem 
                        realizadas no próximo ano. Este registro deverá ser feito de acordo com o eixo para o qual cada ação estará voltada, podendo ser:
                        <br />
                                                Eixo 1) Aprimoramento da Gestão Municipal 
                        <br />
                                                Eixo 2) Gestão da Rede de proteção Social
                        <br />
                                                Eixo 3) Implementação do Controle Social 
                        <br />
                                                <br />
                                                Caso não haja nenhuma ação planejada para o próximo ano, deverá ser registrada uma justificativa a este respeito.
                                            </div>
                                        </div>
                                    </fieldset>
                                    <div class="row">
                                        <div class="cell auto-size">
                                            <tr>
                                                <td align="left">
                                                    <asp:ListView ID="lst" runat="server" OnItemCommand="lst_ItemCommand">
                                                        <LayoutTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="0" class="table striped border bordered">
                                                                <thead class="info">
                                                                    <tr>
                                                                        <th width="60">Visualizar/<br />
                                                                            Editar </th>
                                                                        <th width="350">Identificação da Ação </th>
                                                                        <th width="250">Nome da Ação </th>

                                                                        <th width="100">Período de realização<br />
                                                                            desta ação </th>
                                                                        <th width="40">Status<br />
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
                                                                <td colspan="5"><b>Eixo:</b> <%#DataBinder.Eval(Container.DataItem, "Key") %></td>
                                                            </tr>
                                                            <asp:ListView ID="lstItems" runat="server" DataSource='<%#Eval("Items") %>' OnItemDataBound="lstItems_ItemDataBound" DataKeyNames="Id">
                                                                <LayoutTemplate>
                                                                    <tr id="itemPlaceholder" runat="server">
                                                                    </tr>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td align="center"><a href='FAcaoPlanejamento.aspx?id=<%# Server.UrlEncode(Genericos.clsCrypto.Encrypt(DataBinder.Eval(Container.DataItem, "Id").ToString())) %>&amp;idCentro=<%=Request.QueryString["id"]%>'>
                                                                            <img  runat="server" id="btnImg"  Visible='<%#((Int32)DataBinder.Eval(Container.DataItem, "anoInicial")) >= 2021 ? true : false %>' src="../Styles/Icones/find.png" alt="Visualizar Ação" border="0" />
                                                                        </a></td>
                                                                        <td><%#DataBinder.Eval(Container.DataItem, "Identificacao") %></td>
                                                                        <td><%#DataBinder.Eval(Container.DataItem, "Nome") %></td>
                                                                        <td align="center"><%# DataBinder.Eval(Container.DataItem, "PrevisaoExecucao") %></td>
                                                                        <td align="center">
                                                                            <div style="z-index: 1; text-align:center;">
                                                                                <asp:Panel style=" position: relative; float: none;" runat="server" id="pnlEditarSituacao">
                                                                                    <image src="../Styles/Icones/edit.png" id="btnEditarSituacao"  onclick='$("#painel_edit_<%# DataBinder.Eval(Container.DataItem, "Id")%>").toggle()' />
                                                                                </asp:Panel>
                                                                                <asp:Panel style="position: relative; float: none; margin-left: 10px;" runat="server" id="pnlExibirSituacao">
                                                                                    <image src="../Styles/Icones/find.png" id="btnExibirSituacao" hidden onclick='$("#painel_edit_<%# DataBinder.Eval(Container.DataItem, "Id")%>").toggle()' />
                                                                                </asp:Panel>
                                                                            </div>

                                                                            <asp:Panel ID="painel_edit" runat="server">
                                                                                <div id="painel_edit_<%# DataBinder.Eval(Container.DataItem, "Id")%>"
                                                                                    style="width: 220px; height: 350px; margin: 20px 0px 0px 0px; background-color: #fff; display: none; border: 1px solid #aaa; position: absolute; clear: both; box-shadow: -2px 2px #ccc8c8; z-index: 3">
                                                                                    <div style="width: 50px; height: 50px; position: absolute; right: 5px; top: 10px; font-size: 12px;" onclick="$('#painel_edit_<%# DataBinder.Eval(Container.DataItem, "Id")%>').hide()">Fechar</div>
                                                                                    <p style="color: red; float: left; padding: 5px;">Selecione um status:</p>
                                                                                    <div style="width: 120px; height: 30px; background-color: #fff; position: relative; clear: both; padding: 5px; text-align: left;">
                                                                                        <asp:RadioButton ID="rdSituacao1" GroupName="GNSituacao" Text="Iniciada" Checked='<%# (Int32)DataBinder.Eval(Container.DataItem, "Situacao") == 1 ? true : false %>' runat="server" />
                                                                                    </div>
                                                                                    <div style="width: 120px; height: 30px; background-color: #fff; position: relative; clear: both; padding: 5px; text-align: left;">
                                                                                        <asp:RadioButton ID="rdSituacao2" GroupName="GNSituacao" Text="Finalizada" Checked='<%# (Int32)DataBinder.Eval(Container.DataItem, "Situacao") == 2 ? true : false %>' runat="server" />
                                                                                    </div>
                                                                                    <div style="width: 120px; height: 30px; background-color: #fff; position: relative; clear: both; padding: 5px; text-align: left;">
                                                                                        <asp:RadioButton ID="rdSituacao3" GroupName="GNSituacao" Text="Adiada"  Checked='<%# (Int32)DataBinder.Eval(Container.DataItem, "Situacao") == 3 ? true : false %>'  runat="server"/>
                                                                                    </div>
                                                                                    <div style="width: 120px; height: 30px; background-color: #fff; position: relative; clear: both; padding: 5px; text-align: left;">
                                                                                        <asp:RadioButton ID="rdSituacao4" GroupName="GNSituacao" Text="Cancelada" Checked='<%# (Int32)DataBinder.Eval(Container.DataItem, "Situacao") == 4 ? true : false %>' runat="server" />
                                                                                    </div>
                                                                                    <br />
                                                                                    <div style="clear: both; padding: 5px;">
                                                                                        <p style="font-weight: bolder; float: left; padding: 5px;">Comentários:</p>
                                                                                        <asp:TextBox runat="server" Text='<%# (String)DataBinder.Eval(Container.DataItem, "SituacaoComentario") %>' TextMode="multiline" Rows="4" Columns="20" Style="width: 200px" MaxLength="2000" ID="txtAreaSituacaoComentario"></asp:TextBox>
                                                                                    </div>
                                                                                    <p style="color: green; float: right; padding: 5px;">
                                                                                        <asp:Button runat="server" ID="btnSalvar" Text="salvar" OnClick="btnSalvarSituacao_Click" CommandName="StatusCommand" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>'
                                                                                            Style="background-color: #4CAF50; border: none; color: white; padding: 5px 32px; text-align: center; text-decoration: none; display: inline-block; font-size: 16px;" />
                                                                                    </p>

                                                                                </div>
                                                                            </asp:Panel>


                                                                        </td>

                                                                        <td class="align-center">

                                                                            <asp:ImageButton ID="btnExcluir"
                                                                                runat="server"
                                                                                CausesValidation="false"
                                                                                CommandArgument='<%#((Int32)DataBinder.Eval(Container.DataItem, "anoInicial")) >= 2023 ? DataBinder.Eval(Container.DataItem, "Id") : -1 %>'
                                                                                CommandName="Excluir"
                                                                                ImageUrl="~/Styles/Icones/editdelete.png"
                                                                                Visible='<%#((Int32)DataBinder.Eval(Container.DataItem, "anoInicial")) >= 2023 ? true : false %>'
                                                                                OnClientClick="return confirm('Deseja realmente remover a ação?');" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                
                                                            </asp:ListView>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <EmptyDataTemplate>
                                                            <div align="center" style="width: 100%;">
                                                                <br />
                                                                <b class="titulo">Não existe registro de ações planejadas para o próximo exercício.</b>
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        
                                                    </asp:ListView>
                                                </td>
                                            </tr>
                                        </div>
                                    </div>
                                    <div class="row" id="trjustificativa" runat="server" visible="false">
                                        <fieldset class="border-blue">
                                            <legend class="lgnd"><b class="fg-blue">Justifique:</b></legend>
                                            <div class="row">
                                                <div class="cell" align="center">
                                                    <asp:TextBox Width="100%" ID="txtJustificativa" runat="server" TextMode="MultiLine"
                                                        Height="200px" MaxLength="2000"></asp:TextBox>
                                                    <br />
                                                    <skm:TextBoxCounter ID="txtCounterJustificativa" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 2000 caracteres."
                                                        Font-Bold="True" TextBoxControlId="txtJustificativa" MaxCharacterLength="2000" />
                                                </div>
                                                <div class="row">
                                                    <div class="cell">
                                                        <asp:Button ID="btnSalvarJusitificativa" runat="server" Text="Salvar" SkinID="button-save"
                                                            Width="80px" OnClick="btnSalvarJusitificativa_Click" />
                                                        <asp:Button ID="btnCancelarJustificativa" runat="server" Text="Cancelar" SkinID="button-save"
                                                            Width="80px" OnClientClick="return confirm('Deseja realmente apagar a justificativa?');" OnClick="btnCancelarJustificativa_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
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
                                    <div class="row">
                                        <div class="cell">
                                            <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar Ação" SkinID="button-save"
                                                Width="186px" PostBackUrl="~/BlocoVI/FAcaoPlanejamento.aspx" /></td>
                                            <asp:Button ID="btnJustificar" runat="server" Text="Justificar" SkinID="button-save"
                                                Width="186px" OnClick="btnJustificar_Click" /></td>
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
                        <a href="FDiretrizes.aspx"><span class="mif-arrow-left" />Anterior</a>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
