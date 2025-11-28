using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserRollAPI.Models;
using UserRollAPI.Models.Dtos;

namespace UserRollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UserRoleDbContext _context;

        public RoleController(UserRoleDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Role>> InsertRole(RoleDto roleDto)
        {

            try
            {
                var role = new Role
                {
                    Id = Guid.NewGuid(),
                    RoleName = roleDto.RoleName
                };

                if (role != null)
                {
                    await _context.Role.AddAsync(role);
                    await _context.SaveChangesAsync();
                    return StatusCode(201, new { message = "Sikeres hozzáadás", result = role });
                }

                return NotFound(new { message = "Sikertelen hozzáadás" });
            }
            catch (Exception ex)
            {

                return StatusCode(400, new { message = ex.Message, result = "" });
            }
        }

        [HttpGet]
        public async Task<ActionResult<Role>> GetAllRole()
        {

            try
            {
                return Ok(new { message = "Sikeres lekérdezés", result = await _context.Role.ToArrayAsync() });
            }
            catch (Exception ex)
            {

                return StatusCode(400, new { message = ex.Message, result = "" });
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Role>> DeleteAnRole(Guid id)
        {

            try
            {
                var role = await _context.Role.FirstOrDefaultAsync(x => x.Id == id);

                if (role != null)
                {
                    _context.Remove(role);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Sikeres törlés" });
                }

                return NotFound(new { message = "Nincs ilyen id", result = role });
            }
            catch (Exception ex)
            {

                return StatusCode(400, new { message = ex.Message, result = "" });
            }
        }

        [HttpPut]
        public async Task<ActionResult<Role>> UpdateRoleData(Guid id, RoleDto roleDto)
        {

            try
            {
                var role = await _context.Role.FirstOrDefaultAsync(x => x.Id == id);

                if (role != null)
                {
                    role.RoleName = roleDto.RoleName;

                    _context.Role.Update(role);
                    await _context.SaveChangesAsync();
                    return Ok(new { message = "Sikeres frissítés", result = role });
                }

                return NotFound(new { message = "nincs ilyen id", result = role });
            }
            catch (Exception ex)
            {

                return StatusCode(400, new { message = ex.Message, result = "" });
            }
        }
    }
}
