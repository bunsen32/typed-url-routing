// -----------------------------------------------------------------------
// <copyright file="IntComponent.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Dysphoria.Net.UrlRouting.PathComponents
{
	using System;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class IntComponent: PathComponent<int>
	{
		public static readonly IntComponent Instance = new IntComponent();

		private IntComponent()
			: base(@"[-+]?\d+")
		{
		}

		public override int FromString(string str)
		{
			try
			{
				return int.Parse(str);
			}
			catch (OverflowException ov)
			{
				throw new FormatException("String value is out of range", ov);
			}
		}

		public override string ToString(int value)
		{
			return value.ToString();
		}
	}
}
