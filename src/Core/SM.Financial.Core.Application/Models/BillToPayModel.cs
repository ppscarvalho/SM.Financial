﻿namespace SM.Financial.Core.Application.Models
{
    public class BillToPayModel
    {
        public Guid? Id { get; set; }
        public Guid SupplierId { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Value { get; set; }
        public string? Status { get; set; }
    }
}
