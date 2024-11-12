using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using ProductSimple_Backend.Application;
using ProductSimple_Backend.Application.Handlers;
using ProductSimple_Backend.Data;
using ProductSimple_Backend.Infrastructure;
using ProductSimple_Backend.Services.Authorization;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Scoped
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//MediatR
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<CreateProductHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<DeleteProductHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<UpdateProductHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<GetAllProductsHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<GetProductByIdHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<GetProductByNameHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<GetProductByDescriptionHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<CreateCategoryHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<DeleteCategoryHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<UpdateCategoryHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<GetAllCategoriesHandler>() );
builder.Services.AddMediatR( o => o.RegisterServicesFromAssemblyContaining<GetCategoryByIdHandler>() );

// database
string connectionString = builder.Configuration.GetConnectionString( "DefaultConnection" ) ?? "";

builder.Services.AddDbContext<ProductSimpleDbContext>( o => o.UseMySQL( connectionString ) );

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
	} );

builder.Services.AddAuthorization( options =>
{
	options.AddPolicy( "PermissionPolicy", policy =>
		policy.Requirements.Add( new AuthorizePermissionAttribute( "" ) ) );
} );

// Swagger
var openApiSecuritySchemeDefinition = new OpenApiSecurityScheme
{
	In = ParameterLocation.Header,
	Description = "Authoriza��o JWT. Digite o token.",
	Name = "ApiKey",
	Type = SecuritySchemeType.ApiKey,
	BearerFormat = "JWT",
	Scheme = "bearer"
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

var app = builder.Build();

// Configura��o do pipeline
if( app.Environment.IsDevelopment() )
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();  
	app.UseSwaggerUI(); 
}


app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();