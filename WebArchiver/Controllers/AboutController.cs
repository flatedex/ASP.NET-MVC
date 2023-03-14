using Microsoft.AspNetCore.Mvc;

namespace WebArchiver.Controllers
{
	public class AboutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
