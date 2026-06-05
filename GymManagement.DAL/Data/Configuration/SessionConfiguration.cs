using GymManagement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Data.Controllers
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>

    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(tb =>
            {

                tb.HasCheckConstraint("SessionCapacityCheck", "Capacity Between 1 and 25");
                tb.HasCheckConstraint("SessionEndDateCheck", "EndDate > StartDate");

            }



        }
    }
}
