using MediatR;

using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application.Handlers
{
	public class GetProductByIdHandler :IRequestHandler<GetProductByIdQuery, ProdutoDto>
	{
		private readonly IProductRepository _productRepository;

		public GetProductByIdHandler( IProductRepository productRepository )
		{
			_productRepository = productRepository;
		}

		public async Task<ProdutoDto?> Handle( GetProductByIdQuery request, CancellationToken cancellationToken )
		{
			try
			{
				var product = await _productRepository.GetProductById(request.Id);

				return  product is null ? null : new ProdutoDto( product );
			}
			catch( Exception )
			{
				return null;
			}
		}
	}
}
