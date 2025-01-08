(function () {

    'use strict';
    angular.module("pmas").controller('indexController', ['$scope', '$location', 'authData', 'userService', 'dataService', function ($scope, $location, authData, userService, dataService) {

        $scope.logOut = function () {
            loginService.logOut();
            $location.path('/login');
        }

        $scope.authentication = authData.authenticationData;
        dataService.GetUsuarioLogado().then(function (data) {
            $scope.usuariologado = data;
            console.log(data);
            $scope.SiteMenu = [];
            $scope.SiteMenu = data.funcionalidades;
            var menu = buildMenu(data.funcionalidades);
            authData.authenticationData.IsAuthenticated = true;
            var teste = $('#bs-example-navbar-collapse-1').html(menu);
            function buildMenu(menus) {
                var rootMenu = [];
                var childMenu = [];
                var rootIndex = 0;
                var childIndex = 0;
                for (i = 0; i < menus.length; i++) {
                    var menu = menus[i];
                    if (menu.idPai == null) {
                        rootMenu[rootIndex] = menu;
                        rootIndex++;
                    } else {
                        childMenu[childIndex] = menu;
                        childIndex++;
                    }
                }
                var generated = "<ul class='nav navbar-nav'>";
                for (var i = 0; i < rootMenu.length; i++) {
                    var menuPai = rootMenu[i];
                    var generatedMenu = buildChildMenu(menuPai, childMenu, generated);
                    generated = generatedMenu;
                }
                return generated + "</ul>";
            }
            function buildChildMenu(menuPai, childMenu, generatedMenu) {
                var dataflexOrder = menuPai.ordem;
                var dataflexOrigin = menuPai.ordem - 1;
                var IdPai = menuPai.id;
                if (menuPai.idPai == null) {
                    generatedMenu = generatedMenu + "<li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>" + menuPai.nome + "</a><ul class='dropdown-menu'>";
                }
                else {
                    generatedMenu = generatedMenu + "<li class='divider'></li><li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>" + menuPai.nome + "</a><ul class='dropdown-menu'>";
                }
                for (var j = 0; j < childMenu.length; j++) {
                    var child = childMenu[j];
                    if (IdPai == child.idPai) {
                        if (isHaveChild(child.id, childMenu)) {
                            generatedMenu = generatedMenu + "<li>";
                            generatedMenu = buildChildSubMenu(child, childMenu, generatedMenu) + "</li></ul>";
                        }
                        else {
                            generatedMenu = generatedMenu + "<li><a href='" + child.pagina + "'>" + child.nome + "</a></li>";
                        }
                    }
                }
                if (menuPai.idPai == null) {
                    generatedMenu = generatedMenu + "</ul>";
                } else {
                    generatedMenu = generatedMenu + "</li>";
                }
                return generatedMenu;
            }
            function isHaveChild(parentId, childMenu) {
                for (var k = 0; k < childMenu.length; k++) {
                    if (childMenu[k].idPai == parentId) return true;
                }
                return false;
            }
            function buildChildSubMenu(menuPai, childMenu, generatedMenu) {
                var dataflexOrder = menuPai.ordem;
                var dataflexOrigin = menuPai.ordem - 1;
                var IdPai = menuPai.id;
                if (menuPai.idPai == null) {
                    generatedMenu = generatedMenu + "<li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>" + menuPai.nome + "</a><ul class='dropdown-menu'>";
                }
                else {
                    generatedMenu = generatedMenu + "<li class='divider'></li><li class='dropdown-submenu'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>" + menuPai.nome + "</a><ul class='dropdown-menu'>";
                }
                for (var j = 0; j < childMenu.length; j++) {
                    var child = childMenu[j];
                    if (IdPai == child.idPai) {
                        //console.log(child);
                        generatedMenu = generatedMenu + "<li><a href='" + child.pagina + "' class='dropdown-submenu' data-toggle='dropdown'>" + child.nome + "</a>";
                        // "<li><a href=>" + child.nome + "</a></li>";
                        //if (isHaveChild(child.id, childMenu)) {
                        //    generatedMenu = generatedMenu + "<li>";
                        //    generatedMenu = buildChildSubMenu(child, childMenu, generatedMenu) + "</li></ul>";
                        //}
                        //else {
                        //    
                        //}
                    }
                }
                if (menuPai.idPai == null) {
                    generatedMenu = generatedMenu + "</ul>";
                } else {
                    generatedMenu = generatedMenu + "</li>";
                }
                return generatedMenu;
            }
            $location.path('/home');
        }, function (error) {
            $location.path('/login');
            console.log(error);
            $scope.message = error.error_description;
        })



    }]);
})();