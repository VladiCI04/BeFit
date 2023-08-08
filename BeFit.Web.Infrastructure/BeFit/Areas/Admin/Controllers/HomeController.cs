using Microsoft.AspNetCore.Mvc;

namespace BeFit.Areas.Admin.Controllers
{
	public class HomeController : BaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
