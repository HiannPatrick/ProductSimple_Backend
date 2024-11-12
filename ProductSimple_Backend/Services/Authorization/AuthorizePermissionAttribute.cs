using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ProductSimple_Backend.Services.Authorization
{
	public class AuthorizePermissionAttribute :AuthorizeAttribute, IAuthorizationFilter, IAuthorizationRequirement
	{
		private readonly string _permission;

		public AuthorizePermissionAttribute( string permission )
		{
			_permission = permission;
		}

		public void OnAuthorization( AuthorizationFilterContext context )
		{
			var user = context.HttpContext.User;

			if( !user.HasClaim( "permission", _permission ) )
			{
				context.Result = new ForbidResult();
			}
		}
	}

}
