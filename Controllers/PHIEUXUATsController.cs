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
    public class PHIEUXUATsController : Controller
    {
        private SachEntities db = new SachEntities();

        // GET: PHIEUXUATs
        public ActionResult Index()
        {
            var pHIEUXUATs = db.PHIEUXUATs.Include(p => p.DAILY);
            return View(pHIEUXUATs.ToList());
        }

        // GET: PHIEUXUATs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var xuat = new XUATModel();
            xuat.pHIEUXUAT = db.PHIEUXUATs.Include(s => s.DAILY).Where(s => s.MAPX == id).FirstOrDefault();
            xuat.cTPXes = db.CTPXes.Include(s => s.SACH).Where(s => s.MAPX == id).ToList();
            if (xuat == null)
            {
                return HttpNotFound();
            }
            return View(xuat);
        }

        // GET: PHIEUXUATs/Create
        public ActionResult Create()
        {
            ViewBag.MANXB = new SelectList(db.DAILies, "MADL", "TENDL");
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH");
            return View();
        }

        // POST: PHIEUNHAPs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Prefix="px")] PHIEUXUAT phieuxuat,
                                   [Bind(Prefix="ctpx")] CTPX[] ctpx)
        {
            ViewBag.mess = null;
            try
            {
                if (ModelState.IsValid)
                {
                    db.PHIEUXUATs.Add(phieuxuat);
                    db.SaveChanges();
                    int ma = db.PHIEUNHAPs.Select(x => x.MAPN).LastOrDefault();
                    int t = 0;
                    foreach (CTPX u in ctpx)
                    {
                        SACH a = db.SACHes.Find(u.MASACH);
                        if (u.SL > a.SL)
                        {
                            ViewBag.mess = "Quá số lượng";
                            return View();
                        }
                        t = t + u.SL * a.DGB;
                    }
                    DAILY n = db.DAILies.Find(phieuxuat.MADL);
                    n.TONGNO = n.TONGNO + t;
                    var mdn = db.MACDINHs.FirstOrDefault();
                    if (n.TONGNO > int.Parse(mdn.GIATRI))
                    {
                        ViewBag.mess = "Đại lý nợ quá " + mdn.GIATRI;
                        return View();
                    }
                    db.Entry(n).State = EntityState.Modified;
                    db.SaveChanges();
                    foreach (CTPX u in ctpx)
                    {
                        if (u != null)
                        {
                            u.MAPX = ma;
                            db.CTPXes.Add(u);
                            db.SaveChanges();
                            SACH a = db.SACHes.Find(u.MASACH);
                            a.SL = a.SL - u.SL;
                            db.Entry(a).State = EntityState.Modified;
                            db.SaveChanges();
                            SACHBANDC s = new SACHBANDC();
                            s.MAPX = phieuxuat.MAPX;
                            s.MADL = phieuxuat.MADL;
                            s.SL = u.SL;
                            s.BANDC = 0;
                            db.SACHBANDCs.Add(s);
                            db.SaveChanges();
                        }
                    }
                    CONGNO c = new CONGNO();
                    c.MAPX = phieuxuat.MAPX;
                    c.MADL = phieuxuat.MADL;
                    c.TIENNO = t;
                    c.TIENTRA = 0;
                    db.CONGNOes.Add(c);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            
            }
            catch (DbEntityValidationException e)
            {

            }
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", phieuxuat.MADL);
            ViewBag.MASACH = new SelectList(db.SACHes, "MASACH", "TENSACH");
            phieuxuat.CTPXes = ctpx;
            createxuat pnvm = new createxuat();
            pnvm.px = phieuxuat;
            return View(pnvm);
        }

        // GET: PHIEUXUATs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUXUAT pHIEUXUAT = db.PHIEUXUATs.Find(id);
            if (pHIEUXUAT == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", pHIEUXUAT.MADL);
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
            ViewBag.MADL = new SelectList(db.DAILies, "MADL", "TENDL", pHIEUXUAT.MADL);
            return View(pHIEUXUAT);
        }

        // GET: PHIEUXUATs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUXUAT pHIEUXUAT = db.PHIEUXUATs.Find(id);
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
            PHIEUXUAT pHIEUXUAT = db.PHIEUXUATs.Find(id);
            db.PHIEUXUATs.Remove(pHIEUXUAT);
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
