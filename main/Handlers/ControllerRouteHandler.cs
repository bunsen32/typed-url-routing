// -----------------------------------------------------------------------
// <copyright file="ControllerRouteHandler.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Uk.Co.Cygnets.UrlRouting.Handlers
{
	using System;
	using System.Web.Mvc;
	using System.Web.Routing;
using System.Collections.Specialized;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class ControllerRouteHandler<C>: AbstractRouteHandler
		where C: ControllerBase
	{
		private readonly AbstractRequestPattern pattern;
		private readonly string controllerName, actionName;
		private readonly Type controllerType;
		private readonly Func<C, RequestContext, ActionResult> handler;

		public ControllerRouteHandler(AbstractRequestPattern pattern, string controllerName, string actionName, Func<C, RequestContext, ActionResult> handler)
		{
			this.pattern = pattern;
			this.controllerName = controllerName;
			this.actionName = actionName;
			this.controllerType = typeof(C);
			this.handler = handler;
		}

		protected override void ProcessRequest(RequestContext context)
		{
			var controller = Activator.CreateInstance<C>();
			try
			{
				var controllerContext = new ControllerContext(context, controller);
				controller.ControllerContext = controllerContext;
				var result = this.handler.Invoke(controller, context);

				controllerContext.RouteData.Values["controller"] = this.controllerName;
				controllerContext.RouteData.Values["action"] = this.actionName;
				result.ExecuteResult(controllerContext);
			}
			finally
			{
				var disposable = controller as IDisposable;
				if (disposable != null) disposable.Dispose();
			}
		}

		protected override AbstractUrlPattern UrlPattern
		{
			get { return this.pattern.Url; }
		}
	}
}
