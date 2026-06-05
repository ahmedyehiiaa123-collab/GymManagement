using GymManagement.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Data.Controllers
{
    public class MemberConfiguration : GymUserController<Member>, IEntityTypeConfiguration<Member>
    { 

        public new  void Configure(EntityTypeBuilder<Member> builder)
        {


            builder.Property(x => x.CreateAt)
                .HasColumnName("JoinDate")
                .HasDefaultValueSql("GETDATE()");
            base.Configure(builder);    
        }

}
}
