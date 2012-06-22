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

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public static class HtmlHelperExtensions
	{
		public static MvcHtmlString Link(this HtmlHelper self, string linkText, Uri uri, object htmlAttributes = null)
		{
			if (String.IsNullOrEmpty(linkText))
				throw new ArgumentException("Link text not allowed to be null or empty", "linkText");

			TagBuilder tagBuilder = new TagBuilder("a")
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
