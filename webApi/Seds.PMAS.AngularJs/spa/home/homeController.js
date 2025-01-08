//(function (app) {
//    'use strict';

//    app.controller('homeController', homeController);
//    $scope.message = "Bem Vindo ao PMAS BETA";
//    homeController.$inject = ['$scope']
//    function homeController($scope) {

//    };
//    app.controller('homeController', ["$scope", homeController]);
//})(angular.module('pmas'));

//(function () {

//    'use strict';
//    app.controller('homeController', ['$scope', 'authData', function ($scope, authData) {
//        $scope.message = "Bem Vindo ao PMAS BETA";
//        $scope.authentication = authData.authenticationData;
//    }]);
//})();


(function () {
    'use strict';
    angular.module("pmas").controller('homeController', ['$scope', 'dataService', function ($scope, dataService) {
        //FETCH DATA FROM SERVICES
        $scope.message = "Bem vindo ao PMAS Beta";
        //dataService.GetAuthenticateData().then(function (data) {
        //    console.log(data);
        //    $scope.message = data;
        //    $scope.data = data;
        //})
    }])
})();