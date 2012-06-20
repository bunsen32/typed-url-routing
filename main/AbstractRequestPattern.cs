// -----------------------------------------------------------------------
// <copyright file="AbstractRequestPattern.cs" company="">
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
	public abstract class AbstractRequestPattern
	{
		private readonly HttpMethod method;

		public AbstractRequestPattern(HttpMethod method)
		{
			this.method = method;
		}

		public HttpMethod Method { get { return this.method; } }

		protected abstract AbstractUrlPattern AbstractUrlPattern { get; }

		public AbstractUrlPattern Url { get { return this.AbstractUrlPattern; } }
	}
}
