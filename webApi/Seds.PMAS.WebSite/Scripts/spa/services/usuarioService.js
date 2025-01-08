(function (app) {
    var usuarioService = function ($http, usuarioApiUrl) {
        var result;
        var getUsuario = function () {
            return $http.get(usuarioApiUrl);
        };


        var atualizar = function (usuario) {
            return $http.put(usuarioApiUrl + usuario.Id, usuario);
        };

        var criar = function (usuario) {
            console.log(usuario);
            return $http.post(usuarioApiUrl, usuario);
        };

        var deletar = function (usuario) {
            return $http.delete(usuario);
        };

        return {
            getUsuario: getUsuario,
            atualizar: atualizar,
            criar: criar,
            deletar: deletar
        };
    };
    app.factory("usuarioService", usuarioService);
}(angular.module("pmas")));