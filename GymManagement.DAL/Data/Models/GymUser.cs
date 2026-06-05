using GymManagement.DAL.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Data.Models
{
    public class GymUser : BaseEntity

    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public DateOnly DateofBirth { get; set; }

        public Address Address { get; set; }  
        public Gender Gender { get; set; }
    }

    [Owned]
    public class Address {

        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public int BuildingNumber { get; set; };



    }
}
