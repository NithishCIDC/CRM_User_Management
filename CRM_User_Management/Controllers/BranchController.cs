using CRM_User.Application.DTO;
using CRM_User.Service.BranchService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_User.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }
        [HttpPost("CreateBranch")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBranch([FromBody] AddBranchDTO branch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _branchService.GetByEmail(branch.Email!);
                    if (response == null)
                    {
                        await _branchService.AddBranch(branch);
                        return Accepted(new ResponseSuccess { Message = "Branch is Created" });
                    }
                    return BadRequest(new ResponseError { Error = "Branch already exists" });
                }
                return BadRequest(new ResponseError { Error = "Invalid Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseError { Error = ex + " Internal Server Error" });
            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBranch()
        {
            try
            {
                var response = await _branchService.GetAll();
                if (response != null)
                {
                    return Ok(new ResponseSuccess { Message = "Data fetched Successfully", Data = response });
                }
                return BadRequest(new ResponseError { Error = "No Branch Found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseError { Error = ex + " Internal Server Error" });
            }
        }
        [HttpGet("GetBranchById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBranchById(Guid id)
        {
            try
            {
                var response = await _branchService.GetById(id);
                if (response != null)
                {
                    return Ok(new ResponseSuccess { Message = "Data fetched Successfully", Data = response });
                }
                return BadRequest(new ResponseError { Error = "No Branch Found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseError { Error = ex + " Internal Server Error" });
            }
        }
        [HttpPut("UpdateBranch")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBranch([FromBody] UpdateBranchDTO branch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _branchService.UpdateBranch(branch);
                    return Accepted(new ResponseSuccess { Message = "Branch is Updated" });
                }
                return BadRequest(new ResponseError { Error = "Invalid Data" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseError { Error = ex + " Internal Server Error" });
            }
        }
        [HttpDelete("DeleteBranch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteBranch(Guid Id)
        {
            try
            {
                var response = await _branchService.GetById(Id);
                if (response != null)
                {
                    await _branchService.DeleteBranch(response);
                    return Ok(new ResponseSuccess { Message = "Branch is Deleted" });
                }
                return BadRequest(new ResponseError { Error = "No Branch Found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseError { Error = ex + " Internal Server Error" });
            }
        }
    }
}
