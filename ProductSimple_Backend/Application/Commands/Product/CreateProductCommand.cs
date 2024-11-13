using MediatR;

namespace ProductSimple_Backend.Application
{
	public record CreateProductCommand( string Nome, string Descricao, double Preco, DateTime? DataValidade, IFormFile Imagem, int CategoriaId ) :IRequest<ReturnCommon>;
}
