using System;
using Dysphoria.Net.UrlRouting.TestApp.Models;

namespace Dysphoria.Net.UrlRouting.TestApp
{
	using System.Web.Routing;
	using Dysphoria.Net.UrlRouting;
	using Dysphoria.Net.UrlRouting.TestApp.Controllers;

	public class Routes : Urls
	{
		public static readonly UrlPattern
			Home = Path("");

		public static readonly UrlPattern<string>
			CatchAll = Path("{0}", AnyString);

		public static readonly UrlPattern
			Literal1 = Path("a/b/c");

		public static readonly UrlPattern<int>
			Path1Arg = Path("a/{0}/c", Int);

		public static readonly UrlPattern<int, string>
			Path2Args = Path("2args/{0}/{1}", Int, AnyString);

		public static readonly UrlPattern<string>
			Path1String = Path("1string/{0}", AnyString);

		public static readonly UrlPattern<DateRange>
			QueryDateRange = Path("dates", Query<DateRange>());

		public static readonly RequestPattern<UrlPattern, DateRange>
			PostDateRange = Post(Body<DateRange>(), Path("dates"));

		public static readonly UrlPattern
			UploadForm = Path("upload-form");

		public static readonly RequestPattern<UrlPattern, UploadForm>
			UploadAFile = Post(Body<UploadForm>(), Path("upload"));

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.ForController<HomeController>()
				.MapRoute(Get(Home), c => c.Home)
				.MapRoute(Get(Literal1), c => c.LiteralPath)
				.MapRoute(Get(Path1Arg), c => c.OneIntArg)
				.MapRoute(Get(Path2Args), c => c.TwoArgs)
				.MapRoute(Get(Path1String), c => c.OneString)
				.MapRoute(Get(QueryDateRange), c => c.DateRange)
				.MapRoute(PostDateRange, c => c.DateRange)
				.MapRoute(Get(UploadForm), c => c.UploadForm)
				.MapRoute(UploadAFile, c => c.UploadAFile)
				;

			// Always declare this last:
			routes.ForController<HomeController>()
				.MapRoute(Get(CatchAll), c => c.NotFound);
		}
	}
}