using Bob.Core.Services.IServices;
using Bob.DataAccess.Repository.IRepository;
using Bob.Model.DTO;
using Bob.Model.DTO.PaginationDTO;
using Microsoft.AspNetCore.Mvc;

namespace BobAPI.Controllers
{
	[Route("api/organization")]
	[ApiController]
	public class OrganizationController(IOrganizationService organizationService) : ControllerBase
	{
		private readonly IOrganizationService _organizationService = organizationService;

        [HttpPost("create")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> CreateOrganization([FromBody] OrganizationDTO organizationDTO)
		{
			var response = await _organizationService.CreateOrganization(organizationDTO);
			return Ok(response);

		}

		[HttpGet("getall")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAllOrganizations([FromQuery] PaginationDTO DTO)
		{
			var response = await _organizationService.GetAllOrganizations(DTO);
			return Ok(response);
		}
	}
}
