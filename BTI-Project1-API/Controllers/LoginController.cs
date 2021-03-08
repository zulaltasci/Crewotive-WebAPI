using BTI_Project1_API.Context;
using BTI_Project1_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTI_Project1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Login
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet]
        public async Task<ActionResult<bool>> CheckLogin(LoginInfo user)
        {
            Person dbperson = new Person();

            foreach (var tempperson in _context.Person)
            {
                if (tempperson.UserName.Equals(user.UserName))
                    dbperson = tempperson;
            }

            if (dbperson == null)
                return BadRequest();

            if (user.UserName == null || user.Password == null)
                return BadRequest();

            if (user.Password == dbperson.Password)
                return true;

            return false;
        }

        public struct LoginInfo
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
