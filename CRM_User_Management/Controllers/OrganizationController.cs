﻿using CRM_User.Application.DTO;
using CRM_User.Service.OrganizationService;
using CRM_User.Web.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;


namespace CRM_User.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationservice)
        {
            _organizationService = organizationservice;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HasPermission("Org.Create")]
        public async Task<IActionResult> CreateOrganization([FromBody] AddOrganizationDTO entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var organization = await _organizationService.GetByEmail(entity.Email!);
                    if (organization is null)
                    {
                        await _organizationService.AddOrganization(entity);
                        Log.Information("Organization Created Successfully");
                        return StatusCode(201,new ResponseSuccess { Message = "Organization Successfully Created" });
                    }
                    Log.Warning("Organization Already Exist");
                    return BadRequest(new ResponseError { Error = "Organization Already Exist" });
                }
                Log.Warning("Invalid Request");
                return BadRequest(new ResponseError { Error = "Invalid Request" });
            }
            catch (Exception ex)
            {
                Log.Error("Error in CreateOrganization: " + ex);
                return StatusCode(500, new ResponseError { Error = ex + " Internal Server Error" });
            }
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HasPermission("Org.GetAll")]
        public async Task<IActionResult> GetAllOrganization()
        {
            try
            {
                var organizations = await _organizationService.GetAll();
                if (organizations != null)
                {
                    Log.Information("All Organizations Retrieved Successfully");
                    return Ok(new ResponseSuccess { Message = "Data fetched Successfully", Data = organizations });
                }
                Log.Warning("No Organization Found");
                return BadRequest(new ResponseError { Error = "No Organization Found" });
            }
            catch (Exception ex)
            {
                Log.Error("Error in AllOrganization: " + ex);
                return StatusCode(500, new ResponseError { Error = ex + " Internal Server Error" });
            }
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HasPermission("Org.GetById")]
        public async Task<IActionResult> GetOrganizationById(Guid id)
        {
            try
            {
                var organization = await _organizationService.GetById(id);
                if (organization != null)
                {
                    Log.Information("Organization Retrieved Successfully");
                    return Ok(new ResponseSuccess { Message = "Data fetched Successfully", Data = organization });
                }
                Log.Warning("Organization Not Found");
                return NotFound(new ResponseError { Error = "Organization Not Found" });
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetOrganization: " + ex); ;
                return StatusCode(500, new ResponseError { Error = ex + " Internal Server Error" });
            }
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HasPermission("Org.Update")]
        public async Task<IActionResult> UpdateOrganization(UpdateOraganizationDTO entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var organization = await _organizationService.GetById(entity.Id);
                    if (organization is not null)
                    {
                        await _organizationService.UpdateOrganization(organization,entity);
                        Log.Information("Organization Updated Successfully");
                        return StatusCode(202,new ResponseSuccess { Message = "Organization Successfully Updated" });
                    }
                    Log.Warning("Organization Not Found");
                    return NotFound(new ResponseError { Error = "Organization Not Found" });
                }
                Log.Warning("Invalid Request");
                return BadRequest(new ResponseError { Error = "Invalid Request" });
            }
            catch (Exception ex)
            {
                Log.Error("Error in UpdateOrganization: " + ex);
                return StatusCode(500, new ResponseError { Error = ex + " Internal Server Error" });
            }
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HasPermission("Org.Delete")]
        public async Task<IActionResult> DeleteOrganization(Guid Id)
        {
            try
            {
                var organization = await _organizationService.GetById(Id);
                if (organization is not null)
                {
                    await _organizationService.DeleteOrganization(organization);
                    Log.Information("Organization Deleted Successfully");
                    return Ok(new ResponseSuccess { Message = "Organization Successfully Deleted" });
                }
                Log.Warning("Organization Not Found");
                return NotFound(new ResponseError { Error = "Organization Not Found" });
            }
            catch (Exception ex)
            {
                Log.Error("Error in DeleteOrganization: " + ex);
                return StatusCode(500, new ResponseError { Error = ex + " Internal Server Error" });
            }
        }
    }
}
