using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoTB.Domain.Entities;
using GoTB.Domain.Concrete;
using GoTB.WebUI.Models;
using System.IO;

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
            if (!IsAdmin())
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
            if (!IsAdmin())
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
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");
            Character character = db.Characters.Find(id);
            db.Characters.Remove(character);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UploadImage()
        {
            var characterWithoutImage = db.Characters.ToArray(); 
            return View(characterWithoutImage);
        }

        [HttpPost]
        public ActionResult UploadImage(SimpleFileView fileView, int charId)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");
            var basePath = AppDomain.CurrentDomain.BaseDirectory + @"Content\Images\";
            var fileName = fileView.UploadedFile.FileName;
            fileView.UploadedFile.SaveAs(basePath + fileName);
            db.Characters.First(c => c.Id == charId).ImageName = fileName;
            db.SaveChanges();
            return RedirectToAction("UploadImage");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private bool IsAdmin()
        {
            return db.UserProfiles.Any(x => x.UserName == User.Identity.Name && x.IsAdmin);
        }
    }
}