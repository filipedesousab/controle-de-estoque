var app = angular.module("App", ['ui.bootstrap']);

app.controller('FornecedorController', function ($scope, $http) {

    $scope.adicionarFornecedor = function () {
        $http({
            url: '/Pessoa/Adicionar',
            method: 'POST',
            data: { Pessoa: $scope.Pessoa }
        })
            .success(function (data, status, headers, config) {
                var msgAlerta = data == -1 ? "Erro no cadastro" : "Cadastrado com sucesso";
                alert(msgAlerta);
            })
            .error(function (data, status, headers, config) {
                // Lança o erro no console do navegador caso ocorra
                console.log("Erro: " + status + "\nConfig: " + JSON.stringify(config) + "\nData:\n" + data);
            });
    };
    
});