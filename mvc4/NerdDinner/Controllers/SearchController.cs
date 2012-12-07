using NerdDinner.Models;
using System;
using System.Collections.Generic;
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
            // TODO
            return db.Dinners.AsEnumerable();
        }

        // GET api/Search?Location=30904
        // GET api/Search?Location=Seattle
        [HttpGet]
        public IEnumerable<Dinner> SearchByPlaceNameOrZip(string Location)
        {
            // TODO
            return db.Dinners.AsEnumerable();
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
