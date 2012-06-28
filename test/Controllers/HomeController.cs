using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using test.Models;

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
			return View(new string[] { first.ToString(), second });
		}

		public ActionResult ShowTwoParametersPlusQuery(int first, string second, string third, string forth)
		{
			return View(new string[] { first.ToString(), second, third, forth });
		}

		[HttpGet]
		public ActionResult ModelParameter(int? id, Abc abc = null)
		{
			return View(abc);
		}
	}
}
