using Dysphoria.Net.UrlRouting.TestApp.Models;

namespace Dysphoria.Net.UrlRouting.TestApp.Controllers
{
	using System.Web.Mvc;

	public class HomeController : Controller
	{
		public const string PlainText = "text/plain";

		public ActionResult Home()
		{
			return this.Content("Hello");
		}

		public ActionResult LiteralPath()
		{
			return this.Content("Literal1");
		}

		public ActionResult OneIntArg(int arg)
		{
			return this.Content("arg=" + arg);
		}

		public ActionResult TwoArgs(int arg0, string arg1)
		{
			return this.Content(string.Format("arg0={0};arg1={1}", arg0, arg1));
		}

		public ActionResult OneString(string arg)
		{
			return this.Content("arg=" + arg);
		}

		public ActionResult DateRange(DateRange r)
		{
			return this.Content(string.Format("from={0:yyyyMMdd};to={1:yyyyMMdd}", r.From, r.To));
		}

		public ActionResult NotFound(string path)
		{
			this.Response.StatusCode = 404;
			return this.View();
		}
	}
}
