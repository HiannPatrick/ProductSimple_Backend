using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductSimple_Backend.Domain
{
	public class ProdutoDto
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Descricao { get; set; }

		public double Preco { get; set; }
		public DateTime? DataValidade { get; set; }
		public string Imagem { get; set; }

		public int CategoriaId { get; set; }
		public string CategoriaNome { get; set; }

        public ProdutoDto(Produto produto)
        {
            Id = produto.Id;
			Nome = produto.Nome;
			Descricao = produto.Descricao;
			Preco = produto.Preco;
			DataValidade = produto.DataValidade;
			Imagem = produto.Imagem;
			CategoriaId = produto.CategoriaId;
			CategoriaNome = produto.Categoria is null ? "" : produto.Categoria.Nome;
        }
    }
}
