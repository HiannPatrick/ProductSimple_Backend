using MediatR;

using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application
{
	public class GetCategoryByIdHandler :IRequestHandler<GetCategoryByIdQuery, Categoria>
	{
		private readonly ICategoryRepository _categoryRepository;

		public GetCategoryByIdHandler( ICategoryRepository categoryRepository )
		{
			_categoryRepository = categoryRepository;
		}

		public async Task<Categoria?> Handle( GetCategoryByIdQuery request, CancellationToken cancellationToken )
		{
			try
			{
				var category = await _categoryRepository.GetCategoryById(request.Id);

				return category;
			}
			catch( Exception )
			{
				return null;
			}
		}
	}
}
