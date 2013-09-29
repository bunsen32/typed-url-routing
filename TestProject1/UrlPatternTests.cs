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
		public void RejectsPathStartingWithoutSlash()
		{
			new TestUrlPattern(0, "bob");
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void RejectsPatternArityTooSmall()
		{
			new TestUrlPattern(1, "/bob", Int);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void RejectsPatternArityTooLarge()
		{
			new TestUrlPattern(1, "/bob{0}/{1}", Int);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void RejectsWrongNumberOfPathComponents()
		{
			new TestUrlPattern(2, "/bob{0}/{1}", Int, Arg("named", Int));
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