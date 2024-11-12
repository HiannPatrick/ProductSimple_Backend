using FluentValidation;

using ProductSimple_Backend.Domain;

namespace ProductSimple_Backend.Application
{
	public class UserValidator :AbstractValidator<User>
	{
        public UserValidator()
        {
			RuleFor( o => o.Email ).NotEmpty().WithMessage( "E-mail é um campo obrigatório." );
			RuleFor( o => o.Email ).EmailAddress().WithMessage( "E-mail inválido." );
			RuleFor( o => o.PasswordHash ).NotEmpty().WithMessage( "Senha é um campo obrigatório." );
			RuleFor( o => o.Permissions ).NotEmpty().WithMessage( "Permissões é um campo obrigatório." );
		}
    }
}
