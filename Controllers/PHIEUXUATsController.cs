using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLsach.Models;

namespace QLsach.Controllers
{
    public class PHIEUXUATsController : Controller
    {
        private SachEntities db = new SachEntities();

        // GET: PHIEUXUATs
        public ActionResult Index()
        {
            var pHIEUXUATs = db.PHIEUXUAT.Include(p => p.DAILY);
            return View(pHIEUXUATs.ToList());
        }

        // GET: PHIEUXUATs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUXUAT pHIEUXUAT = db.PHIEUXUAT.Find(id);
            if (pHIEUXUAT == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUXUAT);
        }

        // GET: PHIEUXUATs/Create
        public ActionResult Create()
        {
            ViewBag.MADL = new SelectList(db.DAILY, "MADL", "TENDL");
            return View();
        }

        // POST: PHIEUXUATs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAPX,NGAY,MADL,TINHTRANG")] PHIEUXUAT pHIEUXUAT)
        {
            if (ModelState.IsValid)
            {
                db.PHIEUXUAT.Add(pHIEUXUAT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MADL = new SelectList(db.DAILY, "MADL", "TENDL", pHIEUXUAT.MADL);
            return View(pHIEUXUAT);
        }

        // GET: PHIEUXUATs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUXUAT pHIEUXUAT = db.PHIEUXUAT.Find(id);
            if (pHIEUXUAT == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADL = new SelectList(db.DAILY, "MADL", "TENDL", pHIEUXUAT.MADL);
            return View(pHIEUXUAT);
        }

        // POST: PHIEUXUATs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPX,NGAY,MADL,TINHTRANG")] PHIEUXUAT pHIEUXUAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUXUAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MADL = new SelectList(db.DAILY, "MADL", "TENDL", pHIEUXUAT.MADL);
            return View(pHIEUXUAT);
        }

        // GET: PHIEUXUATs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUXUAT pHIEUXUAT = db.PHIEUXUAT.Find(id);
            if (pHIEUXUAT == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUXUAT);
        }

        // POST: PHIEUXUATs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEUXUAT pHIEUXUAT = db.PHIEUXUAT.Find(id);
            db.PHIEUXUAT.Remove(pHIEUXUAT);
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
