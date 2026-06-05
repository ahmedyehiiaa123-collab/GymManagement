using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagement.BLL.ViewModels
{
    public class CreateMemberView
    {
        [Required(ErrorMessage ="Name Is Required")]
        [RegularExpression(@"",ErrorMessage ="Name Can only Contain Characters or spaces")]
        public string Name { get; set; }
    }
}
