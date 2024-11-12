using MediatR;

namespace ProductSimple_Backend.Application
{
	public record DeleteCategoryCommand( int Id ) :IRequest<ReturnCommon>;
}
