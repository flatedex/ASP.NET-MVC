using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebArchiver.Controllers
{
	public class DownloadController : Controller
	{
		private IWebHostEnvironment _webHostEnvironment;

		public DownloadController(IWebHostEnvironment webHostEnvironment) {
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			return View("~/Views/Home/Index.cshtml");
		}
		[HttpGet]
		public async Task<IActionResult> SendArchive()
		{
			await Archive();

			String fileDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "UploadFilesDirectory\\UploadFilesDirectory.rar");

			return PhysicalFile(fileDirectory, "application/vnd.rar", "Archive.rar");
		}
		public async Task Archive()
		{
			Process process = new Process();
			String fileDirectory = "ExternalPrograms";
			String batFileDir = Path.Combine(_webHostEnvironment.ContentRootPath, fileDirectory);
			if (!Directory.Exists(batFileDir))
			{
				return;
			}
			String fileName = "ArchiverMVC.bat";
			batFileDir = Path.Combine(batFileDir, fileName);
			process.StartInfo.FileName = batFileDir;
			process.StartInfo.Verb = "runas";
			process.Start();
			
			await process.WaitForExitAsync();
		}
	}
}
