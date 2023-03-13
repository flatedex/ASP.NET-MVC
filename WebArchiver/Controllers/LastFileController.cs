using Microsoft.AspNetCore.Mvc;
using WebArchiver.Data;
using WebArchiver.Models;

namespace WebArchiver.Controllers
{
    public class LastFileController : Controller
    {
        private readonly ApplicationDBContext _db;

        public LastFileController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<LastFile> objFilesList = _db.LastFile.ToList();
            return View(objFilesList);
        }
    }
}
