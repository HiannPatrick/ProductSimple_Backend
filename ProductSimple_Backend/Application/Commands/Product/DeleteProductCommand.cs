using MediatR;

namespace ProductSimple_Backend.Application
{
	public record DeleteProductCommand( int Id ) :IRequest<ReturnCommon>;
}
