namespace Dysphoria.Net.UrlRouting.Test
{
	using System.Web.Routing;
	using Dysphoria.Net.UrlRouting;
	using Dysphoria.Net.UrlRouting.Test.Controllers;
	using Dysphoria.Net.UrlRouting.Test.Models;

	public class SiteUrls : Urls
	{
		public static readonly UrlPattern 
			Home = Path("");

		public static readonly UrlPattern 
			MonsterList = Path("monsters/");

		public static readonly UrlPattern<string>
			MonstersInCategory = Path("monsters/category/{0}", Slug);

		public static readonly UrlPattern<int?> 
			MonsterDetail = Path("monsters/{0}/", Int.Or("new"));

		public static readonly AppLocalUrl
			NewMonster = MonsterDetail[null];

		public static readonly RequestPattern<UrlPattern<int>> 
			DoDeleteMonster = Post(Path("monsters/{0}/delete", Int));

		public static readonly RequestPattern<UrlPattern<int?>, Monster> 
			AddEditMonster = Post(Body<Monster>(), MonsterDetail);

		public static readonly UrlPattern<string, AdvancedSearchOptions>
			AdvancedSearch = Path(
				"search+",
				Arg("q", AnyString),
				Query<AdvancedSearchOptions>());

		public static void Register(RouteCollection routes)
		{
			routes.ForController<HomeController>()
				.MapRoute(Get(Home), c => c.Index);

			routes.ForController<MonstersController>()
				.MapRoute(Get(MonsterList), c => c.List)
				.MapRoute(Get(MonstersInCategory), c => c.ListCategory)
				.MapRoute(Get(MonsterDetail), c => c.ShowNewOrExisting)
				.MapRoute(AddEditMonster, c => c.SaveNewOrExisting)
				.MapRoute(DoDeleteMonster, c => c.Delete)
				.MapRoute(Get(AdvancedSearch), c => c.Search);
		}
	}
}