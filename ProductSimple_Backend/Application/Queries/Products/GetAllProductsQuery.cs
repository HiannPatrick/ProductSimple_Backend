using MediatR;

using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Application
{
	public record GetAllProductsQuery(int PageNumber, int PageSize ) :IRequest<PaginatedResult<Produto>>;
}
