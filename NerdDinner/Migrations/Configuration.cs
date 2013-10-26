namespace NerdDinner.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using NerdDinner.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    using System.Linq;

    internal sealed class NerdDinnerContextConfiguration : DbMigrationsConfiguration<NerdDinner.Models.NerdDinnerContext>
    {
        public NerdDinnerContextConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(NerdDinner.Models.NerdDinnerContext context)
        {
            context.SeedData();
        }
    }

    internal sealed class DefaultConfiguration : DbMigrationsConfiguration<NerdDinner.Models.ApplicationDbContext>
    {
        public DefaultConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(NerdDinner.Models.ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(
                new IdentityRole("Admin")
                );
        }
    }
}
