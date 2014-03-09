// -----------------------------------------------------------------------
// <copyright file="MvcQueryStringEncoding.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
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
	/// TODO: Update summary.
	/// </summary>
	public class MvcQueryStringEncoding<T> : QueryStringEncoding<T>
	{
		public static readonly MvcQueryStringEncoding<T> Instance = new MvcQueryStringEncoding<T>();

		private MvcQueryStringEncoding()
		{
		}

		public override RouteValueDictionary ToDictionary(T value)
		{
			return (value as RouteValueDictionary) ?? new RouteValueDictionary(value);
		}

		public override T FromDictionary(ControllerContext cx)
		{
			return MvcDecoder<T>.Instance.FromQueryString(cx);
		}
	}
}
