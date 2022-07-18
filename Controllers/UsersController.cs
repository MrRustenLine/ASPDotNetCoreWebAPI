using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPDotNetCoreWebAPI.Models;
using ASPDotNetCoreWebAPI.Service;

namespace ASPDotNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;
        private BusinessLogic businessLogic;

        public UsersController(UserContext context)
        {
            _context = context;
            businessLogic = new BusinessLogic();
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO2>>> GetUsers(string? sort_by, string? sort_type, string? f, int? page = 1, int? page_size = 10)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            IQueryable<User> query = _context.Users;

            if ((!string.IsNullOrEmpty(sort_by)) && (!string.IsNullOrEmpty(sort_type)))
            {
                switch (sort_by.ToLower())
                {
                    case "email":
                        if (sort_type == "asc")
                        {
                            query = query.OrderBy(u => u.Email);
                        }
                        else
                        {
                            query = query.OrderByDescending(u => u.Email);
                        }
                        break;
                    case "displayname":
                        if (sort_type == "asc")
                        {
                            query = query.OrderBy(u => u.DisplayName);
                        }
                        else
                        {
                            query = query.OrderByDescending(u => u.DisplayName);
                        }
                        break;
                    case "id":
                        if (sort_type == "asc")
                        {
                            query = query.OrderBy(u => u.Id);
                        }
                        else
                        {
                            query = query.OrderByDescending(u => u.Id);
                        }
                        break;
                }
            }

            if (!string.IsNullOrEmpty(f))
            {
                int equalIndex = f.IndexOf("=");
                if (equalIndex > 4)
                {
                    string filterType = f.Substring(0, equalIndex);
                    string filterValue = f.Substring(equalIndex + 1);
                    switch (filterType.ToLower())
                    {
                        case "email":
                            query = query.Where(u => u.Email.ToLower().Contains(filterValue));
                            break;
                        case "displayname":
                            query = query.Where(u => u.DisplayName.ToLower().Contains(filterValue));
                            break;
                    }
                }
            }

            return await query.Skip(((int)page - 1) * (int)page_size).Take((int)page_size)
                .Select(x => UserToDTO2(x))
                .ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, UserDTO userDTO)
        {
            try
            {
                if (id != userDTO.Id)
                {
                    return BadRequest();
                }

                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                user.DisplayName = userDTO.DisplayName;
                if (businessLogic.ValidatePassword(userDTO.Password))
                {
                    user.Password = userDTO.Password;
                }
                _context.Entry(user).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            catch (ArgumentException ae)
            {
                return Problem($"User could not be updated. {ae.Message}");
            }
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'UserContext.Users'  is null.");
            }

            try
            {
                if (businessLogic.ValidateNewUser(user))
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
                }
                else
                {
                    return Problem("New user could not be validated.");
                }
            }
            catch (ArgumentException ae)
            {
                return Problem($"New user could not be validated. {ae.Message}");
            }

        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static UserDTO2 UserToDTO2(User user) => new UserDTO2
        {
        Id = user.Id,
        DisplayName = user.DisplayName,
        Email = user.Email
        };

    }
}
