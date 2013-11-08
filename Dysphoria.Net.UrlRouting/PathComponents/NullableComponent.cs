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
	using System;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public abstract class NullableComponent<BaseType, NullType> : PathComponent<NullType>
	{
		private readonly NullType nullValue;
		private readonly string nullValueString;
		private readonly PathComponent<BaseType> basis;

		public NullableComponent(PathComponent<BaseType> basis, string nullValueString, NullType nullValue)
			: base(Regex.Escape(nullValueString) + "|" + basis.RegexString)
		{
			if (nullValueString == null) throw new ArgumentNullException("nullValueString cannot be 'null'!");

			this.basis = basis;
			this.nullValueString = nullValueString;
			this.nullValue = nullValue;
		}

		public override NullType FromString(string str)
		{
			return (str ?? "") == this.nullValueString
				? this.nullValue
				: this.ToNullType(this.basis.FromString(str));
		}

		public override string ToString(NullType value)
		{
			return this.IsNull(value)
				? this.nullValueString
				: this.basis.ToString(ToBaseType(value));
		}

		protected abstract NullType ToNullType(BaseType value);

		protected abstract BaseType ToBaseType(NullType nonNullValue);

		protected abstract bool IsNull(NullType possiblyNullValue);
	}
}
