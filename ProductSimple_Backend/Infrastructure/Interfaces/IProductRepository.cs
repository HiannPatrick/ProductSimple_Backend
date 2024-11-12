using ProductSimple_Backend.Application;
using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Infrastructure
{
	public interface IProductRepository
	{
		Task<bool> Create( Produto product );
		Task<bool> Update( Produto product );
		Task<bool> Delete( int id );
		Task<Produto?> GetProductById( int id );
		Task<PaginatedResult<Produto>> GetProductsByName( string name, int pageNumber, int pageSize );
		Task<PaginatedResult<Produto>> GetProductsByDescription( string description, int pageNumber, int pageSize );
		Task<PaginatedResult<Produto>> GetAllProducts( int pageNumber, int pageSize );
	}
}
