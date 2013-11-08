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
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;

	public class BoolComponent : PathComponent<bool>
	{
		public static readonly BoolComponent Default = new BoolComponent(
			"1",
			"0", "", "false", "no", "f", "n");

		private readonly string defaultTrueValue, defaultFalseValue;
		private readonly IEnumerable<string> falseValues;

		public BoolComponent(string defaultTrueValue, params string[] falseValues)
			: base(Regex.Escape(defaultTrueValue) + "|" + string.Join("|", falseValues.Select(f => Regex.Escape(f))))
		{
			if (defaultTrueValue == null) throw new ArgumentNullException("defaultTrueValue cannot be 'null'");
			if (falseValues == null) throw new ArgumentNullException("falseValues cannot be 'null' (but can be empty)");

			this.defaultTrueValue = defaultTrueValue;
			this.falseValues = falseValues;
			this.defaultFalseValue = falseValues.FirstOrDefault();
		}

		public override bool FromString(string str)
		{
			return
				defaultTrueValue.Equals(str, StringComparison.CurrentCultureIgnoreCase) ||
				!falseValues.Contains(str, StringComparer.CurrentCultureIgnoreCase);
		}

		public override string ToString(bool value)
		{
			return value
				? this.defaultTrueValue
				: this.defaultFalseValue;
		}
	}
}
