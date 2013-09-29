// -----------------------------------------------------------------------
// <copyright file="AbstractRequestPattern.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
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
	using System.Web.Mvc;
	using System;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public abstract class AbstractRequestPattern
	{
		private readonly HttpMethod method;

		public AbstractRequestPattern(HttpMethod method)
		{
			this.method = method;
		}

		public HttpMethod Method { get { return this.method; } }

		protected abstract AbstractUrlPattern AbstractUrlPattern { get; }

		public AbstractUrlPattern Url { get { return this.AbstractUrlPattern; } }

		public FormMethod FormMethod
		{
			get
			{
				switch (this.Method)
				{
					case HttpMethod.GET: return FormMethod.Get;
					case HttpMethod.POST: return FormMethod.Post;
					default:
						throw new NotSupportedException("Not an HTML form method: " + this.Method);
				}
			}
		}

	}
}
