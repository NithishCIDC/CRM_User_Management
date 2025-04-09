using Microsoft.AspNetCore.Mvc;
using CRM_User.Service.UserService;
using CRM_User.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using CRM_User.Web.Middleware;

namespace CRM_User.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(IUserService _userService) : ControllerBase
    {
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HasPermission("User.Create")]
        public async Task<IActionResult> CreateUser([FromBody] AddUserDTO entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userService.GetByEmail(entity.Email!);
                    if (user is null)
                    {
                        await _userService.CreateUser(entity);
                        Log.Information("User created successfully");
                        return StatusCode(201, new ResponseSuccess { Message = "User created Successfully" });

                    }
                    Log.Warning("User already exists");
                    return BadRequest(new ResponseError { Error = "User already exists" });
                }
                Log.Warning("Invalid User format");
                return BadRequest(new ResponseError { Error = "Invalid Credential format" });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error creating user");
                return StatusCode(500, new ResponseError { Error = ex.Message });
            }
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HasPermission("User.GetById")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                {
                    Log.Warning("User not found");
                    return NotFound(new ResponseError { Error = "User not found" });
                }
                Log.Information("User found");
                return Ok(new ResponseSuccess { Message = "User found", Data = user });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error getting user");
                return StatusCode(500, new ResponseError { Error = ex.Message });
            }
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HasPermission("User.GetAll")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var users = await _userService.GetAllUser();
                if (users == null)
                {
                    Log.Warning("No User found");
                    return NotFound(new ResponseError { Error = "No User found" });
                }
                Log.Information("Users found");
                return Ok(new ResponseSuccess { Message = "User found", Data = users });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error getting users");
                return StatusCode(500, new ResponseError { Error = ex.Message });
            }
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HasPermission("User.Update")]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userService.GetUserById(entity.Id);
                    if (user != null)
                    {

                        await _userService.UpdateUser(user, entity);
                        Log.Information("User updated successfully");
                        return Ok(new ResponseSuccess { Message = "User updated Successfully" });
                    }
                    Log.Warning("User not found");
                    return NotFound(new ResponseError { Error = "User not found" });
                }
                Log.Warning("Invalid User format");
                return BadRequest(new ResponseError { Error = "Invalid Credential format" });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error updating user");
                return StatusCode(500, new ResponseError { Error = ex.Message });
            }
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HasPermission("User.Delete")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userService.GetUserById(id);
                    if (user != null)
                    {
                        await _userService.DeleteUser(user);
                        Log.Information("User deleted successfully");
                        return Ok(new ResponseSuccess { Message = "User deleted Successfully" });
                    }
                    Log.Warning("User not found");
                    return NotFound(new ResponseError { Error = "User not found" });
                }
                Log.Warning("Invalid User format");
                return BadRequest(new ResponseError { Error = "Invalid Credential format" });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error deleting user");
                return StatusCode(500, new ResponseError { Error = ex.Message });
            }
        }
    }
}
