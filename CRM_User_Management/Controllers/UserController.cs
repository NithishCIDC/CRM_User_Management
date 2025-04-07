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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(AddUserDTO entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userService.GetByEmail(entity.Email!);
                    if (user is not null)
                    {
                        await _userService.CreateUser(entity.Adapt<User>());
                        return Accepted(new ResponseSuccess {Message = "User created Successfully" });
                    }
                    return BadRequest(new ResponseError {  Error = "User already exists" });
                }
                return BadRequest(new ResponseError { Error = "Invalid Credential format" });
            }
            catch (Exception Ex)
            {
                return StatusCode(500, new ResponseError { Error = Ex.Message });
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
            catch (Exception Ex)
            {
                return StatusCode(500, new ResponseError { Error = Ex.Message });
            }
        }

        [HttpGet]
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
            catch (Exception Ex)
            {
                return StatusCode(500, new ResponseError { Error = Ex.Message });
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(UpdateDTO entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = await _userService.UpdateUser(entity.Adapt<User>());
                    return result
                    ? Ok(new ResponseSuccess {  Message = "User updated Successfully" })
                    : BadRequest(new ResponseError { Error = "User Update Failed" });
                }
                else
                {
                    return BadRequest(new ResponseError {  Error = "Invalid Credential format" });
                }
            }
            catch (Exception Ex)
            {
                return StatusCode(500, new ResponseError { Error = Ex.Message });
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
                    bool result = await _userService.DeleteUser(id);
                    return result
                    ? Ok(new ResponseSuccess { Message = "User deleted Successfully" })
                    : BadRequest(new ResponseError {  Error = "User Deletion Failed" });
                }
                else
                {
                    return BadRequest(new ResponseError { Error = "Invalid Credential format" });
                }
            }
            catch (Exception Ex)
            {
                return StatusCode(500, new ResponseError { Error = Ex.Message });
            }
        }
    }
}
