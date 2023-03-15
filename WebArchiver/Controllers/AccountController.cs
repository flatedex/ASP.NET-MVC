using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebArchiver.Data;
using WebArchiver.Models;

namespace WebArchiver.Controllers
{
	public class AccountController : Controller
	{
		private readonly ApplicationDBContext _db;

		public AccountController(ApplicationDBContext db)
		{
			_db = db;
		}
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				User user = await _db.User.FirstOrDefaultAsync(u => u.Email == model.Email);
				if(user == null)
				{
					user = new User 
					{	
						Email = model.Email,
						Password=model.Password,
						Name= model.Name
					};
					_db.User.Add(user);
					await _db.SaveChangesAsync();

					await Authenticate(user);

					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Invalid login and(or) password");
				}
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				User user = await _db.User.FirstOrDefaultAsync(u => u.Name == model.Name && u.Password == model.Password);
				if(user != null)
				{
					await Authenticate(user);

					return RedirectToAction("Index","LastFile");
				}
				ModelState.AddModelError("","Invalid login or password!");
			}
			return View(model);
		}
		public async Task Authenticate(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name)
			};
			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType); 

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}
	}
}
