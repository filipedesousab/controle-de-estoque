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
        [HttpPost]
        public string Adicionar(Pessoa pessoa)
        {
            try
            {
                PessoaDal pessoaDal = new PessoaDal();

                if (!pessoaDal.VerificarSeExisteEmail(pessoa))
                {
                    bool result = pessoaDal.Adicionar(pessoa); // Chama o método Salvar e guarda o resultado na variavel result
                    if (result)
                    {
                        return "0"; // Sucesso
                    }
                    else
                    {
                        return "-1"; // Erro
                    }
                }
                else
                {
                    return "-2"; // Já existe
                }
            }
            catch
            {
                return "-1"; // Erro
            }
        }

        /// <summary>
        /// Método responsável por listar as pessoas ativas
        /// </summary>
        /// <returns>Uma lista JSON</returns>
        [HttpGet]
        public JsonResult ObterRegistrosAtivos()
        {
            PessoaDal pessoaDal = new PessoaDal();
            var list = pessoaDal.ObterRegistrosAtivos();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Método responsável por obter o registro da pessoa pelo id
        /// </summary>
        /// <param name="idPessoa">Id da pessoa</param>
        /// <returns>Um objeto JSON</returns>
        [HttpPost]
        public JsonResult ObterRegistro(int idPessoa)
        {
            PessoaDal pessoaDal = new PessoaDal();
            var resultado = pessoaDal.ObterRegistro(idPessoa);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Edita pessoa
        /// </summary>
        /// <param name="pessoa">Objeto Pessoa</param>
        /// <returns>0 ou -1</returns>
        [HttpPost]
        public string Alterar(Pessoa pessoa)
        {
            PessoaDal pessoaDal = new PessoaDal(); // Cria Instancia da User DAL

            if (!pessoaDal.VerificarSeExisteEmailOutraPessoa(pessoa))
            {
                bool result = pessoaDal.Alterar(pessoa); // Chama o método Salvar e guarda o resultado na variavel result
                if (result)
                {
                    return "0"; // Sucesso
                }
                else
                {
                    return "-1"; // Erro
                }
            }
            else
            {
                return "-2"; // Já existe
            }
        }

        /// <summary>
        /// Desativa uma pessoa
        /// </summary>
        /// <param name="idPessoa">ID da pessoa para desativar</param>
        /// <returns></returns>
        [HttpPost]
        public bool Desativar(int idPessoa)
        {
            PessoaDal notaFiscalDal = new PessoaDal();
            return notaFiscalDal.Desativar(idPessoa);
        }

        /// <summary>
        /// Deleta uma pessoa
        /// </summary>
        /// <param name="idPessoa">ID da pessoa para deletar</param>
        /// <returns></returns>
        [HttpPost]
        public bool Deletar(int idPessoa)
        {
            PessoaDal pessoaDal = new PessoaDal();
            return pessoaDal.Deletar(idPessoa);
        }
    }
}