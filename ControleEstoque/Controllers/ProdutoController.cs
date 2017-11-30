using ControleEstoque.DAL;
using ControleEstoque.Models.Entities;
using System.Web.Mvc;

namespace ControleEstoque.Controllers
{
    public class ProdutoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método responsável por adicionar um produto
        /// </summary>
        /// <returns>Uma lista JSON</returns>
        [HttpPost]
        public string Adicionar(Produto produto)
        {
            ProdutoDal produtoDal = new ProdutoDal();

            bool result = produtoDal.Adicionar(produto); // Chama o método Salvar e guarda o resultado na variavel result
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
        /// Método responsável por listar todos os produtos
        /// </summary>
        /// <returns>Uma lista JSON</returns>
        [HttpGet]
        public JsonResult ObterRegistros()
        {
            ProdutoDal produtoDal = new ProdutoDal();
            var list = produtoDal.ObterRegistros();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Método responsável por obter o registro do produto pelo id
        /// </summary>
        /// <param name="idProduto">Id do produto</param>
        /// <returns>Um objeto JSON</returns>
        [HttpPost]
        public JsonResult ObterRegistro(int idProduto)
        {
            ProdutoDal produtoDal = new ProdutoDal();
            var resultado = produtoDal.ObterRegistro(idProduto);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Edita produto
        /// </summary>
        /// <param name="pessoa">Objeto Produto</param>
        /// <returns>0 ou -1</returns>
        [HttpPost]
        public string Alterar(Produto produto)
        {
            try
            {
                ProdutoDal produtoDal = new ProdutoDal(); // Cria Instancia da DAL

                bool result = produtoDal.Alterar(produto); // Chama o método Salvar e guarda o resultado na variavel result
                if (result)
                {
                    return "0"; // Sucesso
                }
                else
                {
                    return "-1"; // Erro
                }
            }
            catch
            {
                return "-1"; // Erro
            }
        }

        /// <summary>
        /// Deleta um produto
        /// </summary>
        /// <param name="idProduto">ID do produto para deletar</param>
        /// <returns></returns>
        [HttpPost]
        public bool Deletar(int idProduto)
        {
            try
            {
                ProdutoDal produtoDal = new ProdutoDal();
                return produtoDal.Deletar(idProduto);
            }
            catch
            {
                return false;
            }
        }

    }
}