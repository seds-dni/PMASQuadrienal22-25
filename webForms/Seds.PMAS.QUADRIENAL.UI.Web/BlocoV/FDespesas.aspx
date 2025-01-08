<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FDespesas.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.BlocoV.FDespesas" %>

<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>
    <script src="../Scripts/dataFormat.js" type="text/javascript"></script>
    <form name="frmDespesas">
        <div class="frame">
            <fieldset class="border-blue">                                                                    
               <legend class="lgnd"><b class="fg-blue">Despesas</b></legend>

                <asp:Label runat="server" ID="lblRecursosHumanos">Recursos humanos</asp:Label>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:TextBox runat="server" ID="txtRecursosHumanos" Width="110px"></asp:TextBox>
                <br /><br />
                <asp:Label runat="server" ID="lblMaterialDeConsumo">Material de consumo</asp:Label>&nbsp&nbsp<asp:TextBox runat="server" ID="txtMaterialDeConsumo" Width="110px"></asp:TextBox>
                <br /><br />
                <asp:Label runat="server" ID="lblOutrasDespesas">Outras Despesas</asp:Label>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:TextBox runat="server" ID="txtOutrasDespesas" Width="110px"></asp:TextBox>
            </fieldset>
            <br />
            <fieldset class="border-blue">
                <legend class="lgnd"><b class="fg-blue">Receitas</b></legend>
                <asp:Label runat="server" ID="lblValorAplicacoesFinanceiras">Aplicações Financeiras</asp:Label>&nbsp&nbsp<asp:TextBox runat="server" ID="txtValorAplicacoesFinanceiras" Width="110px" Text="0,00"></asp:TextBox>
            </fieldset>

        </div>

        <br />
        
        <div class="frame" runat="server" id="divExecucaoFisica">
            <fieldset class="border-blue">                                                                    
                   <legend class="lgnd"><b class="fg-blue">Execução Física</b></legend>

                   <asp:Label runat="server" ID="lblDemandaEstimada" Text="Demanda Estimada"></asp:Label>&nbsp&nbsp<asp:TextBox runat="server" ID="txtDemandaEstimada" MaxLength="10" Width="110px"></asp:TextBox>
                   <br /><br />
                   <asp:Label runat="server" ID="lblNumeroAtendidos" Text="Número Atendidos"></asp:Label>&nbsp&nbsp<asp:TextBox runat="server" ID="txtNumeroAtendidos" MaxLength="10" Width="110px"></asp:TextBox>
                   <br /><br />
                   <asp:Label runat="server" ID="lblDataImplantacao" Text="Data Implantacao" Visible="false"></asp:Label>&nbsp&nbsp<asp:TextBox runat="server" ID="txtDataImplantacao" onkeypress="mascaraData(this)" Width="110px" MaxLength="10" Visible="false"></asp:TextBox>
                   <br /><br />
                   <asp:Label runat="server" ID="lblNaoFoiImplantado" Text="Não Implantado" Visible="false"></asp:Label>&nbsp&nbsp&nbsp<asp:CheckBox ID="chkNaoImplantado" runat="server" Visible="false"/>
                   <br /><br />
                   <asp:Label runat="server" ID="lblQuantidadeAnualBeneficiarios" Text="Quantidade Anual Beneficiários"></asp:Label>&nbsp&nbsp<asp:TextBox runat="server" ID="txtQuantidadeAnualBeneficiarios" MaxLength="10" Width="110px"></asp:TextBox>
                   <br /><br />
                   <asp:Label runat="server" ID="lblQuantidadeAnualBeneficiariosConcedidos" Text="Quantidade Anual de Benefícios Concedidos"></asp:Label>&nbsp&nbsp<asp:TextBox runat="server" ID="txtQuantidadeAnualBeneficiariosConcedidos" MaxLength="10" Width="110px"></asp:TextBox>
            </fieldset>
        </div>
        <div class="cell">
               <asp:Button runat="server" ID="btnSalvar" text="Salvar" OnClick="btnSalvar_Click" />&nbsp&nbsp
               <asp:Button ID="btnVoltar" runat="server" Text="Voltar" PostBackUrl="~/BlocoV/FPrestacaoDeContas.aspx" />
        </div>
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
        <div class="cell">
                     <table id="Table1" runat="server" visible="false" cellspacing="2" cellpadding="0"
                         width="100%" align="center" class="bg-yellow  fg-black" style="border: 1px dashed blue">
                         <tr>
                             <td style="padding: 15px 10px 2px 15px">
                                 <span class="mif-warning mif-2x"></span>
                                 <b style='color: #000000 !important'>Verifique as inconsistências:</b>
                             </td>
                         </tr>
                         <tr>
                             <td style="padding: 10px 10px 12px 45px;">
                                 <asp:Label ID="Label1" ForeColor="Red" runat="server" />
                             </td>
                         </tr>
                     </table>
        </div>
        <asp:HiddenField ID="hdfAno" runat="server" Value="" />
    </form>
</asp:Content>