//Os controllers do Angular são diferentes dos controllers da aplicação ASP .NET MVC que apenas processam uma requisição e não mantém estad
//O código usa novamente angular.module mas não para criar um módulo mas para obter uma referência ao módulo existente chamado 'filmoteca' que criamos no arquivo filmoteca.js.
//O código passa a referência do módulo para a função como uma variável chamada app.
(function (app) {
    var menuController = function ($scope, $rootScope, menuService) {
        //No arquivo menuservice.js criamos um serviço e estamos encapsulando os recursos da Web API menuController de forma 
        //que agora o nossos controllers AngularJS não vão precisar usar o serviço $http diretamente.
        menuService
         .listarMenu()
        .success(function (data) {
            $scope.SiteMenu = [];
            $scope.SiteMenu = data;
            var menu = buildMenu(data);
            var teste = $('#bs-example-navbar-collapse-1').html(menu);
        });
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


    };


    app.controller('menuController', menuController);
}(angular.module("pmas")));