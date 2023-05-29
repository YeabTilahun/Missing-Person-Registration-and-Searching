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
        public User DeleteUser(string id)
        {
            User user = context.Users.Find(id);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
            return user;
        }

        public List<User> GetAllUser()
        {
            return context.Users.ToList<User>();
        }

        public User GetUserById(string id)
        {
            return context.Users.Find(id);
        }

        public User UpdateUser(User user)
        {
            var userUpdate = context.Users.Attach(user);
            userUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return user;
        }
    }
}
