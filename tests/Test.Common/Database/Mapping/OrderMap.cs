using System.Data.Entity.ModelConfiguration;
using Test.Common.Entites;

namespace Test.Common.Database.Mapping
{
    internal class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            HasKey(t => t.Id);

            Property(t => t.OrderNo)
                .HasColumnName("OrderNo")
                .IsRequired()
                .HasMaxLength(20);
            Property(t => t.OrderAmount)
                .HasColumnName("OrderAmount")
                .IsRequired();
            Property(t => t.ProductNo)
                .HasColumnName("ProductNo")
                .IsRequired()
                .HasMaxLength(20);
            Property(t => t.UserNo)
                .HasColumnName("UserNo")
                .IsRequired()
                .HasMaxLength(20);
            Property(t => t.IsPaid)
                .HasColumnName("IsPaid")
                .IsRequired();
        }
    }
}