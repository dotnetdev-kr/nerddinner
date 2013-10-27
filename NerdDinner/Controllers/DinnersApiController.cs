using NerdDinner.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace NerdDinner.Controllers
{
    public class DinnersApiController : ApiController
    {
        private readonly IDinnerRepository repository;

        public DinnersApiController() :
            this(new DinnerRepository())
        {
        }

        public DinnersApiController(IDinnerRepository repo)
        {
            repository = repo;
        }

        // GET api/Dinners
        [Route("api/dinners")]
        public IQueryable<Dinner> GetDinners()
        {
            return repository.GetAll().Include(r => r.RSVPs);
        }

        // GET api/Default1/5
        [ResponseType(typeof(Dinner))]
        [Route("api/dinners/{id}")]
        public IHttpActionResult GetDinner(int id)
        {
            Dinner dinner = repository.FindBy(x => x.DinnerID == id).Include(r => r.RSVPs).First();
            if (dinner == null)
            {
                return NotFound();
            }

            return Ok(dinner);
        }
    }
}