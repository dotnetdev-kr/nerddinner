using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;


namespace NerdDinner.Models
{

    public class DinnerRepository : IDinnerRepository
    {

        NerdDinnerEntities db = new NerdDinnerEntities();

        //
        // Query Methods

        public IQueryable<Dinner> FindDinnersByText(string q)
        {
            return All.Where(d => d.Title.Contains(q)
                            || d.Description.Contains(q)
                            || d.HostedBy.Contains(q));
        }

        public IQueryable<Dinner> All
        {
            get { return db.Dinners; }
        }

        public IQueryable<Dinner> FindUpcomingDinners()
        {
            return from dinner in All
                   where dinner.EventDate >= DateTime.Now
                   orderby dinner.EventDate
                   select dinner;
        }

        public IQueryable<Dinner> FindByLocation(float latitude, float longitude)
        {

            var dinners = from dinner in FindUpcomingDinners()
                          join i in NearestDinners(latitude, longitude)
                          on dinner.DinnerID equals i.DinnerID
                          select dinner;

            return dinners;
        }

        public IQueryable<Dinner> AllIncluding(params Expression<Func<Dinner, object>>[] includeProperties)
        {
            IQueryable<Dinner> query = All;
            foreach (var includeProperty in includeProperties)
            {
                // query = query.Include(includeProperty);
            }
            return query;
        }

        public Dinner Find(int id)
        {
            return db.Dinners.SingleOrDefault(d => d.DinnerID == id);
        }

        //
        // Insert/Delete Methods

        public void InsertOrUpdate(Dinner dinner)
        {
            db.Dinners.AddObject(dinner);
        }

        public void Delete(int id)
        {
            var dinner = Find(id);
            foreach (RSVP rsvp in dinner.RSVPs.ToList())
                db.RSVPs.DeleteObject(rsvp);
            db.Dinners.DeleteObject(dinner);
        }

        public void DeleteRsvp(RSVP rsvp)
        {
            db.RSVPs.DeleteObject(rsvp);
        }

        //
        // Persistence 

        public void Save()
        {
            db.SaveChanges();
        }


        // Helper Methods

        [EdmFunction("NerdDinnerModel.Store", "DistanceBetween")]
        public static double DistanceBetween(double lat1, double long1, double lat2, double long2)
        {
            throw new NotImplementedException("Only call through LINQ expression");
        }

        public IQueryable<Dinner> NearestDinners(double latitude, double longitude)
        {
            return from d in db.Dinners
                   where DistanceBetween(latitude, longitude, d.Latitude, d.Longitude) < 1000
                   select d;
        }
    }
}
