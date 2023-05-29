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
        public decimal Value { get; private set; }
        public EStatus Status { get; private set; }

        public BillToPay() { }

        public BillToPay(Guid suuplierId, string? description, DateTime dueDate, decimal value) : this()
        {
            SupplierId = suuplierId;
            Description = description;
            DueDate = dueDate;
            Value = value;
            Status = EStatus.Unpaid;
        }

        public BillToPay(Guid id, Guid suuplierId, string? description, DateTime dueDate, decimal value, EStatus status) : this()
        {
            Id = id;
            SupplierId = suuplierId;
            Description = description;
            DueDate = dueDate;
            Value = value;
            Status = status;
        }

        public void ChangeStatusToPay()
        {
            Status = EStatus.PaidOut;
        }
    }
}
