using ControleEstoque.DAL;
using ControleEstoque.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Controllers
{
    public class PessoaController : Controller
    {
        internal string Adicionar(Pessoa pessoa)
        {
            PessoaDal pessoaDal = new PessoaDal();
            if (pessoaDal.Adicionar(pessoa))
            {
                return "0"; // Adicionou
            }
            else
            {
                return "-1"; // Não adicionou
            }
        }
    }
}