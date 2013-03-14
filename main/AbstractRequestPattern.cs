// -----------------------------------------------------------------------
// <copyright file="AbstractRequestPattern.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Dysphoria.Net.UrlRouting
{
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
