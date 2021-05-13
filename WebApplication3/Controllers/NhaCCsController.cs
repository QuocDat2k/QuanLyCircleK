using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLy_CircleK.Models;

namespace QuanLy_CircleK.Controllers
{
    public class NhaCCsController : Controller
    {
        private NNTDbContext db = new NNTDbContext();

        // GET: NhaCCs
        public ActionResult Index()
        {
            return View(db.NhaCCs.Where(x=>!x.Xoa).ToList());
        }

        // GET: NhaCCs/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCC nhaCC = db.NhaCCs.FirstOrDefault(x=>x.Ma == id && !x.Xoa);
            if (nhaCC == null)
            {
                return HttpNotFound();
            }
            return View(nhaCC);
        }

        // GET: NhaCCs/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: NhaCCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNCC,TenNCC,SoDienThoai")] NhaCC nhaCC)
        {
            if (ModelState.IsValid)
            {
                var ncc = new NhaCC();
                ncc.TenNCC = nhaCC.TenNCC;
                ncc.SoDienThoai = nhaCC.SoDienThoai;

                db.NhaCCs.Add(nhaCC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhaCC);
        }

        // GET: NhaCCs/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCC nhaCC = db.NhaCCs.FirstOrDefault(x => x.Ma == id && !x.Xoa);
            if (nhaCC == null)
            {
                return HttpNotFound();
            }
            return View(nhaCC);
        }

        // POST: NhaCCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ma,TenNCC,SoDienThoai")] NhaCC nhaCC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhaCC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhaCC);
        }

        // GET: NhaCCs/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaCC nhaCC = db.NhaCCs.FirstOrDefault(x => x.Ma == id && !x.Xoa);
            if (nhaCC == null)
            {
                return HttpNotFound();
            }
            return View(nhaCC);
        }

        // POST: NhaCCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhaCC nhaCC = db.NhaCCs.FirstOrDefault(x => x.Ma == id && !x.Xoa);
            nhaCC.Xoa = true;
            db.Entry(nhaCC).State = EntityState.Modified;
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
