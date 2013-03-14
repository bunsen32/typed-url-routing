// -----------------------------------------------------------------------
// <copyright file="AbstractTypedRouteHandler.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Dysphoria.Net.UrlRouting.Handlers
{
	using System.Web;
	using System.Web.Routing;

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
