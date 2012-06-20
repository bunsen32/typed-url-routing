// -----------------------------------------------------------------------
// <copyright file="IPathComponent.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Uk.Co.Cygnets.UrlRouting
{
	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public interface IPathComponent<T>
	{
		T FromString(string str);

		string ToString(T value);

		string RegexString { get; }
	}
}
