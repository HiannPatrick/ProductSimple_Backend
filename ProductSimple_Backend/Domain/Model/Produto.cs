using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductSimple_Backend.Domain
{
	public class Produto
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength( 50 )]
		public string Nome { get; set; }

		[Required]
		[MaxLength( 200 )]
		public string Descricao { get; set; }

		public double Preco { get; set; }
		public DateTime? DataValidade { get; set; }

		[MaxLength( 255 )]
		public string Imagem { get; set; }

		[Required]
		[ForeignKey( "Categoria" )]
		public int CategoriaId { get; set; }
		public virtual Categoria Categoria { get; set; }
	}
}
