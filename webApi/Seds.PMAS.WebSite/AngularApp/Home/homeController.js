(function (app) {
    var homeController = function ($scope) {
        $scope.message = "Bem Vindo ao PMAS BETA teste";
    };
    app.controller('homeController', ["$scope", homeController]);
}(angular.module("pmas")));