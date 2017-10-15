/*
 * Esse arquivo está sendo modificado
 * @Filipe
 */

$(document).ready(function(){
	$(".container-principal").attr("style","height: "+(window.innerHeight)+"px; width: "+(window.innerWidth+1)+"px;")
	$('[data-toggle="offcanvas"]').click(function(){
	   $("#navigation").toggleClass("hidden-xs");
    });

    /**
     * Metodo para ativar o botão do menu referente a página atual
     * @Filipe
     */
	function ativarBtn(btn) {
		$(".btn-menu").removeClass('active');
		$(btn).addClass('active');
	}

    /**
     * Método para copiar o conteudo da div referente a página chamada pelo botão
     * @Filipe
     */
	function chamarPagina(page) {
		$("div .conteudo").html($("#"+page).html());
		$("div .conteudo").attr("pagina-atual",page);
		ativarBtn(".btn-"+page);
		$(document).ready();
	}
	chamarPagina('fornecedores');

	$(document).on("click",".btn-menu", function () {
   		console.log($(this).attr('page'));
   		chamarPagina($(this).attr('page'));

	});

	$(document).on("click",".btn-add-edit", function (argument) {
		switch($(this).attr('data-tipo')){
			case 'add-venda':
				criarModal({
					'titulo':'Adicionar Venda',
					'formName':'add-venda',
					'inputs':[
						{
							'type':'text',
							'name':'nome',
                            'placeholder': 'Nome',
                            'class': 'nome-validate'
						},
						{
							'type':'text',
							'name':'quantidade',
                            'placeholder': '123',
                            'class': 'qtd-validate'
						},
						{
							'type':'select',
                            'name': 'cliente',
                            'class': 'select-validate',
                            'lista-title':'Selecione um cliente',
							'lista':[
								{
									'value':'1',
									'text':'Fulano'
								},
								{
									'value':'2',
									'text':'Ciclano'
								}
							]
						}
					],
				})
				break;
			case 'add-produto':
				criarModal({
					'titulo':'Adicionar Produto',
					'formName':'add-produto',
					'inputs':[
						{
							'type':'text',
							'name':'nome',
							'placeholder':'Nome'
						},
						{
							'type':'text',
							'name':'quantidade',
							'placeholder':'123'
						},
						{
							'type':'text',
							'name':'preco',
							'placeholder':'99,00'
						}
					],
				})
				break;
			case 'add-cliente':
				criarModal({
					'titulo':'Adicionar Cliente',
					'formName':'add-cliente',
					'inputs':[
						{
							'type':'text',
							'name':'nome',
							'placeholder':'Nome'
						},
						{
							'type':'text',
							'name':'cnpj',
							'placeholder':'72.356.373/0001-00'
						},
						{
							'type':'text',
							'name':'endereco',
							'placeholder':'Rua R, Cidade - PE'
						}
					],
				})
				break;
			case 'add-fornecedor':
				criarModal({
					'titulo':'Adicionar Fornecedor',
					'formName':'add-produto',
					'inputs':[
						{
							'type':'text',
							'name':'nome',
							'placeholder':'Nome'
						},
						{
							'type':'text',
							'name':'cnpj',
							'placeholder':'72.356.373/0001-00'
						},
						{
							'type':'text',
							'name':'endereco',
							'placeholder':'Rua R, Cidade - PE'
						}
					],
				})
				break;
			case 'edit-produto'||'edit-cliente'||'edit-fornecedor':
				criarModal({
					'titulo':'Adicionar Fornecedor',
					'formName':'edit-produto',
					'inputs':[
						{
							'type':'text',
							'name':'nome',
							'placeholder':'Nome'
						},
						{
							'type':'text',
							'name':'cnpj',
							'placeholder':'72.356.373/0001-00'
						},
						{
							'type':'text',
							'name':'endereco',
							'placeholder':'Rua R, Cidade - PE'
						}
					],
				})
				break;
		}
	});
	function criarModal(conteudo) {
		$("#modal h4").html(conteudo.titulo);
		$("#modal form").attr("name", conteudo.formName);
		var string = "";
		for (var i = 0;i < conteudo.inputs.length; i++) {
			if(conteudo.inputs[i].type == "text"){
				string += "<input class='"+conteudo.inputs[i].class+"' type='"+conteudo.inputs[i].type+"' name='"+conteudo.inputs[i].name+"' placeholder='"+conteudo.inputs[i].placeholder+"' value='";
				string += conteudo.inputs[i].value != undefined ? conteudo.inputs[i].value : '';
                string += "'><div class='validacao-erro " + conteudo.inputs[i].name+"' style='display:none;'>Insira a informação solicitada válida!</div>";
			}else if(conteudo.inputs[i].type == "select"){
                string += "<select name='" + conteudo.inputs[i].name + "' class='form-control " + conteudo.inputs[i].class+"'>";
                string += "<option value=''>"+conteudo.inputs[i].lista-title+"</option>";
                var jsonLista = conteudo.inputs[i].lista;

				for (var i = 0; i < jsonLista.length; i++) {
					string += "<option value='"+jsonLista[i].value+"'>"+jsonLista[i].text+"</option>";
				}
				console.log(string);
			}
		}
		$("#modal .modal-body").html(string);
		$('#modal').modal();
	}

    /**
     * Validação de campos. Ainda em desenvolvimento.
     * @Filipe
     */
    $(document).on("keyup", ".alfa", function (argument) {
        if (!/^[a-zA-Záéíóúãõñàèìòùçäëïöü]+$/.test(argument.key)) {
            console.log("Inválido");
        }
    });

    $(document).on("keyup", ".nome-validate", function (argument) {
        console.log($(this).attr('name'));
        if (!/^[a-zA-Záéíóúãõñàèìòùçäëïöü]+/.test(argument.key)) {
            $('div .' + $(this).attr('name')).html('Caracteres inválidos! Insira apenas letras.');
            $('div .' + $(this).attr('name')).show();
            console.log("Caracteres inválidos: ");
            console.log($('div .' + $(this).attr('name')));
        } else {
            $('div .' + $(this).attr('name')).hide();
        }
    });

    $(document).on("keyup", ".money", function (argument) {
        console.log(argument.key);
        var stringValor = $(this).val().replace(",", "");
        console.log(stringValor);
        stringValor = stringValor.substr(0, stringValor.length - 2) + "." + stringValor.substr(stringValor.length - 2, stringValor.length - 1);
        console.log(stringValor); // 22 .2
        var numValor = parseFloat(stringValor).toString(); //123.21
        console.log(numValor);
        $(this).val(numValor.replace(".", ","));
    });

    /*$(document).on("keyup", ".qtd-validate", function (argument) {
        //console.log($(this).val());
        console.log($(this).attr('name'));
        if (!$(this).val().match(/[0-9]+/)) {
            $('div .' + $(this).attr('name')).html('Número inválido! Insira um número válido.');
            $('div .' + $(this).attr('name')).show();
            console.log("Caracteres inválidos: ");
            console.log($('div .' + $(this).attr('name')));
        } else {
            $('div .' + $(this).attr('name')).hide();
        }
    });*/
});
