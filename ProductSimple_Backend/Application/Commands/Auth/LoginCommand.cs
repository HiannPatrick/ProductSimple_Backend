using MediatR;

namespace ProductSimple_Backend.Application
{
	public record LoginCommand( string Email, string PasswordHash ) :IRequest<ReturnCommon>;
}
