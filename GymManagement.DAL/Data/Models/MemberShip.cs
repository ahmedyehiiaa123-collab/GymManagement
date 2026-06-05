using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GymManagement.DAL.Data.Models
{//Many
    public class MemberShip : BaseEntity
    {
        public DateTime EndDate { get; set; }


        #region Relationships
        public Member Member { get; set; } = default!;

        public int MemberId     { get; set; }
        public Plan Plan { get; set; } = default!;
        public int PlanId { get; set; }


        public string Status => EndDate > DateTime.Now ? "Active" : "Expired";
        public bool IsActive => EndDate > DateTime.Now; 


        #endregion
    }
}
