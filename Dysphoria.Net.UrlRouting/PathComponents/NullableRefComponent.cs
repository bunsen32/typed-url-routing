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
	using System;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class NullableRefComponent<T> : NullableComponent<T, T>
		where T : class
	{
		public NullableRefComponent(PathComponent<T> basis)
			: this(basis, string.Empty)
		{
		}

		public NullableRefComponent(PathComponent<T> basis, string nullValueString)
			: base(basis, nullValueString, null)
		{
		}

		protected override T ToBaseType(T nonNullValue)
		{
			if (nonNullValue == null) throw new ArgumentNullException();
			return nonNullValue;
		}

		protected override T ToNullType(T value)
		{
			return value;
		}

		protected override bool IsNull(T possiblyNullValue)
		{
			return object.ReferenceEquals(possiblyNullValue, null);
		}
	}
}
