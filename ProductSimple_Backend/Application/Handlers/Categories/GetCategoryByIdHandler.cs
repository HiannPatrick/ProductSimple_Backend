using MediatR;

using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application
{
	public class GetCategoryByIdHandler :IRequestHandler<GetCategoryByIdQuery, CategoriaDto>
	{
		private readonly ICategoryRepository _categoryRepository;

		public GetCategoryByIdHandler( ICategoryRepository categoryRepository )
		{
			_categoryRepository = categoryRepository;
		}

		public async Task<CategoriaDto?> Handle( GetCategoryByIdQuery request, CancellationToken cancellationToken )
		{
			try
			{
				var category = await _categoryRepository.GetCategoryById(request.Id);

				if(category is null)
					return null;

				return  new CategoriaDto(category);
			}
			catch( Exception )
			{
				return null;
			}
		}
	}
}
