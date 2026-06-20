using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.BLL.ViewModels
{
    public class HealthRecordViewModel
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string? Note { get; set; }
        public string ?BloodType { get; set; }

    }
}
