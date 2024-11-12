using MediatR;

namespace ProductSimple_Backend.Application
{
	public record DeleteUserCommand( int Id ) :IRequest<ReturnCommon>;
}
