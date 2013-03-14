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
namespace System.Web.Mvc.Html
{
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;
	using Dysphoria.Net.UrlRouting;
	using Dysphoria.Net.UrlRouting.MvcUrlUtilities;

	/// <summary>
	/// Extension method for view code to be able to generate a link from an instantiated UrlPattern.
	/// </summary>
	public static class HtmlHelperExtensions
	{
		public static MvcHtmlString Link(this HtmlHelper self, string linkText, PotentialUrl location, object htmlAttributes = null)
		{
			var uri = PathHelpers.GenerateClientUrl(self.ViewContext.HttpContext, "~" + location.ToString());

			if (String.IsNullOrEmpty(linkText))
				throw new ArgumentException("Link text not allowed to be null or empty", "linkText");

			var tagBuilder = new TagBuilder("a")
			{
				InnerHtml = (!String.IsNullOrEmpty(linkText)) ? HttpUtility.HtmlEncode(linkText) : String.Empty
			};

			if (htmlAttributes != null)
			{
				var attributeDictionary = (htmlAttributes as IDictionary<string, object>) ?? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
				tagBuilder.MergeAttributes(attributeDictionary);
			}

			tagBuilder.MergeAttribute("href", uri);
			var linkString = tagBuilder.ToString(TagRenderMode.Normal);
			return MvcHtmlString.Create(linkString);
		}
	}
}
