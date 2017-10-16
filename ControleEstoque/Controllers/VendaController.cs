using ControleEstoque.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Controllers
{
    public class VendaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método responsável por obter o registro da venda pelo id
        /// </summary>
        /// <param name="idVenda">Id da venda</param>
        /// <returns>Um objeto JSON</returns>
        [HttpPost]
        public JsonResult ObterRegistro(int idVenda)
        {
            VendaDal vendaDal = new VendaDal();
            var resultado = vendaDal.ObterRegistro(idVenda);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Deleta uma venda
        /// </summary>
        /// <param name="idVenda">ID da venda para deletar</param>
        /// <returns></returns>
        [HttpPost]
        public bool Deletar(int idVenda)
        {
            VendaDal vendaDal = new VendaDal();
            return vendaDal.Deletar(idVenda);
        }

    }
}