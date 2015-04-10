describe('YellaController function', function () {
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
        $controller('YellaController', mock);
    }));

    it('Check users modules are called and entered into scope.', function () {
        //check correct changes were made
        expect($rootScope.user).toBe(fakeCurrentUser);
        expect($rootScope.users).toBe(usersMock);
    });
});