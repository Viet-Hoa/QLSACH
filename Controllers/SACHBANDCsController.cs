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
    public class SACHBANDCsController : Controller
    {
        private SachEntities db = new SachEntities();

        // GET: SACHBANDCs
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(DAILY d)
        {
            ViewBag.mess = null;
            var dl = db.DAILies.Where(s => s.TENDL == d.TENDL && s.DIENTHOAI == d.DIENTHOAI).FirstOrDefault();
            if (dl != null)
                return RedirectToAction("List", new { id = dl.MADL });
            else
                ViewBag.mess = "Nhập sai thông tin!";
            return View();
        }
        public ActionResult List(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sACHBANDCs = db.SACHBANDCs.Include(s => s.PHIEUXUAT).Include(s => s.SACH).Where(s=>s.MADL==id).ToList();
            if (!sACHBANDCs.Any())
            {
                return HttpNotFound();
            }
            return View(sACHBANDCs);
        }
        public ActionResult Index()
        {
            var sACHBANDCs = db.SACHBANDCs.Include(s=>s.DAILY).Include(s => s.PHIEUXUAT).Include(s => s.SACH);
            return View(sACHBANDCs.ToList());
        }
        // GET: SACHBANDCs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACHBANDC sACHBANDC = db.SACHBANDCs.Find(id);
            if (sACHBANDC == null)
            {
                return HttpNotFound();
            }
            return View(sACHBANDC);
        }

        // GET: SACHBANDCs/Create
        public ActionResult Create()
        {
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL");
            ViewBag.MAPX = new SelectList(db.PHIEUXUATs, "MAPX", "MAPX");
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH");
            return View();
        }

        // POST: SACHBANDCs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MAPX,MASACH,MADL,SL,BANDC")] SACHBANDC sACHBANDC)
        {
            if (ModelState.IsValid)
            {
                db.SACHBANDCs.Add(sACHBANDC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", sACHBANDC.MADL);
            ViewBag.MAPX = new SelectList(db.PHIEUXUATs, "MAPX", "MAPX", sACHBANDC.MAPX);
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH", sACHBANDC.MASACH);
            return View(sACHBANDC);
        }

        // GET: SACHBANDCs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACHBANDC sACHBANDC = db.SACHBANDCs.Find(id);
            if (sACHBANDC == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", sACHBANDC.MADL);
            ViewBag.MAPX = new SelectList(db.PHIEUXUATs, "MAPX", "MAPX", sACHBANDC.MAPX);
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH", sACHBANDC.MASACH);
            return View(sACHBANDC);
        }

        // POST: SACHBANDCs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MAPX,MASACH,MADL,SL,BANDC")] SACHBANDC sACHBANDC)
        {
            if (ModelState.IsValid)
            {
                var x = db.SACHBANDCs.Find(sACHBANDC.ID);
                x.BANDC = sACHBANDC.BANDC;
                db.Entry(x).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", sACHBANDC.MADL);
            ViewBag.MAPX = new SelectList(db.PHIEUXUATs, "MAPX", "MAPX", sACHBANDC.MAPX);
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH", sACHBANDC.MASACH);
            return View(sACHBANDC);
        }

        // GET: SACHBANDCs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACHBANDC sACHBANDC = db.SACHBANDCs.Find(id);
            if (sACHBANDC == null)
            {
                return HttpNotFound();
            }
            return View(sACHBANDC);
        }

        // POST: SACHBANDCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SACHBANDC sACHBANDC = db.SACHBANDCs.Find(id);
            db.SACHBANDCs.Remove(sACHBANDC);
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
