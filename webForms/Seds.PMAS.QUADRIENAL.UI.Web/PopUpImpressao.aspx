<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUpImpressao.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.PopUpImpressao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table align="center" width="100%" height="100%">
            <tr>
                <td style="HEIGHT: 120px">&nbsp;</td>
            </tr>
            <tr runat="server" id="trReader">
                <td align="center" style="HEIGHT: 22px"><b>Todas as impressões do PMAS estão em 
						formato PDF,<br />
                    se você ainda não tem o Adobe Reader instalado clique <a href="http://ardownload.adobe.com/pub/adobe/reader/win/6.x/6.0/ptb/AdbeRdr60_ptb_full.exe">aqui</a> para fazer o download!</b></td>
            </tr>
            <tr runat="server" id="trGestao" visible="false">
                <td align="center" style="HEIGHT: 22px" class="titulo">
                    <b>Não é possível imprimir o Relatório de Gestão 2016 nesse momento.<br />
                        Para que isso aconteça, o quadro 2 - Execução financeira do Bloco IV deverá estar preenchido e aprovado pelo CMAS.</b>
                </td>
            </tr>
            <tr runat="server" id="trOrgaoGestor" visible="false">
                <td align="center" style="HEIGHT: 22px" class="titulo">
                    <b>Não é possível imprimir o Relatório nesse momento.<br />
                        Para que isso aconteça, no Bloco I - Idenficação, o quadro 3 - Identificação do Orgão Gestor da Assistencia Social deverá ser preenchido.</b>
                </td>
            </tr>
            <tr>
                <td style="HEIGHT: 2px">&nbsp;</td>
            </tr>
            <tr runat="server" id="trImprimir">
                <td valign="top">
                    <table align="center">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnImprimir" runat="server" Text="Visualizar"
                                    OnClick="btnImprimir_Click"></asp:Button></td>
                        </tr>
                        <tr>
                            <td style="HEIGHT: 2px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="titulo">Obs: Se você não conseguir abrir o arquivo PDF faça o seguinte:<br>
                                <ul>
                                    <li>Salve o arquivo no seu computador
                                    </li>
                                    <li>Clique no arquivo salvo com o botão direito do mouse
											<li>Clique em renomear e retire " .htm " e coloque " .pdf "
                                            </li>
                                        <li>Agora você poderá visualizar e imprimir.</li>
                                </ul>

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
