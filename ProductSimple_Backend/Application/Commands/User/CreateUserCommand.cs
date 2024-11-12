using MediatR;

namespace ProductSimple_Backend.Application
{
	public record CreateUserCommand( string Email, string PasswordHash, List<string> Permissions ) :IRequest<ReturnCommon>;
}
