using Microsoft.EntityFrameworkCore;

using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Data
{
	public class ProductSimpleDbContext :DbContext
	{
		public ProductSimpleDbContext( DbContextOptions<DbContext> options ) : base( options ) { }

		public DbSet<Produto> Produtos { get; set; }
		public DbSet<Categoria> Categorias { get; set; }

		public ProductSimpleDbContext()
        {
            
        }
        
		public ProductSimpleDbContext( DbContextOptions<ProductSimpleDbContext> options ): base( options )
		{
		}

		protected override void OnModelCreating( ModelBuilder modelBuilder )
		{
			var produtoBuilder = modelBuilder.Entity<Produto>();

			produtoBuilder.HasOne( o => o.Categoria )
						  .WithMany( o => o.Produtos )
						  .HasForeignKey( o => o.CategoriaId )
						  .HasPrincipalKey( o => o.Id );

			var categoriaBuilder = modelBuilder.Entity<Categoria>();

			categoriaBuilder.HasMany( o => o.Produtos )
							.WithOne( o => o.Categoria )
							.HasForeignKey( o => o.CategoriaId )
							.HasPrincipalKey( o => o.Id );

			base.OnModelCreating( modelBuilder );
		}
	}
}
