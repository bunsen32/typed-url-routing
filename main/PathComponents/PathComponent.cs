// -----------------------------------------------------------------------
// <copyright file="PathComponent.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Uk.Co.Cygnets.UrlRouting
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Uk.Co.Cygnets.Formats;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class PathComponent<T> : IPathComponent<T>, IFormat<T>
	{
		private readonly IFormat<T> stringFormat;
		private readonly string regexString;

		public PathComponent(IFormat<T> stringFormat, string regexString)
		{
			this.stringFormat = stringFormat;
			this.regexString = regexString;
		}

		public IFormat<T> StringFormat
		{
			get { return this.stringFormat; }
		}

		public string RegexString
		{
			get { return this.regexString; }
		}

		public T FromString(string str)
		{
			return this.stringFormat.FromString(str);
		}

		public string ToString(T value)
		{
			return this.stringFormat.ToString(value);
		}
	}
}
