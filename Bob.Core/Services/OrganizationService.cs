using Bob.Model.DTO;
using Bob.Model.Entities;
using Bob.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bob.DataAccess.Repository.IRepository;
using Microsoft.Extensions.Logging;
using Bob.Core.Services.IServices;
using Bob.Core.Exceptions;
using Bob.Model.DTO.PaginationDTO;
using Bob.Model.Entities.Home;

namespace Bob.Core.Services
{
	public class OrganizationService : IOrganizationService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger<OrganizationService> _logger;
		public OrganizationService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrganizationService> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_logger = logger;
		}
		public async Task<APIResponse<OrganizationDTO>> CreateOrganization(OrganizationDTO organizationDTO)
		{
			var organization = _mapper.Map<Organization>(organizationDTO);
			var today = DateTime.Now;
			organization.CreationDate = today;
			organization.ModificationDate = today;

			await _unitOfWork.OrganizationRepository.CreateAsync(organization);
			await _unitOfWork.SaveAsync();

			return new APIResponse<OrganizationDTO>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<OrganizationDTO>(organization)
			};
		}

		public async Task<APIResponse<List<OrganizationDTO>>> GetAllOrganizations(PaginationDTO DTO)
		{
			IEnumerable<Organization> organizations = await _unitOfWork.OrganizationRepository
				.GetAllAsync(pageSize: DTO.PageSize, pageNumber: DTO.PageNumber);

			if(organizations is null)
			{
				throw new NotFoundException($"{nameof(Organization)} {ResponseMessage.NotFound}");
			}

			return new APIResponse<List<OrganizationDTO>>
			{
				IsSuccess = true,
				Message = ResponseMessage.IsSuccess,
				Result = _mapper.Map<List<OrganizationDTO>>(organizations)
			};
		}
	}
}
