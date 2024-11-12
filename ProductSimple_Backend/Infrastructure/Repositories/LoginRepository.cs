using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

using ProductSimple_Backend.Application;
using ProductSimple_Backend.Data;
using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Services.Authorization;

namespace ProductSimple_Backend.Infrastructure
{
	public class LoginRepository : ILoginRepository
	{
		private readonly ProductSimpleDbContext _context;
		private readonly AuthService _authService;

		public LoginRepository( ProductSimpleDbContext context, AuthService authService )
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

			var token = _authService.GenerateJwtToken(user);

			return ReturnCommon.SuccessMessage( token );
		}
	}
}
