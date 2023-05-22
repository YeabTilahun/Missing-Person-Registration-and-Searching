using System.ComponentModel.DataAnnotations;

namespace Missing_Person.Models
{
    public class MissingPerson
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 7)]
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
