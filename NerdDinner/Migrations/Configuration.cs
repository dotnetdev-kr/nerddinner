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
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NerdDinner.Models.NerdDinnerContext context)
        {
            context.Dinners.AddOrUpdate(
                new Dinner()
                {
                    DinnerID = 1,
                    Title = "Dinner at the White House",
                    EventDate = DateTime.Now.AddDays(1),
                    Description = "Dinner at the White House",
                    HostedBy = "@barackobama",
                    ContactPhone = "(555) 555-1111",
                    Address = "1600 Pennsylvania Ave Nw, Washington, DC 20500",
                    Country = "USA",
                    Location = DbGeography.PointFromText(string.Format("POINT({0} {1})", -77.036545, 38.897096), DbGeography.DefaultCoordinateSystemId)
                },
                new Dinner()
                {
                    DinnerID = 2,
                    Title = "Meetup at Big Ben",
                    EventDate = DateTime.Now.AddDays(2),
                    Description = "We're going to meet at Big Ben",
                    HostedBy = "@scottgu",
                    ContactPhone = "(555) 555-1212",
                    Address = "Westminster, London 94710",
                    Country = "UK",
                    Location = DbGeography.PointFromText(string.Format("POINT({0} {1})", -0.1246, 51.5007), DbGeography.DefaultCoordinateSystemId)
                },
                new Dinner()
                {
                    DinnerID = 3,
                    Title = "Lunch at the Sphinx",
                    EventDate = DateTime.Now.AddDays(3),
                    Description = "Let's have lunch at the Sphinx",
                    HostedBy = "@codinghorror",
                    ContactPhone = "(555) 555-1313",
                    Address = "Sudan Street, Giza",
                    Country = "Egypt",
                    Location = DbGeography.PointFromText(string.Format("POINT({0} {1})", 31.2036, 30.0703), DbGeography.DefaultCoordinateSystemId)
                },
                new Dinner()
                {
                    DinnerID = 4,
                    Title = "Snack near Machu Picchu",
                    EventDate = DateTime.Now.AddDays(4),
                    Description = "Stop for a snack near Machu Picchu",
                    HostedBy = "@ShawnWildermuth",
                    ContactPhone = "(555) 555-1414",
                    Address = "Aguas Calientes",
                    Country = "Peru",
                    Location = DbGeography.PointFromText(string.Format("POINT({0} {1})", -72.5455, -13.1630), DbGeography.DefaultCoordinateSystemId)
                },
                new Dinner()
                {
                    DinnerID = 5,
                    Title = "Meal on the Wall",
                    EventDate = DateTime.Now.AddDays(5),
                    Description = "Formal dinner on the Great Wall of China",
                    HostedBy = "@haacked",
                    ContactPhone = "(555) 555-1515",
                    Address = "Beijing",
                    Country = "China",
                    Location = DbGeography.PointFromText(string.Format("POINT({0} {1})", 117.2319, 40.6769), DbGeography.DefaultCoordinateSystemId)
                }
            );
            
            context.RSVPs.AddOrUpdate(
                new RSVP() { RsvpID = 1, DinnerID = 1, AttendeeName = "@shanselman" },
                new RSVP() { RsvpID = 2, DinnerID = 1, AttendeeName = "@pmourfield" },
                new RSVP() { RsvpID = 3, DinnerID = 2, AttendeeName = "@jongalloway" },
                new RSVP() { RsvpID = 4, DinnerID = 2, AttendeeName = "@taraw" },
                new RSVP() { RsvpID = 5, DinnerID = 3, AttendeeName = "@shanselman" },
                new RSVP() { RsvpID = 6, DinnerID = 3, AttendeeName = "@taraw" },
                new RSVP() { RsvpID = 7, DinnerID = 4, AttendeeName = "@jongalloway" },
                new RSVP() { RsvpID = 8, DinnerID = 4, AttendeeName = "@pmourfield" },
                new RSVP() { RsvpID = 9, DinnerID = 5, AttendeeName = "@shanselman" },
                new RSVP() { RsvpID = 10, DinnerID = 5, AttendeeName = "@jongalloway" }
            );
        }
    }
}
