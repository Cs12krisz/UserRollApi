using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRollAPI.Models;
using UserRollAPI.Models.Dtos;

namespace UserRollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleUserController : ControllerBase
    {
        private readonly UserRoleDbContext _context;

        public RoleUserController(UserRoleDbContext context) 
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> AddNewRoleToUser(AddNewSwitchDto addNewSwitchDto)
        {
            try
            {
                var addroleUser = new RoleUser()
                {
                    UsersId = addNewSwitchDto.UserId,
                    RolesId = addNewSwitchDto.RoleId
                };

                await _context.RoleUser.AddAsync(addroleUser);
                await _context.SaveChangesAsync();
                return Ok(addroleUser);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
