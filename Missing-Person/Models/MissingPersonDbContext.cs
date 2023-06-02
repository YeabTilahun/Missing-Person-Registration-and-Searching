using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Missing_Person.Models
{
    public class MissingPersonDbContext : IdentityDbContext
    {
        public MissingPersonDbContext(DbContextOptions<MissingPersonDbContext> options) : base(options)
        {

        }
        public DbSet<MissingPerson> MissingPersons { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
