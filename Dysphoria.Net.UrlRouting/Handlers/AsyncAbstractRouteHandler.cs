// -----------------------------------------------------------------------
// <copyright file="AsyncAbstractRouteHandler.cs" company="Andrew Forrest">©2022 Andrew Forrest</copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may
// not use this file except in compliance with the License. Copy of
// license at: http://www.apache.org/licenses/LICENSE-2.0
//
// This software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS. See License for specific permissions and limitations.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;

namespace Dysphoria.Net.UrlRouting.Handlers
{
	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public abstract class AsyncAbstractRouteHandler : IRouteHandler
	{
		protected abstract AbstractUrlPattern UrlPattern { get; }

		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			requestContext.HttpContext.SetSessionStateBehavior(GetSessionStateBehavior(requestContext));
			return new ActionHandler(this, requestContext);
		}

		protected abstract SessionStateBehavior GetSessionStateBehavior(RequestContext requestContext);

		protected abstract Task ProcessRequest(RequestContext context);

		private class ActionHandler : HttpTaskAsyncHandler
		{
			private readonly AsyncAbstractRouteHandler outer;
			private readonly RequestContext requestContext;

			public ActionHandler(AsyncAbstractRouteHandler outer, RequestContext requestContext)
			{
				this.outer = outer;
				this.requestContext = requestContext;
			}

			public override bool IsReusable => true;

			public override Task ProcessRequestAsync(HttpContext context)
				=> outer.ProcessRequest(requestContext);
		}
	}
}
