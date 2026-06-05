using GymManagement.DAL.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Data.Controllers
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Ignore(x=>x.id);
            //explicit defult

            builder.HasKey(x => new { x.SessionId, x.MemberId });
            builder.Property(x=>x.CreateAt)
                .HasColumnName("BookingDate")
                .HasDefaultValueSql("GETDATE()")


        }
    }
}
