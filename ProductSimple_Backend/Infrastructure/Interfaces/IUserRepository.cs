using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Infrastructure
{
	public interface IUserRepository
	{
		Task<bool> Create( User user );
		Task<bool> Update( User user );
		Task<bool> Delete( int id );
		Task<List<User>> GetAllUsers();
	}
}
