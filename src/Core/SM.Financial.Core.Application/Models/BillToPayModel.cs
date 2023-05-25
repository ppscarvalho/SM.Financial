using SM.Financial.Core.Domain.Enuns;

namespace SM.Financial.Core.Application.Models
{
    public class BillToPayModel
    {
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amout { get; set; }
        public EStatus Status { get; set; }
    }
}
