(function () {
    'use strict';

    angular
        .module('app')
        .controller('copaFilmesController', copaFilmesController);

    copaFilmesController.$inject = ['$scope'];

    function copaFilmesController($scope) {
        $scope.title = 'copaFilmesController';

        activate();

        function activate() { }
    }
})();
