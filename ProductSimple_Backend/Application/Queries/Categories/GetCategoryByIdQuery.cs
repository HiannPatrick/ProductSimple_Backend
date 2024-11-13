using MediatR;

using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Application
{
	public record GetCategoryByIdQuery( int Id ) :IRequest<CategoriaDto>;
}
