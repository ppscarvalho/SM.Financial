using SM.Financial.Core.Application.Commands.BillToPay.Validation;
using SM.Financial.Core.Domain.Enuns;
using SM.Resource.Messagens;

namespace SM.Financial.Core.Application.Commands.BillToPay
{
    public class UpdateBillToPayCommand : CommandHandler
    {
        public Guid Id { get; private set; }
        public Guid SupplierId { get; private set; }
        public string? Description { get; private set; }
        public DateTime DueDate { get; private set; }
        public decimal Amout { get; private set; }
        public EStatus Status { get; private set; }

        public UpdateBillToPayCommand(Guid id, Guid supplierId, string? description, DateTime dueDate, decimal amout, EStatus status)
        {
            Id = id;
            SupplierId = supplierId;
            Description = description;
            DueDate = dueDate;
            Amout = amout;
            Status = status;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateBillToPayCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
