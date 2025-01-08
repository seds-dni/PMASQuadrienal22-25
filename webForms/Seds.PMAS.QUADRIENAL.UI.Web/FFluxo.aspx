<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FFluxo.aspx.cs" Inherits="Seds.PMAS.QUADRIENAL.UI.Web.FFluxo" %>

<%@ Register Assembly="skmControls2" Namespace="skmControls2" TagPrefix="skm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/Util.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            CalculateTotal();
            CalculateTotal2();
            CalculateTotal3();
            CalculateTotal4();
        });

        function CalculateTotal() {
                var txtProtecaoSocialBasica = document.getElementById('<%=txtProtecaoSocialBasica.ClientID%>').value;
                var txtProtecaoSocialMedia = document.getElementById('<%=txtProtecaoSocialMedia.ClientID%>').value;
                var txtProtecaoSocialAlta = document.getElementById('<%=txtProtecaoSocialAlta.ClientID%>').value;
                var txtBeneficiosEventuais = document.getElementById('<%=txtBeneficiosEventuais.ClientID%>').value;
                var txtSaoPauloSolidario = document.getElementById('<%=txtSaoPauloSolidario.ClientID%>').value;

                if (txtProtecaoSocialBasica == '') { document.getElementById('<%=txtProtecaoSocialBasica.ClientID%>').value = '0,00'; txtProtecaoSocialBasica = '0,00' }
                if (txtProtecaoSocialMedia == '') { document.getElementById('<%=txtProtecaoSocialMedia.ClientID%>').value = '0,00'; txtProtecaoSocialMedia = '0,00'; }
                if (txtProtecaoSocialAlta == '') { document.getElementById('<%=txtProtecaoSocialAlta.ClientID%>').value = '0,00'; txtProtecaoSocialAlta = '0,00'; }
                if (txtBeneficiosEventuais == '') { document.getElementById('<%=txtBeneficiosEventuais.ClientID%>').value = '0,00'; txtBeneficiosEventuais = '0,00'; }
                if (txtSaoPauloSolidario == '') { document.getElementById('<%=txtSaoPauloSolidario.ClientID%>').value = '0,00'; txtSaoPauloSolidario = '0,00'; }

                var valores = [txtProtecaoSocialBasica, txtProtecaoSocialMedia, txtProtecaoSocialAlta, txtBeneficiosEventuais, txtSaoPauloSolidario];

                PageMethods.CalcularValores(valores, function (val) {
                    document.getElementById('<%=lblTotalCofinanciamento.ClientID%>').innerText = val;
                    document.getElementById('<%=hidTotalRecursos.ClientID%>').value = val;
                });

                var txtProtecaoBasicaReprogramado = document.getElementById('<%=txtProtecaoBasicaReprogramado.ClientID%>').value;
                var txtProtecaoMediaReprogramado = document.getElementById('<%=txtProtecaoMediaReprogramado.ClientID%>').value;
                var txtProtecaoAltaReprogramado = document.getElementById('<%=txtProtecaoAltaReprogramado.ClientID%>').value;
                var txtBeneficiosEventuaisReprogramado = document.getElementById('<%=txtBeneficiosEventuaisReprogramado.ClientID%>').value;
                var txtSaoPauloSolidarioReprogramado = document.getElementById('<%=txtSaoPauloSolidarioReprogramado.ClientID%>').value;
                var txtProtecaoBasicaDemandas = document.getElementById('<%=txtProtecaoBasicaDemandas.ClientID%>').value;
                var txtProtecaoMediaDemandas = document.getElementById('<%=txtProtecaoMediaDemandas.ClientID%>').value;
                var txtProtecaoAltaDemandas = document.getElementById('<%=txtProtecaoAltaDemandas.ClientID%>').value;
                var txtBeneficiosEventuaisDemandas = document.getElementById('<%=txtBeneficiosEventuaisDemandas.ClientID%>').value;
                var txtSaoPauloSolidarioDemandas = document.getElementById('<%=txtSaoPauloSolidarioDemandas.ClientID%>').value;
                var txtProtecaoBasicaReprogramadoDemandas = document.getElementById('<%=txtProtecaoBasicaReprogramacaoDemandas.ClientID%>').value;
                var txtProtecaoMediaReprogramadoDemandas = document.getElementById('<%=txtProtecaoMediaReprogramacaoDemandas.ClientID%>').value;
                var txtProtecaoAltaReprogramadoDemandas = document.getElementById('<%=txtProtecaoAltaReprogramacaoDemandas.ClientID%>').value;
                var txtBeneficiosEventuaisReprogramadoDemandas = document.getElementById('<%=txtBeneficiosEventuaisDemandasReprogramado.ClientID%>').value;

                if (txtProtecaoBasicaReprogramado == '') { document.getElementById('<%=txtProtecaoBasicaReprogramado.ClientID%>').value = '0,00'; txtProtecaoBasicaReprogramado = '0,00'; }
                if (txtProtecaoMediaReprogramado == '') { document.getElementById('<%=txtProtecaoMediaReprogramado.ClientID%>').value = '0,00'; txtProtecaoMediaReprogramado = '0,00'; }
                if (txtProtecaoAltaReprogramado == '') { document.getElementById('<%=txtProtecaoAltaReprogramado.ClientID%>').value = '0,00'; txtProtecaoAltaReprogramado = '0,00'; }
                if (txtBeneficiosEventuaisReprogramado == '') { document.getElementById('<%=txtBeneficiosEventuaisReprogramado.ClientID%>').value = '0,00'; txtBeneficiosEventuaisReprogramado = '0,00'; }
                if (txtSaoPauloSolidarioReprogramado == '') { document.getElementById('<%=txtSaoPauloSolidarioReprogramado.ClientID%>').value = '0,00'; txtSaoPauloSolidarioReprogramado = '0,00'; }
                if (txtProtecaoBasicaDemandas == '') { document.getElementById('<%=txtProtecaoBasicaDemandas.ClientID%>').value = '0,00'; txtProtecaoBasicaDemandas = '0,00'; }
                if (txtProtecaoMediaDemandas == '') { document.getElementById('<%=txtProtecaoMediaDemandas.ClientID%>').value = '0,00'; txtProtecaoMediaDemandas = '0,00'; }
                if (txtProtecaoAltaDemandas == '') { document.getElementById('<%=txtProtecaoAltaDemandas.ClientID%>').value = '0,00'; txtProtecaoAltaDemandas = '0,00'; }
                if (txtBeneficiosEventuaisDemandas == '') { document.getElementById('<%=txtBeneficiosEventuaisDemandas.ClientID%>').value = '0,00'; txtBeneficiosEventuaisDemandas = '0,00'; }
                if (txtSaoPauloSolidarioDemandas == '') { document.getElementById('<%=txtSaoPauloSolidarioDemandas.ClientID%>').value = '0,00'; txtSaoPauloSolidarioDemandas = '0,00'; }
                if (txtProtecaoBasicaReprogramadoDemandas == '') { document.getElementById('<%=txtProtecaoBasicaReprogramacaoDemandas.ClientID%>').value = '0,00'; txtProtecaoBasicaReprogramacaoDemandas = '0,00'; }
                if (txtProtecaoMediaReprogramadoDemandas == '') { document.getElementById('<%=txtProtecaoMediaReprogramacaoDemandas.ClientID%>').value = '0,00'; txtProtecaoMediaReprogramacaoDemandas = '0,00'; }
                if (txtProtecaoAltaReprogramadoDemandas == '') { document.getElementById('<%=txtProtecaoAltaReprogramacaoDemandas.ClientID%>').value = '0,00'; txtProtecaoAltaReprogramacaoDemandas = '0,00'; }
                if (txtBeneficiosEventuaisReprogramadoDemandas == '') { document.getElementById('<%=txtBeneficiosEventuaisDemandasReprogramado.ClientID%>').value = '0,00'; txtBeneficiosEventuaisDemandasReprogramado = '0,00'; }

                var valoresDemandas = [txtProtecaoBasicaDemandas, txtProtecaoMediaDemandas, txtProtecaoAltaDemandas, txtBeneficiosEventuaisDemandas, txtSaoPauloSolidarioDemandas];
                var valoresReprogramados = [txtProtecaoBasicaReprogramado, txtProtecaoMediaReprogramado, txtProtecaoAltaReprogramado, txtBeneficiosEventuaisReprogramado, txtSaoPauloSolidarioReprogramado];
                var valoresReprogramacaoDemandas = [txtProtecaoBasicaReprogramadoDemandas, txtProtecaoMediaReprogramadoDemandas, txtProtecaoAltaReprogramadoDemandas, txtBeneficiosEventuaisReprogramadoDemandas];

                PageMethods.CalcularValores(valoresReprogramados, function (val) {
                    document.getElementById('<%=lblTotalReprogramacao.ClientID%>').innerText = val;
                    document.getElementById('<%=hidTotalRecursosReprogramados.ClientID%>').value = val;
                });
                 PageMethods.CalcularValores(valoresDemandas, function (val) {
                document.getElementById('<%=lblDemandas.ClientID%>').innerText = val;
                    document.getElementById('<%=hidTotalRecursosDemandas.ClientID%>').value = val;
                });
                PageMethods.CalcularValores(valoresReprogramacaoDemandas, function (val) {
                document.getElementById('<%=lblReprogramacaoDemandas.ClientID%>').innerText = val;
                     document.getElementById('<%=hidTotalRecursosReprogramadosDemandas.ClientID%>').value = val;
                 });

                PageMethods.CalcularProtecaoBasica(txtProtecaoSocialBasica, txtProtecaoBasicaReprogramado, txtProtecaoBasicaDemandas, txtProtecaoBasicaReprogramadoDemandas, function (val) {
                    document.getElementById('<%=lblTotalBasica.ClientID%>').innerText = val;
                });

                PageMethods.CalcularProtecaoMedia(txtProtecaoSocialMedia, txtProtecaoMediaReprogramado, txtProtecaoMediaDemandas, txtProtecaoMediaReprogramadoDemandas, function (val) {
                    document.getElementById('<%=lblTotalProtecaoMedia.ClientID%>').innerText = val;
                });

                PageMethods.CalcularProtecaoAlta(txtProtecaoSocialAlta, txtProtecaoAltaReprogramado, txtProtecaoAltaDemandas, txtProtecaoAltaReprogramadoDemandas, function (val) {
                    document.getElementById('<%=lblTotalProtecaoAlta.ClientID%>').innerText = val;
                });

            PageMethods.CalcularBeneficios(txtBeneficiosEventuais, txtBeneficiosEventuaisReprogramado, txtBeneficiosEventuaisDemandas, txtBeneficiosEventuaisReprogramadoDemandas, function (val) {
                    document.getElementById('<%=lblTotalBeneficiosEventuais.ClientID%>').innerText = val;
                });

                PageMethods.CalcularSPSolidario(txtSaoPauloSolidario, txtSaoPauloSolidarioReprogramado, function (val) {
                    document.getElementById('<%=lblTotalSaoPauloSolidario.ClientID%>').innerText = val;
                });


                var valoresReprogramados = [txtProtecaoSocialBasica, txtProtecaoSocialMedia, txtProtecaoSocialAlta, txtBeneficiosEventuais, txtSaoPauloSolidario, txtProtecaoBasicaReprogramado
                , txtProtecaoMediaReprogramado, txtProtecaoAltaReprogramado, txtBeneficiosEventuaisReprogramado, txtSaoPauloSolidarioReprogramado, txtProtecaoBasicaDemandas, txtProtecaoMediaDemandas
                , txtProtecaoAltaDemandas, txtBeneficiosEventuaisDemandas, txtSaoPauloSolidarioDemandas, txtProtecaoBasicaReprogramadoDemandas, txtProtecaoMediaReprogramadoDemandas
                , txtProtecaoAltaReprogramadoDemandas, txtBeneficiosEventuaisReprogramadoDemandas]

            
                PageMethods.CalcularValoresReprogramados(valoresReprogramados, function (val) {
                    document.getElementById('<%=lblTotalRecursos.ClientID%>').innerText = val;
                });
        }

            //#region CalculateTotal2
            function CalculateTotal2() {
                    var txtProtecaoSocialBasicaExercicio2 = document.getElementById('<%=txtProtecaoSocialBasicaExercicio2.ClientID%>').value;
                    var txtProtecaoSocialMediaExercicio2 = document.getElementById('<%=txtProtecaoSocialMediaExercicio2.ClientID%>').value;
                    var txtProtecaoSocialAltaExercicio2 = document.getElementById('<%=txtProtecaoSocialAltaExercicio2.ClientID%>').value;
                    var txtBeneficiosEventuaisExercicio2 = document.getElementById('<%=txtBeneficiosEventuaisExercicio2.ClientID%>').value;
                    var txtSaoPauloSolidarioExercicio2 = document.getElementById('<%=txtSaoPauloSolidarioExercicio2.ClientID%>').value;

                    if (txtProtecaoSocialBasicaExercicio2 == '') { document.getElementById('<%=txtProtecaoSocialBasicaExercicio2.ClientID%>').value = '0,00'; txtProtecaoSocialBasicaExercicio2 = '0,00' }
                    if (txtProtecaoSocialMediaExercicio2 == '') { document.getElementById('<%=txtProtecaoSocialMediaExercicio2.ClientID%>').value = '0,00'; txtProtecaoSocialMediaExercicio2 = '0,00'; }
                    if (txtProtecaoSocialAltaExercicio2 == '') { document.getElementById('<%=txtProtecaoSocialAltaExercicio2.ClientID%>').value = '0,00'; txtProtecaoSocialAltaExercicio2 = '0,00'; }
                    if (txtBeneficiosEventuaisExercicio2 == '') { document.getElementById('<%=txtBeneficiosEventuaisExercicio2.ClientID%>').value = '0,00'; txtBeneficiosEventuaisExercicio2 = '0,00'; }
                    if (txtSaoPauloSolidarioExercicio2 == '') { document.getElementById('<%=txtSaoPauloSolidarioExercicio2.ClientID%>').value = '0,00'; txtSaoPauloSolidarioExercicio2 = '0,00'; }

                    var valores = [txtProtecaoSocialBasicaExercicio2, txtProtecaoSocialMediaExercicio2, txtProtecaoSocialAltaExercicio2, txtBeneficiosEventuaisExercicio2, txtSaoPauloSolidarioExercicio2];

                    PageMethods.CalcularValores(valores, function (val) {
                        document.getElementById('<%=lblTotalCofinanciamentoExercicio2.ClientID%>').innerText = val;
                        document.getElementById('<%=hidTotalRecursosExercicio2.ClientID%>').value = val;
                    });
                    //#endregion

                    var txtProtecaoBasicaReprogramadoExercicio2 = document.getElementById('<%=txtProtecaoBasicaReprogramadoExercicio2.ClientID%>').value;
                    var txtProtecaoMediaReprogramadoExercicio2 = document.getElementById('<%=txtProtecaoMediaReprogramadoExercicio2.ClientID%>').value;
                    var txtProtecaoAltaReprogramadoExercicio2 = document.getElementById('<%=txtProtecaoAltaReprogramadoExercicio2.ClientID%>').value;
                    var txtBeneficiosEventuaisReprogramadoExercicio2 = document.getElementById('<%=txtBeneficiosEventuaisReprogramadoExercicio2.ClientID%>').value;
                    var txtSaoPauloSolidarioReprogramadoExercicio2 = document.getElementById('<%=txtSaoPauloSolidarioReprogramadoExercicio2.ClientID%>').value;
                    var txtProtecaoBasicaDemandasExercicio2 = document.getElementById('<%=txtProtecaoBasicaDemandasExercicio2.ClientID%>').value;
                    var txtProtecaoMediaDemandasExercicio2 = document.getElementById('<%=txtProtecaoMediaDemandasExercicio2.ClientID%>').value;
                    var txtProtecaoAltaDemandasExercicio2 = document.getElementById('<%=txtProtecaoAltaDemandasExercicio2.ClientID%>').value;

                    var txtBeneficiosEventuaisDemandasExercicio2 = document.getElementById('<%=txtBeneficiosEventuaisDemandasExercicio2.ClientID%>').value;
                    var txtSaoPauloSolidarioDemandasExercicio2 = document.getElementById('<%=txtSaoPauloSolidarioDemandasExercicio2.ClientID%>').value;
                    var txtProtecaoBasicaReprogramadoDemandasExercicio2 = document.getElementById('<%=txtProtecaoBasicaReprogramacaoDemandasExercicio2.ClientID%>').value;
                    var txtProtecaoMediaReprogramadoDemandasExercicio2 = document.getElementById('<%=txtProtecaoMediaReprogramacaoDemandasExercicio2.ClientID%>').value;
                    var txtProtecaoAltaReprogramadoDemandasExercicio2 = document.getElementById('<%=txtProtecaoAltaReprogramacaoDemandasExercicio2.ClientID%>').value;
                    var txtBeneficiosEventuaisReprogramadoDemandasExercicio2 = document.getElementById('<%=txtBeneficiosEventuaisDemandasReprogramadoExercicio2.ClientID%>').value;													


                    if (txtProtecaoBasicaReprogramadoExercicio2 == '') { document.getElementById('<%=txtProtecaoBasicaReprogramadoExercicio2.ClientID%>').value = '0,00'; txtProtecaoBasicaReprogramadoExercicio2 = '0,00'; }
                    if (txtProtecaoMediaReprogramadoExercicio2 == '') { document.getElementById('<%=txtProtecaoMediaReprogramadoExercicio2.ClientID%>').value = '0,00'; txtProtecaoMediaReprogramadoExercicio2 = '0,00'; }
                    if (txtProtecaoAltaReprogramadoExercicio2 == '') { document.getElementById('<%=txtProtecaoAltaReprogramadoExercicio2.ClientID%>').value = '0,00'; txtProtecaoAltaReprogramadoExercicio2 = '0,00'; }
                    if (txtBeneficiosEventuaisReprogramadoExercicio2 == '') { document.getElementById('<%=txtBeneficiosEventuaisReprogramadoExercicio2.ClientID%>').value = '0,00'; txtBeneficiosEventuaisReprogramadoExercicio2 = '0,00'; }
                    if (txtSaoPauloSolidarioReprogramadoExercicio2 == '') { document.getElementById('<%=txtSaoPauloSolidarioReprogramadoExercicio2.ClientID%>').value = '0,00'; txtSaoPauloSolidarioReprogramadoExercicio2 = '0,00'; }
                    if (txtProtecaoBasicaDemandasExercicio2 == '') { document.getElementById('<%=txtProtecaoBasicaDemandasExercicio2.ClientID%>').value = '0,00'; txtProtecaoBasicaDemandasExercicio2 = '0,00'; }
                    if (txtProtecaoMediaDemandasExercicio2 == '') { document.getElementById('<%=txtProtecaoMediaDemandasExercicio2.ClientID%>').value = '0,00'; txtProtecaoMediaDemandasExercicio2 = '0,00'; }
                    if (txtProtecaoAltaDemandasExercicio2 == '') { document.getElementById('<%=txtProtecaoAltaDemandasExercicio2.ClientID%>').value = '0,00'; txtProtecaoAltaDemandasExercicio2 = '0,00'; }
                    
                    if (txtSaoPauloSolidarioDemandasExercicio2 == '') { document.getElementById('<%=txtSaoPauloSolidarioDemandasExercicio2.ClientID%>').value = '0,00'; txtSaoPauloSolidarioDemandasExercicio2 = '0,00'; }
                    if (txtProtecaoBasicaReprogramadoDemandasExercicio2 == '') { document.getElementById('<%=txtProtecaoBasicaReprogramacaoDemandasExercicio2.ClientID%>').value = '0,00'; txtProtecaoBasicaReprogramacaoDemandasExercicio2 = '0,00'; }
                    if (txtProtecaoMediaReprogramadoDemandasExercicio2 == '') { document.getElementById('<%=txtProtecaoMediaReprogramacaoDemandasExercicio2.ClientID%>').value = '0,00'; txtProtecaoMediaReprogramacaoDemandasExercicio2 = '0,00'; }
                    if (txtProtecaoAltaReprogramadoDemandasExercicio2 == '') { document.getElementById('<%=txtProtecaoAltaReprogramacaoDemandasExercicio2.ClientID%>').value = '0,00'; txtProtecaoAltaReprogramacaoDemandasExercicio2 = '0,00'; }


                    if (txtBeneficiosEventuaisDemandasExercicio2 == '') { document.getElementById('<%=txtBeneficiosEventuaisDemandasExercicio2.ClientID%>').value = '0,00'; txtBeneficiosEventuaisDemandasExercicio2 = '0,00'; }
                    if (txtBeneficiosEventuaisReprogramadoDemandasExercicio2 == '') { document.getElementById('<%=txtBeneficiosEventuaisDemandasReprogramadoExercicio2.ClientID%>').value = '0,00'; txtBeneficiosEventuaisDemandasReprogramadoExercicio2 = '0,00'; }


                    var valoresDemandasExercicio2 = [txtProtecaoBasicaDemandasExercicio2, txtProtecaoMediaDemandasExercicio2, txtProtecaoAltaDemandasExercicio2, txtBeneficiosEventuaisDemandasExercicio2, txtSaoPauloSolidarioDemandasExercicio2];
                    var valoresReprogramadosExercicio2 = [txtProtecaoBasicaReprogramadoExercicio2, txtProtecaoMediaReprogramadoExercicio2, txtProtecaoAltaReprogramadoExercicio2, txtBeneficiosEventuaisReprogramadoExercicio2, txtSaoPauloSolidarioReprogramadoExercicio2];
                    var valoresReprogramacaoDemandasExercicio2 = [txtProtecaoBasicaReprogramadoDemandasExercicio2, txtProtecaoMediaReprogramadoDemandasExercicio2, txtProtecaoAltaReprogramadoDemandasExercicio2, txtBeneficiosEventuaisReprogramadoDemandasExercicio2];


                    PageMethods.CalcularValores(valoresReprogramadosExercicio2, function (val) {
                        document.getElementById('<%=lblTotalReprogramacaoExercicio2.ClientID%>').innerText = val;
                        document.getElementById('<%=hidTotalRecursosReprogramadosExercicio2.ClientID%>').value = val;
                    });

                    PageMethods.CalcularValores(valoresDemandasExercicio2, function (val) {
                        document.getElementById('<%=lblDemandasExercicio2.ClientID%>').innerText = val;
                        document.getElementById('<%=hidTotalRecursosDemandasExercicio2.ClientID%>').value = val;
                    });

                    PageMethods.CalcularValores(valoresReprogramacaoDemandasExercicio2, function (val) {
                    document.getElementById('<%=lblReprogramacaoDemandasExercicio2.ClientID%>').innerText = val;
                        document.getElementById('<%=hidTotalRecursosReprogramadosDemandasExercicio2.ClientID%>').value = val;
                    });


                PageMethods.CalcularProtecaoBasica(txtProtecaoSocialBasicaExercicio2, txtProtecaoBasicaReprogramadoExercicio2, txtProtecaoBasicaDemandasExercicio2, txtProtecaoBasicaReprogramadoDemandasExercicio2, function (val) {
                        document.getElementById('<%=lblTotalBasicaExercicio2.ClientID%>').innerText = val;
                });

                PageMethods.CalcularProtecaoMedia(txtProtecaoSocialMediaExercicio2, txtProtecaoMediaReprogramadoExercicio2, txtProtecaoMediaDemandasExercicio2, txtProtecaoMediaReprogramadoDemandasExercicio2, function (val) {
                            document.getElementById('<%=lblTotalProtecaoMediaExercicio2.ClientID%>').innerText = val;
                });


                PageMethods.CalcularProtecaoAlta(txtProtecaoSocialAltaExercicio2, txtProtecaoAltaReprogramadoExercicio2, txtProtecaoAltaDemandasExercicio2, txtProtecaoAltaReprogramadoDemandasExercicio2, function (val) {
                            document.getElementById('<%=lblTotalProtecaoAltaExercicio2.ClientID%>').innerText = val;
                });

                PageMethods.CalcularBeneficios(txtBeneficiosEventuaisExercicio2, txtBeneficiosEventuaisReprogramadoExercicio2, txtBeneficiosEventuaisDemandasExercicio2, txtBeneficiosEventuaisReprogramadoDemandasExercicio2, function (val) {
                        document.getElementById('<%=lblTotalBeneficiosEventuaisExercicio2.ClientID%>').innerText = val;
                });

                PageMethods.CalcularSPSolidario(txtSaoPauloSolidarioExercicio2, txtSaoPauloSolidarioReprogramadoExercicio2, function (val) {
                    document.getElementById('<%=lblTotalSaoPauloSolidarioExercicio2.ClientID%>').innerText = val;
                });


                var valoresReprogramadosExercicio2 = [txtProtecaoSocialBasicaExercicio2, txtProtecaoSocialMediaExercicio2, txtProtecaoSocialAltaExercicio2
                    , txtBeneficiosEventuaisExercicio2, txtSaoPauloSolidarioExercicio2, txtProtecaoBasicaReprogramadoExercicio2, txtProtecaoMediaReprogramadoExercicio2
                    , txtProtecaoAltaReprogramadoExercicio2, txtBeneficiosEventuaisReprogramadoExercicio2, txtSaoPauloSolidarioReprogramadoExercicio2, txtProtecaoBasicaDemandasExercicio2, txtProtecaoMediaDemandasExercicio2
                    , txtProtecaoAltaDemandasExercicio2, txtBeneficiosEventuaisDemandasExercicio2, txtSaoPauloSolidarioDemandasExercicio2, txtProtecaoBasicaReprogramadoDemandasExercicio2, txtProtecaoMediaReprogramadoDemandasExercicio2
                    , txtProtecaoAltaReprogramadoDemandasExercicio2, txtBeneficiosEventuaisReprogramadoDemandasExercicio2]

                PageMethods.CalcularValoresReprogramados(valoresReprogramadosExercicio2, function (val) {
                    document.getElementById('<%=lblTotalRecursosExercicio2.ClientID%>').innerText = val;
                });

            }


        function CalculateTotal3() {
            var txtProtecaoSocialBasicaExercicio3 = document.getElementById('<%=txtProtecaoSocialBasicaExercicio3.ClientID%>').value;
              var txtProtecaoSocialMediaExercicio3 = document.getElementById('<%=txtProtecaoSocialMediaExercicio3.ClientID%>').value;
              var txtProtecaoSocialAltaExercicio3 = document.getElementById('<%=txtProtecaoSocialAltaExercicio3.ClientID%>').value;
              var txtBeneficiosEventuaisExercicio3 = document.getElementById('<%=txtBeneficiosEventuaisExercicio3.ClientID%>').value;
              var txtSaoPauloSolidarioExercicio3 = document.getElementById('<%=txtSaoPauloSolidarioExercicio3.ClientID%>').value;

              if (txtProtecaoSocialBasicaExercicio3 == '') { document.getElementById('<%=txtProtecaoSocialBasicaExercicio3.ClientID%>').value = '0,00'; txtProtecaoSocialBasicaExercicio3 = '0,00' }
              if (txtProtecaoSocialMediaExercicio3 == '') { document.getElementById('<%=txtProtecaoSocialMediaExercicio3.ClientID%>').value = '0,00'; txtProtecaoSocialMediaExercicio3 = '0,00'; }
              if (txtProtecaoSocialAltaExercicio3 == '') { document.getElementById('<%=txtProtecaoSocialAltaExercicio3.ClientID%>').value = '0,00'; txtProtecaoSocialAltaExercicio3 = '0,00'; }
              if (txtBeneficiosEventuaisExercicio3 == '') { document.getElementById('<%=txtBeneficiosEventuaisExercicio3.ClientID%>').value = '0,00'; txtBeneficiosEventuaisExercicio3 = '0,00'; }
              if (txtSaoPauloSolidarioExercicio3 == '') { document.getElementById('<%=txtSaoPauloSolidarioExercicio3.ClientID%>').value = '0,00'; txtSaoPauloSolidarioExercicio3 = '0,00'; }

              var valores = [txtProtecaoSocialBasicaExercicio3, txtProtecaoSocialMediaExercicio3, txtProtecaoSocialAltaExercicio3, txtBeneficiosEventuaisExercicio3, txtSaoPauloSolidarioExercicio3];

              PageMethods.CalcularValores(valores, function (val) {
                  document.getElementById('<%=lblTotalCofinanciamentoExercicio3.ClientID%>').innerText = val;
                        document.getElementById('<%=hidTotalRecursosExercicio3.ClientID%>').value = val;
                    });
              //#endregion

              var txtProtecaoBasicaReprogramadoExercicio3 = document.getElementById('<%=txtProtecaoBasicaReprogramadoExercicio3.ClientID%>').value;
              var txtProtecaoMediaReprogramadoExercicio3 = document.getElementById('<%=txtProtecaoMediaReprogramadoExercicio3.ClientID%>').value;
              var txtProtecaoAltaReprogramadoExercicio3 = document.getElementById('<%=txtProtecaoAltaReprogramadoExercicio3.ClientID%>').value;
              var txtBeneficiosEventuaisReprogramadoExercicio3 = document.getElementById('<%=txtBeneficiosEventuaisReprogramadoExercicio3.ClientID%>').value;
              var txtSaoPauloSolidarioReprogramadoExercicio3 = document.getElementById('<%=txtSaoPauloSolidarioReprogramadoExercicio3.ClientID%>').value;
              var txtProtecaoBasicaDemandasExercicio3 = document.getElementById('<%=txtProtecaoBasicaDemandasExercicio3.ClientID%>').value;
              var txtProtecaoMediaDemandasExercicio3 = document.getElementById('<%=txtProtecaoMediaDemandasExercicio3.ClientID%>').value;
              var txtProtecaoAltaDemandasExercicio3 = document.getElementById('<%=txtProtecaoAltaDemandasExercicio3.ClientID%>').value;
              var txtBeneficiosEventuaisDemandasExercicio3 = document.getElementById('<%=txtBeneficiosEventuaisDemandasExercicio3.ClientID%>').value;
              var txtSaoPauloSolidarioDemandasExercicio3 = document.getElementById('<%=txtSaoPauloSolidarioDemandasExercicio3.ClientID%>').value;
              var txtProtecaoBasicaReprogramadoDemandasExercicio3 = document.getElementById('<%=txtProtecaoBasicaReprogramacaoDemandasExercicio3.ClientID%>').value;
              var txtProtecaoMediaReprogramadoDemandasExercicio3 = document.getElementById('<%=txtProtecaoMediaReprogramacaoDemandasExercicio3.ClientID%>').value;
              var txtProtecaoAltaReprogramadoDemandasExercicio3 = document.getElementById('<%=txtProtecaoAltaReprogramacaoDemandasExercicio3.ClientID%>').value;
              var txtBeneficiosEventuaisReprogramadoDemandasExercicio3 = document.getElementById('<%=txtBeneficiosEventuaisDemandasReprogramadoExercicio3.ClientID%>').value;													

              if (txtProtecaoBasicaReprogramadoExercicio3 == '') { document.getElementById('<%=txtProtecaoBasicaReprogramadoExercicio3.ClientID%>').value = '0,00'; txtProtecaoBasicaReprogramadoExercicio3 = '0,00'; }
              if (txtProtecaoMediaReprogramadoExercicio3 == '') { document.getElementById('<%=txtProtecaoMediaReprogramadoExercicio3.ClientID%>').value = '0,00'; txtProtecaoMediaReprogramadoExercicio3 = '0,00'; }
              if (txtProtecaoAltaReprogramadoExercicio3 == '') { document.getElementById('<%=txtProtecaoAltaReprogramadoExercicio3.ClientID%>').value = '0,00'; txtProtecaoAltaReprogramadoExercicio3 = '0,00'; }
              if (txtBeneficiosEventuaisReprogramadoExercicio3 == '') { document.getElementById('<%=txtBeneficiosEventuaisReprogramadoExercicio3.ClientID%>').value = '0,00'; txtBeneficiosEventuaisReprogramadoExercicio3 = '0,00'; }
              if (txtSaoPauloSolidarioReprogramadoExercicio3 == '') { document.getElementById('<%=txtSaoPauloSolidarioReprogramadoExercicio3.ClientID%>').value = '0,00'; txtSaoPauloSolidarioReprogramadoExercicio3 = '0,00'; }
              if (txtProtecaoBasicaDemandasExercicio3 == '') { document.getElementById('<%=txtProtecaoBasicaDemandasExercicio3.ClientID%>').value = '0,00'; txtProtecaoBasicaDemandasExercicio3 = '0,00'; }
              if (txtProtecaoMediaDemandasExercicio3 == '') { document.getElementById('<%=txtProtecaoMediaDemandasExercicio3.ClientID%>').value = '0,00'; txtProtecaoMediaDemandasExercicio3 = '0,00'; }
              if (txtProtecaoAltaDemandasExercicio3 == '') { document.getElementById('<%=txtProtecaoAltaDemandasExercicio3.ClientID%>').value = '0,00'; txtProtecaoAltaDemandasExercicio3 = '0,00'; }
              if (txtBeneficiosEventuaisDemandasExercicio3 == '') { document.getElementById('<%=txtBeneficiosEventuaisDemandasExercicio3.ClientID%>').value = '0,00'; txtBeneficiosEventuaisDemandasExercicio3 = '0,00'; }
              if (txtSaoPauloSolidarioDemandasExercicio3 == '') { document.getElementById('<%=txtSaoPauloSolidarioDemandasExercicio3.ClientID%>').value = '0,00'; txtSaoPauloSolidarioDemandasExercicio3 = '0,00'; }
              if (txtProtecaoBasicaReprogramadoDemandasExercicio3 == '') { document.getElementById('<%=txtProtecaoBasicaReprogramacaoDemandasExercicio3.ClientID%>').value = '0,00'; txtProtecaoBasicaReprogramacaoDemandasExercicio3 = '0,00'; }
              if (txtProtecaoMediaReprogramadoDemandasExercicio3 == '') { document.getElementById('<%=txtProtecaoMediaReprogramacaoDemandasExercicio3.ClientID%>').value = '0,00'; txtProtecaoMediaReprogramacaoDemandasExercicio3 = '0,00'; }
              if (txtProtecaoAltaReprogramadoDemandasExercicio3 == '') { document.getElementById('<%=txtProtecaoAltaReprogramacaoDemandasExercicio3.ClientID%>').value = '0,00'; txtProtecaoAltaReprogramacaoDemandasExercicio3 = '0,00'; }
              if (txtBeneficiosEventuaisReprogramadoDemandasExercicio3 == '') { document.getElementById('<%=txtBeneficiosEventuaisDemandasReprogramadoExercicio3.ClientID%>').value = '0,00'; txtBeneficiosEventuaisDemandasReprogramadoExercicio3 = '0,00'; }

              var valoresDemandasExercicio3 = [txtProtecaoBasicaDemandasExercicio3, txtProtecaoMediaDemandasExercicio3, txtProtecaoAltaDemandasExercicio3, txtBeneficiosEventuaisDemandasExercicio3, txtSaoPauloSolidarioDemandasExercicio3];
              var valoresReprogramadosExercicio3 = [txtProtecaoBasicaReprogramadoExercicio3, txtProtecaoMediaReprogramadoExercicio3, txtProtecaoAltaReprogramadoExercicio3, txtBeneficiosEventuaisReprogramadoExercicio3, txtSaoPauloSolidarioReprogramadoExercicio3];
              var valoresReprogramacaoDemandasExercicio3 = [txtProtecaoBasicaReprogramadoDemandasExercicio3, txtProtecaoMediaReprogramadoDemandasExercicio3, txtProtecaoAltaReprogramadoDemandasExercicio3, txtBeneficiosEventuaisReprogramadoDemandasExercicio3];

              PageMethods.CalcularValores(valoresReprogramadosExercicio3, function (val) {
                  document.getElementById('<%=lblTotalReprogramacaoExercicio3.ClientID%>').innerText = val;
                        document.getElementById('<%=hidTotalRecursosReprogramadosExercicio3.ClientID%>').value = val;
              });

                 PageMethods.CalcularValores(valoresDemandasExercicio3, function (val) {
                document.getElementById('<%=lblTotalDemandasExercicio3.ClientID%>').innerText = val;
                            document.getElementById('<%=hidTotalRecursosDemandasExercicio3.ClientID%>').value = val;
                        });
                PageMethods.CalcularValores(valoresReprogramacaoDemandasExercicio3, function (val) {
                document.getElementById('<%=lblTotalReprogramacaoDemandasExercicio3.ClientID%>').innerText = val;
                            document.getElementById('<%=hidTotalRecursosReprogramadosDemandasExercicio3.ClientID%>').value = val;
                        });

                PageMethods.CalcularProtecaoBasica(txtProtecaoSocialBasicaExercicio3, txtProtecaoBasicaReprogramadoExercicio3, txtProtecaoBasicaDemandasExercicio3, txtProtecaoBasicaReprogramadoDemandasExercicio3, function (val) {
                        document.getElementById('<%=lblTotalBasicaExercicio3.ClientID%>').innerText = val;
                    });
                PageMethods.CalcularProtecaoMedia(txtProtecaoSocialMediaExercicio3, txtProtecaoMediaReprogramadoExercicio3, txtProtecaoMediaDemandasExercicio3, txtProtecaoMediaReprogramadoDemandasExercicio3, function (val) {
                        document.getElementById('<%=lblTotalProtecaoMediaExercicio3.ClientID%>').innerText = val;
                        });

                PageMethods.CalcularProtecaoAlta(txtProtecaoSocialAltaExercicio3, txtProtecaoAltaReprogramadoExercicio3, txtProtecaoAltaDemandasExercicio3, txtProtecaoAltaReprogramadoDemandasExercicio3, function (val) {
                            document.getElementById('<%=lblTotalProtecaoAltaExercicio3.ClientID%>').innerText = val;
                        });
               PageMethods.CalcularBeneficios(txtBeneficiosEventuaisExercicio3, txtBeneficiosEventuaisReprogramadoExercicio3, txtBeneficiosEventuaisDemandasExercicio3, txtBeneficiosEventuaisReprogramadoDemandasExercicio3, function (val) {
                            document.getElementById('<%=lblTotalBeneficiosEventuaisExercicio3.ClientID%>').innerText = val;
                    });
                    PageMethods.CalcularSPSolidario(txtSaoPauloSolidarioExercicio3, txtSaoPauloSolidarioReprogramadoExercicio3, function (val) {
                        document.getElementById('<%=lblTotalSaoPauloSolidarioExercicio3.ClientID%>').innerText = val;
                });
                var valoresReprogramadosExercicio3 = [txtProtecaoSocialBasicaExercicio3, txtProtecaoSocialMediaExercicio3, txtProtecaoSocialAltaExercicio3
                    , txtBeneficiosEventuaisExercicio3, txtSaoPauloSolidarioExercicio3, txtProtecaoBasicaReprogramadoExercicio3, txtProtecaoMediaReprogramadoExercicio3
                    , txtProtecaoAltaReprogramadoExercicio3, txtBeneficiosEventuaisReprogramadoExercicio3, txtSaoPauloSolidarioReprogramadoExercicio3, txtProtecaoBasicaDemandasExercicio3, txtProtecaoMediaDemandasExercicio3
                    , txtProtecaoAltaDemandasExercicio3, txtBeneficiosEventuaisDemandasExercicio3, txtSaoPauloSolidarioDemandasExercicio3, txtProtecaoBasicaReprogramadoDemandasExercicio3, txtProtecaoMediaReprogramadoDemandasExercicio3
                    , txtProtecaoAltaReprogramadoDemandasExercicio3, txtBeneficiosEventuaisReprogramadoDemandasExercicio3]
                PageMethods.CalcularValoresReprogramados(valoresReprogramadosExercicio3, function (val) {
                    document.getElementById('<%=lblTotalRecursosExercicio3.ClientID%>').innerText = val;
                });

        }


        function CalculateTotal4() {
            var txtProtecaoSocialBasicaExercicio4 = document.getElementById('<%=txtProtecaoSocialBasicaExercicio4.ClientID%>').value;
            var txtProtecaoSocialMediaExercicio4 = document.getElementById('<%=txtProtecaoSocialMediaExercicio4.ClientID%>').value;
            var txtProtecaoSocialAltaExercicio4 = document.getElementById('<%=txtProtecaoSocialAltaExercicio4.ClientID%>').value;
            var txtBeneficiosEventuaisExercicio4 = document.getElementById('<%=txtBeneficiosEventuaisExercicio4.ClientID%>').value;
            var txtSaoPauloSolidarioExercicio4 = document.getElementById('<%=txtSaoPauloSolidarioExercicio4.ClientID%>').value;

            if (txtProtecaoSocialBasicaExercicio4 == '') { document.getElementById('<%=txtProtecaoSocialBasicaExercicio4.ClientID%>').value = '0,00'; txtProtecaoSocialBasicaExercicio4 = '0,00' }
            if (txtProtecaoSocialMediaExercicio4 == '') { document.getElementById('<%=txtProtecaoSocialMediaExercicio4.ClientID%>').value = '0,00'; txtProtecaoSocialMediaExercicio4 = '0,00'; }
            if (txtProtecaoSocialAltaExercicio4 == '') { document.getElementById('<%=txtProtecaoSocialAltaExercicio4.ClientID%>').value = '0,00'; txtProtecaoSocialAltaExercicio4 = '0,00'; }
            if (txtBeneficiosEventuaisExercicio4 == '') { document.getElementById('<%=txtBeneficiosEventuaisExercicio4.ClientID%>').value = '0,00'; txtBeneficiosEventuaisExercicio4 = '0,00'; }
            if (txtSaoPauloSolidarioExercicio4 == '') { document.getElementById('<%=txtSaoPauloSolidarioExercicio4.ClientID%>').value = '0,00'; txtSaoPauloSolidarioExercicio4 = '0,00'; }

            var valores = [txtProtecaoSocialBasicaExercicio4, txtProtecaoSocialMediaExercicio4, txtProtecaoSocialAltaExercicio4, txtBeneficiosEventuaisExercicio4, txtSaoPauloSolidarioExercicio4];

            PageMethods.CalcularValores(valores, function (val) {
                document.getElementById('<%=lblTotalCofinanciamentoExercicio4.ClientID%>').innerText = val;
                  document.getElementById('<%=hidTotalRecursosExercicio4.ClientID%>').value = val;
              });
            //#endregion

            var txtProtecaoBasicaReprogramadoExercicio4 = document.getElementById('<%=txtProtecaoBasicaReprogramadoExercicio4.ClientID%>').value;
            var txtProtecaoMediaReprogramadoExercicio4 = document.getElementById('<%=txtProtecaoMediaReprogramadoExercicio4.ClientID%>').value;
            var txtProtecaoAltaReprogramadoExercicio4 = document.getElementById('<%=txtProtecaoAltaReprogramadoExercicio4.ClientID%>').value;
            var txtBeneficiosEventuaisReprogramadoExercicio4 = document.getElementById('<%=txtBeneficiosEventuaisReprogramadoExercicio4.ClientID%>').value;
            var txtSaoPauloSolidarioReprogramadoExercicio4 = document.getElementById('<%=txtSaoPauloSolidarioReprogramadoExercicio4.ClientID%>').value;
            var txtProtecaoBasicaDemandasExercicio4 = document.getElementById('<%=txtProtecaoBasicaDemandasExercicio4.ClientID%>').value;
            var txtProtecaoMediaDemandasExercicio4 = document.getElementById('<%=txtProtecaoMediaDemandasExercicio4.ClientID%>').value;
            var txtProtecaoAltaDemandasExercicio4 = document.getElementById('<%=txtProtecaoAltaDemandasExercicio4.ClientID%>').value;
            var txtBeneficiosEventuaisDemandasExercicio4 = document.getElementById('<%=txtBeneficiosEventuaisDemandasExercicio4.ClientID%>').value;
            var txtSaoPauloSolidarioDemandasExercicio4 = document.getElementById('<%=txtSaoPauloSolidarioDemandasExercicio4.ClientID%>').value;
            var txtProtecaoBasicaReprogramadoDemandasExercicio4 = document.getElementById('<%=txtProtecaoBasicaReprogramacaoDemandasExercicio4.ClientID%>').value;
            var txtProtecaoMediaReprogramadoDemandasExercicio4 = document.getElementById('<%=txtProtecaoMediaReprogramacaoDemandasExercicio4.ClientID%>').value;
            var txtProtecaoAltaReprogramadoDemandasExercicio4 = document.getElementById('<%=txtProtecaoAltaReprogramacaoDemandasExercicio4.ClientID%>').value;
            var txtBeneficiosEventuaisReprogramadoDemandasExercicio4 = document.getElementById('<%=txtBeneficiosEventuaisDemandasReprogramadoExercicio4.ClientID%>').value;

            if (txtProtecaoBasicaReprogramadoExercicio4 == '') { document.getElementById('<%=txtProtecaoBasicaReprogramadoExercicio4.ClientID%>').value = '0,00'; txtProtecaoBasicaReprogramadoExercicio4 = '0,00'; }
            if (txtProtecaoMediaReprogramadoExercicio4 == '') { document.getElementById('<%=txtProtecaoMediaReprogramadoExercicio4.ClientID%>').value = '0,00'; txtProtecaoMediaReprogramadoExercicio4 = '0,00'; }
            if (txtProtecaoAltaReprogramadoExercicio4 == '') { document.getElementById('<%=txtProtecaoAltaReprogramadoExercicio4.ClientID%>').value = '0,00'; txtProtecaoAltaReprogramadoExercicio4 = '0,00'; }
            if (txtBeneficiosEventuaisReprogramadoExercicio4 == '') { document.getElementById('<%=txtBeneficiosEventuaisReprogramadoExercicio4.ClientID%>').value = '0,00'; txtBeneficiosEventuaisReprogramadoExercicio4 = '0,00'; }
            if (txtSaoPauloSolidarioReprogramadoExercicio4 == '') { document.getElementById('<%=txtSaoPauloSolidarioReprogramadoExercicio4.ClientID%>').value = '0,00'; txtSaoPauloSolidarioReprogramadoExercicio4 = '0,00'; }
            if (txtProtecaoBasicaDemandasExercicio4 == '') { document.getElementById('<%=txtProtecaoBasicaDemandasExercicio4.ClientID%>').value = '0,00'; txtProtecaoBasicaDemandasExercicio4 = '0,00'; }
            if (txtProtecaoMediaDemandasExercicio4 == '') { document.getElementById('<%=txtProtecaoMediaDemandasExercicio4.ClientID%>').value = '0,00'; txtProtecaoMediaDemandasExercicio4 = '0,00'; }
            if (txtProtecaoAltaDemandasExercicio4 == '') { document.getElementById('<%=txtProtecaoAltaDemandasExercicio4.ClientID%>').value = '0,00'; txtProtecaoAltaDemandasExercicio4 = '0,00'; }
            if (txtBeneficiosEventuaisDemandasExercicio4 == '') { document.getElementById('<%=txtBeneficiosEventuaisDemandasExercicio4.ClientID%>').value = '0,00'; txtBeneficiosEventuaisDemandasExercicio4 = '0,00'; }
            if (txtSaoPauloSolidarioDemandasExercicio4 == '') { document.getElementById('<%=txtSaoPauloSolidarioDemandasExercicio4.ClientID%>').value = '0,00'; txtSaoPauloSolidarioDemandasExercicio4 = '0,00'; }
            if (txtProtecaoBasicaReprogramadoDemandasExercicio4 == '') { document.getElementById('<%=txtProtecaoBasicaReprogramacaoDemandasExercicio4.ClientID%>').value = '0,00'; txtProtecaoBasicaReprogramacaoDemandasExercicio4 = '0,00'; }
            if (txtProtecaoMediaReprogramadoDemandasExercicio4 == '') { document.getElementById('<%=txtProtecaoMediaReprogramacaoDemandasExercicio4.ClientID%>').value = '0,00'; txtProtecaoMediaReprogramacaoDemandasExercicio4 = '0,00'; }
            if (txtProtecaoAltaReprogramadoDemandasExercicio4 == '') { document.getElementById('<%=txtProtecaoAltaReprogramacaoDemandasExercicio4.ClientID%>').value = '0,00'; txtProtecaoAltaReprogramacaoDemandasExercicio4 = '0,00'; }
            if (txtBeneficiosEventuaisReprogramadoDemandasExercicio4 == '') { document.getElementById('<%=txtBeneficiosEventuaisDemandasReprogramadoExercicio4.ClientID%>').value = '0,00'; txtBeneficiosEventuaisDemandasReprogramadoExercicio4 = '0,00'; }

            var valoresDemandasExercicio4 = [txtProtecaoBasicaDemandasExercicio4, txtProtecaoMediaDemandasExercicio4, txtProtecaoAltaDemandasExercicio4, txtBeneficiosEventuaisDemandasExercicio4, txtSaoPauloSolidarioDemandasExercicio4];
            var valoresReprogramadosExercicio4 = [txtProtecaoBasicaReprogramadoExercicio4, txtProtecaoMediaReprogramadoExercicio4, txtProtecaoAltaReprogramadoExercicio4, txtBeneficiosEventuaisReprogramadoExercicio4, txtSaoPauloSolidarioReprogramadoExercicio4];
            var valoresReprogramacaoDemandasExercicio4 = [txtProtecaoBasicaReprogramadoDemandasExercicio4, txtProtecaoMediaReprogramadoDemandasExercicio4, txtProtecaoAltaReprogramadoDemandasExercicio4, txtBeneficiosEventuaisReprogramadoDemandasExercicio4];

                PageMethods.CalcularValores(valoresReprogramadosExercicio4, function (val) {
                document.getElementById('<%=lblTotalReprogramacaoExercicio4.ClientID%>').innerText = val;
                  document.getElementById('<%=hidTotalRecursosReprogramadosExercicio4.ClientID%>').value = val;
                        });
                PageMethods.CalcularValores(valoresDemandasExercicio4, function (val) {
                document.getElementById('<%=lblTotalDemandasExercicio4.ClientID%>').innerText = val;
                            document.getElementById('<%=hidTotalRecursosDemandasExercicio4.ClientID%>').value = val;
                });
                PageMethods.CalcularValores(valoresReprogramacaoDemandasExercicio4, function (val) {
                document.getElementById('<%=lblTotalReprogramacaoDemandasExercicio4.ClientID%>').innerText = val;
                    document.getElementById('<%=hidTotalRecursosReprogramadosDemandasExercicio4.ClientID%>').value = val;
                });

                PageMethods.CalcularProtecaoBasica(txtProtecaoSocialBasicaExercicio4, txtProtecaoBasicaReprogramadoExercicio4, txtProtecaoBasicaDemandasExercicio4, txtProtecaoBasicaReprogramadoDemandasExercicio4, function (val) {
                  document.getElementById('<%=lblTotalBasicaExercicio4.ClientID%>').innerText = val;
                    });
                PageMethods.CalcularProtecaoMedia(txtProtecaoSocialMediaExercicio4, txtProtecaoMediaReprogramadoExercicio4, txtProtecaoMediaDemandasExercicio4, txtProtecaoMediaReprogramadoDemandasExercicio4, function (val) {
                        document.getElementById('<%=lblTotalProtecaoMediaExercicio4.ClientID%>').innerText = val;
                    });

                PageMethods.CalcularProtecaoAlta(txtProtecaoSocialAltaExercicio4, txtProtecaoAltaReprogramadoExercicio4, txtProtecaoAltaDemandasExercicio4, txtProtecaoAltaReprogramadoDemandasExercicio4, function (val) {
                        document.getElementById('<%=lblTotalProtecaoAltaExercicio4.ClientID%>').innerText = val;
                        });
                PageMethods.CalcularBeneficios(txtBeneficiosEventuaisExercicio4, txtBeneficiosEventuaisReprogramadoExercicio4, txtBeneficiosEventuaisDemandasExercicio4, txtBeneficiosEventuaisReprogramadoDemandasExercicio4, function (val) {
                            document.getElementById('<%=lblTotalBeneficiosEventuaisExercicio4.ClientID%>').innerText = val;
                        });
                        PageMethods.CalcularSPSolidario(txtSaoPauloSolidarioExercicio4, txtSaoPauloSolidarioReprogramadoExercicio4,function (val) {
                            document.getElementById('<%=lblTotalSaoPauloSolidarioExercicio4.ClientID%>').innerText = val;
                    });
                    var valoresReprogramadosExercicio4 = [txtProtecaoSocialBasicaExercicio4, txtProtecaoSocialMediaExercicio4, txtProtecaoSocialAltaExercicio4
                        , txtBeneficiosEventuaisExercicio4, txtSaoPauloSolidarioExercicio4, txtProtecaoBasicaReprogramadoExercicio4, txtProtecaoMediaReprogramadoExercicio4
                        , txtProtecaoAltaReprogramadoExercicio4, txtBeneficiosEventuaisReprogramadoExercicio4, txtSaoPauloSolidarioReprogramadoExercicio4, txtProtecaoBasicaDemandasExercicio4, txtProtecaoMediaDemandasExercicio4
                        , txtProtecaoAltaDemandasExercicio4, txtBeneficiosEventuaisDemandasExercicio4, txtSaoPauloSolidarioDemandasExercicio4, txtProtecaoBasicaReprogramadoDemandasExercicio4, txtProtecaoMediaReprogramadoDemandasExercicio4
                        , txtProtecaoAltaReprogramadoDemandasExercicio4, txtBeneficiosEventuaisReprogramadoDemandasExercicio4]
                        PageMethods.CalcularValoresReprogramados(valoresReprogramadosExercicio4, function (val) {
                        document.getElementById('<%=lblTotalRecursosExercicio4.ClientID%>').innerText = val;
                    });
                }


    </script>
    <input type="hidden" runat="server" id="hidTotalRecursosReprogramados" value="0,00" />
    <input type="hidden" runat="server" id="hidTotalRecursosDemandas" value="0,00" />
    <input type="hidden" runat="server" id="hidTotalRecursosReprogramadosDemandas" value="0,00" />
    <input type="hidden" runat="server" id="hidTotalRecursos" value="0,00" />

    <input type="hidden" runat="server" id="hidTotalRecursosReprogramadosExercicio2" value="0,00" />
    <input type="hidden" runat="server" id="hidTotalRecursosDemandasExercicio2" value="0,00" />
    <input type="hidden" runat="server" id="hidTotalRecursosReprogramadosDemandasExercicio2" value="0,00" />
    <input type="hidden" runat="server" id="hidTotalRecursosExercicio2" value="0,00" />

    <input type="hidden" runat="server" id="hidTotalRecursosReprogramadosExercicio3" value="0,00" />
    <input type="hidden" runat="server" id="hidTotalRecursosDemandasExercicio3" value="0,00" />
    <input type="hidden" runat="server" id="hidTotalRecursosReprogramadosDemandasExercicio3" value="0,00" />
    <input type="hidden" runat="server" id="hidTotalRecursosExercicio3" value="0,00" />

    <input type="hidden" runat="server" id="hidTotalRecursosReprogramadosExercicio4" value="0,00" />
    <input type="hidden" runat="server" id="hidTotalRecursosDemandasExercicio4" value="0,00" />
    <input type="hidden" runat="server" id="hidTotalRecursosReprogramadosDemandasExercicio4" value="0,00" />
    <input type="hidden" runat="server" id="hidTotalRecursosExercicio4" value="0,00" />


    <form name="frmAnaliseInterpretacao">
        <div class="accordion" data-role="accordion" data-close-any="true">
            <div class="frame active">
                <div class="heading">
                    <a href="#" runat="server" id="linkAlteracoesQuadro14" visible="false">
                        <img src="../Styles/Icones/irkickflash.png" align="absMiddle" border="0" />Alterado &nbsp;&nbsp;
                    </a>
                    Fluxo PMAS</b>
                                <span class="mif-flow-cascade icon"></span>
                </div>
                <div class="content">
                    <div class="formInput" data-text="Fluxo PMAS">
                        <div class="grid">
                            <div class="row">
                                <div class="cell">
                                    <b>Prefeitura Municipal de
                                         <asp:Label ID="lblMunicipio" runat="server"></asp:Label></b>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell">
                                    <b>Situação Atual do Plano:</b>&nbsp;
                            <asp:Label ID="lblSituacaoAtual" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell">
                                    <fieldset class="border-blue">
                                        <legend class="lgnd">
                                            <asp:Label ID="lblSituacao" runat="server" Font-Bold="true" CssClass="fg-blue" /></legend>
                                        <div class="row">
                                            <div class="cell">
                                                <asp:Label ID="lblParecerDrads" runat="server" Visible="false" /><br />
                                                <asp:Label ID="lblDescricao" runat="server" Text="Motivo:" Font-Bold="true" /><br />
                                                <div class="input-control textarea full-size">
                                                    <asp:TextBox ID="txtDescricao" runat="server" TextMode="MultiLine" MaxLength="8000"
                                                        Height="108px" />
                                                    <br />
                                                </div>
                                            </div>
                                            <skm:TextBoxCounter ID="NameCounter" runat="server" DataFormatString="{1} caractere(s) - ({0} palavra(s)). Máximo 8000 caracteres."
                                                Font-Bold="True" TextBoxControlId="txtDescricao" />
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="row" runat="server" id="trRecursosCofinanciamento" visible="false">
                                <div class="cell">
                                    <table border="0" cellpadding="0" cellspacing="0" class="table border bordered" style="width: 100%;">
                                        <tr class="info">
                                            <th colspan="13" style="height: 15px; top: 1px; left: 1px;">
                                                <span style="vertical-align: middle;">&nbsp;&nbsp;Distribuição dos recursos de cofinanciamento estadual repassados pelo sistema Fundo a Fundo, segundo os programas de trabalho da SEDS:
                                                </span>
                                            </th>
                                        </tr>
                                        <tr class="info">
                                            <%--##exercicio 1--%>
                                            <td colspan="2" style="text-align: center; height: 22px;" width="180"><b>Programa de Trabalho</b></td>
                                            <%--cols 4--%>
                                            <td style="text-align: center; height: 22px; width: 175px;"><b>Valor repassado no exercício 2022</b></td>
                                            <td colspan="1" style="text-align: center; height: 22px;"><b>Valor reprogramado do<br />exercício 2021</b></td>
                                            <td colspan="1" style="text-align: center; height: 22px;"><b>Valor Demandas Parlamentares do<br />exercício 2022</b></td>
                                            <td colspan="1" style="text-align: center; height: 22px;"><b>Valor Reprogramado Demandas Parlamentares do<br />exercício 2021</b></td>
                                            <td style="text-align: center; height: 22px; width: 122px;"colspan="2"><b>Total 2022</b></td>

                                            <%-- ##exercicio 2--%>
                                            <%--cols 4--%>
                                            <td style="text-align: center; height: 22px; width: 175px;"><b>Valor repassado no exercício 2023</b></td>
                                            <td colspan="1" style="text-align: center; height: 22px;"><b>Valor reprogramado do<br />exercício 2022</b></td>
                                            <td colspan="1" style="text-align: center; height: 22px;"><b>Valor Demandas Parlamentares do<br />exercício 2023</b></td>
                                            <td colspan="1" style="text-align: center; height: 22px;"><b>Valor Reprogramado Demandas Parlamentares do<br />exercício 2022</b></td>
                                            <td style="text-align: center; height: 22px; width: 122px;"><b>Total 2023</b></td>
                                        </tr>

                                        <tr>
                                            <td colspan="2" style="text-align: left;" class="info">
                                                <b>Proteção Social Básica</b>
                                            </td>
                                            <%--##exercicio 1--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoSocialBasica" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoBasicaReprogramado" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoBasicaDemandas" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoBasicaReprogramacaoDemandas" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>

                                            <td colspan="2" style="text-align: right;">
                                                <asp:Label ID="lblTotalBasica" Text="0,00" Enabled="false" runat="server" Style="text-align: right"></asp:Label>
                                            </td>
                                            <%-- ##exercicio 2--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoSocialBasicaExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoBasicaReprogramadoExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoBasicaDemandasExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoBasicaReprogramacaoDemandasExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>	
                                            <td colspan="2" style="text-align: right;">
                                                <asp:Label ID="lblTotalBasicaExercicio2" Text="0,00" Enabled="false" runat="server" Style="text-align: right"></asp:Label>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: left;" class="info">
                                                <b>Proteção Social Média Complexidade</b>
                                            </td>
                                            <%--##exercicio 1--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoSocialMedia" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoMediaReprogramado" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoMediaDemandas" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoMediaReprogramacaoDemandas" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td colspan="2" style="text-align: right; width: 25%;">
                                                <asp:Label ID="lblTotalProtecaoMedia" Text="0,00" runat="server" Style="text-align: right"></asp:Label>
                                            </td>

                                            <%-- ##exercicio 2--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoSocialMediaExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoMediaReprogramadoExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoMediaDemandasExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoMediaReprogramacaoDemandasExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td colspan="2" style="text-align: right; width: 25%;">
                                                <asp:Label ID="lblTotalProtecaoMediaExercicio2" Text="0,00" runat="server" Style="text-align: right"></asp:Label>
                                            </td>


                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: left;" class="info">
                                                <b>Proteção Social Alta Complexidade</b>
                                            </td>

                                            <%-- ##exercicio 1--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoSocialAlta" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoAltaReprogramado" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoAltaDemandas" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoAltaReprogramacaoDemandas" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td colspan="2" style="text-align: right;">
                                                <asp:Label ID="lblTotalProtecaoAlta" Text="0,00" runat="server" Style="text-align: right"></asp:Label>
                                            </td>

                                            <%-- ##exercicio 2--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoSocialAltaExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoAltaReprogramadoExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoAltaDemandasExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoAltaReprogramacaoDemandasExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>	
                                            <td colspan="2" style="text-align: right;">
                                                <asp:Label ID="lblTotalProtecaoAltaExercicio2" Text="0,00" runat="server" Style="text-align: right"></asp:Label>
                                            </td>

                                          
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: left;" class="info"><b>Benefícios Eventuais</b></td>

                                            <%-- ##exercicio 1--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtBeneficiosEventuais" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtBeneficiosEventuaisReprogramado" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtBeneficiosEventuaisDemandas" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtBeneficiosEventuaisDemandasReprogramado" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>	
                                            <td colspan="2" style="text-align: right;">
                                                <asp:Label ID="lblTotalBeneficiosEventuais" Text="0,00" runat="server" Style="text-align: right"></asp:Label>
                                            </td>

                                            <%-- ##exercicio 2--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtBeneficiosEventuaisExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtBeneficiosEventuaisReprogramadoExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtBeneficiosEventuaisDemandasExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtBeneficiosEventuaisDemandasReprogramadoExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>	
                                            <td colspan="2" style="text-align: right;">
                                                <asp:Label ID="lblTotalBeneficiosEventuaisExercicio2" Text="0,00" runat="server" Style="text-align: right"></asp:Label>
                                            </td>


                                        </tr>
                                        <tr>

                                            <td colspan="2" style="text-align: left;" class="info">
                                                <b>Programas e Projetos</b></td>

                                            <%-- ##exercicio 1--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtSaoPauloSolidario" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtSaoPauloSolidarioReprogramado" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>

                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtSaoPauloSolidarioDemandas" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="TextBox3" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>	
                                            <td colspan="2" style="text-align: right;">
                                                <asp:Label ID="lblTotalSaoPauloSolidario" Text="0,00" Enabled="false" runat="server" Style="text-align: right"></asp:Label>
                                            </td>

                                            <%-- ##exercicio 2--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtSaoPauloSolidarioExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtSaoPauloSolidarioReprogramadoExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtSaoPauloSolidarioDemandasExercicio2" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="TextBox4" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>	
                                            <td colspan="2" style="text-align: right;">
                                                <asp:Label ID="lblTotalSaoPauloSolidarioExercicio2" Text="0,00" Enabled="false" runat="server" Style="text-align: right"></asp:Label>
                                            </td>
                                       
                                        </tr>
                                        <tr style="height: 22px;" class="info">
                                            <td colspan="2" style="text-align: left;">
                                                <b>Total:</b>
                                            </td>

                                            <%-- ##exercicio 1--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblTotalCofinanciamento" Text="0,00" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right">
                                                <b>
                                                    <asp:Label ID="lblTotalReprogramacao" Text="0,00" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>

                                            <td style="text-align: right">
                                                <b>
                                                    <asp:Label ID="lblDemandas" Text="0,00" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right">
                                                <b>
                                                    <asp:Label ID="lblReprogramacaoDemandas" Text="0,00" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>
                                            <td colspan="2" style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblTotalRecursos" Text="0,00" Enabled="false" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>

                                            <%-- ##exercicio 2--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblTotalCofinanciamentoExercicio2" Text="0,00" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right">
                                                <b>
                                                    <asp:Label ID="lblTotalReprogramacaoExercicio2" Text="0,00" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>

                                            <td style="text-align: right">
                                                <b>
                                                    <asp:Label ID="lblDemandasExercicio2" Text="0,00" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right">
                                                <b>
                                                    <asp:Label ID="lblReprogramacaoDemandasExercicio2" Text="0,00" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>
                                            <td colspan="2" style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblTotalRecursosExercicio2" Text="0,00" Enabled="false" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>
                                           

                                        </tr>

                                        <tr class="info" style="height: 22px;">
                                            <td style="text-align: right;" colspan="2">
                                                <b>Valor total do cofinanciamento estadual:</b></td>

                                            <%-- ##exercicio 1--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right; width: 120px;">
                                                <b>
                                                    <asp:Label ID="lblValorCofinanciamento" Text="0,00" runat="server"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblValorReprogramacao" Text="0,00" runat="server"></asp:Label></b>
                                            </td>

                                            <td style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblValorDemandas" Text="0,00" runat="server"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblValorReprogramacaoDemandas" Text="0,00" runat="server"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right; width: 120px;" colspan="2">
                                                <b>
                                                    <asp:Label ID="lblTotalCofinanciamentoEstadual" Text="0,00" runat="server"></asp:Label></b>
                                            </td>
                                            <%-- ##exercicio 2--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right; width: 120px;">
                                                <b>
                                                    <asp:Label ID="lblValorCofinanciamentoExercicio2" Text="0,00" runat="server"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblValorReprogramacaoExercicio2" Text="0,00" runat="server"></asp:Label></b>
                                            </td>

                                            <td style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblValorDemandasExercicio2" Text="0,00" runat="server"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblValorReprogramacaoDemandasExercicio2" Text="0,00" runat="server"></asp:Label></b>
                                            </td>		
                                            <td style="text-align: right; width: 120px;" colspan="2">
                                                <b>
                                                    <asp:Label ID="lblTotalCofinanciamentoEstadualExercicio2" Text="0,00" runat="server"></asp:Label></b>
                                            </td>
                                          
                                        </tr>
                                    </table>
                                </div>
                            </div>


                            <div class="row" runat="server" id="trRecursosCofinanciamentoParte2" visible="false">
                                <div class="cell">
                                    <table border="0" cellpadding="0" cellspacing="0" class="table border bordered" style="width: 100%;">
                                        <tr class="info">
                                            <th colspan="13" style="height: 15px; top: 1px; left: 1px;">
                                                <span style="vertical-align: middle;">&nbsp;&nbsp;Distribuição dos recursos de cofinanciamento estadual repassados pelo sistema Fundo a Fundo, segundo os programas de trabalho da SEDS:
                                                </span>
                                            </th>
                                        </tr>
                                        <tr class="info">
                                            <%--##exercicio 3--%>
                                            <td colspan="2" style="text-align: center; height: 22px;" width="180"><b>Programa de Trabalho</b></td>
                                            <%--cols 4--%>
                                            <td style="text-align: center; height: 22px; width: 175px;"><b>Valor repassado no exercício 2024</b></td>
                                            <td colspan="1" style="text-align: center; height: 22px;"><b>Valor reprogramado do<br />
                                                exercício 2023</b></td>
                                            <td colspan="1" style="text-align: center; height: 22px;"><b>Valor Demandas Parlamentares do<br /> exercício 2024</b></td>
                                            <td colspan="1" style="text-align: center; height: 22px;"><b>Valor reprogramação demandas do<br /> exercício 2023</b></td>
                                            <td style="text-align: center; height: 22px; width: 122px;"colspan="2"><b>Total 2024</b></td>

                                            <%-- ##exercicio 4--%>
                                            <%--cols 4--%>
                                            <td style="text-align: center; height: 22px; width: 175px;"colspan="1"><b>Valor repassado no exercício 2025</b></td>
                                            <td colspan="1" style="text-align: center; height: 22px;"><b>Valor reprogramado do<br />
                                                exercício 2024</b></td>
                                            <td colspan="1" style="text-align: center; height: 22px;"><b>Valor Demandas Parlamentares do<br />exercício 2025</b></td>
                                            <td colspan="1" style="text-align: center; height: 22px;"><b>Valor reprogramação demandas do<br />
                                                exercício 2024</b></td>
                                            <td style="text-align: center; height: 22px; width: 122px;"><b>Total 2025</b></td>
                                        </tr>

                                        <tr>
                                            <td colspan="2" style="text-align: left;" class="info">
                                                <b>Proteção Social Básica</b>
                                            </td>
                                        
                                            <%-- ##exercicio 3--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoSocialBasicaExercicio3" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoBasicaReprogramadoExercicio3" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoBasicaDemandasExercicio3" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoBasicaReprogramacaoDemandasExercicio3" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td colspan="2" style="text-align: right;">
                                                <asp:Label ID="lblTotalBasicaExercicio3" Text="0,00" Enabled="false" runat="server" Style="text-align: right"></asp:Label>
                                            </td>
                                            <%-- ##exercicio 4--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoSocialBasicaExercicio4" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoBasicaReprogramadoExercicio4" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoBasicaDemandasExercicio4" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoBasicaReprogramacaoDemandasExercicio4" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td colspan="2" style="text-align: right;">
                                                <asp:Label ID="lblTotalBasicaExercicio4" Text="0,00" Enabled="false" runat="server" Style="text-align: right"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: left;" class="info">
                                                <b>Proteção Social Média Complexidade</b>
                                            </td>
                                         

                                            <%-- ##exercicio 3--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoSocialMediaExercicio3" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoMediaReprogramadoExercicio3" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoMediaDemandasExercicio3" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoMediaReprogramacaoDemandasExercicio3" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td colspan="2" style="text-align: right; width: 25%;">
                                                <asp:Label ID="lblTotalProtecaoMediaExercicio3" Text="0,00" runat="server" Style="text-align: right"></asp:Label>
                                            </td>


                                            <%-- ##exercicio 4--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoSocialMediaExercicio4" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoMediaReprogramadoExercicio4" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>

                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoMediaDemandasExercicio4" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>

                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoMediaReprogramacaoDemandasExercicio4" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td colspan="1" style="text-align: right; width: 25%;">
                                                <asp:Label ID="lblTotalProtecaoMediaExercicio4" Text="0,00" runat="server" Style="text-align: right"></asp:Label>
                                            </td>


                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: left;" class="info">
                                                <b>Proteção Social Alta Complexidade</b>
                                            </td>

                                          

                                            <%-- ##exercicio 3--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoSocialAltaExercicio3" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoAltaReprogramadoExercicio3" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>

                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoAltaDemandasExercicio3" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoAltaReprogramacaoDemandasExercicio3" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td colspan="2" style="text-align: right;">
                                                <asp:Label ID="lblTotalProtecaoAltaExercicio3" Text="0,00" runat="server" Style="text-align: right"></asp:Label>
                                            </td>
                                            <%-- ##exercicio 4--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoSocialAltaExercicio4" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoAltaReprogramadoExercicio4" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoAltaDemandasExercicio4" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtProtecaoAltaReprogramacaoDemandasExercicio4" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td colspan="2" style="text-align: right;">
                                                <asp:Label ID="lblTotalProtecaoAltaExercicio4" Text="0,00" runat="server" Style="text-align: right"></asp:Label>
                                            </td>


                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: left;" class="info"><b>Benefícios Eventuais</b></td>

                                           

                                            <%-- ##exercicio 3--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtBeneficiosEventuaisExercicio3" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtBeneficiosEventuaisReprogramadoExercicio3" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>

                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtBeneficiosEventuaisDemandasExercicio3" Text="0,00" runat="server" Enabled="false" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtBeneficiosEventuaisDemandasReprogramadoExercicio3" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td colspan="2" style="text-align: right;">
                                                <asp:Label ID="lblTotalBeneficiosEventuaisExercicio3" Text="0,00" runat="server" Style="text-align: right"></asp:Label>
                                            </td>


                                            <%-- ##exercicio 4--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtBeneficiosEventuaisExercicio4" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtBeneficiosEventuaisReprogramadoExercicio4" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtBeneficiosEventuaisDemandasExercicio4" Text="0,00" runat="server" Enabled="false" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtBeneficiosEventuaisDemandasReprogramadoExercicio4" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td colspan="2" style="text-align: right;">
                                                <asp:Label ID="lblTotalBeneficiosEventuaisExercicio4" Text="0,00" runat="server" Style="text-align: right"></asp:Label>
                                            </td>


                                        </tr>
                                        <tr>

                                            <td colspan="2" style="text-align: left;" class="info">
                                                <b>Programas e Projetos</b></td>

                                          
                                            <%-- ##exercicio 3--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtSaoPauloSolidarioExercicio3" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtSaoPauloSolidarioReprogramadoExercicio3" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtSaoPauloSolidarioDemandasExercicio3" Text="0,00" runat="server" Enabled="false" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="TextBox7" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td colspan="2" style="text-align: right;">
                                                <asp:Label ID="lblTotalSaoPauloSolidarioExercicio3" Text="0,00" Enabled="false" runat="server" Style="text-align: right"></asp:Label>
                                            </td>
                                            <%-- ##exercicio 4--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtSaoPauloSolidarioExercicio4" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtSaoPauloSolidarioReprogramadoExercicio4" Text="0,00" runat="server" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="txtSaoPauloSolidarioDemandasExercicio4" Text="0,00" runat="server" Enabled="false" Style="width: 100px; text-align: right"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:TextBox ID="TextBox8" Text="0,00" runat="server" Style="width: 100px; text-align: right" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td colspan="2" style="text-align: right;">
                                                <asp:Label ID="lblTotalSaoPauloSolidarioExercicio4" Text="0,00" Enabled="false" runat="server" Style="text-align: right"></asp:Label>
                                            </td>

                                        </tr>
                                        <tr style="height: 22px;" class="info">
                                            <td colspan="2" style="text-align: left;">
                                                <b>Total:</b>
                                            </td>

                                         
                                            <%-- ##exercicio 3--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblTotalCofinanciamentoExercicio3" Text="0,00" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right">
                                                <b>
                                                    <asp:Label ID="lblTotalReprogramacaoExercicio3" Text="0,00" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>

                                            <td style="text-align: right">
                                                <b>
                                                    <asp:Label ID="lblTotalDemandasExercicio3" Text="0,00" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>

                                            <td style="text-align: right">
                                                <b>
                                                    <asp:Label ID="lblTotalReprogramacaoDemandasExercicio3" Text="0,00" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>

                                            <td colspan="2" style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblTotalRecursosExercicio3" Text="0,00" Enabled="false" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>
                                            <%-- ##exercicio 4--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblTotalCofinanciamentoExercicio4" Text="0,00" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right">
                                                <b>
                                                    <asp:Label ID="lblTotalReprogramacaoExercicio4" Text="0,00" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>

                                            <td style="text-align: right">
                                                <b>
                                                    <asp:Label ID="lblTotalDemandasExercicio4" Text="0,00" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right">
                                                <b>
                                                    <asp:Label ID="lblTotalReprogramacaoDemandasExercicio4" Text="0,00" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>	
                                            <td colspan="2" style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblTotalRecursosExercicio4" Text="0,00" Enabled="false" runat="server" Style="text-align: right"></asp:Label></b>
                                            </td>


                                        </tr>

                                        <tr class="info" style="height: 22px;">
                                            <td style="text-align: right;" colspan="2">
                                                <b>Valor total do cofinanciamento estadual:</b></td>

                                           
                                            <%-- ##exercicio 3--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right; width: 120px;">
                                                <b>
                                                    <asp:Label ID="lblValorCofinanciamentoExercicio3" Text="0,00" runat="server"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblValorReprogramacaoExercicio3" Text="0,00" runat="server"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblValorDemandasExercicio3" Text="0,00" runat="server"></asp:Label></b>
                                            </td>

                                            <td style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblValorReprogramadoDemandasExercicio3" Text="0,00" runat="server"></asp:Label></b>
                                            </td>		

                                            <td style="text-align: right; width: 120px;" colspan="2">
                                                <b>
                                                    <asp:Label ID="lblTotalCofinanciamentoEstadualExercicio3" Text="0,00" runat="server"></asp:Label></b>
                                            </td>

                                            <%-- ##exercicio 4--%>
                                            <%--cols 4--%>
                                            <td style="text-align: right; width: 120px;">
                                                <b>
                                                    <asp:Label ID="lblValorCofinanciamentoExercicio4" Text="0,00" runat="server"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblValorReprogramacaoExercicio4" Text="0,00" runat="server"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblValorDemandasExercicio4" Text="0,00" runat="server"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right;">
                                                <b>
                                                    <asp:Label ID="lblValorReprogramadoDemandasExercicio4" Text="0,00" runat="server"></asp:Label></b>
                                            </td>
                                            <td style="text-align: right; width: 120px;" colspan="2">
                                                <b>
                                                    <asp:Label ID="lblTotalCofinanciamentoEstadualExercicio4" Text="0,00" runat="server"></asp:Label></b>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <div class="row" id="trAprovacao" runat="server" visible="false">
                                <div class="cell">
                                    <div class="row">
                                        <div class="cell">
                                            Considerando as alterações realizadas pelo Órgão Gestor, o parecer atual do Conselho Municipal de Assistência Social 
                                                sobre o Plano Municipal de Assistência Social - PMAS 2022 é
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="cell">
                                            <asp:RadioButtonList RepeatDirection="Horizontal" runat="server" ID="rblAprovacao"
                                                Style="margin-left: 0px" CellSpacing="20" Height="47px" Font-Bold="true">
                                                <asp:ListItem Selected="False" Value="1" Text="Favorável(Aprova o PMAS 2022/2025)"></asp:ListItem>
                                                <asp:ListItem Selected="False" Value="0" Text="Desfavorável(Rejeita o PMAS 2022/2025)"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell">
                                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" Width="89px"
                                        SkinID="button-save" OnClick="btnSalvar_Click" />
                                    &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnVoltar" runat="server" Text="Voltar"
                                    OnClick="btnVoltar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
