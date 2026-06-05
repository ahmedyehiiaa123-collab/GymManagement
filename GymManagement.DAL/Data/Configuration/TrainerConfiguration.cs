using GymManagement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Data.Controllers
{
    public class TrainerConfiguration : GymUserController<Trainer>, IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {

            builder.Property(x => x.CreateAt)
              .HasColumnName("HireDate")
                .HasDefaultValueSql("GETDATE()");

            base.Configure(builder);
    }
    }
}
