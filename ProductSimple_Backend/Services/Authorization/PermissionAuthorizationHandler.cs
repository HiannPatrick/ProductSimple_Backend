using Microsoft.AspNetCore.Authorization;

using ProductSimple_Backend.Data;
using ProductSimple_Backend.Services.Authorization;

namespace ProductSimple_Backend.Services
{
	public class PermissionAuthorizationHandler :AuthorizationHandler<AuthorizePermissionAttribute>
	{
		private readonly ProductSimpleDbContext _dbContext;

		public PermissionAuthorizationHandler( ProductSimpleDbContext dbContext )
		{
			_dbContext = dbContext;
		}

		protected override async Task HandleRequirementAsync( AuthorizationHandlerContext context, AuthorizePermissionAttribute requirement )
		{
			var userId = int.Parse(context.User.Identity.Name);

			var permissionName = requirement.Permission;

			//var userRoles = await _dbContext.UserRoles
			//                                .Where(ur => ur.UserId == userId)
			//                                .Include(ur => ur.Role)
			//                                .ThenInclude(r => r.RolePermissions)
			//                                .ThenInclude(rp => rp.Permission)
			//                                .ToListAsync();

			//var permissions = userRoles.SelectMany(ur => ur.Role.RolePermissions)
			//						   .Select(rp => rp.Permission.Name);

			//if( permissions.Contains( permissionName ) )
			//	context.Succeed( requirement );

			context.Fail();
		}
	}
}