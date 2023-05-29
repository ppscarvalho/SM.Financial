using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Financial.Core.Domain.Entities;
using SM.Financial.Core.Domain.Enuns;

namespace SM.Financial.Infrastructure.Mappings
{
    public class AccountReceivableMapping : IEntityTypeConfiguration<AccountReceivable>
    {
        public void Configure(EntityTypeBuilder<AccountReceivable> builder)
        {
            builder.ToTable("AccountReceivable");

            builder.Property(c => c.Id)
                .IsRequired();

            builder.Property(c => c.CustomerId)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(c => c.DueDate)
                .IsRequired();

            builder.Property(c => c.Value)
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
