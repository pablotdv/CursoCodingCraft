function ViewModel() {
    var self = this;

    var tokenKey = 'accessToken';

    self.diretorios = ko.observableArray();
    self.arquivos = ko.observableArray();
    self.error = ko.observable();

    self.result = ko.observable();
    self.user = ko.observable();

    self.registerEmail = ko.observable();
    self.registerPassword = ko.observable();
    self.registerPassword2 = ko.observable();

    self.loginEmail = ko.observable();
    self.loginPassword = ko.observable();

    self.diretorioNome = ko.observable();
    self.diretoriPai = ko.observable();

    self.arquivoNome = ko.observable();
    self.arquivoDiretorioId = ko.observable();
    self.arquivo = ko.observable();

    function showError(jqXHR) {
        self.result(jqXHR.status + ': ' + jqXHR.statusText);
    }

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null,
            headers: headers
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    self.callApi = function () {
        self.result('');

        ajaxHelper('/api/values', 'GET').done(function (data) {
            self.result(data);
        }).fail(showError);
    }

    self.register = function () {
        self.result('');

        var data = {
            Email: self.registerEmail(),
            Password: self.registerPassword(),
            ConfirmPassword: self.registerPassword2()
        };

        $.ajax({
            type: 'POST',
            url: '/api/Account/Register',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            self.result("Done!");
        }).fail(showError);
    }

    self.login = function () {
        self.result('');

        var loginData = {
            grant_type: 'password',
            username: self.loginEmail(),
            password: self.loginPassword()
        };

        $.ajax({
            type: 'POST',
            url: '/Token',
            data: loginData
        }).done(function (data) {
            self.user(data.userName);
            // Cache the access token in session storage.
            sessionStorage.setItem(tokenKey, data.access_token);
            getAllArquivos();
            getAllDiretorios();
        }).fail(showError);
    }

    self.logout = function () {
        self.user('');
        sessionStorage.removeItem(tokenKey)
    }

    self.postDiretorio = function () {
        self.result('');

        var data = {
            Nome: self.diretorioNome(),
            DiretorioPaiId: self.diretoriPai()
        };

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'POST',
            url: '/api/Diretorio',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            headers: headers
        }).done(function (data) {
            self.result(data.DiretorioId);
            getAllDiretorios();
        }).fail(showError);
    }

    self.postArquivo = function () {
        self.result('');

        var data = {
            Nome: self.arquivoNome(),
            DiretorioId: self.arquivoDiretorioId(),
        };

        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'POST',
            url: '/api/Arquivo',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            headers: headers
        }).done(function (data) {
            self.result(data.ArquivoId);
            getAllArquivos();
        }).fail(showError);
    }

    function getAllDiretorios() {
        ajaxHelper('/api/Diretorio', 'GET')
            .done(function (data) {
                self.diretorios(data);
            });
    }

    function getAllArquivos() {
        ajaxHelper('/api/Arquivo', 'GET')
            .done(function (data) {
                self.arquivos(data);
            });
    }

    getAllDiretorios();
    getAllArquivos();
}

var app = new ViewModel();

ko.applyBindings(app);