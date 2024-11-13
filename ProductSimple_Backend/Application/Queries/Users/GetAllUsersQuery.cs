using MediatR;

using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Application
{
	public record GetAllUsersQuery :IRequest<List<UserDto>>;
}
