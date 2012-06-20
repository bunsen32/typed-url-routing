// -----------------------------------------------------------------------
// <copyright file="ControllerRouteMapper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Uk.Co.Cygnets.UrlRouting
{
	using System;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Web.Mvc;
	using System.Web.Routing;
	using Uk.Co.Cygnets.UrlRouting.Handlers;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class ControllerRouteMapper<C>
		where C : Controller
	{
		private readonly RouteCollection routes;
		private readonly string controllerName;

		public ControllerRouteMapper(RouteCollection routes)
		{
			this.routes = routes;
			this.controllerName = GetControllerName(typeof(C));
		}

		public RouteCollection Routes { get { return this.routes; } }

		public ControllerRouteMapper<C> MapRoute(RequestPattern<UrlPattern> pattern, Expression<Func<C, Func<ActionResult>>> handler)
		{
			var method = GetMethodInfo(handler);
			var methodFunc = handler.Compile();
			this.AddRouteHandler(pattern, method, (c, param) => methodFunc.Invoke(c).Invoke());
			
			return this; // To allow for method chaining.
		}

		public ControllerRouteMapper<C> MapRoute<P1>(RequestPattern<UrlPattern<P1>> pattern, Expression<Func<C, Func<P1, ActionResult>>> handler)
		{
			var method = GetMethodInfo(handler);
			var methodFunc = handler.Compile();
			var url = pattern.Url;
			this.AddRouteHandler(pattern, method, (c, param) => methodFunc.Invoke(c).Invoke(
				url.Param1.FromString(param[0])));

			return this; // To allow for method chaining.
		}

		public ControllerRouteMapper<C> MapRoute<P1, P2>(RequestPattern<UrlPattern<P1, P2>> pattern, Expression<Func<C, Func<P1, P2, ActionResult>>> handler)
		{
			var method = GetMethodInfo(handler);
			var methodFunc = handler.Compile();
			var url = pattern.Url;
			this.AddRouteHandler(pattern, method, (c, param) => methodFunc.Invoke(c).Invoke(
				url.Param1.FromString(param[0]),
				url.Param2.FromString(param[1])));

			return this; // To allow for method chaining.
		}

		public ControllerRouteMapper<C> MapRoute<P1, P2, P3>(RequestPattern<UrlPattern<P1, P2, P3>> pattern, Expression<Func<C, Func<P1, P2, P3, ActionResult>>> handler)
		{
			var method = GetMethodInfo(handler);
			var methodFunc = handler.Compile();
			var url = pattern.Url;
			this.AddRouteHandler(pattern, method, (c, param) => methodFunc.Invoke(c).Invoke(
				url.Param1.FromString(param[0]),
				url.Param2.FromString(param[1]),
				url.Param3.FromString(param[2])));

			return this; // To allow for method chaining.
		}

		public void AddRouteHandler(AbstractRequestPattern pattern, MethodInfo method, Func<C, string[], ActionResult> handler)
		{
			var actionName = GetActionName(pattern.Method, method);
			this.routes.AddRoute(pattern, new ControllerRouteHandler<C>(pattern, this.controllerName, actionName, handler));
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

		internal static string GetActionName(HttpMethod verb, MethodInfo method)
		{
			var verbString = verb.ToString();
			var methodName = method.Name;
			var startsWithVerb = methodName.Length > verbString.Length && methodName.StartsWith(verbString, ignoreCase: true, culture: null);
			return startsWithVerb 
				? methodName.Substring(verbString.Length) 
				: methodName;
		}

		private static MethodInfo GetMethodInfo(Expression method)
		{
			var lambda = method as LambdaExpression;
			if (lambda == null) throw new ArgumentException("Argument is not a lambda expression (c => c.Thing)");
			var convert = lambda.Body;
			var body = (convert.NodeType == ExpressionType.Convert)
				? ((UnaryExpression)convert).Operand as MethodCallExpression
				: convert as MethodCallExpression;
			if (body == null) throw new ArgumentException("Argument not in correct form (c => c.Thing)");
			var methodInfoValue = body
				.Arguments.OfType<ConstantExpression>()
				.Where(exp => exp.Type == typeof(MethodInfo))
				.Select(exp => (MethodInfo)exp.Value)
				.FirstOrDefault();
			if (methodInfoValue == null) throw new ArgumentException("Cannot find method name in expression.");
			return methodInfoValue;
		}
	}
}
