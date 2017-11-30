var app = angular.module("App", ['ui.bootstrap']);

app.controller('ClienteController', function ($scope, $http, $filter) {

    $scope.submited = false;
    $scope.enviado = false;
    $scope.erro = false;
    $scope.msgSucesso = "";

    $scope.checkFields = function () {
        $scope.submited = true;
    };

    $scope.adicionarCliente = function () {
        $scope.enviado = false;
        if ($scope.myForm.$valid) {
            $scope.myForm.$setPristine();

            $scope.Pessoa.Endereco = document.getElementById('rua').value;
            $scope.Pessoa.Bairro = document.getElementById('bairro').value;
            $scope.Pessoa.TipoDocumento = "CPF";
            $scope.Pessoa.PerfilFornecedor = "N";
            $scope.Pessoa.PerfilCliente = "S";
            $scope.Pessoa.PerfilVendedor = "N";
            $scope.Pessoa.Status = "A";
            console.log($scope.Pessoa);

            if ($scope.OpSalvar) {

                $scope.Pessoa.Municipio = { Id: document.getElementById('ibge').value, Nome: document.getElementById('cidade').value }
                $http({
                    url: '/Pessoa/Adicionar',
                    method: 'POST',
                    data: { pessoa: $scope.Pessoa }
                })
                    .success(function (data, status, headers, config) {
                        if (data == "-1") {
                            $scope.msgErro = "Ocorreu um erro inesperado.";
                            $scope.erro = true;
                            $scope.submited = true;
                        } else if (data == "-2") {
                            $scope.msgErro = "Já existe uma pessoa cadastrada com esse e-mail.";
                            $scope.erro = true;
                            $scope.submited = true;
                        } else {
                            $scope.msgSucesso = "Cliente cadastrado com sucesso.";
                            $scope.enviado = true;
                            $scope.submited = false;
                        }
                        $scope.search();
                        $scope.Pessoa = null;
                    })
                    .error(function (data, status, headers, config) {
                        // Lança o erro no console do navegador caso ocorra
                        console.log("Erro: " + status + "\nConfig: " + JSON.stringify(config) + "\nData:\n" + data);
                    });

            } else {
                $http({
                    url: '/Pessoa/Alterar',
                    method: 'POST',
                    data: { pessoa: $scope.Pessoa }
                })
                    .success(function (data, status, headers, config) {
                        if (data == "-1") {
                            $scope.msgErro = "Ocorreu um erro inesperado.";
                            $scope.erro = true;
                            $scope.submited = true;
                        } else if (data == "-2") {
                            $scope.msgErro = "Já existe uma pessoa cadastrada com esse e-mail.";
                            $scope.erro = true;
                            $scope.submited = true;
                        } else {
                            $scope.msgSucesso = "Cliente alterado com sucesso.";
                            $scope.enviado = true;
                            $scope.submited = false;
                        }
                        $scope.Pessoa = null;
                        $scope.OpSalvar = true;
                        $scope.search();
                    })
                    .error(function (data, status, headers, config) {
                        // Lança o erro no console do navegador caso ocorra
                        console.log("Erro: " + status + "\nConfig: " + JSON.stringify(config) + "\nData:\n" + data);
                    });
            }
        }
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
        $scope.myForm.$setPristine();
        $scope.enviado = false;
        $scope.OpSalvar = false;
        $http({
            url: '/Pessoa/ObterRegistro',
            method: 'POST',
            data: { idPessoa: id }
        })
            .success(function (data, status, headers, config) {
                $scope.Pessoa = data;
                //$scope.Pessoa.DataNascimento = $filter('date')($scope.Pessoa.DataNascimento, "dd/MM/yyyy");
                //$scope.Pessoa.DataNascimento = Date.parse($scope.Pessoa.DataNascimento);
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
                if (data == "True") {
                    $scope.msgSucesso = "Cliente removido com sucesso.";
                    $scope.enviado = true;
                    $scope.erro = false;
                    $scope.submited = false;
                } else {
                    $scope.erro = true;
                    $scope.enviado = false;
                }
                $scope.search();
            })
            .error(function (data, status, headers, config) {
                // Lança o erro no console do navegador caso ocorra
                console.log("Erro: " + status + "\nConfig: " + JSON.stringify(config) + "\nData:\n" + data);
            });
    }

    $scope.cancelarAlteracao = function () {
        $scope.myForm.$setPristine();
        $scope.enviado = false;
        $scope.erro = false;
        $scope.submited = false;
        $scope.Pessoa = null;
        $scope.OpSalvar = true;
    }

    $(document).ready(function () {
        $scope.enviado = false;
        $scope.OpSalvar = true;
        $scope.search();
    });
});