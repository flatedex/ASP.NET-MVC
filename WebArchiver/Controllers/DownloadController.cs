using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace WebArchiver.Controllers
{
	public class DownloadController : Controller
	{
		private IWebHostEnvironment _webHostEnvironment;

		public DownloadController(IWebHostEnvironment webHostEnvironment) {
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task<IActionResult> Index()
		{
			await SendArchive();
			return View();
		}
		public async Task<IActionResult> SendArchive()
		{
			Archive();

			String fileDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "UploadFilesDirectory\\UploadFilesDirectory.rar");
			byte[] bytes = Encoding.UTF8.GetBytes(fileDirectory);
			return File(bytes, "application/vnd.rar", "UploadFilesDirectory.rar");
		}
		public void Archive()
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
		}
	}
}
