using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagement.BLL.ViewModels
{
    public class UpdateSesssionViewModel
    {
        [Required(ErrorMessage = "Description is Required")]
        //string min length 10 ,500 
        [StringLength(500, MinimumLength = 10, ErrorMessage = "String Length between 10 and 500")]

        public string? Description { get; set; }
        [Required(ErrorMessage ="Star Date Is Required")]

        [Display(Name = "Start Date & Time")]

        public DateTime StartDate { get; set; }
        [Required(ErrorMessage ="End Date Is Required")]

        [Display(Name = "End Date & Time")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Trainer is Required")]
        [Display(Name = "Trainer")]
        public int TrainerId { get; set; }
        public int CategoryId { get; set; }
    }
}
