using System;
using System.Security.Policy;
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

		public static readonly UrlPattern
			Async = Path("async");

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
			PostDateRange = Path("dates").Post(Body<DateRange>);

		public static readonly UrlPattern
			UploadForm = Path("upload-form");

		public static readonly RequestPattern<UrlPattern, UploadForm>
			UploadAFile = Path("upload").Post(Body<UploadForm>);

		public static readonly UrlPattern
			Async0Args = Path("async/0-args");

		public static readonly UrlPattern<string>
			Async1Arg = Path("async/1-arg/{0}", Slug);
		
		public static readonly UrlPattern<string, string>
			Async2Args = Path("async/2-args/{0}/{1}", Slug, Slug);
		
		public static readonly UrlPattern<string, string, string>
			Async3Args = Path("async/3-args/{0}/{1}/{2}", Slug, Slug, Slug);
		
		public static readonly UrlPattern<string, string, string, string>
			Async4Args = Path("async/4-args/{0}/{1}/{2}/{3}", Slug, Slug, Slug, Slug);

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.ForController<HomeController>()
				.MapRoute(Home.Get(), c => c.Home)
				.MapRoute(Literal1.Get(), c => c.LiteralPath)
				.MapRoute(Path1Arg.Get(), c => c.OneIntArg)
				.MapRoute(Path2Args.Get(), c => c.TwoArgs)
				.MapRoute(Path1String.Get(), c => c.OneString)
				.MapRoute(QueryDateRange.Get(), c => c.DateRange)
				.MapRoute(PostDateRange, c => c.DateRange)
				.MapRoute(UploadForm.Get(), c => c.UploadForm)
				.MapRoute(UploadAFile, c => c.UploadAFile)
				;

			routes.ForController<HomeController>()
				.MapRoute(Async.Get(), c => c.Async);

			routes.ForController<AsyncController>()
				.MapRoute(Async0Args.Get(), c => c.Async0Arg)
				.MapRoute(Async1Arg.Get(), c => c.Async1Arg)
				.MapRoute(Async2Args.Get(), c => c.Async2Args)
				.MapRoute(Async3Args.Get(), c => c.Async3Args)
				.MapRoute(Async4Args.Get(), c => c.Async4Args)
				;
			
			// Always declare this last:
			routes.ForController<HomeController>()
				.MapRoute(Get(CatchAll), c => c.NotFound);
		}
	}
}