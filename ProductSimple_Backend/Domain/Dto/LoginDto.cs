using System.ComponentModel.DataAnnotations;

namespace ProductSimple_Backend.Domain
{
	public class LoginDto
	{
		[EmailAddress]
		[Required]
        public string Email { get; set; }
		[Required]
        public string PasswordHash { get; set; }
    }
}
