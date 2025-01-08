(function (app) {
    'use strict';

    app.controller('homeController', homeController);

    homeController.$inject = ['$scope']
    function homeController($scope) {
        $scope.message = "Bem Vindo ao PMAS BETA";
    };
    app.controller('homeController', ["$scope", homeController]);
})(angular.module('pmas'));