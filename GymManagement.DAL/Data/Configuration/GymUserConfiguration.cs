using GymManagement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Data.Controllers
{
    public class GymUserController<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Name)
                 .HasColumnType("varchar")
                 .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            builder.HasIndex(x => x.Phone).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("EmailCheck", "Email like '_%@_%._$' ");

            });

            builder.OwnsOne(x => x.Address, address =>
            {
                address.Property(x => x.Street)
                    .HasColumnName("Street")
                    .HasColumnType("varchar")
                    .HasMaxLength(30);
                address.Property(x => x.City)
               .HasColumnName("City")

                .HasColumnType("varchar")
                .HasMaxLength(30);

            }

            );

        }
    }
}