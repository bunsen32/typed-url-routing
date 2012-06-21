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

		public static readonly UrlPattern<int, string, string, string> ParamsPlusQuery = Path("{0}/{1}/query?bob={2};jeff={3}", Int, Slug, Slug, Slug);

		public static void Register(RouteCollection routes)
		{
			routes.ForController<HomeController>()
				.MapRoute(GetHome, c => c.Index)
				.MapRoute(Get(About), c => c.About)
				.MapRoute(Get(ShowParams), c => c.ShowTwoParameters)
				.MapRoute(Get(ParamsPlusQuery), c=>c.ShowTwoParametersPlusQuery);
		}
	}
}