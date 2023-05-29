using SM.Financial.Core.Application.Commands.BillToPay.Validation;
using SM.Resource.Messagens;

namespace SM.Financial.Core.Application.Commands.BillToPay
{
    public class AddBillToPayCommand : CommandHandler
    {
        public Guid SupplierId { get; private set; }
        public string? Description { get; private set; }
        public DateTime DueDate { get; private set; }
        public decimal Value { get; private set; }

        public AddBillToPayCommand()
        {

        }
        public AddBillToPayCommand(Guid suuplierId, string? description, DateTime dueDate, decimal value)
        {
            SupplierId = suuplierId;
            Description = description;
            DueDate = dueDate;
            Value = value;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddBillToPayCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
