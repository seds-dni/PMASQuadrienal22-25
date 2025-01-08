(function () {
    'use strict';

    angular
        .module('pmas')
        .factory('apiFactory', apiFactory)
        //.factory('guid', guid);

    function apiFactory($resource, serviceBase) {
        return $resource(serviceBase + '/:url/:action', { url: '@url', action: '@action' }, {
            //'register': { params: { action: 'inserir' }, method: 'POST' },
            //'update': { params: { action: 'alterar' }, method: 'PUT' },
            //'find': { method: 'GET' },
            //'search': { params: { action: 'pesquisar' }, method: 'GET' },
            //'procurar': { params: { action: 'procurar' }, method: 'GET' }, // Foi criado propositalmente
            //'delete': { params: { action: 'excluir' }, method: 'DELETE' },
            //'deleteRelatorios': { params: { action: 'excluirRelatorios' }, method: 'PUT' },
            //'deleteBairros': { params: { action: 'excluirBairros' }, method: 'POST' }, // Foi criado propositalmente, pois o tipo PUT não excluir os bairros quando esse tem muitos registros
            //'attribute': { params: { action: 'atributos' }, method: 'GET' },
            //'logicDelete': { params: { action: 'excluir' }, method: 'PUT' },
            //'userAuthenticated': { params: { action: 'usuariologado' }, method: 'GET' },
            //'updatePassword': { params: { action: 'alterarsenha' }, method: 'PUT' },
            'login': { params: { action: 'autenticacao' }, method: 'POST' }
            //,
            //'recoverPassword': { params: { action: 'recuperarsenha' }, method: 'POST' },
            //'baixarArquivo': { params: { action: 'obterArquivo' }, method: 'GET' },
            //'obterRelatoriosFixos': { params: { action: 'obterRelatoriosFixos' }, method: 'GET' },
            //'statusPrograma': { params: { action: 'status' }, method: 'GET' },
            //'ordenacao': { params: { action: 'ordenacao' }, method: 'GET' },
            //'validate': { params: { action: 'validar' }, method: 'POST' },
            //'validarExclusao': { params: { action: 'validarExclusao' }, method: 'POST' },
            //'obterCiclosInstituicao': { params: { action: 'obterciclosinstituicao' }, method: 'GET' },
            //'obterProgramasInstituicao': { params: { action: 'obterprogramasinstituicao' }, method: 'GET' },
            //'atualizarMetasManuais': { params: { action: 'atualizarmetasmanuais' }, method: 'PUT' },
            //'alterarStatusBloqueado': { params: { action: 'alterarstatusbloqueado' }, method: 'PUT' },
            //'desvincular': { params: { action: 'desvincular' }, method: 'PUT' },
            //'validarDesvinculacao': { params: { action: 'validarDesvinculacao' }, method: 'PUT' },
            //'reativar': { params: { action: 'reativar' }, method: 'PUT' },
            //'validarReativacao': { params: { action: 'validarReativacao' }, method: 'PUT' },
            //'vincularManualmente': { params: { action: 'vinculacarmanualmente' }, method: 'PUT' },
            //'obterprogramaselegiveis': { params: { action: 'obterprogramaselegiveis' }, method: 'GET' },
            //'instituicaopesquisabairrociclo': { params: { action: 'pesquisarbairrociclo' }, method: 'GET' },
            //'vinculomanual': { params: { action: 'vinculomanual' }, method: 'PUT' },
            //'mover': { params: { action: 'mover' }, method: 'PUT' },
            //'pesquisarbairros': { params: { action: 'pesquisarbairros' }, method: 'GET' },
            //'verificaReativar': { params: { action: 'verificareativar' }, method: 'GET' },
            //'download': { params: { action: 'download' }, method: 'GET' }
        });
    };

    //function guid() {
    //    return {
    //        generate: function () {

    //            function s4() {
    //                return Math.floor((1 + Math.random()) * 0x10000)
    //                    .toString(16)
    //                    .substring(1);
    //            };

    //            return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
    //                s4() + '-' + s4() + s4() + s4();
    //        }
    //    };
    //};
}());