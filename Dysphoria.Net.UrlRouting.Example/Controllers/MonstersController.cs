namespace Dysphoria.Net.UrlRouting.Test.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;
	using Dysphoria.Net.UrlRouting.Test.Models;
	using Dysphoria.Net.UrlRouting.Test.ViewModels;

	public class MonstersController : Controller
	{
		public ActionResult List()
		{
			using (var db = MonstersRepository.Get())
			{
				return View("List", Tuple.Create(db.Monsters.AsEnumerable(), ""));
			}
		}

		public ActionResult ListCategory(string category)
		{
			using (var db = MonstersRepository.Get())
			{
				var monstersInCategory = db.Monsters.Where(m => m.Categories.Contains(category));
				return View("List", Tuple.Create(monstersInCategory, category));
			}
		}

		public ActionResult ShowNewOrExisting(int? id)
		{
			return id.HasValue
				? ShowExisting(id.Value)
				: ShowNew();
		}

		public ActionResult ShowExisting(int id)
		{
			using (var db = MonstersRepository.Get())
			{
				var monster = db.Monsters.FirstOrDefault(m => m.Id == id);
				if (monster == null) return HttpNotFound("Cannot find monster " + id);
				return MonsterView(monster);
			}
		}

		public ActionResult ShowNew()
		{
			return MonsterView(new Monster());
		}

		public ActionResult SaveNewOrExisting(int? id, Monster monster)
		{
			if (!this.ModelState.IsValid)
				return MonsterView(monster);

			using (var db = MonstersRepository.Get())
			{
				// 'Add' works here even if it already exists in the collection--SaveChanges fixes things up.
				// (It's not a real application anyway:) Use the correct data access type shenanigans for your app.
				db.Monsters.Add(monster);
				db.SaveChanges();
			}

			return this.RedirectTo(SiteUrls.MonsterDetail[monster.Id]);
		}

		public ActionResult Delete(int id)
		{
			using (var db = MonstersRepository.Get())
			{
				var monster = db.Monsters.FirstOrDefault(m => m.Id == id);
				if (monster != null)
					db.Monsters.Remove(monster);
			}

			return this.RedirectTo(SiteUrls.MonsterList);
		}

		public ViewResult MonsterView(Monster monster)
		{
			this.ViewBag.AllCategories = MonstersRepository.Get().Categories;
			return View("Detail", monster);
		}

		public ViewResult Search(string query, AdvancedSearchOptions opt)
		{
			var q = (query ?? "").Trim().ToLower();
			var options = opt ?? new AdvancedSearchOptions();
			IEnumerable<Monster> results;
			if (string.IsNullOrWhiteSpace(query))
			{
				results = null;
			}
			else
			{
				var categories = options.Categories ?? new string[0];
				results = MonstersRepository.Get().Monsters
					.Where(m => 
						(m.Name.ToLower().Contains(q) || m.Description.ToLower().Contains(q)) && 
						(!options.IsDangerous || m.IsDangerous) &&
						(categories.All(cat => m.Categories.Contains(cat))));
			}

			this.ViewBag.AllCategories = MonstersRepository.Get().Categories;
			return View("Search", new SearchFormModel() { SearchString = q, Options = options, Results = results });
		}
	}
}
