using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using ProductSimple_Backend.Migrations;

using System.Security.Claims;

namespace ProductSimple_Backend.Services.Authorization
{
	public class RolesService :AuthorizationHandler<RoleRequirement>
	{
		private readonly DataContext _context;

		public RolesService( DataContext context )
		{
			_context = context;
		}

		protected override async Task HandleRequirementAsync( AuthorizationHandlerContext context, RoleRequirement requirement )
		{
			var claims = context.User.Claims.ToList();

			if( !claims.Any() )
				throw new UnauthorizedAccessException( "O token não possui claims." );

			var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

			if( email == null )
				throw new UnauthorizedAccessException( "O claim de e-mail não foi encontrado no token." );

			var user = await _context.Users
									 .Include(u => u.Roles)
									 .FirstOrDefaultAsync(u => u.Email == email);

			var hasPermission = user.Roles.Any( o => o.Name == requirement.Permission );

			if( user != null && hasPermission )
			{
				context.Succeed( requirement );
			}
		}
	}
}
