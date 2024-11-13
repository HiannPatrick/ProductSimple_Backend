using MediatR;

using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application.Handlers
{
	public class CreateProductHandler :IRequestHandler<CreateProductCommand, ReturnCommon>
	{
		private readonly IProductRepository _productRepository;
		private readonly IWebHostEnvironment _environment;
		private readonly ProductValidator _productValidator;
		public CreateProductHandler( IProductRepository productRepository, IWebHostEnvironment environment )
		{
			_productRepository = productRepository;
			_environment = environment;
			_productValidator = new ProductValidator();
		}

		public async Task<ReturnCommon> Handle( CreateProductCommand request, CancellationToken cancellationToken )
		{
			try
			{
				string? fileName = null;

				if( request.Imagem != null && request.Imagem.Length > 0 )
				{
					fileName = $"{Guid.NewGuid()}_{request.Imagem.FileName}";

					string? filePath = Path.Combine(_environment.WebRootPath, fileName);

					using( var stream = new FileStream( filePath, FileMode.Create ) )
					{
						await request.Imagem.CopyToAsync( stream );
					}
				}

				string imagem = fileName is null ? "" : $"/images/{fileName}";

				var product = new Produto
				{
					Nome = request.Nome,
					Descricao = request.Descricao,
					DataValidade = request.DataValidade,
					Imagem = imagem,
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
