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
			String fileDirectory = "UploadFilesDirectory";
			String filePath = Path.Combine(_webHostEnvironment.WebRootPath, fileDirectory);
			if (!Directory.Exists(filePath))
			{
				Directory.CreateDirectory(filePath);
			}
			String fileName = file.FileName;
			filePath = Path.Combine(filePath, fileName);

			using (FileStream fs = System.IO.File.Create(filePath)) { await file.CopyToAsync(fs); }

			Archive();

			return RedirectToAction("Index");
		}
		public void Archive()
		{
			Process process= new Process();
			string batFileDir = String.Format(@"C:\C# Projects\ASP.NET-MVC\WebArchiver\ExternalPrograms\ArchiverMVC.bat");
			process.StartInfo.FileName = batFileDir;
			process.StartInfo.Verb = "runas";
			process.Start();
		}
		public async Task SendArchive()
		{

		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}