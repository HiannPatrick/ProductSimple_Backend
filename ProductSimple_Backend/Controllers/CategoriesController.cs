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
	public class CategoriesController :ControllerBase
	{
		private readonly IMediator _mediator;

		public CategoriesController( IMediator mediator )
		{

			_mediator = mediator;
		}

		[HttpGet( "all" )]
		[AuthorizePermission( "GetCategory" )]
		public async Task<IActionResult> GetAll(  )
		{
			var query = new GetAllCategoriesQuery();

			var result = await _mediator.Send(query);

			if( result == null )
				return NotFound( ReturnCommon.FailureMessage( "Categorias não localizadas." ) );

			return Ok( result );
		}

		[HttpGet( "{id}" )]
		[AuthorizePermission( "GetCategory" )]
		public async Task<IActionResult> Get( int id )
		{
			var query = new GetCategoryByIdQuery( id );

			var result = await _mediator.Send(query);

			if( result == null )
				return NotFound( ReturnCommon.FailureMessage( "Categoria não localizada." ) );

			return Ok( result );
		}

		[HttpPost]
		[AuthorizePermission( "CreateCategory" )]
		public async Task<IActionResult> Create( [FromBody] CreateCategoryCommand command )
		{
			var result = await _mediator.Send(command);

			return result.Success ? Ok( result ) : BadRequest( result );
		}

		[HttpPut( "{id}" )]
		[AuthorizePermission( "EditCategory" )]
		public async Task<IActionResult> Edit( [FromBody] UpdateCategoryCommand command )
		{
			var result = await _mediator.Send(command);

			return result.Success ? Ok( result ) : BadRequest( result );
		}

		[HttpDelete( "{id}" )]
		[AuthorizePermission( "DeleteCategory" )]
		public async Task<IActionResult> Delete( int id )
		{
			var command = new DeleteCategoryCommand(id);

			var result = await _mediator.Send(command);

			return result.Success ? Ok( result ) : BadRequest( result );
		}
	}

}
