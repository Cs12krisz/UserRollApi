using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRollAPI.Models;

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


    }
}
