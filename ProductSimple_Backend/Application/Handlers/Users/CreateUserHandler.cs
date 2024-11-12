using MediatR;

using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application
{
	public class CreateUserHandler :IRequestHandler<CreateUserCommand, ReturnCommon>
	{
		private readonly IUserRepository _userRepository;
		private readonly UserValidator _userValidator;
		public CreateUserHandler( IUserRepository userRepository )
		{
			_userRepository = userRepository;
			_userValidator = new UserValidator();
		}

		public async Task<ReturnCommon> Handle( CreateUserCommand request, CancellationToken cancellationToken )
		{
			try
			{
				var user = new User
				{
					Email = request.Email,
					PasswordHash = request.PasswordHash,
					Permissions = request.Permissions
				};

				var validation = _userValidator.Validate( user );

				if( !validation.IsValid )
				{
					string error = validation.GetErrorMessage();

					return ReturnCommon.FailureMessage( error );
				}

				var hadSuccess = await _userRepository.Create(user);

				return hadSuccess
					   ? ReturnCommon.SuccessMessage( "Usuário criada com sucesso!" )
					   : ReturnCommon.FailureMessage( "Falha ao criar usuario!" );
			}
			catch( Exception )
			{
				return ReturnCommon.FailureMessage( "Falha ao criar o usuário!" );
			}
		}
	}
}
