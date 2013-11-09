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
	using System.Reflection;
	using System.Web.Mvc;
	using Dysphoria.Net.UrlRouting;

	/// <summary>
	/// Extension method for view code to be able to generate a link from an instantiated UrlPattern.
	/// </summary>
	public static class HtmlHelper_Extensions
	{
		// For calling (internal) ViewContext.FormIdGenerator (as per bclcontrib project for .NET BCL)
		private static readonly PropertyInfo FormIdGeneratorPropertyInfo = typeof(ViewContext).GetProperty("FormIdGenerator", BindingFlags.NonPublic | BindingFlags.Instance);

		/// <summary>
		/// Creates a first class 'link' object (okay really a key-value-pair of a string and a URL).
		/// This can be useful if, for example, you need to pass a collection of links to your view.
		/// </summary>
		/// <param name="linkText">The (plain) text of the link</param>
		/// <param name="location">The destination of the link</param>
		/// <returns>Pair of: text and URL</returns>
		public static KeyValuePair<string, AppLocalUrl> LinkTo(this string linkText, AppLocalUrl location)
		{
			return new KeyValuePair<string, AppLocalUrl>(linkText, location);
		}

		public static MvcHtmlString Link(this HtmlHelper self, KeyValuePair<string, AppLocalUrl> link, object htmlAttributes = null)
		{
			return self.Link(link.Key, link.Value, htmlAttributes);
		}

		public static MvcHtmlString Link(this HtmlHelper self, string linkText, AppLocalUrl location, object htmlAttributes = null)
		{
			var uri = location.Resolved(self.ViewContext.HttpContext);

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

		public static MvcForm BeginForm<U>(this HtmlHelper self, RequestPattern<U> route, Func<U, AppLocalUrl> instantiate, object htmlAttributes = null)
			where U : AbstractUrlPattern
		{
			var location = instantiate(route.Url);
			return self.BeginForm(location, route.FormMethod, htmlAttributes);
		}

		public static MvcForm BeginForm(this HtmlHelper self, RequestPattern<UrlPattern> route, object htmlAttributes = null)
		{
			return self.BeginForm(route.Url.Url, route.FormMethod, htmlAttributes);
		}

		public static MvcForm BeginForm(this HtmlHelper self, AppLocalUrl location, FormMethod method = FormMethod.Post, object htmlAttributes = null)
		{
			var viewContext = self. ViewContext;
			var actionUri = location.Resolved(viewContext.HttpContext);
			var tagBuilder = new TagBuilder("form");
			if (htmlAttributes != null)
			{
				var attributeDictionary = (htmlAttributes as IDictionary<string, object>) ?? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
				tagBuilder.MergeAttributes(attributeDictionary);
			}

			// action is implicitly generated, so htmlAttributes take precedence.
			tagBuilder.MergeAttribute("action", actionUri);
			// method is an explicit parameter, so it takes precedence over the htmlAttributes.
			tagBuilder.MergeAttribute("method", HtmlHelper.GetFormMethodString(method), true);

			bool traditionalJavascriptEnabled = viewContext.ClientValidationEnabled &&
												!viewContext.UnobtrusiveJavaScriptEnabled;

			if (traditionalJavascriptEnabled)
			{
				// forms must have an ID for client validation
				tagBuilder.GenerateId(GetFormIdGenerator(viewContext));
			}

			viewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
			var form = new MvcForm(viewContext);

			if (traditionalJavascriptEnabled)
			{
				viewContext.FormContext.FormId = tagBuilder.Attributes["id"];
			}

			return form;
		}

		// Calls (internal) ViewContext.FormIdGenerator (as per bclcontrib project for .NET BCL)
		private static string GetFormIdGenerator(ViewContext viewContext)
		{
			return ((Func<string>)FormIdGeneratorPropertyInfo.GetValue(viewContext, null))();
		}
	}
}
