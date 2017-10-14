using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleEstoque.Models.Entities
{
    public class Venda
    {
        public int Id { get; set; }
        public Pessoa Vendedor { get; set; }
        public DateTime Data { get; set; }
        public double Desconto { get; set; }
        public double Valor { get; set; }

        public Venda()
        {
            this.Vendedor = new Pessoa();
        }
    }
}