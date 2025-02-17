﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using point3ri_Alpha_0._51.Models;
using Microsoft.AspNet.Identity;
using System.Collections;

namespace point3ri_Alpha_0._51.Controllers
{
    [Authorize]
    public class RezervacijasController : Controller
    {
        private point3ri db = new point3ri();

        // GET: Rezervacijas
        public ActionResult Index()
        {
            var rezervacijas = db.Rezervacijas.Include(r => r.AspNetUser).Include(r => r.DanTermini).Include(r => r.Oprema);
            return View(rezervacijas.ToList());
        }

        // GET: Rezervacijas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacijas.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            return View(rezervacija);
        }

        // GET: Rezervacijas/Create
        public ActionResult Create()
        {
            ViewBag.KorisnikID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.DanTerminiID = new SelectList(db.DanTerminis, "ID", "Termin");
            ViewBag.OpremaID = new SelectList(db.Opremas, "ID", "Naziv");
            return View();
        }

        // Primjer za glavnu
        // POST: Rezervacijas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DatumRezervacije,DanTerminiID,OpremaID,ProstorijaID,VrijemeRezerviranja,RezervacijaAktivna")] Rezervacija rezervacija)
        {

            rezervacija.KorisnikID = User.Identity.GetUserId();           
            if (ModelState.IsValid)
            {                
                db.Rezervacijas.Add(rezervacija);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ViewBag.KorisnikID = new SelectList(db.AspNetUsers, "Id", "Email", rezervacija.KorisnikID);
            ViewBag.DanTerminiID = new SelectList(db.DanTerminis, "ID", "ID", rezervacija.DanTerminiID);
            ViewBag.OpremaID = new SelectList(db.Opremas, "ID", "Naziv", rezervacija.OpremaID);
            return View(rezervacija);
        }

        // GET: Rezervacijas/CreateRacunala
        public ActionResult CreateRacunala()
        {
            ViewBag.KorisnikID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.DanTerminiID = new SelectList(db.DanTerminis, "ID", "Termin");
            ViewBag.OpremaID = new SelectList(db.Opremas.Where(o => o.KategorijaOpremeID == 1), "ID", "Naziv");
            ViewBag.ProstorijaID = new SelectList(db.Prostorijas.Where(p => p.Naziv.Contains("IMP")), "ID", "Naziv");
            return View();
        }

        // POST: Rezervacijas/CreateRacunala
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRacunala([Bind(Include = "DatumRezervacije,DanTerminiID,OpremaID,ProstorijaID")] Rezervacija rezervacija)
        {

            rezervacija.KorisnikID = User.Identity.GetUserId();
            rezervacija.VrijemeRezerviranja = DateTime.Now;
            rezervacija.RezervacijaAktivna = true;
            if (ModelState.IsValid)
            {
                db.Rezervacijas.Add(rezervacija);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ViewBag.KorisnikID = new SelectList(db.AspNetUsers, "Id", "Email", rezervacija.KorisnikID);
            ViewBag.DanTerminiID = new SelectList(db.DanTerminis, "ID", "ID", rezervacija.DanTerminiID);
            ViewBag.OpremaID = new SelectList(db.Opremas, "ID", "Naziv", rezervacija.OpremaID);
            ViewBag.ProstorijaID = new SelectList(db.Prostorijas, "ID", "Naziv");
            return View(rezervacija);
        }

        // GET: Rezervacijas/CreateStolovi
        public ActionResult CreateStolovi()
        {
            ViewBag.KorisnikID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.DanTerminiID = new SelectList(db.DanTerminis, "ID", "Termin");
            ViewBag.OpremaID = new SelectList(db.Opremas.Where(o => o.KategorijaOpremeID == 2), "ID", "Naziv");
            ViewBag.ProstorijaID = new SelectList(db.Prostorijas.Where(p => !p.Naziv.Contains("IMP")), "ID", "Naziv");

            return View();
        }

        // POST: Rezervacijas/CreateStolovi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStolovi([Bind(Include = "DatumRezervacije,DanTerminiID,OpremaID,ProstorijaID")] Rezervacija rezervacija)
        {

            rezervacija.KorisnikID = User.Identity.GetUserId();
            rezervacija.VrijemeRezerviranja = DateTime.Now;
            rezervacija.RezervacijaAktivna = true;
            if (ModelState.IsValid)
            {
                db.Rezervacijas.Add(rezervacija);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            ViewBag.KorisnikID = new SelectList(db.AspNetUsers, "Id", "Email", rezervacija.KorisnikID);
            ViewBag.DanTerminiID = new SelectList(db.DanTerminis, "ID", "ID", rezervacija.DanTerminiID);
            ViewBag.OpremaID = new SelectList(db.Opremas, "ID", "Naziv", rezervacija.OpremaID);
            return View(rezervacija);
        }

        // GET: Rezervacijas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacijas.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            ViewBag.KorisnikID = new SelectList(db.AspNetUsers, "Id", "Email", rezervacija.KorisnikID);
            ViewBag.DanTerminiID = new SelectList(db.DanTerminis, "ID", "ID", rezervacija.DanTerminiID);
            ViewBag.OpremaID = new SelectList(db.Opremas, "ID", "Naziv", rezervacija.OpremaID);
            return View(rezervacija);
        }

        // POST: Rezervacijas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,KorisnikID,DatumRezervacije,DanTerminiID,OpremaID,ProstorijaID,VrijemeRezerviranja,RezervacijaAktivna")] Rezervacija rezervacija)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rezervacija).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KorisnikID = new SelectList(db.AspNetUsers, "Id", "Email", rezervacija.KorisnikID);
            ViewBag.DanTerminiID = new SelectList(db.DanTerminis, "ID", "ID", rezervacija.DanTerminiID);
            ViewBag.OpremaID = new SelectList(db.Opremas, "ID", "Naziv", rezervacija.OpremaID);
            return View(rezervacija);
        }

        // GET: Rezervacijas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rezervacija rezervacija = db.Rezervacijas.Find(id);
            if (rezervacija == null)
            {
                return HttpNotFound();
            }
            return View(rezervacija);
        }

        // POST: Rezervacijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rezervacija rezervacija = db.Rezervacijas.Find(id);
            db.Rezervacijas.Remove(rezervacija);
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
