using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;
using NerdDinner.Helpers;
using DDay.iCal.Components;

namespace NerdDinner.Controllers
{
    [HandleErrorWithELMAH]
    public class ServicesController : Controller
    {
        IDinnerRepository dinnerRepository;

        public ServicesController() : this(new DinnerRepository()){}

        public ServicesController(IDinnerRepository repository)
        {
            dinnerRepository = repository;
        }

        [OutputCache(VaryByParam = "none", Duration = 300)]
        public ActionResult RSS()
        {
            var dinners = dinnerRepository.FindUpcomingDinners();

            if (dinners == null)
                return View("NotFound");

            return new RssResult(dinners.ToList(), "Upcoming Nerd Dinners");
        }

        [OutputCache(VaryByParam = "none", Duration = 300)]
        public ActionResult iCalFeed()
        {
            var dinners = dinnerRepository.FindUpcomingDinners();

            if (dinners == null)
                return View("NotFound");

            return new iCalResult(dinners.ToList(), "NerdDinners.ics");
        }
        
        public ActionResult iCal(int id)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");

            return new iCalResult(dinner, "NerdDinner.ics");
        }
    }
}
