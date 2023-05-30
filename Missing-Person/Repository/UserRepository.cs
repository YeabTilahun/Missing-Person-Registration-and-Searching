using Microsoft.EntityFrameworkCore;
using Missing_Person.Models;

namespace Missing_Person.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MissingPersonDbContext context;
        public UserRepository(MissingPersonDbContext context)
        {
            this.context = context;
        }

     /*   public User DeleteUser(string id)
        {
            User user = context.Users.Find(id);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
            return user;
        }*/

        public List<User> GetAllUser()
        {
            return context.Users.ToList<User>();
        }

        public User GetUserById(string id)
        {
            return context.Users.FirstOrDefault(u => u.Id.Equals(id));
        }  

    }
}
