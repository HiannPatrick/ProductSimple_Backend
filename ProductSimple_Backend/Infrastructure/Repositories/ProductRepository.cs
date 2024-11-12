using Microsoft.EntityFrameworkCore;

using ProductSimple_Backend.Application;
using ProductSimple_Backend.Data;
using ProductSimple_Backend.Domain;

using System.Xml.Linq;

namespace ProductSimple_Backend.Infrastructure
{
	public class ProductRepository : IProductRepository
	{
		private readonly ProductSimpleDbContext _context;

		public ProductRepository( ProductSimpleDbContext context )
		{
			_context = context;
		}
		public async Task<bool> Create( Produto Product )
		{
			_context.Produtos.Add( Product );

			var result = await _context.SaveChangesAsync();

			return result > 0;
		}
		public async Task<bool> Update( Produto Product )
		{
			_context.Entry( Product ).State = EntityState.Modified;

			var result = await _context.SaveChangesAsync();

			return result > 0;
		}
		public async Task<bool> Delete( int id )
		{
			Produto? produto = await _context.Produtos.FindAsync( id );

			if( produto == null )
				return false;

			_context.Produtos.Remove( produto );

			var result = await _context.SaveChangesAsync();

			return result > 0;
		}

		public async Task<Produto?> GetProductById( int id )
		{
			return await _context.Produtos.FindAsync( id );
		}

		public async Task<PaginatedResult<Produto>> GetAllProducts( int pageNumber, int pageSize )
		{
			int total = _context.Produtos.Count();

			var produtos = await _context.Produtos
								        .Skip( ( pageNumber - 1 ) * pageSize )
								        .Take( pageSize )
								        .ToListAsync();

			var paginatedResult = new PaginatedResult<Produto>( produtos, total, pageNumber, pageSize );

			return paginatedResult;
		}

		public async Task<PaginatedResult<Produto>> GetProductsByName( string name, int pageNumber, int pageSize )
		{
			var produtos = _context.Produtos.Where( o => o.Nome.Equals( name ) );

			int total = produtos.Count();

			var produtosPaginated = await produtos.Skip( ( pageNumber - 1 ) * pageSize )
								                  .Take( pageSize )
								                  .ToListAsync();

			var paginatedResult = new PaginatedResult<Produto>( produtosPaginated, total, pageNumber, pageSize );

			return paginatedResult;
		}

		public async Task<PaginatedResult<Produto>> GetProductsByDescription( string description, int pageNumber, int pageSize )
		{
			var produtos = _context.Produtos.Where( o => o.Descricao.Equals( description ) );

			int total = produtos.Count();

			var produtosPaginated = await produtos.Skip( ( pageNumber - 1 ) * pageSize )
												  .Take( pageSize )
												  .ToListAsync();

			var paginatedResult = new PaginatedResult<Produto>( produtosPaginated, total, pageNumber, pageSize );

			return paginatedResult;
		}
	}
}
