// -----------------------------------------------------------------------
// <copyright file="AbstractTypedRouteHandler.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Uk.Co.Cygnets.UrlRouting.Handlers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Web;
	using System.Web.Routing;
	using System.Web.Mvc;
using System.Collections.Specialized;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public abstract class AbstractRouteHandler: IRouteHandler
	{
		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			return new ActionHandler(this, requestContext);
		}

		protected abstract void ProcessRequest(RequestContext context);

		protected abstract AbstractUrlPattern UrlPattern { get; }

		private class ActionHandler : IHttpHandler
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
