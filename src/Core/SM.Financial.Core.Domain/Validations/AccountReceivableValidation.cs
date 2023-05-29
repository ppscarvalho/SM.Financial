using FluentValidation;
using SM.Financial.Core.Domain.Entities;

namespace SM.Financial.Core.Domain.Validations
{
    public class AccountReceivableValidation : AbstractValidator<AccountReceivable>
    {
        public AccountReceivableValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("O id da conta não foi informado.");

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
