// -----------------------------------------------------------------------
// <copyright file="PotentialUrl.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Uk.Co.Cygnets.UrlRouting
{
	/// <summary>
	/// TODO: Update summary.
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
				return string.IsNullOrEmpty(querystring)
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
