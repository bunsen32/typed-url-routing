// -----------------------------------------------------------------------
// <copyright file="AbstractUrlPattern.cs" company="Andrew Forrest">©2013 Andrew Forrest</copyright>
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may
// not use this file except in compliance with the License. Copy of
// license at: http://www.apache.org/licenses/LICENSE-2.0
//
// This software is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES 
// OR CONDITIONS. See License for specific permissions and limitations.
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
	/// Base class for UrlPattern classes. Denotes a set of URLs to be matched or generated.
	/// UrlPatterns may have 0 or more parameters which represent variable parts of the URL.
	/// The parameters can exist within the path part of the URL, and can represent query-
	/// string parameters.
	/// </summary>
	public abstract class AbstractUrlPattern
	{
		private readonly int arity;
		private readonly string pathPattern;
		private readonly IList<string> regexStrings;

		private readonly Regex pathRegex;
		private readonly int pathArity;
		private readonly IList<string> queryParameterNames;

		public AbstractUrlPattern(int arity, string pattern, params UrlArgument[] parameters)
		{
			if (arity < 0) throw new ArgumentOutOfRangeException("arity");
			this.arity = arity;

			if (pattern == null) throw new ArgumentNullException("pattern");
			if (pattern.StartsWith("/")) throw new ArgumentException("Paths must not start with '/'.");
			if (pattern.StartsWith("~")) throw new ArgumentException("Paths must not start with '~'.");
			if (pattern.Contains('?')) throw new ArgumentException("Cannot include querystring part in URL pattern.");
			this.pathPattern = pattern;

			if (parameters.Length != this.Arity)
				throw new ArgumentException(string.Format("Should get exactly {0} 'parameters', but got {1}", this.Arity, parameters.Length));

			this.regexStrings = parameters.OfType<ISimpleUrlComponent>().Select(p => p.RegexString).ToList().AsReadOnly();
			this.queryParameterNames = parameters.OfType<IQueryArg>().Select(a => a.Name).ToList();

			this.pathArity = PatternArity(this.pathPattern, 0);
			var pathComponentsCount = parameters.OfType<IPathComponent>().Count();
			if (pathComponentsCount != this.pathArity)
				throw new ArgumentException(string.Format("Wrong number of path components: {0} should be {1} in '{2}'", pathComponentsCount, pathArity, pattern));

			var bracketedRegexes = regexStrings.Select((r, index) => string.Format("(?<{0}>{1})", ParameterName(index), r)).ToArray();
			var regexString = string.Format(this.pathPattern, (object[])bracketedRegexes);
			this.pathRegex = new Regex("^" + regexString + "$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);
		}

		public int Arity { get { return this.arity; } }

		public int PathArity { get { return this.pathArity; } }

		public bool HasQueryParameters { get { return this.Arity > this.PathArity; } }

		public int SimpleParameterCount { get { return this.regexStrings.Count; } }

		public string PathPattern { get { return this.pathPattern; } }

		public Regex PathRegex { get { return this.pathRegex; } }

		public IList<string> ParameterRegexes { get { return this.regexStrings; } }

		public string Description
		{
			get
			{
				var result = new StringBuilder();
				result.AppendFormat(this.PathPattern, Enumerable.Repeat((object)"?", this.pathArity).ToArray());
				if (this.HasQueryParameters)
				{
					result.Append('?');
					for (var i = 0; i < this.queryParameterNames.Count; i++)
					{
						if (i > 0) result.Append('&');
						result.Append(this.queryParameterNames[i]);
						result.Append("=?");
					}
				}

				return result.ToString();
			}
		}

		protected AppLocalUrl PotentialUrlWith(RouteValueDictionary queryOrNull, params string[] p)
		{
			var routeValues = queryOrNull == null 
				? new RouteValueDictionary() 
				: new RouteValueDictionary(queryOrNull);

			for (int i = this.PathArity; i < this.SimpleParameterCount; i++)
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
				if (kv.Value != null)
				{
					if (!firstParam) querystring.Append('&');
					firstParam = false;
					querystring.Append(kv.Key);
					querystring.Append('=');
					querystring.Append(HttpUtility.UrlEncode(Convert.ToString(kv.Value, CultureInfo.InvariantCulture)));
				}
			}

			return new AppLocalUrl(path, querystring.ToString());
		}

		protected string Stringify<T>(UrlArgument<T> p, T value, int position)
		{
			try
			{
				var stringifier = p as SimpleUrlComponent<T>;
				return stringifier != null
					? stringifier.ToString(value)
					: null;
			}
			catch (InvalidUrlComponentValueException problem)
			{
				throw new InvalidUrlArgumentException(string.Format("Invalid argument {{{0}}} to construct URL “{1}”", position, this.Description), problem);
			}
		}

		protected RouteValueDictionary Querify<T>(UrlArgument<T> p, T value)
		{
			var querifier = p as QueryStringEncoding<T>;
			if (querifier == null) return null;
			return querifier.ToDictionary(value);
		}

		public string ParameterName(int index)
		{
			return index < this.PathArity 
				? "__" + index
				: this.queryParameterNames[index - this.PathArity];
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

		private static string PathPatternOf(string url)
		{
			var q = url.IndexOf('?');
			if (q == -1)
				return url;

			var path = url.Substring(0, q);
			var querystring = url.Substring(q + 1);
			if (PlaceholderMatcher.IsMatch(querystring))
				throw new ArgumentException("Not allowed to include placeholders in querystring. Use Urls.Arg/QueryArg instead.");
			return path;
		}
	}
}
