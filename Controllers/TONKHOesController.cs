using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLsach.Models;

namespace QLsach.Controllers
{
    public class TONKHOesController : Controller
    {
        private SachEntities db = new SachEntities();

        // GET: TONKHOes
        public ActionResult Index()
        {
            List<DateTime> tONKHOes = db.TONKHOes.Select(s=>s.NGAY).Distinct().ToList();
            return View(tONKHOes.OrderByDescending(x=>x).ToList());
        }

        // GET: TONKHOes/Details/5
        public ActionResult Details(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateTime date = DateTime.Parse(id);
            var tONKHO = db.TONKHOes.Include(s=>s.SACH).Where(s=>s.NGAY==date).ToList();
            if (tONKHO == null)
            {
                return HttpNotFound();
            }
            return View(tONKHO);
        }

        // POST: TONKHOes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        public ActionResult Create()
        {
            DateTime ngay = db.TONKHOes.Max(s => s.NGAY);
            var tk = db.TONKHOes.Include(s => s.SACH).Where(s => s.NGAY == ngay).ToList();
            var sach = db.SACHes.ToList();
            foreach(TONKHO t in tk)
            {
                t.NGAY = DateTime.Now.Date;
                foreach(SACH s in sach)
                {
                    if (s.MASACH == t.MASACH)
                        t.SL = s.SL;
                }
            }
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH");
            return View(tk);
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "NGAY,MASACH,SL")] List<TONKHO> tk)
        {
            try
            {
                foreach (TONKHO u in tk)
                {
                    u.NGAY = DateTime.Now.Date;
                    db.TONKHOes.Add(u);
                    db.SaveChanges();
                }
            }
            catch(DbEntityValidationException e){}
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH");
            return RedirectToAction("Index");
        }

        // GET: TONKHOes/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TONKHO tONKHO = db.TONKHOes.Find(id);
            if (tONKHO == null)
            {
                return HttpNotFound();
            }
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH", tONKHO.MASACH);
            return View(tONKHO);
        }

        // POST: TONKHOes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NGAY,MASACH,SL")] TONKHO tONKHO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tONKHO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH", tONKHO.MASACH);
            return View(tONKHO);
        }

        // GET: TONKHOes/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TONKHO tONKHO = db.TONKHOes.Find(id);
            if (tONKHO == null)
            {
                return HttpNotFound();
            }
            return View(tONKHO);
        }

        // POST: TONKHOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            TONKHO tONKHO = db.TONKHOes.Find(id);
            db.TONKHOes.Remove(tONKHO);
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
