using Microsoft.AspNetCore.Authorization;

namespace ProductSimple_Backend.Services.Authorization
{
	public class AuthorizePermissionAttribute :Attribute, IAuthorizationRequirement
	{
		public string Permission { get; }

		public AuthorizePermissionAttribute( string permission )
		{
			Permission = permission;
		}
	}
}
