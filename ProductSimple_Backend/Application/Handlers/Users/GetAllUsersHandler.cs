using MediatR;
using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application
{ 
	public class GetAllUsersHandler :IRequestHandler<GetAllUsersQuery, List<UserDto>>
	{
		private readonly IUserRepository _userRepository;

		public GetAllUsersHandler( IUserRepository userRepository )
		{
			_userRepository = userRepository;
		}

		public async Task<List<UserDto>> Handle( GetAllUsersQuery request, CancellationToken cancellationToken )
		{
			try
			{
				var listUsers = await _userRepository.GetAllUsers();

				var listUsersDto = new List<UserDto>();

				foreach ( var user in listUsers )
				{
					listUsersDto.Add( new UserDto( user ) );
				}

				return listUsersDto;
			}
			catch( Exception )
			{
				return new List<UserDto>();
			}
		}
	}
}
