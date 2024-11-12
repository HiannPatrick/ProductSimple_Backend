using MediatR;

using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application
{
	public class DeleteCategoryHandler :IRequestHandler<DeleteCategoryCommand, ReturnCommon>
	{
		private readonly ICategoryRepository _categoryRepository;
		public DeleteCategoryHandler( ICategoryRepository categoryRepository )
		{
			_categoryRepository = categoryRepository;
		}

		public async Task<ReturnCommon> Handle( DeleteCategoryCommand request, CancellationToken cancellationToken )
		{
			try
			{
				var hadSuccess = await _categoryRepository.Delete(request.Id);

				return hadSuccess
					   ? ReturnCommon.SuccessMessage( "Categoria excluída com sucesso!" )
					   : ReturnCommon.FailureMessage( "Falha ao excluir a categoria!" );
			}
			catch( Exception )
			{
				return ReturnCommon.FailureMessage( "Falha ao excluir a categoria!" );
			}
		}
	}
}
