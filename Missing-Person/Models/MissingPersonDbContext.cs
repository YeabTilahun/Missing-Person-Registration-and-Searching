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

       /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var item in modelBuilder.Model.GetEntityTypes())
            {
                var p = item.FindPrimaryKey().Properties.FirstOrDefault(i => i.ValueGenerated != Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.Never);
                if (p != null)
                {
                    p.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.Never;
                }

            }
        }*/
    }
}
