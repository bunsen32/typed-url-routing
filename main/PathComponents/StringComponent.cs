// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Uk.Co.Cygnets.UrlRouting.PathComponents
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
using Uk.Co.Cygnets.Formats;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class StringComponent: PathComponent<string>
	{
		public static readonly StringComponent AnyStringExceptSlash = new StringComponent(@"[^/]*");

		public StringComponent(string regexString)
			: base(Formats.StringFormat.Instance, regexString)
		{
		}
	}
}
