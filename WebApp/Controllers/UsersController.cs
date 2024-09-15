using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlLibrary.Dataverse;
using SqlLibrary.Queries;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserQuery _userRepository;

        public UsersController(UserQuery userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                // Log the operation
                DatabaseConfig.LogDatabaseOperation("Handling GET request for users.");

                var users = _userRepository.GetUsers();

                return Ok(users); // Return the list of users
            }
            catch (Exception ex)
            {
                // Log the error
                DatabaseConfig.LogError("An error occurred while handling GET request for users.", ex);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}