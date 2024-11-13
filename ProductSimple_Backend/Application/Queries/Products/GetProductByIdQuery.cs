using MediatR;

using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Application
{
	public record GetProductByIdQuery( int Id ) :IRequest<ProdutoDto>;
}
