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
using System.Threading.Tasks;

namespace RelogioPonto.Controllers.RelogioControlador
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
		public ActionResult Create([Bind(Include = "Id,Nome,Descricao,Status,Login,Senha,Ip")] Relogio relogio)
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
				var Matches = regex3.Matches(result);
				var Matche = regex4.Matches(result);
				var Match2 = regex5.Matches(result);

				IList<string> Cirurgias = new List<string>();
				foreach (Match m in Match2)
				{
					string div = m.Groups[0].ToString();
					var Matche3 = regex4.Match(div);
					Cirurgias.Add(Matche3.Groups[1].ToString());
				}
				int i;
				i = 1;


			}
		}
		public async System.Threading.Tasks.Task SincronizaTeste()
		{
			string page = "http://www.santacasasaocarlos.com.br/Noticias/Categorias/Todas";
			IList<string> Links = new List<string>();
			using (HttpClient client = new HttpClient())
			using (HttpResponseMessage response = await client.GetAsync(page))
			using (HttpContent content = response.Content)
			{
				// ... Read the string.
				string result = await content.ReadAsStringAsync();
				//Regex regex1 = new Regex(@"<h4 class=""text-center text-descricao"">(.*?)</h4>", RegexOptions.Singleline);
				Regex regexTaga = new Regex(@"<a.*?class=""btn col-xs-12"".*?href=""(.*?)""", RegexOptions.Singleline);
				//var Matches = regex1.Matches(result);
				var Matche = regexTaga.Matches(result);


				foreach (Match m in Matche)
				{
					var Matche3 = regexTaga.Match(result);
					Links.Add(m.Groups[1].ToString());
				}
				int i;
				i = 1;


			}
			foreach (string l in Links)
			{
				using (HttpClient client = new HttpClient())
				using (HttpResponseMessage response = await client.GetAsync("http://www.santacasasaocarlos.com.br" + l))
				using (HttpContent content = response.Content)
				{
					// ... Read the string.
					string result = await content.ReadAsStringAsync();

				}
			}

		}
		public async Task<string> Relogio()
		{
			CookieContainer cookies = new CookieContainer();
			HttpClientHandler handler = new HttpClientHandler
			{
				AutomaticDecompression = DecompressionMethods.GZip
									 | DecompressionMethods.Deflate

			};
			handler.CookieContainer = cookies;

			HttpClient client = new HttpClient(handler);
			client.MaxResponseContentBufferSize = 256000;

			var uri = new Uri(string.Format("http://192.168.22.208/" + @"logme", string.Empty));
			var content = new List<KeyValuePair<string, string>>();
			content.Add(new KeyValuePair<string, string>("username", "ADMIN"));
			content.Add(new KeyValuePair<string, string>("password", "180516"));
			try
			{
				var response = await client.PostAsync(uri, new FormUrlEncodedContent(content));
				if (response.IsSuccessStatusCode)
				{
					IEnumerable<Cookie> responseCookies = cookies.GetCookies(uri).Cast<Cookie>();
					foreach (Cookie cookie in responseCookies)
					{
						//Console.WriteLine(cookie.Name + ": " + cookie.Value);
						var c = cookie;
						cookies.Add(cookie);


					}

					//	var result = await response.Content.ReadAsStringAsync();
					uri = new Uri(string.Format("http://192.168.22.208/" + @"info?_=1506617975243", string.Empty));

					var response2 = await client.GetAsync(uri);



					if (response2.IsSuccessStatusCode)
					{
						var result2 = await response2.Content.ReadAsStringAsync();
						string a;
						a = "teste";
					}
				}

			}

			catch (Exception ex)
			{
				throw ex;
			}
			return "a";


		}

		public async Task<ActionResult> VerificaStatusImpressora()
		{

			var status = buscarStatus();
			foreach (var s in status)
			{
				var r = db.Relogios.Where(m => m.Ip == s.IP).FirstOrDefault();

				if (r == null)
				{
					return HttpNotFound();
				}

				if (s.status == "Cheio")
				{
					r.Status = Models.Relogio.StatusPapel.Cheio;
				}

				db.Entry(r).State = EntityState.Modified;

				if (s.status == "Alto")
				{
					r.Status = Models.Relogio.StatusPapel.Alto;
				}

				db.Entry(r).State = EntityState.Modified;

				if (s.status == "Regular")
				{
					r.Status = Models.Relogio.StatusPapel.Regular;
				}

				db.Entry(r).State = EntityState.Modified;

				if (s.status == "Baixo")
				{
					r.Status = Models.Relogio.StatusPapel.Baixo;
				}

				db.Entry(r).State = EntityState.Modified;


			}
			db.SaveChanges();
			return View();
		}

		public List<RelogioStatus> buscarStatus()
		{
			/*string page = "http://192.168.22.208/InnerRepPlus.html";
			IList<string> Links = new List<string>();
			using (HttpClient client = new HttpClient())
			using (HttpResponseMessage response = await client.GetAsync(page))
			using (HttpContent content = response.Content)
			{
				// ... Read the string.
				string result = await content.ReadAsStringAsync();
				//Regex regex1 = new Regex(@"<h4 class=""text-center text-descricao"">(.*?)</h4>", RegexOptions.Singleline);
				Regex regexTagImg = new Regex(@"<img id=""statusImpressora""style=""border-radius: 3px 3px 3px 3px""src=""pronta2.jpg""", RegexOptions.Singleline);
				//var Matches = regex1.Matches(result);
				var Matche = regexTagImg.Matches(result);


				foreach (Match m in Matche)
				{
					var Matche3 = regexTagImg.Match(result);
					Links.Add(m.Groups[0].ToString());
				}
				int i;
				i = 1;


			}
			foreach (string l in Links)
			{
				using (HttpClient client = new HttpClient())
				using (HttpResponseMessage response = await client.GetAsync("http://www.santacasasaocarlos.com.br" + l))
				using (HttpContent content = response.Content)
				{
					// ... Read the string.
					string result = await content.ReadAsStringAsync();

				}
			}
			*/

			var status = new List<RelogioStatus>();
			status.Add(new RelogioStatus { IP = "", status = "" });
			return status;
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
		public ActionResult Edit([Bind(Include = "Id,Nome,Descricao,Status,Login,Senha,Ip")] Relogio relogio)
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
		private class senhaRelogio
		{
			public string username;
			public string password;

		}

		public class RelogioStatus
		{
			public string IP { get; set; }
			public string status { get; set; }
		}
	}
}
