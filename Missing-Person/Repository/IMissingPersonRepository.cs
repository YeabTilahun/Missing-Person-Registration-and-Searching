using Missing_Person.Models;

namespace Missing_Person.Repository
{
    public interface IMissingPersonRepository
    {
        public List<MissingPerson> GetMissingPeople();
        public MissingPerson GetMissingPerson(int id);
        public MissingPerson AddMissingPerson(MissingPerson missingPerson);
        public MissingPerson DeleteMissingPerson(int id);
        public MissingPerson UpdateMissingPerson(MissingPerson missingPerson);
        public List<MissingPerson> GetMissingPersonByImage(List<string> imgUrl);
        public List<MissingPerson> SearchByName(string name);
    }
}
