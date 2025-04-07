using CRM_User.Application.DTO;
using CRM_User.Service.BranchService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
                        Log.Information("Branch Created Successfully");
                        return Accepted(new AuthResponseSuccess { Message = "Branch is Created"});
                    }
                    Log.Warning("Branch Already Exist");
                    return BadRequest(new AuthResponseError { Error = "Branch already exists" });
                }
                Log.Warning("Invalid Data");
                return BadRequest(new AuthResponseError { Error = "Invalid Data" });
            }
            catch (Exception ex)
            {
                Log.Error("Error in CreateBranch: " + ex);
                return StatusCode(500, new AuthResponseError { Error = " Internal Server Error" });
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
                    Log.Information("All Branches Retrieved Successfully");
                    return Ok(response);
                }
                Log.Warning("No Branch Found");
                return BadRequest(new AuthResponseError { Error = "No Branch Found" });
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetAllBranch: " + ex);
                return StatusCode(500, new AuthResponseError { Error = " Internal Server Error" });
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
                    Log.Information("Branch Retrieved Successfully");
                    return Ok(response);
                }
                Log.Warning("No Branch Found");
                return BadRequest(new AuthResponseError { Error = "No Branch Found" });
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetBranchById: " + ex);
                return StatusCode(500, new AuthResponseError { Error = " Internal Server Error" });
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
                    Log.Information("Branch Updated Successfully");
                    return Accepted(new AuthResponseSuccess { Message = "Branch is Updated" });
                }
                Log.Warning("Invalid Data");
                return BadRequest(new AuthResponseError { Error = "Invalid Data" });
            }
            catch (Exception ex)
            {
                Log.Error("Error in UpdateBranch: " + ex);
                return StatusCode(500, new AuthResponseError { Error = " Internal Server Error" });
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
                    Log.Information("Branch Deleted Successfully");
                    return Ok(new AuthResponseSuccess { Message = "Branch is Deleted" });
                }
                Log.Warning("No Branch Found");
                return BadRequest(new AuthResponseError { Error = "No Branch Found" });
            }
            catch (Exception ex)
            {
                Log.Error("Error in DeleteBranch: " + ex);
                return StatusCode(500, new AuthResponseError { Error = " Internal Server Error" });
            }
        }
    }
}
