using ControleEstoque.DAL;
using ControleEstoque.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Adicionar(Pessoa pessoa)
        {
            PessoaController pc = new PessoaController();
            return pc.Adicionar(pessoa);
        }
    }
}