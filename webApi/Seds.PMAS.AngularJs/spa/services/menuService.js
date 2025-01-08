//Os services do AngularJS são objetos substituíveis que estão ligados entre si usando a injeção de dependência(DI). 
//Você pode usar serviços do Angular para organizar e compartilhar código (reuso) em seu aplicativo.
//O Angular oferece vários serviços úteis (como $http, $log, $route, $location, $window, etc), mas podemos criar serviços de acordo com a necessidade da nossa aplicação.

//1-Service : Quando você estiver usando um Service, ele é instanciado com a palavra-chave "new". Por causa disso, você vai adicionar propriedades a "this" e o serviço retornará "this". Quando você passar o serviço ao seu controlador, essas propriedades em "this" agora estarão disponíveis no controlador através de seu serviço.

//2-Factory (a forma mais popular de criar services) :  Ao usar um Factory você cria um objeto, adiciona propriedades a ele , e então retorna o mesmo objeto. Quando você passar este serviço ao seu controlador, as propriedades do objeto agora estarão disponíveis no controlador através de sua factory.

//3- Provider : Os provedores são o único serviço que você pode passar para a função config(). Use um Provider quando você desejar fornecer configuração ao módulo para o objeto de serviço antes de torná-lo disponível.
(function () {
    'use strict';
    angular.module("pmas").factory('menuService', ['$http', 'serviceBase', 'dataService', function ($http, serviceBase, dataService) {
        var user = dataService.GetUsuarioLogado();
        var menu = {};
        menu.listarMenu = function () {
            return $http.get(serviceBase + '/api/recurso', user).then(function (response) {
                console.log(response);
                return response.data;
            })
        }
        //return {
        //    listarMenu: listarMenu
        //};
        return menu;
    }])

})();