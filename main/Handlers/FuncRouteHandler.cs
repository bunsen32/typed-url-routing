// -----------------------------------------------------------------------
// <copyright file="FuncRouteHandler.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may
// not use this file except in compliance with the License. Copy of
// license at: http://www.apache.org/licenses/LICENSE-2.0
//
// This software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS. See License for specific permissions and limitations.
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
			get { return this.pattern.Url; }
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
