using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dysphoria.Net.UrlRouting.PathComponents
{
	public interface ISimpleUrlComponent
	{
		string RegexString { get; }
	}
}
