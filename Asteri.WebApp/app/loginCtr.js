var loginCtrl = {
    url: "/login",
    templateUrl: "app/login.html",
    resolve: {
        IsLogin: function ($http) {
            return $http({ method: 'GET', url: '/users/IsLogin' });
        },
        CreateAdmin: function ($http) {
            //Create admin user if firt time use
            return $http({ method: 'GET', url: '/users/CreateAdmin' });
        }
    },
    controller: function ($scope, $http,$state, focus) {
        $scope.user = { userName: '', pass: '', message: '' };
        $scope.submit_Click = function () {
            if ($scope.user.userName && $scope.user.pass) {
                $http.post("/Users/Login", $scope.user)
                    .success(function (data, status, headers, config) {
                        switch (data.Result) {
                            case 0: //Login Success
                                //$state.go('home.dashboard');
                                window.location.href = '/#/dashboard';
                                break;
                            case 1: //Login Fail
                                $scope.user.message = data.Message;
                                focus('focusUser');
                                break;
                            default: //Error on server
                                $scope.user.message = "System Error, contact administrator.";
                                focus('focusUser');
                                break;
                        }
                    })
                    .error(function (data, status, headers, config) {
                        //url error
                        $scope.user.message = "System Error, contact administrator.";
                    });
            }
        };
        $scope.userKeyPress = function (keyEvent) {
            if (keyEvent.which === 13)
                focus('focusPass');

            $scope.user.message = "";
        };
        $scope.passKeyPress = function (keyEvent) {
            if (keyEvent.which === 13)
                $scope.submit_Click();
            $scope.user.message = "";
        };
    },
    controllerAs: 'loginCtr',
    onEnter: function (IsLogin, $state, CreateAdmin) {
        //Goto dashboard if has login
        if (IsLogin.data.Result == 0) {
            //$state.go('home.dashboard');
            window.location.href = '/#/dashboard';
        }
    }
};