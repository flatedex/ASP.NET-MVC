using Microsoft.AspNetCore.Mvc;

namespace WebArchiver.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
