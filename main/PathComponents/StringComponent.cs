// -----------------------------------------------------------------------
// <copyright file="StringComponent.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may
// not use this file except in compliance with the License. Copy of
// license at: http://www.apache.org/licenses/LICENSE-2.0
//
// This software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS. See License for specific permissions and limitations.
// -----------------------------------------------------------------------
namespace Dysphoria.Net.UrlRouting.PathComponents
{
	using System;
	using System.Text.RegularExpressions;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class StringComponent : PathComponent<string>
	{
		public static readonly StringComponent AnyStringExceptSlash = new StringComponent(@"[^/]*");
		private readonly Regex regex;

		public StringComponent(string regexString)
			: base(regexString)
		{
			this.regex = new Regex("^" + regexString + "$", RegexOptions.Compiled);
		}

		public Regex Regex { get { return this.regex; } }

		public override string FromString(string str)
		{
			return str;
		}

		public override string ToString(string value)
		{
			if (!this.regex.IsMatch(value ?? "")) throw new InvalidUrlComponentValueException(string.Format("Provided value “{0}” violates constraint: {1}", value, this.RegexString));
			return value;
		}
	}
}
