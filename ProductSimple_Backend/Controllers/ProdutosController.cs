using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductSimple_Backend.Data;
using ProductSimple_Backend.Models;
using ProductSimple_Backend.Services.Authorization;

namespace ProductSimple_Backend.Controllers
{
	[Authorize]
	[ApiController]
	[Route( "api/[controller]" )]
	public class ProdutosController :ControllerBase
	{
		private readonly ProductSimpleDbContext _context;

		public ProdutosController( ProductSimpleDbContext context )
		{
			_context = context;
		}

		[HttpGet]
		[AuthorizePermission( "ObterProduto" )]
		public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos( int pageNumber = 1, int pageSize = 10 )
		{
			if( pageSize > 100 ) 
				pageSize = 100;

			var produtos = await _context.Produtos
		                                 .Skip((pageNumber - 1) * pageSize) 
                                         .Take(pageSize)                   
                                         .ToListAsync();

			return Ok( produtos );
		}

		[HttpGet( "{id}" )]
		[AuthorizePermission( "ObterProduto" )]
		public async Task<ActionResult<Produto>> GetProduto( int id )
		{
			var produto = await _context.Produtos.FindAsync(id);

			if( produto == null )
				return NotFound();

			return produto;
		}

		[HttpPost]
		[AuthorizePermission( "CriarProduto" )]
		public async Task<ActionResult<Produto>> PostProduto( Produto produto )
		{
			_context.Produtos.Add( produto );
			
			await _context.SaveChangesAsync();

			return CreatedAtAction( nameof( GetProduto ), new { id = produto.Id }, produto );
		}

		[HttpPut( "{id}" )]
		[AuthorizePermission( "EditarProduto" )]
		public async Task<IActionResult> PutProduto( int id, Produto produto )
		{
			if( id != produto.Id )
				return BadRequest();

			_context.Entry( produto ).State = EntityState.Modified;

			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete( "{id}" )]
		[AuthorizePermission( "ExcluirProduto" )]
		public async Task<IActionResult> DeleteProduto( int id )
		{
			var produto = await _context.Produtos.FindAsync(id);

			if( produto == null )
				return NotFound();

			_context.Produtos.Remove( produto );

			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}