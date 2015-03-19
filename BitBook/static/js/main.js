app.controller('MainController', function ($scope, currentUser, users, status) {
    $scope.Modules = ["Status", "Image"];
    $scope.user = currentUser;
    $scope.nick = currentUser.nick;
    $scope.users = users;
    $scope.submit = [];
    $scope.submit.Type = $scope.Modules[0];
    $scope.getModuleUrl = function (a) {
        return "Modules/" + a + "/PartialTimeLine.html";
    };
    $scope.getModuleSubmitUrl = function () {
        return "Modules/" + $scope.submit.Type + "/PartialSubmit.html";
    };
    //$scope.statusList = status.get();
});
