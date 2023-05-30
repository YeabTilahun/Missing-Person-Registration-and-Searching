using Microsoft.AspNetCore.Hosting;
using Missing_Person.Models;

namespace Missing_Person.Repository
{
    public class MissingPersonRepository: IMissingPersonRepository
    {
        private readonly MissingPersonDbContext context;
        public MissingPersonRepository(MissingPersonDbContext context)
        {
            this.context = context;
        }

        //Add missing person to the database (register)
        public MissingPerson AddMissingPerson(MissingPerson missingPerson)
        {
            context.MissingPersons.Add(missingPerson);
            context.SaveChanges();
            return missingPerson;
        }

        public MissingPerson DeleteMissingPerson(int id)
        {
            MissingPerson missingPerson = context.MissingPersons.Find(id);
            if (missingPerson != null)
            {
                context.MissingPersons.Remove(missingPerson);
                context.SaveChanges();
            }
            return missingPerson;
        }

        public List<MissingPerson> GetMissingPeople()
        {
           return context.MissingPersons.ToList<MissingPerson>();
        }
        public MissingPerson GetMissingPerson(int id)
        {
            return context.MissingPersons.Find(id);
        }
        public List<MissingPerson> GetMissingPersonByImage(List<string> imgUrl)
        {
            List<MissingPerson> data = new List<MissingPerson>();
            foreach (var item in imgUrl)
            {
                data.Add(context.MissingPersons.FirstOrDefault(e => e.ImageUrl == item));
            }
            return data;
        }

        public List<MissingPerson> SearchByName(string name)
        {
            return context.MissingPersons.Where(e => e.Name == name).ToList();
        }

        public MissingPerson UpdateMissingPerson(MissingPerson missingPerson)
        {
            var modifiedMissingPerson = context.MissingPersons.Attach(missingPerson);
            modifiedMissingPerson.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return missingPerson;
        }
       /* public MissingPerson UpdateMissingPersonStatus(int id, string status)
        {
            MissingPerson missingPerson = context.MissingPersons.Find(id);
            if (missingPerson != null)
            {
                missingPerson.Status = status;
                context.SaveChanges();
            }
            return missingPerson;
        }*/
        public List<MissingPerson> GetMissingPersonByUserId(string userId)
        {
            return context.MissingPersons.Where(e => e.User_Id == userId).ToList();
        }
    }
}
