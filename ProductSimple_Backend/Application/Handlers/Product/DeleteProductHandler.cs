using MediatR;

using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application.Handlers
{
	public class DeleteProductHandler :IRequestHandler<DeleteProductCommand, ReturnCommon>
	{
		private readonly IProductRepository _productRepository;

		public DeleteProductHandler( IProductRepository productRepository )
		{
			_productRepository = productRepository;
		}

		public async Task<ReturnCommon> Handle( DeleteProductCommand request, CancellationToken cancellationToken )
		{
			try
			{

				var hadSuccess = await _productRepository.Delete(request.Id);

				return hadSuccess
					   ? ReturnCommon.SuccessMessage( "Produto excluído com sucesso!" )
					   : ReturnCommon.FailureMessage( "Falha ao excluir o produto!" );
			}
			catch( Exception )
			{
				return ReturnCommon.FailureMessage( "Falha ao excluir o produto!" );
			}
		}
	}
}
