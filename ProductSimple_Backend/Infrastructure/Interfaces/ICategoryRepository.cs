using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Infrastructure
{
	public interface ICategoryRepository
	{
		Task<bool> Create( Categoria category );
		Task<bool> Update( Categoria category );
		Task<bool> Delete( int id );
		Task<Categoria?> GetCategoryById( int id );
		Task<List<Categoria>> GetAllCategories();
	}
}
