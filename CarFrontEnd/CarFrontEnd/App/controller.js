(function () {
    var app = angular.module('app');

    app.controller('ctrl', ['svc', function (svc) {      // svc is the name we are giving to the service, matches name 
        var scope = this;                                // in the service.js file
        scope.selected = {
            year: '',
            make: '',
            model: ''
        };

        scope.options = {
            years: [],
            makes: [],
            models: []
        }

        scope.getYears = function () {
            svc.getYears().then(function (result) {
                scope.options.years = result;
            })
        }

        scope.getMakes = function () {
            svc.getMakes(scope.selected.year).then(function (result) {
                scope.options.makes = result;
            })
        }

        scope.cars = [];                // where the results for the cars will go

        scope.getYears();               // starts the getYears function, getMakes gets started from the Index when
                                        // a click event occurs
    }]);

})();