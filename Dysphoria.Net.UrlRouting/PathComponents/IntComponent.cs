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
	/// A path component which is a (positive or nagative) integer.
	/// </summary>
	public class IntComponent : PathComponent<int>
	{
		public static readonly IntComponent Instance = new IntComponent();

		private IntComponent()
			: base(@"[-+]?\d+")
		{
		}

		public override int FromString(string str)
		{
			try
			{
				return int.Parse(str);
			}
			catch (OverflowException ov)
			{
				throw new FormatException("String value is out of range", ov);
			}
		}

		public override string ToString(int value)
		{
			return value.ToString();
		}
	}
}
