// -----------------------------------------------------------------------
// <copyright file="ControllerRouteHandler.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
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
	using System.Web.Mvc;
	using System.Web.Routing;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class ControllerRouteHandler<C> : AbstractRouteHandler
		where C : ControllerBase
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

		protected override AbstractUrlPattern UrlPattern
		{
			get { return this.pattern.Url; }
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
	}
}
