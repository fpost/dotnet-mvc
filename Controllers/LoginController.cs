using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using LoginApp.Models;

namespace LoginApp.Controllers
{
    public class LoginController : Controller
    {
        // GET /Login/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginForm data)
        {
            string login = data.login;
            string password = data.password;

            ViewData["login"] = login;
            ViewData["password"] = password;

            return View();
        }
    }
}