namespace Dysphoria.Net.UrlRouting.Example
{
	using System.Web.Routing;
	using Dysphoria.Net.UrlRouting;
	using Dysphoria.Net.UrlRouting.Example.Controllers;
	using Dysphoria.Net.UrlRouting.Example.Models;

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
			DoDeleteMonster = Path("monsters/{0}/delete", Int).Post();

		public static readonly RequestPattern<UrlPattern<int?>, Monster>
			AddEditMonster = MonsterDetail.Post(Body<Monster>);

		public static readonly UrlPattern<string, AdvancedSearchOptions>
			AdvancedSearch = Path(
				"search",
				Arg("q", AnyString),
				Query<AdvancedSearchOptions>());

		public static void Register(RouteCollection routes)
		{
			routes.ForController<HomeController>()
				.MapRoute(Home.Get(), c => c.Index);

			routes.ForController<MonstersController>()
				.MapRoute(MonsterList.Get(), c => c.List)
				.MapRoute(MonstersInCategory.Get(), c => c.ListCategory)
				.MapRoute(MonsterDetail.Get(), c => c.ShowNewOrExisting)
				.MapRoute(AddEditMonster, c => c.SaveNewOrExisting)
				.MapRoute(DoDeleteMonster, c => c.Delete)
				.MapRoute(AdvancedSearch.Get(), c => c.Search);
		}
	}
}