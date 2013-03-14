// -----------------------------------------------------------------------
// <copyright file="MvcQueryStringEncoding.cs" company="">
// TODO: Update copyright text.
// </copyright>
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


		public sealed override RouteValueDictionary ToDictionary(T value)
		{
			return this.ToDictionary(null, value);
		}

		public sealed override T FromDictionary(NameValueCollection dict)
		{
			return this.FromDictionary(null, dict);
		}
		
		public abstract RouteValueDictionary ToDictionary(ControllerContext cx, T value);

		public abstract T FromDictionary(ControllerContext cx, NameValueCollection dict);

		private class DefaultEncoding : MvcQueryStringEncoding<T>
		{
			private static ModelBinderDictionary binderDictionary = new ModelBinderDictionary();

			private readonly IModelBinder binder;

			public DefaultEncoding()
			{
				this.binder = binderDictionary.GetBinder(typeof(T), fallbackToDefault: true);
			}

			public override RouteValueDictionary ToDictionary(ControllerContext cx, T value)
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
