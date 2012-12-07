using NerdDinner.Models;
using NerdDinner.Services;
using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NerdDinner.Controllers
{
    public class SearchController : ApiController
    {
        private NerdDinnerContext db = new NerdDinnerContext();

        // GET api/Search?latitude=1.0&longitude=1.0
        [HttpGet]
        public IEnumerable<Dinner> SearchByLocation(float latitude, float longitude)
        {
            return FindByLocation(latitude, longitude);
        }

        // GET api/Search?Location=30904
        // GET api/Search?Location=Seattle
        [HttpGet]
        public IEnumerable<Dinner> SearchByPlaceNameOrZip(string location)
        {
            if (String.IsNullOrEmpty(location)) return null;
            LatLong foundlocation = GeolocationService.PlaceOrZipToLatLong(location);
            if (foundlocation != null)
            {
                return FindByLocation(foundlocation.Lat, foundlocation.Long).
                                OrderByDescending(p => p.EventDate);
            }
            return null;
        }

        // POST api/Search?limit=10
        [HttpPost]
        public IEnumerable<Dinner> GetMostPopularDinners(int limit)
        {
            var mostPopularDinners = from dinner in db.Dinners
                                     where dinner.EventDate >= DateTime.Now
                                     orderby dinner.RSVPs.Count descending
                                     select dinner;

            return mostPopularDinners.Take(limit).AsEnumerable();
        }

        // Thanks Rick Strahl!
        // http://www.west-wind.com/weblog/posts/2012/Jun/21/Basic-Spatial-Data-with-SQL-Server-and-Entity-Framework-50
        protected DbGeography CreatePoint(double latitude, double longitude)
        {
            var text = string.Format(CultureInfo.InvariantCulture.NumberFormat,
                                     "POINT({0} {1})", longitude, latitude);
            // 4326 is most common coordinate system used by GPS/Maps
            return DbGeography.PointFromText(text, 4326);
        }

        protected IEnumerable<Dinner> FindByLocation(float latitude, float longitude)
        {
            var sourcePoint = CreatePoint(45.712113, -121.527200);

            var results =
                db.Dinners
                        .Where(loc => loc.Location.Distance(sourcePoint) < 1000)
                        .OrderBy(loc => loc.Location.Distance(sourcePoint));

            //foreach (Dinner dinner in results)
            //{
            //    dinner.RSVPs = new List<RSVP>();

            //    var rsvps = db.RSVPs.Where(x => x.DinnerID == dinner.DinnerID);

            //    foreach (RSVP rsvp in rsvps)
            //    {
            //        dinner.RSVPs.Add(rsvp);
            //    }
            //}

            return results.AsQueryable<Dinner>();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
