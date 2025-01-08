
(function (app) {
    var prefeitoService = function ($http, prefeitoApiUrl) {
        var result;
        var getPrefeito = function () {
            return $http.get(prefeitoApiUrl);
        };


        var atualizar = function (prefeito) {
            return $http.put(prefeitoApiUrl + prefeito.Id, prefeito);
        };

        var criar = function (prefeito) {
            console.log(prefeito);
            return $http.post(prefeitoApiUrl, prefeito);
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