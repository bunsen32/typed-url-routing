namespace Dysphoria.Net.UrlRouting
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// 'Dummy' class used only to make type inference work nicely in
	/// fluent Urls syntax.
	/// </summary>
	public class TypeWitness<T>
	{
		public static readonly TypeWitness<T> Instance = new TypeWitness<T>();
		private TypeWitness()
		{
		}
	}
}
