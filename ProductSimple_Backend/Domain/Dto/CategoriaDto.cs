using System.ComponentModel.DataAnnotations;

namespace ProductSimple_Backend.Domain
{
	public class CategoriaDto
	{
		public int Id { get; set; }
		public string Nome { get; set; }

        public CategoriaDto(Categoria categoria)
        {
            Id = categoria.Id;
			Nome = categoria.Nome;
        }
    }
}
