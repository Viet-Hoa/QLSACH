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
    public class PHIEUNHAPsController : Controller
    {
        private SachEntities db = new SachEntities();

        // GET: PHIEUNHAPs
        public ActionResult Index()
        {
            var pHIEUNHAP = db.PHIEUNHAP.Include(p => p.NXB);
            return View(pHIEUNHAP.ToList());
        }

        // GET: PHIEUNHAPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nhap = new NHAPModel();
            nhap.pHIEUNHAP = db.PHIEUNHAP.Include(s => s.NXB).Where(s => s.MANXB == id).FirstOrDefault();
            nhap.cTPNs = db.CTPN.Include(s => s.SACH).Where(s => s.MAPN == id).ToList();
            if (nhap == null)
            {
                return HttpNotFound();
            }
            return View(nhap);
        }

        // GET: PHIEUNHAPs/Create
        public ActionResult Create()
        {
            var md = new NHAPModel();
            md.cTPNs.Add(new CTPN());
            ViewBag.MANXB = new SelectList(db.NXB, "MANXB", "TENNXB");
            ViewBag.MASACH = new SelectList(db.SACH, "MASACH", "TENSACH");
            return View(md);
        }
        
        public ActionResult AddCTPN()
        {
            var md = new NHAPModel();
            md.cTPNs.Add(new CTPN());
            return View(md);
        }
        // POST: PHIEUNHAPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] NHAPModel nhap)
        {
            if (ModelState.IsValid)
            {
                db.PHIEUNHAP.Add(nhap.pHIEUNHAP);
                db.SaveChanges();
                int ma = db.PHIEUNHAP.Select(x => x.MAPN).LastOrDefault();
                int t = 0;
                foreach(CTPN u in nhap.cTPNs)
                {
                    u.MAPN = ma;
                    db.CTPN.Add(u);
                    db.SaveChanges();
                    t = t + u.SL * u.DONGIA;
                    SACH a = db.SACH.Find(u.MASACH);
                    a.SL = a.SL + u.SL;
                    a.GIA = u.DONGIA;
                    db.Entry(a).State = EntityState.Modified;
                    db.SaveChanges();
                }
                NXB n = db.NXB.Find(nhap.pHIEUNHAP.MANXB);
                n.TIENNO = n.TIENNO + t;
                db.Entry(n).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MANXB = new SelectList(db.NXB, "MANXB", "TENNXB", nhap.pHIEUNHAP.MANXB);
            ViewBag.MASACH = new SelectList(db.SACH, "MASACH", "TENSACH",nhap.cTPNs.FirstOrDefault().MASACH);
            return View(nhap);
        }

        // GET: PHIEUNHAPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAP pHIEUNHAP = db.PHIEUNHAP.Find(id);
            if (pHIEUNHAP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANXB = new SelectList(db.NXB, "MANXB", "TENNXB", pHIEUNHAP.MANXB);
            return View(pHIEUNHAP);
        }

        // POST: PHIEUNHAPs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAPN,NGAY,MANXB,NGUOIGIAO")] PHIEUNHAP pHIEUNHAP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUNHAP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MANXB = new SelectList(db.NXB, "MANXB", "TENNXB", pHIEUNHAP.MANXB);
            return View(pHIEUNHAP);
        }

        // GET: PHIEUNHAPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAP pHIEUNHAP = db.PHIEUNHAP.Find(id);
            if (pHIEUNHAP == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUNHAP);
        }

        // POST: PHIEUNHAPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEUNHAP pHIEUNHAP = db.PHIEUNHAP.Find(id);
            db.PHIEUNHAP.Remove(pHIEUNHAP);
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
