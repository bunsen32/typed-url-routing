// -----------------------------------------------------------------------
// <copyright file="RouteBindingTests.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
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
	using System.Web.Routing;
	using Xunit;

	public class RouteBindingTests: Urls
	{
		private static readonly UrlPattern Route = Path("some-path");

		[Fact]
		public void CanSuccessfullyBindToMethod()
		{
			new RouteCollection()
				.ForController<SampleController>()
				.MapRoute(Get(Route), c => c.Action);
		}
	}
}
