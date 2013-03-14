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
	/// <summary>
	/// (Frankly weird) abstraction of a URL's path and querystring.
	/// Allows at least <see cref="HtmlHelperExtensions.Link"/> to be strongly typed.
	/// </summary>
	public class PotentialUrl
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

		public override string ToString()
		{
			return this.ApplicationAbsoluteUrl;
		}
	}
}
