using GymManagement.DAL.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Data.Models
{
    public class Session : BaseEntity
    {
        public string Description { get; set; } = default!;
        public int Capacity { get; set; }    
        public DateTime StartDate   { get; set; }
        public DateTime EndDate { get; set; }


        #region Relationships
        public Trainer Trainer { get; set; } = default!;

        public int TrainerId    { get; set; }
        public Category Category { get; set; } = default!;
        public int CategoryId { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        //bookingDate => base Entitiy Created At
        public bool IsAttended { get; set; }
        #endregion
    }

}
