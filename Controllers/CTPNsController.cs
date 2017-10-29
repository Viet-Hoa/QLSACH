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
    public class CTPNsController : Controller
    {
        private SachEntities db = new SachEntities();

        // GET: CTPNs
        public ActionResult Index()
        {
            var cTPN = db.CTPN.Include(c => c.PHIEUNHAP).Include(c => c.SACH);
            return View(cTPN.ToList());
        }

        // GET: CTPNs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPN cTPN = db.CTPN.Find(id);
            if (cTPN == null)
            {
                return HttpNotFound();
            }
            return View(cTPN);
        }

        // GET: CTPNs/Create
        public ActionResult Create()
        {
            ViewBag.MAPN = new SelectList(db.PHIEUNHAP, "MAPN", "NGUOIGIAO");
            ViewBag.MASACH = new SelectList(db.SACH, "MASACH", "TENSACH");
            return View();
        }

        // POST: CTPNs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAPN,MASACH,SL,DONGIA")] CTPN cTPN)
        {
            if (ModelState.IsValid)
            {
                db.CTPN.Add(cTPN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MAPN = new SelectList(db.PHIEUNHAP, "MAPN", "NGUOIGIAO", cTPN.MAPN);
            ViewBag.MASACH = new SelectList(db.SACH, "MASACH", "TENSACH", cTPN.MASACH);
            return View(cTPN);
        }

        // GET: CTPNs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPN cTPN = db.CTPN.Find(id);
            if (cTPN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAPN = new SelectList(db.PHIEUNHAP, "MAPN", "NGUOIGIAO", cTPN.MAPN);
            ViewBag.MASACH = new SelectList(db.SACH, "MASACH", "TENSACH", cTPN.MASACH);
            return View(cTPN);
        }

        // POST: CTPNs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPN,MASACH,SL,DONGIA")] CTPN cTPN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTPN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MAPN = new SelectList(db.PHIEUNHAP, "MAPN", "NGUOIGIAO", cTPN.MAPN);
            ViewBag.MASACH = new SelectList(db.SACH, "MASACH", "TENSACH", cTPN.MASACH);
            return View(cTPN);
        }

        // GET: CTPNs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPN cTPN = db.CTPN.Find(id);
            if (cTPN == null)
            {
                return HttpNotFound();
            }
            return View(cTPN);
        }

        // POST: CTPNs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CTPN cTPN = db.CTPN.Find(id);
            db.CTPN.Remove(cTPN);
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
