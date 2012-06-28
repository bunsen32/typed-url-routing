// -----------------------------------------------------------------------
// <copyright file="QueryComponent.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Uk.Co.Cygnets.UrlRouting.PathComponents
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
using System.Web.Routing;
	using System.Web.Mvc;

	/// <summary>
	/// Kind of fake UrlComponent (since it does not do any direct conversion to/from strings). Placeholder in UrlPattern, always the last
	/// parameter, and encodes/decodes the object which represents the (remainder of) the query string.
	/// </summary>
	public class QueryStringEncoding<T> : UrlArgument<T>
	{
		public RouteValueDictionary ToDictionary(T value)
		{
			return (value as RouteValueDictionary) ?? new RouteValueDictionary(value);
		}

		public T FromDictionary(RouteValueDictionary dict)
		{
			throw new NotImplementedException();
		}
	}
}
