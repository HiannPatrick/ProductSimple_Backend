using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductSimple_Backend.Controllers
{
	[Route( "api/auth" )]
	[ApiController]
	public class AuthController :ControllerBase
	{
		private readonly string _secretKey;

		public AuthController( IConfiguration configuration )
		{
			_secretKey = configuration[ "Jwt:SecretKey" ] ?? "";
		}

		[HttpPost( "token" )]
		public IActionResult GetToken()
		{
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, "AnonymousUser"),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			byte[] encodedSecretKey = Encoding.ASCII.GetBytes(_secretKey);

			var key = new SymmetricSecurityKey(encodedSecretKey);
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.UtcNow.AddHours(1),
				signingCredentials: creds
			);

			var securityHandler = new JwtSecurityTokenHandler();

			string tokenString = securityHandler.WriteToken(token);

			return Ok( new { token = tokenString } );
		}
	}
}
