// -----------------------------------------------------------------------
// <copyright file="UrlPatternTests.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may
// not use this file except in compliance with the License. Copy of
// license at: http://www.apache.org/licenses/LICENSE-2.0
//
// This software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS. See License for specific permissions and limitations.
// -----------------------------------------------------------------------
namespace Dysphoria.Net.UrlRouting.Test
{
	using System;
	using Dysphoria.Net.UrlRouting.PathComponents;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class UrlPatternTests : Urls
	{
		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void RejectsTooFewParameters()
		{
			new TestUrlPattern(1, "");
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void RejectsTooManyParameters()
		{
			new TestUrlPattern(1, "", null, null);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void RejectsPathStartingWithSlash()
		{
			new TestUrlPattern(0, "/bob");
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void RejectsPatternArityTooSmall()
		{
			new TestUrlPattern(1, "bob", Int);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void RejectsPatternArityTooLarge()
		{
			new TestUrlPattern(1, "bob{0}/{1}", Int);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void RejectsWrongNumberOfPathComponents()
		{
			new TestUrlPattern(2, "bob{0}/{1}", Int, Arg("named", Int));
		}

		[TestMethod]
		public void AcceptsPatternWithCorrectParameters()
		{
			new TestUrlPattern(2, "bob/{0}", Int, Arg("named", Int));
		}

		private class TestUrlPattern : AbstractUrlPattern
		{
			public TestUrlPattern(int arity, string pattern, params UrlArgument[] parameters)
				: base(arity, pattern, parameters)
			{
			}
		}
	}
}