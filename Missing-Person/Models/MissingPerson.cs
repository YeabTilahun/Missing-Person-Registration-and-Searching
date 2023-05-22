namespace Missing_Person.Models
{
    public class MissingPerson
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? DOB { get; set; }
        public string? Gender { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string? Eye_Color { get; set; }
        public string? Hair_Color { get; set; }
        public string? Race { get; set; }
        public string? Last_Location { get; set; }
        public IFormFileCollection? ImagePath { get; set; }
        public string? Description { get; set; }
        public string? Contact { get; set; }
        public string? Status { get; set; }
        public string? Missing_Date { get; set; }
    }
}
