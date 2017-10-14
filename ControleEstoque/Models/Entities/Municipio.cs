
namespace ControleEstoque.Models.Entities
{
    public class Municipio
    {
        public int Id { get; set; }
        public Estado Estado { get; set; }
        public string Nome { get; set; }

        public Municipio()
        {
            this.Estado = new Estado();
        }
    }
}