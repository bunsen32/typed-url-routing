// -----------------------------------------------------------------------
// <copyright file="HtmlExtensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace System.Web.Mvc.Html
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Web.Mvc;
	using Uk.Co.Cygnets.UrlRouting;
	using Uk.Co.Cygnets.UrlRouting.MvcUrlUtilities;

	/// <summary>
	/// TODO: Update summary.
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

			tagBuilder.MergeAttribute("href", uri.ToString());
			var linkString = tagBuilder.ToString(TagRenderMode.Normal);
			return MvcHtmlString.Create(linkString);
		}
	}
}
