var utilCache = {};

function SoNumero(e, event) {
    var tecla = event.keyCode ? event.keyCode : (event.which ? event.which : event.charCode);

    if (tecla > 47 && tecla < 58) // numeros de 0 a 9
        return true;
    else {
        if (tecla != 8) // backspace
            event.keyCode = 0;
        else
            return true;
    }
}

function txtBoxFormat(strField, sMask, evtKeyPress) {

    var i, nCount, sValue, fldLen, mskLen, bolMask, sCod, nTecla;

    if (document.all) { // Internet Explorer
        nTecla = evtKeyPress.keyCode;
    }
    else if (document.layers) { // Nestcape
        nTecla = evtKeyPress.which;
    }
    else {
        nTecla = evtKeyPress.which;
        if (nTecla == 8) {
            return true;
        }
    }

    sValue = strField.value;

    // Limpa todos os caracteres de formatação que já estiverem no campo.
    sValue = sValue.toString().replace("-", "");
    sValue = sValue.toString().replace("-", "");
    sValue = sValue.toString().replace(".", "");
    sValue = sValue.toString().replace(".", "");
    sValue = sValue.toString().replace("/", "");
    sValue = sValue.toString().replace("/", "");
    sValue = sValue.toString().replace("(", "");
    sValue = sValue.toString().replace("(", "");
    sValue = sValue.toString().replace(")", "");
    sValue = sValue.toString().replace(")", "");
    sValue = sValue.toString().replace(" ", "");
    sValue = sValue.toString().replace(" ", "");
    sValue = sValue.toString().replace(":", "");
    fldLen = sValue.length;
    mskLen = sMask.length;

    i = 0;
    nCount = 0;
    sCod = "";
    mskLen = fldLen;

    while (i <= mskLen) {
        bolMask = ((sMask.charAt(i) == "-") || (sMask.charAt(i) == ".") ||
(sMask.charAt(i) == "/"))
        bolMask = bolMask || ((sMask.charAt(i) == "(") || (sMask.charAt(i) ==
")") || (sMask.charAt(i) == " "))

        if (bolMask) {
            sCod += sMask.charAt(i);
            mskLen++;
        }
        else {
            sCod += sValue.charAt(nCount);
            nCount++;
        }

        i++;
    }

    strField.value = sCod;

    if (nTecla != 8) { // backspace
        if (sMask.charAt(i - 1) == "9") { // apenas números...
            return ((nTecla > 47) && (nTecla < 58));
        } // números de 0 a 9
        else { // qualquer caracter...
            return true;
        } 
    }
    else {
        return true;
    }
}


//function formatNumber(fld)
//{
//   // debugger;
//    var strNumber = fld.value;

//    while (String(strNumber).match(/^\d{4}/)) { // enquanto houver 4 dígitos sem separador de milhar
//        strNumber = strNumber.replace(/(\d)(\d{3}(\.|$))/, '$1.$2'); // separar o dígito de milhar dos dígitos de centena com o ponto, de trás para frente!
//    }
//    return strNumber;
//}


function formatNumber(input) {
    var num = input.value.replace(/\./g, '');
    if (!isNaN(num)) {
        num = num.toString().split('').reverse().join('').replace(/(?=\d*\.?)(\d{3})/g, '$1.');
        num = num.split('').reverse().join('').replace(/^[\.]/, '');
        input.value = num;
    }

    else {
        alert('Digite somente números');
        input.value = input.value.replace(/[^\d\.]*/g, '');
    }
}


function currencyFormatDoubleClick(campo, milSep, separadorDecimal, e) {
    utilCache.DoubleClickAtivated = true;
    //Reposicionar(campo, separadorDecimal);
}
function currencyFormat(campo, milSep, separadorDecimal, e) {
    format(campo);
}

/**
* Formats the element's value or innerHTML by it's [data-format] attribute.
* @param {element} element - The element to format.
* @param {string} value - (optional) If set, will format the element's value by this instead of it's value/innerHTML.
* @example
* <input type="text" data-format="{dd/MM/yyyy HH:mm}" />
* <input type="text" data-format="{3.0,2?}" /> (3 thousands separator, comma decimal separator, 2 decimal places, optional)
* @returns {string} - The formatted value.
*/
function format(element, value) {
    //var format = element.hasAttribute("data-format") ? element.getAttribute("data-format").replace(/([^\{]*)\{(.*)\}(.*)/g, "$2") : "";
    var format = "{3.0,2}".replace(/([^\{]*)\{(.*)\}(.*)/g, "$2");

    var selectionStart = element.selectionStart;
    var offset = 0;
    var caret = element.value ? (element.value.length !== element.selectionStart) : 0;
    if (caret) {
        offset = (element.value.match(/\./g) || []).length;
    }

    if (format) {
        if (/[0-9]/.test(format[0])) {
            // - NUMBER
            var formats = format.replace(/([1-9]*)([^0-9]*)0([^0-9]*)([0-9]*)(\??)/g, "$1|$2|$3|$4|$5").split("|");
            //console.log("thousands: " + formats[0]);
            //console.log("thousands character: " + formats[1]);        
            //console.log("decimal character: " + formats[2]);
            //console.log("decimal places: " + formats[3]);
            //console.log("omit formats: " + formats[4]);

            var number = (value !== undefined ? value : element.value || element.innerHTML).toString();
            var decimalIndex = -1;

            // - When typing...
            if (value === undefined) {
                decimalIndex = !formats[3] ? 0 : // - no decimal places
                               !formats[4] ? formats[3] : // - fixed decimals
                               number.lastIndexOf(formats[2]) > -1 ? number.length - number.lastIndexOf(formats[2]) - 1 : // - optional decimals, using what exists
                               -1; // - or no decimals 
                if (decimalIndex > formats[3]) { decimalIndex = formats[3]; }
            }
                // - When bind/code...
            else if (number.lastIndexOf(".") > -1) {
                decimalIndex = number.length - number.lastIndexOf(".") - 1;
            }

            var isNegative = number.indexOf("-") > -1;

            // - Cleaning number.
            number = number.replace(/[^\d]/g, '');

            if (number !== "") {
                // - When typing, adding zeroes to the left if it's non-optional decimals.
                if (value === undefined && !formats[4] && number.length < formats[3]) {
                    number = number.padStart(formats[3], "0");
                    decimalIndex = number.length;
                }

                // - Adding decimal places.
                if (decimalIndex > -1) {
                    number = number.substr(0, number.length - decimalIndex) + "." + number.substr(number.length - decimalIndex);
                }

                number = parseFloat(number || 0).toFixed(formats[3] || 0);

                if (isNegative) { number = -number; }
                if (formats[4]) {
                    number = parseFloat(number).toString();
                }
                if (formats[2]) {
                    number = number.toString().replace(".", formats[2]);
                }

                if (formats[0]) {
                    var decimals = "";
                    if (formats[2] && number.indexOf(formats[2]) > -1) {
                        decimals = number.substr(number.indexOf(formats[2]));
                        number = number.substr(0, number.indexOf(formats[2]));
                    }

                    number = number.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1" + formats[1]) + decimals;
                }

                if (element.value && element.value.lastIndexOf(formats[2]) === element.value.length - 1) {
                    number += ",";
                }
            }

            value = number;
        }
        else {
            // - DATE                    
            var date = value !== undefined ? value : element.value || element.innerHTML;
            if (!date) { return date; }
            //console.log(value);
            //var value = new Date(date.replace(/-/g, '\/').replace(/T.+/, ''));
            //var data = new Date(date.replace(/-/g, '\/').replace(/T.+/, ''));
            var data = new Date(date);
            if (isNaN(data)) { return date; }

            value = format.replace("yyyy", data.getFullYear())
                          .replace("yy", data.getFullYear().toString().substr(2))
                          .replace("MM", ("00" + (data.getMonth() + 1)).toString().substr(-2))
                          .replace("dd", ("00" + data.getDate()).toString().substr(-2))
                          .replace("HH", ("00" + data.getHours()).toString().substr(-2))
                          .replace("mm", ("00" + data.getMinutes()).toString().substr(-2))
                          .replace("ss", ("00" + data.getSeconds()).toString().substr(-2));
        }
    }

    switch (element.nodeName.toUpperCase()) {
        case "INPUT":
        case "SELECT":
        case "TEXTAREA":
            {
                element.value = value;
                break;
            }
        default:
            {
                element.innerHTML = value;
                break;
            }
    }

    if (caret && element.selectionEnd) {
        element.selectionStart = element.selectionEnd = selectionStart + (element.value.match(/\./g) || []).length - offset;
    }

    return value;
};

function aplicarFormatacao(element, pattern) 
{
    if (!pattern) { return; }

    var selectionStart = element.selectionStart;
    var caret = element.value.length !== element.selectionStart;
    var value = element.value.replace(/[^\w]/g, '');

    for (var v = 0, p = 0; v < value.length; v++, p++) {
        if (p > pattern.length) {
            value = value.substr(0, pattern.length);
            break;
        }
        else if (/^[0-9]$/.test(pattern[p])) {
            if (!/^[0-9]$/.test(value[v])) {
                value = value.substr(0, v);
                break;
            }
        }
        else if (/^[a-zA-Z]$/.test(pattern[p])) {
            if (!/^[a-zA-Z]$/.test(value[v])) {
                value = value.substr(0, v);
                break;
            }
        }
        else {
            value = value.substr(0, v) + pattern[p] + value.substr(v, value.length);
            //v++; p++;
            if (selectionStart === p) { selectionStart++; }
        }
    }

    element.value = value;

    if (caret) {
        element.selectionStart = element.selectionEnd = selectionStart;
    }
}

function ObterChave(whichCode) {
    var key = '';

    if (whichCode >= 96 && whichCode <= 105) {
        //NumKeyPad
        key = String.fromCharCode(whichCode - 48); // Get key value from key code 
    }
    else {
        //keyboard
        key = String.fromCharCode(whichCode); // Get key value from key code 
    }
    return key;
}


function numbercurrencyFormat(number)
{
    var decimalplaces = 2;
    var decimalcharacter = ",";
    var thousandseparater = ".";
    number = parseFloat(number);
    var sign = number < 0 ? "-" : "";
    var formatted = new String(number.toFixed(decimalplaces));
    if( decimalcharacter.length && decimalcharacter != "," ) { formatted = formatted.replace(/\,/,decimalcharacter); }
    var integer = "";
    var fraction = "";
    var strnumber = new String(formatted);
    var dotpos = decimalcharacter.length ? strnumber.indexOf(decimalcharacter) : -1;
    if( dotpos > -1 )
    {
        if( dotpos ) { integer = strnumber.substr(0,dotpos); }
        fraction = strnumber.substr(dotpos+1);
    }
    else { integer = strnumber; }
    if( integer ) { integer = String(Math.abs(integer)); }
    while( fraction.length < decimalplaces ) { fraction += "0"; }
    temparray = new Array();
    while( integer.length > 3 )
    {
        temparray.unshift(integer.substr(-3));
        integer = integer.substr(0,integer.length-3);
    }
    temparray.unshift(integer);
    integer = temparray.join(thousandseparater);
    return sign + integer + decimalcharacter + fraction;
}



function moeda(valor, casas, separdor_decimal, separador_milhar) {

    var valor_total = parseInt(valor * (Math.pow(10, casas)));
    var inteiros = parseInt(parseInt(valor * (Math.pow(10, casas))) / parseFloat(Math.pow(10, casas)));
    var centavos = parseInt(parseInt(valor * (Math.pow(10, casas))) % parseFloat(Math.pow(10, casas)));

    if (centavos % 10 == 0 && centavos + "".length < 2) {
        centavos = centavos + "0";
    } else if (centavos < 10) {
        centavos = "0" + centavos;
    }

    var milhoes = parseInt(inteiros / 1000000);
    if (milhoes > 0) {
        var milhares = parseInt((inteiros - (milhoes * 1000000)) / 1000);
        inteiros = (inteiros - (milhoes * 1000000)) % 1000;
    } else {
        var milhares = parseInt(inteiros / 1000);
        inteiros = inteiros % 1000;
    }

    var retorno = "";

    if (milhoes > 0) {
        retorno = milhoes + "" + separador_milhar;

        if (milhares > 0) {
            retorno += milhares + "" + separador_milhar;
        } else if (milhares == 0) {
            milhares = "000";
        } else if (milhares < 10) {
            milhares = "00" + milhares;
        } else if (milhares < 100) {
            milhares = "0" + milhares;
        }

        if (inteiros == 0) {
            inteiros = "000";
        } else if (inteiros < 10) {
            inteiros = "00" + inteiros;
        } else if (inteiros < 100) {
            inteiros = "0" + inteiros;
        }

    } else if (milhares > 0) {
        retorno = milhares + "" + separador_milhar + "" + retorno
        if (inteiros == 0) {
            inteiros = "000";
        } else if (inteiros < 10) {
            inteiros = "00" + inteiros;
        } else if (inteiros < 100) {
            inteiros = "0" + inteiros;
        }
    }


    retorno += inteiros + "" + separdor_decimal + "" + centavos;

    return retorno;

}





 
function sumTotal(fld, milSep, decSep) {
    var texto = fld.value;
    var i = 0;
    var aux1 = ''
    var aux2 = '';
    for (i = 0; i < texto.length; i++) {
        if (texto.charAt(i) == decSep) {
            aux1 = texto.substr(texto.length - 3, 3);
            break;
        }
    }

    aux2 = texto.substr(0, texto.length - 3);

    var j = 0;
    var aux3 = '';

    for (j = 0; j < aux2.length; j++) {
        if ((aux2.length - j) % 3 == 0)
            aux3 += milSep;

        aux3 += aux2.charAt(j);
    }

    for (j = 0; j < aux1.length; j++) {
        aux3 += aux1.charAt(j);
    }

    if (aux3.charAt(0) == milSep)
        aux3 = aux3.substr(1, aux3.length - 1);

    fld.value = aux3;
}

function Cover(bottom, top, ignoreSize) {
    var location = Sys.UI.DomElement.getLocation(bottom);
    top.style.position = 'absolute';
    top.style.top = location.y + 'px';
    top.style.left = location.x + 'px';
    if (!ignoreSize) {
        top.style.height = bottom.offsetHeight + 'px';
        top.style.width = bottom.offsetWidth + 'px';
    }
}

function formataCampo(campo, Mascara, evento) {
    var boleanoMascara;

    var Digitato = evento.keyCode;
    exp = /\-|\.|\/|\(|\)| /g
    campoSoNumeros = campo.value.toString().replace(exp, "");

    var posicaoCampo = 0;
    var NovoValorCampo = "";
    var TamanhoMascara = campoSoNumeros.length;;

    if (Digitato != 8) { // backspace 
        for (i = 0; i <= TamanhoMascara; i++) {
            boleanoMascara = ((Mascara.charAt(i) == "-") || (Mascara.charAt(i) == ".")
                                                    || (Mascara.charAt(i) == "/"))
            boleanoMascara = boleanoMascara || ((Mascara.charAt(i) == "(")
                                                    || (Mascara.charAt(i) == ")") || (Mascara.charAt(i) == " "))
            if (boleanoMascara) {
                NovoValorCampo += Mascara.charAt(i);
                TamanhoMascara++;
            } else {
                NovoValorCampo += campoSoNumeros.charAt(posicaoCampo);
                posicaoCampo++;
            }
        }
        campo.value = NovoValorCampo;
        return true;
    } else {
        return true;
    }
}

function mascaraInteiro() {
    if (event.keyCode < 48 || event.keyCode > 57) {
        event.returnValue = false;
        return false;
    }
    return true;
}


function MascaraTelefone(tel) {
    if (mascaraInteiro(tel) == false) {
        event.returnValue = false;
    }
    return formataCampo(tel, '0000-0000', event);
}


function MascaraCelular(tel) {
    if (mascaraInteiro(tel) == false) {
        event.returnValue = false;
    }
    return formataCampo(tel, '00000-0000', event);
}



function wordCount(val) {
    var wom = val.match(/\S+/g);
    return {
        charactersNoSpaces: val.replace(/\s+/g, '').length,
        characters: val.length,
        words: wom ? wom.length : 0,
        lines: val.split(/\r*\n/).length
    };
}


var textarea = document.getElementById("text");
var result = document.getElementById("result");

textarea.addEventListener("input", function () {
    var v = wordCount(this.value);
    result.innerHTML = (
        "<br>Characters (no spaces):  " + v.charactersNoSpaces +
        "<br>Characters (and spaces): " + v.characters +
        "<br>Words: " + v.words +
        "<br>Lines: " + v.lines
    );
}, false);


