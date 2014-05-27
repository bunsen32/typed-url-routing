// -----------------------------------------------------------------------
// <copyright file="RouteCollectionExtensions.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may
// not use this file except in compliance with the License. Copy of
// license at: http://www.apache.org/licenses/LICENSE-2.0
//
// This software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS. See License for specific permissions and limitations.
// -----------------------------------------------------------------------
namespace Dysphoria.Net.UrlRouting
{
	using System;
	using System.Linq;
	using System.Web.Mvc;
	using System.Web.Routing;
	using Dysphoria.Net.UrlRouting.Handlers;

	/// <summary>
	/// Extension method to get a ControllerRouteMapper for a RouteCollection.
	/// </summary>
	public static class RouteCollectionExtensions
	{
		public static ControllerRouteMapper<Ctrl> ForController<Ctrl>(this RouteCollection routes)
			where Ctrl : ControllerBase
		{
			return new ControllerRouteMapper<Ctrl>(routes);
		}

		internal static Route AddRoute(this RouteCollection routes, AbstractRequestPattern pattern, AbstractRouteHandler handler)
		{
			var route = new Route(
				url: GetRouteUrl(pattern.Url),
				defaults: GetDefaults(pattern),
				constraints: GetConstraints(pattern),
				routeHandler: handler);
			var routeName = GetRouteName(pattern);

			routes.Add(routeName, route);
			return route;
		}

		private static string GetRouteName(AbstractRequestPattern pattern)
		{
			return pattern.Method + pattern.Url.Description;
		}

		private static string GetRouteUrl(AbstractUrlPattern url)
		{
			var range = Enumerable.Range(0, url.SimpleParameterCount);
			var parameterNames = range.Select(i => "{" + url.ParameterName(i) + "}").ToArray();
			return string.Format(NoInitialSlash(url.PathPattern), (object[])parameterNames);
		}

		private static string NoInitialSlash(string urlPath)
		{
			if (urlPath == null) throw new ArgumentNullException("urlPath");
			if (urlPath.StartsWith("/")) throw new ArgumentException("Path cannot start with '/'.", "urlPath");
			return urlPath;
		}

		private static RouteValueDictionary GetConstraints(AbstractRequestPattern requestPattern)
		{
			var url = requestPattern.Url;
			var result = new RouteValueDictionary();
			for (int i = 0; i < url.PathArity; i++)
			{
				result[url.ParameterName(i)] = url.ParameterRegexes[i];
			}

			var methodConstraints = GetMethodConstraintsOrNull(requestPattern.Method);
			if (methodConstraints != null)
			{
				result["httpMethod"] = methodConstraints;
			}

			return result;
		}

		private static RouteValueDictionary GetDefaults(AbstractRequestPattern requestPattern)
		{
			return new RouteValueDictionary();
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
