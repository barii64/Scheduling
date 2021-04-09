using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scheduling.Models;
using Microsoft.EntityFrameworkCore;

namespace Scheduling.Controllers
{
    public class UserController : Controller
    {
        private readonly UserContext _context;

        public UserController(UserContext services)
        {
            _context = services;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> LogUser(string login, string password)
        {
            return await _context.Users
                .Where(u => u.Login == login && u.Password == password)
                .FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string login, string password)
        {
            var user = await LogUser(login, password);
            return Json(user);
        }
    }
}
