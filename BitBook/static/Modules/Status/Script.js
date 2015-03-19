app.controller('StatusFormController', ['$state','$scope', 'yellaService','currentUser', function ($state, $scope, yellaService, currentUser) {
    $scope.maxLength = 500;
    $scope.message = "";
    $scope.submitStatus = function () {
        yellaService.newStatus(currentUser.user().nick, 'Status', 'v0.1', {
            status: $scope.message
        });
        $state.forceReload();
    };
}]);
app.controller('StatusController', ['$scope', function ($scope) {
    $scope.statusTxt = $scope.status.data.status; // + "Yaay for controllers";
}]);