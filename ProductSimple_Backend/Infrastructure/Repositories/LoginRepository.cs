using Microsoft.EntityFrameworkCore;

using ProductSimple_Backend.Application;
using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Migrations;
using ProductSimple_Backend.Services;

namespace ProductSimple_Backend.Infrastructure
{
    public class LoginRepository : ILoginRepository
	{
		private readonly DataContext _context;
		private readonly IAuthService _authService;

		public LoginRepository( DataContext context, IAuthService authService )
		{
            _context = context;
			_authService = authService;
		}

		public async Task<ReturnCommon> Authenticate( LoginDto login )
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);

			if( user == null || !_authService.VerifyPassword( login.PasswordHash, user.PasswordHash ) )
			{
				return ReturnCommon.FailureMessage( "Credenciais inválidas!" );
			}

			var roles = _context.Roles.Where( o => o.Users.Where( u => u.Id == user.Id ).Count() > 0 ).ToList();

			user.Roles = roles;

			var token = _authService.GenerateJwtToken(user);

			return ReturnCommon.SuccessMessage( token );
		}
	}
}
