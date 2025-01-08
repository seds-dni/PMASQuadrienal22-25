//Os controllers do Angular são diferentes dos controllers da aplicação ASP .NET MVC que apenas processam uma requisição e não mantém estad
//O código usa novamente angular.module mas não para criar um módulo mas para obter uma referência ao módulo existente chamado 'filmoteca' que criamos no arquivo filmoteca.js.
//O código passa a referência do módulo para a função como uma variável chamada app.
(function (app) {

    var listaController = function ($scope, prefeitoService) {
        //No arquivo filmeService.js criamos um serviço e estamos encapsulando os recursos da Web API FilmeController de forma 
        //que agora o nossos controllers AngularJS não vão precisar usar o serviço $http diretamente.
        prefeitoService
         .getPrefeito()
         .success(function (data) {
             $scope.prefeito = data;
             $scope.prefeito = angular.copy($scope.prefeito);
         });

        $scope.criar = function () {
            $scope.editar = {
                prefeito: {
                    Nome: "",
                    RG: "",
                    DataEmissao: "",
                    SiglaEmissor: "",
                    UF: "",
                    InicioMandato: "",
                    TerminoMandato: "",
                    Email: ""
                }
            };
        };

        //$scope.deleta = function (filme) {
        //    filmeService.deletar(filme)
        //    .success(function () {
        //        removerFilmePorId(filme.Id);
        //    });
        //};
        //var removerFilmePorId = function (id) {
        //    for (var i = 0; i < $scope.filmes.length; i++) {
        //        if ($scope.filmes[i].Id == id) {
        //            $scope.filmes.splice(i, 1);
        //            break;
        //        }
        //    }
        //};
    };
    app.controller('listaController', listaController);
}(angular.module("pmas")));
