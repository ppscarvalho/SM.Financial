using SM.Financial.Core.Application.Commands.AccountReceivable.Validation;
using SM.Financial.Core.Domain.Enuns;
using SM.Resource.Messagens;

namespace SM.Financial.Core.Application.Commands.AccountReceivable
{
    public class AddAccountReceivableCommand : CommandHandler
    {
        public Guid CustomerId { get; private set; }
        public string? Description { get; private set; }
        public DateTime DueDate { get; private set; }
        public decimal Value { get; private set; }
        public EStatus Status { get; private set; }

        public AddAccountReceivableCommand(Guid customerId, string? description, DateTime dueDate, decimal value)
        {
            CustomerId = customerId;
            Description = description;
            DueDate = dueDate;
            Value = value;
            Status = EStatus.ToReceive;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddAccountReceivableCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
