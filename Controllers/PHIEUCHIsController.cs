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
    public class PHIEUCHIsController : Controller
    {
        private SachEntities db = new SachEntities();

        // GET: PHIEUCHIs
        public ActionResult Index()
        {
            var pHIEUCHIs = db.PHIEUCHIs.Include(p => p.NXB);
            return View(pHIEUCHIs.ToList());
        }

        // GET: PHIEUCHIs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUCHI pHIEUCHI = db.PHIEUCHIs.Find(id);
            if (pHIEUCHI == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUCHI);
        }

        // GET: PHIEUCHIs/Create
        public ActionResult Create()
        {
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB");
            return View();
        }

        // POST: PHIEUCHIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NGAY,MANXB,TIEN,NOIDUNG")] PHIEUCHI pHIEUCHI)
        {
            if (ModelState.IsValid)
            {
                var n = db.NXBs.Find(pHIEUCHI.MANXB);
                db.PHIEUCHIs.Add(pHIEUCHI);
                db.SaveChanges();               
                n.TIENNO = n.TIENNO - pHIEUCHI.TIEN;
                db.Entry(n).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", pHIEUCHI.MANXB);
            return View(pHIEUCHI);
        }

        // GET: PHIEUCHIs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUCHI pHIEUCHI = db.PHIEUCHIs.Find(id);
            if (pHIEUCHI == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", pHIEUCHI.MANXB);
            return View(pHIEUCHI);
        }

        // POST: PHIEUCHIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NGAY,MANXB,TIEN,NOIDUNG")] PHIEUCHI pHIEUCHI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUCHI).State = EntityState.Modified;
                db.SaveChanges();
 
                
                return RedirectToAction("Index");
            }
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", pHIEUCHI.MANXB);
            return View(pHIEUCHI);
        }

        // GET: PHIEUCHIs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUCHI pHIEUCHI = db.PHIEUCHIs.Find(id);
            if (pHIEUCHI == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUCHI);
        }

        // POST: PHIEUCHIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEUCHI pHIEUCHI = db.PHIEUCHIs.Find(id);
            NXB n = db.NXBs.Find(pHIEUCHI.MANXB);
            n.TIENNO = n.TIENNO + pHIEUCHI.TIEN;
            db.Entry(n).State = EntityState.Modified;
            db.SaveChanges();
            db.PHIEUCHIs.Remove(pHIEUCHI);
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
