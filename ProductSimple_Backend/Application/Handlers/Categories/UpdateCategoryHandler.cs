using MediatR;

using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application
{
	public class UpdateCategoryHandler :IRequestHandler<UpdateCategoryCommand, ReturnCommon>
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly CategoryValidator _categoryValidator;
		public UpdateCategoryHandler( ICategoryRepository categoryRepository )
		{
			_categoryRepository = categoryRepository;
			_categoryValidator = new CategoryValidator();
		}

		public async Task<ReturnCommon> Handle( UpdateCategoryCommand request, CancellationToken cancellationToken )
		{
			try
			{
				var category = new Categoria
				{
					Id = request.Id,
					Nome = request.Nome
				};

				var validation = _categoryValidator.Validate( category );

				if( !validation.IsValid )
				{
					string error = validation.GetErrorMessage();

					return ReturnCommon.FailureMessage( error );
				}

				var hadSuccess = await _categoryRepository.Update(category);

				return hadSuccess
					   ? ReturnCommon.SuccessMessage( "Categoria atualizada com sucesso!" )
					   : ReturnCommon.FailureMessage( "Falha ao atualizar a categoria!" );
			}
			catch( Exception )
			{
				return ReturnCommon.FailureMessage( "Falha ao atualizar a categoria!" );
			}
		}
	}
}