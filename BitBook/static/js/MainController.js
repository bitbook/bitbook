app.controller('YellaController', function ($scope, $rootScope, currentUser) {
    console.log(currentUser.user());
    $rootScope.user = currentUser.user();
    console.log($rootScope.user);
});
app.controller('RegisterController', function ($scope, $state, $rootScope, currentUser, yellaService) {
    $scope.nameTaken = false;
    $scope.submit = function() {
        if(yellaService.newUser($scope.nick, $scope.desc)){
            localStorage.setItem("user",$scope.nick);
            $scope.nameTaken = false;
            $rootScope.user = currentUser.user();
            $state.go("index");
        }else{
            $scope.nameTaken = true;
        }
    };
});
app.controller('UserController', function ($scope, currentUser, users) {
    $scope.Modules = ["Status", "Image"];
    $scope.users = users;
    $scope.submit = [];
    $scope.submit.Type = $scope.Modules[0];
    $scope.getModuleUrl = function (a) {
        return "Modules/" + a + "/PartialTimeLine.html";
    };
    $scope.getModuleSubmitUrl = function () {
        return "Modules/" + $scope.submit.Type + "/PartialSubmit.html";
    };
});
