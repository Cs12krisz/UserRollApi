using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRollAPI.Models;
using UserRollAPI.Models.Dtos;

namespace UserRollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRoleDbContext _context;

        public UserController(UserRoleDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> InsertUser(UserDto userDto)
        {

            try
            {
                var user = new User 
                {
                    Id = Guid.NewGuid(),
                    Name = userDto.Name,
                    Email = userDto.Email,
                    Password = userDto.Password
                };

                if (user != null)
                {
                    await _context.User.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return StatusCode(201, new {message = "Sikeres hozzáadás", result = user});
                }

                return NotFound(new {message = "Sikertelen hozzáadás"});
            }
            catch (Exception ex)
            {

                return StatusCode(400, new {message = ex.Message, result = ""});
            }
        } 
    }
}
