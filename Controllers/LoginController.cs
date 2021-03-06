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

            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Failed()
        {
            return View();
        }
    }
}