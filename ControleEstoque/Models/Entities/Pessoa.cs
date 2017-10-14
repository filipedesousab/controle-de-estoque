using System;

namespace ControleEstoque.Models.Entities
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool IsPerfilCliente { get; set; }
        public bool IsPerfilVendedor { get; set; }
        public bool IsPerfilFornecedor { get; set; }
        public string Documento { get; set; }
        public string TipoDocumento { get; set; }
        public string Email { get; set; }
        public string Ddd { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public Municipio Municipio { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public char Status { get; set; }

        public Pessoa()
        {
            this.Municipio = new Municipio();
        }
    }
}