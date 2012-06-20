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

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public abstract class AbstractRouteHandler: IRouteHandler
	{
		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			return new ActionHandler(this, requestContext);
		}

		protected abstract void ProcessRequest(RequestContext context, params string[] parameters);

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
				this.outer.ProcessRequest(
					this.requestContext, 
					this.UrlStringParameters());
			}

			private string[] UrlStringParameters()
			{
				var urlPattern = this.outer.UrlPattern;
				var parameters = urlPattern.Arity == 0 ? null : new string[urlPattern.Arity];
				var values = this.requestContext.RouteData.Values;
				for (int i = 0; i < urlPattern.Arity; i++)
				{
					parameters[i] = (values[urlPattern.ParameterName(i)] as string) ?? "";
				}

				return parameters;
			}
		}
	}
}
