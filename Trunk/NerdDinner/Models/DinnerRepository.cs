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
        NerdDinners db = new NerdDinners();

        public IQueryable<Dinner> FindByLocation(float latitude, float longitude)
        {
            List<Dinner> resultList = new List<Dinner>();

            var results = db.Database.SqlQuery<Dinner>("SELECT * FROM Dinners WHERE EventDate >= {0} AND dbo.DistanceBetween({1}, {2}, Latitude, Longitude) < 1000", DateTime.Now, latitude, longitude);
            return results.AsQueryable<Dinner>();
        }

        public IQueryable<Dinner> FindUpcomingDinners()
        {
            return from dinner in All.Include(d=>d.RSVPs)
                   where dinner.EventDate >= DateTime.Now
                   orderby dinner.EventDate
                   select dinner;
        }

        public IQueryable<Dinner> FindDinnersByText(string q)
        {
            return All.Include(d => d.RSVPs).Where(d => d.Title.Contains(q)
                            || d.Description.Contains(q)
                            || d.HostedBy.Contains(q));
        }

        public IQueryable<Dinner> All
        {
            get { return db.Dinners; }
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
            return db.Dinners.Include(r => r.RSVPs).SingleOrDefault(d => d.DinnerID == id);
        }

        //
        // Insert/Delete Methods

        public void InsertOrUpdate(Dinner dinner)
        {
            if (dinner.DinnerID == default(int))
            {
                // New entity
                db.Dinners.Add(dinner);
            }
            else
            {
                // Existing entity
                db.Entry(dinner).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var dinner = Find(id);
            foreach (RSVP rsvp in dinner.RSVPs.ToList())
                db.RSVPs.Remove(rsvp);
            db.Dinners.Remove(dinner);
        }

        public void DeleteRsvp(RSVP rsvp)
        {
            db.RSVPs.Remove(rsvp);
            db.SaveChanges();
        }

        //
        // Persistence 

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
