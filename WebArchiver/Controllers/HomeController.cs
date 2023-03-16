using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using WebArchiver.Models;

namespace WebArchiver.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private IWebHostEnvironment _webHostEnvironment;

		[BindProperty, Display(Name = "Drag files here")]
		public List<IFormFile> Uploads { get; set; } = new();

		public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
		{
			_logger = logger;
			_webHostEnvironment = webHostEnvironment;
		}

		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Upload(IFormFile file)
		{
			var fileDirectory = "UploadFilesDirectory";
			String filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileDirectory);
			if (!Directory.Exists(filePath))
			{
				Directory.CreateDirectory(filePath);
			}
			var fileName = file.FileName;
			filePath = Path.Combine(filePath, fileName);

			using (FileStream fs = System.IO.File.Create(filePath)) { file.CopyTo(fs); }
			return RedirectToAction("Index");
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}