var homeCtr = {
    url: "/home",
    abstract: true,
    resolve: {
        IsLogin: function ($http) {
            return $http({ method: 'GET', url: '/users/IsLogin' });
        }
    },
    templateUrl: "app/home.html",
    controller: function ($scope, $http, $state, IsLogin) {
        //Function for the name of the user logedin
        $scope.UserNameLoged = IsLogin.data.Data;

        $scope.cmdLogout = function () {
            //Function for logout button
            $http.post("/Users/Logout", $scope.user)
                   .success(function (data, status, headers, config) {
                       switch (data.Result) {
                           case 0: //Logout Success
                               //$state.go('login');
                               window.location.href = '/#/login';
                               break;
                           case 1: //Login Fail
                               //$scope.user.message = data.Message;
                               break;
                           default: //Error on server
                               //$scope.user.message = "System Error, contact administrator.";
                               break;
                       }
                   })
                   .error(function (data, status, headers, config) {
                       //url error
                       //$scope.user.message = "System Error, contact administrator.";
                   });
        }
    },
    onEnter: function (IsLogin, $state) {
        //Goto dashboard if has not login
        if (IsLogin.data.Result == 1) {
            $state.go('login');
        }
    }
};