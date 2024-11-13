using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using ProductSimple_Backend.Application;
using ProductSimple_Backend.Services;

namespace ProductSimple_Backend.Controllers
{
	[Authorize]
	[ApiController]
	[Route( "api/[controller]" )]
	public class ProductsController :ControllerBase
	{
		private readonly IMediator _mediator;

		public ProductsController( IMediator mediator )
		{

			_mediator = mediator;
		}

		[HttpGet( "name" )]
		[Authorize( Policy = "GetProduct" )]
		public async Task<IActionResult> GetProductsByName( string name, int pageNumber = 1, int pageSize = 10 )
		{
			var query = new GetProductsByNameQuery(name, pageNumber, pageSize);

			var result = await _mediator.Send(query);

			if( result == null )
				return NotFound( ReturnCommon.FailureMessage( "Produtos não localizados." ) );

			return Ok( result );
		}

		[HttpGet( "description" )]
		[Authorize( Policy = "GetProduct" )]
		public async Task<IActionResult> GetProductsByDescription( string description, int pageNumber = 1, int pageSize = 10 )
		{
			var query = new GetProductsByDescriptionQuery(description, pageNumber, pageSize);

			var result = await _mediator.Send(query);

			if( result == null )
				return NotFound( ReturnCommon.FailureMessage( "Produtos não localizados." ) );

			return Ok( result );
		}

		[HttpGet("all")]
		[Authorize( Policy = "GetProduct" )]
		public async Task<IActionResult> GetAll( int pageNumber = 1, int pageSize = 10 )
		{
			var query = new GetAllProductsQuery(pageNumber, pageSize);

			var result = await _mediator.Send(query);

			if( result == null )
				return NotFound( ReturnCommon.FailureMessage( "Produtos não localizados." ) );

			return Ok( result );
		}

		[HttpGet( "{id}" )]
		[Authorize( Policy = "GetProduct" )]
		public async Task<IActionResult> Get( int id )
		{
			var query = new GetProductByIdQuery( id );

			var result = await _mediator.Send(query);

			if( result == null )
				return NotFound( ReturnCommon.FailureMessage( "Produto não localizado." ) );

			return Ok( result );
		}

		[HttpPost]
		[Authorize( Policy = "CreateProduct" )]
		public async Task<IActionResult> Create( [FromBody] CreateProductCommand command )
		{
			var result = await _mediator.Send(command);

			return result.Success ? Ok( result ) : BadRequest( result );
		}

		[HttpPut( "{id}" )]
		[Authorize( Policy = "EditProduct" )]
		public async Task<IActionResult> Update( int id, [FromBody] UpdateProductCommand command )
		{
			if( id != command.Id )
				return BadRequest( ReturnCommon.FailureMessage( "ID do produto inválido." ) );

			var result = await _mediator.Send(command);

			return result.Success ? Ok( result ) : BadRequest( result );
		}

		[HttpDelete( "{id}" )]
		[Authorize( Policy = "DeleteProduct" )]
		public async Task<IActionResult> Delete( int id )
		{
			var command = new DeleteProductCommand(id);

			var result = await _mediator.Send(command);

			return result.Success ? Ok( result ) : BadRequest( result );
		}
	}
}