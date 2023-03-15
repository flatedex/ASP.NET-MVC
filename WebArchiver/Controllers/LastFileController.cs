using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
            if (User.Identity.IsAuthenticated)
            {
                User loggedUser = _db.User.Where(us => us.Name == User.Identity.Name).ToList().First();

                IQueryable<LastFile> objFilesList = _db.LastFile.Where(u => u.User_id == loggedUser.Id);//.ToList().TakeLast(fileCount).Reverse();
                IEnumerable<LastFile> lf = objFilesList.ToList().TakeLast(fileCount).Reverse();
				return View(objFilesList);
            }
			return RedirectToAction("Login", "Account");
		}
    }
}
