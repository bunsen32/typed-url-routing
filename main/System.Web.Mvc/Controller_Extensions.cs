// -----------------------------------------------------------------------
// <copyright file="ControllerExtensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace System.Web.Mvc
{
	using Dysphoria.Net.UrlRouting;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public static class Controller_Extensions
	{
		public static ActionResult RedirectTo(this ControllerBase self, UrlPattern zeroArgumentPath, bool permanent = false)
		{
			return self.RedirectTo(zeroArgumentPath.Url, permanent);
		}

		public static ActionResult RedirectTo(this ControllerBase self, PotentialUrl location, bool permanent = false)
		{
			var uri = location.Resolved(self.ControllerContext.HttpContext);
			return new RedirectResult(uri, permanent);
		}
	}
}
