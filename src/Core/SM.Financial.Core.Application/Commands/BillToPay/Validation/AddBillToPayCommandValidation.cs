using FluentValidation;

namespace SM.Financial.Core.Application.Commands.BillToPay.Validation
{
    public class AddBillToPayCommandValidation : AbstractValidator<AddBillToPayCommand>
    {
        public AddBillToPayCommandValidation()
        {
            RuleFor(c => c.SuuplierId)
                .NotEmpty()
                .WithMessage("O id do fornecedor não foi informado.");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("A descrição da conta não foi informada.");

            RuleFor(c => c.DueDate)
                .NotEmpty()
                .WithMessage("A data de vencimento da conta não foi informada.");

            RuleFor(c => c.Amout)
                .NotEmpty()
                .WithMessage("o valor da conta não foi informado.");
        }
    }
}
