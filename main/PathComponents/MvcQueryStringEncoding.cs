// -----------------------------------------------------------------------
// <copyright file="MvcQueryStringEncoding.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
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
	using System;
	using System.Collections.Specialized;
	using System.Globalization;
	using System.Web.Mvc;
	using System.Web.Routing;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public abstract class MvcQueryStringEncoding<T> : QueryStringEncoding<T>
	{
		public static readonly MvcQueryStringEncoding<T> Default = new DefaultEncoding();

		private class DefaultEncoding : MvcQueryStringEncoding<T>
		{
			private static ModelBinderDictionary binderDictionary = new ModelBinderDictionary();

			private readonly IModelBinder binder;

			public DefaultEncoding()
			{
				this.binder = binderDictionary.GetBinder(typeof(T), fallbackToDefault: true);
			}

			public override RouteValueDictionary ToDictionary(T value)
			{
				return (value as RouteValueDictionary) ?? new RouteValueDictionary(value);
			}

			public override T FromDictionary(ControllerContext cx, NameValueCollection dict)
			{
				if (cx == null) throw new ArgumentNullException("Needs to run in a Controller context. ControllerContext parameter is required.");
				var bindingContext = new ModelBindingContext()
				{
					FallbackToEmptyPrefix = true, // TODO
					ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(T)),
					ModelName = "",
					ModelState = new ModelStateDictionary(),
					PropertyFilter = (s) => true,
					ValueProvider = new NameValueCollectionValueProvider(dict, CultureInfo.InvariantCulture),
				};
				return (T)this.binder.BindModel(cx, bindingContext);
			}
		}
	}
}
