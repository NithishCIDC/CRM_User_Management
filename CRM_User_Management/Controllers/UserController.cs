using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRM_User.Service.UserService;
using CRM_User.Domain.Model;
using CRM_User.Application.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Mapster;

namespace CRM_User.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                    bool result = await _userService.CreateUser(entity.Adapt<User>());

                    return result
                    ? Ok(new ResponseDTO { Success = true, Message = "User created Successfully" })
                    : BadRequest(new ResponseDTO { Success = false, Message = "User Creation Failed" });
                }
                else
                {
                    return BadRequest(new ResponseDTO { Success = false, Message = "Invalid Credential format" });
                }
            }
            catch (Exception Ex)
            {
                return StatusCode(500, new ResponseDTO { Success = false, Message = Ex.Message });
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound(new ResponseDTO { Success = false, Message = "User not found" });
                }
                return Ok(new ResponseDTO { Success = true, Message = "User found", Data = [user] });
            }
            catch (Exception Ex)
            {
                return StatusCode(500, new ResponseDTO { Success = false, Message = Ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var users = await _userService.GetAllUser();
                if (users == null)
                {
                    return NotFound(new ResponseDTO { Success = false, Message = "No User found" });
                }
                return Ok(new ResponseDTO { Success = true, Message = "User found", Data = [users] });
            }
            catch (Exception Ex)
            {
                return StatusCode(500, new ResponseDTO { Success = false, Message = Ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateDTO entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = await _userService.UpdateUser(entity.Adapt<User>());
                    return result
                    ? Ok(new ResponseDTO { Success = true, Message = "User updated Successfully" })
                    : BadRequest(new ResponseDTO { Success = false, Message = "User Update Failed" });
                }
                else
                {
                    return BadRequest(new ResponseDTO { Success = false, Message = "Invalid Credential format" });
                }
            }
            catch (Exception Ex)
            {
                return StatusCode(500, new ResponseDTO { Success = false, Message = Ex.Message });
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = await _userService.DeleteUser(id);
                    return result
                    ? Ok(new ResponseDTO { Success = true, Message = "User deleted Successfully" })
                    : BadRequest(new ResponseDTO { Success = false, Message = "User Deletion Failed" });
                }
                else
                {
                    return BadRequest(new ResponseDTO { Success = false, Message = "Invalid Credential format" });
                }
            }
            catch (Exception Ex)
            {
                return StatusCode(500, new ResponseDTO { Success = false, Message = Ex.Message });
            }
        }
    }
}
