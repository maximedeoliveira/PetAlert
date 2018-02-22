using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Model;
using Web.Models;

namespace Web.Controllers
{
    public class SignalementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Signalements
        public ActionResult Index()
        {
            return View(db.Signalement.Where(f => f.Desactiver == false).ToList());
        }

        // GET: Signalements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Signalement signalement = db.Signalement.Find(id);
            if (signalement == null)
            {
                return HttpNotFound();
            }
            return View(signalement);
        }

        // GET: Signalements/Create
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> List = db.TypeAnimaux.Select(c => new SelectListItem { Text = c.Nom, Value = c.Id.ToString() }).ToList();
            SignalementViewModel model = new SignalementViewModel { ListTypeAnimaux = List };
            return View(model);
        }

        // POST: Signalements/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SignalementViewModel model)
        {
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            DateTime date = DateTime.Now;
            TypeAnimaux type = db.TypeAnimaux.Find(model.SelectedTypeAnimaux);
            
            Signalement signalement = new Signalement
            {
                Description = model.Description,
                Adresse = model.Adresse,
                Date_ajout = date,
                User = user,
                Type = type
            };

            if (ModelState.IsValid)
            {
                db.Signalement.Add(signalement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(signalement);

        }

        // GET: Signalements/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Signalement signalement = db.Signalement.Find(id);
            if (signalement == null)
            {
                return HttpNotFound();
            }
            RechercheViewModel model = new RechercheViewModel
            {
                Id = id,
                Adresse = signalement.Adresse,
                Description = signalement.Description,
                ListTypeAnimaux = db.TypeAnimaux.Select(c => new SelectListItem { Text = c.Nom, Value = c.Id.ToString() }).ToList(),
                SelectedTypeAnimaux = signalement.Type.Id
            };
            return View(model);
        }

        // POST: Signalements/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SignalementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var originalSignalement = db.Signalement.Find(model.Id);

                if (originalSignalement != null)
                {
                    TypeAnimaux type = db.TypeAnimaux.Find(model.SelectedTypeAnimaux);
                    
                    originalSignalement.Description = model.Description;
                    originalSignalement.Adresse = model.Adresse;
                    originalSignalement.Type = type;
                    originalSignalement.Desactiver = model.Desactiver;
                    
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        // GET: Signalements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Signalement signalement = db.Signalement.Find(id);
            if (signalement == null)
            {
                return HttpNotFound();
            }
            return View(signalement);
        }

        // POST: Signalements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Signalement signalement = db.Signalement.Find(id);
            db.Signalement.Remove(signalement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
