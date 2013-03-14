// -----------------------------------------------------------------------
// <copyright file="HttpMethod.cs" company="">
// TODO: Update copyright text.
// </copyright>
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
