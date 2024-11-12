using MediatR;

using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Application
{
	public record GetProductsByNameQuery( string Name, int PageNumber, int PageSize ) :IRequest<PaginatedResult<Produto>>;
}
