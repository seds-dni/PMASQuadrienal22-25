
(function (app) {
    var prefeituraController = function ($scope, prefeitoService) {
        //No arquivo prefeitoService.js criamos um serviço e estamos encapsulando os recursos da Web API prefeitoController de forma 
        //que agora o nossos controllers AngularJS não vão precisar usar o serviço $http diretamente.


        $scope.criar = prefeitoService
       .getPrefeito()
       .success(function (data) {
           $scope.prefeito = data;
           $scope.editar = {
               prefeito:
                   {
                       Nome: $scope.prefeito.nome,
                       RG: $scope.prefeito.rg,
                       DataEmissao: $scope.prefeito.dataEmissao,
                       SiglaEmissor: $scope.prefeito.siglaEmissor,
                       InicioMandato: $scope.prefeito.inicioMandato,
                       TerminoMandato: $scope.prefeito.terminoMandato,
                       CPF: $scope.prefeito.cpf,
                       Email: $scope.prefeito.email
                   }

           };
           $scope.status = {
               type: "info",
               message: "ready",
               busy: false
           };
       }).error(function () {
           $scope.editar = {
               prefeito: {
                   Nome: "",
                   RG: "",
                   DataEmissao: "",
                   SiglaEmissor: "",
                   UF: "",
                   InicioMandato: "",
                   TerminoMandato: "",
                   Email: "",
                   Status: 1,
                   IdPrefeitura: 1
               }
           }
           $scope.status = {
               type: "info",
               message: "ready",
               busy: true
           };
       });

        $scope.cancelar = function () {
            $scope.editar.prefeito = null;
        };
        $scope.salvar = function () {
            if ($scope.editar.prefeito.Id) {
                atualizaPrefeito();
            } else {
                criaPrefeito();
            }
        };
        var atualizaPrefeito = function () {

            prefeitoService.atualizar($scope.editar.prefeito)
            .success(function () {
                angular.extend($scope.prefeito, $scope.editar.prefeito);
                $scope.editar.prefeito = null;
            });
        };
        var criaPrefeito = function () {

            $scope.editar.prefeito.InicioMandato = $('#InicioMandato').val();
            $scope.editar.prefeito.TerminoMandato = $('#TerminoMandato').val();
            $scope.editar.prefeito.DataEmissao = $('#DataEmissao').val();
            $scope.editar.prefeito.Status = 1;
            $scope.editar.prefeito.IdPrefeitura = 1;
            prefeitoService.criar($scope.editar.prefeito)
            .success(function (prefeito) {
                showMetroDialog('#dialog3');
                $scope.prefeitos.push(prefeito);
                $scope.editar.prefeito = null;
            }).error(function (prefeito) {
                $scope.modelState = prefeito.modelState;
                var error = $scope.modelState;
                showMetroDialog('#dialog4');

                //for (var i = 0; i < error.lenght; i++) {
                //    var log = error[i];
                //    console.log(log.prefeito);
                //}
                var errors = [];
                for (var key in error) {
                    console.log(error);
                    for (var i = 0; i < error[key].length; i++) {
                        console.log(error.ModelState[key][i]);
                        errors.push(error.ModelState[key][i]);
                    }
                }
                $scope.errors = errors;
                console.log($scope.errors);
            });
        };
    };
    app.controller("prefeitoController", prefeitoController);
}(angular.module("pmas")));




//GET
//angular.module('pmas').controller('prefeitoController', prefeitoController)