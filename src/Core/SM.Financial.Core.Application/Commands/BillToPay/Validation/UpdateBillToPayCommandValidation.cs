﻿using FluentValidation;

namespace SM.Financial.Core.Application.Commands.BillToPay.Validation
{
    public class UpdateBillToPayCommandValidation : AbstractValidator<UpdateBillToPayCommand>
    {
        public UpdateBillToPayCommandValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("O id da conta não foi informado.");

            RuleFor(c => c.SupplierId)
                .NotEmpty()
                .WithMessage("O id do fornecedor não foi informado.");

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
