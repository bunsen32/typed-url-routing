// -----------------------------------------------------------------------
// <copyright file="AbstractUrlPattern.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Uk.Co.Cygnets.UrlRouting
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Text.RegularExpressions;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public abstract class AbstractUrlPattern
	{
		private readonly string pattern;
		private readonly IList<string> regexStrings;
		private readonly Regex regex;

		public AbstractUrlPattern(string pattern, params string[] regexStrings)
		{
			var patternArity = PatternArity(pattern);
			if (patternArity != this.Arity) throw new ArgumentException(string.Format("Wrong arity: {0} should be {1}", patternArity, this.Arity));

			this.pattern = pattern;
			this.regexStrings = regexStrings.ToList().AsReadOnly();

			var bracketedRegexes = regexStrings.Select((r, index) => string.Format("(?<{0}>{1})", ParameterName(index), r)).ToArray();
			var regexString = string.Format(pattern, (object[])bracketedRegexes);
			this.regex = new Regex("^" + regexString + "$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);
		}

		public abstract int Arity { get; }

		public Regex Regex { get { return this.regex; } }

		public string Pattern { get { return this.pattern; } }

		public IList<string> ParameterPatterns { get { return this.regexStrings; } }

		public string ParameterName(int index)
		{
			return "p" + index;
		}

		private static readonly Regex PlaceholderMatcher = new Regex(@"\{(\d+)\}", RegexOptions.Compiled);
		private static int PatternArity(string pattern)
		{
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
					var index = int.Parse(match.Captures[0].Value);
					if (indexSet.Contains(index)) throw new ArgumentException("Pattern contains more than one index of value " + index);
					indexSet.Add(index);
					if (index > maxIndex) maxIndex = index;
				}

				if (!indexSet.Contains(0)) throw new ArgumentException("Pattern placeholders do not start from {0}.");
				if (maxIndex != numberOfIndexes - 1) throw new ArgumentException(string.Format("Pattern placeholders must run from 0 to {0}.", numberOfIndexes - 1));
				return numberOfIndexes;
			}
		}
	}
}
