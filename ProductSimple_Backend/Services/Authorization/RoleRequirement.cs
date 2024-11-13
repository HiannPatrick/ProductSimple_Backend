using Microsoft.AspNetCore.Authorization;

namespace ProductSimple_Backend.Services
{
	public class RoleRequirement :IAuthorizationRequirement
	{
		public string Permission { get; }

		public RoleRequirement( string permission )
		{
			Permission = permission;
		}
	}
}
