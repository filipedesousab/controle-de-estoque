using ControleEstoque.DAL;
using ControleEstoque.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Controllers
{
    public class ProdutoVendaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método responsável por adicionar uma lista de ProdutoVenda
        /// </summary>
        /// <returns>Uma lista JSON</returns>
        [HttpPost]
        public string Adicionar(List<ProdutoVenda> produtosVenda)
        {
            ProdutoVendaDal produtoVendaDal = new ProdutoVendaDal();

            bool result = produtoVendaDal.Adicionar(produtosVenda); // Chama o método Salvar e guarda o resultado na variavel result
            if (result)
            {
                return "0"; // Sucesso
            }
            else
            {
                return "-1"; // Erro
            }
        }

        /// <summary>
        /// Método responsável por obter uma lista dos itens da venda
        /// </summary>
        /// <param name="idVenda">Id da Venda</param>
        /// <returns>Um objeto JSON</returns>
        [HttpPost]
        public JsonResult ObterRegistro(int idVenda)
        {
            ProdutoVendaDal produtoVendaDal = new ProdutoVendaDal();
            var resultado = produtoVendaDal.ObterRegistro(idVenda);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Deleta um item da venda
        /// </summary>
        /// <param name="idProdutoVenda">ID do item para deletar</param>
        /// <returns></returns>
        [HttpPost]
        public bool Deletar(int idProdutoVenda)
        {
            ProdutoVendaDal produtoVendaDal = new ProdutoVendaDal();
            return produtoVendaDal.Deletar(idProdutoVenda);
        }

    }
}