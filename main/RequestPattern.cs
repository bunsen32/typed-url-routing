// -----------------------------------------------------------------------
// <copyright file="RequestPattern.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
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
	/// Represents a combination of HTTP method and URL pattern. Matches any
	/// request (or generates requests) with that HTTP method, and conforming
	/// to the URL pattern.
	/// </summary>
	/// <remarks>
	/// <see cref="HttpMethod"/> may also represent multiple methods, e.g. 'Any'.
	/// </remarks>
	public class RequestPattern<UrlPatternType> : AbstractRequestPattern
		where UrlPatternType : AbstractUrlPattern
	{
		private readonly UrlPatternType url;

		public RequestPattern(HttpMethod method, UrlPatternType url)
			: base(method)
		{
			this.url = url;
		}

		protected override AbstractUrlPattern AbstractUrlPattern { get { return this.url; } }

		public new UrlPatternType Url { get { return this.url; } }
	}

	public class RequestPattern<UrlPatternType, PayloadType> : RequestPattern<UrlPatternType>
		where UrlPatternType : AbstractUrlPattern
	{
		private readonly object payloadType;

		public RequestPattern(HttpMethod method, UrlPatternType url, object payloadType)
			: base(method, url)
		{
			this.payloadType = payloadType;
		}
	}
}
