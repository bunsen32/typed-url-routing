namespace Dysphoria.Net.UrlRouting
{
	using System;
	using System.Collections.Specialized;
	using System.Globalization;
	using System.Web.Mvc;

	internal class MvcDecoder<T>
	{
		private static readonly ModelBinderDictionary binderDictionary = new ModelBinderDictionary();
		public static readonly MvcDecoder<T> Instance = new MvcDecoder<T>();
		private readonly IModelBinder binder;

		private MvcDecoder()
		{
			this.binder = binderDictionary.GetBinder(typeof(T), fallbackToDefault: true);
		}

		public T FromDictionary(ControllerContext cx, NameValueCollection dict)
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
