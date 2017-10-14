using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleEstoque.Models.Entities
{
    public class ProdutoVenda
    {
        public int Id { get; set; }
        public Venda Venda { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }

        public ProdutoVenda()
        {
            this.Venda = new Venda();
            this.Produto = new Produto();
        }
    }
}