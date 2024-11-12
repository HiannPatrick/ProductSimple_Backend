using MediatR;

using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Application
{
	public record GetProductsByDescriptionQuery( string Description, int PageNumber, int PageSize ) :IRequest<PaginatedResult<Produto>>;
}
