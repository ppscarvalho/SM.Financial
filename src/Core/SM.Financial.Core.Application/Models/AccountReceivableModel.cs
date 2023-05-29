namespace SM.Financial.Core.Application.Models
{
    public class AccountReceivableModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Value { get; set; }
        public string? Status { get; set; }
    }
}
