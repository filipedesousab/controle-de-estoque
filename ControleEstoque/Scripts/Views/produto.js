var app = angular.module("App", ['ui.bootstrap']);

app.controller('ProdutoController', function ($scope, $http) {

    $scope.submited = false;
    $scope.enviado = false;
    $scope.erro = false;
    $scope.msgSucesso = "";

    $scope.checkFields = function () {
        $scope.submited = true;
    };

    $scope.adicionarProduto = function () {
        $scope.enviado = false;
        if ($scope.myForm.$valid) {
            $scope.myForm.$setPristine();
            if ($scope.OpSalvar) {
                $http({
                    url: '/Produto/Adicionar',
                    method: 'POST',
                    data: { produto: $scope.Produto }
                })
                    .success(function (data, status, headers, config) {
                        if (data == "0") {
                            $scope.msgSucesso = "Produto cadastrado com sucesso.";
                            $scope.enviado = true;
                            $scope.submited = false;
                        } else {
                            $scope.erro = true;
                            $scope.submited = true;
                        }
                        $scope.Produto = null;
                        $scope.OpSalvar = true;
                        $scope.search();
                    })
                    .error(function (data, status, headers, config) {
                        // Lança o erro no console do navegador caso ocorra
                        console.log("Erro: " + status + "\nConfig: " + JSON.stringify(config) + "\nData:\n" + data);
                    });
            } else {
                $http({
                    url: '/Produto/Alterar',
                    method: 'POST',
                    data: { produto: $scope.Produto }
                })
                    .success(function (data, status, headers, config) {
                        if (data == "0") {
                            $scope.msgSucesso = "Produto alterado com sucesso.";
                            $scope.enviado = true;
                            $scope.submited = false;
                        } else {
                            $scope.erro = true;
                            $scope.submited = true;
                        }
                        $scope.Produto = null;
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
        $scope.myForm.$setPristine();
        $scope.enviado = false;
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
                if (data == "True") {
                    $scope.msgSucesso = "Produto removido com sucesso.";
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
        $scope.Produto = null;
        $scope.OpSalvar = true;
    }

    $(document).ready(function () {
        $scope.enviado = false;
        $scope.OpSalvar = true;
        $scope.search();
    });
    
});