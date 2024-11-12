using MediatR;
using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application.Handlers
{
	public class GetAllProductsHandler :IRequestHandler<GetAllProductsQuery, PaginatedResult<Produto>>
	{
		private readonly IProductRepository _productRepository;

		public GetAllProductsHandler( IProductRepository productRepository )
		{
			_productRepository = productRepository;
		}

		public async Task<PaginatedResult<Produto>> Handle( GetAllProductsQuery request, CancellationToken cancellationToken )
		{
			try
			{
				var listProduct = await _productRepository.GetAllProducts( request.PageNumber, request.PageSize);

				return listProduct;
			}
			catch( Exception )
			{
				return new PaginatedResult<Produto>( new List<Produto>(), 0, request.PageNumber, request.PageSize );
			}
		}
	}
}
