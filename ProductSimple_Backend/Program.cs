using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using ProductSimple_Backend.Application;
using ProductSimple_Backend.Application.Handlers;
using ProductSimple_Backend.Application.Handlers.Auth;
using ProductSimple_Backend.Infrastructure;
using ProductSimple_Backend.Migrations;
using ProductSimple_Backend.Services;
using ProductSimple_Backend.Services.Authorization;

using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Scoped
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthorizationHandler, RolesService>();

//MediatR
//Product
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<CreateProductHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<DeleteProductHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<UpdateProductHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<GetAllProductsHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<GetProductByIdHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<GetProductByNameHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<GetProductByDescriptionHandler>() );
//Category
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<CreateCategoryHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<DeleteCategoryHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<UpdateCategoryHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<GetAllCategoriesHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<GetCategoryByIdHandler>() );
//User
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<CreateUserHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<DeleteUserHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<UpdateUserHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<GetAllUsersHandler>() );
//Login
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<LoginHandler>() );

// database
string connectionString = builder.Configuration.GetConnectionString( "DefaultConnection" ) ?? "";

builder.Services.AddDbContext<DataContext>( o => o.UseMySQL( connectionString ) );

//Authentication
string? secretKey = builder.Configuration["Jwt:SecretKey"];

builder.Services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
	.AddJwtBearer( options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidIssuer = builder.Configuration[ "Jwt:Issuer" ],
			ValidAudience = builder.Configuration[ "Jwt:Audience" ],
			IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( secretKey ) )
		};
		options.Events = new JwtBearerEvents
		{
			OnTokenValidated = context =>
			{
				var claimsIdentity = context.Principal.Identity as ClaimsIdentity;

				if( claimsIdentity != null )
				{
					var emailClaim = claimsIdentity.FindFirst(ClaimTypes.Email);

					if( emailClaim == null )
					{
						claimsIdentity.AddClaim( new Claim( ClaimTypes.Email, "teste@teste.com" ) );
					}
				}

				return Task.CompletedTask;
			}
		};
	} );

var rolesList = DbInitializerService.GetAllRoles();

builder.Services.AddAuthorization( options =>
{
	foreach( var role in rolesList )
	{
		options.AddPolicy( role, policy =>
			policy.Requirements.Add( new RoleRequirement( role ) ) );
	}
} );

// Swagger
var openApiSecuritySchemeDefinition = new OpenApiSecurityScheme
{
	In = ParameterLocation.Header,
	Description = "Authorização JWT. Digite o token.",
	Name = "Authorization",
	Type = SecuritySchemeType.Http,
	BearerFormat = "JWT",
	Scheme = "Bearer"
};

var openApiReference = new OpenApiReference
{
	Type = ReferenceType.SecurityScheme,
	Id = "Bearer"
};

var openApiSecurityScheme = new OpenApiSecurityScheme { Reference = openApiReference };

var openApiSecurityRequirement = new OpenApiSecurityRequirement
{
	{
		openApiSecurityScheme,
		 new string[] {}
	}
};

builder.Services.AddSwaggerGen( o =>
{
	o.AddSecurityDefinition( "Bearer", openApiSecuritySchemeDefinition );
	o.AddSecurityRequirement( openApiSecurityRequirement );
} );

// Others
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Cria diretório para imagens
var customPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

// Garante que a pasta exista
if( !Directory.Exists( customPath ) )
{
	Directory.CreateDirectory( customPath );
}

builder.Environment.WebRootPath = customPath;

var app = builder.Build();

app.UseSwagger();

// Configuração do pipeline
if( app.Environment.IsDevelopment() )
{
	app.UseDeveloperExceptionPage();
	app.UseSwaggerUI();
}
else
{
	app.UseSwaggerUI( o =>
	{
		o.SwaggerEndpoint( "/swagger/v1/swagger.json", "ProductSimple_Backend API V1" );
		o.RoutePrefix = string.Empty;
		o.DisplayRequestDuration();
	} );
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

using( var serviceScope = app.Services.CreateScope() )
{
	new DbInitializerService( serviceScope );
}

app.Run();
