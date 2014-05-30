// -----------------------------------------------------------------------
// <copyright file="UrlPatternRequestExtensions.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may
// not use this file except in compliance with the License. Copy of
// license at: http://www.apache.org/licenses/LICENSE-2.0
//
// This software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS. See License for specific permissions and limitations.
// -----------------------------------------------------------------------

using System;

namespace Dysphoria.Net.UrlRouting
{
	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public static class UrlPatternRequestExtensions
	{
		public static RequestPattern<U> Get<U>(this U url) where U : AbstractUrlPattern
		{
			return new RequestPattern<U>(HttpMethod.GET, url);
		}

		public static RequestPattern<U> Post<U>(this U url) where U : AbstractUrlPattern
		{
			return new RequestPattern<U>(HttpMethod.POST, url);
		}

		public static RequestPattern<U, BodyType> Post<U, BodyType>(this U url, TypeWitness<BodyType> body) where U : AbstractUrlPattern
		{
			return new RequestPattern<U, BodyType>(HttpMethod.POST, url);
		}

		public static RequestPattern<U, BodyType> Post<U, BodyType>(this U url, Func<TypeWitness<BodyType>> body) where U : AbstractUrlPattern
		{
			return new RequestPattern<U, BodyType>(HttpMethod.POST, url);
		}

		public static RequestPattern<U> Put<U>(this U url) where U : AbstractUrlPattern
		{
			return new RequestPattern<U>(HttpMethod.PUT, url);
		}

		public static RequestPattern<U, BodyType> Put<U, BodyType>(this U url, TypeWitness<BodyType> body) where U : AbstractUrlPattern
		{
			return new RequestPattern<U, BodyType>(HttpMethod.PUT, url);
		}

		public static RequestPattern<U> Delete<U>(this U url) where U : AbstractUrlPattern
		{
			return new RequestPattern<U>(HttpMethod.DELETE, url);
		}
	}
}
