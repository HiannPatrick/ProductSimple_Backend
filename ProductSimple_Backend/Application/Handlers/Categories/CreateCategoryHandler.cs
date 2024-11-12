using MediatR;

using ProductSimple_Backend.Application;
using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application
{
	public class CreateCategoryHandler :IRequestHandler<CreateCategoryCommand, ReturnCommon>
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly CategoryValidator _categoryValidator;
		public CreateCategoryHandler( ICategoryRepository categoryRepository )
		{
			_categoryRepository = categoryRepository;
			_categoryValidator = new CategoryValidator();
		}

		public async Task<ReturnCommon> Handle( CreateCategoryCommand request, CancellationToken cancellationToken )
		{
			try
			{
				var category = new Categoria
				{
					Nome = request.Nome			
				};

				var validation = _categoryValidator.Validate( category );

				if( !validation.IsValid )
				{
					string error = validation.GetErrorMessage();

					return ReturnCommon.FailureMessage( error );
				}

				var hadSuccess = await _categoryRepository.Create(category);

				return hadSuccess
					   ? ReturnCommon.SuccessMessage( "Categoria criada com sucesso!" )
					   : ReturnCommon.FailureMessage( "Falha ao criar a categoria!" );
			}
			catch( Exception )
			{
				return ReturnCommon.FailureMessage( "Falha ao criar a categoria!" );
			}
		}
	}
}
