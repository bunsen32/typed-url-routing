// -----------------------------------------------------------------------
// <copyright file="ControllerRouteMapper.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may
// not use this file except in compliance with the License. Copy of
// license at: http://www.apache.org/licenses/LICENSE-2.0
//
// This software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS. See License for specific permissions and limitations.
// -----------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Dysphoria.Net.UrlRouting.Handlers;

namespace Dysphoria.Net.UrlRouting
{
	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class ControllerRouteMapper<C>
		where C : ControllerBase
	{
		private readonly RouteCollection routes;
		private readonly string controllerName;

		public ControllerRouteMapper(RouteCollection routes)
		{
			this.routes = routes;
			this.controllerName = GetControllerName(typeof(C));
		}

		public RouteCollection Routes { get { return this.routes; } }

		#region 0-argument URL patterns

		public ControllerRouteMapper<C> MapRoute(
			RequestPattern<UrlPattern> pattern,
			Expression<Func<C, Func<ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(pattern, handler, (c, context) => methodFunc.Invoke(c).Invoke());
		}

		public ControllerRouteMapper<C> MapRoute(
			RequestPattern<UrlPattern> pattern,
			Expression<Func<C, Func<Task<ActionResult>>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(pattern, handler, (c, context) => methodFunc.Invoke(c).Invoke());
		}

		public ControllerRouteMapper<C> MapRoute<BodyType>(
			RequestPattern<UrlPattern, BodyType> pattern,
			Expression<Func<C, Func<BodyType, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var body = pattern.DecodeBody(context);
					return methodFunc.Invoke(c).Invoke(body);
				});
		}

		public ControllerRouteMapper<C> MapRoute<BodyType>(
			RequestPattern<UrlPattern, BodyType> pattern,
			Expression<Func<C, Func<BodyType, Task<ActionResult>>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var body = pattern.DecodeBody(context);
					return methodFunc.Invoke(c).Invoke(body);
				});
		}

		#endregion

		#region 1-argument URL patterns

		public ControllerRouteMapper<C> MapRoute<P1>(
			RequestPattern<UrlPattern<P1>> pattern,
			Expression<Func<C, Func<P1, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var p = pattern.Url.ExtractParameters(context);
					return methodFunc.Invoke(c).Invoke(p.Item1);
				});
		}

		public ControllerRouteMapper<C> MapRoute<P1>(
			RequestPattern<UrlPattern<P1>> pattern,
			Expression<Func<C, Func<P1, Task<ActionResult>>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var p = pattern.Url.ExtractParameters(context);
					return methodFunc.Invoke(c).Invoke(p.Item1);
				});
		}
		
		public ControllerRouteMapper<C> MapRoute<P1, BodyType>(
			RequestPattern<UrlPattern<P1>, BodyType> pattern,
			Expression<Func<C, Func<P1, BodyType, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var p = pattern.Url.ExtractParameters(context);
					var body = pattern.DecodeBody(context);
					return methodFunc.Invoke(c).Invoke(p.Item1, body);
				});
		}

		public ControllerRouteMapper<C> MapRoute<P1, BodyType>(
			RequestPattern<UrlPattern<P1>, BodyType> pattern,
			Expression<Func<C, Func<P1, BodyType, Task<ActionResult>>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var p = pattern.Url.ExtractParameters(context);
					var body = pattern.DecodeBody(context);
					return methodFunc.Invoke(c).Invoke(p.Item1, body);
				});
		}

		#endregion

		#region 2-argument URL patterns

		public ControllerRouteMapper<C> MapRoute<P1, P2>(
			RequestPattern<UrlPattern<P1, P2>> pattern,
			Expression<Func<C, Func<P1, P2, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var p = pattern.Url.ExtractParameters(context);
					return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2);
				});
		}

		public ControllerRouteMapper<C> MapRoute<P1, P2>(
			RequestPattern<UrlPattern<P1, P2>> pattern,
			Expression<Func<C, Func<P1, P2, Task<ActionResult>>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var p = pattern.Url.ExtractParameters(context);
					return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2);
				});
		}

		public ControllerRouteMapper<C> MapRoute<P1, P2, BodyType>(
			RequestPattern<UrlPattern<P1, P2>, BodyType> pattern,
			Expression<Func<C, Func<P1, P2, BodyType, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var p = pattern.Url.ExtractParameters(context);
					var body = pattern.DecodeBody(context);
					return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2, body);
				});
		}

		public ControllerRouteMapper<C> MapRoute<P1, P2, BodyType>(
			RequestPattern<UrlPattern<P1, P2>, BodyType> pattern,
			Expression<Func<C, Func<P1, P2, BodyType, Task<ActionResult>>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var p = pattern.Url.ExtractParameters(context);
					var body = pattern.DecodeBody(context);
					return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2, body);
				});
		}

		#endregion

		#region 3-argument URL patterns

		public ControllerRouteMapper<C> MapRoute<P1, P2, P3>(
			RequestPattern<UrlPattern<P1, P2, P3>> pattern,
			Expression<Func<C, Func<P1, P2, P3, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var p = pattern.Url.ExtractParameters(context);
					return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2, p.Item3);
				});
		}

		public ControllerRouteMapper<C> MapRoute<P1, P2, P3>(
			RequestPattern<UrlPattern<P1, P2, P3>> pattern,
			Expression<Func<C, Func<P1, P2, P3, Task<ActionResult>>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var p = pattern.Url.ExtractParameters(context);
					return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2, p.Item3);
				});
		}

		public ControllerRouteMapper<C> MapRoute<P1, P2, P3, BodyType>(
			RequestPattern<UrlPattern<P1, P2, P3>, BodyType> pattern,
			Expression<Func<C, Func<P1, P2, P3, BodyType, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var p = pattern.Url.ExtractParameters(context);
					var body = pattern.DecodeBody(context);
					return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2, p.Item3, body);
				});
		}

		public ControllerRouteMapper<C> MapRoute<P1, P2, P3, BodyType>(
			RequestPattern<UrlPattern<P1, P2, P3>, BodyType> pattern,
			Expression<Func<C, Func<P1, P2, P3, BodyType, Task<ActionResult>>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var p = pattern.Url.ExtractParameters(context);
					var body = pattern.DecodeBody(context);
					return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2, p.Item3, body);
				});
		}

		#endregion

		#region 4-argument URL patterns

		public ControllerRouteMapper<C> MapRoute<P1, P2, P3, P4>(
			RequestPattern<UrlPattern<P1, P2, P3, P4>> pattern,
			Expression<Func<C, Func<P1, P2, P3, P4, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var p = pattern.Url.ExtractParameters(context);
					return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2, p.Item3, p.Item4);
				});
		}

		public ControllerRouteMapper<C> MapRoute<P1, P2, P3, P4>(
			RequestPattern<UrlPattern<P1, P2, P3, P4>> pattern,
			Expression<Func<C, Func<P1, P2, P3, P4, Task<ActionResult>>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var p = pattern.Url.ExtractParameters(context);
					return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2, p.Item3, p.Item4);
				});
		}

		public ControllerRouteMapper<C> MapRoute<P1, P2, P3, P4, BodyType>(
			RequestPattern<UrlPattern<P1, P2, P3, P4>, BodyType> pattern,
			Expression<Func<C, Func<P1, P2, P3, P4, BodyType, ActionResult>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var p = pattern.Url.ExtractParameters(context);
					var body = pattern.DecodeBody(context);
					return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2, p.Item3, p.Item4, body);
				});
		}

		public ControllerRouteMapper<C> MapRoute<P1, P2, P3, P4, BodyType>(
			RequestPattern<UrlPattern<P1, P2, P3, P4>, BodyType> pattern,
			Expression<Func<C, Func<P1, P2, P3, P4, BodyType, Task<ActionResult>>>> handler)
		{
			var methodFunc = handler.Compile();
			return this.AddRouteHandler(
				pattern,
				handler,
				(c, context) =>
				{
					var p = pattern.Url.ExtractParameters(context);
					var body = pattern.DecodeBody(context);
					return methodFunc.Invoke(c).Invoke(p.Item1, p.Item2, p.Item3, p.Item4, body);
				});
		}

		#endregion

		private ControllerRouteMapper<C> AddRouteHandler<FunctionType>(
			AbstractRequestPattern pattern,
			Expression<FunctionType> handler,
			Func<C, ControllerContext, ActionResult> handlerFunction)
		{
			var method = GetMethodInfo(typeof(C), handler);
			var actionName = method.Name;
			this.routes.AddRoute(
				pattern,
				new ControllerRouteHandler<C>(pattern, this.controllerName, actionName, handlerFunction));

			return this; // To allow for method chaining.
		}

		private ControllerRouteMapper<C> AddRouteHandler<FunctionType>(
			AbstractRequestPattern pattern,
			Expression<FunctionType> handler,
			Func<C, ControllerContext, Task<ActionResult>> handlerFunction)
		{
			var method = GetMethodInfo(typeof(C), handler);
			var actionName = method.Name;
			this.routes.AddRoute(
				pattern,
				new AsyncControllerRouteHandler<C>(pattern, this.controllerName, actionName, handlerFunction));

			return this; // To allow for method chaining.
		}

		internal static string GetControllerName(Type controller)
		{
			const string suffix = "Controller";
			var wholeName = controller.Name;
			var endsWithSuffix = wholeName.Length > suffix.Length && wholeName.EndsWith(suffix);
			return endsWithSuffix
				? wholeName.Substring(0, wholeName.Length - suffix.Length)
				: wholeName;
		}

		private static MethodInfo GetMethodInfo(Type controllerClass, Expression method)
		{
			var lambda = method as LambdaExpression;
			if (lambda == null) throw new ArgumentException("Argument is not a lambda expression (c => c.Thing)");
			var convert = lambda.Body;
			var body = (convert.NodeType == ExpressionType.Convert)
				? ((UnaryExpression)convert).Operand as MethodCallExpression
				: convert as MethodCallExpression;
			if (body == null) throw new ArgumentException("Argument not in correct form (c => c.Thing)");

			var visitor = new MethodGetter(controllerClass);
			visitor.Visit(body);
			if (visitor.Found == null) throw new ArgumentException("Expression does not contain a method reference.");
			return visitor.Found;
		}

		private class MethodGetter : ExpressionVisitor
		{
			private readonly Type classType;
			private MethodInfo found = null;

			public MethodGetter(Type classType)
			{
				this.classType = classType;
			}

			public MethodInfo Found { get { return this.found; } }

			protected override Expression VisitConstant(ConstantExpression node)
			{
				var method = node.Value as MethodInfo;
				if (method != null && !method.IsStatic && method.DeclaringType == classType)
				{
					this.found = method;
				}

				return node;
			}
		}
	}
}
