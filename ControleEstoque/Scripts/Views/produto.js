var app = angular.module("App", ['ui.bootstrap']);

app.controller('ProdutoController', function ($scope, $http) {

    $scope.adicionarProduto = function () {
        //var validacao = $.validarCampos(['nome', 'descricao', 'quantidade', 'fornecedor']);
        //if (validacao) {
        if ($scope.OpSalvar) {
            $http({
                url: '/Produto/Adicionar',
                method: 'POST',
                data: { Produto: $scope.Produto }
            })
                .success(function (data, status, headers, config) {
                    var msgAlerta = data == "-1" ? "Erro no cadastro" : "Cadastrado com sucesso";
                    alert(msgAlerta);
                })
                .error(function (data, status, headers, config) {
                    // Lança o erro no console do navegador caso ocorra
                    console.log("Erro: " + status + "\nConfig: " + JSON.stringify(config) + "\nData:\n" + data);
                });
        } else {
            $http({
                url: '/Produto/Alterar',
                method: 'POST',
                data: { Produto: $scope.Produto }
            })
                .success(function (data, status, headers, config) {
                    var msgAlerta = "";
                    if (data == "0")
                        msgAlerta = "Alterado com sucesso.";
                    else
                        msgAlerta = "Erro na alteração.";
                    alert(msgAlerta);
                    $scope.Produto = [];
                    $scope.OpSalvar = true;
                    $scope.search();
                })
                .error(function (data, status, headers, config) {
                    // Lança o erro no console do navegador caso ocorra
                    console.log("Erro: " + status + "\nConfig: " + JSON.stringify(config) + "\nData:\n" + data);
                });
        }
        //}

    };
    $scope.obterFornecedores = function () {
        $http.get("/Fornecedor/ListarFornecedoresAtivos")
            .success(function (response) {
                console.log(response);
                $scope.Fornecedores = response;
            });
    }

    $scope.search = function (userValue) {
        $http.get("/Produto/ObterRegistros")
            .success(function (response) {
                $scope.Produtos = response; // JSON

                $scope.totalItems = $scope.Produtos.length;
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

                if ($scope.Produtos.length == 0) {
                    $scope.noResult = "Sua Pesquisa não obteve resultados.";
                    $("#no-result").css("display", "block");
                } else {
                    $("#no-result").css("display", "none");
                }
                $scope.obterFornecedores();

            }).error(function (data, status, headers, config) {
                console.log(status);
            });
    }

    $scope.selecionarProduto = function (id) {
        $scope.OpSalvar = false;
        $http({
            url: '/Produto/ObterRegistro',
            method: 'POST',
            data: { idProduto: id }
        })
            .success(function (data, status, headers, config) {
                $scope.Produto = data; // JSON
                console.log(data);
            })
            .error(function (data, status, headers, config) {
                // Lança o erro no console do navegador caso ocorra
                console.log("Erro: " + status + "\nConfig: " + JSON.stringify(config) + "\nData:\n" + data);
            });
    }

    $scope.removerProduto = function (id) {
        $http({
            url: '/Produto/Deletar',
            method: 'POST',
            data: { idProduto: id }
        })
            .success(function (data, status, headers, config) {
                var msgAlerta = data == -1 ? "Erro ao tentar remover" : "Produto removido com sucesso";
                alert(msgAlerta);
            })
            .error(function (data, status, headers, config) {
                // Lança o erro no console do navegador caso ocorra
                console.log("Erro: " + status + "\nConfig: " + JSON.stringify(config) + "\nData:\n" + data);
            });
    }

    $scope.cancelarAlteracao = function () {
        $scope.Produto = [];
        $scope.OpSalvar = true;
    }

    $(document).ready(function () {
        $scope.OpSalvar = true;
        $scope.search();
    });
    
});