// -----------------------------------------------------------------------
// <copyright file="PotentialUrl.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may
// not use this file except in compliance with the License. Copy of
// license at: http://www.apache.org/licenses/LICENSE-2.0
//
// This software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS. See License for specific permissions and limitations.
// -----------------------------------------------------------------------
namespace Dysphoria.Net.UrlRouting
{
	using System;
	using System.Text;
	using System.Web;
	using Dysphoria.Net.UrlRouting.MvcUrlUtilities;

	/// <summary>
	/// Abstraction of a URL which is relative to an app. Can be converted into
	/// a real URL via the <see cref="#Resolved()"/> method.
	/// <para>This is what you get when you fill out the parameters of a <see cref="UrlPattern"/>.
	/// </para>
	/// Allows <see cref="HtmlHelperExtensions.Link"/> to be strongly typed.
	/// </summary>
	public class AppLocalUrl: IEquatable<AppLocalUrl>
	{
		private readonly string path;
		private readonly string querystring;
		private readonly string fragmentIdentifier;

		public AppLocalUrl(string urlPath, string querystring = null, string fragmentIdentifier = null)
		{
			if (urlPath == null) throw new ArgumentNullException("urlPath");

			this.path = urlPath;
			this.querystring = querystring ?? "";
			this.fragmentIdentifier = fragmentIdentifier ?? "";
		}

		public string Path { get { return this.path; } }

		public string Querystring { get { return this.querystring; } }

		public string FragmentIdentifier { get { return this.fragmentIdentifier; } }

		public string ApplicationRelativeUrl
		{
			get
			{
				var result = new StringBuilder(this.Path);
				if (!string.IsNullOrEmpty(this.Querystring))
				{
					result.Append("?");
					result.Append(this.Querystring);
				}

				if (!string.IsNullOrEmpty(this.FragmentIdentifier))
				{
					result.Append("#");
					result.Append(this.FragmentIdentifier);
				}

				return result.ToString();
			}
		}

		public string Resolved(HttpContextBase httpContext)
		{
			return PathHelpers.GenerateClientUrl(httpContext, "~/" + this.ApplicationRelativeUrl);
		}

		public AppLocalUrl WithFragment(string fragmentIdentifier)
		{
			return (fragmentIdentifier ?? "") == this.FragmentIdentifier
				? this
				: new AppLocalUrl(this.Path, this.Querystring, fragmentIdentifier);
		}

		public override string ToString()
		{
			return this.ApplicationRelativeUrl;
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as AppLocalUrl);
		}

		public static bool operator !=(AppLocalUrl first, AppLocalUrl second)
		{
			return !(first == second);
		}

		public static bool operator ==(AppLocalUrl first, AppLocalUrl second)
		{
			return (object)first == null
				? (object)second == null
				: first.Equals(second);
		}

		public bool Equals(AppLocalUrl other)
		{
			return (object)other != null
				&& this.Path == other.Path
				&& this.Querystring == other.Querystring;
		}

		public override int GetHashCode()
		{
			return unchecked((this.Path.GetHashCode() * 41) ^ (this.Querystring ?? "").GetHashCode());
		}
	}
}
