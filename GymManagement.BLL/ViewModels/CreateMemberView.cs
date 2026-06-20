using System.ComponentModel.DataAnnotations;

namespace GymManagement.BLL.ViewModels
{
    public class CreateMemberView
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        public string BuildingNumber { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public double Height { get; set; }

        public double Weight { get; set; }

        public string BloodType { get; set; } = string.Empty;

        public string? Note { get; set; }
    }
}