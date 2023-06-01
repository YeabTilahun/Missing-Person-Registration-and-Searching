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
          //return missing person as  alist if the status is missing and if it is Approved
            return context.MissingPersons.Where(e => e.Status == "Not Found" || e.Status=="Missing" && e.IsApproved == true).ToList();
        }
        public List<MissingPerson> GetMissingPeopleAdmin()
        {
            //return missing person as  alist if the status is missing and if it is Approved
            return context.MissingPersons.ToList();
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
            //remove if their is null inside data
            data.RemoveAll(e => e == null);
            return data;
        }

        public List<MissingPerson> SearchByName(string name)
        {
            //return where name is like the name in the database use like to search or %% to search
            return context.MissingPersons.Where(e => e.Name.Contains(name)).ToList();
        }

        public MissingPerson UpdateMissingPerson(MissingPerson missingPerson)
        {
            var modifiedMissingPerson = context.MissingPersons.Attach(missingPerson);
            modifiedMissingPerson.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return missingPerson;
        }
       
        public List<MissingPerson> GetMissingPersonByUserId(string userId)
        {
            return context.MissingPersons.Where(e => e.User_Id == userId).ToList();
        }
    }
}
