using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;

namespace test.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult ShowTwoParameters(int first, string second)
		{
			dynamic model = (dynamic)new ExpandoObject();
			model.First = first;
			model.Second = second;
			return View(model);
		}
	}
}
