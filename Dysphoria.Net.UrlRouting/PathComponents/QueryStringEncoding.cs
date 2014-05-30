// -----------------------------------------------------------------------
// <copyright file="QueryStringEncoding.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may
// not use this file except in compliance with the License. Copy of
// license at: http://www.apache.org/licenses/LICENSE-2.0
//
// This software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS. See License for specific permissions and limitations.
// -----------------------------------------------------------------------
namespace Dysphoria.Net.UrlRouting.PathComponents
{
	using System.Web.Mvc;
	using System.Web.Routing;

	/// <summary>
	/// Kind of fake UrlComponent (since it does not do any direct conversion to/from strings). Placeholder in UrlPattern, always the last
	/// parameter, and encodes/decodes the object which represents the (remainder of) the query string.
	/// </summary>
	public abstract class QueryStringEncoding<T> : UrlArgument<T>
	{
		public abstract RouteValueDictionary ToDictionary(T value);

		public abstract T FromDictionary(ControllerContext cx);
	}
}
