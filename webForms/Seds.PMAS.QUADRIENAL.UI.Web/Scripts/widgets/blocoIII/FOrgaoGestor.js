function CalculateTotal() {
    var txtEscolarizacaoBasica = document.getElementById('<%= txtEscolarizacaoBasica.ClientID %>').value;
    var txtFundamentalBasica = document.getElementById('<%=txtFundamentalBasica.ClientID%>').value;
    var txtMedioBasica = document.getElementById('<%=txtMedioBasica.ClientID%>').value;
    var txtSuperiorBasica = document.getElementById('<%=txtSuperiorBasica.ClientID%>').value;
    if (txtEscolarizacaoBasica == '') { document.getElementById('<%=txtEscolarizacaoBasica.ClientID%>').value = '0'; txtEscolarizacaoBasica = '0' }
    if (txtFundamentalBasica == '') { document.getElementById('<%=txtFundamentalBasica.ClientID%>').value = '0'; txtFundamentalBasica = '0' }
    if (txtMedioBasica == '') { document.getElementById('<%=txtMedioBasica.ClientID%>').value = '0'; txtMedioBasica = '0'; }
    if (txtSuperiorBasica == '') { document.getElementById('<%=txtSuperiorBasica.ClientID%>').value = '0'; txtSuperiorBasica = '0'; }
    //var valores = [txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica];
    PageMethods.CalcularProtecaoBasica(txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica, change(function (val) {
        document.getElementById('<%=lblTotalBasica.ClientID%>').innerText = val;
    }));

    var txtEscolarizacaoEspecial = document.getElementById('<%=txtEscolarizacaoEspecial.ClientID%>').value;
    var txtFundamentalEspecial = document.getElementById('<%=txtFundamentalEspecial.ClientID%>').value;
    var txtMedioEspecial = document.getElementById('<%=txtMedioEspecial.ClientID%>').value;
    var txtSuperiorEspecial = document.getElementById('<%=txtSuperiorEspecial.ClientID%>').value;
    if (txtEscolarizacaoEspecial == '') { document.getElementById('<%=txtEscolarizacaoBasica.ClientID%>').value = '0'; txtEscolarizacaoBasica = '0' }
    if (txtFundamentalEspecial == '') { document.getElementById('<%=txtFundamentalEspecial.ClientID%>').value = '0'; txtFundamentalEspecial = '0' }
    if (txtMedioEspecial == '') { document.getElementById('<%=txtMedioEspecial.ClientID%>').value = '0'; txtMedioEspecial = '0'; }
    if (txtSuperiorEspecial == '') { document.getElementById('<%=txtSuperiorEspecial.ClientID%>').value = '0'; txtSuperiorEspecial = '0'; }
    //var valores = [txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica];
    PageMethods.CalcularProtecaoEspecial(txtEscolarizacaoEspecial, txtFundamentalEspecial, txtMedioEspecial, txtSuperiorEspecial, function (val) {
        document.getElementById('<%=lblTotalEspecial.ClientID%>').innerText = val;
    });

    var txtEscolarizacaoSocioassistencial = document.getElementById('<%=txtEscolarizacaoSocioassistencial.ClientID%>').value;
    var txtFundamentalSocioassistencial = document.getElementById('<%=txtFundamentalSocioassistencial.ClientID%>').value;
    var txtMedioSocioassistencial = document.getElementById('<%=txtMedioSocioassistencial.ClientID%>').value;
    var txtSuperiorSocioassistencial = document.getElementById('<%=txtSuperiorSocioassistencial.ClientID%>').value;
    if (txtEscolarizacaoEspecial == '') { document.getElementById('<%=txtEscolarizacaoSocioassistencial.ClientID%>').value = '0'; txtEscolarizacaoSocioassistencial = '0' }
    if (txtFundamentalEspecial == '') { document.getElementById('<%=txtFundamentalSocioassistencial.ClientID%>').value = '0'; txtFundamentalSocioassistencial = '0' }
    if (txtMedioSocioassistencial == '') { document.getElementById('<%=txtMedioSocioassistencial.ClientID%>').value = '0'; txtMedioSocioassistencial = '0'; }
    if (txtSuperiorSocioassistencial == '') { document.getElementById('<%=txtSuperiorSocioassistencial.ClientID%>').value = '0'; txtSuperiorSocioassistencial = '0'; }
    //var valores = [txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica];
    PageMethods.CalcularVigilancia(txtEscolarizacaoSocioassistencial, txtFundamentalSocioassistencial, txtMedioSocioassistencial, txtSuperiorSocioassistencial, function (val) {
        document.getElementById('<%=lblTotalSocioassistencial.ClientID%>').innerText = val;
    });


    var txtEscolarizacaoTransferencia = document.getElementById('<%=txtEscolarizacaoTransferencia.ClientID%>').value;
    var txtFundamentalTransferencia = document.getElementById('<%=txtFundamentalTransferencia.ClientID%>').value;
    var txtMedioTransferencia = document.getElementById('<%=txtMedioTransferencia.ClientID%>').value;
    var txtSuperiorTransferencia = document.getElementById('<%=txtSuperiorTransferencia.ClientID%>').value;
    if (txtEscolarizacaoTransferencia == '') { document.getElementById('<%=txtEscolarizacaoTransferencia.ClientID%>').value = '0'; txtEscolarizacaoTransferencia = '0' }
    if (txtFundamentalTransferencia == '') { document.getElementById('<%=txtFundamentalTransferencia.ClientID%>').value = '0'; txtFundamentalTransferencia = '0' }
    if (txtMedioTransferencia == '') { document.getElementById('<%=txtMedioTransferencia.ClientID%>').value = '0'; txtMedioTransferencia = '0'; }
    if (txtSuperiorTransferencia == '') { document.getElementById('<%=txtSuperiorTransferencia.ClientID%>').value = '0'; txtSuperiorTransferencia = '0'; }
    //var valores = [txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica];
    PageMethods.CalcularGestaoTransferencia(txtEscolarizacaoTransferencia, txtFundamentalTransferencia, txtMedioTransferencia, txtSuperiorTransferencia, function (val) {
        document.getElementById('<%=lblTotalTransferencia.ClientID%>').innerText = val;
    });

    var txtEscolarizacaoCadUnico = document.getElementById('<%=txtEscolarizacaoCadUnico.ClientID%>').value;
    var txtFundamentalCadUnico = document.getElementById('<%=txtFundamentalCadUnico.ClientID%>').value;
    var txtMedioCadUnico = document.getElementById('<%=txtMedioCadUnico.ClientID%>').value;
    var txtSuperiorCadUnico = document.getElementById('<%=txtSuperiorCadUnico.ClientID%>').value;
    if (txtEscolarizacaoCadUnico == '') { document.getElementById('<%=txtEscolarizacaoCadUnico.ClientID%>').value = '0'; txtEscolarizacaoCadUnico = '0' }
    if (txtFundamentalCadUnico == '') { document.getElementById('<%=txtFundamentalCadUnico.ClientID%>').value = '0'; txtFundamentalCadUnico = '0' }
    if (txtMedioCadUnico == '') { document.getElementById('<%=txtMedioCadUnico.ClientID%>').value = '0'; txtMedioCadUnico = '0'; }
    if (txtSuperiorCadUnico == '') { document.getElementById('<%=txtSuperiorCadUnico.ClientID%>').value = '0'; txtSuperiorCadUnico = '0'; }
    //var valores = [txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica];
    PageMethods.CalcularGestaoCadUnico(txtEscolarizacaoCadUnico, txtFundamentalCadUnico, txtMedioCadUnico, txtSuperiorCadUnico, function (val) {
        document.getElementById('<%=lblTotalCadUnico.ClientID%>').innerText = val;
    });


    var txtEscolarizacaoGestaoFinanceira = document.getElementById('<%=txtEscolarizacaoGestaoFinanceira.ClientID%>').value;
    var txtFundamentalGestaoFinanceira = document.getElementById('<%=txtFundamentalGestaoFinanceira.ClientID%>').value;
    var txtMedioGestaoFinanceira = document.getElementById('<%=txtMedioGestaoFinanceira.ClientID%>').value;
    var txtSuperiorGestaoFinanceira = document.getElementById('<%=txtSuperiorGestaoFinanceira.ClientID%>').value;
    if (txtEscolarizacaoGestaoFinanceira == '') { document.getElementById('<%=txtEscolarizacaoGestaoFinanceira.ClientID%>').value = '0'; txtEscolarizacaoGestaoFinanceira = '0' }
    if (txtFundamentalGestaoFinanceira == '') { document.getElementById('<%=txtFundamentalGestaoFinanceira.ClientID%>').value = '0'; txtFundamentalGestaoFinanceira = '0' }
    if (txtMedioGestaoFinanceira == '') { document.getElementById('<%=txtMedioGestaoFinanceira.ClientID%>').value = '0'; txtMedioGestaoFinanceira = '0'; }
    if (txtSuperiorGestaoFinanceira == '') { document.getElementById('<%=txtSuperiorGestaoFinanceira.ClientID%>').value = '0'; txtSuperiorGestaoFinanceira = '0'; }
    //var valores = [txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica];
    PageMethods.CalcularGestaoFinanceira(txtEscolarizacaoGestaoFinanceira, txtFundamentalGestaoFinanceira, txtMedioGestaoFinanceira, txtSuperiorGestaoFinanceira, function (val) {
        document.getElementById('<%=lblTotalGestaoFinanceira.ClientID%>').innerText = val;
    });

    var txtEscolarizacaoSUAS = document.getElementById('<%=txtEscolarizacaoSUAS.ClientID%>').value;
    var txtFundamentalSUAS = document.getElementById('<%=txtFundamentalSUAS.ClientID%>').value;
    var txtMedioSUAS = document.getElementById('<%=txtMedioSUAS.ClientID%>').value;
    var txtSuperiorSUAS = document.getElementById('<%=txtSuperiorSUAS.ClientID%>').value;
    if (txtEscolarizacaoSUAS == '') { document.getElementById('<%=txtEscolarizacaoSUAS.ClientID%>').value = '0'; txtEscolarizacaoSUAS = '0' }
    if (txtFundamentalSUAS == '') { document.getElementById('<%=txtFundamentalSUAS.ClientID%>').value = '0'; txtFundamentalSUAS = '0' }
    if (txtMedioSUAS == '') { document.getElementById('<%=txtMedioSUAS.ClientID%>').value = '0'; txtMedioSUAS = '0'; }
    if (txtSuperiorSUAS == '') { document.getElementById('<%=txtSuperiorSUAS.ClientID%>').value = '0'; txtSuperiorSUAS = '0'; }
    //var valores = [txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica];
    PageMethods.CalcularTrabalhoSUAS(txtEscolarizacaoSUAS, txtFundamentalSUAS, txtMedioSUAS, txtSuperiorSUAS, function (val) {
        document.getElementById('<%=lblTotalSUAS.ClientID%>').innerText = val;
    });

    var txtEscolarizacaoRegulacaoSUAS = document.getElementById('<%=txtEscolarizacaoRegulacaoSUAS.ClientID%>').value;
    var txtFundamentalRegulacaoSUAS = document.getElementById('<%=txtFundamentalRegulacaoSUAS.ClientID%>').value;
    var txtMedioRegulacaoSUAS = document.getElementById('<%=txtMedioRegulacaoSUAS.ClientID%>').value;
    var txtSuperiorRegulacaoSUAS = document.getElementById('<%=txtSuperiorRegulacaoSUAS.ClientID%>').value;
    if (txtEscolarizacaoRegulacaoSUAS == '') { document.getElementById('<%=txtEscolarizacaoRegulacaoSUAS.ClientID%>').value = '0'; txtEscolarizacaoRegulacaoSUAS = '0' }
    if (txtFundamentalRegulacaoSUAS == '') { document.getElementById('<%=txtFundamentalRegulacaoSUAS.ClientID%>').value = '0'; txtFundamentalRegulacaoSUAS = '0' }
    if (txtMedioRegulacaoSUAS == '') { document.getElementById('<%=txtMedioRegulacaoSUAS.ClientID%>').value = '0'; txtMedioRegulacaoSUAS = '0'; }
    if (txtSuperiorRegulacaoSUAS == '') { document.getElementById('<%=txtSuperiorRegulacaoSUAS.ClientID%>').value = '0'; txtSuperiorRegulacaoSUAS = '0'; }
    //var valores = [txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica];
    PageMethods.CalcularTrabalhoSUAS(txtEscolarizacaoRegulacaoSUAS, txtFundamentalRegulacaoSUAS, txtMedioRegulacaoSUAS, txtSuperiorRegulacaoSUAS, function (val) {
        document.getElementById('<%=lblTotalRegulacaoSUAS.ClientID%>').innerText = val;
    });

    var txtEscolarizacaoRedeDireta = document.getElementById('<%=txtEscolarizacaoRedeDireta.ClientID%>').value;
    var txtFundamentalRedeDireta = document.getElementById('<%=txtFundamentalRedeDireta.ClientID%>').value;
    var txtMedioRedeDireta = document.getElementById('<%=txtMedioRedeDireta.ClientID%>').value;
    var txtSuperiorRedeDireta = document.getElementById('<%=txtSuperiorRedeDireta.ClientID%>').value;
    if (txtEscolarizacaoRedeDireta == '') { document.getElementById('<%=txtEscolarizacaoRedeDireta.ClientID%>').value = '0'; txtEscolarizacaoRedeDireta = '0' }
    if (txtFundamentalRedeDireta == '') { document.getElementById('<%=txtFundamentalRedeDireta.ClientID%>').value = '0'; txtFundamentalRedeDireta = '0' }
    if (txtMedioRedeDireta == '') { document.getElementById('<%=txtMedioRedeDireta.ClientID%>').value = '0'; txtMedioRedeDireta = '0'; }
    if (txtSuperiorRedeDireta == '') { document.getElementById('<%=txtSuperiorRedeDireta.ClientID%>').value = '0'; txtSuperiorRedeDireta = '0'; }
    //var valores = [txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica];
    PageMethods.CalcularRedeDireta(txtEscolarizacaoRedeDireta, txtFundamentalRedeDireta, txtMedioRedeDireta, txtSuperiorRedeDireta, function (val) {
        document.getElementById('<%=lblTotalRedeDireta.ClientID%>').innerText = val;
    });

    var txtEscolarizacaoOutraEquipe = document.getElementById('<%=txtEscolarizacaoOutraEquipe.ClientID%>').value;
    var txtFundamentalOutraEquipe = document.getElementById('<%=txtFundamentalOutraEquipe.ClientID%>').value;
    var txtMedioOutraEquipe = document.getElementById('<%=txtMedioOutraEquipe.ClientID%>').value;
    var txtSuperiorOutraEquipe = document.getElementById('<%=txtSuperiorOutraEquipe.ClientID%>').value;
    if (txtEscolarizacaoOutraEquipe == '') { document.getElementById('<%=txtEscolarizacaoOutraEquipe.ClientID%>').value = '0'; txtEscolarizacaoOutraEquipe = '0' }
    if (txtFundamentalOutraEquipe == '') { document.getElementById('<%=txtFundamentalOutraEquipe.ClientID%>').value = '0'; txtFundamentalOutraEquipe = '0' }
    if (txtMedioOutraEquipe == '') { document.getElementById('<%=txtMedioOutraEquipe.ClientID%>').value = '0'; txtMedioOutraEquipe = '0'; }
    if (txtSuperiorOutraEquipe == '') { document.getElementById('<%=txtSuperiorOutraEquipe.ClientID%>').value = '0'; txtSuperiorOutraEquipe = '0'; }
    //var valores = [txtEscolarizacaoBasica, txtFundamentalBasica, txtMedioBasica, txtSuperiorBasica];
    PageMethods.CalcularOutraEquipe(txtEscolarizacaoOutraEquipe, txtFundamentalOutraEquipe, txtMedioOutraEquipe, txtSuperiorOutraEquipe, function (val) {
        document.getElementById('<%=lblTotalOutraEquipe.ClientID%>').innerText = val;
    });

    var valores = [txtEscolarizacaoBasica, txtEscolarizacaoEspecial, txtEscolarizacaoSocioassistencial, txtEscolarizacaoTransferencia, txtEscolarizacaoCadUnico, txtEscolarizacaoGestaoFinanceira, txtEscolarizacaoSUAS, txtEscolarizacaoRegulacaoSUAS, txtEscolarizacaoRedeDireta, txtEscolarizacaoOutraEquipe];
    PageMethods.CalcularValores(valores, function (val) {
        console.log(val);
        document.getElementById('<%=lblTotalEscolarizacao.ClientID%>').innerText = val;
    });

    var valores = [txtFundamentalBasica, txtFundamentalEspecial, txtFundamentalSocioassistencial, txtFundamentalTransferencia, txtFundamentalCadUnico, txtFundamentalGestaoFinanceira, txtFundamentalSUAS, txtFundamentalRegulacaoSUAS, txtFundamentalRedeDireta, txtFundamentalOutraEquipe];
    PageMethods.CalcularValores(valores, function (val) {
        console.log(val);
        document.getElementById('<%=lblTotalFundamental.ClientID%>').innerText = val;
    });

    var valores = [txtMedioBasica, txtMedioEspecial, txtMedioSocioassistencial, txtMedioTransferencia, txtMedioCadUnico, txtMedioGestaoFinanceira, txtMedioSUAS, txtMedioRegulacaoSUAS, txtMedioRedeDireta, txtMedioOutraEquipe];
    PageMethods.CalcularValores(valores, function (val) {
        console.log(val);
        document.getElementById('<%=lblTotalMedio.ClientID%>').innerText = val;
    });

    var valores = [txtSuperiorBasica, txtSuperiorEspecial, txtSuperiorSocioassistencial, txtSuperiorTransferencia, txtSuperiorCadUnico, txtSuperiorGestaoFinanceira, txtSuperiorSUAS, txtSuperiorRegulacaoSUAS, txtSuperiorRedeDireta, txtSuperiorOutraEquipe];
    PageMethods.CalcularValores(valores, function (val) {
        console.log(val);
        document.getElementById('<%=lblTotalSuperior.ClientID%>').innerText = val;
    });

    var valores = [txtEscolarizacaoBasica, txtEscolarizacaoEspecial, txtEscolarizacaoSocioassistencial, txtEscolarizacaoTransferencia, txtEscolarizacaoCadUnico, txtEscolarizacaoGestaoFinanceira, txtEscolarizacaoSUAS, txtEscolarizacaoRegulacaoSUAS, txtEscolarizacaoRedeDireta, txtEscolarizacaoOutraEquipe,
        txtFundamentalBasica, txtFundamentalEspecial, txtFundamentalSocioassistencial, txtFundamentalTransferencia, txtFundamentalCadUnico, txtFundamentalGestaoFinanceira, txtFundamentalSUAS, txtFundamentalRegulacaoSUAS, txtFundamentalRedeDireta, txtFundamentalOutraEquipe,
        txtMedioBasica, txtMedioEspecial, txtMedioSocioassistencial, txtMedioTransferencia, txtMedioCadUnico, txtMedioGestaoFinanceira, txtMedioSUAS, txtMedioRegulacaoSUAS, txtMedioRedeDireta, txtMedioOutraEquipe,
        txtSuperiorBasica, txtSuperiorEspecial, txtSuperiorSocioassistencial, txtSuperiorTransferencia, txtSuperiorCadUnico, txtSuperiorGestaoFinanceira, txtSuperiorSUAS, txtSuperiorRegulacaoSUAS, txtSuperiorRedeDireta, txtSuperiorOutraEquipe];
    PageMethods.CalcularValores(valores, function (val) {
        console.log(val);
        document.getElementById('<%=lblTotal.ClientID%>').innerText = val;
    });

    

}

