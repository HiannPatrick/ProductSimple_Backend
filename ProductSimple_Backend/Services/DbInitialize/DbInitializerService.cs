using Microsoft.EntityFrameworkCore;
using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Migrations;
using System.Data;

namespace ProductSimple_Backend.Services
{
    public class DbInitializerService : IDbInitializerService
    {
		private readonly IServiceScope _serviceScope;

		public DbInitializerService( IServiceScope serviceScope )
		{
			_serviceScope = serviceScope;

			this.Migrate();
		}

		public void Migrate()
		{
			using( var context = _serviceScope.ServiceProvider.GetService<DataContext>() )
			{
				if( context == null )
					return;

				context.Database.Migrate();

				var listAdminRoles = this.getAdminRoles();

				foreach( Role role in listAdminRoles )
				{
					if( context.Roles.FirstOrDefault( o => o.Name.Equals( role.Name ) ) != null )
							continue;

					context.Roles.Add( role );
				}

				var listUserRoles = this.getUserRoles();

				foreach( Role role in listUserRoles )
				{
					if( context.Roles.FirstOrDefault( o => o.Name.Equals( role.Name ) ) != null )
						continue;

					context.Roles.Add( role );
				}

				context.SaveChanges();

				string email = "admin@admin.com";

				if( context.Users.FirstOrDefault( o => o.Email.Equals( email ) ) == null )
				{
					string password = BCrypt.Net.BCrypt.HashPassword( "admin" );

					var rolesList = context.Roles.ToList();

					context.Users.Add( new User { Email = email, PasswordHash = password, Roles = rolesList } );

					context.SaveChanges();
				}

				email = "user@user.com";

				if( context.Users.FirstOrDefault( o => o.Email.Equals( email ) ) == null )
				{
					string password = BCrypt.Net.BCrypt.HashPassword( "user" );

					var rolesList = context.Roles.Where( o => o.Name.StartsWith("Get") ).ToList();

					context.Users.Add( new User { Email = email, PasswordHash = password, Roles = rolesList } );

					context.SaveChanges();
				}
			}
		}

		private List<Role> getAdminRoles()
		{
			var list = new List<Role>
			{
				new Role { Name = "GetProduct" },
				new Role { Name = "CreateProduct" },
				new Role { Name = "EditProduct" },
				new Role { Name = "DeleteProduct" },
				new Role { Name = "GetCategory" },
				new Role { Name = "CreateCategory" },
				new Role { Name = "EditCategory" },
				new Role { Name = "DeleteCategory" },
				new Role { Name = "GetUser" },
				new Role { Name = "CreateUser" },
				new Role { Name = "EditUser" },
				new Role { Name = "DeleteUser" }
			};

			return list;
		}

		private List<Role> getUserRoles()
		{
			var list = new List<Role>
			{
				new Role { Name = "GetProduct" },
				new Role { Name = "GetCategory" },
				new Role { Name = "GetUser" }
			};

			return list;
		}
	}
}
