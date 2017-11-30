var app = angular.module("App", ['ui.bootstrap']);

app.controller('FornecedorController', function ($scope, $http) {
    
    $scope.submited = false;
    $scope.enviado = false;
    $scope.erro = false;
    $scope.msgSucesso = "";

    $scope.checkFields = function () {
        $scope.submited = true;
    };

    $scope.adicionarFornecedor = function () {
        $scope.enviado = false;
        if ($scope.myForm.$valid) {
            $scope.myForm.$setPristine();

            $scope.Pessoa.Endereco = document.getElementById('rua').value;
            $scope.Pessoa.Bairro = document.getElementById('bairro').value;
            $scope.Pessoa.TipoDocumento = "CNPJ";
            $scope.Pessoa.PerfilFornecedor = "S";
            $scope.Pessoa.PerfilCliente = "N";
            $scope.Pessoa.PerfilVendedor = "N";
            $scope.Pessoa.Status = "A";

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
                            $scope.msgSucesso = "Fornecedor cadastrado com sucesso.";
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
                            $scope.msgSucesso = "Fornecedor alterado com sucesso.";
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

    $scope.search = function(userValue) {
        $http.get("/Fornecedor/ListarFornecedoresAtivos")
        .success(function (response) {

              $scope.Fornecedores = response; // JSON

              $scope.totalItems = $scope.Fornecedores.length;
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

              if ($scope.Fornecedores.length == 0) {
                  $scope.noResult = "Sua Pesquisa não obteve resultados.";
                  $("#no-result").css("display", "block");
              } else {
                  $("#no-result").css("display", "none");
              }

        }).error(function (data, status, headers, config) {
              console.log(status);
        });
    }

    $scope.selecionarFornecedor = function (id) {
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
                console.log(data);
            })
            .error(function (data, status, headers, config) {
                // Lança o erro no console do navegador caso ocorra
                console.log("Erro: " + status + "\nConfig: " + JSON.stringify(config) + "\nData:\n" + data);
            });
    }

    $scope.removerFornecedor = function (id) {
        $http({
            url: '/Pessoa/Deletar',
            method: 'POST',
            data: { idPessoa: id }
        })
            .success(function (data, status, headers, config) {
                if (data == "True") {
                    $scope.msgSucesso = "Fornecedor removido com sucesso.";
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