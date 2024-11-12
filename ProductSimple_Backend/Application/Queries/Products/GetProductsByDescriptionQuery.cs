using MediatR;

using ProductSimple_Backend.Domain;
using ProductSimple_Backend.Domain.Dto;

namespace ProductSimple_Backend.Application
{
    public record GetProductsByDescriptionQuery( string Description, int PageNumber, int PageSize ) :IRequest<PaginatedResultDto<Produto>>;
}
