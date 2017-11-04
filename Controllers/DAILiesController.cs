using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLsach.Models;

namespace QLsach.Controllers
{
    public class DAILiesController : Controller
    {
        private SachEntities db = new SachEntities();

        // GET: DAILies
        public async Task<ActionResult> Index()
        {
            return View(await db.DAILies.ToListAsync());
        }

        // GET: DAILies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAILY dAILY = await db.DAILies.FindAsync(id);
            if (dAILY == null)
            {
                return HttpNotFound();
            }
            return View(dAILY);
        }

        // GET: DAILies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DAILies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MADL,TENDL,DIACHI,DIENTHOAI,TONGNO")] DAILY dAILY)
        {
            if (ModelState.IsValid)
            {
                db.DAILies.Add(dAILY);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(dAILY);
        }

        // GET: DAILies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAILY dAILY = await db.DAILies.FindAsync(id);
            if (dAILY == null)
            {
                return HttpNotFound();
            }
            return View(dAILY);
        }

        // POST: DAILies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MADL,TENDL,DIACHI,DIENTHOAI,TONGNO")] DAILY dAILY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dAILY).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dAILY);
        }

        // GET: DAILies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAILY dAILY = await db.DAILies.FindAsync(id);
            if (dAILY == null)
            {
                return HttpNotFound();
            }
            return View(dAILY);
        }

        // POST: DAILies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DAILY dAILY = await db.DAILies.FindAsync(id);
            db.DAILies.Remove(dAILY);
            await db.SaveChangesAsync();
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