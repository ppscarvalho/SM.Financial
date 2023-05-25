using SM.Financial.Core.Domain.Enuns;
using SM.Resource.Domain;
using SM.Resource.Interfaces;

namespace SM.Financial.Core.Domain.Entities
{
    public class BillToPay : Entity, IAggregateRoot
    {
        public Guid SupplierId { get; private set; }
        public string? Description { get; private set; }
        public DateTime DueDate { get; private set; }
        public decimal Amout { get; private set; }
        public EStatus Status { get; private set; }

        public BillToPay() { }

        public BillToPay(Guid suuplierId, string? description, DateTime dueDate, decimal amout) : this()
        {
            SupplierId = suuplierId;
            Description = description;
            DueDate = dueDate;
            Amout = amout;
            Status = EStatus.Unpaid;
        }

        public void ChangeStatusToPay()
        {
            Status = EStatus.PaidOut;
        }
    }
}
