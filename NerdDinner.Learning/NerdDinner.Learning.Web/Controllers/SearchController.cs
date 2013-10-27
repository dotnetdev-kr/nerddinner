using NerdDinner.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NerdDinner.Core.ModelRepository;
using NerdDinner.Core.Model;

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
            this(new DinnerRepository<NerdDinnerContext>())
        {
        }

        public SearchController(IDinnerRepository repo)
        {
            repository = repo;
        }

        // GET api/Search?latitude=1.0&longitude=1.0
        [HttpGet]
        public IEnumerable<JsonDinner> SearchByLocation(double latitude, double longitude)
        {
            return FindByLocation(latitude, longitude);
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

        // POST api/Search?limit=10
        [HttpPost]
        public IEnumerable<JsonDinner> GetMostPopularDinners(int limit)
        {
            var mostPopularDinners = from dinner in repository.GetAll()
                                     where dinner.EventDate >= DateTime.Now
                                     orderby dinner.RSVPs.Count descending
                                     select dinner;

            return mostPopularDinners.Take(limit).AsEnumerable().Select(item => JsonDinnerFromDinner(item));
        }

        protected IQueryable<JsonDinner> FindByLocation(double latitude, double longitude)
        {
            var sourcePt = DbGeography.FromText(string.Format("POINT ({0} {1})", longitude, latitude));
            //TODO: CoreGeography mapping to DBGeography
            //Placeholder, need to figure out how to calc with nee Location type or to pass in values from DBGeography
            var sourcePoint = new CoreGeography() { LatitudePoint = sourcePt.Latitude, LongitudePoint = sourcePt.Longitude, Provider = sourcePt.Provider };
           
            var results =
                repository.GetAll()
                .Where(loc => loc.Location.Distance(sourcePoint) < 2000)
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
                Latitude = dinner.Location.LatitudePoint.Value,
                Longitude = dinner.Location.LongitudePoint.Value,
                Title = dinner.Title,
                Description = dinner.Description,
                RSVPCount = dinner.RSVPs.Count(),
                Url = dinner.DinnerID.ToString()
            };
        }
    }
}