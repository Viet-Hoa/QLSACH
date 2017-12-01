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
    public class CONGNOesController : Controller
    {
        private SachEntities db = new SachEntities();

        // GET: CONGNOes
        public ActionResult Index()
        {
            var cONGNOes = db.CONGNOes.Include(c => c.DAILY).Include(c => c.PHIEUXUAT);
            return View(cONGNOes.ToList());
        }

        // GET: CONGNOes/Details/5
        public ActionResult Details(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int dl = int.Parse(id.Substring(0, id.IndexOf("-")));
            int px = int.Parse(id.Substring(id.IndexOf("-")+1));
            CONGNO cONGNO = db.CONGNOes.Where(s=>s.MADL==dl &&s.MAPX==px).FirstOrDefault();
            if (cONGNO == null)
            {
                return HttpNotFound();
            }
            return View(cONGNO);
        }

        // GET: CONGNOes/Create
        public ActionResult Create()
        {
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL");
            ViewBag.MAPX = new SelectList(db.PHIEUXUATs, "MAPX", "MAPX");
            return View();
        }

        // POST: CONGNOes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MADL,MAPX,TIENNO,TIENTRA")] CONGNO cONGNO)
        {
            if (ModelState.IsValid)
            {
                db.CONGNOes.Add(cONGNO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", cONGNO.MADL);
            ViewBag.MAPX = new SelectList(db.PHIEUXUATs, "MAPX", "MAPX", cONGNO.MAPX);
            return View(cONGNO);
        }

        // GET: CONGNOes/Edit/5
        public ActionResult Edit(String id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int dl = int.Parse(id.Substring(0, id.IndexOf("-")));
            int px = int.Parse(id.Substring(id.IndexOf("-") + 1));
            CONGNO cONGNO = db.CONGNOes.Where(s => s.MADL == dl && s.MAPX == px).FirstOrDefault();
            if (cONGNO == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", cONGNO.MADL);
            ViewBag.MAPX = new SelectList(db.PHIEUXUATs, "MAPX", "MAPX", cONGNO.MAPX);
            return View(cONGNO);
        }

        // POST: CONGNOes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MADL,MAPX,TIENNO,TIENTRA")] CONGNO cONGNO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONGNO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", cONGNO.MADL);
            ViewBag.MAPX = new SelectList(db.PHIEUXUATs, "MAPX", "MAPX", cONGNO.MAPX);
            return View(cONGNO);
        }

        // GET: CONGNOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONGNO cONGNO = db.CONGNOes.Find(id);
            if (cONGNO == null)
            {
                return HttpNotFound();
            }
            return View(cONGNO);
        }

        // POST: CONGNOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CONGNO cONGNO = db.CONGNOes.Find(id);
            db.CONGNOes.Remove(cONGNO);
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
