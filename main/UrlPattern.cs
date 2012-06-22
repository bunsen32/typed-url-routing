// -----------------------------------------------------------------------
// <copyright file="UrlPattern.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Uk.Co.Cygnets.UrlRouting
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Web;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class UrlPattern : AbstractUrlPattern
	{
		private static readonly string[] NoStrings = new string[0];

		public UrlPattern(string pattern)
			: base(pattern, NoStrings)
		{
		}

		public override int Arity
		{
			get { return 0; }
		}
	}

	public class UrlPattern<T1> : AbstractUrlPattern
	{
		private readonly IPathComponent<T1> param1;

		public UrlPattern(string pattern, IPathComponent<T1> param1)
			: base(pattern, param1.RegexString)
		{
			this.param1 = param1;
		}

		public override int Arity
		{
			get { return 1; }
		}

		public IPathComponent<T1> Param1 { get { return this.param1; } }

		public Uri With(T1 p1)
		{
			return this.UriWith(
				this.Param1.ToString(p1));
		}
	}

	public class UrlPattern<T1, T2> : AbstractUrlPattern
	{
		private readonly IPathComponent<T1> param1;
		private readonly IPathComponent<T2> param2;

		public UrlPattern(string pattern, IPathComponent<T1> param1, IPathComponent<T2> param2)
			: base(pattern, param1.RegexString, param2.RegexString)
		{
			this.param1 = param1;
			this.param2 = param2;
		}

		public override int Arity
		{
			get { return 2; }
		}

		public IPathComponent<T1> Param1 { get { return this.param1; } }

		public IPathComponent<T2> Param2 { get { return this.param2; } }

		public Uri With(T1 p1, T2 p2)
		{
			return this.UriWith(
				this.Param1.ToString(p1),
				this.Param2.ToString(p2));
		}
	}

	public class UrlPattern<T1, T2, T3> : AbstractUrlPattern
	{
		private readonly IPathComponent<T1> param1;
		private readonly IPathComponent<T2> param2;
		private readonly IPathComponent<T3> param3;

		public UrlPattern(string pattern, IPathComponent<T1> param1, IPathComponent<T2> param2, IPathComponent<T3> param3)
			: base(pattern, param1.RegexString, param2.RegexString, param3.RegexString)
		{
			this.param1 = param1;
			this.param2 = param2;
			this.param3 = param3;
		}

		public override int Arity
		{
			get { return 3; }
		}

		public IPathComponent<T1> Param1 { get { return this.param1; } }

		public IPathComponent<T2> Param2 { get { return this.param2; } }

		public IPathComponent<T3> Param3 { get { return this.param3; } }

		public Uri With(T1 p1, T2 p2, T3 p3)
		{
			return this.UriWith(
				this.Param1.ToString(p1),
				this.Param2.ToString(p2),
				this.Param3.ToString(p3));
		}
	}

	public class UrlPattern<T1, T2, T3, T4> : AbstractUrlPattern
	{
		private readonly IPathComponent<T1> param1;
		private readonly IPathComponent<T2> param2;
		private readonly IPathComponent<T3> param3;
		private readonly IPathComponent<T4> param4;

		public UrlPattern(string pattern, IPathComponent<T1> param1, IPathComponent<T2> param2, IPathComponent<T3> param3, IPathComponent<T4> param4)
			: base(pattern, param1.RegexString, param2.RegexString, param3.RegexString, param4.RegexString)
		{
			this.param1 = param1;
			this.param2 = param2;
			this.param3 = param3;
			this.param4 = param4;
		}

		public override int Arity
		{
			get { return 4; }
		}

		public IPathComponent<T1> Param1 { get { return this.param1; } }

		public IPathComponent<T2> Param2 { get { return this.param2; } }

		public IPathComponent<T3> Param3 { get { return this.param3; } }

		public IPathComponent<T4> Param4 { get { return this.param4; } }

		public Uri With(T1 p1, T2 p2, T3 p3, T4 p4)
		{
			return this.UriWith(
				this.Param1.ToString(p1),
				this.Param2.ToString(p2),
				this.Param3.ToString(p3),
				this.Param4.ToString(p4));
		}
	}
}
