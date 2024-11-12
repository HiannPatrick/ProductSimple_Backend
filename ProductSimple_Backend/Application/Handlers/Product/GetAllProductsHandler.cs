﻿using MediatR;
using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Domain.Dto;
using ProductSimple_Backend.Infrastructure;

namespace ProductSimple_Backend.Application.Handlers
{
    public class GetAllProductsHandler :IRequestHandler<GetAllProductsQuery, PaginatedResultDto<Produto>>
	{
		private readonly IProductRepository _productRepository;

		public GetAllProductsHandler( IProductRepository productRepository )
		{
			_productRepository = productRepository;
		}

		public async Task<PaginatedResultDto<Produto>> Handle( GetAllProductsQuery request, CancellationToken cancellationToken )
		{
			try
			{
				var listProduct = await _productRepository.GetAllProducts( request.PageNumber, request.PageSize);

				return listProduct;
			}
			catch( Exception )
			{
				return new PaginatedResultDto<Produto>( new List<Produto>(), 0, request.PageNumber, request.PageSize );
			}
		}
	}
}
