using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace NerdDinner.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }

    public class CreateApplicationDbContextIfNotExists : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.Roles.Add(
                new IdentityRole("Admin")
                );
            context.SaveChanges();

            base.Seed(context);
        }
    }
}