using MediatR;

namespace ProductSimple_Backend.Application
{
	public record UpdateUserCommand( int Id, string Email, string PasswordHash, List<string> Permissions ) :IRequest<ReturnCommon>;
}
