using Microsoft.EntityFrameworkCore;

using ProductSimple_Backend.Data;
using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Infrastructure
{
	public class CategoryRepository :ICategoryRepository
	{
		private readonly ProductSimpleDbContext _context;

		public CategoryRepository( ProductSimpleDbContext context )
        {
			_context = context;
        }
        public async Task<bool> Create( Categoria category )
		{
			_context.Categorias.Add( category );

			var result = await _context.SaveChangesAsync();

			return result > 0;
		}
		public async Task<bool> Update( Categoria category )
		{
			_context.Entry( category ).State = EntityState.Modified;

			var result = await _context.SaveChangesAsync();

			return result > 0;
		}
		public async Task<bool> Delete( int id )
		{
			Categoria? categoria = await _context.Categorias.FindAsync( id );

			if( categoria == null )
				return false;

			bool productWithCategory = await _context.Produtos
													 .AnyAsync(p => p.CategoriaId == id);

			if( productWithCategory )
				return false;

			_context.Categorias.Remove( categoria );

			var result = await _context.SaveChangesAsync();

			return result > 0;
		}

		public async Task<List<Categoria>> GetAllCategories()
		{
			return await _context.Categorias.ToListAsync();
		}

		public async Task<Categoria?> GetCategoryById( int id )
		{
			return await _context.Categorias.FindAsync(id);
		}
	}
}
