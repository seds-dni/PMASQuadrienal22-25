(function () {
    'use strict';
    angular.module("pmas").factory('dataService', ['$http', 'serviceBase', function ($http, serviceBase) {
        var fac = {};
        fac.GetUsuarioLogado = function () {
            return $http.get(serviceBase + '/api/usuario').then(function (response) {
                return response.data;
            })
        }
        fac.GetAuthenticateData = function () {
            return $http.get(serviceBase + '/api/data/authenticate').then(function (response) {
                return response.data;
            })
        }
        fac.GetAuthorizeData = function () {
            return $http.get(serviceBase + '/api/data/authorize').then(function (response) {
                return response.data;
            })
        }
        return fac;
    }])

})();