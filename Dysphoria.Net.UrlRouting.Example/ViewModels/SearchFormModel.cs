namespace Dysphoria.Net.UrlRouting.Example.ViewModels
{
	using System.Collections.Generic;
	using Dysphoria.Net.UrlRouting.Example.Models;

	public class SearchFormModel
	{
		public string SearchString { get; set; }
		
		public AdvancedSearchOptions Options { get; set; }

		public IEnumerable<Monster> Results { get; set; }
	}
}