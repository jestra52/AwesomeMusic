namespace AwesomeMusic.Services.Validators.CommandValidators.UserValidators
{
    using AwesomeMusic.Data.Commands.UserCommands;
    using AwesomeMusic.Services.Shared;
    using FluentValidation;

    public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("Email cannot be empty");

            RuleFor(c => c.Email)
                .Matches(RegularExpressions.IsEmailAddressValid)
                .WithMessage("Email must be a valid email address");

            RuleFor(c => c.Password)
                .NotEmpty()
                .WithMessage("Password cannot be empty");

            RuleFor(c => c.Password)
                .Matches(RegularExpressions.HasLowerChar)
                .WithMessage("Password must include at least one lower letter");

            RuleFor(c => c.Password)
                .Matches(RegularExpressions.HasUpperChar)
                .WithMessage("Password must include at least one upper letter");

            RuleFor(c => c.Password)
                .Matches(RegularExpressions.HasSpecialChar)
                .WithMessage("Password must include at least one of the following characters: !, @, #, ?, ]");

            RuleFor(c => c.Password)
                .Matches(RegularExpressions.HasMinimum10Chars)
                .WithMessage("Password must be at least 10 letters long");
        }
    }
}
