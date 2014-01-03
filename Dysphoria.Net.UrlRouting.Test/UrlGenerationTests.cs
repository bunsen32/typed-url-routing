namespace Dysphoria.Net.UrlRouting.Test
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class UrlGenerationTests : Urls
	{
		[TestMethod]
		public void SimplePatternGeneratesSimpleAppLocalUrl()
		{
			var pattern = Path("abc/123/xyz");
			Assert.AreEqual("abc/123/xyz", pattern.Url.ToString());
		}

		[TestMethod]
		public void QueryArgsArePopulated()
		{
			var pattern = Path("abc", Arg("bob", Int));
			Assert.AreEqual("abc?bob=23", pattern[23].ToString());
		}

		[TestMethod]
		public void NullableQueryArgsAreReplaced()
		{
			var pattern = Path("abc", Arg("bob", Int.Or("nothing")));
			Assert.AreEqual("abc?bob=nothing", pattern[null].ToString());
		}
	}
}
