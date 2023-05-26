using Missing_Person.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Missing_Person.ViewModel
{
    public class RegisterMissingPersonViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 7, ErrorMessage ="Please enter your full name")]
        [Display(Name = "Full Name")]
        public string? Name { get; set; }

        [Required]
        public string? DOB { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        public string? Height { get; set; }

        public string? Weight { get; set; }

        public string? Eye_Color { get; set; }
        public string? Hair_Color { get; set; }
        public string? Race { get; set; }

        [Required]
        public string? Last_Location { get; set; }

        [Required]
        public IFormFileCollection? ImagePath { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Contact { get; set; }

        public string? Status { get; set; }

        public string? Missing_Date { get; set; }

    }
}
