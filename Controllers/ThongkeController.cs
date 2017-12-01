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
    public class ThongkeController : Controller
    {
        private SachEntities db = new SachEntities();
        // GET: Thongke
        [HttpGet]
        public ActionResult Index()
        {
            DateTime den = DateTime.Now.Date;
            DateTime tu = den.AddDays(-30);
            ViewBag.thu = db.PHIEUTHUs.Where(s => s.NGAY >= tu && s.NGAY <= den).ToList();
            ViewBag.chi = db.PHIEUCHIs.Where(s => s.NGAY >= tu && s.NGAY <= den).ToList();
            ViewBag.nodl = db.CONGNOes.Include(s => s.PHIEUXUAT).Include(s=>s.DAILY).Where(s => s.PHIEUXUAT.NGAY >= tu && s.PHIEUXUAT.NGAY <= den).ToList();
            var x = db.PHIEUNHAPs.Include(s => s.CTPNs).Where(s => s.NGAY >= tu && s.NGAY <= den).ToList();
            foreach (var pn in x)
            {
                int t = 0;
                foreach (var ct in pn.CTPNs)
                {
                    t += ct.SL * ct.SACH.DGM;                   
                }
                pn.NGUOIGIAO = t.ToString();
            }
            ViewBag.noxb = x;
            return View();            
        }
        [HttpPost]
        public ActionResult Index(DateTime tu, DateTime den)
        {
            ViewBag.thu = db.PHIEUTHUs.Where(s => s.NGAY >= tu && s.NGAY <= den).ToList();
            ViewBag.chi = db.PHIEUCHIs.Where(s => s.NGAY >= tu && s.NGAY <= den).ToList();
            ViewBag.nodl = db.CONGNOes.Include(s => s.PHIEUXUAT).Include(s => s.DAILY).Where(s => s.PHIEUXUAT.NGAY >= tu && s.PHIEUXUAT.NGAY <= den).ToList();
            var x = db.PHIEUNHAPs.Include(s => s.CTPNs).Where(s => s.NGAY >= tu && s.NGAY <= den).ToList();
            foreach (var pn in x)
            {
                int t = 0;
                foreach (var ct in pn.CTPNs)
                {
                    t += ct.SL * ct.SACH.DGM;
                }
                pn.NGUOIGIAO = t.ToString();
            }
            ViewBag.noxb = x;
            return View();
        }
        
        
    }
}