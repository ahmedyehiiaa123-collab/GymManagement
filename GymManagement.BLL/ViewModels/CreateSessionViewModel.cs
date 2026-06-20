using GymManagement.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagement.BLL.ViewModels
{
    public class CreateSessionViewModel
    {
        [Required(ErrorMessage ="Desctription is Required")]
        [StringLength(500, MinimumLength = 10 , ErrorMessage ="Description must be between 10 and 500")]
        public string Description { get; set; } = default!;
        [Required(ErrorMessage ="Capacity cannot be empty")]
        [Range(1,25,ErrorMessage ="Capacity must be between 1 , 25")]
        public int Capacity { get; set; }
        [Required(ErrorMessage ="StartDate must be filled")]
        [Display(Name ="Start Date & Time")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage ="EndDate must be filled")]
        [Display(Name ="End Date & Time")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Trainer be filled")]
        [Display(Name ="Trainer")]
        public int TrainerId { get; set; }
        [Required(ErrorMessage = "Category must be filled")]
        [Display(Name = "Category")]

        public int CategoryId { get; set; }

    }
}
