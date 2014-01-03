// -----------------------------------------------------------------------
// <copyright file="SimpleUrlComponent.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
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
	public abstract class SimpleUrlComponent<T> : UrlArgument<T>
	{
		private readonly string regexString;

		public SimpleUrlComponent(string regexString)
			: base()
		{
			this.regexString = regexString;
		}

		public string RegexString
		{
			get { return this.regexString; }
		}

		/// <summary>
		/// Converts a string to a value of type <typeparamref name="T"/>.
		/// Should deal with nulls (at least in as far as they should be treated
		/// like empty strings).
		/// </summary>
		public abstract T FromString(string str);

		/// <summary>
		/// Converts the value of type <typeparamref name="T"/> to a string.
		/// May return null, indicating that the value is missing. A missing value
		/// will equate to "" in a path component, or an omitted query parameter.
		/// </summary>
		public abstract string ToString(T value);
	}
}
