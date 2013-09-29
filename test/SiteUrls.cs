namespace Dysphoria.Net.UrlRouting.Test
{
	using System.Web.Routing;
	using Dysphoria.Net.UrlRouting;
	using Dysphoria.Net.UrlRouting.Test.Controllers;
	using Dysphoria.Net.UrlRouting.Test.Models;

	public class SiteUrls : Urls
	{
		public static readonly RequestPattern<UrlPattern> GetHome = Get(Path("/"));

		public static readonly UrlPattern About = Path("/about");

		public static readonly UrlPattern<int, string> ShowParams = Path("/{0}/{1}/show", Int, Slug);

		public static readonly UrlPattern<int, string, string, string> ParamsPlusQuery = Path("/{0}/{1}/query?bob={2}&jeff={3}", Int, Slug, AnyString, AnyString);

		public static readonly UrlPattern<Abc> Search = Path("/search", Query<Abc>());

		public static readonly UrlPattern<int?> NullableIntPath = Path("/nullable/int/{0}", Int.Or("nothing"));

		public static readonly UrlPattern<string> NullableStringPath = Path("/nullable/str/{0}", Slug.OrNull("nothing"));

		public static void Register(RouteCollection routes)
		{
			routes.ForController<HomeController>()
				.MapRoute(GetHome, c => c.Index)
				.MapRoute(Get(About), c => c.About)
				.MapRoute(Get(ShowParams), c => c.ShowTwoParameters)
				.MapRoute(Get(ParamsPlusQuery), c=>c.ShowTwoParametersPlusQuery)
				.MapRoute(Get(Search), c=> c.ModelParameter);
		}
	}
}