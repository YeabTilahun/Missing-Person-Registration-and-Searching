using Missing_Person.Models;

namespace Missing_Person.ViewModel
{
    public class SearchViewModel
    {
        public IFormFile searchImage { get; set; }
        public string searchName { get; set; }
        public List<MissingPerson>? MissingPeople { get; set; }
    }
}
