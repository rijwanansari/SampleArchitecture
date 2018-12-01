var app = angular.module('cardApp', ['ngMaterial', 'ngRoute', 'ngMessages']);

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider.when('/',
        {
            templateUrl: '/app/home/home.html'
        });
    $routeProvider.when('/Home/Index',
        {
            templateUrl: '/app/home/home.html'
        });

    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
}]);