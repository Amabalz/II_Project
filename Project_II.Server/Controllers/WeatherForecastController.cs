using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Project_II.Server.Data;
using Project_II.Server.Models;

namespace Project_II.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OnlyPans_AppController : Controller
    {
        private readonly MyDbContext _myDbContext;
        public OnlyPans_AppController(MyDbContext myDbContext)
        { _myDbContext = myDbContext; }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _myDbContext.UserData.ToListAsync();
            return Ok(users);
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _myDbContext.UserData.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] User userRequest)
        {
            await _myDbContext.AddAsync(userRequest);
            await _myDbContext.SaveChangesAsync();
            return Ok(userRequest);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _myDbContext.UserData.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            _myDbContext.UserData.Remove(user);
            await _myDbContext.SaveChangesAsync();

            return Ok("User deleted successfully");
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(User userRequest)
        {

            var existingUser = await _myDbContext.UserData.FindAsync(userRequest.id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Update the user properties
            existingUser.username = userRequest.username;
            existingUser.password = userRequest.password;
            existingUser.role = userRequest.role;

            try
            {
                _myDbContext.UserData.Update(existingUser);
                await _myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userRequest.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("User updated successfully");
        }

        private bool UserExists(int id)
        {
            return _myDbContext.UserData.Any(e => e.id == id);
        }
    }
}