using Microsoft.IdentityModel.Tokens;
using ProductSimple_Backend.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductSimple_Backend.Services.Authorization
{
	public class AuthService
	{
		private readonly IConfiguration _configuration;

		public AuthService( IConfiguration configuration )
		{
			_configuration = configuration;
		}

		public string GenerateJwtToken( User user )
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = user.Permissions.Select(permission => new Claim("permission", permission)).ToList();

			claims.Add( new Claim( ClaimTypes.Email, user.Email ) );

			var token = new JwtSecurityToken(
			issuer: _configuration["Jwt:Issuer"],
			audience: _configuration["Jwt:Audience"],
			claims: claims,
			expires: DateTime.Now.AddHours(1),
			signingCredentials: credentials
		);

			return new JwtSecurityTokenHandler().WriteToken( token );
		}

		public bool VerifyPassword( string enteredPassword, string storedHash )
		{
			// Verificar a senha com um algoritmo de hashing seguro
			return true;
		}
	}
}
