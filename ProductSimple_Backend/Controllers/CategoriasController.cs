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
	public class CategoriaController :ControllerBase
	{
		private readonly ProductSimpleDbContext _context;

		public CategoriaController( ProductSimpleDbContext context )
		{
			_context = context;
		}

		[HttpGet]
		[AuthorizePermission( "ObterCategoria" )]
		public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
		{
			return await _context.Categorias.ToListAsync();
		}

		[HttpGet( "{id}" )]
		[AuthorizePermission( "ObterCategoria" )]
		public async Task<ActionResult<Categoria>> GetCategoria( int id )
		{
			var categoria = await _context.Categorias.FindAsync(id);

			if( categoria == null )
				return NotFound();

			return categoria;
		}

		[HttpPost]
		[AuthorizePermission( "CriarCategoria" )]
		public async Task<ActionResult<Categoria>> PostCategoria( Categoria categoria )
		{
			_context.Categorias.Add( categoria );

			await _context.SaveChangesAsync();

			return CreatedAtAction( nameof( GetCategoria ), new { id = categoria.Id }, categoria );
		}

		[HttpPut( "{id}" )]
		[AuthorizePermission( "EditarCategoria" )]
		public async Task<IActionResult> PutCategoria( int id, Categoria categoria )
		{
			if( id != categoria.Id )
				return BadRequest();

			_context.Entry( categoria ).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch( DbUpdateConcurrencyException )
			{
				if( !CategoriaExists( id ) )
					return NotFound();

				throw;
			}

			return Ok( "Categoria atualizada com sucesso!" );
		}

		[HttpDelete( "{id}" )]
		[AuthorizePermission( "ExcluirCategoria" )]
		public async Task<IActionResult> DeleteCategoria( int id )
		{
			Categoria? categoria = await _context.Categorias.FindAsync(id);

			if( categoria == null )
				return NotFound();

			bool produtosUsandoCategoria = await _context.Produtos
														.AnyAsync(p => p.CategoriaId == id);

			if( produtosUsandoCategoria )
				return BadRequest( "A categoria não pode ser excluída porque está associada a um ou mais produtos." );

			_context.Categorias.Remove( categoria );

			await _context.SaveChangesAsync();

			return Ok( "Categoria excluída com sucesso!" );
		}

		private bool CategoriaExists( int id )
		{
			return _context.Categorias.Any( e => e.Id == id );
		}
	}

}
