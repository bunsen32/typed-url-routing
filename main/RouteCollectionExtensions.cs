// -----------------------------------------------------------------------
// <copyright file="RouteCollectionExtensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Uk.Co.Cygnets.UrlRouting
{
	using System;
	using System.Linq;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Routing;
	using Uk.Co.Cygnets.UrlRouting.Handlers;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public static class RouteCollectionExtensions
	{
		public static ControllerRouteMapper<Ctrl> ForController<Ctrl>(this RouteCollection routes)
			where Ctrl : Controller
		{
			return new ControllerRouteMapper<Ctrl>(routes);
		}

		public static Route MapRoute(this RouteCollection routes, RequestPattern<UrlPattern> pattern, Func<HttpContextBase, ActionResult> handler)
		{
			return routes.AddRoute(pattern, FuncRouteHandler.Create(pattern, handler));
		}

		public static Route MapRoute<P1>(this RouteCollection routes, RequestPattern<UrlPattern<P1>> pattern, Func<HttpContextBase, P1, ActionResult> handler)
		{
			return routes.AddRoute(pattern, FuncRouteHandler.Create(pattern, handler));
		}

		public static Route MapRoute<P1, P2>(this RouteCollection routes, RequestPattern<UrlPattern<P1, P2>> pattern, Func<HttpContextBase, P1, P2, ActionResult> handler)
		{
			return routes.AddRoute(pattern, FuncRouteHandler.Create(pattern, handler));
		}

		public static Route MapRoute<P1, P2, P3>(this RouteCollection routes, RequestPattern<UrlPattern<P1, P2, P3>> pattern, Func<HttpContextBase, P1, P2, P3, ActionResult> handler)
		{
			return routes.AddRoute(pattern, FuncRouteHandler.Create(pattern, handler));
		}

		internal static Route AddRoute(this RouteCollection routes, AbstractRequestPattern pattern, AbstractRouteHandler handler)
		{
			var route = new Route(
				url: GetRouteUrl(pattern.Url),
				defaults: new RouteValueDictionary(),
				constraints: GetConstraints(pattern),
				routeHandler: handler);
			var routeName = pattern.Url.Pattern;

			routes.Add(routeName, route);
			return route;
		}

		private static string GetRouteUrl(AbstractUrlPattern url)
		{
			var range = Enumerable.Range(0, url.Arity);
			var parameterNames = range.Select(i => "{" + url.ParameterName(i) + "}").ToArray();
			return string.Format(url.Pattern, (object[])parameterNames);
		}

		private static RouteValueDictionary GetConstraints(AbstractRequestPattern requestPattern)
		{
			var url = requestPattern.Url;
			var result = new RouteValueDictionary();
			for (int i = 0; i < url.Arity; i++)
			{
				result[url.ParameterName(i)] = url.ParameterPatterns[i];
			}

			var methodConstraints = GetMethodConstraintsOrNull(requestPattern.Method);
			if (methodConstraints != null)
			{
				result["httpMethod"] = methodConstraints;
			}

			return result;
		}

		private static HttpMethodConstraint GetMethodConstraintsOrNull(HttpMethod methods)
		{
			if (methods == HttpMethod.Any)
			{
				return null;
			}
			else
			{
				var allMethods = (HttpMethod[])Enum.GetValues(typeof(HttpMethod));
				return new HttpMethodConstraint(
					allMethods
						.Where(method => methods.HasFlag(method))
						.Select(method => method.ToString())
						.ToArray()
					);
			}
		}
	}
}
