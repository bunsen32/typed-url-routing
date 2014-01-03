// -----------------------------------------------------------------------
// <copyright file="UrlGenerationTests.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
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
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class UrlGenerationTests : Urls
	{
		[TestMethod]
		public void SimplePatternGeneratesSimpleAppLocalUrl()
		{
			var p = Path("abc/123/xyz");
			Assert.AreEqual("abc/123/xyz", p.Url.ToString());
		}

		[TestMethod]
		public void QueryArgsArePopulated()
		{
			var p = Path("abc", Arg("bob", Int));
			Assert.AreEqual("abc?bob=23", p[23].ToString());
		}

		[TestMethod]
		public void NullQueryArgsAreOmitted()
		{
			var p = Path("abc", Arg("bob", Int.Or(null)));
			Assert.AreEqual("abc", p[null].ToString());
		}

		[TestMethod]
		public void NullableQueryArgsArePopulated()
		{
			var p = Path("abc", Arg("bob", Int.Or(null)));
			Assert.AreEqual("abc?bob=23", p[23].ToString());
		}

		[TestMethod]
		public void NullQueryArgsAreReplaced()
		{
			var p = Path("abc", Arg("bob", Int.Or("nothing")));
			Assert.AreEqual("abc?bob=nothing", p[null].ToString());
		}

		[TestMethod]
		public void PathArgsArePopulated()
		{
			var p = Path("abc/{0}", Int);
			Assert.AreEqual("abc/23", p[23].ToString());
		}

		[TestMethod]
		public void NullPathArgsAreBlank()
		{
			var p = Path("abc/{0}", Int.Or(null));
			Assert.AreEqual("abc/", p[null].ToString());
		}

		[TestMethod]
		public void NullablePathArgsArePopulated()
		{
			var p = Path("abc/{0}", Int.Or(null));
			Assert.AreEqual("abc/23", p[23].ToString());
		}

		[TestMethod]
		public void NullPathArgsAreReplaced()
		{
			var p = Path("abc/{0}", Int.Or("nothing"));
			Assert.AreEqual("abc/nothing", p[null].ToString());
		}
	}
}
