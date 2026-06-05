using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Data.Models
{
    public class BaseEntity
    {
        public int id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
