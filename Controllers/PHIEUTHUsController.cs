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
    public class PHIEUTHUsController : Controller
    {
        private SachEntities db = new SachEntities();

        // GET: PHIEUTHUs
        public ActionResult Index()
        {
            var pHIEUTHUs = db.PHIEUTHUs.Include(p => p.DAILY);
            return View(pHIEUTHUs.ToList());
        }

        // GET: PHIEUTHUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTHU pHIEUTHU = db.PHIEUTHUs.Find(id);
            if (pHIEUTHU == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUTHU);
        }

        // GET: PHIEUTHUs/Create
        public ActionResult Create()
        {
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL");
            return View();
        }

        // POST: PHIEUTHUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NGAY,MADL,TIEN,NOIDUNG")] PHIEUTHU pHIEUTHU)
        {
            if (ModelState.IsValid)
            {
                db.PHIEUTHUs.Add(pHIEUTHU);
                db.SaveChanges();
                DAILY d = db.DAILies.Find(pHIEUTHU.MADL);
                d.TONGNO = d.TONGNO - pHIEUTHU.TIEN;
                db.Entry(d).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", pHIEUTHU.MADL);
            return View(pHIEUTHU);
        }

        // GET: PHIEUTHUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTHU pHIEUTHU = db.PHIEUTHUs.Find(id);
            if (pHIEUTHU == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", pHIEUTHU.MADL);
            return View(pHIEUTHU);
        }

        // POST: PHIEUTHUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NGAY,MADL,TIEN,NOIDUNG")] PHIEUTHU pHIEUTHU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUTHU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", pHIEUTHU.MADL);
            return View(pHIEUTHU);
        }

        // GET: PHIEUTHUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTHU pHIEUTHU = db.PHIEUTHUs.Find(id);
            if (pHIEUTHU == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUTHU);
        }

        // POST: PHIEUTHUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEUTHU pHIEUTHU = db.PHIEUTHUs.Find(id);
            DAILY d = db.DAILies.Find(pHIEUTHU.MADL);
            d.TONGNO = d.TONGNO + pHIEUTHU.TIEN;
            db.Entry(d).State = EntityState.Modified;
            db.SaveChanges();
            db.PHIEUTHUs.Remove(pHIEUTHU);
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
