using SM.Financial.Core.Domain.Enuns;
using SM.Resource.Domain;
using SM.Resource.Interfaces;

namespace SM.Financial.Core.Domain.Entities
{
    public class AccountReceivable : Entity, IAggregateRoot
    {
        public Guid CustomerId { get; private set; }
        public string? Description { get; private set; }
        public DateTime DueDate { get; private set; }
        public decimal Value { get; private set; }
        public EStatus Status { get; private set; }

        public AccountReceivable() { }

        public AccountReceivable(Guid customerId, string? description, DateTime dueDate, decimal value) : this()
        {
            CustomerId = customerId;
            Description = description;
            DueDate = dueDate;
            Value = value;
            Status = EStatus.ToReceive;
        }

        public void ChangeStatusToPay()
        {
            Status = EStatus.PaidOut;
        }
    }
}
