using MediatR;

namespace ProductSimple_Backend.Application
{
	public record UpdateCategoryCommand( int Id, string Nome ) :IRequest<ReturnCommon>;
}
