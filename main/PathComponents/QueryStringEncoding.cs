// -----------------------------------------------------------------------
// <copyright file="QueryComponent.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Dysphoria.Net.UrlRouting.PathComponents
{
	using System.Collections.Specialized;
	using System.Web.Routing;

	/// <summary>
	/// Kind of fake UrlComponent (since it does not do any direct conversion to/from strings). Placeholder in UrlPattern, always the last
	/// parameter, and encodes/decodes the object which represents the (remainder of) the query string.
	/// </summary>
	public abstract class QueryStringEncoding<T> : UrlArgument<T>
	{
		public abstract RouteValueDictionary ToDictionary(T value);

		public abstract T FromDictionary(NameValueCollection dict);
	}
}
