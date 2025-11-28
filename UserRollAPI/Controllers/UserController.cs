using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserRollAPI.Models;
using UserRollAPI.Models.Dtos;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

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
                byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: userDto.Password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));

                var user = new User 
                {
                    Id = Guid.NewGuid(),
                    Name = userDto.Name,
                    Email = userDto.Email,
                    Password = hashed
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

        [HttpGet]
        public async Task<ActionResult<User>> GetAllUser()
        {

            try
            {
                return Ok(new { message = "Sikeres lekérdezés", result = await _context.User.ToArrayAsync() });
            }
            catch (Exception ex)
            {

                return StatusCode(400, new { message = ex.Message, result = "" });
            }
        }

        [HttpDelete]
        public async Task<ActionResult<User>> DeleteAnUser(Guid id)
        {

            try
            {
               var user = await _context.User.FirstOrDefaultAsync(x => x.Id == id);

                if (user != null) 
                {
                    _context.Remove(user);
                    await _context.SaveChangesAsync();
                    return Ok(new {message = "Sikeres törlés"});
                }

                return NotFound(new { message = "Nincs ilyen id", result = user });
            }
            catch (Exception ex)
            {

                return StatusCode(400, new { message = ex.Message, result = "" });
            }
        }

        [HttpPut]
        public async Task<ActionResult<User>> UpdateUserData(Guid id, UserDto userDto)
        {

            try
            {
                var user = await _context.User.FirstOrDefaultAsync(x => x.Id == id);

                if (user != null) 
                {
                    user.Name = userDto.Name;
                    user.Email = userDto.Email;
                    user.Password = userDto.Password;

                    _context.User.Update(user);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Sikeres frissítés", result = user});
                }

                return NotFound(new { message = "nincs ilyen id", result = user});
            }
            catch (Exception ex)
            {

                return StatusCode(400, new { message = ex.Message, result = "" });
            }
        }
    }
}
