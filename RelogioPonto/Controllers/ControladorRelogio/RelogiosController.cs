using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RelogioPonto.Models;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace RelogioPonto.Controllers.ControladorRelogio
{
    public class RelogiosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Relogios
        public ActionResult Index()
        {
            return View(db.Relogios.ToList());
        }

        // GET: Relogios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relogio relogio = db.Relogios.Find(id);
            if (relogio == null)
            {
                return HttpNotFound();
            }
            return View(relogio);
        }

        // GET: Relogios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Relogios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Descricao,Login,Senha,Ip")] Relogio relogio)
        {
            if (ModelState.IsValid)
            {
                db.Relogios.Add(relogio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(relogio);
        }
		public async System.Threading.Tasks.Task Sincroniza()
		{
			string page = "http://www.aurodeandradefilho.com.br/procedimentos.php";

			using (HttpClient client = new HttpClient())
			using (HttpResponseMessage response = await client.GetAsync(page))
			using (HttpContent content = response.Content)
			{
				// ... Read the string.
				string result = await content.ReadAsStringAsync();
				Regex regex3 = new Regex(@"<h4 class=""text-center text-descricao"">(.*?)</h4>", RegexOptions.Singleline);
				Regex regex4 = new Regex(@"<img.*?src=""(.*?)"".*?>", RegexOptions.Singleline);
				Regex regex5 = new Regex(@"<div class=""item-procedimento box-pink""(.*?)/div>");
				var Matches= regex3.Matches(result);
				var Matche = regex4.Matches(result);
				var Match2 = regex5.Matches(result);

				IList<string> Cirurgias = new List<string>();
				foreach (Match m in Match2)
				{
					string div=m.Groups[0].ToString();
					var Matche3 = regex4.Match(div);
					Cirurgias.Add(Matche3.Groups[1].ToString());
				}
				int i;
				i = 1;


			}
		}

		public async System.Threading.Tasks.Task SincronizaTeste()
		{
			string page = "http://www.santacasasaocarlos.com.br/Noticias/Categorias/Banco_de_Leite";

			using (HttpClient client = new HttpClient())
			using (HttpResponseMessage response = await client.GetAsync(page))
			using (HttpContent content = response.Content)
			{
				// ... Read the string.
				string result = await content.ReadAsStringAsync();
				//Regex regex1 = new Regex(@"<h4 class=""text-center text-descricao"">(.*?)</h4>", RegexOptions.Singleline);
				Regex regex2 = new Regex(@"<a.*?href=""(.*?)"".*?>", RegexOptions.Singleline);
				//var Matches = regex1.Matches(result);
				var Matche = regex2.Matches(result);

				IList<string> Cirurgias = new List<string>();
				foreach (Match m in Matche)
				{
					var Matche3 = regex2.Match(result);
					Cirurgias.Add(Matche3.Groups[0].ToString());
				}
				int i;
				i = 1;


			}
		}


		// GET: Relogios/Edit/5
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relogio relogio = db.Relogios.Find(id);
            if (relogio == null)
            {
                return HttpNotFound();
            }
            return View(relogio);
        }

        // POST: Relogios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Descricao,Login,Senha,Ip")] Relogio relogio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relogio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(relogio);
        }

        // GET: Relogios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relogio relogio = db.Relogios.Find(id);
            if (relogio == null)
            {
                return HttpNotFound();
            }
            return View(relogio);
        }

        // POST: Relogios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Relogio relogio = db.Relogios.Find(id);
            db.Relogios.Remove(relogio);
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
