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
	using System.Web;
	using Dysphoria.Net.UrlRouting;
	using Dysphoria.Net.UrlRouting.MvcUrlUtilities;

	/// <summary>
	/// (Frankly weird) abstraction of a URL's path and querystring.
	/// Allows at least <see cref="HtmlHelperExtensions.Link"/> to be strongly typed.
	/// </summary>
	public class PotentialUrl: IEquatable<PotentialUrl>
	{
		private readonly string path;
		private readonly string querystring;

		public PotentialUrl(string urlPath, string querystring)
		{
			this.path = urlPath;
			this.querystring = querystring;
		}

		public string Path { get { return this.path; } }

		public string Querystring { get { return this.querystring; } }

		public string ApplicationAbsoluteUrl
		{
			get
			{
				return string.IsNullOrEmpty(this.Querystring)
					? this.Path
					: this.Path + "?" + this.Querystring;
			}
		}

		public string Resolved(HttpContextBase httpContext)
		{
			return PathHelpers.GenerateClientUrl(httpContext, "~" + this.ToString());
		}

		public override string ToString()
		{
			return this.ApplicationAbsoluteUrl;
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as PotentialUrl);
		}

		public static bool operator !=(PotentialUrl first, PotentialUrl second)
		{
			return !(first == second);
		}

		public static bool operator ==(PotentialUrl first, PotentialUrl second)
		{
			return (object)first == null
				? (object)second == null
				: first.Equals(second);
		}

		public bool Equals(PotentialUrl other)
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
