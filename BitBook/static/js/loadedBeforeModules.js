var app = angular.module('yella', ['ui.router']);

/*
 * Returns current logged in user or false if not logged in.
 */
app.service('currentUser', function () {
    //return false;
    return {
        nick: 'rlweb',
        desc: 'Milkybars are on me!'
    };
});
app.service('users', function () {
    return [
        {"nick": "rlweb", "desc": "Hello World!"},
        {"nick": "joe", "desc": "Hello World 2"},
        {"nick": "pete", "desc": "Hello World 3"}
    ];
});
app.service('status', function () {
    this.
    this.get = function (nick) {
        return ;
    };
    this.getTimeLine = function () {
        return
    };
});

app.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise("/");
    $stateProvider
        .state('index', {
            url: "/",
            templateUrl: "partials/index.html",
            controller: ['$scope', '$stateParams','status',
                function ($scope, $stateParams, status) {
                    $scope.statusList = status.getTimeline();
                }]
        })
        .state('settings', {
            url: "/settings",
            templateUrl: "partials/settings.html",
            controller: function ($scope) {
                $scope.settings = {};
                $scope.settings.nick = "own";
                $scope.settings.desc = "Hello World!";
            }
        })
        .state('profile', {
            url: "/profile/:nick",
            templateUrl: "partials/user.html",
            controller: ['$scope', '$stateParams', 'status',
                function ($scope, $stateParams, status) {
                    $scope.user = {};
                    $scope.user.nick = $stateParams.nick;
                    $scope.user.desc = "Hello World!123";
                    $scope.statusList = status.get($stateParams.nick);
                }]
        });
});