using CRM_User.Application.DTO;
using CRM_User.Service.OrganizationService;
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
        private readonly IOrganizationService _organizationservice;

        public OrganizationController(IOrganizationService organizationservice)
        {
            _organizationservice = organizationservice;
        }

        [HttpPost("CreateOrganization")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrganization(AddOrganizationDTO entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var organization = _organizationservice.GetByEmail(entity.Email!);
                    if (organization is not null)
                    {
                        Log.Warning("Organization Already Exist");
                        return BadRequest(new ResponseError { Error = "Organization Already Exist" });
                    }
                    else
                    {
                        await _organizationservice.AddOrganization(entity);
                        Log.Information("Organization Created Successfully");
                        return Accepted(new ResponseSuccess { Message = "Organization Successfully Created" });
                    }

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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AllOrganization()
        {
            try
            {
                var organizations = await _organizationservice.GetAll();
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

        [HttpGet("GetOrganization/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrganization(Guid id)
        {
            try
            {
                var organization = await _organizationservice.GetById(id);
                if (organization != null)
                {
                    Log.Information("Organization Retrieved Successfully");
                    return Ok(organization);
                    return Ok(new ResponseSuccess { Message = "Data fetched Successfully", Data = organization });
                }
                Log.Warning("Organization Not Found");
                return BadRequest(new ResponseError { Error = "Organization Not Found" });
            }
            catch (Exception ex)
            {
                Log.Error("Error in GetOrganization: " + ex);;
                return StatusCode(500, new ResponseError { Error = ex + " Internal Server Error" });
            }
        }

        [HttpPut("UpdateOrganization")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOrganization(UpdateOraganizationDTO entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var organization = await _organizationservice.GetById(entity.Id);
                    if (organization is not null)
                    {
                        await _organizationservice.UpdateOrganization(entity);
                        Log.Information("Organization Updated Successfully");
                        return Accepted(new ResponseSuccess { Message = "Organization Successfully Updated" });
                    }
                    Log.Warning("Organization Not Found");
                    return BadRequest(new ResponseError { Error = "Organization Not Found" });
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

        [HttpDelete("DeleteOrganization")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOrganization(Guid Id)
        {
            try
            {
                var organization = await _organizationservice.GetById(Id);
                if (organization is not null)
                {
                    await _organizationservice.DeleteOrganization(organization);
                    Log.Information("Organization Deleted Successfully");
                    return Ok(new ResponseSuccess { Message = "Organization Successfully Deleted" });
                }
                Log.Warning("Organization Not Found");
                return BadRequest(new ResponseError { Error = "Organization Not Found" });
            }
            catch (Exception ex)
            {
                Log.Error("Error in DeleteOrganization: " + ex);
                return StatusCode(500, new ResponseError { Error = ex + " Internal Server Error" });
            }
        }
    }
}
