using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Financial.Core.Domain.Entities;
using SM.Financial.Core.Domain.Enuns;

namespace SM.Financial.Infrastructure.Mappings
{
    public class FinancialMapping : IEntityTypeConfiguration<BillToPay>
    {
        public void Configure(EntityTypeBuilder<BillToPay> builder)
        {
            builder.ToTable("BillToPay");

            builder.Property(c => c.Id)
                .IsRequired();

            builder.Property(c => c.SupplierId)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(c => c.DueDate)
                .IsRequired();

            builder.Property(c => c.Amout)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.Status)
                .HasConversion(
                    to => to.ToString(),
                    from => (EStatus)Enum.Parse(typeof(EStatus), from))
                .IsRequired();
        }
    }
}
