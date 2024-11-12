using MediatR;

using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application.Handlers
{
	public class CreateProductHandler :IRequestHandler<CreateProductCommand, ReturnCommon>
	{
		private readonly IProductRepository _productRepository;
		private readonly ProductValidator _productValidator;
		public CreateProductHandler( IProductRepository productRepository )
		{
			_productRepository = productRepository;
			_productValidator = new ProductValidator();
		}

		public async Task<ReturnCommon> Handle( CreateProductCommand request, CancellationToken cancellationToken )
		{
			try
			{
				var product = new Produto
				{
					Nome = request.Nome,
					Descricao = request.Descricao,
					DataValidade = request.DataValidade,
					Imagem = request.Imagem,
					Preco = request.Preco,
					CategoriaId = request.CategoriaId			
				};

				var validation = _productValidator.Validate( product );

				if( !validation.IsValid )
				{
					string error = validation.GetErrorMessage();

					return ReturnCommon.FailureMessage( error );
				}

				var hadSuccess = await _productRepository.Create(product);

				return hadSuccess
					   ? ReturnCommon.SuccessMessage( "Produto criado com sucesso!" )
					   : ReturnCommon.FailureMessage( "Falha ao criar o produto!" );
			}
			catch( Exception )
			{
				return ReturnCommon.FailureMessage( "Falha ao criar o produto!" );
			}
		}
	}
}
