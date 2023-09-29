using CAVBackEndUpdate.Models;
using CAVBackEndUpdate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CAVBackEndUpdate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       private readonly IUserService _userService;
     public UserController(IUserService userService)
        { 
            _userService = userService;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
           return _userService.GetUsersDataWithOutDate();

        }
        [HttpGet("GetUsersithDateRange")]
        public async Task<ActionResult<List<User>>> GetUsersByDate([FromQuery] AccountDateParameter dateParameter)
        {
            if (dateParameter.StartDate == null || dateParameter.EndDate == null)
            {
                return BadRequest();
            }
            return _userService.GetUsersFromDb(dateParameter);
        }

     
        [HttpGet("GetUserById{'Id'}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {

            var user = _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

    }
}
