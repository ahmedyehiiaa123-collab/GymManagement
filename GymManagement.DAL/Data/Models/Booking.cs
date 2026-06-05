using GymManagement.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Controllers
{
    public class Booking : BaseEntity
    {
        public Member Member { get; set; } = default!;
        public Session Session { get; set; } = default!;
        public int MemberId { get; set; }
        public int SessionId { get; set; }


    }
}
