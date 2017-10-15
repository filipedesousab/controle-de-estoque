using ControleEstoque.DAL;
using ControleEstoque.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Controllers
{
    public class VendedorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método responsável por listar os vendedores ativos
        /// </summary>
        /// <returns>Uma lista JSON</returns>
        [HttpGet]
        public JsonResult ListarVendedoresAtivos()
        {
            Pessoa pessoa = new Pessoa();
            pessoa.PerfilVendedor = 'S';
            pessoa.Status = 'A';

            PessoaDal pessoaDal = new PessoaDal();
            var list = pessoaDal.ObterRegistrosComFiltro(pessoa);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}