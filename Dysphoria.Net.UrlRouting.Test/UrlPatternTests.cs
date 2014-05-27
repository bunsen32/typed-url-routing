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
	using Xunit;

	public class UrlPatternTests : Urls
	{
		[Fact]
		public void RejectsTooFewParameters()
		{
			Assert.Throws<ArgumentException>(()=>
				new TestUrlPattern(1, ""));
		}

		[Fact]
		public void RejectsTooManyParameters()
		{
			Assert.Throws<ArgumentException>(() =>
				new TestUrlPattern(1, "", null, null));
		}

		[Fact]
		public void RejectsPathStartingWithSlash()
		{
			Assert.Throws<ArgumentException>(() =>
				new TestUrlPattern(0, "/bob"));
		}

		[Fact]
		public void RejectsPatternArityTooSmall()
		{
			Assert.Throws<ArgumentException>(() =>
				new TestUrlPattern(1, "bob", Int));
		}

		[Fact]
		public void RejectsPatternArityTooLarge()
		{
			Assert.Throws<ArgumentException>(() =>
				new TestUrlPattern(1, "bob{0}/{1}", Int));
		}

		[Fact]
		public void RejectsWrongNumberOfPathComponents()
		{
			Assert.Throws<ArgumentException>(() =>
				new TestUrlPattern(2, "bob{0}/{1}", Int, Arg("named", Int)));
		}

		[Fact]
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