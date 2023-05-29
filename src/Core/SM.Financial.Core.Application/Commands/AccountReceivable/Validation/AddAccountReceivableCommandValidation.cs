using FluentValidation;

namespace SM.Financial.Core.Application.Commands.AccountReceivable.Validation
{
    public class AddAccountReceivableCommandValidation : AbstractValidator<AddAccountReceivableCommand>
    {
        public AddAccountReceivableCommandValidation()
        {

            RuleFor(c => c.CustomerId)
                .NotEmpty()
                .WithMessage("O id do cliente não foi informado.");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("A descrição da conta não foi informada.");

            RuleFor(c => c.DueDate)
                .NotEmpty()
                .WithMessage("A data de vencimento da conta não foi informada.");

            RuleFor(c => c.Value)
                .NotEmpty()
                .WithMessage("o valor da conta não foi informado.");
        }
    }
}

