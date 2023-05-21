using System.ComponentModel.DataAnnotations.Schema;
namespace Face_Recognition.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //email
        //password
        [ForeignKey("Missing_Person_Id")]
        public int MId { get; set; }
        MissingPerson mp = new MissingPerson();
    }
}
