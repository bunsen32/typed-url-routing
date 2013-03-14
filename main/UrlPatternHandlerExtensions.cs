// -----------------------------------------------------------------------
// <copyright file="UrlPatternHandlerExtensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Dysphoria.Net.UrlRouting
{
	using System;
	using System.Linq;
	using System.Web.Routing;
	using Dysphoria.Net.UrlRouting.PathComponents;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public static class UrlPatternHandlerExtensions
	{
		public static string GetRouteName(this AbstractUrlPattern pattern)
		{
			return pattern.PathPattern;
		}

		public static Tuple<T0> ExtractParameters<T0>(this UrlPattern<T0> url, RequestContext req)
		{
			var p = UrlStringParameters(url, req);
			return Tuple.Create(
				DecodeLastParameter(url.Param0, p, 0, req));
		}

		public static Tuple<T0, T1> ExtractParameters<T0, T1>(this UrlPattern<T0, T1> url, RequestContext req)
		{
			var p = UrlStringParameters(url, req);
			return Tuple.Create(
				url.Param0.FromString(p[0]),
				DecodeLastParameter(url.Param1, p, 1, req));
		}

		public static Tuple<T0, T1, T2> ExtractParameters<T0, T1, T2>(this UrlPattern<T0, T1, T2> url, RequestContext req)
		{
			var p = UrlStringParameters(url, req);
			return Tuple.Create(
				url.Param0.FromString(p[0]),
				url.Param1.FromString(p[1]),
				DecodeLastParameter(url.Param2, p, 2, req));
		}

		public static Tuple<T0, T1, T2, T3> ExtractParameters<T0, T1, T2, T3>(this UrlPattern<T0, T1, T2, T3> url, RequestContext req)
		{
			var p = UrlStringParameters(url, req);
			return Tuple.Create(
				url.Param0.FromString(p[0]),
				url.Param1.FromString(p[1]),
				url.Param2.FromString(p[2]),
				DecodeLastParameter(url.Param3, p, 3, req));
		}

		private static string[] UrlStringParameters(AbstractUrlPattern url, RequestContext req)
		{
			var parameters = url.ParameterCount == 0 ? null : new string[url.ParameterCount];
			var values = req.RouteData.Values;
			for (int i = 0; i < url.PathParameterCount; i++)
			{
				parameters[i] = (values[url.ParameterName(i)] as string) ?? "";
			}

			var query =req.HttpContext.Request.QueryString;
			for (int i = url.PathParameterCount; i < url.ParameterCount; i++)
			{
				parameters[i] = (query[url.ParameterName(i)] as string) ?? "";
			}

			return parameters;
		}

		private static T DecodeLastParameter<T>(UrlArgument<T> descriptor, string[] parameterStrings, int index, RequestContext req)
		{
			var query = req.HttpContext.Request.QueryString;
			var simple = (descriptor as PathComponent<T>);
			var queryParam = (descriptor as QueryStringEncoding<T>);
			if (simple == null && queryParam == null) throw new ArgumentException("Do not recognise UrlParameter subclass " + descriptor.GetType().Name);
			if (simple != null)
			{
				return simple.FromString(parameterStrings[index]);
			}
			else
			{
				return queryParam.FromDictionary(req.HttpContext.Request.QueryString);
			}
		}
	}
}
