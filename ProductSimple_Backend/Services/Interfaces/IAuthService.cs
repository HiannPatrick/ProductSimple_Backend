using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Services
{
    public interface IAuthService
    {
        public string GenerateJwtToken( User user );

        public bool VerifyPassword( string enteredPassword, string storedHash );
    }
}
