describe('UserController function', function () {
    beforeEach(module('yella'));
    var $rootScope, $scope, $controller, mock, currentUserMock, fakeCurrentUser, usersMock;

    beforeEach(inject(function (_$rootScope_, _$controller_, _users_, _currentUser_, $q) {
        $rootScope = _$rootScope_;
        $scope = $rootScope.$new();
        $controller = _$controller_;

        fakeCurrentUser = {
            "createdOn": "2014-11-23T18:25:43.511Z",
            "nick": "milkybar",
            "desc": "Milkybars are on me!"
        };

        currentUserMock = {
            user: function () {
                return fakeCurrentUser;
            }
        };

        usersMock = {};

        mock = {
            '$rootScope': $rootScope,
            '$scope': $scope,
            'users': usersMock,
            'currentUser': currentUserMock
        };
        $controller('UserController', mock);
    }));

    it('Check users are called and entered into scope.', function () {
        expect($scope.users).toBe(usersMock);
    });

    it('Check module data is correct and type should be updated with first one', function () {
        expect($scope.Modules.length).toBe(["Status", "Image"].length);
        expect($scope.submit.Type).toBe("Status");
    });

    it('Check getModuleUrl', function () {
        expect($scope.getModuleUrl("Hello")).toBe("Modules/Hello/PartialTimeLine.html");
    });

    it('Check getModuleSubmitUrl', function () {
        expect($scope.getModuleSubmitUrl()).toBe("Modules/Status/PartialSubmit.html");
    });
});