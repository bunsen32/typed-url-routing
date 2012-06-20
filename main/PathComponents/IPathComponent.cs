// -----------------------------------------------------------------------
// <copyright file="IPathComponent.cs" company="">
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
using System.Text.RegularExpressions;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public interface IPathComponent<T>
	{
		T FromString(string str);

		string ToString(T value);

		string RegexString { get; }
	}
}
