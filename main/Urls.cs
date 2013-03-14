// -----------------------------------------------------------------------
// <copyright file="Urls.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may
// not use this file except in compliance with the License. Copy of
// license at: http://www.apache.org/licenses/LICENSE-2.0
//
// This software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS. See License for specific permissions and limitations.
// -----------------------------------------------------------------------
namespace Dysphoria.Net.UrlRouting
{
	using Dysphoria.Net.UrlRouting.PathComponents;

	public abstract class Urls
	{
		protected static readonly PathComponent<int> Int = IntComponent.Instance;
		protected static readonly PathComponent<string> Slug = String(@"[-_0-9a-zA-Z]+");
		protected static readonly PathComponent<string> PathComponent = String(@"[-_0-9a-zA-Z~+.,]+");
		protected static readonly PathComponent<string> AnyString = String(@".*");

		protected static PathComponent<string> String(string regexPattern)
		{
			return new StringComponent(regexPattern);
		}

		protected static UrlPattern Path(string pattern)
		{
			return new UrlPattern(pattern);
		}

		protected static UrlPattern<P1> Path<P1>(string pattern, UrlArgument<P1> p1)
		{
			return new UrlPattern<P1>(pattern, p1);
		}

		protected static UrlPattern<P1, P2> Path<P1, P2>(string pattern, PathComponent<P1> p1, UrlArgument<P2> p2)
		{
			return new UrlPattern<P1, P2>(pattern, p1, p2);
		}

		protected static UrlPattern<P1, P2, P3> Path<P1, P2, P3>(string pattern, PathComponent<P1> p1, PathComponent<P2> p2, UrlArgument<P3> p3)
		{
			return new UrlPattern<P1, P2, P3>(pattern, p1, p2, p3);
		}

		protected static UrlPattern<P1, P2, P3, P4> Path<P1, P2, P3, P4>(string pattern, PathComponent<P1> p1, PathComponent<P2> p2, PathComponent<P3> p3, UrlArgument<P4> p4)
		{
			return new UrlPattern<P1, P2, P3, P4>(pattern, p1, p2, p3, p4);
		}

		protected static QueryStringEncoding<T> Query<T>()
		{
			return MvcQueryStringEncoding<T>.Default;
		}

		protected static RequestPattern<U> Get<U>(U url) where U : AbstractUrlPattern
		{
			return new RequestPattern<U>(HttpMethod.GET, url);
		}

		protected static RequestPattern<U> Post<U>(U url) where U : AbstractUrlPattern
		{
			return new RequestPattern<U>(HttpMethod.POST, url);
		}

		protected static RequestPattern<U> Put<U>(U url) where U : AbstractUrlPattern
		{
			return new RequestPattern<U>(HttpMethod.PUT, url);
		}

		protected static RequestPattern<U> Delete<U>(U url) where U : AbstractUrlPattern
		{
			return new RequestPattern<U>(HttpMethod.DELETE, url);
		}
	}
}
