// -----------------------------------------------------------------------
// <copyright file="UrlPattern.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Dysphoria.Net.UrlRouting
{
	using Dysphoria.Net.UrlRouting.PathComponents;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class UrlPattern : AbstractUrlPattern
	{
		private static readonly string[] NoStrings = new string[0];

		public UrlPattern(string pattern)
			: base(pattern)
		{
		}

		public override int Arity
		{
			get { return 0; }
		}
	}

	public class UrlPattern<T0> : AbstractUrlPattern
	{
		private readonly UrlArgument<T0> param0;

		public UrlPattern(string pattern, UrlArgument<T0> param0)
			: base(pattern, param0)
		{
			this.param0 = param0;
		}

		public override int Arity
		{
			get { return 1; }
		}

		public UrlArgument<T0> Param0 { get { return this.param0; } }

		public PotentialUrl With(T0 p0)
		{
			return this.PotentialUrlWith(
				Querify(this.Param0, p0),
				Stringify(this.Param0, p0));
		}
	}

	public class UrlPattern<T0, T1> : AbstractUrlPattern
	{
		private readonly PathComponent<T0> param0;
		private readonly UrlArgument<T1> param1;

		public UrlPattern(string pattern, PathComponent<T0> param0, UrlArgument<T1> param1)
			: base(pattern, param0, param1)
		{
			this.param0 = param0;
			this.param1 = param1;
		}

		public override int Arity
		{
			get { return 2; }
		}

		public PathComponent<T0> Param0 { get { return this.param0; } }

		public UrlArgument<T1> Param1 { get { return this.param1; } }

		public PotentialUrl With(T0 p0, T1 p1)
		{
			return this.PotentialUrlWith(
				Querify(this.param1, p1),
				Stringify(this.Param0, p0),
				Stringify(this.Param1, p1));
		}
	}

	public class UrlPattern<T0, T1, T2> : AbstractUrlPattern
	{
		private readonly PathComponent<T0> param0;
		private readonly PathComponent<T1> param1;
		private readonly UrlArgument<T2> param2;

		public UrlPattern(string pattern, PathComponent<T0> param0, PathComponent<T1> param1, UrlArgument<T2> param2)
			: base(pattern, param0, param1, param2)
		{
			this.param0 = param0;
			this.param1 = param1;
			this.param2 = param2;
		}

		public override int Arity
		{
			get { return 3; }
		}

		public PathComponent<T0> Param0 { get { return this.param0; } }

		public PathComponent<T1> Param1 { get { return this.param1; } }

		public UrlArgument<T2> Param2 { get { return this.param2; } }

		public PotentialUrl With(T0 p0, T1 p1, T2 p2)
		{
			return this.PotentialUrlWith(
				Querify(this.Param2, p2),
				Stringify(this.Param0, p0),
				Stringify(this.Param1, p1),
				Stringify(this.Param2, p2));
		}
	}

	public class UrlPattern<T0, T1, T2, T3> : AbstractUrlPattern
	{
		private readonly PathComponent<T0> param0;
		private readonly PathComponent<T1> param1;
		private readonly PathComponent<T2> param2;
		private readonly UrlArgument<T3> param3;

		public UrlPattern(string pattern, PathComponent<T0> param0, PathComponent<T1> param1, PathComponent<T2> param2, UrlArgument<T3> param3)
			: base(pattern, param0, param1, param2, param3)
		{
			this.param0 = param0;
			this.param1 = param1;
			this.param2 = param2;
			this.param3 = param3;
		}

		public override int Arity
		{
			get { return 4; }
		}

		public PathComponent<T0> Param0 { get { return this.param0; } }

		public PathComponent<T1> Param1 { get { return this.param1; } }

		public PathComponent<T2> Param2 { get { return this.param2; } }

		public UrlArgument<T3> Param3 { get { return this.param3; } }

		public PotentialUrl With(T0 p0, T1 p1, T2 p2, T3 p3)
		{
			return this.PotentialUrlWith(
				Querify(this.Param3, p3),
				Stringify(this.Param0, p0),
				Stringify(this.Param1, p1),
				Stringify(this.Param2, p2),
				Stringify(this.Param3, p3));
		}
	}
}
