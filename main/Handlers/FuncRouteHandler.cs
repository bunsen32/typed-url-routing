// -----------------------------------------------------------------------
// <copyright file="TypedRouteHandler.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Uk.Co.Cygnets.UrlRouting.Handlers
{
	using System;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Routing;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class FuncRouteHandler : AbstractRouteHandler
	{
		private readonly AbstractRequestPattern pattern;
		private readonly Func<HttpContextBase, string[], ActionResult> handler;

		public FuncRouteHandler(AbstractRequestPattern pattern, Func<HttpContextBase, string[], ActionResult> handler)
		{
			this.pattern = pattern;
			this.handler = handler;
		}

		protected override AbstractUrlPattern UrlPattern
		{
			get { return pattern.Url; }
		}

		protected override void ProcessRequest(RequestContext context, params string[] parameters)
		{
			var result = this.handler.Invoke(context.HttpContext, parameters);
			throw new NotImplementedException("Need to do something with the result. Needs a ControllerContext");
		}

		public static FuncRouteHandler Create(RequestPattern<UrlPattern> pattern, Func<HttpContextBase, ActionResult> handler)
		{
			return new FuncRouteHandler(pattern, (b, p) => handler(b));
		}

		public static FuncRouteHandler Create<P1>(RequestPattern<UrlPattern<P1>> pattern, Func<HttpContextBase, P1, ActionResult> handler)
		{
			var url = pattern.Url;
			return new FuncRouteHandler(pattern, (b, p) =>
				handler(
					b,
					url.Param1.FromString(p[0])));
		}

		public static FuncRouteHandler Create<P1, P2>(RequestPattern<UrlPattern<P1, P2>> pattern, Func<HttpContextBase, P1, P2, ActionResult> handler)
		{
			var url = pattern.Url;
			return new FuncRouteHandler(pattern, (b, p) =>
				handler(
					b,
					url.Param1.FromString(p[0]),
					url.Param2.FromString(p[1])));
		}

		public static FuncRouteHandler Create<P1, P2, P3>(RequestPattern<UrlPattern<P1, P2, P3>> pattern, Func<HttpContextBase, P1, P2, P3, ActionResult> handler)
		{
			var url = pattern.Url;
			return new FuncRouteHandler(pattern, (b, p) => 
				handler(
					b, 
					url.Param1.FromString(p[0]),
					url.Param2.FromString(p[1]),
					url.Param3.FromString(p[2])));
		}
	}
}
