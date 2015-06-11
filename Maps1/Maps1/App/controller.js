(function () {
    var app = angular.module('app');

    app.controller('Appcontroller', function () {
        var scope = this;                       // use the "this" keyword when not using "$"
        scope.map = { center: { latitude: 38.0283299, longitude: -84.471565 }, zoom: 11 };
    });

})();

// modules keep things out of the global namespace