using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace LoginApp.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET /HelloWorld/

        public IActionResult Index()
        {
            return View();
        }

        // GET /HelloWorld/next/
        public string Next()
        {
            return "Hello Next World ;-)";
        }
    }
}