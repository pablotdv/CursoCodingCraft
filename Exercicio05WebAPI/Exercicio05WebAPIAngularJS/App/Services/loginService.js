/// <reference path="../app.js" />

app.service('loginservice', function ($http) {

    this.register = function (userInfo) {
        var resp = $http({
            url: "http://localhost:42079/api/Account/Register",
            method: "POST",
            data: userInfo,
        });
        return resp;
    };

    this.login = function (userlogin) {

        var resp = $http({
            url: "http://localhost:42079/TOKEN",
            method: "POST",
            data: $.param({ grant_type: 'password', username: userlogin.username, password: userlogin.password }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        });
        return resp;
    };
});