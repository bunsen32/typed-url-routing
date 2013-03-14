// -----------------------------------------------------------------------
// <copyright file="AbstractUrlPattern.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Dysphoria.Net.UrlRouting
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Text;
	using System.Text.RegularExpressions;
	using System.Web;
	using System.Web.Routing;
	using Dysphoria.Net.UrlRouting.PathComponents;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public abstract class AbstractUrlPattern
	{
		private readonly string pattern;
		private readonly IList<string> regexStrings;

		private readonly Regex pathRegex;
		private readonly string pathPattern;
		private readonly int pathArity;
		private readonly IList<string> queryParameterNames;

		public AbstractUrlPattern(string pattern, params UrlArgument[] parameters)
		{
			if (pattern == null) throw new ArgumentNullException("pattern");
			if (!pattern.StartsWith("/")) throw new ArgumentException("We only support absolute paths (include initial '/').");

			this.regexStrings = parameters.OfType<IPathComponent>().Select(p => p.RegexString).ToList().AsReadOnly();
			string queryPattern;
			SplitIntoPathAndQuery(pattern, out this.pathPattern, out queryPattern);
			this.pathArity = PatternArity(this.pathPattern, 0);
			var queryArity = PatternArity(queryPattern, this.pathArity);

			this.pattern = pattern;
			var patternArity = this.pathArity + queryArity;
			if (this.ParameterCount > this.Arity) throw new ArgumentException(string.Format("Number of parameters ({0}) cannot be greater than arity {1}.", this.ParameterCount, this.Arity));
			if (patternArity != this.ParameterCount) throw new ArgumentException(string.Format("Wrong number of regex parameters: {0} should be {1}", this.ParameterCount, patternArity));

			this.queryParameterNames = GetQueryParameterNames(queryPattern, queryArity);

			var bracketedRegexes = regexStrings.Select((r, index) => string.Format("(?<{0}>{1})", ParameterName(index), r)).ToArray();
			var regexString = string.Format(this.pathPattern, (object[])bracketedRegexes);
			this.pathRegex = new Regex("^" + regexString + "$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);
		}

		public abstract int Arity { get; }

		public int ParameterCount { get { return this.regexStrings.Count; } }

		public int PathParameterCount { get { return this.pathArity; } }

		public Regex PathRegex { get { return this.pathRegex; } }

		public string Pattern { get { return this.pattern; } }

		public string PathPattern { get { return this.pathPattern; } }

		public IList<string> ParameterPatterns { get { return this.regexStrings; } }

		protected PotentialUrl PotentialUrlWith(RouteValueDictionary queryOrNull, params string[] p)
		{
			var routeValues = queryOrNull == null 
				? new RouteValueDictionary() 
				: new RouteValueDictionary(queryOrNull);

			for (int i = this.PathParameterCount; i < this.ParameterCount; i++)
			{
				var value = p[i];
				if (value != null)
					routeValues[this.ParameterName(i)] = value;
			}

			var path = string.Format(this.PathPattern, (object[])p);

			var querystring = new StringBuilder();
			var firstParam = true;
			foreach (var kv in routeValues)
			{
				if (!firstParam) querystring.Append('&');
				firstParam = false;
				querystring.Append(kv.Key);
				querystring.Append('=');
				querystring.Append(HttpUtility.UrlEncode(Convert.ToString(kv.Value, CultureInfo.InvariantCulture)));
			}

			return new PotentialUrl(path, querystring.ToString());
		}

		protected string Stringify<T>(UrlArgument<T> p, T value)
		{
			var stringifier = p as PathComponent<T>;
			return stringifier != null
				? stringifier.ToString(value)
				: null;
		}

		protected RouteValueDictionary Querify<T>(UrlArgument<T> p, T value)
		{
			var querifier = p as QueryStringEncoding<T>;
			if (querifier == null) return null;
			return querifier.ToDictionary(value);
		}

		public string ParameterName(int index)
		{
			return index < this.PathParameterCount 
				? "__" + index
				: this.queryParameterNames[index - this.PathParameterCount];
		}

		private static readonly Regex PlaceholderMatcher = new Regex(@"\{(\d+)\}", RegexOptions.Compiled);
		private static int PatternArity(string pattern, int startsFrom = 0)
		{
			if (startsFrom < 0) throw new ArgumentOutOfRangeException("startsFrom", "Must start from >= 0");
			var matches = PlaceholderMatcher.Matches(pattern);
			var numberOfIndexes = matches.Count;
			if (numberOfIndexes == 0)
			{
				return 0; // Success and no more checks.
			}
			else
			{
				var indexSet = new HashSet<int>();
				var maxIndex = -1;
				foreach (Match match in matches)
				{
					var index = int.Parse(match.Groups[1].Value);
					if (indexSet.Contains(index)) throw new ArgumentException("Pattern contains more than one index of value " + index);
					indexSet.Add(index);
					if (index > maxIndex) maxIndex = index;
				}

				if (!indexSet.Contains(startsFrom)) throw new ArgumentException(string.Format("Pattern placeholders do not start from {0}.", startsFrom));
				var shouldEndAt = startsFrom + numberOfIndexes - 1;
				if (maxIndex != shouldEndAt) throw new ArgumentException(string.Format("Pattern placeholders must run from {0} to {1}.", startsFrom, shouldEndAt));
				return numberOfIndexes;
			}
		}

		private const char ParameterSeparator = '&';
		private static IList<string> GetQueryParameterNames(string queryPattern, int expectedArity)
		{
			var result = new List<string>(expectedArity);
			var parts = queryPattern.Split(ParameterSeparator);
			foreach (var part in parts)
			{
				var eq = part.IndexOf('=');
				if (eq != -1)
				{
					var name = part.Substring(0, eq);
					var value = part.Substring(eq + 1);
					if (PlaceholderMatcher.IsMatch(value))
					{
						result.Add(name);
					}
				}
			}

			return result;
		}

		private static void SplitIntoPathAndQuery(string url, out string path, out string query)
		{
			var q = url.IndexOf('?');
			if (q == -1)
			{
				path = url;
				query = "";
			}
			else
			{
				path = url.Substring(0, q);
				query = url.Substring(q + 1);
			}
		}
	}
}
