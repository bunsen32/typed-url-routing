using System.Web;
using System.Web.Mvc;

namespace Dysphoria.Net.UrlRouting.TestApp
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}