using MediatR;

using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Application
{
	public record UpdateUserCommand( int Id, string Email, string PasswordHash, List<Role> Roles ) :IRequest<ReturnCommon>;
}
