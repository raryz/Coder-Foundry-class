(function () {
    var app = angular.module('app');

    app.controller('ctrl', ['svc', function (svc) {      // svc is the name we are giving to the service, matches name 
        var scope = this;                                // in the service.js file
        scope.selected = {
            year: '',
            make: '',
            model: '',
            trim: ''
        };

        scope.options = {
            years: [],
            makes: [],
            models: [],
            trims: []
        }

        scope.getYears = function () {
            svc.getYears().then(function (result) {
                scope.options.years = result;
            })
        }

        scope.getMakes = function () {

            scope.selected.make = '';
            scope.selected.model = '';
            scope.selected.trim = '';

            svc.getMakes(scope.selected).then(function (result) {
                scope.options.makes = result;
            })
        }

        scope.getModels = function () {

            scope.selected.model = '';
            scope.selected.trim = '';

            svc.getModels(scope.selected).then(function (result) {
                scope.options.models = result;
            })
        }

        scope.getTrims = function () {

            scope.selected.trim = '';

            svc.getTrims(scope.selected).then(function (result) {
                scope.options.trims = result;
            })
        }

        scope.cars = [];                // where the results for the cars will go

        scope.getYears();               // starts the getYears function, getMakes gets started from the Index when
        // a click event occurs
    }]);

})();