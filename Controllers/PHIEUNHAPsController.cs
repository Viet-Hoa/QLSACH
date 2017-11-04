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
            var pHIEUNHAP = db.PHIEUNHAPs.Include(p => p.NXB);
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
            nhap.pHIEUNHAP = db.PHIEUNHAPs.Include(s => s.NXB).Where(s => s.MAPN == id).FirstOrDefault();
            nhap.cTPNs = db.CTPNs.Include(s => s.SACH).Where(s => s.MAPN == id).ToList();
            if (nhap == null)
            {
                return HttpNotFound();
            }
            return View(nhap);
        }

        // GET: PHIEUNHAPs/Create
        public ActionResult Create()
        {
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB");
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH");
            return View();
        }
        
        // POST: PHIEUNHAPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAPN,NGAY,MANXB,NGUOIGIAO")] PHIEUNHAP phieunhap, 
                                   [Bind(Include = "MAPN,MASACH,SL")] CTPN[] ctpn)
        {
            if (ModelState.IsValid)
            {
                db.PHIEUNHAPs.Add(phieunhap);
                db.SaveChanges();
                int ma = db.PHIEUNHAPs.Select(x => x.MAPN).LastOrDefault();
                int t = 0;
                foreach(CTPN u in ctpn)
                {
                    if (u != null)
                    {
                        u.MAPN = ma;
                        db.CTPNs.Add(u);
                        db.SaveChanges();
                        SACH a = db.SACHes.Find(u.MASACH);
                        a.SL = a.SL + u.SL;
                        t = t + u.SL * a.DGM;
                        db.Entry(a).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                NXB n = db.NXBs.Find(phieunhap.MANXB);
                n.TIENNO = n.TIENNO + t;
                db.Entry(n).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", phieunhap.MANXB);
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH");
            phieunhap.CTPNs= ctpn;
            createnhap pnvm = new createnhap();
            pnvm.pn = phieunhap;
            return View(pnvm);
        }

        // GET: PHIEUNHAPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAP pHIEUNHAP = db.PHIEUNHAPs.Find(id);
            if (pHIEUNHAP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", pHIEUNHAP.MANXB);
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
            ViewBag.MANXB = new SelectList(db.NXBs, "MANXB", "TENNXB", pHIEUNHAP.MANXB);
            return View(pHIEUNHAP);
        }

        // GET: PHIEUNHAPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUNHAP pHIEUNHAP = db.PHIEUNHAPs.Find(id);
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
            PHIEUNHAP pHIEUNHAP = db.PHIEUNHAPs.Find(id);
            db.PHIEUNHAPs.Remove(pHIEUNHAP);
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
