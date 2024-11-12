using FluentValidation;

using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Application
{
	public class CategoryValidator :AbstractValidator<Categoria>
	{
        public CategoryValidator()
        {
			RuleFor( o => o.Nome ).NotEmpty().WithMessage( "Nome é um campo obrigatório." );
			RuleFor( o => o.Nome ).MaximumLength( 50 ).WithMessage( "O nome deve ter no máximo 50 caracteres!" );
		}
    }
}
