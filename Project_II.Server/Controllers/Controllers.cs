using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_II.Server.Data;
using Project_II.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace Project_II.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]

    
    //!!!!!!!!!!!!!!!//
    //USER CONTROLLER//
    //!!!!!!!!!!!!!!!//
    public class UserController : Controller
    {
        private readonly MyDbContext _myDbContext;
        public UserController(MyDbContext myDbContext)
        { _myDbContext = myDbContext; }



        //GET ALL USERS
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _myDbContext.UserData.ToListAsync();
            return Ok(users);
        }



        //GET USER BY ID
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


        //ADD USER
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] User userRequest)
        {
            var existingUserEmail = await _myDbContext.UserData.FirstOrDefaultAsync(user => user.email == userRequest.email);
            var existingUserUsername = await _myDbContext.UserData.FirstOrDefaultAsync(user => user.username == userRequest.username);

            if (!new EmailAddressAttribute().IsValid(userRequest.email))
            {
                return BadRequest("Invalid email address");
            }
            if (existingUserEmail != null)
            {
                return BadRequest("User with the same email already exists.");
            }

            if (existingUserUsername != null)
            {
                return BadRequest("User with the same username already exists.");
            }


            var passwordHasher = new PasswordHasher<User>();
            userRequest.password = passwordHasher.HashPassword(userRequest, userRequest.password);

            await _myDbContext.AddAsync(userRequest);
            await _myDbContext.SaveChangesAsync();
            return Ok(userRequest);
        }


        //DELETE USER
        [HttpDelete("DeleteUser/{id}")] //SEE LATER
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


        //UPDATE USER
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(User userRequest)
        {
            var existingUser = await _myDbContext.UserData.FindAsync(userRequest.id);
            if (existingUser == null)
            {
                return NotFound();
            }

            if (userRequest.email != null)
            {
                if (!new EmailAddressAttribute().IsValid(userRequest.email))
                {
                    return BadRequest("Invalid email address");
                }

                var existingUserEmail = await _myDbContext.UserData.FirstOrDefaultAsync(user => user.email == userRequest.email);
                if (existingUserEmail != null)
                {
                    return BadRequest("User with the same email already exists");
                }
                existingUser.email = userRequest.email;
            }

            if(userRequest.username != null)
            {
                var existingUserUsername = await _myDbContext.UserData.FirstOrDefaultAsync(user => user.username == userRequest.username);
                if (existingUserUsername != null)
                {
                    return BadRequest("The username is taken");
                }
                existingUser.username = userRequest.username;
            }
            
            if(userRequest.profile_picture != null)
            {
                existingUser.profile_picture = userRequest.profile_picture;
            }

            if(userRequest.password != null)
            {
                var passwordHasher = new PasswordHasher<User>();
                userRequest.password = passwordHasher.HashPassword(userRequest, userRequest.password);
                existingUser.password = userRequest.password;
            }

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


        //UPDATE USER LEVEL
        [HttpPut("UpdateUserLevel")]
        public async Task<IActionResult> UpdateUserLevel(User userRequest)
        {
            var existingUser = await _myDbContext.UserData.FindAsync(userRequest.id);
            if (existingUser == null)
            {
                return NotFound();
            }
            existingUser.level = userRequest.level;
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
            return Ok("User level updated successfully");

        }


        //UPDATE USER ROLE
        [HttpPut("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole(User userRequest)
        {
            var existingUser = await _myDbContext.UserData.FindAsync(userRequest.id);
            if (existingUser == null)
            {
                return NotFound();
            }
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
            return Ok("User role updated successfully");

        }


        //LOGIN
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] User userRequest)
        {
            var existingUser = await _myDbContext.UserData.FirstOrDefaultAsync(user => user.email == userRequest.email || user.username == userRequest.username);
            if(existingUser == null)
            {
                return NotFound("User not found");
            }

            var passwordHasher = new PasswordHasher<User>();
            if(passwordHasher.VerifyHashedPassword(userRequest, existingUser.password, userRequest.password) == 0)
            {
                return Unauthorized("Password and user don't match");
            }

            return Ok("Login was successful");
        }


        //EXTRA
        private bool UserExists(int id)
        {
            return _myDbContext.UserData.Any(e => e.id == id);
        }
    }

    //!!!!!!!!!!!!!!!!!//
    //TICKET CONTROLLER//
    //!!!!!!!!!!!!!!!!!//
    public class TicketController : Controller
    {
        private readonly MyDbContext _myDbContext;
        public TicketController(MyDbContext myDbContext)
        { _myDbContext = myDbContext; }


        //GET ALL TICKETS
        [HttpGet("GetAllTickets")]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _myDbContext.TicketData.ToListAsync();
            return Ok(tickets);
        }


        //GET TICKET BY ID
        [HttpGet("GetTicket/{id}")]
        public async Task<IActionResult> GetTicket(int id)
        {
            var ticket = await _myDbContext.TicketData.FindAsync(id);
            if (ticket == null)
            {
                return NotFound("Ticket not found");
            }
            return Ok(ticket);
        }


        //ADD TICKET
        [HttpPost("AddTicket")]
        public async Task<IActionResult> AddTicket([FromBody] Ticket ticketRequest)
        {
            await _myDbContext.AddAsync(ticketRequest);
            await _myDbContext.SaveChangesAsync();
            return Ok(ticketRequest);
        }


        //DELETE TICKET
        [HttpDelete("DeleteTicket/{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _myDbContext.TicketData.FindAsync(id);
            if (ticket == null)
            {
                return NotFound("Ticket not found");
            }

            _myDbContext.TicketData.Remove(ticket);
            await _myDbContext.SaveChangesAsync();

            return Ok("Ticket deleted successfully");
        }
        

        //UPDATE TICKET
        [HttpPut("UpdateTicket")]
        public async Task<IActionResult> UpdateTicket(Ticket ticketRequest)
        {

            var existingTicket = await _myDbContext.TicketData.FindAsync(ticketRequest.id);
            if (existingTicket == null)
            {
                return NotFound();
            }

            // Update the ticket properties
            existingTicket.title = ticketRequest.title;
            existingTicket.data = ticketRequest.data;
            existingTicket.status = ticketRequest.status;

            try
            {
                _myDbContext.TicketData.Update(existingTicket);
                await _myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(ticketRequest.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Ticket updated successfully");
        }


        //EXTRA
        private bool TicketExists(int id)
        {
            return _myDbContext.TicketData.Any(e => e.id == id);
        }
    }

    //!!!!!!!!!!!!!!!!!!!//
    //COMMENTS CONTROLLER//
    //!!!!!!!!!!!!!!!!!!!//
    public class CommentController : Controller
    {
        private readonly MyDbContext _myDbContext;
        public CommentController(MyDbContext myDbContext)
        { _myDbContext = myDbContext; }


        //GET ALL COMMENTS
        [HttpGet("GetAllComments")]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _myDbContext.CommentData.ToListAsync();
            return Ok(comments);
        }


        //GET COMMENT BY ID
        [HttpGet("GetComment/{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _myDbContext.CommentData.FindAsync(id);
            if (comment == null)
            {
                return NotFound("Comment not found");
            }
            return Ok(comment);
        }


        //ADD COMMENT
        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment([FromBody] Comment commentRequest)
        {
            await _myDbContext.AddAsync(commentRequest);
            await _myDbContext.SaveChangesAsync();
            return Ok(commentRequest);
        }


        //DELETE COMMENT
        [HttpDelete("DeleteComment/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _myDbContext.CommentData.FindAsync(id);
            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            _myDbContext.CommentData.Remove(comment);
            await _myDbContext.SaveChangesAsync();

            return Ok("Comment deleted successfully");
        }


        //UPDATE COMMENT
        [HttpPut("UpdateComment")]
        public async Task<IActionResult> UpdateComment(Comment commentRequest)
        {

            var existingComment = await _myDbContext.CommentData.FindAsync(commentRequest.id);
            if (existingComment == null)
            {
                return NotFound();
            }

            existingComment.data = commentRequest.data;
            existingComment.likes = commentRequest.likes;
            existingComment.dislikes = commentRequest.dislikes;

            try
            {
                _myDbContext.CommentData.Update(existingComment);
                await _myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(commentRequest.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Comment updated successfully");
        }


        //EXTRA
        private bool CommentExists(int id)
        {
            return _myDbContext.CommentData.Any(e => e.id == id);
        }
    }

    //!!!!!!!!!!!!!!!//
    //BANS CONTROLLER//
    //!!!!!!!!!!!!!!!//
    public class BanController : Controller
    {
        private readonly MyDbContext _myDbContext;
        public BanController(MyDbContext myDbContext)
        { _myDbContext = myDbContext; }


        //GET ALL BANS
        [HttpGet("GetAllBans")]
        public async Task<IActionResult> GetAllBans()
        {
            var bans = await _myDbContext.BanList.ToListAsync();
            return Ok(bans);
        }


        //GET BAN BY ID
        [HttpGet("GetBan/{id}")]
        public async Task<IActionResult> GetBan(int id)
        {
            var ban = await _myDbContext.BanList.FindAsync(id);
            if (ban == null)
            {
                return NotFound("Ban not found");
            }
            return Ok(ban);
        }


        //ADD BAN
        [HttpPost("AddBan")]
        public async Task<IActionResult> AddBan([FromBody] Ban banRequest)
        {
            await _myDbContext.AddAsync(banRequest);
            await _myDbContext.SaveChangesAsync();
            return Ok(banRequest);
        }


        //DELETE BAN
        [HttpDelete("DeleteBan/{id}")]
        public async Task<IActionResult> DeleteBan(int id)
        {
            var ban = await _myDbContext.BanList.FindAsync(id);
            if (ban == null)
            {
                return NotFound("Ban not found");
            }

            _myDbContext.BanList.Remove(ban);
            await _myDbContext.SaveChangesAsync();

            return Ok("Ban deleted successfully");
        }


        //UPDATE BAN
        [HttpPut("UpdateBan")]
        public async Task<IActionResult> UpdateBan(Ban banRequest)
        {

            var existingBan = await _myDbContext.BanList.FindAsync(banRequest.id);
            if (existingBan == null)
            {
                return NotFound();
            }

            existingBan.email = banRequest.email;
            existingBan.reason = banRequest.reason;

            try
            {
                _myDbContext.BanList.Update(existingBan);
                await _myDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BanExists(banRequest.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Ban updated successfully");
        }


        //EXTRA
        private bool BanExists(int id)
        {
            return _myDbContext.BanList.Any(e => e.id == id);
        }
    }
}