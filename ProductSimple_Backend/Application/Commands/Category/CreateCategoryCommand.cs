using MediatR;

namespace ProductSimple_Backend.Application
{
	public record CreateCategoryCommand( string Nome ) :IRequest<ReturnCommon>;
}
