
(function (app) {
    var usuarioController = function ($scope, usuarioService) {
        $scope.criar = usuarioService
       .getUsuario()
       .success(function (data) {
           $scope.usuario = data;
           $scope.editar = {
               usuario:
                   {
                       Perfil: $scope.usuario.Perfil,
                       Drads: $scope.usuario.Drads,
                       Municipio: $scope.usuario.Municipio,
                       Nome: $scope.usuario.Nome,
                       Email: $scope.usuario.Email,
                       RG: $scope.usuario.RG,
                       OrgaoEmissor: $scope.usuario.OrgaoEmissor,
                       UF: $scope.prefeito.UF,
                       CPF: $scope.usuario.CPF,
                       CEP: $scope.usuario.CEP,
                       Endereco: $scope.usuario.Endereco,
                       Numero: $scope.usuario.Numero,
                       Complemento: $scope.usuario.Complemento,
                       Bairro: $scope.usuario.Bairro,
                       Cidade: $scope.usuario.Cidade,
                       Telefone: $scope.usuario.Telefone,
                       Instituicao: $scope.usuario.Instituicao,
                       Cargo: $scope.usuario.Cargo,
                       Login: $scope.usuario.Login,
                       Situacao: $scope.usuario.Situacao
                   }
           };
           $scope.status = {
               type: "info",
               message: "ready",
               busy: false
           };
       }).error(function () {
           $scope.editar = {
               usuario:
                  {
                      Perfil: '',
                      Drads: '',
                      Municipio: '',
                      Nome: '',
                      Email: '',
                      RG: '',
                      OrgaoEmissor: '',
                      UF: '',
                      CPF: '',
                      CEP: '',
                      Endereco: '',
                      Numero: '',
                      Complemento: '',
                      Bairro: '',
                      Cidade: '',
                      Telefone: '',
                      Instituicao: '',
                      Cargo: '',
                      Login: '',
                      Situacao: ''

                  }
           }
           $scope.status = {
               type: "info",
               message: "ready",
               busy: true
           };
       });

        $scope.cancelar = function () {
            $scope.editar.usuario = null;
        };
        $scope.salvar = function () {
            if ($scope.editar.usuario.Id) {
                atualizaPrefeito();
            } else {
                criaPrefeito();
            }
        };
        var atualizaPrefeito = function () {

            prefeitoService.atualizar($scope.editar.usuario)
            .success(function () {
                angular.extend($scope.prefeito, $scope.editar.usuario);
                $scope.editar.usuario = null;
            });
        };
        var criaUsuario = function () {

            prefeitoService.criar($scope.editar.usuario)
            .success(function (usuario) {
                showMetroDialog('#dialog3');
                $scope.usuario.push(usuario);
                $scope.editar.usuario = null;
            }).error(function (usuario) {
                $scope.modelState = usuario.modelState;
                var error = $scope.modelState;
                showMetroDialog('#dialog4');
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
    app.controller("usuarioController", usuarioController);
}(angular.module("pmas")));


