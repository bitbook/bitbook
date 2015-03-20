app.controller('YellaController', function ($scope, $rootScope, currentUser, users) {
    $rootScope.user = currentUser.user();
    // Get List of Users for right hand side.
    $rootScope.users = users;
});
app.controller('RegisterController', function ($scope, $state, $rootScope, currentUser, yellaService, users) {
    // hide name taken warning
    $scope.nameTaken = false;
    // form submition code
    $scope.submit = function() {
        // send to service to create user
        if(yellaService.newUser($scope.nick, $scope.desc)){
            // if returned done set username in local storage to
            localStorage.setItem("user",$scope.nick);
            $scope.nameTaken = false;
            // log user in
            $rootScope.user = currentUser.user();
            $rootScope.$emit('updateUsers',yellaService.getUsers());
            // the above line seems over complicated just to update the side bar with the new user
            // show index
            $state.go("index");
        }else{
            // else show name taken warning.
            $scope.nameTaken = true;
        }
    };
});
app.controller('UserController', function ($scope,$rootScope, users) {
    // Get List of Users for right hand side.
    $scope.users = users;
    $rootScope.$on('updateUsers', function(event, args) {
        $scope.users = args;
    });
    // the above lines seems over complicated just to update the side bar with the new user
    // Module Systen
    $scope.Modules = ["Status", "Image"];
    $scope.submit = [];
    $scope.submit.Type = $scope.Modules[0];
    $scope.getModuleUrl = function (a) {
        return "Modules/" + a + "/PartialTimeLine.html";
    };
    $scope.getModuleSubmitUrl = function () {
        return "Modules/" + $scope.submit.Type + "/PartialSubmit.html";
    };
});
