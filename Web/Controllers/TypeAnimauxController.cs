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
    [Authorize(Roles="Admin")]
    public class TypeAnimauxController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TypeAnimaux
        public ActionResult Index()
        {
            return View(db.TypeAnimaux.ToList());
        }

        // GET: TypeAnimaux/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeAnimaux typeAnimaux = db.TypeAnimaux.Find(id);
            if (typeAnimaux == null)
            {
                return HttpNotFound();
            }
            return View(typeAnimaux);
        }

        // GET: TypeAnimaux/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeAnimaux/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom")] TypeAnimaux typeAnimaux)
        {
            if (ModelState.IsValid)
            {
                db.TypeAnimaux.Add(typeAnimaux);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeAnimaux);
        }

        // GET: TypeAnimaux/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeAnimaux typeAnimaux = db.TypeAnimaux.Find(id);
            if (typeAnimaux == null)
            {
                return HttpNotFound();
            }
            return View(typeAnimaux);
        }

        // POST: TypeAnimaux/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom")] TypeAnimaux typeAnimaux)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeAnimaux).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeAnimaux);
        }

        // GET: TypeAnimaux/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeAnimaux typeAnimaux = db.TypeAnimaux.Find(id);
            if (typeAnimaux == null)
            {
                return HttpNotFound();
            }
            return View(typeAnimaux);
        }

        // POST: TypeAnimaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeAnimaux typeAnimaux = db.TypeAnimaux.Find(id);
            db.TypeAnimaux.Remove(typeAnimaux);
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
