// -----------------------------------------------------------------------
// <copyright file="AbstractRouteHandler.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
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
	using System.Web;
	using System.Web.Routing;
	using System.Web.SessionState;
	using System;
	using System.Web.Mvc;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public abstract class AbstractRouteHandler : IRouteHandler
	{
		protected abstract AbstractUrlPattern UrlPattern { get; }

		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			requestContext.HttpContext.SetSessionStateBehavior(GetSessionStateBehavior(requestContext));
			return new ActionHandler(this, requestContext);
		}

		protected abstract SessionStateBehavior GetSessionStateBehavior(RequestContext requestContext);

		protected abstract void ProcessRequest(RequestContext context);

		private class ActionHandler : IHttpHandler, IRequiresSessionState
		{
			private readonly AbstractRouteHandler outer;
			private readonly RequestContext requestContext;

			public ActionHandler(AbstractRouteHandler outer, RequestContext requestContext)
			{
				this.outer = outer;
				this.requestContext = requestContext;
			}

			public bool IsReusable
			{
				get { return true; }
			}

			public void ProcessRequest(HttpContext context)
			{
				this.outer.ProcessRequest(this.requestContext);
			}
		}
	}
}
