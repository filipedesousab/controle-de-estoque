var app = angular.module("App", ['ui.bootstrap']);

app.controller('ClienteController', function ($scope, $http) {

    $scope.adicionarCliente = function () {

        $scope.Pessoa.Endereco = document.getElementById('rua').value;
        $scope.Pessoa.Bairro = document.getElementById('bairro').value;
        $scope.Pessoa.Municipio = { Id: document.getElementById('ibge').value, Nome: document.getElementById('cidade').value }
        $scope.Pessoa.TipoDocumento = "CPF";
        $scope.Pessoa.PerfilFornecedor = "N";
        $scope.Pessoa.PerfilCliente = "S";
        $scope.Pessoa.PerfilVendedor = "N";
        $scope.Pessoa.Status = "A";
        console.log($scope.Pessoa);
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

    $scope.search = function (userValue) {
        $http.get("/Cliente/ListarClientesAtivos")
            .success(function (response) {

                $scope.Clientes = response; // JSON

                $scope.totalItems = $scope.Clientes.length;
                $scope.viewby = 10;
                $scope.currentPage = 1;
                $scope.itemsPerPage = $scope.viewby;
                $scope.maxSize = 5; // Number of pager buttons to show

                $scope.setPage = function (pageNo) {
                    $scope.currentPage = pageNo;
                };

                $scope.setItemsPerPage = function (num) {
                    $scope.itemsPerPage = num;
                    $scope.currentPage = 1;
                }

                if ($scope.Clientes.length == 0) {
                    $scope.noResult = "Sua Pesquisa não obteve resultados.";
                    $("#no-result").css("display", "block");
                } else {
                    $("#no-result").css("display", "none");
                }

            }).error(function (data, status, headers, config) {
                console.log(status);
            });
    }

    $scope.selecionarCliente = function (id) {
        $http({
            url: '/Pessoa/ObterRegistro',
            method: 'POST',
            data: { idPessoa: id }
        })
            .success(function (data, status, headers, config) {
                //var msgAlerta = data == -1 ? "Erro ao tentar remover" : "Fornecedor removido com sucesso";
                //alert(msgAlerta);
                console.log(data);
            })
            .error(function (data, status, headers, config) {
                // Lança o erro no console do navegador caso ocorra
                console.log("Erro: " + status + "\nConfig: " + JSON.stringify(config) + "\nData:\n" + data);
            });
    }

    $scope.removerCliente = function (id) {
        $http({
            url: '/Pessoa/Deletar',
            method: 'POST',
            data: { idPessoa: id }
        })
            .success(function (data, status, headers, config) {
                var msgAlerta = data == -1 ? "Erro ao tentar remover" : "Cliente removido com sucesso";
                alert(msgAlerta);
            })
            .error(function (data, status, headers, config) {
                // Lança o erro no console do navegador caso ocorra
                console.log("Erro: " + status + "\nConfig: " + JSON.stringify(config) + "\nData:\n" + data);
            });
    }

    $(document).ready(function () {
        $scope.search();
    });
});