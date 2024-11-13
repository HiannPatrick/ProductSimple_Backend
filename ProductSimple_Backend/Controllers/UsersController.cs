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
	public class UsersController :ControllerBase
	{
		private readonly IMediator _mediator;

		public UsersController( IMediator mediator )
		{

			_mediator = mediator;
		}

		[HttpGet( "all" )]
		[Authorize( Policy = "GetUser" )]
		public async Task<IActionResult> GetAll(  )
		{
			var query = new GetAllUsersQuery();

			var result = await _mediator.Send(query);

			if( result == null )
				return NotFound( ReturnCommon.FailureMessage( "Usuarios não encontrados." ) );

			return Ok( result );
		}

		[HttpPost]
		[Authorize( Policy = "CreateUser" )]
		public async Task<IActionResult> Create( [FromBody] CreateUserCommand command )
		{
			var result = await _mediator.Send(command);

			return result.Success ? Ok( result ) : BadRequest( result );
		}

		[HttpPut( "{id}" )]
		[Authorize( Policy = "EditUser" )]
		public async Task<IActionResult> Edit( [FromBody] UpdateUserCommand command )
		{
			var result = await _mediator.Send(command);

			return result.Success ? Ok( result ) : BadRequest( result );
		}

		[HttpDelete( "{id}" )]
		[Authorize( Policy = "DeleteUser" )]
		public async Task<IActionResult> Delete( int id )
		{
			var command = new DeleteUserCommand(id);

			var result = await _mediator.Send(command);

			return result.Success ? Ok( result ) : BadRequest( result );
		}
	}

}
