// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Dysphoria.Net.UrlRouting.PathComponents
{
	using System;
	using System.Text.RegularExpressions;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class StringComponent: PathComponent<string>
	{
		public static readonly StringComponent AnyStringExceptSlash = new StringComponent(@"[^/]*");
		private readonly Regex regex;

		public StringComponent(string regexString)
			: base(regexString)
		{
			this.regex = new Regex("^" + regexString + "$", RegexOptions.Compiled);
		}

		public override string FromString(string str)
		{
			return str;
		}

		public override string ToString(string value)
		{
			if (!this.regex.IsMatch(value)) throw new ArgumentException(string.Format("Provided value ({0}) violates constraint: {1}", value, this.RegexString));
			return value;
		}
	}
}
