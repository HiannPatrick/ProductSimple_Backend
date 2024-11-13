using MediatR;
using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application
{ 
	public class GetAllUsersHandler :IRequestHandler<GetAllUsersQuery, List<User>>
	{
		private readonly IUserRepository _userRepository;

		public GetAllUsersHandler( IUserRepository userRepository )
		{
			_userRepository = userRepository;
		}

		public async Task<List<User>> Handle( GetAllUsersQuery request, CancellationToken cancellationToken )
		{
			try
			{
				var listUsers = await _userRepository.GetAllUsers();

				return listUsers;
			}
			catch( Exception )
			{
				return new List<User>();
			}
		}
	}
}
