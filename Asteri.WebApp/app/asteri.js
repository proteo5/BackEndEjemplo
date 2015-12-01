var asteri = angular.module('asteri', ['ui.router']);

asteri.config(function ($stateProvider, $urlRouterProvider) {
    //
    // For any unmatched url, redirect to /state1
    $urlRouterProvider.otherwise("/dashboard");
    //
    // Now set up the states
    $stateProvider
      .state('login', loginCtrl);
    $stateProvider.state('home', homeCtr);
    $stateProvider.state('home.dashboard', {
        url: "^/dashboard",
        templateUrl: "app/home.dashboard.html"
    });
    $stateProvider.state('home.settings', settingsCtrl);
    $stateProvider.state('home.settings.users', usersCtrl);
});

asteri.directive('focusOn', function () {
    return function (scope, elem, attr) {
        scope.$on('focusOn', function (e, name) {
            if (name === attr.focusOn) {
                elem[0].focus();
            }
        });
    };
});

asteri.factory('focus', function ($rootScope, $timeout) {
    return function (name) {
        $timeout(function () {
            $rootScope.$broadcast('focusOn', name);
        });
    }
});

//This is used for html templating
String.prototype.format = function () {
    var args = arguments;
    return this.replace(/{(\d+)}/g, function (match, number) {
        return typeof args[number] != 'undefined'
          ? args[number]
          : match
        ;
    });
};