// -----------------------------------------------------------------------
// <copyright file="IntComponent.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
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
	using System.Text.RegularExpressions;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class NullableComponent<T> : PathComponent<T?>
		where T : struct
	{
		private readonly string nullValue;
		private readonly PathComponent<T> basis;

		public NullableComponent(PathComponent<T> basis)
			: this(basis, string.Empty)
		{
		}

		public NullableComponent(PathComponent<T> basis, string nullValue)
			: base(Regex.Escape(nullValue) + "|" + basis.RegexString)
		{
			this.basis = basis;
			this.nullValue = nullValue;
		}

		public override T? FromString(string str)
		{
			return (str ?? "") == this.nullValue
				? (T?)null
				: this.basis.FromString(str);
		}

		public override string ToString(T? value)
		{
			return value == null
				? this.nullValue
				: this.basis.ToString(value.Value);
		}
	}
}
