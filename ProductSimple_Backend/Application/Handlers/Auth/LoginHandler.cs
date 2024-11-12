using MediatR;
using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application.Handlers.Auth
{
	public class LoginHandler :IRequestHandler<LoginCommand, ReturnCommon>
	{
		private readonly ILoginRepository _loginRepository;
		private readonly LoginValidator _loginValidator;
		public LoginHandler( ILoginRepository loginRepository )
		{
			_loginRepository = loginRepository;
			_loginValidator = new LoginValidator();
		}

		public async Task<ReturnCommon> Handle( LoginCommand request, CancellationToken cancellationToken )
		{
			try
			{
				var login = new LoginDto
				{
					Email = request.Email,
					PasswordHash = request.PasswordHash
				};

				var validation = _loginValidator.Validate( login );

				if( !validation.IsValid )
				{
					string error = validation.GetErrorMessage();

					return ReturnCommon.FailureMessage( error );
				}

				return await _loginRepository.Authenticate(login);
			}
			catch( Exception )
			{
				return ReturnCommon.FailureMessage( "Falha ao efetuar o login!" );
			}
		}
	}
}
