namespace Dysphoria.Net.UrlRouting
{
	using System;
	using System.Web.Mvc;

	internal class MvcDecoder<T>
	{
		private static readonly ModelBinderDictionary binderDictionary = new ModelBinderDictionary();
		private static readonly ValueProviderFactoryCollection bodyProviders = new ValueProviderFactoryCollection
		{
			new FormValueProviderFactory(),
			new JsonValueProviderFactory(),
			new HttpFileCollectionValueProviderFactory(),
		};

		public static readonly MvcDecoder<T> Instance = new MvcDecoder<T>();
		private readonly IModelBinder binder;

		private MvcDecoder()
		{
			this.binder = binderDictionary.GetBinder(typeof(T), fallbackToDefault: true);
		}

		public T FromForm(ControllerContext cx)
		{
			return FromValueProvider(cx, bodyProviders.GetValueProvider(cx));
		}

		public T FromQueryString(ControllerContext cx)
		{
			return FromValueProvider(cx, new QueryStringValueProvider(cx));
		}

		private T FromValueProvider(ControllerContext cx, IValueProvider valueProvider)
		{
			if (cx == null) throw new ArgumentNullException("Needs to run in a Controller context. ControllerContext parameter is required.");
			var bindingContext = new ModelBindingContext()
			{
				FallbackToEmptyPrefix = true, // TODO
				ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(T)),
				ModelName = "",
				ModelState = new ModelStateDictionary(),
				PropertyFilter = (s) => true,
				ValueProvider = valueProvider,
			};
			return (T)this.binder.BindModel(cx, bindingContext);
		}
	}
}
