using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoTB.Domain.Entities;
using GoTB.Domain.Concrete;

namespace MvcApplication1.Controllers
{
    [Authorize]
    public class CharacterEditController : Controller
    {
        private EFDbContext db = new EFDbContext();

        //
        // GET: /CharacterEdit/

        public ActionResult Index()
        {
            return View(db.Characters.ToList());
        }

        //
        // GET: /CharacterEdit/Details/5

        public ActionResult Details(int id = 0)
        {
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        //
        // GET: /CharacterEdit/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CharacterEdit/Create

        [HttpPost]
        public ActionResult Create(Character character)
        {
            if (IsNotAdmin())
                return RedirectToAction("Login", "Account");
            if (ModelState.IsValid)
            {
                db.Characters.Add(character);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(character);
        }

        //
        // GET: /CharacterEdit/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        //
        // POST: /CharacterEdit/Edit/5

        [HttpPost]
        public ActionResult Edit(Character character)
        {
            if (IsNotAdmin())
                return RedirectToAction("Login", "Account");
            if (ModelState.IsValid)
            {
                db.Entry(character).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(character);
        }

        //
        // GET: /CharacterEdit/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        //
        // POST: /CharacterEdit/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (IsNotAdmin())
                return RedirectToAction("Login", "Account");
            Character character = db.Characters.Find(id);
            db.Characters.Remove(character);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private bool IsNotAdmin()
        {
            var context = new EFDbContext();
            if (context.UserProfiles.Count(x => x.UserName == User.Identity.Name) < 0)
                return true;
            return false;
        }
    }
}