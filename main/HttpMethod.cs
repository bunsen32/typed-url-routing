// -----------------------------------------------------------------------
// <copyright file="HttpMethod.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
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
	using System;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	[Flags]
	public enum HttpMethod : ushort
	{
		OPTIONS = 1,
		GET = 2,
		HEAD = 4,
		POST = 8,
		PUT = 16,
		DELETE = 32,
		TRACE = 64,
		CONNECT = 128,
		Any = 0xffff,
	}
}
