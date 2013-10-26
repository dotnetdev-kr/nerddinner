using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;

namespace NerdDinner.Models
{
    public class NerdDinnerContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<NerdDinner.Models.NerdDinnerContext>());

        public NerdDinnerContext()
            : base("name=NerdDinnerContext")
        {
        }

        public System.Data.Entity.DbSet<NerdDinner.Models.Dinner> Dinners { get; set; }
        public System.Data.Entity.DbSet<NerdDinner.Models.RSVP> RSVPs { get; set; }

        public void SeedData()
        {
            var testDinner1 = new Dinner()
            {
                DinnerID = 1,
                Title = "Dinner at the White House",
                EventDate = DateTime.Now.AddDays(5),
                Description = "Dinner at the White House",
                HostedBy = "The President",
                ContactPhone = "(202) 456-1111",
                Address = "1600 Pennsylvania Ave Nw Washington, DC 20500",
                Country = "USA",
                Location = DbGeography.PointFromText(string.Format("POINT({0} {1})", -77.036545, 38.897096), DbGeography.DefaultCoordinateSystemId),
                RSVPs = new List<RSVP>()
            };
            testDinner1.RSVPs.Add(new RSVP() { RsvpID = 1, DinnerID = 1, AttendeeName = "@shanselman" });
            testDinner1.RSVPs.Add(new RSVP() { RsvpID = 2, DinnerID = 1, AttendeeName = "@pmourfield" });

            Dinners.Add(testDinner1);

            SaveChanges();
        }
    }
}