using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoginApp.Models;
//To avoid conflict
using sys_RegEx = System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

namespace LoginApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly LoginAppDbContext _context;
        public UsersController(LoginAppDbContext context)
        {
            _context = context;
        }

        private string HashString(string toHash)
        {
            HashAlgorithm algo = SHA1.Create();
            byte[] hashed_bytes = algo.ComputeHash(Encoding.UTF8.GetBytes(toHash));

            StringBuilder builder = new StringBuilder();
            foreach(byte i in hashed_bytes)
            {
                builder.Append(i.ToString("X2"));
            }

            return builder.ToString();
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        // GET: Users/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginForm data)
        {
            if(data == null)
            {
                return RedirectToAction("Index","Login");
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.Name == data.login);
            if (user == null)
            {
                return RedirectToAction("WrongUsername","Users");
            }
            
            if(user.Password == HashString(data.password))
            {
                return RedirectToAction("Success", "Login");
            }
            else
            {
                return RedirectToAction("Failed", "Login");
            }
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Password")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    user.Password = HashString(user.Password);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Name))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.SingleOrDefaultAsync(m => m.ID == id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string name)
        {
            return _context.User.Any(e => e.Name == name);
        }

        
        public async Task<IActionResult> Register()
        {
            var query_data = await _context.Regex.ToListAsync();
            ViewData["regexList"] = query_data;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterForm data)
        {
            List<Regex> query_data;

            if(ModelState.IsValid)
            {
                User user = new User();
                user.Name = data.Name;
                if(data.Password == data.PasswordCheck && !this.UserExists(data.Name))
                {
                    //Sprawdzenie regexa
                    if(sys_RegEx.Regex.IsMatch(data.Password, data.Regex))
                    {
                        query_data = await _context.Regex.ToListAsync();
                        ViewData["errorMsg"] = "Password matchess regex: " + data.Regex +". User added.";
                        ViewData["regexList"] = query_data;
                        user.Password = HashString(data.Password);
                        _context.User.Add(user);
                        await _context.SaveChangesAsync();
                        return View(data);
                    }
                    else
                    {
                        query_data = await _context.Regex.ToListAsync();
                        ViewData["errorMsg"] = "Password doesn't match regex: " + data.Regex;
                        ViewData["regexList"] = query_data;
                        return View(data);
                    }   
                }
                else
                {
                    query_data = await _context.Regex.ToListAsync();
                    ViewData["errorMsg"] = "Passwords do not match";
                    ViewData["regexList"] = query_data;
                    return View(data);
                }
            }
            
            query_data = await _context.Regex.ToListAsync();           
            ViewData["regexList"] = query_data;
            return View(data); 
        }

        public IActionResult WrongUsername()
        {
            return View();
        }
    }

}
