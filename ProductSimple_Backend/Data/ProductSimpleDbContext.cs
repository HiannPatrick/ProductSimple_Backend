using Microsoft.EntityFrameworkCore;

using ProductSimple_Backend.Models;

namespace ProductSimple_Backend.Data
{
	public class ProductSimpleDbContext :DbContext
	{
		public ProductSimpleDbContext( DbContextOptions<DbContext> options ) : base( options ) { }

		public DbSet<Produto> Produtos { get; set; }
		public DbSet<Categoria> Categorias { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
		public DbSet<RolePermission> RolePermissions { get; set; }

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

			var rolePermissionBuilder = modelBuilder.Entity<RolePermission>();
			
			rolePermissionBuilder.HasKey( rp => new { rp.RoleId, rp.PermissionId } );

			var userRoleBuilder = modelBuilder.Entity<UserRole>();
			
			userRoleBuilder.HasKey( ur => new { ur.UserId, ur.RoleId } );

			base.OnModelCreating( modelBuilder );
		}
	}
}
