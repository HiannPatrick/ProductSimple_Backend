using MediatR;
using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application
{ 
	public class GetAllCategoriesHandler :IRequestHandler<GetAllCategoriesQuery, List<Categoria>>
	{
		private readonly ICategoryRepository _categoryRepository;

		public GetAllCategoriesHandler( ICategoryRepository categoryRepository )
		{
			_categoryRepository = categoryRepository;
		}

		public async Task<List<Categoria>> Handle( GetAllCategoriesQuery request, CancellationToken cancellationToken )
		{
			try
			{
				var listProduct = await _categoryRepository.GetAllCategories();

				return listProduct;
			}
			catch( Exception )
			{
				return new List<Categoria>();
			}
		}
	}
}
