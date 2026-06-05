using GymManagement.DAL.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Data.Models
{
    public class Trainer : GymUser
    {
        //HireDate =Create ar in base
        public Specialties Specialties { get; set; }


        public ICollection<Session> Sessions { get; set; } = default!;

    }
}
