using MediatR;

using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application
{
	public class UpdateUserHandler :IRequestHandler<UpdateUserCommand, ReturnCommon>
	{
		private readonly IUserRepository _userRepository;
		private readonly UserValidator _userValidator;
		public UpdateUserHandler( IUserRepository userRepository )
		{
			_userRepository = userRepository;
			_userValidator = new UserValidator();
		}

		public async Task<ReturnCommon> Handle( UpdateUserCommand request, CancellationToken cancellationToken )
		{
			try
			{
				var user = new User
				{
					Id = request.Id,
					Email = request.Email,
					PasswordHash = request.PasswordHash,
					Roles = request.Roles
				};

				var validation = _userValidator.Validate( user );

				if( !validation.IsValid )
				{
					string error = validation.GetErrorMessage();

					return ReturnCommon.FailureMessage( error );
				}

				var hadSuccess = await _userRepository.Update(user);

				return hadSuccess
					   ? ReturnCommon.SuccessMessage( "Usuário atualizado com sucesso!" )
					   : ReturnCommon.FailureMessage( "Falha ao atualizar usuario!" );
			}
			catch( Exception )
			{
				return ReturnCommon.FailureMessage( "Falha ao criar o usuário!" );
			}
		}
	}
}
