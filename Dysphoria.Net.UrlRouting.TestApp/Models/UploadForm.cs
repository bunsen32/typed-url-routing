namespace Dysphoria.Net.UrlRouting.TestApp.Models
{
	using System.Web;

	public class UploadForm
	{
		public HttpPostedFileBase File { get; set; }
		public string Name { get; set; }
	}
}