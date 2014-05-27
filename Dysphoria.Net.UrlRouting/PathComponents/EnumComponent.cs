// -----------------------------------------------------------------------
// <copyright file="EnumComponent.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
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
	using System.Linq;
	using System.Text.RegularExpressions;

	public class EnumComponent<EnumType> : PathComponent<EnumType>
		where EnumType : struct
	{
		public static readonly EnumComponent<EnumType> Instance = new EnumComponent<EnumType>();
		private EnumComponent()
			: base(EnumRegexPattern<EnumType>())
		{
		}

		private static string EnumRegexPattern<EType>()
		{
			var values = Enum.GetNames(typeof(EType))
				.Select(v => Regex.Escape(v));
			return string.Join("|", values);
		}

		public override EnumType FromString(string str)
		{
			return (EnumType)Enum.Parse(typeof(EnumType), str, ignoreCase: true);
		}

		public override string ToString(EnumType value)
		{
			return value.ToString();
		}
	}
}
