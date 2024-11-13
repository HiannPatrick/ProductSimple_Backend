using MediatR;

using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Application
{
	public record CreateUserCommand( string Email, string PasswordHash, List<Role> Roles ) :IRequest<ReturnCommon>;
}
