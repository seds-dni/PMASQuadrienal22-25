(function (app) {
    var prefeitoService = function ($http, prefeitoApiUrl) {
        var result;
        var getPrefeito = function () {
            return $http.get(prefeitoApiUrl);
        };
        var atualizar = function (prefeito) {
            return $http.put(prefeitoApiUrl, prefeito);
        };
        var criar = function (prefeito) {
            result = $http.post(prefeitoApiUrl, prefeito).then(function (data, status) {
                result = (data);
                console.log(result);
            }).catch(function () {
                alert("Ocorreu um erro, prefeito não cadastrado");
            });
            return result;
        };
        var deletar = function (prefeito) {
            return $http.delete(prefeito);
        };
        return {
            getPrefeito: getPrefeito,
            atualizar: atualizar,
            criar: criar,
            deletar: deletar
        };
    };
    app.factory("prefeitoService", prefeitoService);
}(angular.module("pmas")));


//(function (app) {
//    'use strict';
//    app.factory('prefeitoService', prefeitoService);

//    var prefeitoService = function ($http, $q, $log) {
//        var chachedPrefeito;

//        var singleProject = function (id) {
//            return $http.get("http://localhost:51735/api/prefeito/")
//                        .then(function (serviceResp) {
//                            return serviceResp.data;
//                        });
//        };

//        //var result;
//        var getPrefeito = function () {
//            return $http.get("http://localhost:51735/api/prefeito/");
//        };


//        var atualizar = function (prefeito) {
//            return $http.put(prefeitoApiUrl + prefeito.Id, prefeito);
//        };

//        var criar = function (prefeito) {
//            console.log(prefeito);
//            return $http.post(prefeitoApiUrl, prefeito);
//        };

//        var deletar = function (prefeito) {
//            return $http.delete(prefeito);
//        };

//        return {
//            getPrefeito: getPrefeito,
//            atualizar: atualizar,
//            criar: criar,
//            deletar: deletar
//        };
//    };
//    app.factory("prefeitoService", prefeitoService);
//}(angular.module("pmas")));