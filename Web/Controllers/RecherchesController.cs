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
using Microsoft.AspNet.Identity;
using System.IO;
using System.Globalization;
using System.Text;

namespace Web.Controllers
{
    public class RecherchesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: 
        public ActionResult Index()
        {
            ViewBag.ListTypeAnimaux = db.TypeAnimaux.ToList();
            return View(db.Recherche.Where(f => f.Desactiver == false).ToList());
        }

        // GET: Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recherche recherche = db.Recherche.Find(id);
            if (recherche == null)
            {
                return HttpNotFound();
            }
            return View(recherche);
        }

        // GET: Create
        [Authorize(Roles = "Membre, Admin")]
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> List = db.TypeAnimaux.Select(c => new SelectListItem { Text = c.Nom, Value = c.Id.ToString() }).ToList();
            RechercheViewModel model = new RechercheViewModel { ListTypeAnimaux = List };
            return View(model);
        }

        // POST: Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Membre, Admin")]
        public ActionResult Create(RechercheViewModel model, HttpPostedFileBase file)
        {
            Random rnd = new Random();
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            DateTime date = DateTime.Now;
            TypeAnimaux type = db.TypeAnimaux.Find(model.SelectedTypeAnimaux);

            // Upload des images
            List<Image> images = new List<Image>();
            if (file.ContentLength > 0)
            {
                // Supprime les accents du nom du fichier
                string _name = Path.GetFileName(file.FileName);
                //String _FileName = rnd.Next(1, 9999999).ToString();
                String _FileName = ((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
                _FileName += Path.GetExtension(_name);

                string _path = Path.Combine(Server.MapPath("~/Content/UploadedFiles"), _FileName);
                file.SaveAs(_path);
                Image image = new Image();
                image.Url = "Content/UploadedFiles/" + _FileName;
                images.Add(image);
            }

            Recherche recherche = new Recherche
            {
                Nom = model.Nom,
                Description = model.Description,
                Sexe = model.Sexe,
                Race = model.Race,
                Adresse = model.Adresse,
                Date_ajout = date,
                Date_recherche = model.Date_recherche,
                User = user,
                Images = images,
                Type = type
            };

            if (ModelState.IsValid)
            {
                db.Recherche.Add(recherche);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recherche);
        }

        // GET: Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recherche recherche = db.Recherche.Find(id);
            if (recherche == null)
            {
                return HttpNotFound();
            }
            RechercheViewModel model = new RechercheViewModel
            {
                Id = id,
                Adresse = recherche.Adresse,
                Description = recherche.Description,
                Date_recherche = recherche.Date_recherche,
                Nom = recherche.Nom,
                Race = recherche.Race,
                Sexe = recherche.Sexe,
                ListTypeAnimaux = db.TypeAnimaux.Select(c => new SelectListItem { Text = c.Nom, Value = c.Id.ToString() }).ToList(),
                SelectedTypeAnimaux = recherche.Type.Id
            };
            return View(model);
        }

        // POST: Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RechercheViewModel model)
        {
            if (ModelState.IsValid)
            {
                var originalRecherche = db.Recherche.Find(model.Id);

                if (originalRecherche != null)
                {
                    TypeAnimaux type = db.TypeAnimaux.Find(model.SelectedTypeAnimaux);

                    originalRecherche.Nom = model.Nom;
                    originalRecherche.Description = model.Description;
                    originalRecherche.Sexe = model.Sexe;
                    originalRecherche.Race = model.Race;
                    originalRecherche.Adresse = model.Adresse;
                    originalRecherche.Type = type;
                    originalRecherche.Desactiver = model.Desactiver;
                    originalRecherche.Trouver = model.Trouver;

                    //db.Entry(originalRecherche).CurrentValues.SetValues(updateRecherche);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                //db.Entry(recherche).State = EntityState.Modified;
                //db.SaveChanges();
            }
            return View(model);
        }

        // GET: Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recherche recherche = db.Recherche.Find(id);
            if (recherche == null)
            {
                return HttpNotFound();
            }
            return View(recherche);
        }

        // POST: Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recherche recherche = db.Recherche.Find(id);
            db.Recherche.Remove(recherche);
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
