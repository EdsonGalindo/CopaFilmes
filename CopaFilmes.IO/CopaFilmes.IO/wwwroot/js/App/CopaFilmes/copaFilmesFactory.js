(function () {
    'use strict';

    angular
        .module('app')
        .factory('copaFilmesFactory', copaFilmesFactory);

    copaFilmesFactory.$inject = ['$http'];

    function copaFilmesFactory($http) {
        var service = {
            getData: getData
        };

        return service;

        function getData() { }
    }
})();