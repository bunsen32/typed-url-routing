using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Uk.Co.Cygnets.UrlRouting;
using System.Web.Routing;
using test.Controllers;

namespace test
{
	public class SiteUrls : Urls
	{
		public static readonly RequestPattern<UrlPattern> GetHome = Get(Path(""));

		public static readonly UrlPattern About = Path("about");

		public static readonly UrlPattern<int, string> ShowParams = Path("{0}/{1}/show", Int, Slug);

		public static void Register(RouteCollection routes)
		{
			routes.ForController<HomeController>()
				.MapRoute(GetHome, c => c.Index)
				.MapRoute(Get(About), c => c.About)
				.MapRoute(Get(ShowParams), c => c.ShowTwoParameters);
		}
	}
}