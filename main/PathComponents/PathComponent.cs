// -----------------------------------------------------------------------
// <copyright file="PathComponent.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Dysphoria.Net.UrlRouting.PathComponents
{
	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public abstract class PathComponent<T> : UrlArgument<T>, IPathComponent
	{
		private readonly string regexString;

		public PathComponent(string regexString)
		{
			this.regexString = regexString;
		}

		public string RegexString
		{
			get { return this.regexString; }
		}

		public abstract T FromString(string str);

		public abstract string ToString(T value);
	}
}
