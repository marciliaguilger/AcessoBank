using Bank.Transfer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Transfer.Infrastructure.Mappings
{
    public class TransferenceMap : IEntityTypeConfiguration<Transference>
    {
        public void Configure(EntityTypeBuilder<Transference> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.AccountOrigin)
                .HasColumnType("varchar(20)")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(t => t.AccountDestination)
                .HasColumnType("varchar(20)")
                .HasMaxLength(20)
                .IsRequired();
            
            builder.Property(t => t.Amount)
                .HasColumnType("decimal")
                .IsRequired();
            
            builder.Property(t => t.RequestDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(t => t.TransferStatus)
               .HasColumnType("varchar(200)")
               .HasMaxLength(200)
               .IsRequired();

            builder.Property(t => t.TransferStatusDetail)
               .HasColumnType("varchar(2000)")
               .HasMaxLength(2000);

        }
    }
}
