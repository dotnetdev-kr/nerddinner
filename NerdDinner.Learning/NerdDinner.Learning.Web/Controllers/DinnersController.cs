using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;
using NerdDinner.Core.Model;
using NerdDinner.Core.ModelRepository;


namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {
        private readonly IDinnerRepository repository;

        public DinnersController() :
            this(new DinnerRepository<NerdDinnerContext>())
        {
        }

        public  DinnersController(IDinnerRepository repo)
        {
            repository = repo;
        }

        // GET: /Dinners/
        public ActionResult Index()
        {
            return View(repository.GetAll());
        }

        // GET: /Dinners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinner dinner = repository.FindBy(x => x.DinnerID == id).First();
            if (dinner == null)
            {
                return HttpNotFound();
            }
            return View(dinner);
        }

        // GET: /Dinners/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Dinners/Create
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                dinner.HostedBy = User.Identity.Name;
                repository.Add(dinner);
                repository.Save(); ;
                return RedirectToAction("Index");
            }

            return View(dinner);
        }

        // GET: /Dinners/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinner dinner = repository.FindBy(x => x.DinnerID == id).First();
            if (!UserIsAuthorizedForAction(dinner))
            {
                return View("InvalidOwner");
            }
            if (dinner == null)
            {
                return HttpNotFound();
            }
            return View(dinner);
        }

        // POST: /Dinners/Edit/5
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Dinner dinner)
        {
            if (!UserIsAuthorizedForAction(dinner))
            {
                return View("InvalidOwner");
            }
            if (ModelState.IsValid)
            {
                repository.Edit(dinner);
                repository.Save();
                return RedirectToAction("Index");
            }
            return View(dinner);
        }

        // GET: /Dinners/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dinner dinner = repository.FindBy(x => x.DinnerID == id).First();
            if (dinner == null)
            {
                return HttpNotFound();
            }
            if (!UserIsAuthorizedForAction(dinner))
            {
                return View("InvalidOwner");
            }
            return View(dinner);
        }

        // POST: /Dinners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Dinner dinner = repository.FindBy(x => x.DinnerID == id).First();
            if(!UserIsAuthorizedForAction(dinner))
            {
                return View("InvalidOwner");
            }
            repository.Delete(dinner);
            repository.Save();
            return RedirectToAction("Index");
        }

        private bool UserIsAuthorizedForAction(Dinner dinner)
        {
            if (!User.IsInRole("Admin"))
            {
                return dinner.IsHostedBy(User.Identity.Name);
            }
            return true;
        }
    }
}
