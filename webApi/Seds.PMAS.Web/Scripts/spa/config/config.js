'use strict';

var urlServiceAutentication;
var urlServiceRecoverPassword;
var tempoAlertas = 10000;

(function () {
    var httpResquest = new XMLHttpRequest();

    httpResquest.onreadystatechange = function () {
        if (httpResquest.readyState == 4) {
            if (httpResquest.status == 200) {
                var data = JSON.parse(httpResquest.responseText);

                hostApi += data.host;

                if (!!data.port) {
                    hostApi += ':' + data.port;
                }
                hostApi += ':' + data.port;

                urlServiceAutentication = hostApi + '/login';
            }
        }
    };
    httpResquest.open('GET', 'config.json');
    httpResquest.send();
}());