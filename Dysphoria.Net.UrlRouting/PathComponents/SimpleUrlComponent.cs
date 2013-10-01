using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dysphoria.Net.UrlRouting.PathComponents
{
	public abstract class SimpleUrlComponent<T> : UrlArgument<T>
	{
		private readonly string regexString;

		public SimpleUrlComponent(string regexString)
			: base()
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
