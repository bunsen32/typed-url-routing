// -----------------------------------------------------------------------
// <copyright file="PathComponent.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may
// not use this file except in compliance with the License. Copy of
// license at: http://www.apache.org/licenses/LICENSE-2.0
//
// This software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS. See License for specific permissions and limitations.
// -----------------------------------------------------------------------
namespace Dysphoria.Net.UrlRouting.PathComponents
{
	/// <summary>
	/// A kind of a path component which forms a single chunk of the request URL.
	/// As distinct from a <seealso cref="QueryStringEncoding`1"/> which is decoded
	/// from zero or more query parameters.
	/// </summary>
	public abstract class PathComponent<T> : SimpleUrlComponent<T>, IPathComponent
	{
		public PathComponent(string regexString)
			: base(regexString)
		{
		}
	}
}
