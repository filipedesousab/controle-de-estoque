var app = angular.module("App", ['ui.bootstrap']);

app.controller('FornecedorController', function ($scope, $http) {

    $scope.adicionarFornecedor = function () {

        $scope.Pessoa.Endereco = document.getElementById('rua').value;
        $scope.Pessoa.Bairro = document.getElementById('bairro').value;
        $scope.Pessoa.Municipio = { Id: document.getElementById('ibge').value, Nome: document.getElementById('cidade').value }
        $scope.Pessoa.TipoDocumento = "CNPJ";
        $scope.Pessoa.PerfilFornecedor = "S";
        $scope.Pessoa.PerfilCliente = "N";
        $scope.Pessoa.PerfilVendedor = "N";
        $scope.Pessoa.Status = "A";

        $http({
            url: '/Pessoa/Adicionar',
            method: 'POST',
            data: { pessoa: $scope.Pessoa }
        })
            .success(function (data, status, headers, config) {
                var msgAlerta = "";
                if (data == "-1")
                    msgAlerta = "Erro no cadastro";
                else if (data == "-2")
                    msgAlerta = "Já existe uma pessoa cadastrada com esse e-mail.";
                else
                    msgAlerta = "Cadastrado com sucesso";
                alert(msgAlerta);
            })
            .error(function (data, status, headers, config) {
                // Lança o erro no console do navegador caso ocorra
                console.log("Erro: " + status + "\nConfig: " + JSON.stringify(config) + "\nData:\n" + data);
            });
    };
    
});