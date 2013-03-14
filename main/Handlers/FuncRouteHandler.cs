// -----------------------------------------------------------------------
// <copyright file="TypedRouteHandler.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Dysphoria.Net.UrlRouting.Handlers
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
		private readonly Func<RequestContext, ActionResult> handler;

		public FuncRouteHandler(AbstractRequestPattern pattern, Func<RequestContext, ActionResult> handler)
		{
			this.pattern = pattern;
			this.handler = handler;
		}

		protected override AbstractUrlPattern UrlPattern
		{
			get { return pattern.Url; }
		}

		protected override void ProcessRequest(RequestContext context)
		{
			var result = this.handler.Invoke(context);
			throw new NotImplementedException("Need to do something with the result. Needs a ControllerContext");
		}

		public static FuncRouteHandler Create(RequestPattern<UrlPattern> pattern, Func<HttpContextBase, ActionResult> handler)
		{
			return new FuncRouteHandler(pattern, (req) => handler(req.HttpContext));
		}

		public static FuncRouteHandler Create<P1>(RequestPattern<UrlPattern<P1>> pattern, Func<HttpContextBase, P1, ActionResult> handler)
		{
			return new FuncRouteHandler(pattern, (req) =>
			{
				var p = pattern.Url.ExtractParameters(req);
				return handler.Invoke(req.HttpContext, p.Item1);
			});
		}

		public static FuncRouteHandler Create<P1, P2>(RequestPattern<UrlPattern<P1, P2>> pattern, Func<HttpContextBase, P1, P2, ActionResult> handler)
		{
			return new FuncRouteHandler(pattern, (req) =>
			{
				var p = pattern.Url.ExtractParameters(req);
				return handler.Invoke(req.HttpContext, p.Item1, p.Item2);
			});
		}

		public static FuncRouteHandler Create<P1, P2, P3>(RequestPattern<UrlPattern<P1, P2, P3>> pattern, Func<HttpContextBase, P1, P2, P3, ActionResult> handler)
		{
			return new FuncRouteHandler(pattern, (req) =>
			{
				var p = pattern.Url.ExtractParameters(req);
				return handler.Invoke(req.HttpContext, p.Item1, p.Item2, p.Item3);
			});
		}
	}
}
