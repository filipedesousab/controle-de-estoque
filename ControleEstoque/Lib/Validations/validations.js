$.validarCampos = function (array, callback) {
    var retorno = null;

    for (var i = 0; i < array.length; i++) {
        switch (array[i]) {
            case 'nome':
                retorno = validaNome();
                break;
            case 'email':
                retorno = validaEmail();
                break;
            case 'ddd':
                retorno = validaDdd();
                break;
            case 'telefone':
                retorno = validaTelefone();
                break;
            case 'documento':
                retorno = validaDocumento();
                break;
            case 'cep':
                retorno = validaCep();
                break;
            case 'endereco':
                retorno = validaEndereco();
                break;
            case 'bairro':
                retorno = validaBairro();
                break;
            case 'cidade':
                retorno = validaCidade();
                break;
            case 'uf':
                retorno = validaUf();
                break;
            case 'dinheiro':
                validaDinheiro();
                break;
            case 'descricao':
                validaDescricao();
                break;
            case 'quantidade':
                validaQuantidade();
                break;
            case 'fornecedor':
                validaFornecedor();
                break;
        }
        if (!retorno) {
            break;
        }
    }
    return retorno;
}

function mascaraData(value = "") {
    var elemento = value == "" ? $('.data-mascara') : value;
    //  /  /   20
    //  /  /  20
    var stringValor = $(elemento).val();
    stringValor = stringValor.replace(/\D*/g, "");
    var novaString = '';
    for (var i = 0; i < 8 - stringValor.length; i++) {
        novaString += ' ';
    }
    novaString += stringValor;
    novaString = novaString.substr(0, 2) + '/' + novaString.substr(2, 2) + '/' + novaString.substr(4, 4);
    $(elemento).val(novaString);
}

function mascaraDinheiro(value = "") {
    var elemento = value == "" ? $('.dinheiro-mascara') : value;
    var stringValor = $(elemento).val().replace(",", "");
    console.log(stringValor);
    stringValor = stringValor.length == 0 ? "0000" : stringValor;
    stringValor = stringValor.length == 1 ? "000" + stringValor : stringValor;
    stringValor = stringValor.length == 2 ? "00" + stringValor : stringValor;
    stringValor = stringValor.length == 3 ? "0" + stringValor : stringValor;
    stringValor = stringValor.substr(0, stringValor.length - 2) + "." + stringValor.substr(stringValor.length - 2, stringValor.length - 1);
    console.log(stringValor); // 22 .2
    var numValor = parseFloat(stringValor).toFixed(2); //123.21
    console.log(numValor);
    $(elemento).val(numValor.replace(".", ","));
}

function validaData(value = "") {
    var elemento = value == "" ? $('.data-validate') : value;
    var valor = $(elemento).val().replace(/\D*/g, "");
    if ($(elemento).val() == "" || $(elemento).val() == undefined || /^[^0-9]+$/g.test(valor)) {
        $('div .' + $(elemento).attr('name')).html('Data invalida.');
        $('div .' + $(elemento).attr('name')).show();
        return false;
    } else {
        $('div .' + $(elemento).attr('name')).hide();
        return true;
    }
}

function validaPreco(value = "") {
    var elemento = value == "" ? $('.preco-validate') : value;
    var valor = $(elemento).val().replace(',','');
    if ($(elemento).val() == "" || $(elemento).val() == undefined || /^[^0-9]+$/g.test(valor)) {
        $('div .' + $(elemento).attr('name')).html('Valor invalido.');
        $('div .' + $(elemento).attr('name')).show();
        return false;
    } else {
        $('div .' + $(elemento).attr('name')).hide();
        return true;
    }
}

function validaNome(value = "") {
    var elemento = value == "" ? $('.nome-validate') : value;
    if ($(elemento).val() == "" || $(elemento).val() == undefined) {
        $('div .' + $(elemento).attr('name')).html('Insira um nome.');
        $('div .' + $(elemento).attr('name')).show();
        return false;
    } else if (!/^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$/.test($(elemento).val())) {
        $('div .' + $(elemento).attr('name')).html('Caracteres invalidos! Insira apenas letras.');
        $('div .' + $(elemento).attr('name')).show();
        return false;
    } else {
        $('div .' + $(elemento).attr('name')).hide();
        return true;
    }
}

function validaDescricao(value = "") {
    var elemento = value == "" ? $('.descricao-validate') : value;
    if ($(elemento).val() == "" || $(elemento).val() == undefined) {
        $('div .' + $(elemento).attr('name')).html('Insira uma descricao.');
        $('div .' + $(elemento).attr('name')).show();
        return false;
    } else {
        $('div .' + $(elemento).attr('name')).hide();
        return true;
    }
}

function validaCampoNotNull(value = "") {
    var elemento = value == "" ? $('.notnull-validate') : value;
    if ($(elemento).val() == "" || $(elemento).val() == undefined) {
        $('div .' + $(elemento).attr('name')).html('Dado invalido.');
        $('div .' + $(elemento).attr('name')).show();
        return false;
    } else {
        $('div .' + $(elemento).attr('name')).hide();
        return true;
    }
}

function validaNumero(value = "") {
    var elemento = value == "" ? $('.numero-validate') : value;
    var valor = $(elemento).val();
    if ($(elemento).val() == "" || $(elemento).val() == undefined || /^[^0-9]+$/g.test(valor)) {
        $('div .' + $(elemento).attr('name')).html('Valor invalido.');
        $('div .' + $(elemento).attr('name')).show();
        return false;
    } else {
        $('div .' + $(elemento).attr('name')).hide();
        return true;
    }
}

function validaNumeroComMascara(value = "") {
    var elemento = value == "" ? $('.numero-mask-validate') : value;
    var valor = $(elemento).val().replace(/\D*/g, "");
    if ($(elemento).val() == "" || $(elemento).val() == undefined || /^[^0-9]+$/g.test(valor)) {
        $('div .' + $(elemento).attr('name')).html('Valor invalido.');
        $('div .' + $(elemento).attr('name')).show();
        return false;
    } else {
        $('div .' + $(elemento).attr('name')).hide();
    }
    return true;
}

function validaEmail(value = "") {
    var elemento = value == "" ? $('.email-validate') : value;
    var valor = $(elemento).val();
    var regex = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if ($(elemento).val() == "" || $(elemento).val() == undefined || !regex.test(valor)) {
        $('div .' + $(elemento).attr('name')).html('Email invalido.');
        $('div .' + $(elemento).attr('name')).show();
        return false;
    } else {
        $('div .' + $(elemento).attr('name')).hide();
    }
    return true;
}

function validaDdd() {
    return validaNumeroComMascara($('.ddd-validate'));
}
function validaTelefone() {
    return validaNumeroComMascara($('.telefone-validate'));
}
function validaDocumento() {
    return validaNumeroComMascara($('.documento-validate'));
}
function validaCep() {
    return validaNumeroComMascara($('.cep-validate'));
}
function validaEndereco() {
    return validaCampoNotNull($('.endereco-validate'));
}
function validaBairro() {
    return validaCampoNotNull($('.bairro-validate'));
}
function validaCidade() {
    return validaCampoNotNull($('.cidade-validate'));
}
function validaUf() {
    return validaCampoNotNull($('.uf-validate'));
}

$(document).on("keyup", ".data-mascara", function (argument) {
    mascaraData(this);
});

$(document).on("keyup", ".data-validate", function (argument) {
    validaData(this);
});

$(document).on("keyup", ".dinheiro-mascara", function (argument) {
    mascaraDinheiro(this);
});

$(document).on("keyup", ".preco-validate", function (argument) {
    validaPreco(this);
});

$(document).on("keyup", ".nome-validate", function (argument) {
    validaNome(this);
});

$(document).on("keyup", ".descricao-validate", function (argument) {
    validaDescricao(this);
});

$(document).on("keyup", ".numero-validate", function (argument) {
    validaNumero(this);
});
$(document).on("keyup", ".quantidade-validate", function (argument) {
    validaNumero(this);
});
$(document).on("keyup", ".email-validate", function (argument) {
    validaEmail(this);
});
$(document).on("keyup", ".ddd-validate", function (argument) {
    validaNumeroComMascara(this);
});
$(document).on("keyup", ".telefone-validate", function (argument) {
    validaNumeroComMascara(this);
});
$(document).on("keyup", ".documento-validate", function (argument) {
    validaNumeroComMascara(this);
});
$(document).on("keyup", ".cep-validate", function (argument) {
    validaNumeroComMascara(this);
});
$(document).on("keyup", ".endereco-validate", function (argument) {
    validaCampoNotNull(this);
});
$(document).on("keyup", ".bairro-validate", function (argument) {
    validaCampoNotNull(this);
});
$(document).on("keyup", ".cidade-validate", function (argument) {
    validaCampoNotNull(this);
});
$(document).on("keyup", ".uf-validate", function (argument) {
    validaCampoNotNull(this);
});