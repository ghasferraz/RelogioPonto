using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RelogioPonto.Models;

namespace RelogioPonto.Controllers.ControladorRelogio
{
    public class Relogios1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Relogios1
        public async Task<ActionResult> Index()
        {
            return View(await db.Relogios.ToListAsync());
        }

        // GET: Relogios1/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relogio relogio = await db.Relogios.FindAsync(id);
            if (relogio == null)
            {
                return HttpNotFound();
            }
            return View(relogio);
        }

        // GET: Relogios1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Relogios1/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,Descricao,Status,Login,Senha,Ip")] Relogio relogio)
        {
            if (ModelState.IsValid)
            {
                db.Relogios.Add(relogio);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(relogio);
        }

        // GET: Relogios1/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relogio relogio = await db.Relogios.FindAsync(id);
            if (relogio == null)
            {
                return HttpNotFound();
            }
            return View(relogio);
        }

        // POST: Relogios1/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,Descricao,Status,Login,Senha,Ip")] Relogio relogio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relogio).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(relogio);
        }

        // GET: Relogios1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relogio relogio = await db.Relogios.FindAsync(id);
            if (relogio == null)
            {
                return HttpNotFound();
            }
            return View(relogio);
        }

        // POST: Relogios1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Relogio relogio = await db.Relogios.FindAsync(id);
            db.Relogios.Remove(relogio);
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
