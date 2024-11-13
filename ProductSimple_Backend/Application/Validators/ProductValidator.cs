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
			RuleFor( o => o.CategoriaId ).NotNull().WithMessage( "Categoria é um campo obrigatório." );
			RuleFor( o => o.Preco ).GreaterThan(0).WithMessage( "O preço deve ser maior que zero." );
			RuleFor( o => o.DataValidade ).GreaterThan(DateTime.Now.Date).WithMessage( "A data de validade deve ser maior que a data atual!" );
		}
    }
}
