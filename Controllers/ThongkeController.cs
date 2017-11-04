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
        public ActionResult Index()
        {
            ViewBag.NXB = db.NXBs.ToList();
            ViewBag.DL = db.DAILies.ToList();
            return View();
        }
    }
}