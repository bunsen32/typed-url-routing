using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dysphoria.Net.UrlRouting.TestApp.Controllers
{
	public class AsyncController : Controller
	{
		public async Task<ActionResult> Async0Arg()
		{
			await Task.CompletedTask;
			return Content("success");
		}
		
		public async Task<ActionResult> Async1Arg(string a)
		{
			await Task.CompletedTask;
			return Content($"args={a}");
		}
		
		public async Task<ActionResult> Async2Args(string a, string b)
		{
			await Task.CompletedTask;
			return Content($"args={a}, {b}");
		}
		
		public async Task<ActionResult> Async3Args(string a, string b, string c)
		{
			await Task.CompletedTask;
			return Content($"args={a}, {b}, {c}");
		}
		
		public async Task<ActionResult> Async4Args(string a, string b, string c, string d)
		{
			await Task.CompletedTask;
			return Content($"args={a}, {b}, {c}, {d}");
		}
	}
}