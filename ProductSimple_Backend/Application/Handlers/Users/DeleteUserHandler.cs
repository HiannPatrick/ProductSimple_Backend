using MediatR;

using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application
{
	public class DeleteUserHandler :IRequestHandler<DeleteUserCommand, ReturnCommon>
	{
		private readonly IUserRepository _userRepository;
		public DeleteUserHandler( IUserRepository userRepository )
		{
			_userRepository = userRepository;
		}

		public async Task<ReturnCommon> Handle( DeleteUserCommand request, CancellationToken cancellationToken )
		{
			try
			{
				var hadSuccess = await _userRepository.Delete(request.Id);

				return hadSuccess
					   ? ReturnCommon.SuccessMessage( "Usuário excluído com sucesso!" )
					   : ReturnCommon.FailureMessage( "Falha ao excluir o usuário!" );
			}
			catch( Exception )
			{
				return ReturnCommon.FailureMessage( "Falha ao excluir o usuário!" );
			}
		}
	}
}
