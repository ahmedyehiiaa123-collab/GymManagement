using GymManagement.DAL.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Data.Models
{
    public class Member : GymUser
    {
        public  string? photo { get; set; }
        //joinDate=Cretatedat of BaseEntity


        #region RelationShips
        public HealthRecord HealthRecord { get; set; } = default!;
        public ICollection<MemberShip> MemberShips { get; set; } = default!;
        public ICollection<Booking> Bookings { get; set; } = default!;
       

        #endregion

    }
}
