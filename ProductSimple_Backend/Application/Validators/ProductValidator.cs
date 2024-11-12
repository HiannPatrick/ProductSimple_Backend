using FluentValidation;

using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Application
{
	public class ProductValidator :AbstractValidator<Produto>
	{
        public ProductValidator()
        {
			RuleFor( o => o.Nome ).NotEmpty().WithMessage( "Nome é um campo obrigatório." );
			RuleFor( o => o.Nome ).MaximumLength( 50 ).WithMessage( "O nome deve ter no máximo 50 caracteres!" );
			RuleFor( o => o.Descricao ).NotEmpty().WithMessage( "Descrição é um campo obrigatório." );
			RuleFor( o => o.Descricao ).MaximumLength( 200 ).WithMessage( "A descricao deve ter no máximo 200 caracteres!" );
			RuleFor( o => o.Categoria ).NotEmpty().WithMessage( "Categoria é um campo obrigatório." );
			RuleFor( o => o.Preco ).GreaterThan(0).WithMessage( "Categoria é um campo obrigatório." );
		}
    }
}
