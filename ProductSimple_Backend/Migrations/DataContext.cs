using Microsoft.EntityFrameworkCore;
using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Migrations
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var produtoBuilder = modelBuilder.Entity<Produto>();

            produtoBuilder.HasOne(o => o.Categoria)
                          .WithMany(o => o.Produtos)
                          .HasForeignKey(o => o.CategoriaId)
                          .HasPrincipalKey(o => o.Id);

            var categoriaBuilder = modelBuilder.Entity<Categoria>();

            categoriaBuilder.HasMany(o => o.Produtos)
                            .WithOne(o => o.Categoria)
                            .HasForeignKey(o => o.CategoriaId)
                            .HasPrincipalKey(o => o.Id);

            var userBuilder = modelBuilder.Entity<User>();

            userBuilder.HasKey(o => o.Id);
            userBuilder.HasMany(o => o.Roles).WithMany(o => o.Users);

            var roleBuilder = modelBuilder.Entity<Role>();

            roleBuilder.HasKey(o => o.Id);
            roleBuilder.HasMany(o => o.Users).WithMany(o => o.Roles);

            base.OnModelCreating(modelBuilder);
        }
    }
}
