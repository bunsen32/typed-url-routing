namespace Dysphoria.Net.UrlRouting.Test
{
	using System;
	using System.Web.Mvc;

	public class SampleController : ControllerBase
	{
		public ActionResult Action()
		{
			return new HttpStatusCodeResult(404);
		}

		public ActionResult ActionInt(int arg)
		{
			return new HttpStatusCodeResult(404);
		}

		public ActionResult ActionStruct(SampleModel model)
		{
			return new HttpStatusCodeResult(404);
		}

		protected override void ExecuteCore()
		{
			throw new NotImplementedException();
		}
	}
}
