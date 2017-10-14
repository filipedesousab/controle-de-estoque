
namespace ControleEstoque.Models.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int QuantidadeEstoque { get; set; }
        public double PrecoVenda { get; set; }
        public Pessoa Fornecedor { get; set; }

        public Produto()
        {
            this.Fornecedor = new Pessoa();
        }
    }
}