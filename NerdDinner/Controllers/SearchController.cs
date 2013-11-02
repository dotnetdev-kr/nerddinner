using NerdDinner.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NerdDinner.Controllers
{
    public class JsonDinner
    {
        public int DinnerID { get; set; }
        public DateTime EventDate { get; set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
        public int RSVPCount { get; set; }
        public string Url { get; set; }
    }

    public class SearchController : ApiController
    {
        private readonly IDinnerRepository repository;

        public SearchController() :
            this(new DinnerRepository())
        {
        }

        public SearchController(IDinnerRepository repo)
        {
            repository = repo;
        }

        // GET api/Search?latitude=1.0&longitude=1.0&distance=2000
        [HttpGet]
        public IEnumerable<JsonDinner> SearchByLocation(double latitude, double longitude, double distance)
        {
            return FindByLocation(latitude, longitude, distance);
        }

        // GET api/Search?location=30901
        // GET api/Search?location=Seattle
        [HttpGet]
        public IEnumerable<JsonDinner> SearchByPlaceNameOrZip(string location)
        {
            if (String.IsNullOrEmpty(location)) return null;
            //LatLong foundlocation = GeolocationService.PlaceOrZipToLatLong(location);
            //if (foundlocation != null)
            //{
                //return FindByLocation(foundlocation.Lat, foundlocation.Long)
                                //.OrderByDescending(p => p.EventDate);
            //}
            return null;
        }

        // GET api/Search/PopularDinners?limit=10
        [HttpGet]
        [Route("api/search/PopularDinners")]
        public IEnumerable<JsonDinner> PopularDinners(int limit)
        {
            var mostPopularDinners = from dinner in repository.GetAll()
                                     where dinner.EventDate >= DateTime.Now
                                     orderby dinner.RSVPs.Count descending
                                     select dinner;

            return mostPopularDinners.Take(limit).AsEnumerable().Select(item => JsonDinnerFromDinner(item));
        }

        // GET api/Search/UpcomingDinners?limit=10
        [HttpGet]
        [Route("api/search/UpcomingDinners")]
        public IEnumerable<JsonDinner> UpcomingDinners(int limit)
        {
            var upcomingDinners = from dinner in repository.GetAll()
                                     where dinner.EventDate >= DateTime.Now
                                     orderby dinner.EventDate descending
                                     select dinner;

            return upcomingDinners.Take(limit).AsEnumerable().Select(item => JsonDinnerFromDinner(item));
        }

        // GET api/Search/ClosestDinners?latitude=1.0&longitude=1.0&limit=10
        [HttpGet]
        [Route("api/search/ClosestDinners")]
        public IEnumerable<JsonDinner> ClosestDinners(double latitude, double longitude, int limit)
        {
            var sourcePoint = DbGeography.FromText(string.Format("POINT ({0} {1})", longitude, latitude));

            var closeDinners =
                repository.GetAll()
                .OrderBy(loc => loc.Location.Distance(sourcePoint));

            return closeDinners.Take(limit).AsEnumerable().Select(item => JsonDinnerFromDinner(item));
        }

        protected IQueryable<JsonDinner> FindByLocation(double latitude, double longitude, double distance)
        {
            var sourcePoint = DbGeography.FromText(string.Format("POINT ({0} {1})", longitude, latitude));

            var results =
                repository.GetAll()
                .Where(loc => loc.Location.Distance(sourcePoint) < distance)
                .OrderBy(loc => loc.Location.Distance(sourcePoint));

            var jsonDinners = results.AsEnumerable()
                    .Select(item => JsonDinnerFromDinner(item));

            return jsonDinners.AsQueryable<JsonDinner>();
        }

        private JsonDinner JsonDinnerFromDinner(Dinner dinner)
        {
            return new JsonDinner
            {
                DinnerID = dinner.DinnerID,
                EventDate = dinner.EventDate,
                Latitude = dinner.Location.Latitude.Value,
                Longitude = dinner.Location.Longitude.Value,
                Title = dinner.Title,
                Description = dinner.Description,
                RSVPCount = dinner.RSVPs.Count(),
                Url = dinner.DinnerID.ToString()
            };
        }
    }
}