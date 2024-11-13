using MediatR;

using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Infrastructure;

using System;

namespace ProductSimple_Backend.Application.Handlers
{
	public class UpdateProductHandler :IRequestHandler<UpdateProductCommand, ReturnCommon>
	{
		private readonly IProductRepository _productRepository;
		private readonly IWebHostEnvironment _environment;
		private readonly ProductValidator _productValidator;
		public UpdateProductHandler( IProductRepository productRepository, IWebHostEnvironment environment )
		{
			_productRepository = productRepository;
			_environment = environment;
			_productValidator = new ProductValidator();
		}

		public async Task<ReturnCommon> Handle( UpdateProductCommand request, CancellationToken cancellationToken )
		{
			try
			{
				string? fileName = null;

				if( request.Imagem != null && request.Imagem.Length > 0 )
				{
					fileName = $"{Guid.NewGuid()}_{request.Imagem.FileName}";

					string? filePath = Path.Combine(_environment.WebRootPath, "images", fileName);

					using( var stream = new FileStream( filePath, FileMode.Create ) )
					{
						await request.Imagem.CopyToAsync( stream );
					}
				}

				string imagem = fileName is null ? "" : $"/images/{fileName}";

				var product = new Produto
				{
					Id = request.Id,
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
					   ? ReturnCommon.SuccessMessage( "Produto atualizado com sucesso!" )
					   : ReturnCommon.FailureMessage( "Falha ao criar o produto!" );
			}
			catch( Exception )
			{
				return ReturnCommon.FailureMessage( "Falha ao criar o produto!" );
			}
		}
	}
}
