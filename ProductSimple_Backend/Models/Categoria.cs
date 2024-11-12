using System.ComponentModel.DataAnnotations;

namespace ProductSimple_Backend.Models
{
	public class Categoria
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength( 50 )]
		public string Nome { get; set; }
		public ICollection<Produto>? Produtos { get; set; }
	}
}
