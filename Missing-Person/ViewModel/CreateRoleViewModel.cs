using System.ComponentModel.DataAnnotations;

namespace Missing_Person.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name ="Role")]
        public string? RoleName { get; set; }
    }
}
