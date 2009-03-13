using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers {

    public class JsonDinner {
        public int      DinnerID    { get; set; }
        public DateTime EventDate   { get; set; }
        public string   Title       { get; set; }
        public double   Latitude    { get; set; }
        public double   Longitude   { get; set; }
        public string   Description { get; set; }
        public int      RSVPCount   { get; set; }
    }

    public class SearchController : Controller {

        IDinnerRepository dinnerRepository;

        //
        // Dependency Injection enabled constructors

        public SearchController()
            : this(new DinnerRepository()) {
        }

        public SearchController(IDinnerRepository repository) {
            dinnerRepository = repository;
        }

        //
        // AJAX: /Search/FindByLocation?longitude=45&latitude=-90

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SearchByLocation(float latitude, float longitude)
        {
            var dinners = dinnerRepository.FindByLocation(latitude, longitude);

            var jsonDinners = from dinner in dinners
                              select JsonDinnerFromDinner(dinner);

            return Json(jsonDinners.ToList());
        }

        //
        // AJAX: /Search/GetMostPopularDinners
        // AJAX: /Search/GetMostPopularDinners?limit=5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetMostPopularDinners(int? limit)
        {
            var dinners = dinnerRepository.FindUpcomingDinners();

            // Default the limit to 40, if not supplied.
            if (!limit.HasValue)
                limit = 40;

            var mostPopularDinners = from dinner in dinners
                                     orderby dinner.RSVPs.Count descending
                                     select JsonDinnerFromDinner(dinner);

            return Json(mostPopularDinners.Take(limit.Value).ToList());
        }

        private JsonDinner JsonDinnerFromDinner(Dinner dinner)
        {
            return new JsonDinner
            {
                DinnerID = dinner.DinnerID,
                EventDate = dinner.EventDate,
                Latitude = dinner.Latitude,
                Longitude = dinner.Longitude,
                Title = dinner.Title,
                Description = dinner.Description,
                RSVPCount = dinner.RSVPs.Count
            };
        }

    }
}
