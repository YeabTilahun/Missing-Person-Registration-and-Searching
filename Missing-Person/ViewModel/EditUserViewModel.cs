using Missing_Person.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Missing_Person.ViewModel
{
    public class EditUserViewModel
    {
      public User users { get; set; } = new User();
    }
}