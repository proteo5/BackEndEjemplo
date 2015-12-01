var usersCtrl = {
    controllerAs: 'usersCtr',
    url: "^/settings/users",
    templateUrl: "app/settings/users.html",
    resolve: {

    },
    controller: function ($scope, $http, $state,$compile, focus) {
        $scope.user = {};
        //Declare the table
        $scope.table = $('#table_id').DataTable({
            columns: [
                { data: 'User' },
                { data: 'UserNames' },
                { data: 'UsersLastNames' },
                { data: 'Email' },
                { data: 'IsActive' },
                { data: 'Action' }
            ]
        });

        $scope.NewUser_Click = function () {
            $scope.user.title = "New User";
            $scope.user.id = 0;
            $scope.user.user = "";
            $scope.user.password = "";
            $scope.user.password2 = "";
            $scope.user.names = "";
            $scope.user.lastNames = "";
            $scope.user.email = "";
            $('#userModal').modal('show')
        };

        $scope.CreateUser_Click = function () {
            if ($scope.user.password != $scope.user.password2) {
                alert("The passwords don't match");
            }
            else if ($scope.user.user && $scope.user.names && $scope.user.lastNames
                && $scope.user.email && $scope.user.password && $scope.user.password2) {
                $http.post("/Users/Create",
                    {
                        "user": $scope.user.user,
                        "names": $scope.user.names,
                        "lastnames": $scope.user.lastNames,
                        "email": $scope.user.email,
                        "password": $scope.user.password
                    })
                    .success(function (data, status, headers, config) {
                        switch (data.Result) {
                            case 0: //User exists
                                $scope.fillTable();
                                $('#userModal').modal('hide')
                                break;
                            case 1: //User don't exists
                                alert("User " + $scope.user.user + " already exists.");
                                break;
                            default: //Error on server
                                alert("There is an error on the server, please contact the administrator.");
                                break;
                        }
                    })
                    .error(function (data, status, headers, config) {
                        alert("There is an error on the server, please contact the administrator.");
                    });
            }
        };

        $scope.UpdateUser_Click = function () {
            if ($scope.user.user && $scope.user.names && $scope.user.lastNames
               && $scope.user.email && $scope.user.IsActive != undefined && $scope.user.id > 0) {
                $http.post("/Users/Update",
                    {
                        "id": $scope.user.id,
                        "user": $scope.user.user,
                        "names": $scope.user.names,
                        "lastnames": $scope.user.lastNames,
                        "email": $scope.user.email,
                        "isActive": $scope.user.IsActive
                    })
                    .success(function (data, status, headers, config) {
                        switch (data.Result) {
                            case 0: //User exists
                                $scope.fillTable();
                                $('#userModalEdith').modal('hide')
                                break;
                            case 1: //User don't exists
                                alert("There is a problem with the user " + $scope.user.user + ", try again or contact your administrator.");
                                break;
                            default: //Error on server
                                alert("There is an error on the server, please contact the administrator.");
                                break;
                        }
                    })
                    .error(function (data, status, headers, config) {
                        alert("There is an error on the server, please contact the administrator.");
                    });
            }
        };

        $scope.EditUser_Click = function (Id) {
            $http.post("/Users/GetById",
                     {
                         "id": Id,
                     })
                     .success(function (data, status, headers, config) {
                         switch (data.Result) {
                             case 0: //User exists
                                 $scope.user.title = "Update User";
                                 $scope.user.id = data.Data.Id;
                                 $scope.user.user = data.Data.User;
                                 $scope.user.names = data.Data.UserNames;;
                                 $scope.user.lastNames = data.Data.UsersLastNames;
                                 $scope.user.email = data.Data.Email;
                                 $scope.user.IsActive = data.Data.IsActive;
                                 $('#userModalEdith').modal('show')
                                 break;
                             case 1: //User don't exists
                                 alert("User " + $scope.user.user + " already exists.");
                                 break;
                             default: //Error on server
                                 alert("There is an error on the server, please contact the administrator.");
                                 break;
                         }
                     })
                     .error(function (data, status, headers, config) {
                         alert("There is an error on the server, please contact the administrator.");
                     });
        };

        $scope.fillTable = function () {
            $http({ method: 'GET', url: '/Users/GetAll' })
                .success(function (data, status, headers, config) {
                    switch (data.Result) {
                        case 0: //User exists
                            $scope.table.clear().draw();
                            var tableAction = $("#tableAction").html();
                            var tableActionContainer = $("#tableActionContainer").html();
                            var tableCheckbox = $("#tableCheckbox").html();
                            $.each(data.Data, function (index, value) {
                                var checked = value.IsActive ? 'checked="yes"' : "";
                                value.IsActive = tableCheckbox.format(checked);
                                value.Action = tableActionContainer.format(index);
                                $scope.table.row.add(value).draw(false);
                                var tempElem = tableAction.format("EditUser_Click(" + value.Id + ")", "Edit");
                                var temp = $compile(tempElem)($scope);
                                angular.element(document.getElementById("container_" + index)).append(temp);
                            });
                            break;
                        case 1: //User don't exists
                            alert("We don't have users.");
                            break;
                        default: //Error on server
                            alert("There is an error on the server, please contact the administrator.");
                            break;
                    }
                })
                .error(function (data, status, headers, config) {
                    alert("There is an error on the server, please contact the administrator.");
                });
        };

        $scope.fillTable();
    },
    onEnter: function ($state) {

    }
};