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
    public class NHAPController : Controller
    {
        private SachEntities db = new SachEntities();
        // GET: NHAP
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nhap = new NHAPModel();
            nhap.pHIEUNHAP = db.PHIEUNHAP.Include(s => s.NXB).Where(s => s.MANXB == id).FirstOrDefault();
            nhap.cTPNs = db.CTPN.Include(s=>s.SACH).Where(s => s.MAPN == id).ToList();
            if (nhap == null)
            {
                return HttpNotFound();
            }
            return View(nhap);
        }
        /*public ActionResult Create()
        {
            ViewBag.MASACH = new SelectList(db.SACH, "MASACH", "TENSACH");
            return View();
        }
        //POST: NHAP
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MAPN,NGAY,MANXB,NGUOIGIAO")] PHIEUNHAP pHIEUNHAP,[Bind(Include = "MAPN,MASACH,SL,DONGIA")] CTPN cTPN )
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Index");
            }

  
            return View();
        }
        */
    }
}