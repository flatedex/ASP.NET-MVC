using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using WebArchiver.Data;
using WebArchiver.Models;

namespace WebArchiver.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private IWebHostEnvironment _webHostEnvironment;
		private ApplicationDBContext _db;
		private readonly int toKbytes = 1024; 

		[BindProperty, Display(Name = "Drag files here")]
		public List<IFormFile> Uploads { get; set; } = new();

		public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, ApplicationDBContext db)
		{
			_logger = logger;
			_webHostEnvironment = webHostEnvironment;
			_db = db;
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

			await AddFilesToDB(file);

			Archive();

			return RedirectToAction("Index");
		}
		public async Task AddFilesToDB(IFormFile file)
		{
			LastFile lf = new LastFile()
			{
				Size = file.Length / toKbytes + 1,
				Name = file.FileName,
				CreatedDateTime = DateTime.Now,
				User_id = 0
			};
			if(User.Identity.IsAuthenticated)
			{
				User loggedUser = _db.User.Where(us => us.Name == User.Identity.Name).ToList().First();
				lf.User_id = loggedUser.Id;
			}
			_db.LastFile.Add(lf);
			await _db.SaveChangesAsync();
		}
		public void Archive()
		{
			Process process= new Process();
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