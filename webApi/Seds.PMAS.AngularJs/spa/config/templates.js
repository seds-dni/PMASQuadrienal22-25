angular.module('pmas').run(['$templateCache', function ($templateCache) {
    'use strict';

    $templateCache.put('template/footer.html',
        "<footer class=\"footer\" data-ng-hide=\"!authentication.IsAuthenticated\" data-ng-controller=\"indexController\"><div class=\"row\"> <img id=\"imgBandeiraBranca\" src=\"Content/site/images/gov-white.png\" /></div></footer>"
    );

    $templateCache.put('template/header.html',
      "<div class=\"header\" data-ng-hide=\"!authentication.IsAuthenticated\" data-ng-controller=\"indexController\"><div class=\"app-bar fixed-top  fg-black\" data-role=\"appbar\" data-ng-hide=\"!authentication.IsAuthenticated\"><a class=\"app-bar-element branding\"><strong>Plano Municipal da Assistência Social PMAS</strong></a> <span class=\"app-bar-divider\"></span><div class=\"app-bar-element place-right\">Bem Vindo Usuário {{authentication.userName}}! <a class=\"dropdown-toggle fg-black\"><span class=\"mif-cog\"></span></a></div></div><div class=\"row\"><div class=\"divide-nav\"><img id=\"imgBandeira\" src=\"Content/site/images/bandeira_menor.png\" /><img id=\"logoPmas\" src=\"Content/site/images/logoPMAS.png\" height=\"60\" /></div></div> <nav class=\"navbar navbar-inverse navbar-fixed-top\" role=\"navigation\"><div class=\"navbar-header\"><button type=\"button\" class=\"navbar-toggle\" data-toggle=\"collapse\" data-target=\"#bs-example-navbar-collapse-1\"><span class=\"sr-only\">Toggle navigation</span><span class=\"icon-bar\"></span><span class=\"icon-bar\"></span><span class=\"icon-bar\"></span></button></div><div class=\"collapse navbar-collapse\" id=\"bs-example-navbar-collapse-1\"></div></nav></div>"
    );


    //$templateCache.put('template/profile-menu.html',
    //  "<li class=\"btn-wave\" data-ui-sref-active=\"active\"><a data-ui-sref=\"pages.profile.profile-about\">About</a></li><li class=\"btn-wave\" data-ui-sref-active=\"active\"><a data-ui-sref=\"pages.profile.profile-timeline\">Timeline</a></li><li class=\"btn-wave\" data-ui-sref-active=\"active\"><a data-ui-sref=\"pages.profile.profile-photos\">Photos</a></li><li class=\"btn-wave\" data-ui-sref-active=\"active\"><a data-ui-sref=\"pages.profile.profile-connections\">Connections</a></li>"
    //);


    //$templateCache.put('template/carousel/carousel.html',
    //  "<div ng-mouseenter=\"pause()\" ng-mouseleave=\"play()\" class=\"carousel\" ng-swipe-right=\"prev()\" ng-swipe-left=\"next()\"><ol class=\"carousel-indicators\" ng-show=\"slides.length > 1\"><li ng-repeat=\"slide in slides | orderBy:'index' track by $index\" ng-class=\"{active: isActive(slide)}\" ng-click=\"select(slide)\"></li></ol><div class=\"carousel-inner\" ng-transclude></div><a class=\"left carousel-control\" ng-click=\"prev()\" ng-show=\"slides.length > 1\"><span class=\"zmdi zmdi-chevron-left\"></span></a> <a class=\"right carousel-control\" ng-click=\"next()\" ng-show=\"slides.length > 1\"><span class=\"zmdi zmdi-chevron-right\"></span></a></div>"
    //);


    //$templateCache.put('template/datepicker/day.html',
    //  "<table role=\"grid\" aria-labelledby=\"{{::uniqueId}}-title\" aria-activedescendant=\"{{activeDateId}}\" class=\"dp-table dpt-day\"><thead><tr class=\"tr-dpnav\"><th><button type=\"button\" class=\"pull-left btn-dp\" ng-click=\"move(-1)\" tabindex=\"-1\"><i class=\"zmdi zmdi-long-arrow-left\"></i></button></th><th colspan=\"{{::5 + showWeeks}}\"><button id=\"{{::uniqueId}}-title\" role=\"heading\" aria-live=\"assertive\" aria-atomic=\"true\" type=\"button\" ng-click=\"toggleMode()\" ng-disabled=\"datepickerMode === maxMode\" tabindex=\"-1\" class=\"w-100 btn-dp\"><div class=\"dp-title\">{{title}}</div></button></th><th><button type=\"button\" class=\"pull-right btn-dp\" ng-click=\"move(1)\" tabindex=\"-1\"><i class=\"zmdi zmdi-long-arrow-right\"></i></button></th></tr><tr class=\"tr-dpday\"><th ng-if=\"showWeeks\" class=\"text-center\"></th><th ng-repeat=\"label in ::labels track by $index\" class=\"text-center\"><small aria-label=\"{{::label.full}}\">{{::label.abbr}}</small></th></tr></thead><tbody><tr ng-repeat=\"row in rows track by $index\"><td ng-if=\"showWeeks\" class=\"text-center h6\"><em>{{ weekNumbers[$index] }}</em></td><td ng-repeat=\"dt in row track by dt.date\" class=\"text-center\" role=\"gridcell\" id=\"{{::dt.uid}}\" ng-class=\"::dt.customClass\"><button type=\"button\" class=\"w-100 btn-dp btn-dpday btn-dpbody\" ng-class=\"{'dp-today': dt.current, 'dp-selected': dt.selected, 'dp-active': isActive(dt)}\" ng-click=\"select(dt.date)\" ng-disabled=\"dt.disabled\" tabindex=\"-1\"><span ng-class=\"::{'dp-day-muted': dt.secondary, 'dp-day-today': dt.current}\">{{::dt.label}}</span></button></td></tr></tbody></table>"
    //);


    //$templateCache.put('template/datepicker/month.html',
    //  "<table role=\"grid\" aria-labelledby=\"{{::uniqueId}}-title\" aria-activedescendant=\"{{activeDateId}}\" class=\"dp-table\"><thead><tr class=\"tr-dpnav\"><th><button type=\"button\" class=\"pull-left btn-dp\" ng-click=\"move(-1)\" tabindex=\"-1\"><i class=\"zmdi zmdi-long-arrow-left\"></i></button></th><th><button id=\"{{::uniqueId}}-title\" role=\"heading\" aria-live=\"assertive\" aria-atomic=\"true\" type=\"button\" ng-click=\"toggleMode()\" ng-disabled=\"datepickerMode === maxMode\" tabindex=\"-1\" class=\"w-100 btn-dp\"><div class=\"dp-title\">{{title}}</div></button></th><th><button type=\"button\" class=\"pull-right btn-dp\" ng-click=\"move(1)\" tabindex=\"-1\"><i class=\"zmdi zmdi-long-arrow-right\"></i></button></th></tr></thead><tbody><tr ng-repeat=\"row in rows track by $index\"><td ng-repeat=\"dt in row track by dt.date\" class=\"text-center\" role=\"gridcell\" id=\"{{::dt.uid}}\" ng-class=\"::dt.customClass\"><button type=\"button\" class=\"w-100 btn-dp btn-dpbody\" ng-class=\"{'dp-selected': dt.selected, 'dp-active': isActive(dt)}\" ng-click=\"select(dt.date)\" ng-disabled=\"dt.disabled\" tabindex=\"-1\"><span ng-class=\"::{'dp-day-today': dt.current}\">{{::dt.label}}</span></button></td></tr></tbody></table>"
    //);


    //$templateCache.put('template/datepicker/popup.html',
    //  "<div><ul class=\"dropdown-menu\" dropdown-nested ng-if=\"isOpen\" ng-keydown=\"keydown($event)\" ng-click=\"$event.stopPropagation()\"><li ng-transclude></li><li ng-if=\"showButtonBar\" class=\"dp-actions clearfix\"><button type=\"button\" class=\"btn btn-link\" ng-click=\"select('today')\">{{ getText('current') }}</button> <button type=\"button\" class=\"btn btn-link\" ng-click=\"close()\">{{ getText('close') }}</button></li></ul></div>"
    //);


    //$templateCache.put('template/datepicker/year.html',
    //  "<table role=\"grid\" aria-labelledby=\"{{::uniqueId}}-title\" aria-activedescendant=\"{{activeDateId}}\" class=\"dp-table\"><thead><tr class=\"tr-dpnav\"><th><button type=\"button\" class=\"pull-left btn-dp\" ng-click=\"move(-1)\" tabindex=\"-1\"><i class=\"zmdi zmdi-long-arrow-left\"></i></button></th><th colspan=\"3\"><button id=\"{{::uniqueId}}-title\" role=\"heading\" aria-live=\"assertive\" aria-atomic=\"true\" type=\"button\" class=\"w-100 btn-dp\" ng-click=\"toggleMode()\" ng-disabled=\"datepickerMode === maxMode\" tabindex=\"-1\"><div class=\"dp-title\">{{title}}</div></button></th><th><button type=\"button\" class=\"pull-right btn-dp\" ng-click=\"move(1)\" tabindex=\"-1\"><i class=\"zmdi zmdi-long-arrow-right\"></i></button></th></tr></thead><tbody><tr ng-repeat=\"row in rows track by $index\"><td ng-repeat=\"dt in row track by dt.date\" class=\"text-center\" role=\"gridcell\" id=\"{{::dt.uid}}\"><button type=\"button\" class=\"w-100 btn-dp btn-dpbody\" ng-class=\"{'dp-selected': dt.selected, 'dp-active': isActive(dt)}\" ng-click=\"select(dt.date)\" ng-disabled=\"dt.disabled\" tabindex=\"-1\"><span ng-class=\"::{'dp-day-today': dt.current}\">{{::dt.label}}</span></button></td></tr></tbody></table>"
    //);


    //$templateCache.put('template/modal/bloquear_instituicao.html',
    //  "<div class=\"modal-header\"><h3 class=\"modal-title\">Confirmação para bloqueio!</h3></div><div class=\"modal-body\">Confirma o bloqueio da instituição selecionada?</div><div class=\"modal-footer\"><button type=\"button\" ng-click=\"cancel()\" class=\"btn btn-primary btn-sm m-t-5\">Voltar</button> <button type=\"button\" ng-click=\"ok()\" class=\"btn btn-primary btn-sm m-t-5\">Confirmar</button></div>"
    //);


    //$templateCache.put('template/modal/cancel.html',
    //  "<div class=\"modal-header\"><h3 class=\"modal-title\">Confirmação</h3></div><div class=\"modal-body\">Você deseja cancelar essa operação</div><div class=\"modal-footer\"><button type=\"button\" ng-click=\"cancel()\" class=\"btn btn-primary btn-sm m-t-5\">Cancelar</button> <button type=\"button\" ng-click=\"ok()\" class=\"btn btn-primary btn-sm m-t-5\">Confirmar</button></div>"
    //);


    //$templateCache.put('template/modal/delete.html',
    //  "<div class=\"modal-header\"><h3 class=\"modal-title\">Permissão para exclusão!</h3></div><div class=\"modal-body\">Confirma a exclusão do registro selecionado?</div><div class=\"modal-footer\"><button type=\"button\" ng-click=\"cancel()\" class=\"btn btn-primary btn-sm m-t-5\">Voltar</button> <button type=\"button\" ng-click=\"ok()\" class=\"btn btn-primary btn-sm m-t-5\">Confirmar</button></div>"
    //);


    //$templateCache.put('template/modal/desbloquear_instituicao.html',
    //  "<div class=\"modal-header\"><h3 class=\"modal-title\">Confirmação para desbloqueio!</h3></div><div class=\"modal-body\">Confirma o desbloqueio da instituição selecionada?</div><div class=\"modal-footer\"><button type=\"button\" ng-click=\"cancel()\" class=\"btn btn-primary btn-sm m-t-5\">Voltar</button> <button type=\"button\" ng-click=\"ok()\" class=\"btn btn-primary btn-sm m-t-5\">Confirmar</button></div>"
    //);


    //$templateCache.put('template/modal/desvincular-beneficiario.html',
    //  "<div class=\"modal-header\"><h3 class=\"modal-title\">Permissão para desvincular</h3></div><div class=\"modal-body\">Deseja realmente desvincular o beneficiário {{ nomeBeneficiario }}?</div><div class=\"modal-footer\"><button type=\"button\" ng-click=\"cancel()\" class=\"btn btn-primary btn-sm m-t-5\">Voltar</button> <button type=\"button\" ng-click=\"ok()\" class=\"btn btn-primary btn-sm m-t-5\">Confirmar</button></div>"
    //);


    //$templateCache.put('template/modal/district.html',
    //  "<div class=\"modal-header\"><h3 class=\"modal-title\">Adicionar Bairros</h3></div><div class=\"modal-body\"><div class=\"row\"><div id=\"groupCpf\" class=\"col-sm-12 m-b-30\"><div class=\"fg-line\"><label>Nome do bairro:<span class=\"required\">*</span></label><input type=\"text\" class=\"form-control input-sm\"></div><small class=\"help-block element-hide\"></small></div></div><div class=\"table-responsive\"><table class=\"table table-striped table-vmiddle ng-scope ng-table\"><thead class=\"ng-scope\"><tr><th title=\"\" class=\"header\" style=\"width: 80%\"><div class=\"ng-table-header ng-scope\"><span class=\"ng-binding sort-indicator\">Bairro</span></div></th><th title=\"\" class=\"header\" style=\"width: 90%\"><div class=\"ng-table-header ng-scope\"><span class=\"ng-binding sort-indicator\"></span></div></th><th title=\"\" class=\"header center\" style=\"width: 10%\"><div class=\"ng-table-header ng-scope\"><span class=\"ng-binding sort-indicator\">Excluir</span></div></th></tr></thead><tbody><tr><td class=\"ng-binding\">Território 1</td><td></td><td class=\"center\"><i ng-click=\"test.delete(1);\" class=\"zmdi zmdi-delete zmdi-hc-fw tm-icon-table click\" title=\"Excluir\"></i></td></tr><tr><td>Território 2</td><td></td><td class=\"center\"><i ng-click=\"test.delete(1);\" class=\"zmdi zmdi-delete zmdi-hc-fw tm-icon-table click\" title=\"Excluir\"></i></td></tr><tr><td class=\"ng-binding\">Território 3</td><td></td><td class=\"center\"><i ng-click=\"test.delete(1);\" class=\"zmdi zmdi-delete zmdi-hc-fw tm-icon-table click\" title=\"Excluir\"></i></td></tr><tr><td>Território 4</td><td></td><td class=\"center\"><i ng-click=\"test.delete(1);\" class=\"zmdi zmdi-delete zmdi-hc-fw tm-icon-table click\" title=\"Excluir\"></i></td></tr></tbody></table></div></div><div class=\"modal-footer\"><button type=\"button\" ng-click=\"cancel()\" class=\"btn btn-primary btn-sm m-t-5\">Voltar</button> <button type=\"button\" ng-click=\"ok()\" class=\"btn btn-primary btn-sm m-t-5\">Salvar</button></div>"
    //);


    //$templateCache.put('template/modal/download.html',
    //  "<div class=\"modal-header\"><h3 class=\"modal-title\">Permissão para download!</h3></div><div class=\"modal-body\">Confirma o download do registro selecionado?</div><div class=\"modal-footer\"><button type=\"button\" ng-click=\"cancel()\" class=\"btn btn-primary btn-sm m-t-5\">Voltar</button> <button type=\"button\" ng-click=\"ok()\" class=\"btn btn-primary btn-sm m-t-5\">Confirmar</button></div>"
    //);


    //$templateCache.put('template/modal/finish.html',
    //  "<div class=\"modal-header\"></div><div class=\"modal-body\">Deseja encerrar a sessão atual?</div><div class=\"modal-footer\"><button type=\"button\" ng-click=\"cancel()\" class=\"btn btn-primary btn-sm m-t-5\">Voltar</button> <button type=\"button\" ng-click=\"ok()\" class=\"btn btn-primary btn-sm m-t-5\">Confirmar</button></div>"
    //);


    //$templateCache.put('template/modal/password.html',
    //  "<div class=\"modal-header\"><h3 class=\"modal-title\">Permissão para alteração!</h3></div><div class=\"modal-body\">Confirma a alteração da senha?</div><div class=\"modal-footer\"><button type=\"button\" ng-click=\"cancel()\" class=\"btn btn-primary btn-sm m-t-5\">Voltar</button> <button type=\"button\" ng-click=\"ok()\" class=\"btn btn-primary btn-sm m-t-5\">Confirmar</button></div>"
    //);


    //$templateCache.put('template/modal/reativar_beneficiario_elegivel.html',
    //  "<div class=\"modal-header\"><h3 class=\"modal-title\">Confirmação para reativação!</h3></div><div class=\"modal-body\">O cidadão/família ficará apto para concorrer ao benefício do programa. Confirma a reativação?</div><div class=\"modal-footer\"><button type=\"button\" ng-click=\"cancel()\" class=\"btn btn-primary btn-sm m-t-5\">Voltar</button> <button type=\"button\" ng-click=\"ok()\" class=\"btn btn-primary btn-sm m-t-5\">Confirmar</button></div>"
    //);


    //$templateCache.put('template/modal/reativar_pessoa_beneficiario.html',
    //  "<div class=\"modal-header\"><h3 class=\"modal-title\">Confirmação para reativação!</h3></div><div class=\"modal-body\">O cidadão/família retornará para a lista de beneficiários do programa. Confirma a reativação?</div><div class=\"modal-footer\"><button type=\"button\" ng-click=\"cancel()\" class=\"btn btn-primary btn-sm m-t-5\">Voltar</button> <button type=\"button\" ng-click=\"ok()\" class=\"btn btn-primary btn-sm m-t-5\">Confirmar</button></div>"
    //);


    //$templateCache.put('template/modal/territory.html',
    //  "<div class=\"modal-header\"><h3 class=\"modal-title\">Territórios</h3></div><div class=\"modal-body\"><div class=\"table-responsive\"><table class=\"table table-striped table-vmiddle ng-scope ng-table\"><thead class=\"ng-scope\"><tr><th title=\"\" class=\"header\" style=\"width: 30%\"><div class=\"ng-table-header ng-scope\"><span class=\"ng-binding sort-indicator\">Território</span></div></th><th title=\"\" class=\"header\" style=\"width: 30%\"><div class=\"ng-table-header ng-scope\"><span class=\"ng-binding sort-indicator\">Programa</span></div></th><th title=\"\" class=\"header center\" style=\"width: 20%\"><div class=\"ng-table-header ng-scope\"><span class=\"ng-binding sort-indicator\">Meta</span></div></th><th title=\"\" class=\"header center\" style=\"width: 10%\"><div class=\"ng-table-header ng-scope\"><span class=\"ng-binding sort-indicator\">Alterar</span></div></th><th title=\"\" class=\"header center\" style=\"width: 10%\"><div class=\"ng-table-header ng-scope\"><span class=\"ng-binding sort-indicator\">Excluir</span></div></th></tr></thead><tbody><tr><td class=\"ng-binding\">Território 1</td><td>Programa 1</td><td class=\"center\">15</td><td class=\"center\"><i class=\"zmdi zmdi-edit zmdi-hc-fw tm-icon-table click\"></i></td><td class=\"center\"><i ng-click=\"test.delete(1);\" class=\"zmdi zmdi-delete zmdi-hc-fw tm-icon-table click\" title=\"Excluir\"></i></td></tr><tr><td>Território 2</td><td>Programa 2</td><td class=\"center\">11</td><td class=\"center\"><i class=\"zmdi zmdi-edit zmdi-hc-fw tm-icon-table click\"></i></td><td class=\"center\"><i ng-click=\"test.delete(1);\" class=\"zmdi zmdi-delete zmdi-hc-fw tm-icon-table click\" title=\"Excluir\"></i></td></tr><tr><td class=\"ng-binding\">Território 3</td><td>Programa 3</td><td class=\"center\">32</td><td class=\"center\"><i class=\"zmdi zmdi-edit zmdi-hc-fw tm-icon-table click\"></i></td><td class=\"center\"><i ng-click=\"test.delete(1);\" class=\"zmdi zmdi-delete zmdi-hc-fw tm-icon-table click\" title=\"Excluir\"></i></td></tr><tr><td>Território 4</td><td>Programa 4</td><td class=\"center\">16</td><td class=\"center\"><i class=\"zmdi zmdi-edit zmdi-hc-fw tm-icon-table click\"></i></td><td class=\"center\"><i ng-click=\"test.delete(1);\" class=\"zmdi zmdi-delete zmdi-hc-fw tm-icon-table click\" title=\"Excluir\"></i></td></tr></tbody></table></div></div><div class=\"modal-footer\"><button type=\"button\" ng-click=\"cancel()\" class=\"btn btn-primary btn-sm m-t-5\">Voltar</button> <button type=\"button\" ng-click=\"ok()\" class=\"btn btn-primary btn-sm m-t-5\">Confirmar</button></div>"
    //);


    //$templateCache.put('template/pagination/pager.html',
    //  "<ul class=\"pager\"><li ng-class=\"{disabled: noPrevious(), previous: align}\"><a href ng-click=\"selectPage(page - 1, $event)\">Previous</a></li><li ng-class=\"{disabled: noNext(), next: align}\"><a href ng-click=\"selectPage(page + 1, $event)\">Next</a></li></ul>"
    //);


    //$templateCache.put('template/pagination/pagination.html',
    //  "<ul class=\"pagination\"><li ng-if=\"boundaryLinks\" ng-class=\"{disabled: noPrevious()}\"><a href ng-click=\"selectPage(1, $event)\"><i class=\"zmdi zmdi-more-horiz\"><i></i></i></a></li><li ng-if=\"directionLinks\" ng-class=\"{disabled: noPrevious()}\"><a href ng-click=\"selectPage(page - 1, $event)\"><i class=\"zmdi zmdi-chevron-left\"></i></a></li><li ng-repeat=\"page in pages track by $index\" ng-class=\"{active: page.active}\"><a href ng-click=\"selectPage(page.number, $event)\">{{page.text}}</a></li><li ng-if=\"directionLinks\" ng-class=\"{disabled: noNext()}\"><a href ng-click=\"selectPage(page + 1, $event)\"><i class=\"zmdi zmdi-chevron-right\"></i></a></li><li ng-if=\"boundaryLinks\" ng-class=\"{disabled: noNext()}\"><a href ng-click=\"selectPage(totalPages, $event)\"><i class=\"zmdi zmdi-more-horiz\"><i></i></i></a></li></ul>"
    //);


    //$templateCache.put('template/tabs/tabset.html',
    //  "<div class=\"clearfix\"><ul class=\"tab-nav\" ng-class=\"{'tn-vertical': vertical, 'tn-justified': justified, 'tab-nav-right': right}\" ng-transclude></ul><div class=\"tab-content\"><div class=\"tab-pane\" ng-repeat=\"tab in tabs\" ng-class=\"{active: tab.active}\" tab-content-transclude=\"tab\"></div></div></div>"
    //);

}]);
