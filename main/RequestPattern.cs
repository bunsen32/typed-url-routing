// -----------------------------------------------------------------------
// <copyright file="RequestPattern.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Uk.Co.Cygnets.UrlRouting
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class RequestPattern<UrlPatternType>: AbstractRequestPattern
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
