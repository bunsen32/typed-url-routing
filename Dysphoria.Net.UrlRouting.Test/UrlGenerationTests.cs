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
	using Xunit;

	public class UrlGenerationTests : Urls
	{
		[Fact]
		public void SimplePatternGeneratesSimpleAppLocalUrl()
		{
			var p = Path("abc/123/xyz");
			Assert.Equal("abc/123/xyz", p.Url.ToString());
		}

		[Fact]
		public void QueryArgsArePopulated()
		{
			var p = Path("abc", Arg("bob", Int));
			Assert.Equal("abc?bob=23", p[23].ToString());
		}

		[Fact]
		public void NullQueryArgsAreOmitted()
		{
			var p = Path("abc", Arg("bob", Int.Or(null)));
			Assert.Equal("abc", p[null].ToString());
		}

		[Fact]
		public void NullableQueryArgsArePopulated()
		{
			var p = Path("abc", Arg("bob", Int.Or(null)));
			Assert.Equal("abc?bob=23", p[23].ToString());
		}

		[Fact]
		public void NullQueryArgsAreReplaced()
		{
			var p = Path("abc", Arg("bob", Int.Or("nothing")));
			Assert.Equal("abc?bob=nothing", p[null].ToString());
		}

		[Fact]
		public void PathArgsArePopulated()
		{
			var p = Path("abc/{0}", Int);
			Assert.Equal("abc/23", p[23].ToString());
		}

		[Fact]
		public void NullPathArgsAreBlank()
		{
			var p = Path("abc/{0}", Int.Or(null));
			Assert.Equal("abc/", p[null].ToString());
		}

		[Fact]
		public void NullablePathArgsArePopulated()
		{
			var p = Path("abc/{0}", Int.Or(null));
			Assert.Equal("abc/23", p[23].ToString());
		}

		[Fact]
		public void NullPathArgsAreReplaced()
		{
			var p = Path("abc/{0}", Int.Or("nothing"));
			Assert.Equal("abc/nothing", p[null].ToString());
		}
	}
}
