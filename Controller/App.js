
/// <reference path="../../Scripts/angular.js" />
/// <reference path="../../Scripts/angular-datatables.buttons.js" />
//var application = angular.module('iApp', ['datatables', 'datatables.buttons']);
var application = angular.module('iApp', []);
application.controller('LogoutController', function ($location, $scope, $window) {
    $scope.logout = function () {
        $window.localStorage.clear();
        window.location.assign("login.html");
    }

});










