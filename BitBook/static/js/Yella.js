var app = angular.module('yella', ['ui.router']);
/*
 * Returns current logged in user or false if not logged in.
 */
app.service('currentUser', function (yellaService) {
    this.user = function () {
        console.log('currentUser ' + localStorage.getItem("user"));
        if (localStorage.getItem("user")) {
            return yellaService.getUser(localStorage.getItem("user"));
        }
        return false;
    }
});
app.service('users', function (yellaService) {
    return yellaService.getUsers();
});
app.service('status', function (yellaService) {
    this.getTimeLine = function () {
        this.list = [];
        var users = yellaService.getUsers();
        angular.forEach(users, function (user) {
            var userStatus = yellaService.getUserStatusList(user.nick);
            this.list = this.list.concat(userStatus);
        }, this);
        return this.list;
    };
});

app.service('profile', function (yellaService) {
    this.get = function (nick) {
        var a = yellaService.getUser(nick);
        a.status = yellaService.getUserStatusList(nick);
        return a;
    };
});

app.config(function ($stateProvider, $urlRouterProvider, $provide) {
    $urlRouterProvider.otherwise("/");
    $stateProvider
        .state('index', {
            url: "/",
            templateUrl: "partials/index.html",
            controller: ['$scope', '$stateParams', 'status',
                function ($scope, $stateParams, status) {
                    $scope.statusList = status.getTimeLine();
                }]
        })
        .state('settings', {
            url: "/settings",
            templateUrl: "partials/settings.html",
            controller: function ($scope, $rootScope) {
                $scope.settings = {};
                $scope.settings.nick = $rootScope.user.nick;
                $scope.settings.desc = $rootScope.user.nick;
            }
        })
        .state('profile', {
            url: "/profile/:nick",
            templateUrl: "partials/user.html",
            controller: ['$scope', '$stateParams', '$state', 'profile',
                function ($scope, $stateParams, $state, profile) {
                    var receivedProfile = profile.get($stateParams.nick);
                    if (receivedProfile === false) {
                        $state.go("index");
                    }
                    $scope.userProfile = {};
                    $scope.userProfile.nick = receivedProfile.nick;
                    $scope.userProfile.desc = receivedProfile.desc;
                    $scope.statusList = receivedProfile.status;
                }]
        });
    $provide.decorator('$state', function ($delegate, $stateParams) {
        $delegate.forceReload = function () {
            return $delegate.go($delegate.current, $stateParams, {
                reload: true,
                inherit: false,
                notify: true
            });
        };
        return $delegate;
    });
});
