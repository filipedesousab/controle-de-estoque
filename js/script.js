$(document).ready(function(){
	$('[data-toggle="offcanvas"]').click(function(){
	   $("#navigation").toggleClass("hidden-xs");
	});

	function ativarBtn(btn) {
		$(".btn-menu").removeClass('active');
		$(btn).addClass('active');
	}

	function chamarPagina(page) {
		$("div.conteudo").html($("#"+page).html());
		$("div.conteudo").attr("pagina-atual",page);
		ativarBtn(".btn-"+page);
		$(document).ready();
	}

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
							'placeholder':'Nome'
						},
						{
							'type':'text',
							'name':'quantidade',
							'placeholder':'123'
						},
						{
							'type':'select',
							'name':'cliente',
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
				string += "<input type='"+conteudo.inputs[i].type+"' name='"+conteudo.inputs[i].name+"' placeholder='"+conteudo.inputs[i].placeholder+"' value='";
				string += conteudo.inputs[i].value != undefined ? conteudo.inputs[i].value : '';
				string += "'>";
			}else if(conteudo.inputs[i].type == "select"){
				string += "<select name='"+conteudo.inputs[i].name+"' class='form-control'>";
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
	chamarPagina('home');
});