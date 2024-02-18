using Bob.Model;
using Bob.Model.DTO;
using Bob.Model.DTO.PaginationDTO;
using Bob.Model.DTO.ShoutoutDTO;
using Bob.Model.DTO.UserDTO;

namespace Bob.Core.Services.IServices
{
	public interface IUserService
	{
		//Users
		Task<APIResponse<List<UserResponseDTO>>> GetUsers(PaginationDTO DTO);
		Task<APIResponse<UserResponseDTO>> GetUser(Guid id);
		Task<APIResponse<UserCompositeDTO>> CreateUser(UserCompositeDTO userCompositeDTO);
		Task<APIResponse<UpdateUserRequest>> UpdateUser(UpdateUserRequest  DTO);
		Task<APIResponse<UserAddressDTO>> UpdateAddress(UserAddressDTO DTO);
		Task<APIResponse<UserPayrollDTO>> UpdatePayroll(UserPayrollDTO DTO);
		Task<APIResponse<UserSocialDTO>> UpdateSocial( UserSocialDTO DTO);
		Task<APIResponse<UserFinancialDTO>> UpdateFinancial(UserFinancialDTO DTO);
		Task<APIResponse<UserContactDTO>> UpdateContact(UserContactDTO DTO);
		Task<APIResponse<UserEmploymentInformationDTO>> UpdateEmploymentInformation(UserEmploymentInformationDTO DTO);
	}
}