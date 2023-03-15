using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebArchiver.Data;
using WebArchiver.Models;

namespace WebArchiver.Controllers
{
    public class LastFileController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly int fileCount = 5;

        public LastFileController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<LastFile> objFilesList = _db.LastFile.ToList().TakeLast(fileCount).Reverse();
            return View(objFilesList);
        }
    }
}
