namespace Dysphoria.Net.UrlRouting.Test.ViewModels
{
	using System.Collections.Generic;
	using Dysphoria.Net.UrlRouting.Test.Models;

	public class SearchFormModel
	{
		public string SearchString { get; set; }
		
		public AdvancedSearchOptions Options { get; set; }

		public IEnumerable<Monster> Results { get; set; }
	}
}