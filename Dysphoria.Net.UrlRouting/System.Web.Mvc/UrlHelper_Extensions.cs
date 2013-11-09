// -----------------------------------------------------------------------
// <copyright file="HtmlHelperExtensions.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may
// not use this file except in compliance with the License. Copy of
// license at: http://www.apache.org/licenses/LICENSE-2.0
//
// This software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS. See License for specific permissions and limitations.
// -----------------------------------------------------------------------
namespace System.Web.Mvc
{
	using Dysphoria.Net.UrlRouting;

	/// <summary>
	/// Extension method for view code to be able to generate a link from an instantiated UrlPattern.
	/// </summary>
	public static class UrlHelper_Extensions
	{
		public static string Of(this UrlHelper self, AppLocalUrl location)
		{
			return location.Resolved(self.RequestContext.HttpContext);
		}
	}
}
