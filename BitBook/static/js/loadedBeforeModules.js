var app = angular.module('yella', ['ui.router']);

app.service('dht', function () {
    this.get = function (key) {
        switch (key) {
            case "friends":
                return [
                    {"nick": "rlweb", "desc": "Hello World!"},
                    {"nick": "joe", "desc": "Hello World 2"},
                    {"nick": "pete", "desc": "Hello World 3"}
                ];
                break;
            case "rlweb":
                return {
                    "createdOn": "2014-11-23T18:25:43.511Z",
                    "nick": "rlweb",
                    "picture_small": "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQA....",
                    "picture_full": "http://i.imgur.com/oSg78PE.gif",
                    "desc": "Hello, welcome to my profile...."
                };
                break;
            case "joe":
                return {
                    "createdOn": "2014-11-23T18:25:43.511Z",
                    "nick": "joe",
                    "picture_small": "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQA....",
                    "picture_full": "http://i.imgur.com/oSg78PE.gif",
                    "desc": "Hello, welcome to my profile123...."
                };
                break;
            case "pete":
                return {
                    "createdOn": "2014-11-23T18:25:43.511Z",
                    "nick": "pete",
                    "picture_small": "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQA....",
                    "picture_full": "http://i.imgur.com/oSg78PE.gif",
                    "desc": "Hello, welcome to my profile321...."
                };
                break;
            case "rlweb_status":
                return [
                    "10",
                    "9"
                ];
                break;
            case "joe_status":
                return [
                    "11"
                ];
                break;
            case "pete_status":
                return [
                    "12"
                ];
                break;
            case "9":
                return {
                    "nick": "rlweb",
                    "createdOn": "2014-11-23T18:25:43.511Z",
                    "type": "Image",
                    "version": "v0.1",
                    "data": {
                        "imageSrc": "http://www.crosscountrytrains.co.uk/media/22701/trains_to_bristol.jpg"
                    }
                };
                break;
            case "10":
                return {
                    "nick": "rlweb",
                    "createdOn": "2014-11-23T18:25:43.511Z",
                    "type": "Status",
                    "version": "v0.1",
                    "data": {
                        "status": "Hello World!rlweb"
                    }
                };
                break;
            case "11":
                return {
                    "nick": "joe",
                    "createdOn": "2014-11-23T18:25:43.511Z",
                    "type": "Status",
                    "version": "v0.1",
                    "data": {
                        "status": "Hello World!Joe"
                    }
                };
                break;
            case "12":
                return {
                    "nick": "pete",
                    "createdOn": "2014-11-23T18:25:43.511Z",
                    "type": "Status",
                    "version": "v0.1",
                    "data": {
                        "status": "Hello World!Pete"
                    }
                };
                break;
            default:
                return false
        }
    };
    this.put = function (key, value) {
        return true;
    };
});

app.service('yellaService', function (dht) {
    this.getUsers = function () {
        return dht.get("friends");
    };
    this.getUser = function (user) {
        return dht.get(user);
    };
    this.getUserStatusList = function (user) {
        var a = dht.get(user + "_status");
        var list = [];
        a.forEach(function (element) {
            list.push(dht.get(element));
        });
        return list;
    };
});

/*
 * Returns current logged in user or false if not logged in.
 */
app.service('currentUser', function (yellaService) {
    //return false;
    return yellaService.getUser("rlweb");
});
app.service('users', function (yellaService) {
    return yellaService.getUsers();
});
app.service('status', function (yellaService) {
    this.getTimeLine = function(){
        this.list = [];
        var users = yellaService.getUsers();
        angular.forEach(users,function (user) {
            var userStatus = yellaService.getUserStatusList(user.nick);
            this.list = this.list.concat(userStatus);
        },this);
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

app.config(function ($stateProvider, $urlRouterProvider) {
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
            controller: function ($scope, currentUser) {
                $scope.settings = {};
                $scope.settings.nick = currentUser.nick;
                $scope.settings.desc = currentUser.desc;
            }
        })
        .state('profile', {
            url: "/profile/:nick",
            templateUrl: "partials/user.html",
            controller: ['$scope', '$stateParams', 'profile',
                function ($scope, $stateParams, profile) {
                    var recievedProfile = profile.get($stateParams.nick);
                    $scope.user = {};
                    $scope.user.nick = recievedProfile.nick;
                    $scope.user.desc = recievedProfile.desc;
                    $scope.statusList = recievedProfile.status;
                }]
        });
});