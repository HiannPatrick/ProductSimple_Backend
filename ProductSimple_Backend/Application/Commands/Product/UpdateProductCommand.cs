using MediatR;

namespace ProductSimple_Backend.Application
{
	public record UpdateProductCommand( int Id, string Nome, string Descricao, double Preco, DateTime? DataValidade, IFormFile Imagem, int CategoriaId ) :IRequest<ReturnCommon>;
}
