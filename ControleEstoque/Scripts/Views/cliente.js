var app = angular.module("App", ['ui.bootstrap']);

app.controller('ClienteController', function ($scope, $http, $filter) {

    $scope.adicionarCliente = function () {
        var validacao = $.validarCampos(['nome', 'email', 'ddd', 'telefone', 'documento', 'cep', 'endereco', 'bairro', 'cidade', 'uf']);

        if (validacao) {
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
                        var msgAlerta = "";
                        if (data == "-1")
                            msgAlerta = "Erro no cadastro";
                        else if (data == "-2")
                            msgAlerta = "J� existe uma pessoa cadastrada com esse e-mail.";
                        else
                            msgAlerta = "Cadastrado com sucesso";
                        alert(msgAlerta);
                        $scope.search();
                    })
                    .error(function (data, status, headers, config) {
                        // Lan�a o erro no console do navegador caso ocorra
                        console.log("Erro: " + status + "\nConfig: " + JSON.stringify(config) + "\nData:\n" + data);
                    });

            } else {
                $http({
                    url: '/Pessoa/Alterar',
                    method: 'POST',
                    data: { pessoa: $scope.Pessoa }
                })
                    .success(function (data, status, headers, config) {
                        var msgAlerta = "";
                        if (data == "-1")
                            msgAlerta = "Erro no cadastro";
                        else if (data == "-2")
                            msgAlerta = "J� existe uma pessoa cadastrada com esse e-mail.";
                        else
                            msgAlerta = "Alterado com sucesso";
                        alert(msgAlerta);
                        $scope.Pessoa = [];
                        $scope.OpSalvar = true;
                        $scope.search();
                    })
                    .error(function (data, status, headers, config) {
                        // Lan�a o erro no console do navegador caso ocorra
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
                    $scope.noResult = "Sua Pesquisa n�o obteve resultados.";
                    $("#no-result").css("display", "block");
                } else {
                    $("#no-result").css("display", "none");
                }

            }).error(function (data, status, headers, config) {
                console.log(status);
            });
    }

    $scope.selecionarCliente = function (id) {
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
                // Lan�a o erro no console do navegador caso ocorra
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
                // Lan�a o erro no console do navegador caso ocorra
                console.log("Erro: " + status + "\nConfig: " + JSON.stringify(config) + "\nData:\n" + data);
            });
    }

    $scope.cancelarAlteracao = function () {
        $scope.Pessoa = [];
        $scope.OpSalvar = true;
    }

    $(document).ready(function () {
        $scope.OpSalvar = true;
        $scope.search();
    });
});