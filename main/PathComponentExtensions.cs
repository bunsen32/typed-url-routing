
namespace Dysphoria.Net.UrlRouting
{
	using Dysphoria.Net.UrlRouting.PathComponents;

	public static class PathComponentRefExtensions
	{
		public static NullableRefComponent<T> OrNull<T>(this PathComponent<T> b, string nullValueString)
			where T: class
		{
			return new NullableRefComponent<T>(b, nullValueString);
		}

		public static NullableValueComponent<T> Or<T>(this PathComponent<T> b, string nullValueString)
			where T : struct
		{
			return new NullableValueComponent<T>(b, nullValueString);
		}
	}
}
