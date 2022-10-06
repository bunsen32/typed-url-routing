// -----------------------------------------------------------------------
// <copyright file="AsyncControllerRouteHandler.cs" company="Andrew Forrest">©2022 Andrew Forrest</copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may
// not use this file except in compliance with the License. Copy of
// license at: http://www.apache.org/licenses/LICENSE-2.0
//
// This software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS. See License for specific permissions and limitations.
// -----------------------------------------------------------------------

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using System.Web.Routing;
using System.Web.SessionState;

namespace Dysphoria.Net.UrlRouting.Handlers
{
	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class AsyncControllerRouteHandler<C> : AsyncAbstractRouteHandler
		where C : ControllerBase
	{
		private static readonly ConcurrentDictionary<Type, SessionStateBehavior> _sessionStateCache = new ConcurrentDictionary<Type, SessionStateBehavior>();
		private readonly AbstractRequestPattern _pattern;
		private readonly string _controllerName, _actionName;
		private readonly Type _controllerType;
		private readonly Func<C, ControllerContext, Task<ActionResult>> _handler;

		public AsyncControllerRouteHandler(AbstractRequestPattern pattern, string controllerName, string actionName, Func<C, ControllerContext, Task<ActionResult>> handler)
		{
			_pattern = pattern;
			_controllerName = controllerName;
			_actionName = actionName;
			_controllerType = typeof(C);
			_handler = handler;
		}

		protected override AbstractUrlPattern UrlPattern
		{
			get { return _pattern.Url; }
		}

		protected override SessionStateBehavior GetSessionStateBehavior(RequestContext requestContext)
		{
			return GetControllerSessionBehavior(requestContext, _controllerType);
		}

		protected override async Task ProcessRequest(RequestContext context)
		{
			var controller = Activator.CreateInstance<C>();
			var disposable = controller as IDisposable;
			try
			{
				var fullController = controller as Controller;
				if (fullController != null)
				{
					fullController.ActionInvoker = new Invoker(this);
					SetUpRouteData(context.RouteData);
					var asyncController = (IAsyncController)fullController;
					await Task.Factory.FromAsync(asyncController.BeginExecute, asyncController.EndExecute, context, TaskCreationOptions.None);
				}
				else
				{
					await ProcessRequestMinimally(new ControllerContext(context, controller), controller);
				}
			}
			finally
			{
				disposable?.Dispose();
			}
		}

		/// <summary>
		/// Processes request, but without involving any filters. Therefore authentication
		/// will probably not work. This is only practically any use for testing.
		/// </summary>
		private async Task ProcessRequestMinimally(ControllerContext controllerContext, C controller)
		{
			SetUpRouteData(controllerContext.RouteData);
			controller.ControllerContext = controllerContext;
			var result = await _handler.Invoke(controller, controllerContext);

			result.ExecuteResult(controllerContext);
		}

		private void SetUpRouteData(RouteData routeData)
		{
			routeData.Values["controller"] = _controllerName;
			routeData.Values["action"] = _actionName;
		}

		protected internal virtual SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, Type controllerType)
		{
			return _sessionStateCache.GetOrAdd(
				controllerType,
				type =>
				{
					var attr = type.GetCustomAttributes(typeof(SessionStateAttribute), inherit: true)
						.OfType<SessionStateAttribute>()
						.FirstOrDefault();

					return (attr != null) ? attr.Behavior : SessionStateBehavior.Default;
				});
		}

		private class Invoker : AsyncControllerActionInvoker
		{
			private readonly AsyncControllerRouteHandler<C> outer;

			public Invoker(AsyncControllerRouteHandler<C> outer)
			{
				this.outer = outer;
			}

			protected override IAsyncResult BeginInvokeActionMethod(
				ControllerContext controllerContext,
				ActionDescriptor actionDescriptor,
				IDictionary<string, object> parameters,
				AsyncCallback callback,
				object state)
			{
				return TaskAsyncHelper<ActionResult>.BeginTask(
					() => outer._handler.Invoke((C)controllerContext.Controller, controllerContext),
					callback,
					state);
			}

			protected override ActionResult EndInvokeActionMethod(IAsyncResult asyncResult)
			{
				return TaskAsyncHelper<ActionResult>.EndTask(asyncResult);
			}
		}
	}
}