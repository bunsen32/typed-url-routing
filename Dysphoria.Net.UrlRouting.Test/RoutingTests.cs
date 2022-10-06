// -----------------------------------------------------------------------
// <copyright file="RoutingTests.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
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
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System.Text;
	using Dysphoria.Net.UrlRouting;
	using FakeHost;
	using Xunit;
	using Xunit.Extensions;
	using IO = System.IO;

	public class RouteTests: Urls
	{
		static RouteTests()
		{
			Browser.InitializeAspNetRuntime(IO.Path.GetFullPath(@"..\..\..\Dysphoria.Net.UrlRouting.TestApp"));
		}

		[Fact]
		public void FakeHostWorks()
		{
			Assert.Equal("Hello", GetStringResultOrNull(""));
		}

		[Fact]
		public void AsyncActionMethodsWork()
		{
			Assert.Equal("Hello async!", GetStringResultOrNull("async"));
		}

		[Fact]
		public void NonExistantGives404()
		{
			Assert.Null(GetStringResultOrNull("non-existant/path"));
		}

		[Fact]
		public void LiteralPathMatches()
		{
			Assert.Equal("Literal1", GetStringResult("a/b/c"));
		}

		[Theory,
		InlineData("a/99/c", "arg=99"),
		InlineData("a/0/c", "arg=0"),
		InlineData("a/-99/c", "arg=-99"),
		InlineData("a/99.9/c", null),
		InlineData("a/2b/c", null)]
		public void SingleIntArgMatches(string path, string expectedResult)
		{
			Assert.Equal(expectedResult, GetStringResultOrNull(path));
		}

		[Theory]
		[InlineData("2args/99/bob", "arg0=99;arg1=bob")]
		[InlineData("2args/99", null)]
		[InlineData("2args//bob", null)]
		public void TwoArgsMatch(string path, string expectedResult)
		{
			Assert.Equal(expectedResult, GetStringResultOrNull(path));
		}

		[Fact(Skip = "Not working yet. Cannot yet match across path segments.")]
		public void EmptyStringMatches()
		{
			Assert.Equal("arg=", GetStringResult("1string/"));
		}

		[Fact]
		public void QueryStringDateUsesUSDateFormat()
		{
			Assert.Equal("from=20010102;to=20010304", GetStringResult("dates?From=01/02/2001&To=03/04/2001"));
		}

		[Fact]
		public void FormDateUsesLocalDateFormat()
		{
			var form = new NameValueCollection(2);
			form["From"] = "01/02/2001";
			form["To"] = "03/04/2001";
			Assert.Equal("from=20010201;to=20010403", PostStringResultOrNull("dates", form));
		}

		[Fact]
		public void FileUploadWorks()
		{
			using (var browser = new Browser())
			{
				var form = new Dictionary<string, object>(2);
				form["Name"] = "bob";
				form["File"] = new FormUpload.FileParameter(
					Encoding.UTF8.GetBytes("Hello World!"),
					"filename.txt",
					"text/plain");
				var formBytes = FormUpload.GetMultipartFormData(form, "boundary");
				var formString = Encoding.UTF8.GetString(formBytes);
				var result = browser.Post("/upload", formString, FormUpload.HttpEncoding + "; boundary=boundary");
				Assert.Equal("file-content=Hello World!;name=bob", result.ResponseText);
			}
		}

		protected string GetStringResult(string relativePath)
		{
			var resultString = GetStringResultOrNull(relativePath);
			if (resultString == null) throw new Exception("Path not found: " + relativePath);
			return resultString;
		}

		protected string GetStringResultOrNull(string relativePath)
		{
			using (var browser = new Browser())
			{
				var result = browser.Get("/" + relativePath);
				if (result.StatusCode == 404 || result.ResponseText.Contains("The resource cannot be found")) return null;
				Assert.Equal(200, result.StatusCode);
				return result.ResponseText;
			}
		}

		protected string PostStringResultOrNull(string relativePath, NameValueCollection form)
		{
			using (var browser = new Browser())
			{
				browser.AppendHeader("Accept-Language", "en-GB,en");
				var result = browser.Post("/" + relativePath, form);
				if (result.StatusCode == 404 || result.ResponseText.Contains("The resource cannot be found")) return null;
				Assert.Equal(200, result.StatusCode);
				return result.ResponseText;
			}
		}
	}
}
