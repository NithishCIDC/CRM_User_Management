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

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrganization([FromBody] AddOrganizationDTO entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var organization = _organizationservice.GetByEmail(entity.Email!);
                    if (organization is null)
                    {
                        await _organizationservice.AddOrganization(entity);
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

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrganization(Guid id)
        {
            try
            {
                var organization = await _organizationservice.GetById(id);
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

        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
