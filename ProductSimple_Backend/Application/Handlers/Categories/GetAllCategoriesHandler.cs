using MediatR;
using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application
{ 
	public class GetAllCategoriesHandler :IRequestHandler<GetAllCategoriesQuery, List<CategoriaDto>>
	{
		private readonly ICategoryRepository _categoryRepository;

		public GetAllCategoriesHandler( ICategoryRepository categoryRepository )
		{
			_categoryRepository = categoryRepository;
		}

		public async Task<List<CategoriaDto>> Handle( GetAllCategoriesQuery request, CancellationToken cancellationToken )
		{
			try
			{
				var listCategory = await _categoryRepository.GetAllCategories();

				var listCategoryDto = new List<CategoriaDto>();

				foreach ( var category in listCategory )
				{
					listCategoryDto.Add(new CategoriaDto(category));
				}

				return listCategoryDto;
			}
			catch( Exception )
			{
				return new List<CategoriaDto>();
			}
		}
	}
}
