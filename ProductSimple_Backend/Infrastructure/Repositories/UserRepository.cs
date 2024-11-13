using Microsoft.EntityFrameworkCore;
using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Migrations;

namespace ProductSimple_Backend.Infrastructure
{
    public class UserRepository :IUserRepository
	{
		private readonly DataContext _context;

		public UserRepository( DataContext context )
        {
			_context = context;
        }
        public async Task<bool> Create( User user )
		{
			_context.Users.Add( user );

			var result = await _context.SaveChangesAsync();

			return result > 0;
		}
		public async Task<bool> Update( User user )
		{
			_context.Entry( user ).State = EntityState.Modified;

			var result = await _context.SaveChangesAsync();

			return result > 0;
		}

		public async Task<List<User>> GetAllUsers()
		{
			return await _context.Users.ToListAsync();
		}

		public async Task<bool> Delete( int id )
		{
			User? user = await _context.Users.FindAsync( id );

			if( user == null )
				return false;

			_context.Users.Remove( user );

			var result = await _context.SaveChangesAsync();

			return result > 0;
		}
	}
}
