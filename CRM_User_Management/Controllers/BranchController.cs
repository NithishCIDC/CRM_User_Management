﻿using CRM_User.Application.DTO;
using CRM_User.Service.BranchService;
using CRM_User.Web.Middleware;
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

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HasPermission("Branch.Create")]
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
                        return StatusCode(201,new ResponseSuccess { Message = "Branch is Created" });

                    }
                    Log.Warning("Branch Already Exist");
                    return BadRequest(new ResponseError { Error = "Branch already exists" });
                }
                Log.Warning("Invalid Data");
                return BadRequest(new ResponseError { Error = "Invalid Data" });
            }
            catch (Exception ex)
            {
                Log.Error("Error in CreateBranch: " + ex);
                return StatusCode(500, new ResponseError { Error = ex + " Internal Server Error" });
            }
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HasPermission("Branch.GetAll")]
        public async Task<IActionResult> GetAllBranch()
        {
            try
            {
                var response = await _branchService.GetAll();
                if (response != null)
                {
                    Log.Information("All Branches Retrieved Successfully");
                    return Ok(new ResponseSuccess { Message = "Data fetched Successfully", Data = response });
                }
                Log.Warning("No Branch Found");
                return BadRequest(new ResponseError { Error = "No Branch Found" });
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetAllBranch: " + ex);
                return StatusCode(500, new ResponseError { Error = ex + " Internal Server Error" });
            }
        }

        [HttpGet("{Id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HasPermission("Branch.GetById")]
        public async Task<IActionResult> GetBranchById(Guid id)
        {
            try
            {
                var response = await _branchService.GetById(id);
                if (response != null)
                {
                    Log.Information("Branch Retrieved Successfully");
                    return Ok(new ResponseSuccess { Message = "Data fetched Successfully", Data = response });
                }
                Log.Warning("No Branch Found");
                return BadRequest(new ResponseError { Error = "No Branch Found" });
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetBranchById: " + ex);
                return StatusCode(500, new ResponseError { Error = ex + " Internal Server Error" });
            }
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HasPermission("Branch.Update")]
        public async Task<IActionResult> UpdateBranch([FromBody] UpdateBranchDTO entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var branch = await _branchService.GetById(entity.Id);
                    if (branch != null)
                    {
                        await _branchService.UpdateBranch(branch,entity);
                        Log.Information("Branch Updated Successfully");
                        return Accepted(new ResponseSuccess { Message = "Branch is Updated" });
                    }
                    Log.Warning("No Branch Found");
                    return NotFound(new ResponseError { Error = "No Branch Found" });
                }
                Log.Warning("Invalid Data");
                return BadRequest(new ResponseError { Error = "Invalid Data" });
            }
            catch (Exception ex)
            {
                Log.Error("Error in UpdateBranch: " + ex);
                return StatusCode(500, new ResponseError { Error = ex + " Internal Server Error" });
            }
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HasPermission("Branch.Delete")]
        public async Task<IActionResult> DeleteBranch(Guid Id)
        {
            try
            {
                var response = await _branchService.GetById(Id);
                if (response != null)
                {
                    await _branchService.DeleteBranch(response);
                    Log.Information("Branch Deleted Successfully");
                    return Ok(new ResponseSuccess { Message = "Branch is Deleted" });
                }
                Log.Warning("No Branch Found");
                return NotFound(new ResponseError { Error = "No Branch Found" });
            }
            catch (Exception ex)
            {
                Log.Error("Error in DeleteBranch: " + ex);
                return StatusCode(500, new ResponseError { Error = ex + " Internal Server Error" });
            }
        }
    }
}
