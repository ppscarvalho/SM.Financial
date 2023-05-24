using SM.Financial.Core.Domain.Enuns;
using SM.Resource.Messagens;

namespace SM.Financial.Core.Application.Commands.BillToPay
{
    public class UpdateBillToPayCommand : CommandHandler
    {
        public Guid Id { get; private set; }
        public Guid SuuplierId { get; private set; }
        public string? Description { get; private set; }
        public DateTime DueDate { get; private set; }
        public decimal Amout { get; private set; }
        public EStatus Status { get; private set; }

        public UpdateBillToPayCommand(Guid id, Guid suuplierId, string? description, DateTime dueDate, decimal amout)
        {
            Id = id;
            SuuplierId = suuplierId;
            Description = description;
            DueDate = dueDate;
            Amout = amout;
            Status = EStatus.Unpaid;
        }
    }
}
