using GymManagement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Configuration
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(x => x.PlanName)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(x => x.Price)
                .HasPrecision(10, 2);
            builder.Property(x => x.Description)
                .HasColumnType("varchar")
              .HasMaxLength(200);

            builder.Property(x => x.CreateAt)
                .HasDefaultValueSql("GETDATE()");

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("PlanDurationCheck", "DurationDays Between 1 and 365");
            });
           
        }
    }
}