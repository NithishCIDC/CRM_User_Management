using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRM_User.Service.UserService;
using CRM_User.Domain.Model;
using CRM_User.Application.DTO;
using Mapster;
using Microsoft.AspNetCore.Authorization;

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
                        return StatusCode(201, new ResponseSuccess { Message = "User created Successfully" });

                    }
                    return BadRequest(new ResponseError { Error = "User already exists" });
                }
                return BadRequest(new ResponseError { Error = "Invalid Credential format" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseError { Error = ex.Message });
            }
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound(new ResponseError { Error = "User not found" });
                }
                return Ok(new ResponseSuccess { Message = "User found", Data = user });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseError { Error = ex.Message });
            }
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var users = await _userService.GetAllUser();
                if (users == null)
                {
                    return NotFound(new ResponseError { Error = "No User found" });
                }
                return Ok(new ResponseSuccess { Message = "User found", Data = users });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseError { Error = ex.Message });
            }
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userService.GetUserById(entity.Id);
                    if (user!=null)
                    {
                        await _userService.UpdateUser(user,entity);
                        return Ok(new ResponseSuccess { Message = "User updated Successfully" });
                    }
                    return NotFound(new ResponseError { Error = "User not found" });
                }
                return BadRequest(new ResponseError { Error = "Invalid Credential format" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseError { Error = ex.Message });
            }
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                        return Ok(new ResponseSuccess { Message = "User deleted Successfully" });
                    }
                    return NotFound(new ResponseError { Error = "User not found" });
                }
                return BadRequest(new ResponseError { Error = "Invalid Credential format" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseError { Error = ex.Message });
            }
        }
    }
}
