namespace Dysphoria.Net.UrlRouting.PathComponents
{
	/// <summary>
	/// A named querystring argument.
	/// </summary>
	/// <remarks>
	/// Contrast with <see cref="QueryStringEncoding"/> which represents an
	/// object composed of zero or more query string arguments.
	/// </remarks>
	public class QueryArg<T> : SimpleUrlComponent<T>, IQueryArg
	{
		private readonly string name;
		private readonly PathComponent<T> type;

		public QueryArg(string name, PathComponent<T> type)
			: base(type.RegexString)
		{
			this.name = name;
			this.type = type;
		}

		public string Name { get { return this.name; } }

		public PathComponent<T> Type { get { return this.type; } }

		public override T FromString(string str)
		{
			return this.type.FromString(str);
		}

		public override string ToString(T value)
		{
			return this.type.ToString(value);
		}
	}
}
