using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Domain.Dto;

namespace ProductSimple_Backend.Infrastructure
{
    public interface IProductRepository
	{
		Task<bool> Create( Produto product );
		Task<bool> Update( Produto product );
		Task<bool> Delete( int id );
		Task<Produto?> GetProductById( int id );
		Task<PaginatedResultDto<Produto>> GetProductsByName( string name, int pageNumber, int pageSize );
		Task<PaginatedResultDto<Produto>> GetProductsByDescription( string description, int pageNumber, int pageSize );
		Task<PaginatedResultDto<Produto>> GetAllProducts( int pageNumber, int pageSize );
	}
}
