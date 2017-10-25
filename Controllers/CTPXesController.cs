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
    public class CTPXesController : Controller
    {
        private SachEntities db = new SachEntities();

        // GET: CTPXes
        public ActionResult Index()
        {
            var cTPXes = db.CTPX.Include(c => c.PHIEUXUAT).Include(c => c.SACH);
            return View(cTPXes.ToList());
        }

        // GET: CTPXes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPX cTPX = db.CTPX.Find(id);
            if (cTPX == null)
            {
                return HttpNotFound();
            }
            return View(cTPX);
        }

        // GET: CTPXes/Create
        public ActionResult Create()
        {
            ViewBag.MAPX = new SelectList(db.PHIEUXUAT, "MAPX", "MAPX");
            ViewBag.MASACH = new SelectList(db.SACH, "MASACH", "TENSACH");
            return View();
        }

        // POST: CTPXes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAPX,MASACH,SL,DGTB,DGB")] CTPX cTPX)
        {
            if (ModelState.IsValid)
            {
                db.CTPX.Add(cTPX);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MAPX = new SelectList(db.PHIEUXUAT, "MAPX", "MAPX", cTPX.MAPX);
            ViewBag.MASACH = new SelectList(db.SACH, "MASACH", "TENSACH", cTPX.MASACH);
            return View(cTPX);
        }

        // GET: CTPXes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPX cTPX = db.CTPX.Find(id);
            if (cTPX == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAPX = new SelectList(db.PHIEUXUAT, "MAPX", "MAPX", cTPX.MAPX);
            ViewBag.MASACH = new SelectList(db.SACH, "MASACH", "TENSACH", cTPX.MASACH);
            return View(cTPX);
        }

        // POST: CTPXes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPX,MASACH,SL,DGTB,DGB")] CTPX cTPX)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTPX).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MAPX = new SelectList(db.PHIEUXUAT, "MAPX", "MAPX", cTPX.MAPX);
            ViewBag.MASACH = new SelectList(db.SACH, "MASACH", "TENSACH", cTPX.MASACH);
            return View(cTPX);
        }

        // GET: CTPXes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTPX cTPX = db.CTPX.Find(id);
            if (cTPX == null)
            {
                return HttpNotFound();
            }
            return View(cTPX);
        }

        // POST: CTPXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CTPX cTPX = db.CTPX.Find(id);
            db.CTPX.Remove(cTPX);
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
