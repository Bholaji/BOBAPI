using Bob.Model.DTO;
using Bob.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bob.Model.DTO.PaginationDTO;

namespace Bob.Core.Services.IServices
{
	public interface IOrganizationService
	{
		Task<APIResponse<OrganizationDTO>> CreateOrganization(OrganizationDTO organizationDTO);
		Task<APIResponse<List<OrganizationDTO>>> GetAllOrganizations(PaginationDTO DTO);
	}
}
